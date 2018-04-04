using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;
using Matrix = TheAnimalKingdom.Util.Matrix;

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
            List<ObstacleEntity> obstaclesToLookOutFor = FindObstacleEntitiesWithinRange();
                        
            var distToNearestObject = double.MaxValue;
            ObstacleEntity nearestDangerousObject = null;
            Vector2D localPositionOfNearestDangerousObject = null;
            
            var heading = MovingEntity.VVelocity.Clone().Normalize();
            
            foreach (var obstacle in obstaclesToLookOutFor)
            {
                
                Vector2D localPosition = GetPositionInLocalSpace(obstacle, heading,
                    heading.Perpendicular(), MovingEntity.VPos);

                if (localPosition.X > 0)
                {
                    double safeDistance = obstacle.Bradius + MovingEntity.Bradius;

                    if (Math.Abs(localPosition.Y) < safeDistance)
                    {
                        var cX = localPosition.X;
                        var cY = localPosition.Y;

                        var sqrtPart = Math.Sqrt(safeDistance * safeDistance - cY * cY);

                        var ip = cX - sqrtPart;

                        if (ip <= 0.0)
                        {
                            ip = cX + sqrtPart;
                        }

                        if (ip < distToNearestObject)
                        {
                            distToNearestObject = ip;
                            nearestDangerousObject = obstacle;
                            localPositionOfNearestDangerousObject = localPosition;
                        }
                    }
                }
            }

            Vector2D steeringForce = new Vector2D();

            if (nearestDangerousObject != null)
            {
                var rectLength = Rectangle[2].Y - Rectangle[0].Y;
                double multiplier = 1.0 + (rectLength - localPositionOfNearestDangerousObject.X) / rectLength;

                steeringForce.Y = (nearestDangerousObject.Bradius - localPositionOfNearestDangerousObject.Y) *
                                  multiplier;
                steeringForce.X = (nearestDangerousObject.Bradius - localPositionOfNearestDangerousObject.X) * 0.2;
            }

            return VectorToWorldSpace(steeringForce, heading, heading.Perpendicular());
        }

        private Vector2D GetPositionInLocalSpace(ObstacleEntity obstacle, Vector2D heading, Vector2D side, Vector2D position)
        {
            Vector2D obstaclePos = obstacle.VPos;

            var tX = -position.Clone().DotMultiplication(heading);
            var tY = -position.Clone().DotMultiplication(side);
            
            Matrix transformMatrix = new Matrix(heading.X, heading.Y, 0.0, heading.Y, heading.X, 0.0, tX, tY, 1.0);

            obstaclePos = transformMatrix * obstaclePos;

            return obstaclePos;
        }

        private Vector2D VectorToWorldSpace(Vector2D vector, Vector2D heading, Vector2D side)
        {
            
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

        private List<ObstacleEntity> FindObstacleEntitiesWithinRange()
        {           
            List<ObstacleEntity> obstaclesWithinRange = new List<ObstacleEntity>();
            
            foreach (ObstacleEntity obstacle in MovingEntity.World.Obstacles)
            {
                double distanceToObstace = Vector2D.DistanceSquared(obstacle.VPos, MovingEntity.VPos);
                                
                double minAllowedDist = (RectangleDistance * 2) + obstacle.Bradius;
               
                if (distanceToObstace < minAllowedDist * minAllowedDist)
                {
                    obstaclesWithinRange.Add(obstacle);
                    obstacle.Tag();
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