using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\fatim\Desktop\MyPractice\C#\SChallenge\BasicJSON.json";



            string json = File.ReadAllText(path);
            dynamic jsonObj = JsonConvert.DeserializeObject(json); //deserialise json

            ServerClass.StartServer(jsonObj);
            //Graph.InitLists(jsonObj);
            // Graph.CreateGraph(jsonObj, "Node0_6", "Node0_4");

            Console.ReadLine();

           
        }
    }
}
