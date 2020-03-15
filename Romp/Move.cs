using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Square = System.Int32;

namespace Romp
{    
    //interface IMove
    //{
    //    Square Src { get; }
    //    Square Dest { get; }
    //    string SrcCoord { get; }
    //    string DestCoord { get; }
    //    string GetNotation(Game game, NotationFormat fmt = NotationFormat.Coord);
    //}

    public enum NotationFormat { StdShort, StdLong, Coord };

    //class Move : IMove
    //{
    //    public static readonly Move Null = new Move(Square.Null, Square.Null);

    //    public Move(Square src, Square dest)
    //    {
    //        Src = src;
    //        Dest = dest;
    //    }

    //    public Move(Move move)
    //    {
    //        Src = move.Src;
    //        Dest = move.Dest;
    //    }

    //    public Square Src { get; private set; } = Square.Null;
    //    public Square Dest { get; private set; } = Square.Null;
    //    public string SrcCoord => Constants.CoordinateStrs[(int)Src];
    //    public string DestCoord => Constants.CoordinateStrs[(int)Dest];

    //    public string GetNotation(Game game, NotationFormat fmt = NotationFormat.Coord)
    //    {
    //        switch (fmt)
    //        {                
    //            case NotationFormat.Coord:
    //                return SrcCoord + DestCoord;
    //            case NotationFormat.StdLong:
    //                throw new NotImplementedException("format not yet supported");
    //            case NotationFormat.StdShort:
    //                throw new NotImplementedException("format not yet supported");
    //            default:
    //                throw new ArgumentOutOfRangeException("invalid fmt", nameof(fmt));

    //        }
    //    }
    //}

    class Move 
    {
        // DUNNO: not sure if I should inherit or include Move in here or just duplicate the fields
        // private Move _move = null;

        public Move(Square src, Square dest, PieceType movingPieceType, Color movingPieceColor, MoveType moveType = MoveType.Quiet)
        {
            Src = src;
            Dest = dest;
            MvType = moveType;
            MovingPieceType = movingPieceType;
            MovingPieceColor = movingPieceColor;
        }

        //public ExtendedMove(Move move, MoveType moveType)
        //    : this(move.Src, move.Dest, moveType)
        //{
        //    // _move = move;
        //    MvType = moveType;
        //}

        public Move(Move move)            
        {            
            MovingPieceType = move.MovingPieceType;
            CapturedPieceType = move.CapturedPieceType;
            PromotionPieceType = move.PromotionPieceType;
            MovingPieceColor = move.MovingPieceColor;
            MvType = move.MvType;
            Src = move.Src;
            Dest = move.Dest;

        }

        public PieceType MovingPieceType { get; set; } = PieceType.Unknown;
        public PieceType CapturedPieceType { get; set; } = PieceType.Unknown;
        public PieceType PromotionPieceType { get; set; } = PieceType.Unknown;
        public MoveType MvType { get; set; } = MoveType.Null;        
        public Square Src { get; set; } = Square.Null;
        public Square Dest { get; set; } = Square.Null;

        public string SrcCoord => Constants.CoordinateStrs[(int)Src];
        public string DestCoord => Constants.CoordinateStrs[(int)Dest];
        public Color MovingPieceColor { get; set; } = Color.All;
        public Color CapturedPieceColor => (MovingPieceColor == Color.White) ? Color.Black :(( MovingPieceColor == Color.Black) ? Color.White : Color.All);

        public string GetNotation(Game game, NotationFormat fmt = NotationFormat.Coord)
        {
            switch (fmt)
            {
                case NotationFormat.Coord:
                    return SrcCoord + DestCoord;
                case NotationFormat.StdLong:
                    throw new NotImplementedException("format not yet supported");
                case NotationFormat.StdShort:
                    throw new NotImplementedException("format not yet supported");
                default:
                    throw new ArgumentOutOfRangeException("invalid fmt", nameof(fmt));

            }
        }
    }
}

/*
    // standard algebraic notation tranlator
    // to (de)serialize moves
    static class Notation
    {


        static Notation() { }

        public static Move MoveFromNotation(string note, NotationFormat fmt = NotationFormat.Coord)
        {
            if (String.IsNullOrEmpty(note))
            { 
                throw new ArgumentException("null or empty value", nameof(note));
            }

            switch (fmt)
            {
                case NotationFormat.StdShort:
                    return Move.Null;
                case NotationFormat.StdLong:
                    return Move.Null;
                case NotationFormat.Coord:
                {                    
                    return new Move(CoordToSquare(note[0..1]), CoordToSquare(note[2..3]));
                }                    
                default: // return null move if some invalid value is passed
                    throw new ArgumentOutOfRangeException(nameof(fmt));
            }
        }

        public static ExtendedMove ExtMoveFromNotation(string note, NotationFormat fmt = NotationFormat.Coord)
        {
            var result = MoveFromNotation(note, fmt);
            if (result != Move.Null)
            {
                switch (fmt)
                {
                    case NotationFormat.StdShort:
                        return Move.Null;
                    case NotationFormat.StdLong:
                        return Move.Null;
                    case NotationFormat.Coord:
                    {
                        return new Move(CoordToSquare(note[0..1]), CoordToSquare(note[2..3]));
                    }
                    default: // return null move if some invalid value is passed
                        throw new ArgumentOutOfRangeException(nameof(fmt));
                }
            }
        }

        public static string MoveToNotation(IMove move, NotationFormat fmt = NotationFormat.Coord)
        {
            if (move is null)            
                throw new ArgumentNullException(nameof(move));

            switch (fmt)
            {
                case NotationFormat.StdShort:
                    break;
                case NotationFormat.StdLong:
                    break;
                case NotationFormat.Coord:
                    return SquareIndexToCoord(move.Src) + SquareIndexToCoord(move.Dest);
                default:
                    break;
            }

            return String.Empty;
        }

        private static Square CoordToSquare(string n)
        {
            if (n.Length > 2)
                throw new ArgumentException("invalid square notation value", nameof(n));

            int file = _files.IndexOf(n[0]);
            int rank = _ranks.IndexOf(n[1]);

            if (file == -1)
                throw new ArgumentException("invalid file value", nameof(n));

            if (rank == -1)
                throw new ArgumentException("invalid rank value", nameof(n));

            return (Square)(rank * 8 + file);
        }

        private static string SquareIndexToCoord(int idx)
        {
            if (idx < 0 || idx > 63)
                throw new ArgumentOutOfRangeException(nameof(idx));

            return Constants.CoordinateStrs[idx];
        }

        private static string SquareIndexToCoord(Square idx)
        {            
            return SquareIndexToCoord((int)idx);
        }

    }

     */
