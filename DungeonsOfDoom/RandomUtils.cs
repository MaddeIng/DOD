using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    static class RandomUtils
    {
        static Random random = new Random();

        public static int Randomizer(int min, int max)
        {
            return random.Next(min, max);
        }

        public static bool Percentage(int percent)
        {
            return Randomizer(0, 100) < percent;
        }
    }
}
