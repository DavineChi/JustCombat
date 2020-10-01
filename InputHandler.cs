using JustCombat.Entities;
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

            if (IsKeyPressed(Keys.Q))
            {
                JustCombat.UserInterface.CoolDownTimer.Start(2.5f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                JustCombat.TargetingSystem.Release();
            }
        }

        public static void OnMouseHover()
        {
            MouseState state = Mouse.GetState();

            int mouseX = state.X;
            int mouseY = state.Y;

            Wraith wraith = JustCombat.Wraith;

            if (mouseX >= wraith.GetX() &&
                mouseX <= wraith.GetX() + wraith.GetWidth() * Constants.SPRITE_SCALE &&
                mouseY >= wraith.GetY() &&
                mouseY <= wraith.GetY() + wraith.GetHeight() * Constants.SPRITE_SCALE)
            {
                // Show the attack / sword cursor...
                JustCombat.Cursor = JustCombat.Cursor2[1];

                if (state.LeftButton == ButtonState.Pressed)
                {
                    JustCombat.TargetingSystem.Acquire(JustCombat.Wraith);
                }
            }

            else
            {
                // Show the select / glove cursor...
                JustCombat.Cursor = JustCombat.Cursor2[0];
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
