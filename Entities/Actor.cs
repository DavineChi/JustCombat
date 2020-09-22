using System;

namespace JustCombat
{
    public abstract class Actor : Entity
    {
        public enum State { NORMAL, RESTED, IN_COMBAT, DEAD };

        private Direction _heading;
        private bool _alive;

        protected int _level;
        protected HealthBar _healthBar;

        private State _state;

        public Actor(string name, float x, float y, float width, float height, float scale, Direction heading)
        {
            _level = 1;
            _name = name;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _boundingBox = new BoundingBox(x, y, width, height, scale);
            _hitPoints = 80;
            _maxHitPoints = _hitPoints;
            _heading = heading;
            _alive = true;
        }

        public abstract bool Move(float dx, float dy, bool isRunning);

        public bool Intersects(BoundingBox other)
        {
            return _boundingBox.Intersects(other.GetPrimRectangle());
        }

        public void SetHeading(float heading)
        {
            _heading.SetHeading(heading);
        }

        public Direction GetDirection() { return _heading; }

        public int GetLevel() { return _level; }

        public void SetLevel(int level)
        {
            if (level >= 1)
            {
                _level = level;
            }

            else
            {
                throw new ArgumentException("Input parameter must not be less than 1: " + level);
            }
        }

        public void AddHitPoints(int addHitPointsAmount)
        {
            if ((_hitPoints + addHitPointsAmount) > _maxHitPoints)
            {
                _hitPoints = _maxHitPoints;
            }

            else
            {
                _hitPoints = _hitPoints + addHitPointsAmount;
            }
        }

        public int GetHitPoints() { return _hitPoints; }

        public void SetHitPoints(int hitPoints)
        {
            /* Qualify the hitPoints parameter, ensuring that
             * the value is non-negative and that it does not
             * cause this Actor's hit points to become greater
             * than the maximum.
             */
            if ((hitPoints >= 0) && (hitPoints <= _maxHitPoints))
            {
                _hitPoints = hitPoints;

                if (hitPoints > 0)
                {
                    _alive = true;
                }

                else
                {
                    _alive = false;
                }
            }

            else
            {
                if (hitPoints > _maxHitPoints)
                {
                    throw new ArithmeticException("Hit Points (" + hitPoints + ") cannot exceed maximum: " + _maxHitPoints);
                }

                else
                {
                    throw new ArgumentException("Input parameter must not be less than zero: " + hitPoints);
                }
            }
        }

        public void RemoveHitPoints(int removeHitPointsAmount)
        {
            // Still need to determine how to handle overkill cases.
            // int overkillAmount;

            if (removeHitPointsAmount >= _hitPoints)
            {
                // _overkillAmount = removeHitPointsAmount - _hitPoints;
                _hitPoints = 0;
                // Death has occurred!
                _alive = false;
            }

            else
            {
                _hitPoints = _hitPoints - removeHitPointsAmount;
            }

            // return overkillAmount;
        }

        public void AddMaxHitPoints(int addMaxHitPointsAmount)
        {
            if (addMaxHitPointsAmount >= 0)
            {
                _maxHitPoints = _maxHitPoints + addMaxHitPointsAmount;
            }

            else
            {
                throw new ArgumentException("Input parameter must not be less than zero: " + addMaxHitPointsAmount);
            }
        }

        public int GetMaxHitPoints() { return _maxHitPoints; }

        public void SetMaxHitPoints(int maxHitPoints)
        {
            if (maxHitPoints >= 1)
            {
                _maxHitPoints = maxHitPoints;
            }

            else
            {
                throw new ArgumentException("Input parameter must not be less than 1: " + maxHitPoints);
            }

            // TODO: This doesn't seem to make sense...
            if (_hitPoints > maxHitPoints)
            {
                _hitPoints = maxHitPoints;
            }
        }

        public HealthBar GetHealthBar()
        {
            return _healthBar;
        }

        public Actor.State GetState()
        {
            return _state;
        }

        public void SetState(Actor.State state)
        {
            _state = state;
        }

        public bool IsAlive()
        {
            return _alive;
        }

        public void Kill()
        {
            _hitPoints = 0;
            _alive = false;
        }

        public void Revive()
        {
            _hitPoints = (int)(Math.Ceiling(_maxHitPoints * 0.25));
            _alive = true;
        }

        public void Teleport(float x, float y)
        {
            this.SetX(x);
            this.SetY(y);
        }
    }
}
