using Microsoft.Xna.Framework;
using System;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public class HealthBar : CommonBar
    {
        public enum State { EMPTY, FULL, COMBAT, REGEN };

        private const float REGEN_DELAY = 2.0f;

        private const float STEP_FN_LEVEL_01_TO_04  = 0.2f;
        private const float STEP_FN_LEVEL_05_TO_07  = 0.065f;
        private const float STEP_FN_LEVEL_08_TO_09  = 0.025f;
        private const float STEP_FN_LEVEL_10_AND_UP = 0.005f;

        private HealthBar.State _state;

        private GameTime _timer;
        private GameTime _cooldownTimer;

        private int _secondsCounter;

        private bool _cooldown;

        public HealthBar(int xPosition, int yPosition, int width, int height) :
            base(xPosition, yPosition, width, height)
        {
            Color frameColor = Color.Gray;    //new Color(0.0f, 178.0f, 0.0f);
            Color fillBarColor = Color.Green; //new Color(153.0f, 153.0f, 153.0f);

            _frame.SetColor(frameColor);
            _fillBar.SetColor(fillBarColor);

            _state = State.FULL;
            _timer = new GameTime();
            _cooldownTimer = new GameTime();

            _cooldown = false;
        }

        private void QueryState()
        {
            float hitPoints = (float)(Player.Instance().GetHitPoints());
            float maxHitPoints = (float)(Player.Instance().GetMaxHitPoints());
            float fillFactor = hitPoints / maxHitPoints;

            if (hitPoints < maxHitPoints)
            {
                _state = State.REGEN;
            }

            if (hitPoints == maxHitPoints)
            {
                _state = State.FULL;
            }

            if (Player.Instance().GetState() == Player.State.IN_COMBAT)
            {
                _state = State.COMBAT;
            }

            _fillBar.SetWidth(_width * fillFactor);
        }

        public HealthBar.State GetState()
        {
            return _state;
        }

        public void SetState(HealthBar.State state)
        {
            _state = state;
        }

        public int GetTime()
        {
            return _timer.ElapsedGameTime.Seconds;
        }

        public GameTime GetTimer()
        {
            return _timer;
        }

        public GameTime GetCooldownTimer()
        {
            return _cooldownTimer;
        }

        public override PrimRectangle GetFillBar()
        {
            return _fillBar;
        }

        public override PrimRectangle GetFrame()
        {
            return _frame;
        }

        public override void Update()
        {
            QueryState();

            _secondsCounter = _secondsCounter + _cooldownTimer.ElapsedGameTime.Seconds;

            if (_state == State.REGEN)
            {
                if (_secondsCounter < 3)
                {
                    _cooldown = true;
                }

                else
                {
                    _cooldown = false;
                }

                if (!_cooldown && _secondsCounter > REGEN_DELAY)
                {
                    // TODO: reset the timer
                    //_timer.ElapsedGameTime.Seconds;
                    _secondsCounter = 0;

                    int level = Player.Instance().GetLevel();
                    int fillValue = 0;

                    if (level >= 1 && level <= 4)
                    {
                        fillValue = (int)(Player.Instance().GetMaxHitPoints() * STEP_FN_LEVEL_01_TO_04);
                    }

                    else if (level >= 5 && level <= 7)
                    {
                        fillValue = (int)(Player.Instance().GetMaxHitPoints() * STEP_FN_LEVEL_05_TO_07);
                    }

                    else if (level >= 8 && level <= 9)
                    {
                        fillValue = (int)(Player.Instance().GetMaxHitPoints() * STEP_FN_LEVEL_08_TO_09);
                    }

                    else if (level >= 10)
                    {
                        fillValue = (int)(Player.Instance().GetMaxHitPoints() * STEP_FN_LEVEL_10_AND_UP);
                    }

                    Player.Instance().AddHitPoints(fillValue);

                    float fillFactor = (float)(Player.Instance().GetHitPoints()) / (float)(Player.Instance().GetMaxHitPoints());

                    _fillBar.SetWidth(_width*  fillFactor);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            _frame.Draw(spriteBatch);
            _fillBar.Draw(spriteBatch);
        }
    }
}
