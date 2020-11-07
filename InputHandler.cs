using JustCombat.Common;
using JustCombat.Entities;
using JustCombat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace JustCombat
{
    public class InputHandler
    {
        // Implementation reference:
        // https://community.monogame.net/t/one-shot-key-press/11669

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
            return (_currentMouseState.RightButton  == ButtonState.Pressed) &&
                  !(_previousMouseState.RightButton == ButtonState.Pressed);
        }

        private static void TranslateMovement(float dx, float dy, bool isRunning)
        {
            Player player = Player.Instance();
            OrthographicCamera camera = JustCombat.WorldCamera;
            Vector2 screenCoords;
            int[] newPositions;
            int[] testPositions;
            bool translateCamera = false;

            newPositions = Util.GetNewPosition(dx, dy, isRunning);
            screenCoords = camera.WorldToScreen(newPositions[0], newPositions[1]);
            testPositions = new int[] { (int)(screenCoords.X), (int)(screenCoords.Y) };
            translateCamera = Util.BeyondMapScrollTransformBounds(testPositions, (int)(player.GetWidth()), (int)(player.GetHeight()));
            
            if (translateCamera)
            {
                int oldX = (int)(player.GetX());
                int oldY = (int)(player.GetY());

                int newX = newPositions[0];
                int newY = newPositions[1];

                int diffX = (oldX - newX) * (-1);
                int diffY = (oldY - newY) * (-1);

                camera.Move(new Vector2(diffX, diffY));

                // Update the TransformMatrix, because the camera has moved.
                JustCombat.TransformMatrix = camera.GetViewMatrix();
            }

            player.Move(newPositions, isRunning);
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

                InputHandler.TranslateMovement(dx, dy, isRunning);
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
                Blink();
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

            if (IsKeyPressed(Keys.I) && !(UserInterface.InventoryPanel.IsDisplayed()))
            {
                UserInterface.InventoryPanel.SetDisplayed(true);
            }

            else if (IsKeyPressed(Keys.I) && UserInterface.InventoryPanel.IsDisplayed())
            {
                UserInterface.InventoryPanel.SetDisplayed(false);
            }

            if (IsKeyPressed(Keys.C) && !(UserInterface.CharacterPanel.IsDisplayed()))
            {
                UserInterface.CharacterPanel.SetDisplayed(true);
            }

            else if (IsKeyPressed(Keys.C) && UserInterface.CharacterPanel.IsDisplayed())
            {
                UserInterface.CharacterPanel.SetDisplayed(false);
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

                Console.WriteLine("Debug toggled.");
            }

            if (keyboardState.IsKeyDown(Keys.OemPeriod))
            {
                OrthographicCamera camera = JustCombat.WorldCamera;
                JustCombat.TransformMatrix = camera.GetViewMatrix();

                camera.ZoomIn(0.1f);
            }

            if (keyboardState.IsKeyDown(Keys.OemComma))
            {
                OrthographicCamera camera = JustCombat.WorldCamera;
                JustCombat.TransformMatrix = camera.GetViewMatrix();

                camera.ZoomOut(0.1f);
            }

            if (IsKeyPressed(Keys.OemPlus))
            {
                Player.Instance().AddXP(300);
            }

            if (keyboardState.IsKeyDown(Keys.OemCloseBrackets))
            {
                Player.Instance().AddXP(300);
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

        private static void Blink()
        {
            Player player = Player.Instance();

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
                        if (actor is Player)
                        {
                            // Show the select / glove cursor...
                            UserInterface.Cursor = UserInterface.CursorList[0];
                        }

                        else
                        {
                            // Show the attack / sword cursor...
                            UserInterface.Cursor = UserInterface.CursorList[1];
                        }

                        JustCombat.UserInterface.GetCursorInfoPanel().SetText(actor.ToString());
                        JustCombat.UserInterface.GetCursorInfoPanel().SetDisplayed(true);

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
                        UserInterface.Cursor = UserInterface.CursorList[0];
                        JustCombat.UserInterface.GetCursorInfoPanel().SetText(string.Empty);
                        JustCombat.UserInterface.GetCursorInfoPanel().SetDisplayed(false);

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
