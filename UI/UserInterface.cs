﻿using JustCombat.Collision;
using JustCombat.Entities;
using JustCombat.Panels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace JustCombat.UI
{
    public class UserInterface
    {
        public static Texture2D Cursor;
        public static Texture2D[] CursorList;
        public static CharacterPanel CharacterPanel;
        public static InventoryPanel InventoryPanel;
        public static CollisionBox ScrollTransformBounds;
        public static CollisionBox ViewContainerBounds;

        private static UserInterface _userInterface = null;

        // TODO: Convert this to its own class:
        private static string[] actionKeys = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };

        private Player _player;
        private Actor _target;
        private SpriteSheet _cursorSheet;
        private Vector2 _cursorPosition;
        private Texture2D _topBarBackpanel;
        private Texture2D _actionBarPanel;
        private Texture2D _xpBarFrame;
        private ActorInfoCard _playerInfoCard;
        private ActorInfoCard _targetInfoCard;
        private InfoPanel _cursorInfoPanel;
        private SpriteFont _fontConsolas13;
        private SpriteFont _fontCandara10;
        private ExperienceBar _experienceBar;
        private bool _debug;

        private List<Actor> _actorList;

        //public CooldownTimer CoolDownTimer = new CooldownTimer();

        private UserInterface()
        {
            CharacterPanel = new CharacterPanel("Character", 20, 120, 320, 440, Color.Wheat);
            InventoryPanel = new InventoryPanel("Inventory", 960, 480, 220, 220, Color.CornflowerBlue);

            ScrollTransformBounds = new CollisionBox(480, 341, 240, 135);
            ViewContainerBounds = new CollisionBox(0, 100, Constants.SCREEN_WIDTH, (Constants.SCREEN_HEIGHT - 100));

            ScrollTransformBounds.GetPrimRectangle().SetColor(Color.Magenta);
            ViewContainerBounds.GetPrimRectangle().SetColor(Color.Magenta);

            _player = Player.Instance();
            _topBarBackpanel = JustCombat.GameContent.TopBarBackpanel;
            _actionBarPanel = JustCombat.GameContent.ActionBarPanel;
            _xpBarFrame = JustCombat.GameContent.ExperienceBarFrame;
            _playerInfoCard = new ActorInfoCard(_player, new Vector2(16.0f, 4.0f));
            _targetInfoCard = new ActorInfoCard(null);
            _cursorInfoPanel = new InfoPanel("", 998, (Constants.SCREEN_HEIGHT - 26), 200, 24, new Color(0.0f, 0.004f, 0.125f, 0.5f), true);
            _fontConsolas13 = JustCombat.GameContent.FontConsolas13;
            _fontCandara10 = JustCombat.GameContent.FontCandara10;
            _experienceBar = new ExperienceBar(285, 706, 630, 8, null);
            _debug = false;

            _actorList = new List<Actor>();

            InitCursors();
        }

        private void InitCursors()
        {
            CursorList = new Texture2D[2];
            _cursorSheet = new SpriteSheet(JustCombat.GameContent.Cursor, 48, 48);

            CursorList[0] = _cursorSheet.GetTexture("glove", 0, 0);
            CursorList[1] = _cursorSheet.GetTexture("sword", 1, 0);

            Cursor = CursorList[0];
        }

        public static UserInterface Instance()
        {
            if (_userInterface == null)
            {
                _userInterface = new UserInterface();
            }

            return _userInterface;
        }

        public void ClearTarget()
        {
            _targetInfoCard.SetActor(null);
        }

        public ActorInfoCard GetTargetInfoCard()
        {
            return _targetInfoCard;
        }

        public InfoPanel GetCursorInfoPanel()
        {
            return _cursorInfoPanel;
        }

        public bool InDebugMode()
        {
            return _debug;
        }

        public void SetDebug(bool debug)
        {
            _debug = debug;
        }

        public void Update(GameTime gameTime)
        {
            _cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            _target = (Actor)(JustCombat.TargetingSystem.GetCurrentTarget());

            _playerInfoCard.Update(gameTime);

            if (_target != null)
            {
                _targetInfoCard.Update(gameTime);
            }

            //CoolDownTimer.Update(gameTime);

            //MapScrollTransformBounds;

            _experienceBar.Update(gameTime);
            
            foreach (Actor actor in JustCombat.EntityContainer)
            {
                int width  = (int)(actor.GetWidth());
                int height = (int)(actor.GetHeight());

                Vector2 oldPosition = new Vector2(actor.GetX(), actor.GetY());
                Vector2 newPosition = JustCombat.WorldCamera.WorldToScreen(oldPosition);

                CollisionBox candidate = new CollisionBox(newPosition.X, newPosition.Y, width, height);

                if (candidate.Intersects(ViewContainerBounds))
                {
                    if (_actorList.Contains(actor))
                    {
                        continue;
                    }

                    else
                    {
                        _actorList.Add(actor);
                    }
                }

                else
                {
                    _actorList.Remove(actor);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();

            string currentXP = _player.GetExperiencePoints().ToString();
            string maximumXP = _player.GetMaxExperiencePoints().ToString();

            int screenMouseX = mouseState.X;
            int screenMouseY = mouseState.Y;

            Vector2 mouseVectorScreen = new Vector2(screenMouseX, screenMouseY);
            Vector2 mouseVectorWorld = JustCombat.WorldCamera.ScreenToWorld(mouseVectorScreen);

            int worldMouseX = (int)(mouseVectorWorld.X);
            int worldMouseY = (int)(mouseVectorWorld.Y);

            string target = JustCombat.TargetingSystem.ToString();

            // TODO: Need to keep this entire class and elements fixed to screen coords...
            //OrthographicCamera camera = JustCombat.WorldCamera;
            
            spriteBatch.Draw(_topBarBackpanel, new Vector2(0.0f, 0.0f), Color.White);
            spriteBatch.Draw(_actionBarPanel, new Vector2(((Constants.SCREEN_WIDTH / 2) - (_actionBarPanel.Width / 2)), (Constants.SCREEN_HEIGHT - _actionBarPanel.Height)), Color.White);

            int _counter = 324;

            for (int i = 0; i < actionKeys.Length; i++)
            {
                string text = actionKeys[i];

                spriteBatch.DrawString(_fontCandara10, text, new Vector2(_counter, 729), Color.White);

                _counter = _counter + 52;
            }

            if (_player.GetLevel() < Constants.MAXIMUM_PLAYER_LEVEL)
            {
                spriteBatch.Draw(_xpBarFrame, new Vector2(282, 703), Color.White);
                _experienceBar.Draw(spriteBatch);
                spriteBatch.DrawString(_fontCandara10, "XP   " + currentXP + " / " + maximumXP, new Vector2(560, 705), Color.White);
            }

            _playerInfoCard.Draw(spriteBatch);

            if (_target != null)
            {
                _targetInfoCard.Draw(spriteBatch);
            }

            if (InventoryPanel.IsDisplayed())
            {
                InventoryPanel.Draw(spriteBatch);
            }

            if (CharacterPanel.IsDisplayed())
            {
                CharacterPanel.Draw(spriteBatch);
            }

            if (_cursorInfoPanel.IsDisplayed())
            {
                _cursorInfoPanel.Draw(spriteBatch);
            }

            if (_debug)
            {
                spriteBatch.DrawString(_fontConsolas13, DateTime.Now.ToString(), new Vector2(500.0f, 10.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "X: " + _player.GetX() + ", Y: " + _player.GetY(), new Vector2(500.0f, 30.0f), Color.White);
                //spriteBatch.DrawString(_fontConsolas13, "empty3: ", new Vector2(500.0f, 50.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, target, new Vector2(500.0f, 70.0f), Color.White);

                //spriteBatch.DrawString(_font, "CD: " + CoolDownTimer.ToString(), new Vector2(550.0f, 50.0f), Color.White);

                spriteBatch.DrawString(_fontConsolas13, "Visible Actors: " + _actorList.Count.ToString(), new Vector2(730.0f, 10.0f), Color.White);
                //spriteBatch.DrawString(_fontConsolas13, "empty2: ", new Vector2(730.0f, 30.0f), Color.White);
                //spriteBatch.DrawString(_fontConsolas13, "empty3: ", new Vector2(730.0f, 50.0f), Color.White);
                //spriteBatch.DrawString(_fontConsolas13, "empty4: ", new Vector2(730.0f, 70.0f), Color.White);

                //spriteBatch.DrawString(_font, "Health " + _player.GetHitPoints().ToString() + " / " + _player.GetMaxHitPoints().ToString(), new Vector2(744.0f, 6.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "   Player State: " + _player.GetState().ToString(), new Vector2(950.0f, 10.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "HealthBar State: " + _player.GetHealthBarState().ToString(), new Vector2(950.0f, 30.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "        Heading: " + _player.GetDirection().GetHeading().ToString(), new Vector2(950.0f, 50.0f), Color.White);
                spriteBatch.DrawString(_fontConsolas13, "  Taking Damage: " + _player._takingDamage.ToString(), new Vector2(950.0f, 70.0f), Color.White);

                spriteBatch.DrawString(_fontConsolas13, "Screen Mouse: (" + screenMouseX + "," + screenMouseY + ")", new Vector2(screenMouseX + 50, screenMouseY + 50), Color.White);
                spriteBatch.DrawString(_fontConsolas13, " World Mouse: (" + worldMouseX + "," + worldMouseY + ")", new Vector2(screenMouseX + 50, screenMouseY + 70), Color.White);

                ScrollTransformBounds.Draw(spriteBatch);
                ViewContainerBounds.Draw(spriteBatch);
            }

            spriteBatch.Draw(Cursor, _cursorPosition, Color.White);
        }
    }
}
