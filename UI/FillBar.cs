
namespace JustCombat.UI
{
    public class FillBar
    {
        public enum State { EMPTY, FULL, COMBAT, REGEN };

        protected State _state;
        protected float _width;
        protected PrimRectangle _bar;
        protected CooldownTimer _timer;
        protected Actor _actor;

        public FillBar(int xPosition, int yPosition, int width, int height, Actor actor)
        {
            _bar = new PrimRectangle(xPosition, yPosition, width, height, true);
            _width = width;
            _actor = actor;
        }

        public State GetState() { return _state; }
    }
}
