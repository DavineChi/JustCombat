using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class Player : Actor
    {
        public enum State { NORMAL, RESTED, IN_COMBAT, DEAD };

        protected static Player _player = null;

        private SpriteSheet _spriteSheet;

        private Texture2D _playerSprites;
        private SpriteSheet _playerSpriteSheet;

        private Texture2D currentDirection;
        private Texture2D[] playerDirections;

        private State _state;

        protected Player(string name, float x, float y, float width, float height, Direction heading) :
            base(name, x, y, width, height, heading)
        {
            _playerSprites = JustCombat.gameContent.FumikoImage;
            _spriteSheet = new SpriteSheet(_playerSprites, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

            playerDirections = new Texture2D[4];

            InitStaticDirectionSprites();
        }

        public static Player Instance()
        {
            if (_player == null)
            {
                _player = new Player("Ayrn", 340.0f, 280.0f, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT, new Direction(180.0f));
            }

            return _player;
        }

        // Helper method to initialize the static directional sprites for this Actor.
        private void InitStaticDirectionSprites()
        {
            int counter = 0;

            for (int i = 0; i < 4; i++)
            {
                playerDirections[i] = _spriteSheet.GetTexture(1, (counter), Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
                counter = counter + 1;
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.SetDirection(90.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.SetDirection(180.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.SetDirection(270.0f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(this._x, this._y);

            if (this.GetDirection().GetHeading() == 0.0f)   { currentDirection = playerDirections[0]; }
            if (this.GetDirection().GetHeading() == 90.0f)  { currentDirection = playerDirections[1]; }
            if (this.GetDirection().GetHeading() == 180.0f) { currentDirection = playerDirections[2]; }
            if (this.GetDirection().GetHeading() == 270.0f) { currentDirection = playerDirections[3]; }

            spriteBatch.Draw(currentDirection, position, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
        }

        public Player.State GetState()
        {
            return _state;
        }

        public void SetState(Player.State state)
        {
            _state = state;
        }

        public static void AddLevel()
        {
            if (_player == null)
            {
                throw new NullReferenceException("Check the Instance() method for errors.");
            }

            if (!_player.IsAlive())
            {
                throw new ArgumentException("Cannot level up a dead player.");
            }

            if ((_player.GetLevel() + 1) <= Constants.MAXIMUM_PLAYER_LEVEL)
            {
                _player.SetLevel(_player.GetLevel() + 1);

                UpdateAttributes();
            }
        }

        private static void UpdateAttributes()
        {
            _player.SetMaxHitPoints(HitPoints.Calculate(_player));
            _player.SetHitPoints(_player.GetMaxHitPoints());
            // TODO: set player experience points
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
