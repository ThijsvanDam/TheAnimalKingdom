using System;
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
        public static int MouseX = 300;
        public static int MouseY = 300;

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
            List<BaseGameEntity> allesz = new List<BaseGameEntity>();

            Gazelle g1 = new Gazelle(new Vector2D(200, 200), this);

            Gazelle g2 = new Gazelle(new Vector2D(30, 20), this);

            Gazelle g3 = new Gazelle(new Vector2D(25, 10), this);

//            
//
            StaticEntity s1 = new StaticEntity(new Vector2D(MouseX, MouseY), this);

            RoundObstacle o1 = new RoundObstacle(new Vector2D(400, 200), 2f, this);
            RoundObstacle o2 = new RoundObstacle(new Vector2D(200, 100), 3f, this);
            RoundObstacle o3 = new RoundObstacle(new Vector2D(300, 300), 5f, this);

//            Gazelle g4 = new Gazelle(new Vector2D(40, 55), this);

            g1.SteeringBehaviours.WanderOn(1);
            g2.SteeringBehaviours.WanderOn(1);
//            g3.SteeringBehaviours.WanderOn(1);
            g3.SteeringBehaviours.ArriveOn(s1, 1);
            g1.SteeringBehaviours.ObstacleAvoidanceOn(1);
            //            g1.SteeringBehaviours.ArriveOn(s1, 1);
            //            g1.SteeringBehaviours.SeekOn(s1, 1);

            allesz.Add(o1);
            allesz.Add(o2);
            allesz.Add(o3);
            allesz.Add(g1);
            allesz.Add(g2);
            allesz.Add(g3);
            allesz.Add(s1);
            //            allesz.Add(g4);
            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {
                Gazelle g = new Gazelle(new Vector2D(r.NextDouble(), r.NextDouble()), this);
                g.SteeringBehaviours.WanderOn(1);
                g.SteeringBehaviours.ArriveOn(s1, 1);
                allesz.Add(g);
            }

            Entities.AddRange(allesz);
        }

        public void Update(float timeElapsed)
        {
            foreach (BaseGameEntity entity in Entities)
            {
                entity.Update(timeElapsed);
                if (entity.ID == 3)
                {
                    entity.VPos.X = MouseX;
                    entity.VPos.Y = MouseY;
//                    Console.WriteLine(entity.ID + ": " + entity.VPos.X);
                }
            }
        }

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }


        public void Render(Graphics g)
        {
            foreach (BaseGameEntity entity in Entities)
            {
                entity.Render(g);
            }
        }
    }
}
