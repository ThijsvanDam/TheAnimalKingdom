using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class BaseGameEntity
    {
        private static int NextValidID = 0;

        public int ID { get; private set; }
        public World World { get; set; }
        public Vector2D VPos { get; }
        public float Bradius { get; set; }
        public Color Color;


        public BaseGameEntity(Vector2D position, World world)
        {
            _setID();
            VPos = position;
            World = world;
        }

        private void _setID()
        {
            ID = NextValidID;
            NextValidID++;
        }

        public abstract void Update(float time_elapsed);
        
        public abstract void Render(Graphics g);
    }
}
