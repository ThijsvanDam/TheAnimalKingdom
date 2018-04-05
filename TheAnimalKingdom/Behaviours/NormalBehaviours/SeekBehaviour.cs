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
    public class SeekBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Goal;
        public Vector2D Steering;

        public SeekBehaviour(MovingEntity movingEntity, BaseGameEntity goal) : base(movingEntity)
        {
            Goal = goal;
            Steering = new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            Vector2D currentVelocity = MovingEntity.VVelocity;

            Vector2D desiredVelocity = Goal.VPos.Clone().Substract(MovingEntity.VPos).Normalize();
            desiredVelocity.Multiply(MovingEntity.DMaxForce);
            Steering = desiredVelocity.Substract(currentVelocity);
            return Steering;
        }

        public override void DrawBehavior(Graphics g)
        {
            double x = MovingEntity.VPos.X;
            double y = MovingEntity.VPos.Y;

            g.DrawLine(new Pen(Color.Red), (int)x, (int)y, (int)(Steering.X + x), (int)(Steering.Y + y));
        }
    }
}
