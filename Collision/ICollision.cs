using Microsoft.Xna.Framework;

namespace JustCombat.Collision
{
    public interface ICollision
    {
        bool Intersects(PrimShape other);
        bool WillIntersect(PrimShape other, GameTime delta);
    }
}
