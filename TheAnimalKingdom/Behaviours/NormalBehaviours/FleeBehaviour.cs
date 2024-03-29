﻿using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.NormalBehaviours
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Enemy;
        public Vector2D steering;

        public FleeBehaviour(MovingEntity movingEntity, BaseGameEntity enemy) : base(movingEntity)
        {
            Enemy = enemy;
            steering = new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            if (CheckIfObstacleInVision())
            {
                return new Vector2D(0, 0);
            }
            
            Vector2D currentVelocity = MovingEntity.VVelocity;

            Vector2D desiredVelocity = Enemy.VPos.Clone().Substract(MovingEntity.VPos).Normalize();
            Vector2D newDesiredVelocity = new Vector2D(-desiredVelocity.X, -desiredVelocity.Y);
            newDesiredVelocity.Multiply(MovingEntity.DMaxForce);
            steering = newDesiredVelocity.Substract(currentVelocity);
            return steering;
        }

        public bool CheckIfObstacleInVision()
        {
            // TODO: Implement this, we forgot it.
            return false;
        }

        public override void DrawBehavior(Graphics g)
        {
            double x = MovingEntity.VPos.X;
            double y = MovingEntity.VPos.Y;

            g.DrawLine(new Pen(Color.Red), (int)x, (int)y, (int)(steering.X + x), (int)(steering.Y + y));
        }
    }
}
