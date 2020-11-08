﻿
namespace JustCombat
{
    public class ExperienceSystem
    {
        private static int[] _matrix = new int[] { 0,
                                                   400,
                                                   900,
                                                   1400,
                                                   2100,
                                                   2800,
                                                   3600,
                                                   4500,
                                                   5400,
                                                   6500,
                                                   7600,
                                                   8800,
                                                   10100,
                                                   11400,
                                                   12900,
                                                   14400,
                                                   16000,
                                                   17700,
                                                   19400,
                                                   21300,
                                                   23200,
                                                   25200,
                                                   27300,
                                                   29400,
                                                   31700,
                                                   34000,
                                                   36400,
                                                   38900,
                                                   41400,
                                                   44300,
                                                   47400,
                                                   50800,
                                                   54500,
                                                   58600,
                                                   62800,
                                                   67100,
                                                   71600,
                                                   76100,
                                                   80800,
                                                   85700,
                                                   90700,
                                                   95800,
                                                   101000,
                                                   106300,
                                                   111800,
                                                   117500,
                                                   123200,
                                                   129100,
                                                   135100,
                                                   141200,
                                                   147500,
                                                   153900,
                                                   160400,
                                                   167100,
                                                   173900,
                                                   180800,
                                                   187900,
                                                   195000,
                                                   202300,
                                                   209800 };

        public static int GetExperienceForNextLevel(int level)
        {
            if (level == Constants.MAXIMUM_PLAYER_LEVEL)
            {
                level = level - 1;
            }

            return _matrix[level];
        }
    }
}