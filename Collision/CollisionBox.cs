using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JustCombat.Collision
{
    public class CollisionBox : ICollision
    {
        private Rectangle _rectangle;
        private PrimRectangle _primRectangle;

        public CollisionBox(float x, float y, float width, float height) :
            this(x, y, width, height, 1.0f)
        {
        }

        public CollisionBox(float x, float y, float width, float height, float scale)
        {
            float scaledWidth = (width * scale);
            float scaledHeight = (height * scale);

            _rectangle = new Rectangle((int)(x), (int)(y), (int)(scaledWidth), (int)(scaledHeight));
            _primRectangle = new PrimRectangle(x, y, scaledWidth, scaledHeight);
        }
        
        public bool Intersects(CollisionBox other)
        {
            bool result = false;

            Rectangle candidate = other.GetRectangle();

            float otherX = candidate.X;
            float otherY = candidate.Y;
            float otherW = candidate.Width;
            float otherH = candidate.Height;

            float thisX = _rectangle.X;
            float thisY = _rectangle.Y;
            float thisW = _rectangle.Width;
            float thisH = _rectangle.Height;

            if ((otherX + otherW) >= _primRectangle.GetX() && otherX <= (_primRectangle.GetX() + _primRectangle.GetWidth()) &&
                (otherY + otherH) >= _primRectangle.GetY() && otherY <= (_primRectangle.GetY() + _primRectangle.GetHeight()))
            {
                result = true;
            }

            return result;
        }

        public bool Intersects(PrimShape other)
        {
            if (other.GetType() == typeof(PrimRectangle))
            {
                Rectangle candidate = new Rectangle((int)(other.GetX()), (int)(other.GetY()), (int)(other.GetWidth()), (int)(other.GetHeight()));

                return _rectangle.Intersects(candidate);
            }

            else
            {
                throw new InvalidCastException();
            }
        }

        // TODO: implement
        public bool WillIntersect(PrimShape other, GameTime delta)
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
