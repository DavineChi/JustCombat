using JustCombat.Entities;
using JustCombat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public class TargetingSystem
    {
        private static TargetingSystem _targetingSystem = null;

        private Entity _currentTarget;

        private TargetingSystem()
        {
            _currentTarget = null;
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
            Release();

            _currentTarget = target;

            UserInterface.Instance().GetTargetInfoCard().SetActor((Actor)(target));

            _currentTarget.GetBoundingBox().GetPrimRectangle().SetColor(Color.Gold);
        }

        public void Release()
        {
            if (_currentTarget != null)
            {
                _currentTarget.GetBoundingBox().GetPrimRectangle().SetColor(Color.Green);

                UserInterface.Instance().ClearTarget();
            }

            _currentTarget = null;
        }

        public Entity GetCurrentTarget()
        {
            return _currentTarget;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
