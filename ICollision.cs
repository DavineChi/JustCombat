using Microsoft.Xna.Framework;

namespace JustCombat
{
    public interface ICollision
    {
        bool Intersects(Rectangle other);
        bool WillIntersect(Rectangle other, GameTime delta);
    }
}
