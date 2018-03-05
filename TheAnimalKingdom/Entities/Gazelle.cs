using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Behaviours;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    class Gazelle : Animal
    {
        public Gazelle(Vector2D position, World world) : base(position, world)
        {
            Color = Color.Coral;
            Bradius = 5;
            VVelocity = new Vector2D(0, 0);
            DMass = 10;
            DMaxSpeed = 5;
            DDeceleration = 3;
            DMaxForce = 10;
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            SteeringBehaviours.DrawBehaviors(g);
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
        }
    }
}
