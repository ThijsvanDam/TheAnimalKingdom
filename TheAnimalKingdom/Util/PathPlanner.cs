using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom.Util
{
    public class PathPlanner
    {
        private GraphSearch _search;
        private readonly MovingEntity _owner;

        public PathPlanner(MovingEntity owner)
        {
            _owner = owner;
        }

        public void RequestPathToTarget(Vector2D target)
        {
            var sourceNodeIndex = _owner.World.graph.FindNearestNode(_owner.VPos).Index;
            var targetNodeIndex = _owner.World.graph.FindNearestNode(target).Index;
            _search = new AStarSearch(graph: _owner.World.graph, source: sourceNodeIndex, target: targetNodeIndex);
            _owner.FindPathResult = PathResult.InProgress;
        }

        public void RequestPathToItem(ItemType itemType)
        {
            var sourceNodeIndex = _owner.World.graph.FindNearestNode(_owner.VPos).Index;
            _search = new DijkstraSearch(graph: _owner.World.graph, source: sourceNodeIndex, type: itemType);
            _owner.FindPathResult = PathResult.InProgress;
        }
        
        public PathResult CycleOnce()
        {
            if (_search == null) return PathResult.InProgress;
            
            var result = _search.CycleOnce();

            if (result == PathResult.Found || result == PathResult.NotFound)
            {
                _owner.FindPathResult = result;
            }

            return result;
        }

        public Stack<NavGraphNode> GetRoute()
        {
            return _search.GetRoute();
        }

        public void Render(Graphics g)
        {
            _search?.Render(g);
        }
    }
}