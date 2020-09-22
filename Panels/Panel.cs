using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public abstract class Panel
    {
        protected string _id = "";
        protected float _x;
        protected float _y;
        protected float _width;
        protected float _height;
        protected Color _color;
        protected Texture2D _panel;
        protected bool _displayed = false;

        public string GetID()        { return _id; }
        public void SetID(string id) { _id = id; }

        public float GetX()      { return _x; }
        public float GetY()      { return _y; }
        public float GetWidth()  { return _width;  }
        public float GetHeight() { return _height; }

        public void SetX(float x) { _x = x; }
        public void SetY(float y) { _y = y; }

        public Color GetColor()
        {
            return _color;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public Texture2D GetPanel()
        {
            return _panel;
        }

        public bool IsDisplayed()
        {
            return _displayed;
        }

        public void SetDisplayed(bool displayed)
        {
            _displayed = displayed;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
