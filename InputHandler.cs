using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class InputHandler
    {
        // Implementation reference: https://community.monogame.net/t/one-shot-key-press/11669

        private static KeyboardState _currentKeyState;
        private static KeyboardState _previousKeyState;

        public static void UpdateKeyboardState()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !(_previousKeyState.IsKeyDown(key));
        }

        public static void HandleInput()
        {
            UpdateKeyboardState();

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

            if (IsKeyPressed(Keys.OemOpenBrackets))
            {
                player.SetState(Player.State.IN_COMBAT);
            }

            if (IsKeyPressed(Keys.OemCloseBrackets))
            {
                player.SetState(Player.State.NORMAL);
            }

            if (IsKeyPressed(Keys.T))
            {
                player.Teleport(300.0f, 300.0f);
            }

            if (IsKeyPressed(Keys.H))
            {
                player.SetHitPoints(player.GetHitPoints() / 2);
            }

            if (IsKeyPressed(Keys.L))
            {
                Player.AddLevel();
            }

            if (IsKeyPressed(Keys.I) && !(JustCombat.InvPanel.IsDisplayed()))
            {
                JustCombat.InvPanel.SetDisplayed(true);
            }

            else if (IsKeyPressed(Keys.I) && JustCombat.InvPanel.IsDisplayed())
            {
                JustCombat.InvPanel.SetDisplayed(false);
            }

            if (IsKeyPressed(Keys.C) && !(JustCombat.CharPanel.IsDisplayed()))
            {
                JustCombat.CharPanel.SetDisplayed(true);
            }

            else if (IsKeyPressed(Keys.C) && JustCombat.CharPanel.IsDisplayed())
            {
                JustCombat.CharPanel.SetDisplayed(false);
            }
        }

        public static bool IsValidMovementKey(KeyboardState state)
        {
            return (state.IsKeyDown(Keys.W) ||
                    state.IsKeyDown(Keys.A) ||
                    state.IsKeyDown(Keys.S) ||
                    state.IsKeyDown(Keys.D));
        }
    }
}
