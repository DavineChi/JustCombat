using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JustCombat
{
    public class Player : Actor
    {
        private Texture2D currentDirection;
        private Texture2D[] playerDirections;

        public float Width { get; set; }
        public float Height { get; set; }

        public Direction Heading { get; set; }

        public Texture2D FumikoSheet { get; set; }
        public SpriteBatch Batch { get; set; }

        public Player(string name,
                      float x,
                      float y,
                      float width,
                      float height,
                      Direction heading,
                      SpriteBatch spriteBatch,
                      GameContent gameContent) :
            base(name, x, y, width, height, heading)
        {
            _name = name;
            _x = x;
            _y = y;
            Heading = heading;
            FumikoSheet = gameContent.FumikoImage;
            Batch = spriteBatch;

            playerDirections = new Texture2D[4];
        }

        public void WalkNorth()
        {
            Heading.SetHeading(0.0f);
        }

        public void WalkEast()
        {
            Heading.SetHeading(90.0f);
        }

        public void WalkSouth()
        {
            Heading.SetHeading(180.0f);
        }

        public void WalkWest()
        {
            Heading.SetHeading(270.0f);
        }

        public override bool MoveX(float dx, float dy, long delta)
        {
            throw new NotImplementedException();
        }

        public override bool MoveY(float dx, float dy, long delta)
        {
            throw new NotImplementedException();
        }
    }
}
