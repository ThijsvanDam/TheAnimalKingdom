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
        public List<MovingEntity> Entities = new List<MovingEntity>();
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
            List<Gazelle> gazellig = new List<Gazelle>();

            Gazelle g1 = new Gazelle(new Vector2D(10, 10), this);

            Gazelle g2 = new Gazelle(new Vector2D(30, 20), this);

            Gazelle g3 = new Gazelle(new Vector2D(25, 10), this);

            gazellig.Add(g1);
            gazellig.Add(g2);
            gazellig.Add(g3);

            Entities.AddRange(gazellig);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity entity in Entities)
            {
                entity.Update(timeElapsed);
            }
        }

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }


        public void Render(Graphics g)
        {
            foreach (MovingEntity entity in Entities)
            {
                entity.Render(g);
            }
        }
    }
}
