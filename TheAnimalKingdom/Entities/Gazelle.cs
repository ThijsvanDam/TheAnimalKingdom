using System;
using System.Drawing;
using System.Linq;
using TheAnimalKingdom.Goals.CompositeGoals;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class Gazelle : Animal
    {   
        public Gazelle(Vector2D position, World world) : base(position, world)
        {
            Color = Color.SandyBrown;
            Bradius = 5;
            VVelocity = new Vector2D(0, 0);
            DMass = 10;
            DMaxSpeed = 7;
            DDeceleration = 3;
            DMaxForce = 10;
            
            HashTagLifeGoal = new GoalThinkGazelle(this);
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
            g.DrawString("G", new Font(new FontFamily("Times New Roman"), 6f), new SolidBrush(Color.Black), (float)left, (float)top);

            base.Render(g);
        }

        public override void Update(float time_elapsed)
        {
            if (IsCloseToFood())
            {
                Hunger -= 0.05;

            }
            else
            {
                Hunger += 0.02;

            }
            
            base.Update(time_elapsed);
        }

        public bool IsCloseToFood()
        {
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(SquaredObstacle)))
            {
                var obstacle = (SquaredObstacle) entity;

                if (Vector2D.DistanceSquared(VPos, entity.VPos) <= 500 && obstacle.Type != ItemType.None)
                    return true;
            }

            return false;
        }

        public override double DistanceToClosestLion()
        {
            double nearestDistance = double.MaxValue;
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(Lion)))
            {

                var dist = Vector2D.DistanceSquared(VPos, entity.VPos);
                if (dist < nearestDistance)
                    nearestDistance = dist;
            }
            return nearestDistance;
        }

        public override MovingEntity IsScaredOf()
        {
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(Lion)))
            {
                if (Vector2D.DistanceSquared(VPos, entity.VPos) <= 5000)
                    return (MovingEntity)entity;
            }

            return null;
        }
    }
}
