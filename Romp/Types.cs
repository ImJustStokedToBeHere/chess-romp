using System;

namespace Romp
{
    [Flags]
    internal enum CastlingRights
    {
        WhiteKing = 0x01,
        WhiteQueen = 0x02,
        BlackKing = 0x04,
        BlackQueen = 0x08
    }
    
    internal enum Color
    {
        White = 0,
        Black = 1,
        All = 2
    }

    [Flags]
    internal enum Direction
    {
        //North = 8,
        //East = 1,
        //South = -North,
        //West = -East,
        //NorthEast = North + East,
        //SouthEast = South + East,
        //SouthWest = South + West,
        //NorthWest = North + West
        North,
        South,
        East,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }

    [Flags]
    internal enum PieceType : Byte
    {
        Unknown = 0x0,
        Pawn = 0x1,
        Rook = 0x2,
        Knight = 0x4,
        Bishop = 0x8,
        Queen = 0x10,
        King = 0x20
    }

    [Flags]
    internal enum MoveType 
    {
        Null = 0x0,        
        DoublePawnPush = 0x1,
        KingsideCastle = 0x2,
        QueensideCastle = 0x4,
        Capture = 0x10,
        EnpassantCapture = 0x20,
        Promotion = 0x40,
        Quiet = 0x100
    }

    //[Flags]
    //internal enum MoveType
    //{
    //    Captures,
    //    Quiets,
    //    Evasions,
    //    Legal
    //}

    internal enum Square : Byte
    {
        A1, B1, C1, D1, E1, F1, G1, H1,
        A2, B2, C2, D2, E2, F2, G2, H2,
        A3, B3, C3, D3, E3, F3, G3, H3,
        A4, B4, C4, D4, E4, F4, G4, H4,
        A5, B5, C5, D5, E5, F5, G5, H5,
        A6, B6, C6, D6, E6, F6, G6, H6,
        A7, B7, C7, D7, E7, F7, G7, H7,
        A8, B8, C8, D8, E8, F8, G8, H8,
        Null
    }

    //internal static class Pieces
    //{
    //    public const int WHITE = 0;
    //    public const int BLACK = 1;

    //    public const int PAWN = 0;
    //    public const int ROOK = 1;
    //    public const int KNIGHT = 2;
    //    public const int BISHOP = 3;
    //    public const int QUEEN = 4;
    //    public const int KING = 5;
    //    public const int UNKNOWN = 6;
    //}

    static class TypeExtensions
    {
        public static int Integer(this Square s)
        {
            return (int)s;
        }

        public static uint Integer(this Direction d)
        {
            return (uint)d;
        }

        public static Square Square(this int i)
        {
            return (Square)i;
        }

        public static Square Square(this ulong i)
        {
            return (Square)i;
        }

        public static int Index(this PieceType p)
        {
            return (int)BoardUtils.ScanBitsForward((ulong)p);
        }

        public static int Index(this Color c)
        {
            return (int)c;
        }
    }

    /*
     Removing this stuff as it isn't as useful as imagined. It's not very functional.

     */
    //enum Rank : Byte
    //{
    //    _1 = 0,
    //    _2 = 1,
    //    _3 = 2,
    //    _4 = 3,
    //    _5 = 4,
    //    _6 = 5,
    //    _7 = 6,
    //    _8 = 7
    //}

    //enum File : Byte
    //{
    //    A = 0,
    //    B = 1,
    //    C = 2,
    //    D = 3,
    //    E = 4,
    //    F = 5,
    //    G = 6,
    //    H = 7
    //}
}
