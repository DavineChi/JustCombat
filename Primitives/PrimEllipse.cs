using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace JustCombat.Primitives
{
    public class PrimEllipse : PrimShape
    {
        private const int DEFAULT_SIDES = 10;
        private int _sides;

        public PrimEllipse(Vector2 center, Vector2 radius) :
            this(center.X, center.Y, radius.X, radius.Y, DEFAULT_SIDES, Color.White, 1, 0)
        {
        }

        public PrimEllipse(float x, float y, float width, float height) :
            this(x, y, width, height, DEFAULT_SIDES, Color.White, 1, 0)
        {
        }

        public PrimEllipse(Vector2 center, Vector2 radius, float thickness) :
            this(center.X, center.Y, radius.X, radius.Y, DEFAULT_SIDES, Color.White, thickness, 0)
        {
        }

        public PrimEllipse(float x, float y, float width, float height, float thickness) :
            this(x, y, width, height, DEFAULT_SIDES, Color.White, thickness, 0)
        {
        }

        public PrimEllipse(float x, float y, float width, float height, float thickness, int layerDepth) :
            this(x, y, width, height, DEFAULT_SIDES, Color.White, thickness, layerDepth)
        {
        }

        public PrimEllipse(float x, float y, float width, float height, int sides, float thickness, int layerDepth) :
            this(x, y, width, height, sides, Color.White, thickness, layerDepth)
        {
        }

        public PrimEllipse(float x, float y, float width, float height, Color color, float thickness, int layerDepth) :
            this(x, y, width, height, DEFAULT_SIDES, color, thickness, layerDepth)
        {
        }

        public PrimEllipse(float x, float y, float width, float height, int sides, Color color, float thickness, int layerDepth)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _sides = sides;
            _color = color;
            _thickness = thickness;
            _layerDepth = layerDepth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawEllipse(new Vector2(_x, _y), new Vector2(_width, _height), _sides, _color, _thickness, _layerDepth);
        }
    }
}
