using JustCombat.Collision;
using JustCombat.Primitives;

namespace JustCombat.Entities
{
    public abstract class Entity
    {
        protected string _name = "";
        protected float _x;
        protected float _y;
        protected float _width;
        protected float _height;
        protected CollisionBox _boundingBox;
        protected CollisionBox _collisionBox;
        protected BoundingCircle _boundingCircle;
        protected PrimEllipse _primEllipse;
        protected int _hitPoints;
        protected int _previousHitPoints;
        protected int _maxHitPoints;

        public string GetName()          { return _name; }
        public void SetName(string name) { _name = name; }

        public float GetX()      { return _x; }
        public float GetY()      { return _y; }
        public float GetWidth()  { return _width;  }
        public float GetHeight() { return _height; }

        public void SetX(float x) { _x = x; }
        public void SetY(float y) { _y = y; }

        public CollisionBox GetBoundingBox()  { return _boundingBox;  }
        public CollisionBox GetCollisionBox() { return _collisionBox; }

        public void SetBoundingBox(CollisionBox boundingBox)   { _boundingBox = boundingBox;   }
        public void SetCollisionBox(CollisionBox collisionBox) { _collisionBox = collisionBox; }
    }
}
