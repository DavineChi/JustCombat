using Microsoft.Xna.Framework;
using System;

namespace JustCombat
{
    public class BoundingBox : ICollision
    {
        private Rectangle _rectangle;

        public BoundingBox(float x, float y, float width, float height)
        {
            _rectangle = new Rectangle((int)(x), (int)(y), (int)(width), (int)(height));
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
    }
}
