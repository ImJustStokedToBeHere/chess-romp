using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Romp
{
    class Move
    {
        

        public Move() { }

        public Move(Square src, Square dest, PieceType type, SpecialtyMove spMove)
        {
            Src = src;
            Dest = dest;
            Type = type;
            Specialty = spMove;
        }

        public Move(Move move)
        {
            Src = move.Src;
            Dest = move.Dest;
            Type = move.Type;
            Specialty = move.Specialty;
        }

        

        public static Move FromNotation(string note)
        {
            return SANTranslator.FromStdNotation(note);
        }

        public Square Src { get; set; } = (Square)Constants.NULL_SQUARE;
        public Square Dest { get; set; } = (Square)Constants.NULL_SQUARE;
        public PieceType Type { get; set; } = PieceType.Unknown;
        public SpecialtyMove Specialty { get; set; } = SpecialtyMove.Unknown;

        public string AsNotationString() => SANTranslator.ToStdNotation(this);

    }
}
