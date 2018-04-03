using System.Collections.Generic;
using System.Drawing;

namespace TheAnimalKingdom.Util
{
    public class PathManager
    {
        private List<PathPlanner> _searchRequests;
        private readonly int _numSearchCyclesPerUpdate;

        public PathManager(int numCyclesPerUpdate)
        {
            _numSearchCyclesPerUpdate = numCyclesPerUpdate;
            _searchRequests = new List<PathPlanner>();
        }

        public void UpdateSearches()
        {
            var numSearchCyclesRemaining = _numSearchCyclesPerUpdate;

            foreach (var currentPath in _searchRequests)
            {
                if (numSearchCyclesRemaining == 0 || _searchRequests.Count == 0) break;
               
                var result = currentPath.CycleOnce();

                if (result == PathResult.Found || result == PathResult.NotFound)
                {
                    _searchRequests.Remove(currentPath);
                }

                numSearchCyclesRemaining--;
            }
        }

        public void Register(PathPlanner planner)
        {
            _searchRequests.Add(planner);
        }

        public void UnRegister(PathPlanner planner)
        {
            _searchRequests.Remove(planner);
        }

        public void Render(Graphics g)
        {
            foreach (var searchRequest in _searchRequests)
            {
                searchRequest.Render(g);
            }
        }
    }
}