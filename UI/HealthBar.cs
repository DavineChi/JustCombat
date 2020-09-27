﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class HealthBar : FillBar
    {
        private const float REGEN_DELAY = 2.0f;

        private const float STEP_FN_LEVEL_01_TO_04  = 0.2f;
        private const float STEP_FN_LEVEL_05_TO_07  = 0.065f;
        private const float STEP_FN_LEVEL_08_TO_09  = 0.025f;
        private const float STEP_FN_LEVEL_10_AND_UP = 0.005f;

        private FillBar.State _state;

        private GameTime _cooldownTimer;

        private float _secondsCounter;

        private bool _inCooldown;

        private string _seconds = "";

        public HealthBar(int xPosition, int yPosition, int width, int height) :
            base(xPosition, yPosition, width, height)
        {
            Color barColor = Color.Green;

            _bar.SetColor(barColor);

            _state = FillBar.State.FULL;
            _cooldownTimer = new GameTime();

            _inCooldown = false;

            _secondsCounter = 0;
        }

        private void QueryState(Actor actor)
        {
            float hitPoints = (float)(actor.GetHitPoints());
            float maxHitPoints = (float)(actor.GetMaxHitPoints());
            float fillFactor = hitPoints / maxHitPoints;

            if (actor.GetState() == Actor.State.IN_COMBAT)
            {
                _state = State.COMBAT;
            }

            else if (hitPoints < maxHitPoints)
            {
                _state = State.REGEN;
            }

            else if (hitPoints == maxHitPoints)
            {
                _state = State.FULL;
            }

            _bar.SetWidth(_width * fillFactor);
        }

        public FillBar.State GetState()
        {
            return _state;
        }

        public void SetState(FillBar.State state)
        {
            _state = state;
        }

        public GameTime GetCooldownTimer()
        {
            return _cooldownTimer;
        }

        public override PrimRectangle GetBar()
        {
            return _bar;
        }

        public void ResetTimer()
        {
            _secondsCounter = 0;
        }

        private void Tick(GameTime gameTime)
        {
            _seconds = ((float)(gameTime.ElapsedGameTime.TotalSeconds)).ToString();

            _secondsCounter = _secondsCounter + (float)(gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Update(Actor actor, GameTime gameTime)
        {
            QueryState(actor);

            if (_state == State.REGEN)
            {
                Tick(gameTime);

                if (_secondsCounter < REGEN_DELAY)
                {
                    _inCooldown = true;
                }

                else
                {
                    _inCooldown = false;
                }

                if (!_inCooldown && _secondsCounter > 2)
                {
                    ResetTimer();

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

                    _bar.SetWidth(_width * fillFactor);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);

            spriteBatch.DrawString(JustCombat.gameContent.GameFont, _seconds, new Vector2(400.0f, 100.0f), Color.White);
            spriteBatch.DrawString(JustCombat.gameContent.GameFont, _secondsCounter.ToString(), new Vector2(400.0f, 120.0f), Color.White);
        }
    }
}
