using System;
using System.Linq;
using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom.Util
{
    public class DijkstraSearch : GraphSearch
    {
        private readonly ItemType _requestedEntityType;
        
        public DijkstraSearch(SparseGraph graph, int source, ItemType type) : base(graph, source, 0)
        {
            _requestedEntityType = type;
        }

        public override PathResult CycleOnce()
        {           
            var current = Lowest();

            if (current == -1) return PathResult.NotFound;
            
            if (IsTerminationConditionSatisfied(current))
            {
                _target = current;
                _copiedNodes[current].IsTarget = true;
                return PathResult.Found;
            }

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
                    _copiedNodes[successor].H = 0;
                }

                _copiedNodes[successor].G = successorCurrentCost;
                _copiedNodes[successor].Prev = current;
            }
            _closed.Add(current);
            _open.Remove(current);

            return PathResult.InProgress;
        }

        private bool IsTerminationConditionSatisfied(int nodeIndex)
        {
            var nearbyEntity = _copiedNodes[nodeIndex].NearbyEntity;

            return (nearbyEntity == _requestedEntityType);
        }
    }
}