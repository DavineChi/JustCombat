using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class HealthBar : FillBar, IDrawable
    {
        private const float REGEN_DELAY = 2.0f;

        private const float STEP_FN_LEVEL_01_TO_04  = 0.2f;
        private const float STEP_FN_LEVEL_05_TO_07  = 0.065f;
        private const float STEP_FN_LEVEL_08_TO_09  = 0.025f;
        private const float STEP_FN_LEVEL_10_AND_UP = 0.005f;

        public HealthBar(int xPosition, int yPosition, int width, int height, Actor actor) :
            base(xPosition, yPosition, width, height, actor)
        {
            _bar.SetColor(Color.Green);

            _state = State.FULL;
            _timer = new CooldownTimer(REGEN_DELAY);
        }

        private void QueryState()
        {
            float hitPoints = (float)(_actor.GetHitPoints());
            float maxHitPoints = (float)(_actor.GetMaxHitPoints());
            float fillFactor = hitPoints / maxHitPoints;

            if (_actor.GetState() == Actor.State.IN_COMBAT)
            {
                _state = State.COMBAT;

                _timer.Reset();
            }

            else if (hitPoints < maxHitPoints)
            {
                _state = State.REGEN;

                if (!_timer.IsRunning())
                {
                    _timer.Start();
                }
            }

            else if (hitPoints == maxHitPoints)
            {
                _state = State.FULL;
            }

            _bar.SetWidth(_width * fillFactor);
        }

        public void Update(GameTime gameTime)
        {
            this.QueryState();

            if (_state == State.REGEN)
            {
                _timer.Update(gameTime);
                
                if (_timer.IsComplete())
                {
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
        }
    }
}
