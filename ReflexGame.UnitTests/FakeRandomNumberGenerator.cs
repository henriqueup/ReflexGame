using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexGame.UnitTests
{
    class FakeRandomNumberGenerator : IRandomNumberGenerator
    {
        public int NextInt(int start, int limit)
        {
            return 20;
        }
    }
}
