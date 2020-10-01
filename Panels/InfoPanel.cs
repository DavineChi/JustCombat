using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustCombat.Panels
{
    public class InfoPanel : Panel
    {
        public InfoPanel(int x, int y, int width, int height, Color color)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
            _panel = PrimRectangle.Create(x, y, width, height, color);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_panel, new Vector2(_x, _y), Color.White);
            spriteBatch.DrawString(JustCombat.gameContent.FontConsolas10, "test", new Vector2(_x + 4, _y + 4), Color.White);
        }
    }
}
