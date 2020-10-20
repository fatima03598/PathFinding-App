using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace PathFinding
{
    class Node
    {
        public string Id { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public bool SptSet { get; set; }
        public int Dist { get; set; }
        public int parent { get; set; }

        public Node()
        {
            
        }
        public Node(string id, double xPosition, double yPosition)
        {
            Id = id;
            XPosition = xPosition;
            YPosition = yPosition;
           
        }


        
    }
}
