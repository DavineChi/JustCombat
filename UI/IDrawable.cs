using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public interface IDrawable
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
