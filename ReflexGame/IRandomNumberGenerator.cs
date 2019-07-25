using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexGame
{
    public interface IRandomNumberGenerator
    {
        int NextInt(int start, int limit);
    }
}
