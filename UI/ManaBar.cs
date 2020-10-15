using JustCombat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ManaBar : FillBar, IDrawable
    {
        public ManaBar(int xPosition, int yPosition, int width, int height, Actor actor) :
            base(xPosition, yPosition, width, height, actor)
        {
            _bar.SetColor(new Color(0, 100, 200));
        }

        public void Update(GameTime gameTime)
        {
            float fillFactor = _actor.GetHitPointsFillFactor(); // TODO: add manabar method

            _bar.SetWidth(_width * fillFactor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);
        }
    }
}
