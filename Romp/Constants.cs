using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    static class Constants
    {        
        public const Byte NULL_SQUARE = 65;

        public static ulong[] BitPositions = 
        {
             0x0000000000000001,
             0x0000000000000002,
             0x0000000000000004,
             0x0000000000000008,
             0x0000000000000010,
             0x0000000000000020,
             0x0000000000000040,
             0x0000000000000080,
             0x0000000000000100,
             0x0000000000000200,
             0x0000000000000400,
             0x0000000000000800,
             0x0000000000001000,
             0x0000000000002000,
             0x0000000000004000,
             0x0000000000008000,
             0x0000000000010000,
             0x0000000000020000,
             0x0000000000040000,
             0x0000000000080000,
             0x0000000000100000,
             0x0000000000200000,
             0x0000000000400000,
             0x0000000000800000,
             0x0000000001000000,
             0x0000000002000000,
             0x0000000004000000,
             0x0000000008000000,
             0x0000000010000000,
             0x0000000020000000,
             0x0000000040000000,
             0x0000000080000000,
             0x0000000100000000,
             0x0000000200000000,
             0x0000000400000000,
             0x0000000800000000,
             0x0000001000000000,
             0x0000002000000000,
             0x0000004000000000,
             0x0000008000000000,
             0x0000010000000000,
             0x0000020000000000,
             0x0000040000000000,
             0x0000080000000000,
             0x0000100000000000,
             0x0000200000000000,
             0x0000400000000000,
             0x0000800000000000,
             0x0001000000000000,
             0x0002000000000000,
             0x0004000000000000,
             0x0008000000000000,
             0x0010000000000000,
             0x0020000000000000,
             0x0040000000000000,
             0x0080000000000000,
             0x0100000000000000,
             0x0200000000000000,
             0x0400000000000000,
             0x0800000000000000,
             0x1000000000000000,
             0x2000000000000000,
             0x4000000000000000,
             0x8000000000000000
        };

        public static readonly string[] CoordinateStrs =
        {
            "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1",
            "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2",
            "A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3",
            "A4", "B4", "C4", "D4", "E4", "F4", "G4", "H4",
            "A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5",
            "A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6",
            "A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7",
            "A8", "B8", "C8", "D8", "E8", "F8", "G8", "H8"
        };

        public static readonly ulong[] ClearRanks = 
        {
            0xFFFFFFFFFFFFFF00,
            0xFFFFFFFFFFFF00FF,
            0xFFFFFFFFFF00FFFF,
            0xFFFFFFFF00FFFFFF,
            0xFFFFFF00FFFFFFFF,
            0xFFFF00FFFFFFFFFF,
            0xFF00FFFFFFFFFFFF,
            0x00FFFFFFFFFFFFFF
        };

        public static readonly ulong[] MaskRanks =
        {
            ~Constants.ClearRanks[0],
            ~Constants.ClearRanks[1],
            ~Constants.ClearRanks[2],
            ~Constants.ClearRanks[3],
            ~Constants.ClearRanks[4],
            ~Constants.ClearRanks[5],
            ~Constants.ClearRanks[6],
            ~Constants.ClearRanks[7]
        };

        public static readonly ulong[] ClearFiles = 
        {
            0xFEFEFEFEFEFEFEFE,
            0xFDFDFDFDFDFDFDFD,
            0xFBFBFBFBFBFBFBFB,
            0xF7F7F7F7F7F7F7F7,
            0xEFEFEFEFEFEFEFEF,
            0xDFDFDFDFDFDFDFDF,
            0xBFBFBFBFBFBFBFBF,
            0x7F7F7F7F7F7F7F7F
        };

        public static readonly ulong[] MaskFiles =
        {
            ~Constants.ClearFiles[0],
            ~Constants.ClearFiles[1],
            ~Constants.ClearFiles[2],
            ~Constants.ClearFiles[3],
            ~Constants.ClearFiles[4],
            ~Constants.ClearFiles[5],
            ~Constants.ClearFiles[6],
            ~Constants.ClearFiles[7]
        };
    }
}
