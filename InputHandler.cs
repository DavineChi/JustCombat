using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class InputHandler
    {
        // Implementation reference: https://community.monogame.net/t/one-shot-key-press/11669

        private static KeyboardState _currentKeyState;
        private static KeyboardState _previousKeyState;

        private static MouseState _currentMouseState;
        private static MouseState _previousMouseState;

        public static void UpdateKeyboardState()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }

        public static void UpdateMouseState()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !(_previousKeyState.IsKeyDown(key));
        }

        public static bool RightButtonPressed()
        {
            return (_currentMouseState.RightButton == ButtonState.Pressed) &&
                  !(_previousMouseState.RightButton == ButtonState.Pressed);
        }

        public static void HandleInput()
        {
            UpdateKeyboardState();
            UpdateMouseState();

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Player player = Player.Instance();
            
            bool isRunning = Keyboard.GetState().IsKeyDown(Keys.LeftShift);

            if (IsValidMovementKey(keyboardState))
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
                player.SetState(Player.ActorState.IN_COMBAT);
            }

            if (IsKeyPressed(Keys.OemCloseBrackets))
            {
                player.SetState(Player.ActorState.NORMAL);
            }

            if (IsKeyPressed(Keys.T))
            {
                float heading = player.GetDirection().GetHeading();

                float currentX = player.GetX();
                float currentY = player.GetY();

                float distance = 150.0f;

                if (heading == 0)
                {
                    player.Teleport(currentX, (currentY - distance));
                }

                if (heading == 90)
                {
                    player.Teleport((currentX + distance), currentY);
                }

                if (heading == 180)
                {
                    player.Teleport(currentX, (currentY + distance));
                }

                if (heading == 270)
                {
                    player.Teleport((currentX - distance), currentY);
                }

                //player.Teleport(300.0f, 300.0f);
            }

            if (IsKeyPressed(Keys.G))
            {
                player.RemoveHitPoints(10);
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

            //if (IsKeyPressed(Keys.Q))
            //{
            //    JustCombat.UserInterface.CoolDownTimer.Start(2.5f);
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                JustCombat.TargetingSystem.Release();
            }

            if (IsKeyPressed(Keys.F5))
            {
                if (JustCombat.UserInterface.InDebugMode())
                {
                    JustCombat.UserInterface.SetDebug(false);
                }

                else
                {
                    JustCombat.UserInterface.SetDebug(true);
                }
            }

            if (RightButtonPressed())
            {
                Entity entity = TargetingSystem.Instance().GetCurrentTarget();

                if (entity != null)
                {
                    if (entity is Actor)
                    {
                        Actor victim = (Actor)(entity);

                        victim.RemoveHitPoints(13);
                    }
                }
            }
        }

        public static void OnMouseHover()
        {
            MouseState state = Mouse.GetState();
            
            foreach (Entity entity in JustCombat.EntityContainer)
            {
                if (entity is Actor)
                {
                    Actor actor = (Actor)(entity);

                    if (actor.MouseOver(state))
                    {
                        // Show the attack / sword cursor...
                        JustCombat.Cursor = JustCombat.Cursor2[1];

                        if (state.LeftButton == ButtonState.Pressed)
                        {
                            JustCombat.TargetingSystem.Acquire(actor);
                            break;
                        }

                        return;
                    }

                    else
                    {
                        // Show the select / glove cursor...
                        JustCombat.Cursor = JustCombat.Cursor2[0];

                        if (state.LeftButton == ButtonState.Pressed)
                        {
                            JustCombat.TargetingSystem.Release();
                        }
                    }
                }
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
