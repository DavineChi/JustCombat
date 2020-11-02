using JustCombat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled.Renderers;

namespace JustCombat
{
    public class DrawOrderManager
    {
        private static Player _player = Player.Instance();
        private static TiledMapRenderer _mapRenderer = JustCombat.GameMapRenderer;

        private static Wraith _wraithOne = JustCombat.WraithOne;
        private static Actor _wraithTwo = JustCombat.WraithTwo;

        public static void Draw(SpriteBatch spriteBatch)
        {
            Matrix matrix = JustCombat.WorldCamera.GetViewMatrix();

            float ellipseX = _player.GetX() + ((Constants.PLAYER_WIDTH  * Constants.SPRITE_SCALE) / 2);
            float ellipseY = _player.GetY() +  (Constants.PLAYER_HEIGHT * Constants.SPRITE_SCALE);

            _mapRenderer.Draw(matrix, null, null, 0);

            spriteBatch.DrawEllipse(new Vector2(ellipseX, (ellipseY - 2)), new Vector2(24, 10), 10, Color.White, 1, 0);

            _player.Draw(spriteBatch);

            _wraithOne.Draw(spriteBatch);
            _wraithTwo.Draw(spriteBatch);
        }
    }
}
