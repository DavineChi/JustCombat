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

        public float GetX() { return _x; }
        public float GetY() { return _y; }
        public float GetWidth()  { return _width;  }
        public float GetHeight() { return _height; }

        public void SetX(float x) { _x = x; }
        public void SetY(float y) { _y = y; }
        public void SetWidth(float width)   { _width = width;   }
        public void SetHeight(float height) { _height = height; }

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
