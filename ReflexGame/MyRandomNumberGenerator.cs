using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexGame
{
    public class MyRandomNumberGenerator : IRandomNumberGenerator
    {
        public int NextInt(int start, int limit)
        {
            Random rnd = new Random();
            return rnd.Next(start, limit);
        }
    }
}
