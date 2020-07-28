using Microsoft.Xna.Framework;
using System;

namespace JustCombat
{
    public class BoundingBox : ICollision
    {
        private int _posX;
        private int _posY;
        private int _width;
        private int _height;

        private Rectangle _rectangle;

        public BoundingBox(int posX, int posY, int width, int height)
        {
            _posX = posX;
            _posY = posY;
            _width = width;
            _height = height;

            _rectangle = new Rectangle(_posX, _posY, _width, _height);
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
            return _posX;
        }

        public void SetPosX(int posX)
        {
            _posX = posX;
        }

        public int GetPosY()
        {
            return _posY;
        }

        public void SetPosY(int posY)
        {
            _posY = posY;
        }

        public int GetWidth()
        {
            return _width;
        }

        public void SetWidth(int width)
        {
            _width = width;
        }

        public int GetHeight()
        {
            return _height;
        }

        public void SetHeight(int height)
        {
            _height = height;
        }
    }
}
