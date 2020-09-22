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

        public PrimRectangle(float x, float y, float width, float height, Color color) :
            this(x, y, width, height, color, 1)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, float thickness) :
            this(x, y, width, height, Color.White, thickness)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, bool fill) :
            this(x, y, width, height, Color.White, 1, 0, fill)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, float thickness, bool fill) :
            this(x, y, width, height, Color.White, thickness, 0, fill)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness) :
            this(x, y, width, height, color, thickness, 0)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness, int layerDepth) :
            this(x, y, width, height, color, thickness, layerDepth, false)
        {
        }

        public PrimRectangle(float x, float y, float width, float height, Color color, float thickness, int layerDepth, bool fill)
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

        public static Texture2D Create(int x, int y, int width, int height)
        {
            return PrimRectangle.Create(x, y, width, height, Color.White);
        }

        public static Texture2D Create(int x, int y, int width, int height, Color color)
        {
            Texture2D result;

            GraphicsDevice graphicsDevice = JustCombat.graphics.GraphicsDevice;
            Rectangle rectangle = new Rectangle(x, y, width, height);
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            result = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            result.SetData(data);

            return result;
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
