using System;

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

        private bool _cooldown;

        public HealthBar(float xPosition, float yPosition, float width, float height) :
            base(xPosition, yPosition, width, height)
        {
            _state = State.FULL;

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
            // TODO: implementation
            throw new NotImplementedException();
        }
    }
}
