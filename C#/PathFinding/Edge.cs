using System;


namespace PathFinding
{
    class Edge
    {
        public string Id { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        private double Cost;


        public Edge()
        {

        }

        public Edge(string id, string source, string target)
        {
            this.Id = id;
            this.Source = source;
            this.Target = target;
        }

        public void SetCost(double sourceX, double sourceY, double targetX, double targetY)
        {
            double xDiffSq = Math.Pow(sourceX - targetX, 2);
            double yDiffSq = Math.Pow(sourceY - targetY, 2);
            this.Cost = Math.Sqrt(xDiffSq + yDiffSq);

        }

        public double GetCost()
        {
            return Cost;
        }
    }
}


