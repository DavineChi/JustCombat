using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustCombat.Panels
{
    public class InfoPanel : Panel
    {
        private string _text;

        public InfoPanel(string text, int x, int y, int width, int height, Color color) :
            this(text, x, y, width, height, color, false)
        {
        }

        public InfoPanel(string text, int x, int y, int width, int height, Color color, bool hasBorder)
        {
            _text = text;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
            _panel = PrimRectangle.Create(x, y, width, height, color);

            if (hasBorder)
            {
                _border = new PrimRectangle(x, y, width, height, Color.White, 1, 0, false);
            }
        }

        public string GetText()
        {
            return _text;
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_displayed)
            {
                if (_border != null)
                {
                    _border.Draw(spriteBatch);
                }

                spriteBatch.Draw(_panel, new Vector2(_x, _y), Color.White);
                spriteBatch.DrawString(JustCombat.gameContent.FontConsolas10, _text, new Vector2(_x + 4, _y + 4), Color.White);
            }
        }
    }
}
