using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace JustCombat
{
    public class PrimRectangle : PrimShape
    {
        private bool _fill;

        public PrimRectangle(float x, float y, float width, float height) :
            this(x, y, width, height, Color.White)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, float thickness) :
            this(x, y, width, height, Color.White, thickness)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color) :
            this(x, y, width, height, color, 1)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness) :
            this(x, y, width, height, color, thickness, 0)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness, float layerDepth) :
            this(x, y, width, height, color, thickness, layerDepth, false)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness, float layerDepth, bool fill)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
            _thickness = thickness;
            _layerDepth = layerDepth;
            _fill = fill;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_fill)
            {
                spriteBatch.FillRectangle(_x, _y, _width, _height, _color, _layerDepth);
            }

            else
            {
                spriteBatch.DrawRectangle(_x, _y, _width, _height, _color, _thickness, _layerDepth);
            }
        }
    }
}
