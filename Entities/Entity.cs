
namespace JustCombat
{
    public abstract class Entity
    {
        protected string _name = "";
        protected float _x;
        protected float _y;
        protected float _width;
        protected float _height;
        protected BoundingBox _boundingBox;
        protected int _hitPoints;
        protected int _maxHitPoints;

        public string GetName()          { return _name; }
        public void SetName(string name) { _name = name; }

        public float GetX()      { return _x; }
        public float GetY()      { return _y; }
        public float GetWidth()  { return _width; }
        public float GetHeight() { return _height; }

        public void SetX(float x) { _x = x; }
        public void SetY(float y) { _y = y; }

        public BoundingBox GetBoundingBox() { return _boundingBox; }
        public void SetBoundingBox(BoundingBox boundingBox) { _boundingBox = boundingBox; }
    }
}
