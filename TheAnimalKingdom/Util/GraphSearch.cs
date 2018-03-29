using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheAnimalKingdom.Util
{
    public class AStarSearch
    {
        private readonly SparseGraph _graph;

        private readonly int _source;
        private readonly int _target;
        private readonly List<int> _open;
        private readonly List<int> _closed;
        private readonly List<NavGraphNode> _copiedNodes;
        
        private bool _foundPath;
        private List<int> _route;
        
        private Vector2D _targetLocation;

        public AStarSearch(SparseGraph graph, int source, int target)
        {
            _graph = graph;
            _source = source;
            _target = target;
            _copiedNodes = _graph.NodeList;
            _targetLocation = _copiedNodes[_target].Position;
            
            _open = new List<int>();
            _closed = new List<int>();
            
            _foundPath = Search();
            _route = ConstructPath();
        }
        
        /// <summary>
        /// Source: http://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
        /// </summary>
        /// <returns></returns>
        private bool Search()
        {
            var current = _source;
            
            _open.Add(current);

            while (_open.Count > 0)
            {
                current = Lowest();

                if (current == _target) return true;

                foreach (var connectedEdge in _graph.GetConnectedEdges(current))
                {
                    var successor = connectedEdge.To;
                    var successorCurrentCost = _copiedNodes[current].G + connectedEdge.Cost;
                    
                    if (_open.Contains(successor))
                    {
                        if(_copiedNodes[successor].G <= successorCurrentCost) continue;
                    } 
                    else if (_closed.Contains(successor))
                    {
                        if (_copiedNodes[successor].G <= successorCurrentCost) continue;
                        _closed.Remove(successor);
                        _open.Add(successor);
                    }
                    else
                    {
                        _open.Add(successor);
                        _copiedNodes[successor].H = CalculateManhattan(successor);
                    }

                    _copiedNodes[successor].G = successorCurrentCost;
                    _copiedNodes[successor].Prev = current;
                }
                _closed.Add(current);
                _open.Remove(current);
            }

            return false;
        }

        private int Lowest()
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

        private double CalculateManhattan(int nodeTo)
        {
            var currentLocation = _copiedNodes[nodeTo].Position;
            return Math.Abs(_targetLocation.X - currentLocation.X) +
                   Math.Abs(_targetLocation.Y - currentLocation.Y) /
                   15f; // ToDo: Remove this magic number (*Width set in generator)
        }

        private List<int> ConstructPath()
        {
            var routeStack = new Stack<int>();
            
            var current = _target;
            while (current != _source)
            {
                routeStack.Push(current);
                current = _copiedNodes[current].Prev;
            }
            routeStack.Push(_source);

            var route = new List<int>();

            while (routeStack.Count > 0)
            {
                route.Add(routeStack.Pop());
            }

            return route;
        }

        public void Render(Graphics g)
        {
            if (!_foundPath) return;

            for (int i = 0; i + 1 < _route.Count; i++)
            {
                var nodeFrom = _copiedNodes[_route[i]];
                var nodeTo = _copiedNodes[_route[i + 1]];
                
                g.DrawLine(new Pen(Color.Yellow), nodeFrom.Position.ToPoint(), nodeTo.Position.ToPoint());
            }
        }
    }
}