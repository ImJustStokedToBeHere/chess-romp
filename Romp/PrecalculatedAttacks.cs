
using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    static class SlidingPieceAttacks
    {
        private const ulong OUTER_SQUARES = Constants.MASK_FILE_A | Constants.MASK_FILE_H | Constants.MASK_RANK_1 | Constants.MASK_RANK_8;

        public static readonly ulong[] _rookMasks = new ulong[64];
        private static readonly ulong[] _bishopMasks = new ulong[64];

        static SlidingPieceAttacks()
        {
            // generate the rook masks            
            for (int i = 0; i < Constants.SQUARE_COUNT; i++)
            {
                // initialize the rook masks
                _rookMasks[i] = ~OUTER_SQUARES 
                    & (ConsecutiveSquares.GetBoard(Direction.North, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.South, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.East, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.West, i.Square()));

                // initialize the bishop masks
                _bishopMasks[i] = ~OUTER_SQUARES 
                    & (ConsecutiveSquares.GetBoard(Direction.NorthEast, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.SouthEast, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.NorthWest, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.SouthWest, i.Square()));


            }
        }
    }

    static class ConsecutiveSquares
    {
        private static readonly ulong[,] _boards = new ulong[8, 64];

        static ConsecutiveSquares()
        {
            for (int i = 0; i < Constants.SQUARE_COUNT; i++)
            {
                _boards[Direction.North.Integer(), i] = 0x0101010101010100UL << i;

                _boards[Direction.South.Integer(), i] = 0x0080808080808080UL >> (63 - i);

                _boards[Direction.East.Integer(), i] = 0x2UL * ((0x1UL << (i | 7)) - (0x1UL << i));

                _boards[Direction.West.Integer(), i] = (0x1UL << i) - (0x1UL << (i & 56));

                _boards[Direction.NorthWest.Integer(), i] = 
                    BoardUtils.MoveSquaresLeft(0x102040810204000UL, 7 - BoardUtils.File(i)) << (BoardUtils.Rank(i) * 8);

                _boards[Direction.NorthEast.Integer(), i] =
                    BoardUtils.MoveSquaresRight(0x8040201008040200UL, BoardUtils.File(i)) << (BoardUtils.Rank(i) * 8);

                _boards[Direction.SouthWest.Integer(), i] =
                    BoardUtils.MoveSquaresLeft(0x40201008040201UL, 7 - BoardUtils.File(i)) >> ((7 - BoardUtils.Rank(i)) * 8);

                _boards[Direction.SouthEast.Integer(), i] =
                    BoardUtils.MoveSquaresRight(0x2040810204080UL, BoardUtils.File(i)) >> ((7 - BoardUtils.Rank(i)) * 8);
            }
        }

        public static ulong GetBoard(Direction dir, Square srcSquare) 
        {
            return _boards[dir.Integer(), srcSquare.Integer()];
        }
    }
}
