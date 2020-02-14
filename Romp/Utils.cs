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

        public static ulong VerticalFlip(ulong board)
        {
            // probably don't need to mask the first and last ones
            return ((0x00000000000000FF & (board >> 56))
                | (0x000000000000FF00 & (board >> 40))
                | (0x0000000000FF0000 & (board >> 24))
                | (0x00000000FF000000 & (board >> 8))
                | (0x000000FF00000000 & (board << 8))
                | (0x0000FF0000000000 & (board << 24))
                | (0x00FF000000000000 & (board << 40))
                | (0xFF00000000000000 & (board << 56)));
        }

        // https://www.chessprogramming.org/Flipping_Mirroring_and_Rotating
        public static ulong HorizontalFlip(ulong board)
        {
            const ulong k1 = 0x5555555555555555;
            const ulong k2 = 0x3333333333333333;
            const ulong k4 = 0x0f0f0f0f0f0f0f0f;

            board = ((board >> 1) & k1) | ((board & k1) << 1);
            board = ((board >> 2) & k2) | ((board & k2) << 2);
            board = ((board >> 4) & k4) | ((board & k4) << 4);

            return board;
        }

        public static ulong Rotate180(ulong board)
        {
            return HorizontalFlip(VerticalFlip(board));
        }

        // HACK: this is probably the slowest way I could possibly do this but it's not going to be used very often
        public static string DisplayAsBinaryBoard(ulong board)
        {
            var binary = Convert.ToString((long)board, 2).PadLeft(64, '0');
            var sb = new StringBuilder();

            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    string s = binary.Substring(i, 8);
                    string s2 = String.Empty;

                    for (int j = 7; j > -1; j--)
                    {
                        if (j == 0)
                            s2 += s[j];
                        else
                            s2 += s[j] + " ";
                    }

                    sb.AppendLine(s2);

                }
            }

            return sb.ToString();
        }
    }
}
