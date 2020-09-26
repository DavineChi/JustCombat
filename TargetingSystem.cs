
namespace JustCombat
{
    public class TargetingSystem
    {
        private static TargetingSystem _targetingSystem = null;

        private Entity _currentTarget;

        protected TargetingSystem()
        {
        }

        public static TargetingSystem Instance()
        {
            if (_targetingSystem == null)
            {
                _targetingSystem = new TargetingSystem();
            }

            return _targetingSystem;
        }

        public void Acquire(Entity target)
        {
            _currentTarget = target;
        }

        public void Release()
        {
            _currentTarget = null;
        }

        public override string ToString()
        {
            string selected;

            if (_currentTarget == null)
            {
                selected = "no_target";
            }

            else
            {
                selected = _currentTarget.ToString();
            }

            return selected;
        }
    }
}
