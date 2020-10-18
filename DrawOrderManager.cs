using JustCombat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled.Renderers;

namespace JustCombat
{
    public class DrawOrderManager
    {
        public static void Draw(SpriteBatch spriteBatch)
        {
            Player player = Player.Instance();
            TiledMapRenderer mapRenderer = JustCombat.GameMapRenderer;
            OrthographicCamera camera = JustCombat.WorldCamera;
            Actor wraithOne = JustCombat.WraithOne;
            Actor wraithTwo = JustCombat.WraithTwo;

            float ellipseX = player.GetX() + ((Constants.PLAYER_WIDTH  * Constants.SPRITE_SCALE) / 2);
            float ellipseY = player.GetY() +  (Constants.PLAYER_HEIGHT * Constants.SPRITE_SCALE);

            mapRenderer.Draw(camera.GetViewMatrix(), null, null, 0);

            if (JustCombat.UserInterface.InDebugMode())
            {
                JustCombat.MapTransformBounds.Draw(spriteBatch);
            }

            //ObstacleTest.Draw(spriteBatch);

            spriteBatch.DrawEllipse(new Vector2(ellipseX, (ellipseY - 2)), new Vector2(24, 10), 10, Color.White, 1, 0);

            player.Draw(spriteBatch);

            //Vector2 wraithOnePosition = camera.ScreenToWorld(new Vector2(wraithOne.GetX(), wraithOne.GetY()));
            //Vector2 wraithTwoPosition = camera.ScreenToWorld(new Vector2(wraithTwo.GetX(), wraithTwo.GetY()));

            Vector2 wraithOnePosition = camera.WorldToScreen(new Vector2(wraithOne.GetX(), wraithOne.GetY()));
            Vector2 wraithTwoPosition = camera.WorldToScreen(new Vector2(wraithTwo.GetX(), wraithTwo.GetY()));

            wraithOne.Draw(spriteBatch);
            wraithTwo.Draw(spriteBatch);

            spriteBatch.DrawString(JustCombat.GameContent.FontConsolas12, wraithOnePosition.ToString(), new Vector2(1000, 400), Color.White);
            spriteBatch.DrawString(JustCombat.GameContent.FontConsolas12, wraithTwoPosition.ToString(), new Vector2(1000, 420), Color.White);

            //spriteBatch.Draw(Cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);

            JustCombat.UserInterface.Draw(spriteBatch);

            spriteBatch.Draw(JustCombat.Cursor, JustCombat.CursorPosition, Color.White);
        }
    }
}
