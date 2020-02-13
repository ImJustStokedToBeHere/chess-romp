using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Romp
{
    class Move
    {
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

        // thanks wikipedia
        public static ulong ScanBitsForward(ulong board)
        {
            Debug.Assert(board != 0);
            return index64[((board ^ (board - 1)) * DEBRUIJN64) >> 58];
        }

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

        public static Move FromNotation(string note)
        {
            return SANTranslator.FromStdNotation(note);
        }

        public Square Src { get; set; } = (Square)Constants.NULL_SQUARE;
        public Square Dest { get; set; } = (Square)Constants.NULL_SQUARE;
        public PieceType Type { get; set; } = PieceType.Unknown;
        public SpecialtyMove Specialty { get; set; } = SpecialtyMove.Unknown;

        public Move() { }

        public Move(Square src, Square dest, PieceType type, SpecialtyMove spMove)
        {
            Src = src;
            Dest = dest;
            Type = type;
            Specialty = spMove;
        }

        public Move(Move move)
        {
            Src = move.Src;
            Dest = move.Dest;
            Type = move.Type;
            Specialty = move.Specialty;
        }

        public string AsNotationString() => SANTranslator.ToStdNotation(this);

    }
}
