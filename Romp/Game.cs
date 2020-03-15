using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class Game
    {
        public CastlingRights CurCastleRights { get; private set; } = CastlingRights.WhiteKing
                | CastlingRights.WhiteQueen
                | CastlingRights.BlackKing
                | CastlingRights.BlackQueen;

        public Game() 
        {
            
        }

        public Game(string fenStartingPosition)            
        {
            Board = FENTranslator.GameBoardFromFEN(fenStartingPosition);
            State = FENTranslator.GameStateFromFEN(fenStartingPosition);
        }

        public GameBoard Board { get; set; }
        public GameState State { get; set; }

        public string AsFEN()
        {
            return String.Empty;
        }

        public void Apply(Move move)
        {

        }
    }
}
