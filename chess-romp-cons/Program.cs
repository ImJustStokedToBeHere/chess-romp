using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Romp;
using System.CommandLine;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RompCons
{
    class Program
    {
        // const string FILEPATH = @"C:\Users\JoeVidunas\source\repos\chess-romp\data\test.pgn";

        //        static void Main(string[] args)
        //        {            
        //            if (args.Length == 0)
        //            {
        //#if (DEBUG)
        //                // poll the task list for debugging
        //                var reset = new AutoResetEvent(false);
        //                var timer = new Timer(CheckTasks, null, 5000, 5000);
        //#endif
        //                UCI.StartMainLoop(args);
        //                Task.WaitAll(UCI.TaskList.ToArray());
        //            }
        //            else
        //            {
        //                // TODOTODO: non-uci cmdline option handling
        //            }
        //        }

        //        static void CheckTasks(object taskList)
        //        {   
        //            foreach (var task in UCI.TaskList)            
        //                Debug.WriteLine($"task: {task.Id} desc: {UCI.TaskIdLookup[task.Id]} status: {Enum.GetName(typeof(TaskStatus), task.Status)}");
        //        }


        private static readonly ulong[] nums = new ulong[] 
        {
            0x07EDD5E59A4E28C2, 0x6c00049b0002001, 0x100200010090040, 0x2480041000800801, 0x280028004000800,
            0x900410008040022, 0x280020001001080, 0x2880002041000080, 0xa000800080400034, 0x4808020004000,
            0x2290802004801000, 0x411000d00100020, 0x402800800040080, 0xb000401004208, 0x2409000100040200,
            0x1002100004082, 0x22878001e24000, 0x1090810021004010, 0x801030040200012, 0x500808008001000,
            0xa08018014000880, 0x8000808004000200, 0x201008080010200, 0x801020000441091, 0x800080204005,
            0x1040200040100048, 0x120200402082, 0xd14880480100080, 0x12040280080080, 0x100040080020080,
            0x9020010080800200, 0x813241200148449, 0x491604001800080, 0x100401000402001, 0x4820010021001040,
            0x400402202000812, 0x209009005000802, 0x810800601800400, 0x4301083214000150, 0x204026458e001401,
            0x40204000808000, 0x8001008040010020, 0x8410820820420010, 0x1003001000090020, 0x804040008008080,
            0x12000810020004, 0x1000100200040208, 0x430000a044020001, 0x280009023410300, 0xe0100040002240,
            0x200100401700, 0x2244100408008080, 0x8000400801980, 0x2000810040200, 0x8010100228810400,
            0x2000009044210200, 0x4080008040102101, 0x40002080411d01, 0x2005524060000901, 0x502001008400422,
            0x489a000810200402, 0x1004400080a13, 0x4000011008020084, 0x26002114058042
        };

        static int Main(string[] args)
        {

            //foreach (var val in Romp.SlidingPieceAttacks._rookMasks)
            //{
            //    Console.WriteLine("=======================================================");
            //    Console.WriteLine(Utils.DisplayAsBinaryBoard(val, true));
            //    Console.WriteLine("=======================================================");
            //    Console.ReadKey();

            //}

            for (int i = 0; i < Constants.SQUARE_COUNT; i++)
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.North));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.North, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.South));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.South, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.East));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.East, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.West));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.West, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.NorthEast));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.NorthEast, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.SouthEast));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.SouthEast, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.NorthWest));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.NorthWest, i.Square()), true));
                Console.WriteLine("++++++++++++++++");
                Console.WriteLine(Enum.GetName(typeof(Direction), Direction.SouthWest));
                Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.SouthWest, i.Square()), true));
                Console.WriteLine("=======================================================");
                Console.ReadKey();

            }



            return 0;
        }
    }
}
