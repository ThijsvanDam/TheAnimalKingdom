using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class ObstacleEntity : BaseGameEntity
    {
        protected ObstacleEntity(Vector2D position, float size, World world) : base(position, world)
        {
            Bradius = (size * 25) / 2;
            Bradius = Bradius;
            world.Obstacles.Add(this);
        }
    }
}
