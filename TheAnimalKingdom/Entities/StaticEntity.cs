using System.Drawing;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class StaticEntity : BaseGameEntity
    {       
        public StaticEntity(Vector2D position, World world) : base(position, world)
        {
            Color = Color.Green;
            Bradius = 3;
        }

        public override void Update(float time_elapsed)
        {
            
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
        }
    }
}