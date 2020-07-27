using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class Player : Actor
    {
        private SpriteSheet spriteSheet;

        private Texture2D playerNorth;
        private Texture2D playerEast;
        private Texture2D playerSouth;
        private Texture2D playerWest;

        private Texture2D currentDirection;
        private Texture2D[] playerDirections;
        
        public Texture2D FumikoSheet { get; set; }

        public Player(string name,
                      float x,
                      float y,
                      float width,
                      float height,
                      Direction heading,
                      GameContent gameContent) :
            base(name, x, y, width, height, heading)
        {
            FumikoSheet = gameContent.FumikoImage;
            spriteSheet = new SpriteSheet(FumikoSheet, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

            playerNorth = spriteSheet.GetTexture(24, 0,  Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            playerEast  = spriteSheet.GetTexture(24, 32, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            playerSouth = spriteSheet.GetTexture(24, 64, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            playerWest  = spriteSheet.GetTexture(24, 96, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

            playerDirections = new Texture2D[] { playerNorth, playerEast, playerSouth, playerWest };
        }

        public Texture2D GetSprite()
        {
            if (currentDirection == null)
            {
                currentDirection = playerDirections[2];
            }

            return currentDirection;
        }

        public void SetDirection(float heading)
        {
            this.SetHeading(heading);

            if (heading == 0.0f)   { currentDirection = playerDirections[0]; }
            if (heading == 90.0f)  { currentDirection = playerDirections[1]; }
            if (heading == 180.0f) { currentDirection = playerDirections[2]; }
            if (heading == 270.0f) { currentDirection = playerDirections[3]; }
        }

        public void WalkNorth()
        {
            this.SetHeading(0.0f);

            _y = _y - Constants.PLAYER_SPEED_WALK;
        }

        public void WalkEast()
        {
            this.SetHeading(90.0f);

            _x = _x + Constants.PLAYER_SPEED_WALK;
        }

        public void WalkSouth()
        {
            this.SetHeading(180.0f);

            _y = _y + Constants.PLAYER_SPEED_WALK;
        }

        public void WalkWest()
        {
            this.SetHeading(270.0f);

            _x = _x - Constants.PLAYER_SPEED_WALK;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.SetDirection(0.0f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.SetDirection(90.0f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.SetDirection(180.0f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.SetDirection(270.0f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(this._x, this._y);

            if (this.GetDirection().GetHeading() == 0.0f)   { currentDirection = playerNorth; }
            if (this.GetDirection().GetHeading() == 90.0f)  { currentDirection = playerEast;  }
            if (this.GetDirection().GetHeading() == 180.0f) { currentDirection = playerSouth; }
            if (this.GetDirection().GetHeading() == 270.0f) { currentDirection = playerWest;  }

            spriteBatch.Draw(currentDirection, position, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
        }

        public override bool MoveX(float dx, float dy, long delta)
        {
            throw new NotImplementedException();
        }

        public override bool MoveY(float dx, float dy, long delta)
        {
            throw new NotImplementedException();
        }
    }
}
