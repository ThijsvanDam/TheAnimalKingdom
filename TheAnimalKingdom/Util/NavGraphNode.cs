using System;

namespace TheAnimalKingdom.Util
{
    public class NavGraphNode : GraphNode
    {
        public Vector2D Position { get; }
        public bool IsTarget { get; set; }
        
        // For algorithm
        public double G { get; set; }
        public double H { get; set; }
        public double T => G + H;
        public int Prev { get; set; }

        public NavGraphNode(int idx, Vector2D position) : base(idx)
        {
            Position = position;
            G = 0;
            H = 0;
        }
    }
}