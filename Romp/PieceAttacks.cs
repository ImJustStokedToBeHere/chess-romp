
using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    /// <summary>
    /// 
    /// </summary>
    static class PieceAttacks
    {
        /// <summary>
        /// mask of all outer ranks and files, this field is constant
        /// </summary>
        private const ulong OUTER_SQUARES = Constants.MASK_FILE_A | Constants.MASK_FILE_H | Constants.MASK_RANK_1 | Constants.MASK_RANK_8;
        private static readonly ulong[] _rookAttackMasks = new ulong[64];
        private static readonly ulong[] _bishopAttackMasks = new ulong[64];
        private static readonly ulong[,] _rookMagicAttacks = new ulong[64, 4096]; // 16MB
        private static readonly ulong[,] _bishopMagicAttacks = new ulong[64, 1024]; //4MB
        private static readonly ulong[] _knightAttacks = new ulong[64];

        // magics and and index bits taken from ShallowBlue
        /// <summary>
        /// 
        /// </summary>
        private static readonly ulong[] _rookMagicHashValues = new ulong[]
        {
            0xa8002c000108020UL,  0x6c00049b0002001UL,  0x100200010090040UL,  0x2480041000800801UL, 0x280028004000800UL,
            0x900410008040022UL,  0x280020001001080UL,  0x2880002041000080UL, 0xa000800080400034UL, 0x4808020004000UL,
            0x2290802004801000UL, 0x411000d00100020UL,  0x402800800040080UL,  0xb000401004208UL,    0x2409000100040200UL,
            0x1002100004082UL,    0x22878001e24000UL,   0x1090810021004010UL, 0x801030040200012UL,  0x500808008001000UL,
            0xa08018014000880UL,  0x8000808004000200UL, 0x201008080010200UL,  0x801020000441091UL,  0x800080204005UL,
            0x1040200040100048UL, 0x120200402082UL,     0xd14880480100080UL,  0x12040280080080UL,   0x100040080020080UL,
            0x9020010080800200UL, 0x813241200148449UL,  0x491604001800080UL,  0x100401000402001UL,  0x4820010021001040UL,
            0x400402202000812UL,  0x209009005000802UL,  0x810800601800400UL,  0x4301083214000150UL, 0x204026458e001401UL,
            0x40204000808000UL,   0x8001008040010020UL, 0x8410820820420010UL, 0x1003001000090020UL, 0x804040008008080UL,
            0x12000810020004UL,   0x1000100200040208UL, 0x430000a044020001UL, 0x280009023410300UL,  0xe0100040002240UL,
            0x200100401700UL,     0x2244100408008080UL, 0x8000400801980UL,    0x2000810040200UL,    0x8010100228810400UL,
            0x2000009044210200UL, 0x4080008040102101UL, 0x40002080411d01UL,   0x2005524060000901UL, 0x502001008400422UL,
            0x489a000810200402UL, 0x1004400080a13UL,    0x4000011008020084UL, 0x26002114058042UL
        };

        /// <summary>
        /// 
        /// </summary>
        private static readonly ulong[] _bishopMagicHashValues = new ulong[]
        {
            0x89a1121896040240UL, 0x2004844802002010UL, 0x2068080051921000UL, 0x62880a0220200808UL, 0x4042004000000UL,
            0x100822020200011UL, 0xc00444222012000aUL, 0x28808801216001UL, 0x400492088408100UL, 0x201c401040c0084UL,
            0x840800910a0010UL, 0x82080240060UL, 0x2000840504006000UL, 0x30010c4108405004UL, 0x1008005410080802UL,
            0x8144042209100900UL, 0x208081020014400UL, 0x4800201208ca00UL, 0xf18140408012008UL, 0x1004002802102001UL,
            0x841000820080811UL, 0x40200200a42008UL, 0x800054042000UL, 0x88010400410c9000UL, 0x520040470104290UL,
            0x1004040051500081UL, 0x2002081833080021UL, 0x400c00c010142UL, 0x941408200c002000UL, 0x658810000806011UL,
            0x188071040440a00UL, 0x4800404002011c00UL, 0x104442040404200UL, 0x511080202091021UL, 0x4022401120400UL,
            0x80c0040400080120UL, 0x8040010040820802UL, 0x480810700020090UL, 0x102008e00040242UL, 0x809005202050100UL,
            0x8002024220104080UL, 0x431008804142000UL, 0x19001802081400UL, 0x200014208040080UL, 0x3308082008200100UL,
            0x41010500040c020UL, 0x4012020c04210308UL, 0x208220a202004080UL, 0x111040120082000UL, 0x6803040141280a00UL,
            0x2101004202410000UL, 0x8200000041108022UL, 0x21082088000UL, 0x2410204010040UL, 0x40100400809000UL,
            0x822088220820214UL, 0x40808090012004UL, 0x910224040218c9UL, 0x402814422015008UL, 0x90014004842410UL,
            0x1000042304105UL, 0x10008830412a00UL, 0x2520081090008908UL, 0x40102000a0a60140UL,
        };

        /// <summary>
        /// the number of bits needed to index all of the blocker combinations at a particular square
        /// </summary>
        private static readonly int[] _rookBlockerComboIndexBitCount = 
        {
            12, 11, 11, 11, 11, 11, 11, 12,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            11, 10, 10, 10, 10, 10, 10, 11,
            12, 11, 11, 11, 11, 11, 11, 12
        };

        /// <summary>
        /// the number of bits needed to index all of the blocker combinations at a particular square
        /// </summary>
        private static readonly int[] _bishopBlockerComboIndexBitCount = 
        {
            6, 5, 5, 5, 5, 5, 5, 6,
            5, 5, 5, 5, 5, 5, 5, 5,
            5, 5, 7, 7, 7, 7, 5, 5,
            5, 5, 7, 9, 9, 7, 5, 5,
            5, 5, 7, 9, 9, 7, 5, 5,
            5, 5, 7, 7, 7, 7, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5,
            6, 5, 5, 5, 5, 5, 5, 6
        };

        /// <summary>
        /// 
        /// </summary>
        static PieceAttacks()
        {
            // generate the seperate sliding piece masks for all squares
            // - no need to gen queen mask because it is the OR of both the rook and bishopkkkkd            

            for (int i = 0; i < Constants.SQUARE_COUNT; i++)
            {
                // initialize the rook masks
                _rookAttackMasks[i] = ~OUTER_SQUARES
                    & (ConsecutiveSquares.GetBoard(Direction.North, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.South, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.East, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.West, i.Square()));

                // initialize the bishop masks
                _bishopAttackMasks[i] = ~OUTER_SQUARES
                    & (ConsecutiveSquares.GetBoard(Direction.NorthEast, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.SouthEast, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.NorthWest, i.Square())
                        | ConsecutiveSquares.GetBoard(Direction.SouthWest, i.Square()));

                ulong pos = 0x1UL << i;

                //_knightAttacks[i] = (((start << 15) | (start << -17)) |
                //    ((start << -15) | (start << 17)) |
                //    ((start << 6) | (start << -10)) |
                //    ((start << -6) | (start << 10)));


                //_knightAttacks[i] = ((start << 15) | (start >> 17))
                //      | ((start >> 15) | (start << 17))
                //      | ((start << 6) | (start >> 10))
                //      | ((start >> 6) | (start << 10));

                _knightAttacks[i] = (((pos << 15) | (pos >> 17)) & Constants.CLEAR_FILE_H) |
                    (((pos << 6) | (pos >> 10)) & (Constants.CLEAR_FILE_G & Constants.CLEAR_FILE_H)) |
                    (((pos >> 15) | (pos << 17)) & Constants.CLEAR_FILE_A) |                    
                    (((pos >> 6) | (pos << 10)) & (Constants.CLEAR_FILE_A & Constants.CLEAR_FILE_B));
            }

            // generate attack board per square and blocker combination
            for (int sq = 0; sq < Constants.SQUARE_COUNT; sq++)
            {
                for (int blockerComboIdx = 0; blockerComboIdx < (1 << _bishopBlockerComboIndexBitCount[sq]); blockerComboIdx++)
                {
                    ulong blockers = GenerateBlockersFromMaskAndIdx(blockerComboIdx, _bishopAttackMasks[sq]);
                    var key = (blockers * _bishopMagicHashValues[sq]) >> (Constants.SQUARE_COUNT - _bishopBlockerComboIndexBitCount[sq]);
                    _bishopMagicAttacks[sq, key] = GetBishopAttackBoardClassic(sq.Square(), blockers);
                }

                for (int blockerComboIdx = 0; blockerComboIdx < (1 << _rookBlockerComboIndexBitCount[sq]); blockerComboIdx++)
                {
                    ulong blockers = GenerateBlockersFromMaskAndIdx(blockerComboIdx, _rookAttackMasks[sq]);
                    var key = (blockers * _rookMagicHashValues[sq]) >> (Constants.SQUARE_COUNT - _rookBlockerComboIndexBitCount[sq]);
                    _rookMagicAttacks[sq, key] = GetRookAttackBoardClassic(sq.Square(), blockers);
                }
            }
        }

        /// <summary>
        /// gets the attack board for a piece in position using rays of consecutive squares for the pieces attack directions 
        /// which are then modified by the blocked squares
        /// note: this can also be used in real time, not just when pre-calculating
        /// </summary>
        /// <param name="sq">the current position of the bishop</param>
        /// <param name="blockedSquares">board set with all blocked squares</param>
        /// <returns>valid squares for attack</returns>
        public static ulong GetBishopAttackBoardClassic(Square sq, ulong blockedSquares)
        {
            // set attack to all squares to the NE of the position
            ulong attack = ConsecutiveSquares.GetBoard(Direction.NorthEast, sq);
            // check if any of the blockers coincide with current attack squares
            if ((blockedSquares & attack) > 0)
            {
                // get the first coinciding blocked square
                var firstSq = BoardUtils.ScanBitsForward(ConsecutiveSquares.GetBoard(Direction.NorthEast, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.NorthEast, firstSq.Square());
            }

            attack |= ConsecutiveSquares.GetBoard(Direction.NorthWest, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.NorthWest, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsForward(ConsecutiveSquares.GetBoard(Direction.NorthWest, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.NorthWest, firstSq.Square());
            }

            attack |= ConsecutiveSquares.GetBoard(Direction.SouthEast, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.SouthEast, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsBackward(ConsecutiveSquares.GetBoard(Direction.SouthEast, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.SouthEast, firstSq.Square());
            }

            attack |= ConsecutiveSquares.GetBoard(Direction.SouthWest, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.SouthWest, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsBackward(ConsecutiveSquares.GetBoard(Direction.SouthWest, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.SouthWest, firstSq.Square());
            }

            return attack;
        }

        /// <summary>
        /// gets the attack board for a piece in position using rays of consecutive squares for the pieces attack directions 
        /// which are then modified by the blocked squares
        /// note: this can also be used in real time, not just when pre-calculating
        /// </summary>
        /// <param name="sq">the current position of the rook</param>
        /// <param name="blockedSquares">board set with all blocked squares</param>
        /// <returns>valid squares for attack</returns>
        public static ulong GetRookAttackBoardClassic(Square sq, ulong blockedSquares)
        {
            // set attack to all squares to the NE of the position
            ulong attack = ConsecutiveSquares.GetBoard(Direction.North, sq);
            // check if any of the blockers coincide with current attack squares
            if ((blockedSquares & attack) > 0)
            {
                // get the first coinciding blocked square
                var firstSq = BoardUtils.ScanBitsForward(ConsecutiveSquares.GetBoard(Direction.North, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.North, firstSq.Square());
            }

            attack |= ConsecutiveSquares.GetBoard(Direction.East, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.East, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsForward(ConsecutiveSquares.GetBoard(Direction.East, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.East, firstSq.Square());
            }

            // both south and west use reverse bitscan because that will give the square closesst to our attacking piece
            attack |= ConsecutiveSquares.GetBoard(Direction.South, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.South, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsBackward(ConsecutiveSquares.GetBoard(Direction.South, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.South, firstSq.Square());
            }

            attack |= ConsecutiveSquares.GetBoard(Direction.West, sq);
            if ((ConsecutiveSquares.GetBoard(Direction.West, sq) & blockedSquares) > 0)
            {
                var firstSq = BoardUtils.ScanBitsBackward(ConsecutiveSquares.GetBoard(Direction.West, sq) & blockedSquares);
                attack &= ~ConsecutiveSquares.GetBoard(Direction.West, firstSq.Square());
            }

            return attack;
        }

        /// <summary>
        /// get the pre-calculated attack squares using the magic hash for the source square
        /// </summary>
        /// <param name="sq">bishop position</param>
        /// <param name="blockedSquares">all the blockers on the board, this can be all pieces except for the subject bishop</param>
        /// <returns>the valid attack squares</returns>
        public static ulong GetBishopAttackBoardMagic(Square sq, ulong blockedSquares)
        {
            var sqIdx = sq.Integer();
            // get only relevant blockers
            blockedSquares &= _bishopAttackMasks[sqIdx];
            // generate the key by hashing the magic number and the blocked squares and then shift to cut the hash down to our indices size
            var key = (blockedSquares * _bishopMagicHashValues[sqIdx]) >> (Constants.SQUARE_COUNT - _bishopBlockerComboIndexBitCount[sqIdx]);
            // fetch via square and hash the blocker and magic to get the key index
            return _bishopMagicAttacks[sqIdx, key];
        }

        /// <summary>
        /// get the pre-calculated attack squares using the magic hash for the source square
        /// </summary>
        /// <param name="sq">rook position</param>
        /// <param name="blockedSquares">all the blockers on the board, this can be all pieces except for the subject rook</param>
        /// <returns>the valid attack squares</returns>
        public static ulong GetRookAttackBoardMagic(Square sq, ulong blockedSquares)
        {
            var sqIdx = sq.Integer();
            // get only relevant blockers
            blockedSquares &= _rookAttackMasks[sqIdx];
            // generate the key by hashing the magic number and the blocked squares and then shift to cut the hash down to our indices size
            var key = (blockedSquares * _rookMagicHashValues[sqIdx]) >> (Constants.SQUARE_COUNT - _rookBlockerComboIndexBitCount[sqIdx]);
            // fetch via square and hash the blocker and magic to get the key index
            return _rookMagicAttacks[sqIdx, key];
        }

        public static ulong GetKnightAttackBoard(Square square)
        {
            return _knightAttacks[square.Integer()];
        }

        /// <summary>
        /// generates a blocker bitboard for an attack pattern, using the index of the combination
        /// </summary>
        /// <param name="combinationIdx">the index of the combination for the set of all possible blockers for a square and attack pattern</param>
        /// <param name="mask">piece attack pattern mask</param>
        /// <returns>combination of blockers from attack pattern and index</returns>
        private static ulong GenerateBlockersFromMaskAndIdx(int combinationIdx, ulong mask)
        {
            ulong blockerBoard = 0x0UL;
            int maskBits = BoardUtils.CountSetBits(mask);
            for (int i = 0; i < maskBits; i++)
            {
                int lsb = BoardUtils.PopLSB(ref mask);
                if ((combinationIdx & (0x1 << i)) == combinationIdx)
                    blockerBoard |= (0x1UL << lsb);
            }

            return blockerBoard;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static class ConsecutiveSquares
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ulong[,] _boards = new ulong[8, 64];

        /// <summary>
        /// 
        /// </summary>
        static ConsecutiveSquares()
        {
            for (int i = 0; i < Constants.SQUARE_COUNT; i++)
            {
                _boards[Direction.North.Integer(), i] = 0x0101010101010100UL << i;

                _boards[Direction.South.Integer(), i] = 0x0080808080808080UL >> (63 - i);

                _boards[Direction.East.Integer(), i] = ((0x1UL << (i | 7)) - (0x1UL << i)) * 2;

                _boards[Direction.West.Integer(), i] = (0x1UL << i) - (0x1UL << (i & 56));

                _boards[Direction.NorthWest.Integer(), i] =
                    BoardUtils.MoveSquaresLeft(0x102040810204000UL, 7 - BoardUtils.File(i)) << (BoardUtils.Rank(i) * 8);

                _boards[Direction.NorthEast.Integer(), i] =
                    BoardUtils.MoveSquaresRight(0x8040201008040200UL, BoardUtils.File(i)) << (BoardUtils.Rank(i) * 8);

                _boards[Direction.SouthWest.Integer(), i] =
                    BoardUtils.MoveSquaresLeft(0x40201008040201UL, 7 - BoardUtils.File(i)) >> (7 - BoardUtils.Rank(i) * 8);

                _boards[Direction.SouthEast.Integer(), i] =
                    BoardUtils.MoveSquaresRight(0x2040810204080UL, BoardUtils.File(i)) >> (7 - BoardUtils.Rank(i) * 8);
            }
        }

        /// <summary>
        /// gets consecutive squares in a direction from the source square
        /// </summary>
        /// <param name="dir">compass direction</param>
        /// <param name="srcSquare">board square/bit index 0..63</param>
        /// <returns>board with line in direction set</returns>
        public static ulong GetBoard(Direction dir, Square srcSquare)
        {
            return _boards[dir.Integer(), srcSquare.Integer()];
        }
    }
}
