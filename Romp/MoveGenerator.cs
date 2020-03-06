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

        private List<Move> _moves = new List<Move>();

        public MoveGenerator(Game game)
            : this(game.Board, game.State){ }

        public MoveGenerator(GameBoard board, GameState state)
        {
            _board = board;
            _state = state;
        }

        public List<Move> Moves => _moves;

        // TODOTODO: goin to need to filter the total moves down in the getter
        public List<Move> LegalMoves => _moves;

        public void Reset()
        {
            _moves.Clear();
        }


        //public List<Move> Generate(Game game)
        //{
        //    return Generate(game.Board, game.State);
        //}

        //public List<Move> Generate(GameBoard board, GameState state)
        //{
        //    var result = new List<Move>();

        //    switch (state.SideToMove)
        //    {
        //        case Color.White:
        //        {
        //            GenWhitePawnMoves(board, result);
        //            // GenWhiteRookMoves(board, result);

        //            break;
        //        }
        //        case Color.Black:
        //        {
        //            break;
        //        }   
        //    }

        //    return result;
        //}
        
        private void GenWhitePawnMoves(GameBoard board, List<Move> moves)
        {
            // generate fwd 1 space moves
            var pawns = board.WhitePawns;
            // single moved pawns
            var movedSingle = pawns << 8;
            // get unoccupied spaces
            var unoccupiedSpaces = ~board.AllPieces();
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
                    moves.Add(new Move((dest - 8).Square(), dest.Square(), MoveType.Promotion)
                    { 
                        MovingPieceType = PieceType.Pawn
                    });
                }
            }

            while (movedSingle > 0)
            {
                var dest = BoardUtils.PopLSB(ref movedSingle);
                moves.Add(new Move((dest - 8).Square(), dest.Square(), MoveType.Quiet) 
                {
                     MovingPieceType = PieceType.Pawn
                });
            }

            // double moved pawns
            var movedDouble = pawns << 16;
            // get unoccupied spaces
            unoccupiedSpaces = ~board.AllPieces();
            // take collisions into account and make sure that we're only operating in the fourth rank
            movedDouble &= (unoccupiedSpaces & Constants.MASK_RANK_4);

            while (movedDouble > 0)
            {
                var dest = BoardUtils.PopLSB(ref movedDouble);
                moves.Add(new Move((dest - 16).Square(), dest.Square(), MoveType.DoublePawnPush)
                {
                    MovingPieceType = PieceType.Pawn,
                });
            }

            // do right captures

            // do left captures

        }

        private void GenWhiteKingMoves(GameBoard board, List<Move> moves)
        {

        }
        
    }
}