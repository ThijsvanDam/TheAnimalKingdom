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

            List<ObstacleEntity> obstaclesWithinRange = FindObstacleEntitiesInCollisionCourse();

            foreach (ObstacleEntity obstacle in obstaclesWithinRange)
            {
                Vector2D localSpacePosition = GetPositionInLocalSpace(obstacle);
            }


            return new Vector2D(0, 0);
        }

        private Vector2D GetPositionInLocalSpace(ObstacleEntity obstacle)
        {
            Vector2D obstaclePos = obstacle.VPos;

            double direction = Vector2D.Direction(Rectangle[0], Rectangle[2]);
            
            Vector2D position = MovingEntity.VPos;


            return new Vector2D(0, 0);
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

        private List<ObstacleEntity> FindObstacleEntitiesInCollisionCourse()
        {           
            List<ObstacleEntity> obstaclesWithinRange = new List<ObstacleEntity>();
            
            foreach (ObstacleEntity obstacle in MovingEntity.World.Obstacles)
            {
                var distanceToObstace = Vector2D.DistanceSquared(obstacle.VPos, MovingEntity.VPos);
                
                var directionOfAgent = Math.Atan2(MovingEntity.VVelocity.X, MovingEntity.VVelocity.Y);

                var distanceToObstacle = obstacle.VPos.Clone().Substract(MovingEntity.VPos);
                
                var directionToObstacle = Math.Atan2(distanceToObstacle.X, distanceToObstacle.Y);
                
                Console.WriteLine("directionToObstacle:" + directionToObstacle + "\ndirectionOfAgent:" + directionOfAgent);
                
                double minAllowedDist = (RectangleDistance * 2) + obstacle.Bradius;

                if (distanceToObstace < minAllowedDist * minAllowedDist)
                {
                    obstacle.Tag();
                    
                    if (directionToObstacle < Math.PI / 2 && directionToObstacle > - Math.PI / 2)
                    {
                        obstacle.Color = Color.Black;
                        obstaclesWithinRange.Add(obstacle);
                        obstacle.Tag();
                    }
                    else
                    {
                        obstacle.RemoveTag();
                    }
                }
                else
                {
                    obstacle.RemoveTag();
                }
            }

            return obstaclesWithinRange;
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