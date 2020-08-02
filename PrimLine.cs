using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace JustCombat
{
    public class PrimLine : PrimShape
    {
        private float _x1;
        private float _y1;
        private float _x2;
        private float _y2;

        public PrimLine(float x1, float y1, float x2, float y2) :
            this(x1, y1, x2, y2, Color.White)
        {
        }

        public PrimLine(float x1, float y1, float x2, float y2, float thickness) :
            this(x1, y1, x2, y2, Color.White, thickness)
        {
        }

        public PrimLine(float x1, float y1, float x2, float y2, Color color) :
            this(x1, y1, x2, y2, color, 1)
        {
        }

        public PrimLine(float x1, float y1, float x2, float y2, Color color, float thickness) :
            this(x1, y1, x2, y2, color, thickness, 0)
        {
        }

        public PrimLine(float x1, float y1, float x2, float y2, Color color, float thickness, float layerDepth)
        {
            _x1 = x1;
            _x2 = x2;
            _y1 = y1;
            _y2 = y2;
            _color = color;
            _thickness = thickness;
            _layerDepth = layerDepth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(_x1, _y1, _x2, _y2, _color, _thickness, _layerDepth);
        }
    }
}
