using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Util
{
    class RandomGenerator
    {
        static Random random;

        static RandomGenerator()
        {
            random = new Random((int)DateTime.Now.Ticks);
        }

        public int RandomNodeCount()
        {
            return random.Next(0, 2);
        }

        public int RandomRowNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
