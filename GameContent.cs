using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;

namespace JustCombat
{
    public class GameContent
    {
        public Texture2D FumikoImage { get; set; }
        public SpriteFont LabelFont { get; set; }
        public SpriteFont GameFont { get; set; }
        public TiledMap GameMap { get; set; }
        public Texture2D WraithImage { get; set; }
        public Texture2D Cursor { get; set; }

        public GameContent(ContentManager content)
        {
            FumikoImage = content.Load<Texture2D>("Fumiko");
            LabelFont = content.Load<SpriteFont>("Consolas14");
            GameFont = content.Load<SpriteFont>("FrizQuadrataTTRegular");
            GameMap = content.Load<TiledMap>("northshire_test");
            WraithImage = content.Load<Texture2D>("wraith");
            Cursor = content.Load<Texture2D>("cursors_combined");
        }
    }
}
