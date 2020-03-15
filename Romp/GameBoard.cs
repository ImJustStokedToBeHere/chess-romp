using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class GameState
    {
        public Color SideToMove = Color.White;
        public ulong MoveCount = 0;
        public ulong BlackEnpassant { get; private set; } = 0X0UL;
        public ulong WhiteEnpassant { get; private set; } = 0X0UL;
    }

    // UNDONE: maybe we'll need  an enumerator interface at some point
    // : IEnumerable<ulong>
    class GameBoard
    {
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

        private ulong[,] _boards = new ulong[2, 6];

        public GameBoard(bool makeEmpty)
        {
            Reset(makeEmpty);
        }

        public ulong WhitePawns
        {
            get => _boards[Color.White.Index(), PieceType.Pawn.Index()];
            private set => _boards[Color.White.Index(), PieceType.Pawn.Index()] = value;
        }

        public ulong WhiteRooks
        {
            get => _boards[Color.White.Index(), PieceType.Rook.Index()];
            private set => _boards[Color.White.Index(), PieceType.Rook.Index()] = value;
        }

        public ulong WhiteKnights
        {
            get => _boards[Color.White.Index(), PieceType.Knight.Index()];
            private set => _boards[Color.White.Index(), PieceType.Knight.Index()] = value;
        }

        public ulong WhiteBishops
        {
            get => _boards[Color.White.Index(), PieceType.Bishop.Index()];
            private set => _boards[Color.White.Index(), PieceType.Bishop.Index()] = value;
        }

        public ulong WhiteQueen
        {
            get => _boards[Color.White.Index(), PieceType.Queen.Index()];
            private set => _boards[Color.White.Index(), PieceType.Queen.Index()] = value;
        }

        public ulong WhiteKing
        {
            get => _boards[Color.White.Index(), PieceType.King.Index()];
            private set => _boards[Color.White.Index(), PieceType.King.Index()] = value;
        }

        public ulong BlackPawns
        {
            get => _boards[Color.Black.Index(), PieceType.Pawn.Index()];
            private set => _boards[Color.Black.Index(), PieceType.Pawn.Index()] = value;
        }

        public ulong BlackRooks
        {
            get => _boards[Color.Black.Index(), PieceType.Rook.Index()];
            private set => _boards[Color.Black.Index(), PieceType.Rook.Index()] = value;
        }

        public ulong BlackKnights
        {
            get => _boards[Color.Black.Index(), PieceType.Knight.Index()];
            private set => _boards[Color.Black.Index(), PieceType.Knight.Index()] = value;
        }

        public ulong BlackBishops
        {
            get => _boards[Color.Black.Index(), PieceType.Bishop.Index()];
            private set => _boards[Color.Black.Index(), PieceType.Bishop.Index()] = value;
        }

        public ulong BlackQueen
        {
            get => _boards[Color.Black.Index(), PieceType.Queen.Index()];
            private set => _boards[Color.Black.Index(), PieceType.Queen.Index()] = value;
        }

        public ulong BlackKing
        {
            get => _boards[Color.Black.Index(), PieceType.King.Index()];
            private set => _boards[Color.Black.Index(), PieceType.King.Index()] = value;
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

        public ulong this[int color, int piece]
        {
            get
            {
                if (color < 0 || piece < 0 || color > 2 || piece > 5)
                    throw new ArgumentOutOfRangeException();

                return _boards[color, piece];
            }
        }

        public ulong GetPieces(PieceType type1, PieceType type2, Color color1, Color color2)
        {
            ulong result = type1 switch
            {
                PieceType.Pawn => color1 switch
                {
                    Color.White => WhitePawns,
                    Color.Black => BlackPawns,
                    Color.All => WhitePawns | BlackPawns,
                    _ => throw new ArgumentException(nameof(color1))
                },
                PieceType.Rook => color1 switch
                {
                    Color.White => WhiteRooks,
                    Color.Black => BlackRooks,
                    Color.All => WhiteRooks | BlackRooks,
                    _ => throw new ArgumentException(nameof(color1))
                },
                PieceType.Knight => color1 switch
                {
                    Color.White => WhiteKnights,
                    Color.Black => BlackKnights,
                    Color.All => WhiteKnights | BlackKnights,
                    _ => throw new ArgumentException(nameof(color1))
                },
                PieceType.Bishop => color1 switch
                {
                    Color.White => WhiteBishops,
                    Color.Black => BlackBishops,
                    Color.All => WhiteBishops | BlackBishops,
                    _ => throw new ArgumentException(nameof(color1))
                },
                PieceType.Queen => color1 switch
                {
                    Color.White => WhiteQueen,
                    Color.Black => BlackQueen,
                    Color.All => WhiteQueen | BlackQueen,
                    _ => throw new ArgumentException(nameof(color1))
                },
                PieceType.King => color1 switch
                {
                    Color.White => WhiteKing,
                    Color.Black => BlackKing,
                    Color.All => WhiteKing | BlackKing,
                    _ => throw new ArgumentException(nameof(color1))
                },
                _ => throw new ArgumentException(nameof(type1))
            };

            return  result | type2 switch
            {
                PieceType.Pawn => color2 switch
                {
                    Color.White => WhitePawns,
                    Color.Black => BlackPawns,
                    Color.All => WhitePawns | BlackPawns,
                    _ => throw new ArgumentException(nameof(color2))
                },
                PieceType.Rook => color2 switch
                {
                    Color.White => WhiteRooks,
                    Color.Black => BlackRooks,
                    Color.All => WhiteRooks | BlackRooks,
                    _ => throw new ArgumentException(nameof(color2))
                },
                PieceType.Knight => color2 switch
                {
                    Color.White => WhiteKnights,
                    Color.Black => BlackKnights,
                    Color.All => WhiteKnights | BlackKnights,
                    _ => throw new ArgumentException(nameof(color2))
                },
                PieceType.Bishop => color2 switch
                {
                    Color.White => WhiteBishops,
                    Color.Black => BlackBishops,
                    Color.All => WhiteBishops | BlackBishops,
                    _ => throw new ArgumentException(nameof(color2))
                },
                PieceType.Queen => color2 switch
                {
                    Color.White => WhiteQueen,
                    Color.Black => BlackQueen,
                    Color.All => WhiteQueen | BlackQueen,
                    _ => throw new ArgumentException(nameof(color2))
                },
                PieceType.King => color2 switch
                {
                    Color.White => WhiteKing,
                    Color.Black => BlackKing,
                    Color.All => WhiteKing | BlackKing,
                    _ => throw new ArgumentException(nameof(color2))
                },
                _ => throw new ArgumentException(nameof(type2))
            };
        }

        public ulong GetPieces(Color color, PieceType type)
        {
            return type switch
            {
                PieceType.Pawn => color switch
                {
                    Color.White => WhitePawns,
                    Color.Black => BlackPawns,
                    Color.All => WhitePawns | BlackPawns,
                    _ => throw new ArgumentException(nameof(color))
                },
                PieceType.Rook => color switch
                {
                    Color.White => WhiteRooks,
                    Color.Black => BlackRooks,
                    Color.All => WhiteRooks | BlackRooks,
                    _ => throw new ArgumentException(nameof(color))
                },
                PieceType.Knight => color switch
                {
                    Color.White => WhiteKnights,
                    Color.Black => BlackKnights,
                    Color.All => WhiteKnights | BlackKnights,
                    _ => throw new ArgumentException(nameof(color))
                },
                PieceType.Bishop => color switch
                {
                    Color.White => WhiteBishops,
                    Color.Black => BlackBishops,
                    Color.All => WhiteBishops | BlackBishops,
                    _ => throw new ArgumentException(nameof(color))
                },
                PieceType.Queen => color switch
                {
                    Color.White => WhiteQueen,
                    Color.Black => BlackQueen,
                    Color.All => WhiteQueen | BlackQueen,
                    _ => throw new ArgumentException(nameof(color))
                },
                PieceType.King => color switch
                {
                    Color.White => WhiteKing,
                    Color.Black => BlackKing,
                    Color.All => WhiteKing | BlackKing,
                    _ => throw new ArgumentException(nameof(color))
                },
                _ => throw new ArgumentException(nameof(type))
            };
        }
        

        public PieceType PieceOnSquare(Square s)
        {
            return PieceType.Unknown;
        }

        public bool SquareIsEmpty(Square s)
        {
            return true;
        }

        public int Count(Color c, PieceType type)
        {
#if !DEBUG
            return BoardUtils.CountSetBits(GetPieces(c, type));
#else

            var pieces = GetPieces(c, type);
            return BoardUtils.CountSetBits(pieces);
#endif
        }

        public int Count(PieceType type)
        {
            return Count(Color.All, type);
        }

        public List<Square> Squares(Color c)
        {
            return Squares(c, PieceType.King | PieceType.Queen | PieceType.Bishop | PieceType.Knight | PieceType.Rook | PieceType.Pawn);
        }

        public List<Square> Squares(Color c, PieceType type)
        {
            //var result = new List<Square>()_;
            //if ((type & PieceType.King) == PieceType.King)
            //{

            //}



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

        public ulong AllPieces()
        {
            return AllBlackPieces() | AllWhitePieces();
        }

        public ulong AllBlackPieces()
        {
            return BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueen | BlackKing;
        }

        public ulong AllWhitePieces()
        {
            return WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueen | WhiteKing;
        }

        public ulong AllUnderlings(Color color)
        {
            if (color == Color.White)
                return WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueen;
            else if (color == Color.Black)
                return BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueen;
            else
                throw new ArgumentException(nameof(color));
        }

        public GameRecord GetGameRecord()
        {
            return null;
        }


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
    }
}
