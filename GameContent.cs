using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace JustCombat
{
    public class GameContent
    {
        public Texture2D FumikoImage { get; set; }
        public SpriteFont LabelFont { get; set; }
        public SpriteFont GameFont { get; set; }

        public GameContent(ContentManager content)
        {
            FumikoImage = content.Load<Texture2D>("Fumiko");
            LabelFont = content.Load<SpriteFont>("Consolas14");
            GameFont = content.Load<SpriteFont>("FrizQuadrataTTRegular");
        }
    }
}
