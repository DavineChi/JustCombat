using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JustCombat
{
    public class PrimitiveRectangle
    {
        public static Texture2D Create(int x, int y, int width, int height)
        {
            Texture2D result;

            GraphicsDevice graphicsDevice = JustCombat.graphics.GraphicsDevice;
            Rectangle rectangle = new Rectangle(x, y, width, height);
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            result = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }

            result.SetData(data);

            return result;
        }
    }
}
