using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom.Util
{
    public class PathPlanner
    {
        public Stack<NavGraphNode> Route
        {
            get { return _search.GetRoute(); }
        }

        private GraphSearch _search;
        private readonly MovingEntity _owner;
        private Stack<NavGraphNode> _route;

        public PathPlanner(MovingEntity owner)
        {
            _owner = owner;
        }

        public void RequestPathToTarget(Vector2D target)
        {
            var sourceNodeIndex = _owner.World.graph.FindNearestNode(_owner.VPos).Index;
            var targetNodeIndex = _owner.World.graph.FindNearestNode(target).Index;
            _search = new AStarSearch(graph: _owner.World.graph, source: sourceNodeIndex, target: targetNodeIndex);
            _route = null;
            _owner.FindPathResult = PathResult.InProgress;
            _owner.World.PathManager.Register(this);
        }

        public void RequestPathToItem(ItemType itemType)
        {
            var sourceNodeIndex = _owner.World.graph.FindNearestNode(_owner.VPos).Index;
            _search = new DijkstraSearch(graph: _owner.World.graph, source: sourceNodeIndex, type: itemType);
            _route = null;
            _owner.FindPathResult = PathResult.InProgress;
            _owner.World.PathManager.Register(this);
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
    }
}