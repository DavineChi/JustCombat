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

        public SpriteFont FontCandara10 { get; set; }
        public SpriteFont FontCandara11 { get; set; }
        public SpriteFont FontCandara12 { get; set; }

        public SpriteFont FontCandaraLight10 { get; set; }
        public SpriteFont FontCandaraLight11 { get; set; }
        public SpriteFont FontCandaraLight12 { get; set; }

        public SpriteFont FontJosefinSlab10 { get; set; }
        public SpriteFont FontJosefinSlab11 { get; set; }
        public SpriteFont FontJosefinSlab12 { get; set; }

        public TiledMap GameMap { get; set; }
        public Texture2D WraithImage { get; set; }
        public Texture2D Cursor { get; set; }
        public Texture2D TopBarBackpanel { get; set; }
        public Texture2D ActionBarPanel { get; set; }
        public Texture2D ExperienceBarFrame { get; set; }
        public Texture2D ActorInfoCard { get; set; }
        //public Texture2D ItemTile { get; set; }

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

            FontCandara10 = content.Load<SpriteFont>("Candara10");
            FontCandara11 = content.Load<SpriteFont>("Candara11");
            FontCandara12 = content.Load<SpriteFont>("Candara12");

            FontCandaraLight10 = content.Load<SpriteFont>("CandaraLight10");
            FontCandaraLight11 = content.Load<SpriteFont>("CandaraLight11");
            FontCandaraLight12 = content.Load<SpriteFont>("CandaraLight12");

            FontJosefinSlab10 = content.Load<SpriteFont>("JosefinSlab10");
            FontJosefinSlab11 = content.Load<SpriteFont>("JosefinSlab11");
            FontJosefinSlab12 = content.Load<SpriteFont>("JosefinSlab12");

            GameMap = content.Load<TiledMap>("northshire_test");
            WraithImage = content.Load<Texture2D>("wraith");
            Cursor = content.Load<Texture2D>("cursors_combined");
            TopBarBackpanel = content.Load<Texture2D>("top_bar_backpanel");
            ActionBarPanel = content.Load<Texture2D>("action_bar_with_tiles");
            ExperienceBarFrame = content.Load<Texture2D>("xp_bar_frame");
            ActorInfoCard = content.Load<Texture2D>("actor_card_v8");
            //ItemTile = content.Load<Texture2D>("item_tile");
        }
    }
}
