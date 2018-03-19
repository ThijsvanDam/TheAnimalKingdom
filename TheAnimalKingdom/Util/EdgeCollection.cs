using System.Collections.Generic;

namespace TheAnimalKingdom.Util
{
    public class EdgeCollection
    {
        /// <summary>
        /// Efficient storage for edges when lowest costing edge is important
        /// </summary>

        private List<GraphEdge> _edges;
        private int _indexOfLowestCost;
        private double _lowestCost;
        private int _currentIndex;
        
        public EdgeCollection()
        {
            _edges = new List<GraphEdge>();
            _indexOfLowestCost = -1;
            _currentIndex = 0;
        }

        public void Search()
        {
            
        }
        
        /// <summary>
        /// Add an edge to the collection and updates the lowest cost if needed
        /// </summary>
        /// <param name="edge">The edge to add</param>
        /// <param name="addedCost">Optional: the cost to add according to the specified algorithm</param>
        public void Add(GraphEdge edge, double addedCost = 0)
        {
            _edges[_currentIndex] = edge;
            _edges[_currentIndex].AddedCost = addedCost;
            if (_edges[_currentIndex].TotalCost < _lowestCost)
            {
                _indexOfLowestCost = _currentIndex;
                _lowestCost = _edges[_currentIndex].TotalCost;
            }

            _currentIndex++;
        }
        
        /// <summary>
        /// Returns the lowest costing edge
        /// </summary>
        /// <returns>Edge with lowest cost</returns>
        public GraphEdge LowestCost()
        {
            if (_indexOfLowestCost < 0) return null;
            
            return _edges[_indexOfLowestCost];
        }
        
    }
}