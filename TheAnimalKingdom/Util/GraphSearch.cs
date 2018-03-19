using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TheAnimalKingdom.Util
{
    public enum State { Unvisited, Visited }
    
    public class AStarSearch
    {
        private SparseGraph _graph;
        
        private int _source;
        private int _target;
        private Vector2D _targetLocation;
        private List<int> _open;
        private List<int> _closed;

        private EdgeCollection _edges;

        private int _currentNode;
        
        public AStarSearch(SparseGraph graph, int source, int target)
        {
            _graph = graph;
            _source = source;
            _target = target;
            _targetLocation = graph.GetNode(target).Position;

            _edges = new EdgeCollection();

            _currentNode = source;

            Search();
        }
        
        private void Search()
        {
            var currentCost = 0;
            
            while (_open.Count > 0)
            {
                //Find connected edges and add to EdgeCollection with the heuristic cost
                var connectedEdges = _graph.GetConnectedEdges(_currentNode);

                foreach (var edge in connectedEdges)
                {
                    var to = edge.To == _currentNode ? edge.From : edge.To;
                    var heuristicCost = CalculateManhattan(to);
                    _edges.Add(edge: edge, addedCost: heuristicCost + 1.0); // ToDO: Find a way to add the cost of the previous edges
                }

                var lowestCostingEdge = _edges.LowestCost();
                var nextNode = lowestCostingEdge.To;
                
            }
        }

        private double CalculateManhattan(int nodeToIndex)
        {
            var currentLocation = _graph.GetNode(nodeToIndex).Position;
            return Math.Abs(_targetLocation.X - currentLocation.X) + Math.Abs(_targetLocation.Y - currentLocation.Y) / 15f; // ToDo: Remove this magic number (*Width set in generator)
        }

        private double CalculateCost()
        {
            return 0.0;
        }

        public void Render(Graphics g)
        {
//            if (!_foundPath) return;
//            
//            var nodeFrom = _graph.GetNode(_source);
//            var currentNodeIndex = _source;
//
//            while (currentNodeIndex != -1)
//            {
//                var nodeTo = _graph.GetNode(_route[currentNodeIndex]);
//                
//                if (nodeTo == null) break;
//                
//                g.DrawLine(new Pen(Color.Yellow), nodeFrom.Position.ToPoint(), nodeTo.Position.ToPoint());
//
//                nodeFrom = nodeTo;
//                currentNodeIndex = nodeTo.Index;
//            }
        }
    }
}