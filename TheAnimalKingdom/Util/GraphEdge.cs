namespace TheAnimalKingdom.Util
{
    public class GraphEdge
    {
        public int From { get; set; }
        public int To { get; set; }
        public double Cost { get; set; }

        public GraphEdge(int from, int to, double cost)
        {
            From = from;
            To = to;
            Cost = cost;
        }

        public GraphEdge(int from, int to) : this(from, to, cost: 1.0)
        {
        }

        public GraphEdge() : this(from: -1, to: -1, cost: 1.0)
        {
        }
    }
}