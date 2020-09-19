using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JustCombat
{
    public class Animation
    {
        private string _id;
        private List<Texture2D> _framesList;
        private Texture2D _currentFrame;
        private float _speed = 1.0f;
        private int _duration;
        private int _frameIndex = 0;
        private int _time = 0;

        public Animation(string id)
        {
            _id = id;
        }

        public Animation(string id, Texture2D[] frames, int duration)
        {
            _id = id;
            _framesList = Animation.FramesArrayToList(frames);
            _duration = duration;
        }

        public Animation(string id, List<Texture2D> frames, int duration)
        {
            _id = id;
            _framesList = frames;
            _duration = duration;
        }

        private static List<Texture2D> FramesArrayToList(Texture2D[] frames)
        {
            List<Texture2D> result = new List<Texture2D>();

            for (int i = 0; i < frames.Length; i++)
            {
                result.Add(frames[i]);
            }

            return result;
        }

        public string GetID()
        {
            return _id;
        }

        public void SetFramesList(Texture2D[] frames)
        {
            _framesList = Animation.FramesArrayToList(frames);
        }

        public void SetFramesList(List<Texture2D> framesList)
        {
            _framesList = framesList;
        }

        public int GetDuration()
        {
            return _duration;
        }

        public void SetDuration(int duration)
        {
            _duration = duration;
        }

        public void Update(GameTime gameTime)
        {
            int delta = gameTime.ElapsedGameTime.Milliseconds;

            _time = _time + delta;

            if (_time > (_duration * _speed))
            {
                _frameIndex++;

                if (_frameIndex > (_framesList.Count - 1))
                {
                    _frameIndex = 0;
                }

                _time = 0;
                _currentFrame = _framesList[_frameIndex];
            }
        }

        public void Draw(Vector2 position, float scale, SpriteBatch spriteBatch)
        {
            Draw((int)(position.X), (int)(position.Y), scale, spriteBatch);
        }

        public void Draw(int x, int y, float scale, SpriteBatch spriteBatch)
        {
            if (_currentFrame == null)
            {
                _currentFrame = _framesList[0];
            }

            spriteBatch.Draw(_currentFrame, new Vector2(x, y), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
