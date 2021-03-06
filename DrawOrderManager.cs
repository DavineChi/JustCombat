﻿using JustCombat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled.Renderers;

namespace JustCombat
{
    public class DrawOrderManager
    {
        private static Player _player = Player.Instance();
        private static TiledMapRenderer _mapRenderer = JustCombat.GameMapRenderer;

        private static Wraith _wraithOne = JustCombat.WraithOne;
        private static Actor _wraithTwo = JustCombat.WraithTwo;
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            OrthographicCamera camera = JustCombat.WorldCamera;
            Matrix matrix = camera.GetViewMatrix();

            //float ellipseX = _player.GetX() + ((Constants.PLAYER_WIDTH  * Constants.SPRITE_SCALE) / 2);
            //float ellipseY = _player.GetY() +  (Constants.PLAYER_HEIGHT * Constants.SPRITE_SCALE);

            //Vector2 playerEllipse = new Vector2(ellipseX, (ellipseY - 2));

            _mapRenderer.Draw(matrix, null, null, 0);

            foreach (Entity entity in JustCombat.EntityContainer)
            {
                Actor actor = (Actor)(entity);

                float padding = actor.GetHeight();
                float bottomY = actor.GetY() + actor.GetHeight();
                float offsetY = (Constants.SCREEN_HEIGHT + padding) - bottomY;

                Vector2 worldCoords  = camera.ScreenToWorld(0, offsetY);

                // A layer depth of 0.0f is front-most, 1.0f is back-most.
                float layer = (worldCoords.Y / 1000.0f);

                if (actor is Player)
                {
                    //spriteBatch.DrawEllipse(playerEllipse, new Vector2(24, 10), 10, Color.White, 1, 1);
                    _player.Draw(spriteBatch, layer);
                }

                else
                {
                    actor.Draw(spriteBatch, layer);
                }
            }
        }
    }
}
