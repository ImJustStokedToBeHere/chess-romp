using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class ZobristKey
    {
        private static readonly ulong[] _positionIds = new ulong[781];
        
        static ZobristKey()
        {
            // using MersenneTwister instead of System.Random because I know how it works.
        }
    }
}
