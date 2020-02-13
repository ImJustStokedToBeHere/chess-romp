using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class MoveGenerator
    {
        static MoveGenerator() { }
        public MoveGenerator() { }

        public List<Move> Generate(GameBoard board, GameState state)
        {
            var result = new List<Move>();

            foreach (PieceType p in Enum.GetValues(typeof(PieceType)))
            {
                var pieces = board.Pieces(state.SideToMove, p);
                if (state.SideToMove == Color.Black)
                {
                    // we need to rotate the board 180 if so
                    






                }

            }


            return null;
        }

    }
}
