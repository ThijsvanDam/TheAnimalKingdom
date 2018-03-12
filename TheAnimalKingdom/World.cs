﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Behaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom
{
    public class World
    {
        public static int Intensity = 0;
        public static int MouseX = 0;
        public static int MouseY = 0;

        public List<BaseGameEntity> Entities = new List<BaseGameEntity>();
        public List<ObstacleEntity> Obstacles = new List<ObstacleEntity>();
        public int Width { get; set; }
        public int Height { get; set; }


        public World(int w, int h)
        {
            Width = w;
            Height = h;
            _populate();
        }

        private void _populate()
        {

            StaticEntity s1 = new StaticEntity(new Vector2D(0, 0), this); // the entity with ID=0 always follow the cursor so it has to be at the start.
            Gazelle g1 = new Gazelle(new Vector2D(0, 0), this);
            //            Gazelle g2 = new Gazelle(new Vector2D(30, 20), this);
            //            Gazelle g3 = new Gazelle(new Vector2D(25, 10), this);

            g1.SteeringBehaviours.ArriveOn(s1, 2);
            g1.SteeringBehaviours.ObstacleAvoidanceOn(1);

            Entities.AddRange(new List<BaseGameEntity>()
            {
                s1, g1,
            });




            #region Grid
            float fullSize = 25f;

            int[,] gameGrid = new int[20, 24]
            {//    1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21  22  23  24      
                {  0,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 1
                {  0,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 2 
                {  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 3
                {  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 4
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 5
                {  1,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 6
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 7
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 8
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 9
                {  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 10
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 11
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 12
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 13
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0  }, // 14
                {  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  0,  0,  1,  1,  0,  0,  0,  0,  0  }, // 15
                {  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  1,  0,  0,  1,  0,  0,  0,  0,  0  }, // 16
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 17
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0  }, // 18
                {  1,  1,  1,  1,  1,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0  }, // 19
                {  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 20
            };

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    if (gameGrid[i, j] == 1)
                    {
                        Obstacles.Add(new SquaredObstacle(new Vector2D((fullSize * j) + (25f/2f), (fullSize * i) + (fullSize / 2)), 1f, this));
                    }
                }
            }
            #endregion
        }

        public void Update(float timeElapsed)
        {
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

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }


        public void Render(Graphics g)
        {
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
