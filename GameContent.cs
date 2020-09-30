using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;

namespace JustCombat
{
    public class GameContent
    {
        public Texture2D FumikoImage { get; set; }
        public SpriteFont LabelFont { get; set; }
        public SpriteFont FontFrizQuad { get; set; }
        public SpriteFont FontConsolas14 { get; set; }
        public SpriteFont FontConsolas13 { get; set; }
        public SpriteFont FontConsolas12 { get; set; }
        public SpriteFont FontConsolas11 { get; set; }
        public SpriteFont FontConsolas10 { get; set; }
        public TiledMap GameMap { get; set; }
        public Texture2D WraithImage { get; set; }
        public Texture2D Cursor { get; set; }
        public Texture2D TopBarBackpanel { get; set; }
        public Texture2D ActorInfoCard { get; set; }

        public GameContent(ContentManager content)
        {
            FumikoImage = content.Load<Texture2D>("Fumiko_v2");
            LabelFont = content.Load<SpriteFont>("Consolas14");
            FontFrizQuad = content.Load<SpriteFont>("FrizQuadrataTTRegular");
            FontConsolas14 = content.Load<SpriteFont>("Consolas14");
            FontConsolas13 = content.Load<SpriteFont>("Consolas13");
            FontConsolas12 = content.Load<SpriteFont>("Consolas12");
            FontConsolas11 = content.Load<SpriteFont>("Consolas11");
            FontConsolas10 = content.Load<SpriteFont>("Consolas10");
            GameMap = content.Load<TiledMap>("northshire_test");
            WraithImage = content.Load<Texture2D>("wraith");
            Cursor = content.Load<Texture2D>("cursors_combined");
            TopBarBackpanel = content.Load<Texture2D>("top_bar_backpanel");
            ActorInfoCard = content.Load<Texture2D>("actor_card_v8");
        }
    }
}
