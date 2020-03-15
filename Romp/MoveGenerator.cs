using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
/*
NOTES:
- lsh 8 this moves all pieces forward by one space
- the square index - 8 is the same square on the previous row

PAWN:
- can go fwd 1
- can go fwd 2
- can get promoted
- can capture right diag
- can capture left anti-diag
- can atk via passing

ROOK:
- can go vert/horiz, full board, any direction
- can castle if if has not been moved
*/

    // pseudo-legal move generator
    class MoveGenerator
    {
        private GameBoard _board;
        private GameState _state;

        //private List<Move> _moves = new List<Move>();

        public MoveGenerator(Game game)
            : this(game.Board, game.State){ }

        public MoveGenerator(GameBoard board, GameState state)
        {
            _board = board;
            _state = state;
        }
        
        // TODOTODO: goin to need to filter the total moves down in the getter
        // we should maybe make a query class which can handle ordering and search parameters and then grab them on request
        // public List<Move> Moves => _moves;
        //public List<Move> LegalMoves => _moves;


        //public void Reset()
        //{
        //    _moves.Clear();
        //}

        //public Move this[int idx]
        //{
        //    get => _moves[idx];
        //}

        public List<Move> GenerateMoves()
        {
            var result = new List<Move>();
            GenWhitePawnMoves(result);
            GenWhiteKnightMoves(result);

            return result;
        }

        //public List<Move> GenerateLegalMoves()
        //{
        //    var allMoves = GenerateMoves();
        //    return GetLegalMoves(allMoves);
        //}

        public List<Move> GetLegalMoves(List<Move> allMoves)
        {
            return new List<Move>(allMoves.ToArray());
        }

        private void GenWhiteKnightMoves(List<Move> moves)
        {

        }

        private void GenWhitePawnMoves(List<Move> moves)
        {
            // generate fwd 1 space moves
            var pawns = _board.WhitePawns;
            // single moved pawns
            var movedSingle = pawns << 8;
            // get unoccupied spaces
            var unoccupiedSpaces = ~_board.AllPieces();
            // take collisions into account
            movedSingle &= unoccupiedSpaces;

            // take promotions into account if they exist            
            if ((Constants.MASK_RANK_8 & movedSingle) > 0)
            {
                var promos = Constants.MASK_RANK_8 & movedSingle;
                // remove the promotions from the moved pawns
                movedSingle &= Constants.CLEAR_RANK_8;
                // get all of the promoted pieces and convert them to moves
                while (promos > 0)
                {
                    var dest = BoardUtils.PopLSB(ref promos);                    
                    moves.Add(new Move((dest - 8).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion));
                }
            }

            // make signle moves quiets into moves
            while (movedSingle > 0)
            {
                var dest = BoardUtils.PopLSB(ref movedSingle);
                moves.Add(new Move((dest - 8).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Quiet));
            }

            // double moved pawns
            var movedDouble = pawns << 16;
            // get unoccupied spaces
            unoccupiedSpaces = ~_board.AllPieces();
            // take collisions into account and make sure that we're only operating in the fourth rank
            movedDouble &= (unoccupiedSpaces & Constants.MASK_RANK_4);

            while (movedDouble > 0)
            {
                var dest = BoardUtils.PopLSB(ref movedDouble);
                moves.Add(new Move((dest - 16).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.DoublePawnPush));
            }
            // do right side attacks
            // get destination for right captures
            var rightCapDest = (pawns << 9) & ~Constants.MASK_FILE_A;
            // get right captures minus the king because we can't acutally capture it
            var rightPlainCaptures = rightCapDest & (_board.AllBlackPieces() & ~_board.BlackKing);
            // isolate the captures that are also promotions
            var rightPromoCaptures = rightPlainCaptures & Constants.MASK_RANK_8;
            // remove the promotions from the regular captures
            rightPlainCaptures &= ~Constants.MASK_RANK_8;

            while (rightPlainCaptures > 0)
            {
                var dest = BoardUtils.PopLSB(ref rightPlainCaptures);
                moves.Add(new Move((dest - 9).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Capture)
                {                    
                    CapturedPieceType = _board.PieceOnSquare(dest.Square())
                });
            }

            while (rightPromoCaptures > 0)
            {
                var dest = BoardUtils.PopLSB(ref rightPromoCaptures);
                // need to make 4 separate promotion types for each 
                moves.Add(new Move((dest - 9).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion | MoveType.Capture)
                {                    
                    CapturedPieceType = _board.PieceOnSquare(dest.Square())
                });
            }

            // check if there is a black enpassant square
            if (_state.BlackEnpassant != 0)
            {
                var rightEnpassantCapture = pawns & _state.BlackEnpassant & ~Constants.MASK_FILE_A;
                if (rightEnpassantCapture > 0)
                {
                    var dest = BoardUtils.PopLSB(ref rightEnpassantCapture);
                    moves.Add(new Move((dest - 9).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.EnpassantCapture)
                    {                        
                        CapturedPieceType = PieceType.Pawn
                    });                    
                }
            }

            // do left side attacks
            // get destination for left captures
            var leftCapDest = (pawns << 7) & ~Constants.MASK_FILE_H;
            // get right captures minus the king because we can't acutally capture it
            var leftPlainCaptures = leftCapDest & (_board.AllBlackPieces() & ~_board.BlackKing);
            // isolate the captures that are also promotions
            var leftPromoCaptures = leftPlainCaptures & Constants.MASK_RANK_8;
            // remove the promotions from the regular captures
            leftPlainCaptures &= ~Constants.MASK_RANK_8;
            // make plain captures into moves
            while (leftPlainCaptures > 0)
            {
                var dest = BoardUtils.PopLSB(ref leftPlainCaptures);
                moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Capture)
                {                    
                    CapturedPieceType = _board.PieceOnSquare(dest.Square())
                });
            }
            // make promotion captures into moves
            while (leftPromoCaptures > 0)
            {
                var dest = BoardUtils.PopLSB(ref rightPromoCaptures);
                // need to make 4 separate promotion types for each Queen, Bishop, Knight, and Rook
                moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion | MoveType.Capture)
                {                    
                    CapturedPieceType = _board.PieceOnSquare(dest.Square()),
                    PromotionPieceType = PieceType.Queen
                });

                moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion | MoveType.Capture)
                {
                    CapturedPieceType = _board.PieceOnSquare(dest.Square()),
                    PromotionPieceType = PieceType.Bishop
                });

                moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion | MoveType.Capture)
                {
                    CapturedPieceType = _board.PieceOnSquare(dest.Square()),
                    PromotionPieceType = PieceType.Knight
                });

                moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.Promotion | MoveType.Capture)
                {
                    CapturedPieceType = _board.PieceOnSquare(dest.Square()),
                    PromotionPieceType = PieceType.Rook
                });
            }
            // check if there is a black enpassant square
            if (_state.BlackEnpassant != 0)
            {
                var leftEnpassantCapture = pawns & _state.BlackEnpassant & ~Constants.MASK_FILE_H;
                if (leftEnpassantCapture > 0)
                {
                    var dest = BoardUtils.PopLSB(ref leftEnpassantCapture);
                    moves.Add(new Move((dest - 7).Square(), dest.Square(), PieceType.Pawn, Color.White, MoveType.EnpassantCapture)
                    {                       
                        CapturedPieceType = PieceType.Pawn
                    });
                }
            }
        }        
    }
}