using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinding
{
    class Graph
    {
        static List<Edge> edgesList = new List<Edge>();
        static List<Node> nodeList = new List<Node>();
        private static string[] nodeNames = new string[100];
        private static int[,] mat = new int[nodeList.Count, nodeList.Count];
      


        public static void InitLists(dynamic jsonObj)
        {
            CreateEdgeList(jsonObj);
            CreateNodeList(jsonObj);
            CalculateCosts();
        }
        public static string CreateGraph(string source, string target)
        {
           
            nodeNames = new string[nodeList.Count];
            mat = new int[nodeList.Count, nodeList.Count];

            CreateMatrix();

            FindPath path = new FindPath();
            int id =  path.dijkstra(mat, source, target, nodeList);

            return id.ToString();
        }

        public static string[] GetNodes()
        {
            for (int i = 0; i < nodeNames.Length; i++)
            {
                nodeNames[i] = nodeList[i].Id;
            }
            return nodeNames;
        }

        private static void CreateEdgeList(dynamic jsonObj)
        {
            foreach (var edge in jsonObj.Edges)
            {
                foreach (var props in edge)
                {
                    string id = props.Id.ToString();
                    string source = props.Source.ToString();
                    string target = props.Target.ToString();
                    edgesList.Add(new Edge() { Id = id, Source = source, Target = target });


                }
            }
        }


        private static void CreateNodeList(dynamic jsonObj)
        {
            foreach (var node in jsonObj.Nodes)
            {
                foreach (var x in node)
                {
                    double xValue = x.Position.X.ToObject<double>();
                    double yValue = x.Position.Y.ToObject<double>();

                    nodeList.Add(new Node() { Id = x.Id.ToString(), XPosition = xValue, YPosition = yValue });
                }
            }

        }


        private static void CalculateCosts()
        {
            foreach (var singleNode in nodeList)
            {
                foreach (var item in edgesList)
                {
                    if (item.Source == singleNode.Id)
                    {
                       
                        foreach (var targetNode in nodeList)
                        {
                            if (targetNode.Id == item.Target)
                            {
                                item.SetCost(singleNode.XPosition, singleNode.YPosition, targetNode.XPosition, targetNode.YPosition);

                            }
                        }
                    }

                }
            }

        }

        private static void CreateMatrix()
        {

            for (int e = 0; e < edgesList.Count; e++)
            {
                for (int n = 0; n < nodeList.Count; n++)
                {
                    for (int tn = 0; tn < nodeList.Count; tn++)
                    {
                        if (nodeList[n].Id == edgesList[e].Source && nodeList[tn].Id == edgesList[e].Target)
                        {
                            mat[n, tn] = 10;
                        }

                    }

                }
            }


        }
    }
}
