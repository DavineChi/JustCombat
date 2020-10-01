using JustCombat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public class TargetingSystem
    {
        //private static TargetingSystem _targetingSystem = null;

        private ActorInfoCard _targetInfoCard;
        private Entity _currentTarget;

        public TargetingSystem()
        {
        }

        //public static TargetingSystem Instance()
        //{
        //    if (_targetingSystem == null)
        //    {
        //        _targetingSystem = new TargetingSystem();
        //    }

        //    return _targetingSystem;
        //}

        public void Acquire(Entity target)
        {
            _currentTarget = target;

            if (target is Actor)
            {
                _targetInfoCard = new ActorInfoCard((Actor)(_currentTarget), new Vector2(244.0f, 4.0f));
            }
        }

        public void Release()
        {
            _currentTarget = null;
            _targetInfoCard = null;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_targetInfoCard != null)
            {
                _targetInfoCard.Draw(spriteBatch);
            }
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
                selected = _currentTarget.GetName();
            }

            return selected;
        }
    }
}
