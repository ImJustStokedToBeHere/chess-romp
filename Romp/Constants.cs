using System;
using System.Collections.Generic;

namespace Romp
{
    internal static class Constants
    {        
        public const int SQUARE_COUNT = 64;
        public const Byte NULL_SQUARE = SQUARE_COUNT;

        public const string FEN_START_STRING = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public const ulong CLEAR_RANK_1 = 0xFFFFFFFFFFFFFF00;
        public const ulong CLEAR_RANK_2 = 0xFFFFFFFFFFFF00FF;
        public const ulong CLEAR_RANK_3 = 0xFFFFFFFFFF00FFFF;
        public const ulong CLEAR_RANK_4 = 0xFFFFFFFF00FFFFFF;
        public const ulong CLEAR_RANK_5 = 0xFFFFFF00FFFFFFFF;
        public const ulong CLEAR_RANK_6 = 0xFFFF00FFFFFFFFFF;
        public const ulong CLEAR_RANK_7 = 0xFF00FFFFFFFFFFFF;
        public const ulong CLEAR_RANK_8 = 0x00FFFFFFFFFFFFFF;

        public const ulong MASK_RANK_1 = ~CLEAR_RANK_1;
        public const ulong MASK_RANK_2 = ~CLEAR_RANK_2;
        public const ulong MASK_RANK_3 = ~CLEAR_RANK_3;
        public const ulong MASK_RANK_4 = ~CLEAR_RANK_4;
        public const ulong MASK_RANK_5 = ~CLEAR_RANK_5;
        public const ulong MASK_RANK_6 = ~CLEAR_RANK_6;
        public const ulong MASK_RANK_7 = ~CLEAR_RANK_7;
        public const ulong MASK_RANK_8 = ~CLEAR_RANK_8;

        public const ulong CLEAR_FILE_A = 0xFEFEFEFEFEFEFEFE;
        public const ulong CLEAR_FILE_B = 0xFDFDFDFDFDFDFDFD;
        public const ulong CLEAR_FILE_C = 0xFBFBFBFBFBFBFBFB;
        public const ulong CLEAR_FILE_D = 0xF7F7F7F7F7F7F7F7;
        public const ulong CLEAR_FILE_E = 0xEFEFEFEFEFEFEFEF;
        public const ulong CLEAR_FILE_F = 0xDFDFDFDFDFDFDFDF;
        public const ulong CLEAR_FILE_G = 0xBFBFBFBFBFBFBFBF;
        public const ulong CLEAR_FILE_H = 0x7F7F7F7F7F7F7F7F;

        public const ulong MASK_FILE_A = ~CLEAR_FILE_A;
        public const ulong MASK_FILE_B = ~CLEAR_FILE_B;
        public const ulong MASK_FILE_C = ~CLEAR_FILE_C;
        public const ulong MASK_FILE_D = ~CLEAR_FILE_D;
        public const ulong MASK_FILE_E = ~CLEAR_FILE_E;
        public const ulong MASK_FILE_F = ~CLEAR_FILE_F;
        public const ulong MASK_FILE_G = ~CLEAR_FILE_G;
        public const ulong MASK_FILE_H = ~CLEAR_FILE_H;

        public const ulong BLACK_SQUARES = 0xAA55AA55AA55AA55;
        public const ulong WHITE_SQUARES = ~BLACK_SQUARES;

        public static ulong[] BitPositions =
{
             0x0000000000000001,
             0x0000000000000002,
             0x0000000000000004,
             0x0000000000000008,
             0x0000000000000010,
             0x0000000000000020,
             0x0000000000000040,
             0x0000000000000080,
             0x0000000000000100,
             0x0000000000000200,
             0x0000000000000400,
             0x0000000000000800,
             0x0000000000001000,
             0x0000000000002000,
             0x0000000000004000,
             0x0000000000008000,
             0x0000000000010000,
             0x0000000000020000,
             0x0000000000040000,
             0x0000000000080000,
             0x0000000000100000,
             0x0000000000200000,
             0x0000000000400000,
             0x0000000000800000,
             0x0000000001000000,
             0x0000000002000000,
             0x0000000004000000,
             0x0000000008000000,
             0x0000000010000000,
             0x0000000020000000,
             0x0000000040000000,
             0x0000000080000000,
             0x0000000100000000,
             0x0000000200000000,
             0x0000000400000000,
             0x0000000800000000,
             0x0000001000000000,
             0x0000002000000000,
             0x0000004000000000,
             0x0000008000000000,
             0x0000010000000000,
             0x0000020000000000,
             0x0000040000000000,
             0x0000080000000000,
             0x0000100000000000,
             0x0000200000000000,
             0x0000400000000000,
             0x0000800000000000,
             0x0001000000000000,
             0x0002000000000000,
             0x0004000000000000,
             0x0008000000000000,
             0x0010000000000000,
             0x0020000000000000,
             0x0040000000000000,
             0x0080000000000000,
             0x0100000000000000,
             0x0200000000000000,
             0x0400000000000000,
             0x0800000000000000,
             0x1000000000000000,
             0x2000000000000000,
             0x4000000000000000,
             0x8000000000000000
        };

        public static readonly string[] CoordinateStrs =
        {
            "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1",
            "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2",
            "A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3",
            "A4", "B4", "C4", "D4", "E4", "F4", "G4", "H4",
            "A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5",
            "A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6",
            "A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7",
            "A8", "B8", "C8", "D8", "E8", "F8", "G8", "H8"
        };

        public static readonly List<char> Ranks = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' };
        public static readonly List<char> Files = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

        public static readonly Dictionary<PieceType, char> InvertedPieceLookup = new Dictionary<PieceType, char>
        {
            {  PieceType.King, 'K' },
            {  PieceType.Queen, 'Q' },
            {  PieceType.Bishop, 'B' },
            {  PieceType.Knight,'N' },
            {  PieceType.Rook, 'R' }
        };

        public static readonly Dictionary<char, PieceType> PieceLookup = new Dictionary<char, PieceType>
        {
            { 'K', PieceType.King },
            { 'Q', PieceType.Queen },
            { 'B', PieceType.Bishop },
            { 'N', PieceType.Knight },
            { 'R', PieceType.Rook }
        };

    }
}
