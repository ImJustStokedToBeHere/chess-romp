using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class Game
    {
        public CastlingRights CastlingRights { get; private set; } = CastlingRights.WhiteKing
                | CastlingRights.WhiteQueen
                | CastlingRights.BlackKing
                | CastlingRights.BlackQueen;

        private GameBoard _board;
        private GameState _state;
        private MoveGenerator _gen;


        public Game()
        {
            _board = new GameBoard(false);
            _state = new GameState();
            _gen = new MoveGenerator();
        }

        public Game(string FenStartingPosition)
        {

            _gen = new MoveGenerator();

        }

        public string AsFEN()
        {
            return String.Empty;
        }

    }
}
