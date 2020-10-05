using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class HealthBar : FillBar, IDrawable
    {
        public HealthBar(int xPosition, int yPosition, int width, int height, Actor actor) :
            base(xPosition, yPosition, width, height, actor)
        {
            _bar.SetColor(Color.Green);
        }

        public void Update(GameTime gameTime)
        {
            float fillFactor = _actor.GetHitPointsFillFactor();

            _bar.SetWidth(_width * fillFactor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);
        }
    }
}
