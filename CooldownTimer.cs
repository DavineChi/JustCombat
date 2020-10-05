using Microsoft.Xna.Framework;
using System;

namespace JustCombat
{
    public class CooldownTimer
    {
        private float _timerValue;
        private float _remainingTime;
        private bool _running;
        private int _counter;
        private GameTime _gameTime;

        public CooldownTimer(float timerValue)
        {
            _timerValue = timerValue;
            _remainingTime = 0;
            _running = false;
            _counter = 0;
    }

        public float GetRemainingTime()
        {
            return _remainingTime;
        }

        public void Update(GameTime gameTime)
        {
            if (_running)
            {
                _gameTime = gameTime;
                _remainingTime = _remainingTime - (float)(_gameTime.ElapsedGameTime.TotalSeconds);

                if (_remainingTime <= 0)
                {
                    _remainingTime = 0;
                    _running = false;

                    _counter++;
                }

                if (_counter >= Int32.MaxValue)
                {
                    throw new OverflowException("Internal counter overflow: " + _counter);
                }
            }
        }

        public void Start()
        {
            this.Reset();

            _running = true;
        }

        public void Reset()
        {
            _remainingTime = _timerValue;
            _counter = 0;
            _running = false;
        }

        public bool IsComplete()
        {
            return _remainingTime == 0.0f;
        }

        public bool IsRunning()
        {
            return _running;
        }

        public int Iterations()
        {
            return _counter;
        }

        public override string ToString()
        {
            string result = "";

            if (_gameTime == null)
            {
                result = "null";
            }

            else
            {
                result = _remainingTime.ToString("0.0");
            }

            return result;
        }
    }
}
