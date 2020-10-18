using Microsoft.Xna.Framework;
using System;

namespace JustCombat.Collision
{
    public class BoundingCircle : ICollision
    {
        private float _x;
        private float _y;
        private float _radius;
        
        public BoundingCircle(float x, float y, float radius) :
            this(x, y, radius, 1.0f)
        {
        }

        public BoundingCircle(float x, float y, float radius, float scale)
        {

        }

        public float GetRadius()
        {
            return _radius;
        }

        public void SetRadius(float radius)
        {
            _radius = radius;
        }

        public float GetPosX()
        {
            return _x;
        }

        public void SetPosX(float x)
        {
            _x = x;
        }

        public float GetPosY()
        {
            return _y;
        }

        public void SetPosY(float y)
        {
            _y = y;
        }

        public float[] GetCenter()
        {
            return new float[] { _x, _y };
        }

        // TODO: implement
        public bool Intersects(PrimShape other)
        {
            throw new NotImplementedException();
        }

        // TODO: implement
        public bool WillIntersect(PrimShape other, GameTime delta)
        {
            throw new NotImplementedException();
        }
    }
}
