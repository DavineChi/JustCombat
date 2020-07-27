using Microsoft.Xna.Framework;
using System;

namespace JustCombat
{
    public class AnimationFactory
    {
        public static Animation CreateAnimationIdlePlayer()
        {
            Animation result = new Animation();

            int additionalFrames = 36;
            int[] baseSequence = { 32, 64, 0, 64, 32 };
            int[] finalSequence = new int[baseSequence.Length + additionalFrames];

            for (int k = 0; k < baseSequence.Length + additionalFrames; k++)
            {
                if (k < baseSequence.Length)
                {
                    finalSequence[k] = baseSequence[k];
                }

                else
                {
                    finalSequence[k] = 32;
                }
            }

            for (int i = 0; i < finalSequence.Length; i++)
            {
                int yOffset = finalSequence[i];

                // Idle player sprite farthest right on spritesheet:
                // (24 pixels wide) * (16 sprites to the right) = 384
                //result.AddFrame(new Rectangle(384, yOffset, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT), TimeSpan.FromSeconds(Constants.PLAYER_ANIMATION_SPEED));
            }

            return result;
        }
    }
}
