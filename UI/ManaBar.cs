using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ManaBar : FillBar
    {
        private const float REGEN_DELAY = 2.0f;

        private const float STEP_FN_LEVEL_01_TO_04  = 0.2f;
        private const float STEP_FN_LEVEL_05_TO_07  = 0.065f;
        private const float STEP_FN_LEVEL_08_TO_09  = 0.025f;
        private const float STEP_FN_LEVEL_10_AND_UP = 0.005f;

        public ManaBar(int xPosition, int yPosition, int width, int height) :
            base(xPosition, yPosition, width, height)
        {
            _bar.SetColor(new Color(0, 100, 200));

            _state = State.FULL;
            _timer = new CooldownTimer(REGEN_DELAY);
        }

        private void QueryState(Actor actor)
        {
            float hitPoints = (float)(actor.GetHitPoints());
            float maxHitPoints = (float)(actor.GetMaxHitPoints());
            float fillFactor = hitPoints / maxHitPoints;

            if (actor.GetState() == Actor.State.IN_COMBAT)
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

        public FillBar.State GetState()
        {
            return _state;
        }

        public override PrimRectangle GetBar()
        {
            return _bar;
        }

        public override void Update(Actor actor, GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);
        }
    }
}
