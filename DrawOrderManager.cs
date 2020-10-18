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
            Panel inventoryPanel = JustCombat.InvPanel;
            Panel characterPanel = JustCombat.CharPanel;
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

            if (inventoryPanel.IsDisplayed())
            {
                inventoryPanel.Draw(spriteBatch);
            }

            if (characterPanel.IsDisplayed())
            {
                characterPanel.Draw(spriteBatch);
            }

            wraithOne.Draw(spriteBatch);
            wraithTwo.Draw(spriteBatch);

            //spriteBatch.Draw(Cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);

            JustCombat.UserInterface.Draw(spriteBatch);

            spriteBatch.Draw(JustCombat.Cursor, JustCombat.CursorPosition, Color.White);
        }
    }
}
