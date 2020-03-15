using System;

namespace Romp
{
    // Forsyth-Edwards notation
    internal static class FENTranslator
    {
        public static GameBoard GameBoardFromFEN(string FEN)
        {
            return null;
        }

        public static GameState GameStateFromFEN(string FEN)
        {
            return null;
        }

        public static string ToFEN(Game game)
        {
            //var boardPos = 56;
            //int idx = 0;
            //int rank = 8;

            //while (idx < 64)
            //{
            //    ulong sq = (1UL << boardPos);
            //    bool occupied = (sq & board.AllPieces()) != 0;

            //    if (occupied)
            //    {
            //        if ((sq & board[Pieces.WHITE, Pieces.PAWN]) == board[Pieces.WHITE, Pieces.PAWN])
            //            return "";
            //        else if ((sq & board[Pieces.WHITE, Pieces.ROOK]) == board[Pieces.WHITE, Pieces.ROOK])
            //            return "";
            //        else if ((sq & board[Pieces.WHITE, Pieces.KNIGHT]) == board[Pieces.WHITE, Pieces.KNIGHT])
            //            return "";
            //        else if ((sq & board[Pieces.WHITE, Pieces.BISHOP]) == board[Pieces.WHITE, Pieces.BISHOP])
            //            return "";
            //        else if ((sq & board[Pieces.WHITE, Pieces.QUEEN]) == board[Pieces.WHITE, Pieces.QUEEN])
            //            return "";
            //        else if ((sq & board[Pieces.WHITE, Pieces.KING]) == board[Pieces.WHITE, Pieces.KING])
            //            return "";
            //    }
            //}     
            return String.Empty;
        }

        public static string ToFEN(GameBoard board, GameState state)
        {
            return String.Empty;
        }

    }
}
