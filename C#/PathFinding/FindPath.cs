using System;
using System.Collections.Generic;


namespace PathFinding
{
    class FindPath
    {
        private int NO_PARENT = -1;
        private string path;
        private int Id;
        public int dijkstra(int[,] matrix, string source, string target, List<Node> nList)
        {
            int nNodes = nList.Count;

            for (int i = 0; i < nNodes; i++)
            {
                if (nList[i].Id != source)
                {
                    nList[i].Dist = int.MaxValue;
                    nList[i].SptSet = false;

                }
                else
                {
                    nList[i].Dist = 0;
                    nList[i].parent = NO_PARENT;
                }
            }


            for (int i = 1; i < nNodes; i++)
            {
                int nearestNode = -1;
                int shortestDistance = int.MaxValue;

                for (int nIndex = 0; nIndex < nNodes; nIndex++)
                {
                    if (!nList[nIndex].SptSet && nList[nIndex].Dist < shortestDistance)
                    {
                        nearestNode = nIndex;
                        shortestDistance = nList[nIndex].Dist;
                    }
                }

                nList[nearestNode].SptSet = true;
                if (nList[nearestNode].Id == target)
                {

                   
                    Console.Write("Vertex\t Distance\tPath");
                    Console.Write("\n" + source + " -> ");
                    Console.Write(nList[nearestNode].Id + " \t\t ");
                    Console.Write(nList[nearestNode].Dist + "\t\t");
                    
                    printPath(nearestNode, nList);
                     Id = AddToTable(source, nList[nearestNode].Id, nList[nearestNode].Dist);
                    
                    break;
                    
                }

                for (int n = 0; n < nNodes; n++)
                {
                    int edgeDistance = matrix[nearestNode, n];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < nList[n].Dist))
                    {
                        nList[n].parent = nearestNode;
                        nList[n].Dist = shortestDistance + edgeDistance;
                    }
                }

            }
            return Id;

        }
        
        
        private void printPath(int v, List<Node> nList)
        {
            if (v == NO_PARENT)
            {
                return;
            }

            printPath(nList[v].parent, nList);
            if (nList[v].parent != -1)
            {
                Console.Write(nList[nList[v].parent].Id + " ");
                path += nList[nList[v].parent].Id + ",";
                
            }
           

        }

        private int AddToTable(string source, string target, int distance)
        {
            int id = SqLite.CreateRow(source, target, distance, path );
            return id;
        }
    }
}
