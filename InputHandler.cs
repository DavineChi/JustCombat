using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class InputHandler
    {
        public static void HandleInput(HealthBar healthBar)
        {
            KeyboardState state = Keyboard.GetState();
            Player player = Player.Instance();
            
            bool isRunning = Keyboard.GetState().IsKeyDown(Keys.LeftShift);

            if (IsValidMovementKey(state))
            {
                int dx = 0;
                int dy = 0;

                // North
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    dy = -1;
                }

                // East
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    dx = 1;
                }

                // South
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    dy = 1;
                }

                // West
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    dx = -1;
                }

                player.Move(dx, dy, isRunning);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemOpenBrackets))
            {
                player.SetState(Player.State.IN_COMBAT);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemCloseBrackets))
            {
                player.SetState(Player.State.NORMAL);
                healthBar.ResetTimer(); // TODO: ???
                //healthBar.ResetCooldownTimer();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                player.Teleport(300.0f, 300.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                player.SetHitPoints(player.GetHitPoints() / 2);
                healthBar.ResetTimer();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                Player.AddLevel();
            }
        }

        private static bool IsValidMovementKey(KeyboardState state)
        {
            return (state.IsKeyDown(Keys.W) ||
                    state.IsKeyDown(Keys.A) ||
                    state.IsKeyDown(Keys.S) ||
                    state.IsKeyDown(Keys.D));
        }
    }
}
