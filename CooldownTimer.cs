using Microsoft.Xna.Framework;

namespace JustCombat
{
    public class CooldownTimer
    {
        private float _timerValue;
        private float _duration;
        private bool _running = false;

        private GameTime _gameTime;

        public CooldownTimer()
        {
            _duration = 0;
        }

        public CooldownTimer(float timerValue)
        {
            _timerValue = timerValue;
            _duration = 0;
        }

        public float GetDuration()
        {
            return _duration;
        }

        public void Update(GameTime gameTime)
        {
            if (_running)
            {
                _gameTime = gameTime;
                _duration = _duration - (float)(_gameTime.ElapsedGameTime.TotalSeconds);

                if (_duration <= 0)
                {
                    _duration = 0;
                    _running = false;
                }
            }
        }

        public void Start()
        {
            this.Reset();

            _running = true;
        }

        public void Start(float duration)
        {
            _duration = duration;
            _running = true;
        }

        public void Reset()
        {
            _duration = _timerValue;
        }

        public bool IsComplete()
        {
            return _duration == 0.0f;
        }

        public bool IsRunning()
        {
            return _running;
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
                result = _duration.ToString("0.0");
            }

            return result;
        }
    }
}
