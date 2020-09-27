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

        private FillBar.State _state;

        private GameTime _cooldownTimer;

        private float _secondsCounter;

        private bool _inCooldown;

        public ManaBar(int xPosition, int yPosition, int width, int height) :
            base(xPosition, yPosition, width, height)
        {
            Color barColor = new Color(0, 100, 200);

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
                _state = FillBar.State.COMBAT;
            }

            else if (hitPoints < maxHitPoints)
            {
                _state = FillBar.State.REGEN;
            }

            else if (hitPoints == maxHitPoints)
            {
                _state = FillBar.State.FULL;
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
            _secondsCounter = _secondsCounter + (float)(gameTime.ElapsedGameTime.TotalSeconds);
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
