﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class GameState
    {
        public Color SideToMove = Color.White;
        public ulong MoveCount = 0;
    }

    // UNDONE: maybe we'll need  an enumerator interface at some point
    // : IEnumerable<ulong>
    class GameBoard 
    {
#region ENUMERABLE INTERFACE
        //public class BoardEnum : IEnumerator<ulong>
        //{
        //    private ulong[] _bitBoards;

        //    // Enumerators are positioned before the first element
        //    // until the first MoveNext() call.
        //    int position = -1;

        //    public BoardEnum(ulong[] list)
        //    {
        //        _bitBoards = list;
        //    }

        //    public bool MoveNext()
        //    {
        //        position++;
        //        return (position < _bitBoards.Length);
        //    }

        //    public void Reset()
        //    {
        //        position = -1;
        //    }

        //    public ulong Current
        //    {
        //        get
        //        {
        //            try
        //            {
        //                return _bitBoards[position];
        //            }
        //            catch (IndexOutOfRangeException)
        //            {
        //                throw new InvalidOperationException();
        //            }
        //        }
        //    }

        //    object IEnumerator.Current { get; }

        //    #region IDisposable Support
        //    private bool disposedValue = false; // To detect redundant calls

        //    protected virtual void Dispose(bool disposing)
        //    {
        //        if (!disposedValue)
        //        {
        //            if (disposing)
        //            {
        //                // TODO: dispose managed state (managed objects).
        //            }

        //            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //            // TODO: set large fields to null.

        //            disposedValue = true;
        //        }
        //    }

        //    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //    // ~BoardEnum()
        //    // {
        //    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    //   Dispose(false);
        //    // }

        //    // This code added to correctly implement the disposable pattern.
        //    public void Dispose()
        //    {
        //        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //        Dispose(true);
        //        // TODO: uncomment the following line if the finalizer is overridden above.
        //        // GC.SuppressFinalize(this);
        //    }
        //}

#endregion

        public const ulong WHITE_PAWNS_DEFAULT = 0x000000000000FF00;
        public const ulong BLACK_PAWNS_DEFAULT = 0x00FF000000000000;
        public const ulong ALL_PAWNS_DEFAULT = WHITE_PAWNS_DEFAULT | BLACK_PAWNS_DEFAULT;

        public const ulong WHITE_ROOKS_DEFAULT = 0x0000000000000081;
        public const ulong BLACK_ROOKS_DEFAULT = 0x8100000000000000;
        public const ulong ALL_ROOKS_DEFAULT = WHITE_ROOKS_DEFAULT | BLACK_ROOKS_DEFAULT;

        public const ulong WHITE_KNIGHTS_DEFAULT = 0x0000000000000042;
        public const ulong BLACK_KNIGHTS_DEFAULT = 0x4200000000000000;
        public const ulong ALL_KNIGHTS_DEFAULT = WHITE_KNIGHTS_DEFAULT | BLACK_KNIGHTS_DEFAULT;

        public const ulong WHITE_BISHOPS_DEFAULT = 0x0000000000000024;
        public const ulong BLACK_BISHOPS_DEFAULT = 0x2400000000000000;
        public const ulong ALL_BISHOPS_DEFAULT = WHITE_BISHOPS_DEFAULT | BLACK_BISHOPS_DEFAULT;

        public const ulong WHITE_QUEEN_DEFAULT = 0x0000000000000008;
        public const ulong BLACK_QUEEN_DEFAULT = 0x0800000000000000;
        public const ulong ALL_QUEEN_DEFAULT = WHITE_QUEEN_DEFAULT | BLACK_QUEEN_DEFAULT;

        public const ulong WHITE_KING_DEFAULT = 0x0000000000000010;
        public const ulong BLACK_KING_DEFAULT = 0x1000000000000000;
        public const ulong ALL_KING_DEFAULT = WHITE_KING_DEFAULT | BLACK_KING_DEFAULT;

        private ulong[] _boards = new ulong[13];

        public ulong WhitePawns
        {
            get => _boards[(int)Piece.WPawn];
            set => _boards[(int)Piece.WPawn] = value;
        }

        public ulong WhiteRooks
        {
            get => _boards[(int)Piece.WRook];
            set => _boards[(int)Piece.WRook] = value;
        }

        public ulong WhiteKnights
        {
            get => _boards[(int)Piece.WKnight];
            set => _boards[(int)Piece.WKnight] = value;
        }

        public ulong WhiteBishops
        {
            get => _boards[(int)Piece.WBishop];
            set => _boards[(int)Piece.WBishop] = value;
        }

        public ulong WhiteQueen
        {
            get => _boards[(int)Piece.WQueen];
            set => _boards[(int)Piece.WQueen] = value;
        }

        public ulong WhiteKing
        {
            get => _boards[(int)Piece.WKing];
            set => _boards[(int)Piece.WKing] = value;
        }

        public ulong BlackPawns
        {
            get => _boards[(int)Piece.BPawn];
            set => _boards[(int)Piece.BPawn] = value;
        }

        public ulong BlackRooks
        {
            get => _boards[(int)Piece.BRook];
            set => _boards[(int)Piece.BRook] = value;
        }

        public ulong BlackKnights
        {
            get => _boards[(int)Piece.BKnight];
            set => _boards[(int)Piece.BKnight] = value;
        }

        public ulong BlackBishops
        {
            get => _boards[(int)Piece.BBishop];
            set => _boards[(int)Piece.BBishop] = value;
        }

        public ulong BlackQueen
        {
            get => _boards[(int)Piece.BQueen];
            set => _boards[(int)Piece.BQueen] = value;
        }

        public ulong BlackKing
        {
            get => _boards[(int)Piece.BKing];
            set => _boards[(int)Piece.BKing] = value;
        }

        public CastlingRights CastlingRights { get; private set; } = 
            CastlingRights.WhiteKing 
            | CastlingRights.WhiteQueen 
            | CastlingRights.BlackKing 
            | CastlingRights.BlackQueen;


        public GameBoard(bool makeEmpty)
        {
            Reset(makeEmpty);
        }


        public void Reset(bool makeEmpty)
        {
            if (makeEmpty)
            {
                BlackPawns = 0;
                BlackRooks = 0;
                BlackKnights = 0;
                BlackBishops = 0;
                BlackQueen = 0;
                BlackKing = 0;

                WhitePawns = 0;
                WhiteRooks = 0;
                WhiteKnights = 0;
                WhiteBishops = 0;
                WhiteQueen = 0;
                WhiteKing = 0;
            }
            else
            {
                BlackPawns = BLACK_PAWNS_DEFAULT;
                BlackRooks = BLACK_ROOKS_DEFAULT;
                BlackKnights = BLACK_KNIGHTS_DEFAULT;
                BlackBishops = BLACK_BISHOPS_DEFAULT;
                BlackQueen = BLACK_QUEEN_DEFAULT;
                BlackKing = BLACK_KING_DEFAULT;

                WhitePawns = WHITE_PAWNS_DEFAULT;
                WhiteRooks = WHITE_ROOKS_DEFAULT;
                WhiteKnights = WHITE_KNIGHTS_DEFAULT;
                WhiteBishops = WHITE_BISHOPS_DEFAULT;
                WhiteQueen = WHITE_QUEEN_DEFAULT;
                WhiteKing = WHITE_KING_DEFAULT;
            }
        }

        public ulong Pieces()
        {
            return AllPieces();
        }

        public ulong this[Piece p]
        {
            get
            {
                switch (p)
                {
                    case Piece.WPawn:
                        return WhitePawns;
                    case Piece.WKnight:
                        return WhiteKnights;
                    case Piece.WBishop:
                        return WhiteBishops;
                    case Piece.WRook:
                        return WhiteRooks;
                    case Piece.WQueen:
                        return WhiteQueen;
                    case Piece.WKing:
                        return WhiteKing;
                    case Piece.BPawn:
                        return BlackPawns;
                    case Piece.BKnight:
                        return BlackKnights;
                    case Piece.BBishop:
                        return BlackBishops;
                    case Piece.BRook:
                        return BlackRooks;
                    case Piece.BQueen:
                        return BlackQueen;
                    case Piece.BKing:
                        return BlackKing;
                    default:
                        return 0;
                }
            }            
        }

        // maybe we need this at some point
        //public void Pieces(Piece p, ref ulong result)
        //{

        //}

        public ulong Pieces(Piece p1, Piece p2)
        {
            

            ulong result;
            switch (p1)
            {
                case Piece.WPawn:
                    result =  WhitePawns;
                    break;
                case Piece.WKnight:
                    result = WhiteKnights;
                    break;
                case Piece.WBishop:
                    result = WhiteBishops;
                    break;
                case Piece.WRook:
                    result = WhiteRooks;
                    break;
                case Piece.WQueen:
                    result = WhiteQueen;
                    break;
                case Piece.WKing:
                    result = WhiteKing;
                    break;
                case Piece.BPawn:
                    result = BlackPawns;
                    break;
                case Piece.BKnight:
                    result = BlackKnights;
                    break;
                case Piece.BBishop:
                    result = BlackBishops;
                    break;
                case Piece.BRook:
                    result = BlackRooks;
                    break;
                case Piece.BQueen:
                    result = BlackQueen;
                    break;
                case Piece.BKing:
                    result = BlackKing;
                    break;
                default:
                    result = 0;
                    break;
            }

            if (p1 == p2)
                return result;

            switch (p2)
            {
                case Piece.WPawn:
                    return result | WhitePawns;
                case Piece.WKnight:
                    return result | WhiteKnights;
                case Piece.WBishop:
                    return result | WhiteBishops;
                case Piece.WRook:
                    return result | WhiteRooks;
                case Piece.WQueen:
                    return result | WhiteQueen;
                case Piece.WKing:
                    return result | WhiteKing;
                case Piece.BPawn:
                    return result | BlackPawns;
                case Piece.BKnight:
                    return result | BlackKnights;
                case Piece.BBishop:
                    return result | BlackBishops;
                case Piece.BRook:
                    return result | BlackRooks;
                case Piece.BQueen:
                    return result | BlackQueen;
                case Piece.BKing:
                    return result | BlackKing;
                default:
                    return result;
            }
        }

        public ulong Pieces(Color c, PieceType type)
        {
            if (c == Color.White)
            {
                switch (type)
                {
                    case PieceType.Pawn:
                        return WhitePawns;                        
                    case PieceType.Knight:
                        return WhiteKnights;                        
                    case PieceType.Bishop:
                        return WhiteBishops;                        
                    case PieceType.Rook:
                        return WhiteRooks;                        
                    case PieceType.Queen:
                        return WhiteQueen;                        
                    case PieceType.King:
                        return WhiteKing;
                    default:
                        return 0;
                }
            }
            else
            {
                switch (type)
                {
                    case PieceType.Pawn:
                        return BlackPawns;
                    case PieceType.Knight:
                        return BlackKnights;
                    case PieceType.Bishop:
                        return BlackBishops;
                    case PieceType.Rook:
                        return BlackRooks;
                    case PieceType.Queen:
                        return BlackQueen;
                    case PieceType.King:
                        return BlackKing;
                    default:
                        return 0;
                }
            }            
        }

        public ulong Pieces(Color c1, PieceType type1, Color c2, PieceType type2)
        {
            ulong result;

            if (c1 == Color.White)
            {
                switch (type1)
                {
                    case PieceType.Pawn:
                        result = WhitePawns;
                        break;
                    case PieceType.Knight:
                        result = WhiteKnights;
                        break;
                    case PieceType.Bishop:
                        result = WhiteBishops;
                        break;
                    case PieceType.Rook:
                        result = WhiteRooks;
                        break;
                    case PieceType.Queen:
                        result = WhiteQueen;
                        break;
                    case PieceType.King:
                        result = WhiteKing;
                        break;
                    default:
                        result = 0;
                        break;
                }
            }
            else
            {
                switch (type1)
                {
                    case PieceType.Pawn:
                        result = BlackPawns;
                        break;
                    case PieceType.Knight:
                        result = BlackKnights;
                        break;
                    case PieceType.Bishop:
                        result = BlackBishops;
                        break;
                    case PieceType.Rook:
                        result = BlackRooks;
                        break;
                    case PieceType.Queen:
                        result = BlackQueen;
                        break;
                    case PieceType.King:
                        result = BlackKing;
                        break;
                    default:
                        result = 0;
                        break;
                }
            }

            if (c2 == Color.White)
            {
                switch (type2)
                {
                    case PieceType.Pawn:
                        return result | WhitePawns;                        
                    case PieceType.Knight:
                        return result | WhiteKnights;                        
                    case PieceType.Bishop:
                        return result | WhiteBishops;                        
                    case PieceType.Rook:
                        return result | WhiteRooks;
                       
                    case PieceType.Queen:
                        return result | WhiteQueen;
                    case PieceType.King:
                        return result | WhiteKing;
                    default:
                        return result | 0;
                }
            }
            else
            {
                switch (type2)
                {
                    case PieceType.Pawn:
                        return result | BlackPawns;
                    case PieceType.Knight:
                        return result | BlackKnights;
                    case PieceType.Bishop:
                        return result | BlackBishops;
                    case PieceType.Rook:
                        return result | BlackRooks;
                    case PieceType.Queen:
                        return result | BlackQueen;
                    case PieceType.King:
                        return result | BlackKing;
                    default:
                        return result | 0;
                }
            }
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


        // UNDONE: maybe we need an enumerator interface at somepoint
        //IEnumerator<ulong> IEnumerable<ulong>.GetEnumerator()
        //{
        //    return (IEnumerator<ulong>)GetEnumerator();
        //}

        //public IEnumerator<ulong> GetEnumerator()
        //{
        //    return new BoardEnum(_boards);
        //}

        private ulong AllPieces()
        {
            return AllBlackPieces() | AllWhitePieces();
        }

        private ulong AllBlackPieces()
        {
            return BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueen | BlackKing;
        }

        private ulong AllWhitePieces()
        {
            return WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueen | WhiteKing;
        }
    }
}
