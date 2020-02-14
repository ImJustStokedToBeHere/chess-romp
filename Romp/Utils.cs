using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    static class Utils
    {
        public static File FileOf(Square s)
        {
            return (File)((Byte)s & 7);
        }

        public static Rank RankOf(Square s)
        {
            return (Rank)((Byte)s >> 3);
        }

        public static Direction PawnPush(Color c)
        {
            return c == Color.White ? Direction.North : Direction.South;
        }

        public static ulong ClearRank(Rank r, ulong board)
        {
            return board & Constants.ClearRanks[(int)r];
        }
    }
}
