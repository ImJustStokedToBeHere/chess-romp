using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class MoveSearch
    {




        public class Parameters
        {
            public string SearchMoves = String.Empty;
            public bool Ponder = false;
            public int WhiteTimeRemaining = 0;
            public int BlackTimeRemaining = 0;
            public int WhiteInc = 0;
            public int BlackInc = 0;
            public int MovesToGo = 0;
            public int Depth = 0;
            public int Nodes = 0;
            public int Mate = 0;
            public int MoveTime = 0;
            public bool Infinite = false;
        }
    }
}
