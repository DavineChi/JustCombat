using System;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public abstract class Actor : Entity, IEventHandler
    {
        private const float REGEN_DELAY  = 2.0f;
        private const float COMBAT_DELAY = 3.0f;

        public enum ActorState { NORMAL, RESTED, IN_COMBAT, DEAD };
        public enum HealthBarState { EMPTY, REGEN, FULL, COMBAT };
        public enum Alignment { FRIENDLY, NEUTRAL, HOSTILE };

        protected int _level;
        protected ActorState _actorState;
        protected HealthBarState _healthState;
        protected Alignment _alignment;
        protected float _hitPointsFillFactor;
        protected CooldownTimer _hitPointsTimer;
        protected CooldownTimer _combatExitTimer;

        public bool _takingDamage;

        private Direction _heading;
        private bool _alive;

        public Actor(string name, float x, float y, float width, float height, float scale, Direction heading) :
            this(1, name, x, y, width, height, scale, heading, 80)
        {
        }

        public Actor(int level, string name, float x, float y, float width, float height, float scale, Direction heading) :
            this(level, name, x, y, width, height, scale, heading, 80)
        {
        }

        public Actor(int level, string name, float x, float y, float width, float height, float scale, Direction heading, int hitPoints)
        {
            _level = level;
            _name = name;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _boundingBox = new BoundingBox(x, y, width, height, scale);
            _hitPoints = hitPoints;
            _previousHitPoints = _hitPoints;
            _maxHitPoints = _hitPoints;
            _heading = heading;
            _alive = true;

            _hitPointsFillFactor = _hitPoints / _maxHitPoints;
            _hitPointsTimer = new CooldownTimer(REGEN_DELAY);
            _combatExitTimer = new CooldownTimer(COMBAT_DELAY);
            _takingDamage = false;
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

        public void CompareLastHitPoints()
        {
            if (_hitPoints < _previousHitPoints)
            {
                _previousHitPoints = _hitPoints;
                _takingDamage = true;
                _combatExitTimer.Reset();
            }
        }

        public void SetHitPoints(int hitPoints)
        {
            /* Qualify the hitPoints parameter, ensuring that
             * the value is non-negative and that it does not
             * cause this Actor's hit points to become greater
             * than the maximum.
             */
            if ((hitPoints >= 0) && (hitPoints <= _maxHitPoints))
            {
                _previousHitPoints = _hitPoints;
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

            _previousHitPoints = _hitPoints;

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

        public ActorState GetState()
        {
            return _actorState;
        }

        public HealthBarState GetHealthBarState()
        {
            return _healthState;
        }

        public void SetState(ActorState state)
        {
            _actorState = state;
        }

        protected void QueryState()
        {
            _hitPointsFillFactor = (float)(_hitPoints) / (float)(_maxHitPoints);

            CompareLastHitPoints();

            if (!_alive)
            {
                _actorState = ActorState.DEAD;
                _healthState = HealthBarState.EMPTY;
                _takingDamage = false;

                return;
            }

            if (_takingDamage)
            {
                _actorState = ActorState.IN_COMBAT;
                _healthState = HealthBarState.COMBAT;

                bool timerComplete = _combatExitTimer.IsComplete();
                int iterations = _combatExitTimer.Iterations();

                if (timerComplete && (iterations > 0))
                {
                    _takingDamage = false;
                }

                if (!_combatExitTimer.IsRunning())
                {
                    _combatExitTimer.Start();
                }
            }

            else
            {
                if (_hitPoints < _maxHitPoints)
                {
                    if (_combatExitTimer.IsComplete())
                    {
                        _actorState = ActorState.NORMAL;
                        _healthState = HealthBarState.REGEN;

                        if (!_hitPointsTimer.IsRunning())
                        {
                            _hitPointsTimer.Start();
                        }
                    }
                }

                if (_hitPoints == _maxHitPoints)
                {
                    _healthState = HealthBarState.FULL;
                }
            }
        }

        public float GetHitPointsFillFactor()
        {
            return _hitPointsFillFactor;
        }

        public Alignment GetAlignment()
        {
            return _alignment;
        }

        public void SetAlignment(Alignment alignment)
        {
            _alignment = alignment;
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
            this.GetBoundingBox().GetPrimRectangle().SetX(x);
            this.GetBoundingBox().GetPrimRectangle().SetY(y);
        }

        public bool MouseOver(MouseState state)
        {
            bool result = false;

            int mouseX = state.X;
            int mouseY = state.Y;

            if (mouseX >= this._x &&
                mouseX <= this._x + this._width * Constants.SPRITE_SCALE &&
                mouseY >= this._y &&
                mouseY <= this._y + this._height * Constants.SPRITE_SCALE)
            {
                result = true;
            }

            return result;
        }

        public override string ToString()
        {
            return "Level " + _level + " " + _name;
        }
    }
}
