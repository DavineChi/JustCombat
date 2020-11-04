using JustCombat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ExperienceBar : FillBar
    {
        public ExperienceBar(int xPosition, int yPosition, int width, int height, Actor actor) :
            base(xPosition, yPosition, width, height, actor)
        {
            _bar.SetColor(new Color(89, 0, 89));
        }

        public void Update(GameTime gameTime)
        {
            Player player = Player.Instance();

            float fillFactor = (float)(player.GetExperiencePoints()) / (float)(player.GetMaxExperiencePoints());

            _bar.SetWidth(_width * fillFactor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);
        }
    }
}
