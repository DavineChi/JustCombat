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

        private Texture2D _playerSprites;
        private SpriteSheet _spriteSheet;

        private Texture2D _currentDirection;
        private Texture2D[] _playerDirections;

        private Animation _animatePlayerNorthWalking;
        private Animation _animatePlayerEastWalking;
        private Animation _animatePlayerSouthWalking;
        private Animation _animatePlayerWestWalking;

        private Animation _animatePlayerIdle;

        private State _state;

        protected Player(string name, float x, float y, float width, float height, Direction heading) :
            base(name, x, y, width, height, heading)
        {
            _playerSprites = JustCombat.gameContent.FumikoImage;
            _spriteSheet = new SpriteSheet(_playerSprites, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

            _playerDirections = new Texture2D[4];

            InitStaticDirectionSprites();

            _animatePlayerNorthWalking = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, 0, 0, 3, 180);
            _animatePlayerEastWalking  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, 0, 1, 3, 180);
            _animatePlayerSouthWalking = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, 0, 2, 3, 180);
            _animatePlayerWestWalking  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, 0, 3, 3, 180);

            _animatePlayerIdle = AnimationFactory.CreateAnimationIdlePlayer(_spriteSheet, 16, 0, 80);
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
                _playerDirections[i] = _spriteSheet.GetTexture(1, (counter), Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
                counter = counter + 1;
            }
        }

        public Texture2D GetSprite()
        {
            if (_currentDirection == null)
            {
                _currentDirection = _playerDirections[2];
            }

            return _currentDirection;
        }

        public void SetDirection(float heading)
        {
            this.SetHeading(heading);

            if (heading == 0.0f)   { _currentDirection = _playerDirections[0]; }
            if (heading == 90.0f)  { _currentDirection = _playerDirections[1]; }
            if (heading == 180.0f) { _currentDirection = _playerDirections[2]; }
            if (heading == 270.0f) { _currentDirection = _playerDirections[3]; }
        }
        
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { this.SetDirection(0.0f);   }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { this.SetDirection(90.0f);  }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { this.SetDirection(180.0f); }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { this.SetDirection(270.0f); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(this._x, this._y);

            if (this.GetDirection().GetHeading() == 0.0f)   { _currentDirection = _playerDirections[0]; }
            if (this.GetDirection().GetHeading() == 90.0f)  { _currentDirection = _playerDirections[1]; }
            if (this.GetDirection().GetHeading() == 180.0f) { _currentDirection = _playerDirections[2]; }
            if (this.GetDirection().GetHeading() == 270.0f) { _currentDirection = _playerDirections[3]; }

            spriteBatch.Draw(_currentDirection, position, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
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

        private int[] GetNewPosition(float dx, float dy, bool isRunning)
        {
            int[] result = new int[2];

            float newX = _x + dx;
            float newY = _y + dy;

            if (isRunning)
            {
                newX = (newX + (dx * Constants.PLAYER_SPEED_RUN));
                newY = (newY + (dy * Constants.PLAYER_SPEED_RUN));
            }

            else
            {
                newX = (newX + (dx * Constants.PLAYER_SPEED_WALK));
                newY = (newY + (dy * Constants.PLAYER_SPEED_WALK));
            }

            // Round up or down depending on the direction moved.
            if (dx > 0 || dy > 0)
            {
                newX = (int)(Math.Ceiling(newX));
                newY = (int)(Math.Ceiling(newY));
            }

            else
            {
                newX = (int)(Math.Floor(newX));
                newY = (int)(Math.Floor(newY));
            }

            result[0] = (int)(newX);
            result[1] = (int)(newY);

            return result;
        }

        public override bool Move(float dx, float dy, bool isRunning)
        {
            bool result = false; // TODO: for collision-detection
            int[] newPositions = GetNewPosition(dx, dy, isRunning);

            this.SetX(newPositions[0]);
            this.SetY(newPositions[1]);

            return result;
        }
    }
}
