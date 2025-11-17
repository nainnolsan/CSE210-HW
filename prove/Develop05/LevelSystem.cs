using System;

namespace Develop05
{
    class LevelSystem
    {
        public int CalculateLevel(int score)
        {
            if (score <= 0)
            {
                return 1;
            }

            int level = 1;
            int threshold = 100;
            int remaining = score;

            while (remaining >= threshold)
            {
                remaining -= threshold;
                level++;
                threshold = threshold * 2;
            }

            return level;
        }
    }
}
