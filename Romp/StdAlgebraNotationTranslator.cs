using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Romp
{
    // standard algebraic notation tranlator
    // to (de)serialize moves
    static class SANTranslator
    {
        private static Dictionary<PieceType, char> _invNotationLookup = null;
        private static Dictionary<char, PieceType> _notationLookup = null;

        static SANTranslator()
        {
            SetDefaultNotationLookup();
        }

        public static void SetDefaultNotationLookup()
        {
            var lookup = new Dictionary<char, PieceType>
            {
                { 'K', PieceType.King },
                { 'Q', PieceType.Queen },
                { 'B', PieceType.Bishop },
                { 'N', PieceType.Knight },
                { 'R', PieceType.Rook }
            };

            SetNotationLookup(lookup);
        }

        public static void SetNotationLookup(Dictionary<char, PieceType> lookup)
        {
            _notationLookup = lookup;
            _invNotationLookup = _notationLookup.ToDictionary((i) => i.Value, (i) => i.Key);
        }

        public static Move FromStdNotation(string note)
        {
            if (String.IsNullOrEmpty(note))
            {
                throw new ArgumentException("message", nameof(note));
            }

            switch (note.Length)
            {
                case 2: // this is just a pawn move forward
                {
                    break;
                }
                case 3:
                {
                    break;
                }
                default:
                    break;
            }

            return null;
        }

        public static string ToStdNotation(Move move)
        {
            if (move is null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            return String.Empty;
        }

    }
}
