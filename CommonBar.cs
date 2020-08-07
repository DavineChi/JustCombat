using Microsoft.Xna.Framework;

namespace JustCombat
{
    public abstract class CommonBar
    {
        private const int FRAME_THICKNESS = 1;

        protected float _width;
        protected PrimRectangle _frame;
        protected PrimRectangle _fillBar;

        public CommonBar(int xPosition, int yPosition, int width, int height)
        {
            _frame = new PrimRectangle((xPosition - FRAME_THICKNESS), (yPosition - FRAME_THICKNESS), (width + (FRAME_THICKNESS * 2)), (height + (FRAME_THICKNESS * 2)));
            _fillBar = new PrimRectangle((xPosition + 1), (yPosition + 1), (width - 2), (height - 2), true);
            _width = width - 2;
        }
        
        public abstract PrimRectangle GetFrame();
        public abstract PrimRectangle GetFillBar();
        public abstract void Update(Actor actor, GameTime gameTime);

        public float GetWidth() { return _width; }
    }
}
