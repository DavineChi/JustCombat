using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using JustCombat.Common;

namespace JustCombat
{
    public class Player : Actor
    {
        protected static Player _player = null;

        private const int ANIMATION_SPEED_WALKING = 180;
        private const int ANIMATION_SPEED_RUNNING = 130;
        private const int ANIMATION_SPEED_IDLE = 80;

        private Texture2D _playerSprites;
        private SpriteSheet _spriteSheet;

        private Texture2D _currentDirection;
        private Texture2D[] _playerDirections;

        private Animation _animatePlayerNorthWalking;
        private Animation _animatePlayerEastWalking;
        private Animation _animatePlayerSouthWalking;
        private Animation _animatePlayerWestWalking;
        private Animation _animatePlayerNorthRunning;
        private Animation _animatePlayerEastRunning;
        private Animation _animatePlayerSouthRunning;
        private Animation _animatePlayerWestRunning;
        private Animation _animatePlayerIdle;
        private Animation _currentAnimation;

        private bool _moving;

        protected Player(string name, float x, float y, float width, float height, Direction heading) :
            base(name, x, y, width, height, Constants.SPRITE_SCALE, heading)
        {
            _playerSprites = JustCombat.gameContent.FumikoImage;
            _spriteSheet = new SpriteSheet(_playerSprites, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            _playerDirections = new Texture2D[4];

            _boundingBox.GetPrimRectangle().SetColor(Color.Green);

            InitStaticDirectionSprites();

            _animatePlayerNorthWalking = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "nWalk", 0, 0, 3, ANIMATION_SPEED_WALKING);
            _animatePlayerEastWalking  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "eWalk", 0, 1, 3, ANIMATION_SPEED_WALKING);
            _animatePlayerSouthWalking = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "sWalk", 0, 2, 3, ANIMATION_SPEED_WALKING);
            _animatePlayerWestWalking  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "wWalk", 0, 3, 3, ANIMATION_SPEED_WALKING);

            _animatePlayerNorthRunning = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "nRun", 3, 0, 3, ANIMATION_SPEED_RUNNING);
            _animatePlayerEastRunning  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "eRun", 3, 1, 3, ANIMATION_SPEED_RUNNING);
            _animatePlayerSouthRunning = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "sRun", 3, 2, 3, ANIMATION_SPEED_RUNNING);
            _animatePlayerWestRunning  = AnimationFactory.CreateAnimationHorizontal(_spriteSheet, "wRun", 3, 3, 3, ANIMATION_SPEED_RUNNING);

            _animatePlayerIdle = AnimationFactory.CreateAnimationIdlePlayer(_spriteSheet, "idle", 16, 0, ANIMATION_SPEED_IDLE);

            _currentAnimation = _animatePlayerIdle;

            _moving = false;
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

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            float heading = this.GetDirection().GetHeading();

            _moving = false;

            if (keyState.IsKeyDown(Keys.W)) { this.SetDirection(0.0f);   }
            if (keyState.IsKeyDown(Keys.D)) { this.SetDirection(90.0f);  }
            if (keyState.IsKeyDown(Keys.S)) { this.SetDirection(180.0f); }
            if (keyState.IsKeyDown(Keys.A)) { this.SetDirection(270.0f); }

            if (heading == 0.0f)   { _currentDirection = _playerDirections[0]; }
            if (heading == 90.0f)  { _currentDirection = _playerDirections[1]; }
            if (heading == 180.0f) { _currentDirection = _playerDirections[2]; }
            if (heading == 270.0f) { _currentDirection = _playerDirections[3]; }
            
            if (InputHandler.IsValidMovementKey(keyState))
            {
                _moving = true;
            }

            if (keyState.IsKeyDown(Keys.LeftShift))
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    _currentAnimation = _animatePlayerNorthRunning;
                }

                else if (keyState.IsKeyDown(Keys.D))
                {
                    _currentAnimation = _animatePlayerEastRunning;
                }

                else if (keyState.IsKeyDown(Keys.S))
                {
                    _currentAnimation = _animatePlayerSouthRunning;
                }

                else if (keyState.IsKeyDown(Keys.A))
                {
                    _currentAnimation = _animatePlayerWestRunning;
                }

                else if (heading == 180)
                {
                    _currentAnimation = _animatePlayerIdle;
                    _moving = false;
                }
            }

            else if (!(keyState.IsKeyDown(Keys.LeftShift)))
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    _currentAnimation = _animatePlayerNorthWalking;
                }

                else if (keyState.IsKeyDown(Keys.D))
                {
                    _currentAnimation = _animatePlayerEastWalking;
                }

                else if (keyState.IsKeyDown(Keys.S))
                {
                    _currentAnimation = _animatePlayerSouthWalking;
                }

                else if (keyState.IsKeyDown(Keys.A))
                {
                    _currentAnimation = _animatePlayerWestWalking;
                }

                else if (heading == 180)
                {
                    _currentAnimation = _animatePlayerIdle;
                    _moving = false;
                }
            }
            
            _currentAnimation.Update(gameTime);
            _combatExitTimer.Update(gameTime);
            UpdateFillTimer(gameTime);
        }

        public void UpdateFillTimer(GameTime gameTime)
        {
            this.QueryState();

            if (_healthState == HealthBarState.REGEN)
            {
                _hitPointsTimer.Update(gameTime);

                if (_hitPointsTimer.IsComplete())
                {
                    int fillValue = 0;

                    if (_level >= 1 && _level <= 4)
                    {
                        fillValue = (int)(_maxHitPoints * Constants.STEP_FN_LEVEL_01_TO_04);
                    }

                    else if (_level >= 5 && _level <= 7)
                    {
                        fillValue = (int)(_maxHitPoints * Constants.STEP_FN_LEVEL_05_TO_07);
                    }

                    else if (_level >= 8 && _level <= 9)
                    {
                        fillValue = (int)(_maxHitPoints * Constants.STEP_FN_LEVEL_08_TO_09);
                    }

                    else if (_level >= 10)
                    {
                        fillValue = (int)(_maxHitPoints * Constants.STEP_FN_LEVEL_10_AND_UP);
                    }

                    this.AddHitPoints(fillValue);

                    _hitPointsFillFactor = (float)(_hitPoints) / (float)(_maxHitPoints);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawPlayer(spriteBatch);

            if (JustCombat.UserInterface.InDebugMode())
            {
                _boundingBox.Draw(spriteBatch);
            }
        }

        private void DrawPlayer(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(this._x, this._y);
            float heading = this.GetDirection().GetHeading();

            if (_moving)
            {
                _currentAnimation.Draw(position, Constants.SPRITE_SCALE, spriteBatch);
            }

            else
            {
                if (heading == 0 || heading == 90 || heading == 270)
                {
                    spriteBatch.Draw(_currentDirection, position, null, Color.White, 0f, Vector2.Zero, Constants.SPRITE_SCALE, SpriteEffects.None, 0f);
                }

                else
                {
                    _currentAnimation.Draw(position, Constants.SPRITE_SCALE, spriteBatch);
                }
            }
        }
    }
}
