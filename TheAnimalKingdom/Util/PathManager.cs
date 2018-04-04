using System.Collections.Generic;

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
                    UnRegister(currentPath);
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
    }
}