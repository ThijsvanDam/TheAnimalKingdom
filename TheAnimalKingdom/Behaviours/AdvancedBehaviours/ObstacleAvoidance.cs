using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.AdvancedBehaviours
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        public double RectangleDistance;
        public double RectangleWidth;

        public Vector2D rDist;
        public Vector2D one;
        public Vector2D two;
        public Vector2D three;
        public Vector2D four;

        public ObstacleAvoidance(MovingEntity movingEntity) : base(movingEntity)
        {
            RectangleDistance = 100;
            RectangleWidth = 10;
            rDist = new Vector2D(0, 0);
            one = new Vector2D(0, 0);
            two = new Vector2D(0, 0);
            three = new Vector2D(0, 0);
            four = new Vector2D(0, 0);
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

            // The square and its distance
            rDist = currentPos.Clone().Add((currentDirection.Clone().Multiply(RectangleDistance)));
            
            // Create rectangle
            one = currentDirection.Perpendicular().Normalize().Multiply(10);
            two = new Vector2D(-one.X, -one.Y);
            three = two.Clone().Add(currentDirection.Clone().Multiply(RectangleDistance * 2));
            four = one.Clone().Add(currentDirection.Clone().Multiply(RectangleDistance * 2));

            one.Add(currentPos);
            two.Add(currentPos);
            three.Add(currentPos);
            four.Add(currentPos);






            return new Vector2D(0, 0);
        }

        public override void DrawBehavior(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), (int)rDist.X, (int)rDist.Y, (int)5, (int)5);
//            g.FillEllipse(new SolidBrush(Color.Red), (int)one.X, (int)one.Y, (int)5, (int)5);
//            g.FillEllipse(new SolidBrush(Color.Red), (int)two.X, (int)two.Y, (int)5, (int)5);
//            g.FillEllipse(new SolidBrush(Color.Red), (int)three.X, (int)three.Y, (int)5, (int)5);
//            g.FillEllipse(new SolidBrush(Color.Red), (int)four.X, (int)four.Y, (int)5, (int)5);

            g.FillPolygon(new SolidBrush(Color.Blue), new []{one.toPoint(), two.toPoint(), three.toPoint(), four.toPoint() });
        }
    }
}