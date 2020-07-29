using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustCombat
{
    public class PrimitiveRectangle
    {
        public static Texture2D CreateRectangle(int x, int y, int width, int height)
        {
            return PrimitiveRectangle.CreateRectangle(x, y, width, height, Color.White);
        }

        public static Texture2D CreateRectangle(int x, int y, int width, int height, Color color)
        {
            Texture2D result;
            GraphicsDevice graphicsDevice = JustCombat.graphics.GraphicsDevice;
            Rectangle rectangle = new Rectangle(x, y, width, height);
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            result = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            result.SetData(data);

            return result;
        }
    }
}
