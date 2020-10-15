using JustCombat.Entities;
using System;

namespace JustCombat
{
    public class HitPoints
    {
        public static int Calculate(Player player)
        {
            int hitPoints;

            if (player.GetType() != typeof(Player))
            {
                throw new ArgumentException("Invalid parameter type.");
            }

            else
            {
                hitPoints = CalculatePlayerHitPoints(player.GetLevel());
            }

            return hitPoints;
        }

        public static int Calculate(int level)
        {
            int hitPoints;

            if (level < 1)
            {
                throw new ArgumentException("Invalid parameter: " + level);
            }

            hitPoints = CalculatePlayerHitPoints(level);

            return hitPoints;
        }

        private static int CalculatePlayerHitPoints(int level)
        {
            double hitPoints;
            int convertedHitPoints;
            int newHitPoints;

            hitPoints = ((163 * (level * level)) / 9) - ((131 * level) / 3) + (1130 / 9);
            convertedHitPoints = (int)(Math.Ceiling(hitPoints));
            newHitPoints = ((convertedHitPoints / 5) * 5);

            if (newHitPoints < convertedHitPoints)
            {
                newHitPoints = newHitPoints + 5;
            }

            return newHitPoints;
        }

        private static int CalculateMonsterHitPoints(int level)
        {
            double hitPoints;
            int convertedHitPoints;
            int newHitPoints;

            hitPoints = (int)((10 * (Math.Pow(level, 3)) / 9) + (475 * ((Math.Pow(level, 2)) / 36)) - ((1165 * level) / 36) + (1675 / 18));
            convertedHitPoints = (int)(Math.Ceiling(hitPoints));
            newHitPoints = ((convertedHitPoints / 5) * 5);

            if (newHitPoints < convertedHitPoints)
            {
                newHitPoints = newHitPoints + 5;
            }

            return newHitPoints;
        }
    }
}
