using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Text;


namespace PathFinding
{
    class ServerClass
    {

        private static string Id;
        private static string source;
        private static string target;
        private static string[] names;
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
       

        public static void  StartServer(dynamic jsonObj)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            Task listenTask = HandleIncomingConnections(jsonObj);
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
        private static async Task HandleIncomingConnections(dynamic jsonObj)
        {
            bool runServer = true;
      
            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

             if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/findPath"))
                {
                    Graph.InitLists(jsonObj);
                    names = Graph.GetNodes();
                    using (Stream body = req.InputStream) 
                    {
                        using (StreamReader reader = new StreamReader(body, req.ContentEncoding))
                        {
                            bool badReq = false;
                            string s = reader.ReadToEnd();
                            string[] mainResponse = s.Split('=', '&');
                            if(mainResponse.Length == 4 && Array.Exists(mainResponse, element => element.ToLower() == "source") && Array.Exists(mainResponse, element => element.ToLower() == "target")) 
                            {
                                int sourceIndex = Array.FindIndex(mainResponse, t => t.Equals("source", StringComparison.InvariantCultureIgnoreCase));
                                int targetIndex = Array.FindIndex(mainResponse, t => t.Equals("target", StringComparison.InvariantCultureIgnoreCase));
                                   
                                        if (Array.Exists(names, element => element == mainResponse[sourceIndex + 1]) && Array.Exists(names, element => element == mainResponse[targetIndex + 1]))
                                        {
                                                source = mainResponse[sourceIndex + 1];
                                                target = mainResponse[targetIndex + 1];

                                        }
                                        else
                                        {
                                            
                                             badReq = true;
                                            
                                        }
                                 
                                
                                if (!badReq )
                                {
                                    FindPath(jsonObj);

                                    if(Int16.Parse(Id) > 0)
                                    {
                                       
                                       
                                        byte[] data = Encoding.UTF8.GetBytes(String.Format(Id));
                                        resp.ContentType = "text/plain";
                                        resp.ContentEncoding = Encoding.UTF8;
                                        resp.ContentLength64 = data.LongLength;

                                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                                        resp.Close();
                                       
                                    } else
                                    {
                                        await HandleBadRequest(resp);
                                    }
                                   
                                    
                                }
                                else
                                {
                                    
                                    await HandleBadRequest(resp);
                                }
                                

                            }
                            else
                            {
                                await HandleBadRequest(resp);
                            }
                      
                        }
                    }
                    
                } else
                {
                    string response = "Invalid Request";
                    byte[] data = Encoding.UTF8.GetBytes(String.Format(response));
                    resp.ContentType = "text/plain";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }

               
            }
        }


        private static void FindPath(dynamic jsonObj)
        {
            Id = Graph.CreateGraph(source, target);

        }
        private static async Task HandleBadRequest(HttpListenerResponse resp)
        {
            
            string response = "Invalid Request Data";
            byte[] data = Encoding.UTF8.GetBytes(String.Format(response));
            resp.ContentType = "text/plain";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
    }
}
