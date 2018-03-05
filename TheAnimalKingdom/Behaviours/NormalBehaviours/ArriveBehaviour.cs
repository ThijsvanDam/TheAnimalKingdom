using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.NormalBehaviours
{
    public class ArriveBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Goal;

        public Vector2D desiredVelocity;

        public ArriveBehaviour(MovingEntity movingEntity, BaseGameEntity goal) : base(movingEntity)
        {
            Goal = goal;
            desiredVelocity = new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            double deceleration = MovingEntity.DDeceleration;

            Vector2D toTarget = Goal.VPos.Clone().Substract(MovingEntity.VPos);

            double distance = toTarget.Length();



            if (distance > 0.05)
            {
                const double decelerationTweaker = 1;
                double speed = distance / (deceleration * decelerationTweaker);

                speed = speed <= MovingEntity.DMaxSpeed ? speed : MovingEntity.DMaxSpeed;

                desiredVelocity = toTarget.Multiply(speed / distance);
                Vector2D currentVelocity = MovingEntity.VVelocity;
                desiredVelocity.Substract(currentVelocity);
            }
            else
            {
                desiredVelocity = new Vector2D(0, 0);
            }
            return desiredVelocity;
        }

        public override void DrawBehavior(Graphics g)
        {
            double x = MovingEntity.VPos.X;
            double y = MovingEntity.VPos.Y;

            g.DrawLine(new Pen(Color.Red), (int)x, (int)y, (int)(desiredVelocity.X + x), (int)(desiredVelocity.Y + y));
        }
    }
}
