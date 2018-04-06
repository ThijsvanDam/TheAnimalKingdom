using System.Drawing;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class Lion : Animal
    {
        public Lion(Vector2D position, World world) : base(position, world, true)
        {
            Color = Color.Orange;
            Bradius = 5;
            VVelocity = new Vector2D(0, 0);
            DMass = 15;
            DMaxSpeed = 5;
            DDeceleration = 3;
            DMaxForce = 10;
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 3;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
            g.DrawString("L", new Font(new FontFamily("Times New Roman"), 10f), new SolidBrush(Color.Black), (float)left, (float)top);

            base.Render(g);
        }
    }
}