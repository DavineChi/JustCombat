
namespace JustCombat
{
    public class Direction
    {
        private float _heading;

        public Direction(float heading)
        {
            _heading = heading;
        }

        public float GetHeading()
        {
            return _heading;
        }

        public void SetHeading(float heading)
        {
            _heading = heading;
        }
    }
}
