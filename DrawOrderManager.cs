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

            mapRenderer.Draw(camera.GetViewMatrix(), null, null, 0);

            if (JustCombat.UserInterface.InDebugMode())
            {
                JustCombat.MapTransformBounds.Draw(spriteBatch);
            }

            //ObstacleTest.Draw(spriteBatch);

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
