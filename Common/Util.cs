﻿using JustCombat.Collision;
using JustCombat.Entities;
using JustCombat.UI;
using System;

namespace JustCombat.Common
{
    public static class Util
    {
        public static int[] GetNewPosition(float dx, float dy, bool isRunning)
        {
            int[] result = new int[2];

            float _x = Player.Instance().GetX();
            float _y = Player.Instance().GetY();

            float newX = _x + dx;
            float newY = _y + dy;

            if (isRunning)
            {
                newX = (newX + (dx * Constants.PLAYER_SPEED_RUN));
                newY = (newY + (dy * Constants.PLAYER_SPEED_RUN));
            }

            else
            {
                newX = (newX + (dx * Constants.PLAYER_SPEED_WALK));
                newY = (newY + (dy * Constants.PLAYER_SPEED_WALK));
            }

            // Round up or down depending on the direction moved.
            if (dx > 0 || dy > 0)
            {
                newX = (int)(Math.Ceiling(newX));
                newY = (int)(Math.Ceiling(newY));
            }

            else
            {
                newX = (int)(Math.Floor(newX));
                newY = (int)(Math.Floor(newY));
            }

            result[0] = (int)(newX);
            result[1] = (int)(newY);

            return result;
        }

        public static bool BeyondMapScrollTransformBounds(int[] positions, int width, int height)
        {
            bool result = true;

            int futureX = positions[0];
            int futureY = positions[1];

            CollisionBox candidate = new CollisionBox(futureX, futureY, width, height);
            CollisionBox boundary = UserInterface.ScrollTransformBounds;

            if (candidate.Intersects(boundary))
            {
                result = false;
            }

            return result;
        }
    }
}
