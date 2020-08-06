using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JustCombat
{
    public class AnimationFactory
    {
        public static Animation CreateAnimationIdlePlayer(SpriteSheet spriteSheet, int x, int y, int duration)
        {
            Animation result;
            Texture2D[] frames;

            int additionalFrames = 36;
            int[] baseSequence = { 1, 2, 0, 2, 1 };
            int[] finalSequence = new int[baseSequence.Length + additionalFrames];

            for (int k = 0; k < baseSequence.Length + additionalFrames; k++)
            {
                if (k < baseSequence.Length)
                {
                    finalSequence[k] = baseSequence[k];
                }

                else
                {
                    finalSequence[k] = 1;
                }
            }

            frames = new Texture2D[finalSequence.Length];

            for (int i = 0; i < finalSequence.Length; i++)
            {
                int yOffset = finalSequence[i];

                // Idle player sprite farthest right on spritesheet:
                // (24 pixels wide) * (16 sprites to the right) = 384
                //result.AddFrame(new Rectangle(384, yOffset, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT), TimeSpan.FromSeconds(Constants.PLAYER_ANIMATION_SPEED));

                frames[i] = spriteSheet.GetTexture(x, yOffset);
            }

            result = new Animation(frames, duration);

            return result;
        }
    }
}
