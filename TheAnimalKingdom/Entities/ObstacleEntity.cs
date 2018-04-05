using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class ObstacleEntity : BaseGameEntity
    {
        protected ObstacleEntity(Vector2D position, float size, World world) : base(position, world)
        {
            Bradius = (size * 25) / 2;
        }

        public abstract void Tag();
        public abstract void RemoveTag();
    }
}
