using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.BaseBehaviours
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }

        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity movingEntity)
        {
            MovingEntity = movingEntity;
        }

        public abstract void DrawBehavior(Graphics g);
    }
}
