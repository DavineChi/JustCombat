using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.Entities
{
    public class Wraith : Actor
    {
        private Texture2D[] _wraithDirections;

        public Wraith(int level, string name, float x, float y, float width, float height, float scale, Direction heading, int hitPoints) :
            base(level, name, x, y, width, height, scale, heading, hitPoints)
        {
            _sprites = JustCombat.GameContent.WraithImage;
            _spriteSheet = new SpriteSheet(_sprites, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            _wraithDirections = new Texture2D[4];

            _boundingBox.GetPrimRectangle().SetColor(Color.Green);

            InitStaticDirectionSprites();
        }

        // Helper method to initialize the static directional sprites for this Actor.
        private void InitStaticDirectionSprites()
        {
            int counter = 0;

            for (int i = 0; i < 1; i++)
            {
                _wraithDirections[i] = _spriteSheet.GetTexture(0, (counter), Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
                counter = counter + 1;
            }
        }

        public Texture2D GetSprites()
        {
            return _sprites;
        }

        public void Update(GameTime gameTime)
        {
            this.QueryState();
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(_sprites, new Vector2(_x, _y), null, Color.White, 0f, Vector2.Zero, Constants.SPRITE_SCALE, SpriteEffects.None, 0f);

        //    if (JustCombat.UserInterface.InDebugMode())
        //    {
        //        _boundingBox.Draw(spriteBatch);
        //    }
        //}
    }
}
