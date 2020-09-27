using Microsoft.Xna.Framework;

namespace JustCombat.UI
{
    public abstract class FillBar
    {
        public enum State { EMPTY, FULL, COMBAT, REGEN };

        protected float _width;
        protected PrimRectangle _bar;

        public FillBar(int xPosition, int yPosition, int width, int height)
        {
            _bar = new PrimRectangle(xPosition, yPosition, width, height, true);
            _width = width;
        }

        public abstract PrimRectangle GetBar();
        public abstract void Update(Actor actor, GameTime gameTime);

        public float GetWidth() { return _width; }
    }
}
