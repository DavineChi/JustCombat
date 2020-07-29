using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustCombat
{
    public class SpriteSheet
    {
        private Texture2D _texture;
        private int _tileWidth;
        private int _tileHeight;

        public SpriteSheet(Texture2D texture, int tileWidth, int tileHeight)
        {
            _texture = texture;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public static Texture2D GetTexture(Texture2D texture, int x, int y, int width, int height)
        {
            Texture2D result = new Texture2D(JustCombat.graphics.GraphicsDevice, width, height);
            Rectangle cropRectangle = new Rectangle((x * width), (y * height), width, height);
            Color[] colorData = new Color[width * height];

            texture.GetData(0, cropRectangle, colorData, 0, colorData.Length);
            result.SetData(colorData);

            return result;
        }

        public Texture2D GetTexture(int x, int y)
        {
            return SpriteSheet.GetTexture(_texture, x, y, _tileWidth, _tileHeight);
        }

        public Texture2D GetTexture(int x, int y, int width, int height)
        {
            return SpriteSheet.GetTexture(_texture, x, y, width, height);
        }
    }
}
