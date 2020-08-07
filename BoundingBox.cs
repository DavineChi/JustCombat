using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace JustCombat
{
    public class BoundingBox : ICollision
    {
        private Rectangle _rectangle;
        private PrimRectangle _primRectangle;

        public BoundingBox(float x, float y, float width, float height) :
            this(x, y, width, height, 1.0f)
        {
        }

        public BoundingBox(float x, float y, float width, float height, float scale)
        {
            float scaledWidth = (width * scale);
            float scaledHeight = (height * scale);

            _rectangle = new Rectangle((int)(x), (int)(y), (int)(scaledWidth), (int)(scaledHeight));
            _primRectangle = new PrimRectangle(x, y, scaledWidth, scaledHeight);
        }
        
        public bool Intersects(Rectangle other)
        {
            return _rectangle.Intersects(other);
        }

        // TODO: implement
        public bool WillIntersect(Rectangle other, GameTime delta)
        {
            throw new NotImplementedException();
        }

        public Rectangle GetRectangle()
        {
            return _rectangle;
        }

        public PrimRectangle GetPrimRectangle()
        {
            return _primRectangle;
        }

        public float GetPosX()
        {
            return _rectangle.X;
        }

        public void SetPosX(float x)
        {
            _rectangle.X = (int)(x);
        }

        public float GetPosY()
        {
            return _rectangle.Y;
        }

        public void SetPosY(float y)
        {
            _rectangle.Y = (int)(y);
        }

        public float GetWidth()
        {
            return _rectangle.Width;
        }

        public void SetWidth(float width)
        {
            _rectangle.Width = (int)(width);
        }

        public float GetHeight()
        {
            return _rectangle.Height;
        }

        public void SetHeight(float height)
        {
            _rectangle.Height = (int)(height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _primRectangle.Draw(spriteBatch);
        }
    }
}
