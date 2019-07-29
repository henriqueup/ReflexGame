using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexGame
{
    public class MyRandomNumberGenerator : IRandomNumberGenerator
    {
        Random rnd;

        public MyRandomNumberGenerator()
        {
            rnd = new Random();
        }

        public int NextInt(int start, int limit)
        {
            return rnd.Next(start, limit);
        }
    }
}
