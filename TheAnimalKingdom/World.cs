using System;
using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.CompositeGoals;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom
{
    public class World
    {
        public static int Intensity = 0;
        public static int MouseX = 0;
        public static int MouseY = 0;
        public static bool ShouldRenderGraph { get; set; }

        public List<BaseGameEntity> Entities = new List<BaseGameEntity>();
        public List<ObstacleEntity> Obstacles = new List<ObstacleEntity>();
        public int Width { get; set; }
        public int Height { get; set; }

        public PathManager PathManager { get; }
        
        public SparseGraph graph;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            
            PathManager = new PathManager(numCyclesPerUpdate:200);
            _populate();

            ShouldRenderGraph = false;
        }

        private void _populate()
        {
            FillObstaclesWithArray(GetFunPlayField());
            graph = GraphGenerator.FloodFill(world: this, startPosition: new Vector2D(15f, 15f));
            
            StaticEntity s1 = new StaticEntity(new Vector2D(0, 0), this); // the entity with ID=0 always follow the cursor so it has to be at the start.
            Gazelle g1 = new Gazelle(new Vector2D(20f, 20f), this);
 
            Entities.AddRange(new List<BaseGameEntity>()
            {
                s1, g1,
            });
        }

        public void Update(float timeElapsed)
        {
            PathManager.UpdateSearches();
    
            foreach (BaseGameEntity entity in Entities)
            {
                entity.Update(timeElapsed);
                if (entity.ID == 0)
                {
                    entity.VPos.X = MouseX;
                    entity.VPos.Y = MouseY;
                }
            }
        }

        private void FillObstaclesWithArray(int[,] array)
        {
            float fullSize = 25f;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 1)
                    {
                        Obstacles.Add(new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this));
                    }
                    if (array[i, j] == 2)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this);
                        a.Color = Color.Red;
                        Obstacles.Add(a);
                    }
                    if (array[i, j] == 3)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this);
                        a.Color = Color.Blue;
                        Obstacles.Add(a);
                    }
                }
            }
        }

        public int[,] GetFullWithObstaclesPlayField()
        {
            int[,] array = new int[20,24];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = 1;
                }
            }
            return array;
        }

        public int[,] GetFunPlayField()
        {
            return new int[20, 24]
            {//    1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21  22  23  24      
                {  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1  }, // 1
                {  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 2 
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1  }, // 3
                {  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1  }, // 4
                {  1,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1  }, // 5
                {  1,  0,  1,  1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1  }, // 6
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  1  }, // 7
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  1  }, // 8
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  1,  1,  1,  0,  1  }, // 9
                {  1,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0,  0,  1,  1,  1,  0,  1  }, // 10
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  1,  1,  2,  2,  1,  1,  0,  0,  0,  1,  1,  1,  0,  1  }, // 11
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 12
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 13
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  1  }, // 14
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  0,  0,  1,  1,  0,  0,  0,  0,  1  }, // 15
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  1,  0,  0,  1,  0,  0,  0,  0,  1  }, // 16
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1  }, // 17
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  1  }, // 18
                {  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3  }, // 19
                {  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3  }, // 20
            };
        }

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }

        public void StartPathFollowing(Vector2D target)
        {
            var entity = (MovingEntity)Entities[1];
            //entity.HashTagLifeGoal.AddSubgoal(new GoalMoveToPosition(entity, target));
            var staticObject = new StaticEntity(target, this);
            entity.SteeringBehaviours.ArriveOn(target, 1.0f);
            entity.DMaxForce = 20.0f;
        }


        public void Render(Graphics g)
        {
            if (ShouldRenderGraph)
            {
                graph.Render(g);
            }

            foreach (ObstacleEntity obstacleEntity in Obstacles)
            {
                obstacleEntity.Render(g);
            }
            foreach (BaseGameEntity baseGameEntity in Entities)
            {
                baseGameEntity.Render(g);
            }
        }
    }
}
