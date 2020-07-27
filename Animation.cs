using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace JustCombat
{
    public class Animation
    {
        private List<Frame> _frames = new List<Frame>();
        private SpriteSheet _spriteSheet;
        private int _currentFrame = 0;

        public Animation()
        {
            
        }

        public Animation(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public void AddFrame(TimeSpan timeSpan, int x, int y)
        {

        }

        public void AddFrame(int duration, int x, int y)
        {

        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Draw(spriteBatch, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch, float x, float y)
        {
            this.Draw(spriteBatch, x, y, this.GetWidth(), this.GetHeight());
        }

        public void Draw(SpriteBatch spriteBatch, float x, float y, float width, float height)
        {
            this.Draw(spriteBatch, x, y, width, height, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, float x, float y, float width, float height, Color color)
        {
            if (_frames.Count == 0)
            {
                return;
            }

            Frame frame = (Frame)_frames[_currentFrame];

            //spriteBatch.Draw(FumikoSheet, topLeftOfSprite, sourceRectangle, Color.White);
            //spriteBatch.Draw();
        }

        public int GetWidth()
        {
            return _frames[_currentFrame]._texture.Width;
        }

        public int GetHeight()
        {
            return _frames[_currentFrame]._texture.Height;
        }

        private class Frame
        {
            public Rectangle _rectangle;
            public Texture2D _texture;
            public TimeSpan _timeSpan;
            public int _duration;
            public int _x = 0;
            public int _y = 0;

            public Frame(Texture2D texture, TimeSpan timeSpan)
            {
                _texture = texture;
                _timeSpan = timeSpan;
            }

            public Frame(Texture2D texture, int duration)
            {
                _texture = texture;
                _duration = duration;
            }

            public Frame(TimeSpan timeSpan, int x, int y)
            {
                //_texture = _spriteSheet.GetTexture(x, y);
                _timeSpan = timeSpan;
                _x = x;
                _y = y;
            }

            public Frame(int duration, int x, int y)
            {
                //_texture = _spriteSheet.GetTexture(x, y);
                _duration = duration;
                _x = x;
                _y = y;
            }
        }
    }
}
