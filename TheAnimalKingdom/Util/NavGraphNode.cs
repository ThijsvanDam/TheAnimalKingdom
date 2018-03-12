namespace TheAnimalKingdom.Util
{
    public class NavGraphNode : GraphNode
    {
        public Vector2D Position { get; }

        public NavGraphNode(int idx, Vector2D position) : base(idx)
        {
            Position = position;
        }
    }
}