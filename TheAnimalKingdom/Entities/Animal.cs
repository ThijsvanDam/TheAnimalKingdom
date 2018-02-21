using System.Drawing;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class Animal : MovingEntity
    {
        public Color Color;

        protected Animal(Vector2D position, World world) : base(position, world)
        {

        }
    }
}