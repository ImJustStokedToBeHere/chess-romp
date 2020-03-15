using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Romp
{
    static class BoardUtils
    {
        /// <summary>
        /// magic number for scanning bitboards
        /// </summary>
        private const ulong DEBRUIJN64 = 0x03f79d71b4cb0a89;

        private static readonly ulong[] index64 =
        {
            0, 47,  1, 56, 48, 27,  2, 60,
           57, 49, 41, 37, 28, 16,  3, 61,
           54, 58, 35, 52, 50, 42, 21, 44,
           38, 32, 29, 23, 17, 11,  4, 62,
           46, 55, 26, 59, 40, 36, 15, 53,
           34, 51, 20, 43, 31, 22, 10, 45,
           25, 39, 14, 33, 19, 30,  9, 24,
           13, 18,  8, 12,  7,  6,  5, 63
        };

        /// <summary>
        /// returns the file from the square index (0-64)
        /// </summary>
        /// <param name="square">the bit index of the board square 0..63</param>
        /// <returns>the column</returns>
        public static int File(int square)
        {
            return square % 8;
        }

        /// <summary>
        /// returns the rank from the square index (0-64)
        /// </summary>
        /// <param name="square">the bit index of the board square 0..63</param>
        /// <returns>the row</returns>
        public static int Rank(int square)
        {
            return square / 8;
        }

        /// <summary>
        /// flips the bit board over the vertical center
        /// </summary>
        /// <param name="board">board as uint64</param>
        /// <returns>flipped board</returns>
        /// <see>https://www.chessprogramming.org/Flipping_Mirroring_and_Rotating</see>
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

        /// <summary>
        /// flips the bit board over the vertical center
        /// </summary>
        /// <param name="board">board as uint64</param>
        /// <returns>flipped board</returns>
        /// <see>https://www.chessprogramming.org/Flipping_Mirroring_and_Rotating</see>
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

        /// <summary>
        /// flips board over horizontal and vertical to make a 180 rotation
        /// </summary>
        /// <param name="board">board as uint64</param>
        /// <returns></returns>
        public static ulong Rotate180(ulong board)
        {
            return HorizontalFlip(VerticalFlip(board));
        }

        /// <summary>
        /// counts all bits starting at the LSB
        /// </summary>
        /// <param name="board">board as 64 bit uint</param>
        /// <returns>count of bits set to 1</returns>
        public static int CountSetBits(ulong board)
        {
            ulong result = 0;
            while (board > 0)
            {
                result += board & 1;
                board >>= 1;
            }
            // dont like the cast but it should never be above 128 (theoretically)
            return (int)result;
        }

        /// <summary>
        /// scans for the LSB
        /// </summary>
        /// <param name="board">board to get LSB from</param>
        /// <returns>the index of the LSB or -1 if not present</returns>
        public static ulong ScanBitsForward(ulong board)
        {
            Debug.Assert(board != 0);
            return index64[((board ^ (board - 1)) * DEBRUIJN64) >> 58];
        }

        /// <summary>
        /// scans for the LSB
        /// </summary>
        /// <param name="board">board to get LSB from</param>
        /// <returns>the index of the MSB or -1 if not present</returns>
        public static ulong ScanBitsBackward(ulong board)
        {
            Debug.Assert(board != 0);
            board |= board >> 1;
            board |= board >> 2;
            board |= board >> 4;
            board |= board >> 8;
            board |= board >> 16;
            board |= board >> 32;
            return index64[(board * DEBRUIJN64) >> 58];
        }

        /// <summary>
        /// sets the LSB of the board returns it's idx
        /// </summary>
        /// <param name="board">board to evaluate, modifies the found LSB</param>
        /// <returns>the count of bits set in the board</returns>
        public static int PopLSB(ref ulong board)
        {
            var idx = CountSetBits(board);
            board &= board - 1;
            return idx;
        }

        /// <summary>
        /// moves all set bits sqCount to the left
        /// </summary>
        /// <param name="board">current board value</param>
        /// <param name="sqCount">count to move to the left</param>
        /// <returns></returns>
        public static ulong MoveSquaresLeft(ulong board, int sqCount)
        {
            ulong result = board;
            for (int i = 0; i < sqCount; i++)
                result = ((result >> 1) & (~Constants.MASK_FILE_H));
            return result;
        }

        /// <summary>
        /// moves all set bits sqCount to the right
        /// </summary>
        /// <param name="board">current board value</param>
        /// <param name="sqCount">count to move to the left</param>
        /// <returns></returns>
        public static ulong MoveSquaresRight(ulong board, int sqCount)
        {
            ulong result = board;
            for (int i = 0; i < sqCount; i++)
                result = ((result << 1) & (~Constants.MASK_FILE_A));
            return result;
        }
    }
}
