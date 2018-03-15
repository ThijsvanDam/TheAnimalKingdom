using System;

namespace TheAnimalKingdom.Util
{
    public class NavGraphNode : GraphNode
    {
        public Vector2D Position { get; }
        public bool IsTarget { get; set; }

        public NavGraphNode(int idx, Vector2D position) : base(idx)
        {
            Position = position;
        }
    }
}