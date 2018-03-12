namespace TheAnimalKingdom.Util
{
    public class GraphNode
    {
        public int Index { get; set; }

        public GraphNode(int idx)
        {
            Index = idx;
        }

        public GraphNode() : this(-1)
        {
        }
    }
}