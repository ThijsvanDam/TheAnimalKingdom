﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class WanderBehaviour : SteeringBehaviour
    {

        public double CircleDistance = 100;
        public double CircleRadius = 80;
        public double LastAngle = 0;
        public double TurningAngle = (Math.PI * 2) / 20; // 360 / 72 = steps of 5 degrees

        public Vector2D cDist;
        public Vector2D vDest;
        public Vector2D steering;

        public WanderBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
            cDist = new Vector2D(0, 0);
            vDest = new Vector2D(0, 0);
            steering = new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            // Generate a random new angle to find a point on the circle. Do this with the TurningAngle steps.
            Random r = new Random();
            double currentAngle = LastAngle;
            if (r.Next(0, 60) % 2 == 1)
            {
                currentAngle += TurningAngle;
            }
            else
            {
                currentAngle -= TurningAngle;
            }

            // Current position of the entity.
            Vector2D currentPos = MovingEntity.VPos;

            // Direction the entity currently goes to with applied force.
            Vector2D direction = MovingEntity.VVelocity.Clone();

            // The direction the entity currently goes to, but normalized.
            Vector2D currentDirection = direction.Normalize();

            // The middle of the circle.
            // This is calculated by adding up the direction (multiplied by the distance) to our current position.
            cDist = currentPos.Clone().Add((currentDirection.Clone().Multiply(CircleDistance)));

            // Calculate our X and Y for our randomly generated new angle.
            double X = Math.Cos(currentAngle) * CircleRadius;
            double Y = Math.Sin(currentAngle) * CircleRadius;

            // Our new destination we want to travel towards.
            vDest = cDist.Clone().Add(new Vector2D(X, Y));
            // One problem: This is the destination from 0, 0.
            // So, to get the dest from our entities location, we can just substract our entities location from it.
            steering = vDest.Clone().Substract(MovingEntity.VPos);

            // Update our last angle so we can get a random angle next time.
            LastAngle = currentAngle;
            return steering;
        }

        public override void DrawBehavior(Graphics g)
        {
            // Middle of the circle
            g.FillEllipse(new SolidBrush(Color.Red), (int)cDist.X, (int)cDist.Y, (int)5, (int)5);

            // Circumference of the circle
            g.DrawEllipse(new Pen(Color.Blue), (int)(cDist.X - CircleRadius), (int)(cDist.Y - CircleRadius), (int)CircleRadius * 2, (int)CircleRadius * 2);

            // Draw the destination our entity has to steer towards.
            g.FillEllipse(new SolidBrush(Color.DeepPink), (int)(vDest.X - 3), (int)(vDest.Y - 3), 6, 6);
        }
    }
}
