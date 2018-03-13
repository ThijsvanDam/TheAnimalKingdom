using System.Collections.Generic;

namespace TheAnimalKingdom.Util
{
    public static class GraphGenerator
    {
        private const float Width = 30f;
        
        public static SparseGraph FloodFill(World world, Vector2D startPosition)
        {
            var graph = new SparseGraph(false);
            
            var firstNode = new NavGraphNode(
                idx: graph.NextNodeIndex, 
                position: startPosition
            );
            
            graph.AddNode(firstNode);

            FloodFill(graph: graph, world: world, node: firstNode);

            return graph;
        }

        private static void FloodFill(SparseGraph graph, World world, NavGraphNode node)
        {            
            /*
             * This method should create all edges and neighbouring nodes that are possible
             * 
             * C:     Current node
             * 1 - 8: Neighbouring nodes for which the edges/nodes should be generated if not colliding with object
             * W:     Width or space between nodes
             * 
             *       1 -- 2 -- 3
             *       | \  |  / |
             *       |  \ | /  |
             *       4 -- C -- 5
             *       |  / | \  |
             *       | /  |  \ |
             *       6 -- 7 -- 8
             *
             *
             *  1 (c.X - W, c.Y - W)
             *  2 (c.X    , c.Y - W)
             *  3 (c.X + W, c.Y - W)
             *  4 (c.X - W, c.Y    )
             *  5 (c.X + W, c.Y    )
             *  6 (c.X - W, c.Y + W)
             *  7 (c.X    , c.Y + W)
             *  8 (c.X + W, c.Y + W)
             */
            
            
            List<Vector2D> neighbouringPoints = new List<Vector2D>()
            {
                new Vector2D(x: node.Position.X - Width, y: node.Position.Y - Width), // 1
                new Vector2D(x: node.Position.X        , y: node.Position.Y - Width), // 2
                new Vector2D(x: node.Position.X + Width, y: node.Position.Y - Width), // 3
                
                new Vector2D(x: node.Position.X - Width, y: node.Position.Y        ), // 4
                new Vector2D(x: node.Position.X + Width, y: node.Position.Y        ), // 5
                
                new Vector2D(x: node.Position.X - Width, y: node.Position.Y + Width), // 6
                new Vector2D(x: node.Position.X        , y: node.Position.Y + Width), // 7
                new Vector2D(x: node.Position.X + Width, y: node.Position.Y + Width), // 8
            };

            foreach (var point in neighbouringPoints)
            {
                // Do not add node if it collides with obstacle and skip to next node
                if (world.Obstacles.Exists(o => 
                    point.X >= o.VPos.X - o.Bradius && point.X <= o.VPos.X + o.Bradius
                    &&
                    point.Y >= o.VPos.Y - o.Bradius && point.Y <= o.VPos.Y + o.Bradius
                )) continue;
                
                // Also skip this node if it is outside of the world boundaries
                if (point.X < 0 || point.Y < 0 || point.X > world.Width || point.Y > world.Height) continue;
                
                // Try to get the neighbouring node and create a new node if it doesn't exist
                var neighbour = graph.GetNode(point.X, point.Y) ?? new NavGraphNode(graph.NextNodeIndex, point);
                
                // If the index is equal to the NextNodeIndex, it means that the node was created here and thus should
                // be added to the graph. If it isn't equal, we got an existing node from the graph earlier.
                var neighbourDidAlreadyExist = neighbour.Index != graph.NextNodeIndex;
                if (!neighbourDidAlreadyExist)
                {
                    graph.AddNode(neighbour);
                }
                
                // Check if edge exists, and add one if that is not the case
                if (!graph.EdgeList.Exists(x => x.From == node.Index && x.To == neighbour.Index))
                {
                    graph.AddEdge(new GraphEdge(from: node.Index, to: neighbour.Index));
                }

                // Go on with the neighbour node, if it didn't exist yet, otherwise skip to the next node
                if(neighbourDidAlreadyExist) continue;
                FloodFill(graph: graph, world: world, node: neighbour);
            }
        }
    }
}