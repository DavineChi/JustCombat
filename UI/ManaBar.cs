using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.UI
{
    public class ManaBar : FillBar, IDrawable
    {
        private const float REGEN_DELAY = 2.0f;

        private const float STEP_FN_LEVEL_01_TO_04  = 0.4f;
        private const float STEP_FN_LEVEL_05_TO_07  = 0.13f;
        private const float STEP_FN_LEVEL_08_TO_09  = 0.05f;
        private const float STEP_FN_LEVEL_10_AND_UP = 0.01f;

        public ManaBar(int xPosition, int yPosition, int width, int height, Actor actor) :
            base(xPosition, yPosition, width, height, actor)
        {
            _bar.SetColor(new Color(0, 100, 200));

            _state = State.FULL;
            _timer = new CooldownTimer(REGEN_DELAY);
        }

        private void QueryState()
        {
            //float hitPoints = (float)(_actor.GetHitPoints());
            //float maxHitPoints = (float)(_actor.GetMaxHitPoints());
            //float fillFactor = hitPoints / maxHitPoints;

            //if (_actor.GetState() == Actor.State.IN_COMBAT)
            //{
            //    _state = State.COMBAT;

            //    _timer.Reset();
            //}

            //else if (hitPoints < maxHitPoints)
            //{
            //    _state = State.REGEN;

            //    if (!_timer.IsRunning())
            //    {
            //        _timer.Start();
            //    }
            //}

            //else if (hitPoints == maxHitPoints)
            //{
            //    _state = State.FULL;
            //}

            //_bar.SetWidth(_width * fillFactor);
        }

        public void Update(GameTime gameTime)
        {
            this.QueryState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _bar.Draw(spriteBatch);
        }
    }
}
