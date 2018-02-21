using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom
{
    public class World
    {
        public List<MovingEntity> Entities = new List<MovingEntity>();

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }

        public void Process()
        {
            Console.WriteLine("process");
        }


        public void Render()
        {
            Console.WriteLine("render");
        }
    }
}
