using System;
using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.FuzzyLogic;
using TheAnimalKingdom.FuzzyLogic.FuzzySets;
using TheAnimalKingdom.FuzzyLogic.FuzzyTerms;
using TheAnimalKingdom.Util;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheAnimalKingdom.Behaviours;

namespace TheAnimalKingdom
{
    public class World
    {
        public static int MouseX = 0;
        public static int MouseY = 0;
        public static bool GodMode { get; set; }

        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<ObstacleEntity> Obstacles = new List<ObstacleEntity>();
        public int Width { get; set; }
        public int Height { get; set; }

        public PathManager PathManager { get; }
        
        public SparseGraph graph;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            
            PathManager = new PathManager(numCyclesPerUpdate:50);
            _populate();

            GodMode = false;
        }

        private void _populate()
        {
            FillObstaclesWithArray(GetFunPlayField());
            graph = GraphGenerator.FloodFill(world: this, startPosition: new Vector2D(50f, 50f));
            GraphGenerator.SetNearestItems(this);
            
            Lion l1 = new Lion(new Vector2D(50f, 50f), this);
            Lion l2 = new Lion(new Vector2D(60f, 60f), this);
            
            Gazelle g1 = new Gazelle(new Vector2D(200f, 200f), this);
            Gazelle g2 = new Gazelle(new Vector2D(250f, 250f), this);
 
            Entities.AddRange(new List<MovingEntity>()
            {
                g1, 
                l1, 
                g2, 
                l2
            });

            Random r = new Random();
            for (int i = 0; i < 1; i++)
            {
                Entities.Add(new Gazelle(new Vector2D(r.Next(200, 400), r.Next(200, 400)), this));
            }

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
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this, ItemType.Grass);
                        Obstacles.Add(a);
                    }
                    if (array[i, j] == 3)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this, ItemType.Water);
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
                {  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  2,  2,  2,  1  }, // 2 
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  2,  2,  1  }, // 3
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  1  }, // 4
                {  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 5
                {  1,  1,  1,  1,  1,  1,  2,  2,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 6
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 7
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 8
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 9
                {  1,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  2,  2,  2,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 10
                {  1,  0,  0,  0,  2,  1,  0,  0,  0,  0,  2,  2,  2,  2,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 11
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 12
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 13
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 14
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  2,  2,  2,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 15
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  2,  1,  2,  2,  0,  0,  0,  0,  0,  0,  0,  1  }, // 16
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  1,  1,  2,  2,  0,  0,  0,  0,  0,  0,  1  }, // 17
                {  1,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  1  }, // 18
                {  1,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  1  }, // 19
                {  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1  }, // 20
            };
        }

        public void Render(Graphics g)
        {

            foreach (ObstacleEntity obstacleEntity in Obstacles)
            {
                obstacleEntity.Render(g);
            }
            if (GodMode)
            {
                graph.Render(g);
            }
            foreach (BaseGameEntity baseGameEntity in Entities)
            {
                baseGameEntity.Render(g);
            }
        }
    }
}
