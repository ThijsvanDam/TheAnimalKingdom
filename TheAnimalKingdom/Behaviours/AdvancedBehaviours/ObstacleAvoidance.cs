using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.AdvancedBehaviours
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        public double RectangleDistance;
        public double RectangleWidth;
        public double MinimumRectangleLength;

        public Vector2D rDist;
        public List<Vector2D> Rectangle;

        public ObstacleAvoidance(MovingEntity movingEntity) : base(movingEntity)
        {
            RectangleDistance = 0;
            RectangleWidth = 10;
            MinimumRectangleLength = 5;
            rDist = new Vector2D(0, 0);

            Rectangle = new List<Vector2D>()
            {
                new Vector2D(0, 0),
                new Vector2D(0, 0),
                new Vector2D(0, 0),
                new Vector2D(0, 0)
            };
        }

        public override Vector2D Calculate()
        {
            // All obstacles and its position
            List<ObstacleEntity> obstacles = MovingEntity.World.Obstacles;

            // Current position of the entity.
            Vector2D currentPos = MovingEntity.VPos;

            // Direction the entity currently goes to with applied force.
            Vector2D direction = MovingEntity.VVelocity.Clone();

            // The direction the entity currently goes to, but normalized.
            Vector2D currentDirection = direction.Normalize();

            double speed = MovingEntity.VVelocity.Length() * 10;
            double maxSpeed = MovingEntity.DMaxSpeed;

            double extra = ((speed / maxSpeed) * MinimumRectangleLength);

            RectangleDistance = MinimumRectangleLength + extra;

            // The square and its distance
            rDist = currentPos.Clone().Add((currentDirection.Clone().Multiply(RectangleDistance)));

            // Create rectangle
            Rectangle = GetRectangle();
            
            var agentHeading = Math.Atan2(MovingEntity.VVelocity.X, MovingEntity.VVelocity.Y);
               
            // Get all obstacles within range
            List<ObstacleEntity> obstaclesWithinRange = FindObstaclesWithinRange();
            
            Vector2D totalForce = new Vector2D();

            foreach (var obstacle in obstaclesWithinRange)
            {
                // All obstacles that are within range
                var objectToLocalSpace = ToLocalSpace(obstacle.VPos, agentHeading);
                
                // Once we have all obstacles within our range, translate them to the local space and check if they are in
                // front of the moving entity  
                if (objectToLocalSpace.Y > 0)
                {
                    // All obstacles that are within range AND in front of the moving entity
                    MovingEntity.World.Obstacles[obstacle.ID].Color = Color.Red;
                                       
                    var severity = (10 / Vector2D.DistanceSquared(new Vector2D(0,0), objectToLocalSpace)) * speed;
                    
                    // Calculate the forces that should be added to change direction
                    var forceX = - objectToLocalSpace.X * severity;
                    var forceY = - objectToLocalSpace.Y * severity;
            
                    //And add it to the total force
                    totalForce.Add(new Vector2D(forceX ,forceY));
                }
                else
                {
                    MovingEntity.World.Obstacles[obstacle.ID].Color = Color.Green;
                }
            }
            return totalForce;
        }

        private List<Vector2D> GetRectangle()
        {
            Vector2D direction = MovingEntity.VVelocity.Clone();
            Vector2D currentDirection = direction.Normalize();
            Vector2D currentPos = MovingEntity.VPos;

            List<Vector2D> rectangle = new List<Vector2D>();

            Vector2D one = currentDirection.Perpendicular().Normalize().Multiply(RectangleWidth);
            Vector2D two = new Vector2D(-one.X, -one.Y);
            Vector2D three = two.Clone().Add(currentDirection.Clone().Multiply(RectangleDistance * 2));
            Vector2D four = one.Clone().Add(currentDirection.Clone().Multiply(RectangleDistance * 2));

            rectangle.AddRange(new[] {one, two, three, four});

            foreach (Vector2D point in rectangle)
            {
                point.Add(currentPos);
            }

            return rectangle;
        }

        private List<ObstacleEntity> FindObstaclesWithinRange()
        {           
            List<ObstacleEntity> obstaclesWithinRange = new List<ObstacleEntity>();
            
            var agentHeading = Math.Atan2(MovingEntity.VVelocity.X, MovingEntity.VVelocity.Y);
            
            foreach (ObstacleEntity obstacle in MovingEntity.World.Obstacles)
            {
                var distanceToObstacle = Vector2D.DistanceSquared(obstacle.VPos, MovingEntity.VPos);
                                
                double minAllowedDist = (RectangleDistance * 2) + obstacle.Bradius;

                if (distanceToObstacle < minAllowedDist * minAllowedDist)
                {   
                    obstacle.Tag();
                    obstaclesWithinRange.Add(obstacle);
                }
                else
                {
                    obstacle.RemoveTag();
                }
            }

            return obstaclesWithinRange;
        }
        
        private Vector2D ToLocalSpace(Vector2D worldPosition, double agentHeading)
        {
            Vector2D positionInLocalSpace = worldPosition.Clone().Substract(MovingEntity.VPos);
            Vector2D positionAndHeadingInLocalSpace = positionInLocalSpace.Rotate(agentHeading);

            return positionAndHeadingInLocalSpace;
        }

        public override void DrawBehavior(Graphics g)
        {
            g.FillPolygon(new SolidBrush(Color.Blue), new[]
            {
                Rectangle[0].ToPoint(),
                Rectangle[1].ToPoint(),
                Rectangle[2].ToPoint(),
                Rectangle[3].ToPoint()
            });
            g.FillEllipse(new SolidBrush(Color.Red), (int) rDist.X, (int) rDist.Y, (int) 5, (int) 5);
        }
    }
}