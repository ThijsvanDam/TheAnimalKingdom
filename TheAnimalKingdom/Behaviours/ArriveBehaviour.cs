using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class ArriveBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Goal;

        public ArriveBehaviour(MovingEntity movingEntity, BaseGameEntity goal) : base(movingEntity)
        {
            Goal = goal;
        }

        public override Vector2D Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
