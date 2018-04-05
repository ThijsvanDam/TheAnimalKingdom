using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class Animal : MovingEntity
    {

        protected Animal(Vector2D position, World world, bool isPredator) : base(position, world, isPredator)
        {

        }
    }
}