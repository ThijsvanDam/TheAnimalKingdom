using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Goal;

        public SeekBehaviour(MovingEntity movingEntity, BaseGameEntity goal) : base(movingEntity)
        {
            Goal = goal;
        }

        public override Vector2D Calculate()
        {
            Vector2D currentVelocity = MovingEntity.VVelocity;

//            Vector2D desiredVelocity = new Vector2D(Goal.VPos.X - MovingEntity.VPos.X, Goal.VPos.Y - MovingEntity.VPos.Y).Normalize();

            Vector2D desiredVelocity = Goal.VPos.Clone().Substract(MovingEntity.VPos).Normalize();
            desiredVelocity.Multiply(MovingEntity.DMaxSpeed);
            return desiredVelocity.Substract(currentVelocity);
        }

        public override void DrawBehavior(Graphics g)
        {
//            throw new NotImplementedException();
        }
    }
}
