
namespace JustCombat.UI
{
    public class FillBar
    {
        protected float _width;
        protected PrimRectangle _bar;
        protected Actor _actor;

        public FillBar(int xPosition, int yPosition, int width, int height, Actor actor)
        {
            _bar = new PrimRectangle(xPosition, yPosition, width, height, true);
            _width = width;
            _actor = actor;
        }

        public Actor GetActor() { return _actor; }

        public void SetActor(Actor actor) { _actor = actor; }
    }
}
