using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat.Panels
{
    public class InventoryPanel : Panel
    {
        public InventoryPanel(string id, int x, int y, int width, int height) :
            this(id, x, y, width, height, Color.White)
        {
        }

        public InventoryPanel(string id, int x, int y, int width, int height, Color color)
        {
            _id = id;
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            _panel = PrimRectangle.Create(x, y, width, height, color);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_panel, new Vector2(_x, _y), Color.White);
        }
    }
}
