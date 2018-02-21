using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class BaseGameEntity
    {
        private static int NextValidID;

        public int ID { get; private set; }
        public Vector2D Pos { get; }
        public float Scale { get; }
        public float Bradius { get; }


        public BaseGameEntity(int id)
        {
            _setID(id);
        }

        private void _setID(int id)
        {
            if (id >= NextValidID)
            {
                ID = id;
                NextValidID++;
            }

        }

        public abstract void Update(double time_elapsed);

        public abstract void Render();
    }
}
