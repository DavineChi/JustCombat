using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JustCombat.Entities
{
    public class Wraith : Actor
    {
        private Texture2D _wraithSprites;
        private SpriteSheet _spriteSheet;

        private Texture2D[] _wraithDirections;

        public Wraith(string name, float x, float y, float width, float height, float scale, Direction heading, int hitPoints) :
            base(name, x, y, width, height, scale, heading, hitPoints)
        {
            _wraithSprites = JustCombat.gameContent.WraithImage;
            _spriteSheet = new SpriteSheet(_wraithSprites, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
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

        public override bool Move(float dx, float dy, bool isRunning)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _boundingBox.Draw(spriteBatch);

            spriteBatch.Draw(_wraithSprites, new Vector2(_x, _y), null, Color.White, 0f, Vector2.Zero, Constants.SPRITE_SCALE, SpriteEffects.None, 0f);
        }
    }
}
