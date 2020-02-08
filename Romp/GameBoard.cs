using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class GameState
    {
        public Color SideToMove = Color.White;
        public UInt64 MoveCount = 0;
    }

    class GameBoard
    {
        public const UInt64 WHITE_PAWNS_DEFAULT = 0x000000000000FF00;
        public const UInt64 BLACK_PAWNS_DEFAULT = 0x00FF000000000000;
        public const UInt64 ALL_PAWNS_DEFAULT = WHITE_PAWNS_DEFAULT | BLACK_PAWNS_DEFAULT;

        public const UInt64 WHITE_ROOKS_DEFAULT = 0x0000000000000081;
        public const UInt64 BLACK_ROOKS_DEFAULT = 0x8100000000000000;
        public const UInt64 ALL_ROOKS_DEFAULT = WHITE_ROOKS_DEFAULT | BLACK_ROOKS_DEFAULT;

        public const UInt64 WHITE_KNIGHTS_DEFAULT = 0x0000000000000042;
        public const UInt64 BLACK_KNIGHTS_DEFAULT = 0x4200000000000000;
        public const UInt64 ALL_KNIGHTS_DEFAULT = WHITE_KNIGHTS_DEFAULT | BLACK_KNIGHTS_DEFAULT;

        public const UInt64 WHITE_BISHOPS_DEFAULT = 0x0000000000000024;
        public const UInt64 BLACK_BISHOPS_DEFAULT = 0x2400000000000000;
        public const UInt64 ALL_BISHOPS_DEFAULT = WHITE_BISHOPS_DEFAULT | BLACK_BISHOPS_DEFAULT;

        public const UInt64 WHITE_QUEEN_DEFAULT = 0x0000000000000008;
        public const UInt64 BLACK_QUEEN_DEFAULT = 0x0800000000000000;
        public const UInt64 ALL_QUEEN_DEFAULT = WHITE_QUEEN_DEFAULT | BLACK_QUEEN_DEFAULT;

        public const UInt64 WHITE_KING_DEFAULT = 0x0000000000000010;
        public const UInt64 BLACK_KING_DEFAULT = 0x1000000000000000;
        public const UInt64 ALL_KING_DEFAULT = WHITE_KING_DEFAULT | BLACK_KING_DEFAULT;

        public UInt64 Pieces()
        {
            return AllPieces();
        }

        public UInt64 Pieces(PieceType type)
        {
            return 0;
        }

        public UInt64 Pieces(PieceType type, PieceType type2)
        {
            return 0;
        }

        public UInt64 Pieces(Color c, PieceType type)
        {
            return 0;
        }

        public UInt64 Pieces(Color c, PieceType type, PieceType type2)
        {
            return 0;
        }

        public Piece PieceOnSquare(Square s)
        {
            return Piece.None;
        }

        public bool SquareIsEmpty(Square s)
        {
            return true;
        }

        public int Count(Color c, PieceType type)
        {
            return 0;
        }

        public int Count(PieceType type)
        {
            return 0;
        }

        public List<Square> Squares(Color c)
        {
            return new List<Square>();
        }

        private UInt64 _blackPawns;
        private UInt64 _blackRooks;
        private UInt64 _blackKnights;
        private UInt64 _blackBishops;
        private UInt64 _blackQueen;
        private UInt64 _blackKing;

        private UInt64 _whitePawns;
        private UInt64 _whiteRooks;
        private UInt64 _whiteKnights;
        private UInt64 _whiteBishops;
        private UInt64 _whiteQueen;
        private UInt64 _whiteKing;


        private UInt64 AllPieces()
        {
            return AllBlackPieces() | AllWhitePieces();
        }

        private UInt64 AllBlackPieces()
        {
            return _blackPawns | _blackRooks | _blackKnights | _blackBishops | _blackQueen | _blackKing;
        }

        private UInt64 AllWhitePieces()
        {
            return _whitePawns | _whiteRooks | _whiteKnights | _whiteBishops | _whiteQueen | _whiteKing;
        }
    }
}
