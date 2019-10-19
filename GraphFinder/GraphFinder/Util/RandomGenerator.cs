using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Util
{
    class RandomGenerator
    {
        public int RandomNodeCount()
        {
            return new Random().Next(0, 2);
        }

        public int RandomRowNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
