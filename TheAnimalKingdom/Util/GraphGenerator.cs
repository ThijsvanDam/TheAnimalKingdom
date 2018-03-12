namespace TheAnimalKingdom.Util
{
    public class GraphGenerator
    {
        public static SparseGraph<GraphEdge, NavGraphNode> Generate()
        {
            return new SparseGraph<GraphEdge, NavGraphNode>(false);
        }
    }
}