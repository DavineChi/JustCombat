using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public abstract class PrimShape
    {
        protected float _x;
        protected float _y;
        protected float _width;
        protected float _height;
        protected Color _color;
        protected float _thickness;
        protected int _layerDepth;

        public Color GetColor()
        {
            return _color;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
