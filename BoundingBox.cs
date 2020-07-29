using Microsoft.Xna.Framework;
using System;

namespace JustCombat
{
    public class BoundingBox : ICollision
    {
        private Rectangle _rectangle;

        public BoundingBox(int x, int y, int width, int height)
        {
            _rectangle = new Rectangle(x, y, width, height);
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

        public int GetPosX()
        {
            return _rectangle.X;
        }

        public void SetPosX(int x)
        {
            _rectangle.X = x;
        }

        public int GetPosY()
        {
            return _rectangle.Y;
        }

        public void SetPosY(int y)
        {
            _rectangle.Y = y;
        }

        public int GetWidth()
        {
            return _rectangle.Width;
        }

        public void SetWidth(int width)
        {
            _rectangle.Width = width;
        }

        public int GetHeight()
        {
            return _rectangle.Height;
        }

        public void SetHeight(int height)
        {
            _rectangle.Height = height;
        }
    }
}
