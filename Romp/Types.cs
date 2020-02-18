using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    [Flags]
    enum CastlingRights
    {
        WhiteKing,
        WhiteQueen,
        BlackKing,
        BlackQueen
    }

    [Flags]
    enum Color
    {
        White,
        Black,
        All
    }

    enum Piece
    {
        None,
        WPawn = 1,
        WKnight,
        WBishop,
        WRook,
        WQueen,
        WKing,
        BPawn = 9,
        BKnight,
        BBishop,
        BRook,
        BQueen,
        BKing
    }

    [Flags]
    enum Direction
    {
        North = 8,
        East = 1,
        South = -North,
        West = -East,
        NorthEast = North + East,
        SouthEast = South + East,
        SouthWest = South + West,
        NorthWest = North + West
    }

    enum PieceType : Byte
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        Unknown
    }

    [Flags]
    enum SpecialtyMove : Byte
    {
        Quiet = 0x0,
        DoublePawnPush = 0x1,
        KingsideCastle = 0x2,
        QueensideCastle = 0x3,
        Capture = 0x4,
        EnpassantCapture = 0x5,
        KnightPromotion = 0x8,
        BishopPromotion = 0x9,
        RookPromotion = 0xA,
        QueenPromotion = 0xB,
        KnightPromotionCapture = 0xC,
        BishopPromotionCapture = 0xD,
        RookPromotionCapture = 0xE,
        QueenPromotionCapture = 0xF,
        Unknown
    }

    [Flags]
    enum MoveType
    {
        Captures,
        Quiets,
        Evasions,
        Legal
    }

    enum Square : Byte
    {
        A1, B1, C1, D1, E1, F1, G1, H1,
        A2, B2, C2, D2, E2, F2, G2, H2,
        A3, B3, C3, D3, E3, F3, G3, H3,
        A4, B4, C4, D4, E4, F4, G4, H4,
        A5, B5, C5, D5, E5, F5, G5, H5,
        A6, B6, C6, D6, E6, F6, G6, H6,
        A7, B7, C7, D7, E7, F7, G7, H7,
        A8, B8, C8, D8, E8, F8, G8, H8
    }

    static class Pieces
    {
        public const int WHITE = 0;
        public const int BLACK = 1;

        public const int PAWN = 0;
        public const int ROOK = 1;
        public const int KNIGHT = 2;
        public const int BISHOP = 3;
        public const int QUEEN = 4;
        public const int KING = 5;
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
