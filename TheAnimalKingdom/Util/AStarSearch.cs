using System;
using System.Linq;

namespace TheAnimalKingdom.Util
{
    public class AStarSearch : GraphSearch
    {
        public AStarSearch(SparseGraph graph, int source, int target) : base(graph, source, target)
        {            
        }

        /// <summary>
        /// Source: http://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
        /// and book Programming Game AI By Example
        /// </summary>
        /// <returns></returns>
        public override PathResult CycleOnce()
        {
            var current = Lowest();

            if (current == _target) return PathResult.Found;

            if (current == -1) return PathResult.NotFound;
            

            var connectedEdges = _edges.Where(x => x.From == current);
                
            foreach (var connectedEdge in connectedEdges)
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

            return PathResult.InProgress;
        }
        
        private double CalculateManhattan(int nodeTo)
        {
            var currentLocation = _copiedNodes[nodeTo].Position;
            return Math.Abs(_targetLocation.X - currentLocation.X) +
                   Math.Abs(_targetLocation.Y - currentLocation.Y) /
                   15f;
        }
    }
}