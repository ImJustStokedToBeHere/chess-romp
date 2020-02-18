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
                var pieces = board.GetPieces(state.SideToMove, p);
                if (state.SideToMove == Color.Black)                
                    pieces = Bits.Rotate180(pieces);

                switch (p)
                {
                    case PieceType.Pawn:
                        break;
                    case PieceType.Rook:
                        break;
                    case PieceType.Knight:
                        break;
                    case PieceType.Bishop:
                        break;
                    case PieceType.Queen:
                        break;
                    case PieceType.King:
                        break;
                    default:
                        break;
                }






            }


            return null;
        }

    }
}