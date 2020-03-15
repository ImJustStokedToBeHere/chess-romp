using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class TranspositionTable
    {
        enum NodeType { PV, ALL, CUT };

        class Entry
        {
            public ZobristKey Key;
            public NodeType Type;
            public double Created = DateTime.Now.ToOADate();
            public long Score;
            public int Depth;
            public Move Refutation;
            public Move Best;
        }
        
    }
}
