using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace TheAnimalKingdom.Util
{  
    public abstract class GraphSearch
    {        
        protected int _source;
        protected int _target;
        protected List<int> _open;
        protected List<int> _closed;
        private Stack<NavGraphNode> _route;

        protected readonly List<NavGraphNode> _copiedNodes;
        protected readonly List<GraphEdge> _edges;
        
        protected bool _foundPath;
        
        protected Vector2D _targetLocation;

        public GraphSearch(SparseGraph graph, int source, int target)
        {            
            _copiedNodes = graph.NodeList;
            _edges = graph.EdgeList;
            _target = target;
            _source = source;
            _targetLocation = _copiedNodes[_target].Position;
            _open = new List<int>();
            _closed = new List<int>();
            
            _open.Add(_source);
        }

        public abstract PathResult CycleOnce();

        protected int Lowest()
        {
            var lowestCost = double.MaxValue;
            var lowest = -1;
            
            foreach (var nodeIndex in _open)
            {                
                if (_copiedNodes[nodeIndex].T < lowestCost)
                {
                    lowest = nodeIndex;
                    lowestCost = _copiedNodes[nodeIndex].T;
                }
            }

            return lowest;
        }

        public Stack<NavGraphNode> GetRoute()
        {
            if (_route == null)
            {
                var routeStack = new Stack<NavGraphNode>();
            
                var current = _target;
                while (current != _source)
                {
                    routeStack.Push(_copiedNodes[current]);

                    if (current == _copiedNodes[current].Prev) break; //Prevent looping if path hasn't been completely found yet
                    
                    current = _copiedNodes[current].Prev;
                }
                routeStack.Push(_copiedNodes[_source]);

                _route = routeStack;
            }
            
            return _route;
        }

        public void Render(Graphics g)
        {
            Stack<NavGraphNode> copiedRoute = GetRoute();
            
            if (!_foundPath) return;

            while (copiedRoute.Count > 1)
            {
                var nodeFrom = copiedRoute.Pop();
                var nodeTo = copiedRoute.Peek();
                
                g.DrawLine(new Pen(Color.Yellow), nodeFrom.Position.ToPoint(), nodeTo.Position.ToPoint());
            }
        }
    }
}