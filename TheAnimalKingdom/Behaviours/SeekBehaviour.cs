using System;
using System.Collections.Generic;
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
//            Console.WriteLine();

//            string x = (MovingEntity.VPos.X - Goal.VPos.X).ToString();
//            string y = (MovingEntity.VPos.Y - Goal.VPos.Y).ToString();
//            Console.WriteLine("FORCE:" + x + " + " + y);
//
//            return new Vector2D(-MovingEntity.VPos.X + Goal.VPos.X, -MovingEntity.VPos.Y + Goal.VPos.Y);

            Vector2D currentVelocity = MovingEntity.VVelocity;

            Vector2D desiredVelocity = new Vector2D(Goal.VPos.X - MovingEntity.VPos.X, Goal.VPos.Y - MovingEntity.VPos.Y).Normalize();
            desiredVelocity.Multiply(MovingEntity.DMaxSpeed);
            return desiredVelocity.Substract(currentVelocity);
        }
    }
}
