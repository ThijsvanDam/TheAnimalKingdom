namespace TheAnimalKingdom.Util
{
    public class NavGraphNode : GraphNode
    {
        private Vector2D Position;

        public NavGraphNode(int idx, Vector2D position) : base(idx)
        {
            Position = position;
        }
    }
}