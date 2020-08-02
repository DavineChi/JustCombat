
namespace JustCombat
{
    public abstract class CommonBar
    {
        private const float FRAME_THICKNESS = 0.5f;

        protected float _width;
        protected PrimRectangle _frame;
        protected PrimRectangle _fillBar;

        public CommonBar(float xPosition, float yPosition, float width, float height)
        {
            _frame = new PrimRectangle((xPosition - FRAME_THICKNESS), (yPosition - FRAME_THICKNESS), (width + (FRAME_THICKNESS * 2)), (height + (FRAME_THICKNESS * 2)));
            _fillBar = new PrimRectangle(xPosition, yPosition, width, height);
            _width = width;
        }

        public abstract PrimRectangle GetFrame();
        public abstract PrimRectangle GetFillBar();
    }
}
