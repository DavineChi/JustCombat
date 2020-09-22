using Microsoft.Xna.Framework;

namespace JustCombat
{
    public interface ICollision
    {
        bool Intersects(PrimShape other);
        bool WillIntersect(PrimShape other, GameTime delta);
    }
}
