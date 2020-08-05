using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class InputHandler
    {
        public static void HandleInput()
        {
            KeyboardState state = Keyboard.GetState();
            Player player = Player.Instance();

            if (IsValidMovementKey(state))
            {
                bool running = Keyboard.GetState().IsKeyDown(Keys.LeftShift);



                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.WalkNorth();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.WalkEast();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.WalkSouth();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.WalkWest();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                player.Teleport(300.0f, 300.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                player.SetHitPoints(player.GetHitPoints() / 2);
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
                    state.IsKeyDown(Keys.D) ||
                    state.IsKeyDown(Keys.LeftShift));
        }
    }
}
