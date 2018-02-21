using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAnimalKingdom.Entities
{
    class BaseGameEntity
    {
        private static int NextValidID;

        private int _ID;
        public int ID => _ID;


        public BaseGameEntity(int id)
        {
            _setID(id);
        }

        private void _setID(int id)
        {
            if (id >= NextValidID)
            {
                _ID = id;
                NextValidID++;
            }
            
        }
        public virtual void Update()
        {

        }
    }
}
