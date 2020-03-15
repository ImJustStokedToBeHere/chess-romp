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

        //static int Main(string[] args)
        //{   
        //    for (int i = 0; i < Constants.SQUARE_COUNT; i++)
        //    {
        //        Console.WriteLine("=======================================================");
        //        var board = PieceAttacks.GetKnightAttackBoard(i.Square());
        //        Console.WriteLine($"{board:X}");
        //        Console.WriteLine(Utils.DisplayAsBinaryBoard(board, true));
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.North)}: {ConsecutiveSquares.GetBoard(Direction.North, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.North, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.South)}: {ConsecutiveSquares.GetBoard(Direction.South, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.South, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.East)}: {ConsecutiveSquares.GetBoard(Direction.East, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.East, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.West)}: {ConsecutiveSquares.GetBoard(Direction.West, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.West, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.NorthEast)}: {ConsecutiveSquares.GetBoard(Direction.NorthEast, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.NorthEast, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.SouthEast)}: {ConsecutiveSquares.GetBoard(Direction.SouthEast, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.SouthEast, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.NorthWest)}: {ConsecutiveSquares.GetBoard(Direction.NorthWest, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.NorthWest, i.Square()), true));
        //        //Console.WriteLine("++++++++++++++++");
        //        //Console.WriteLine($"{Enum.GetName(typeof(Direction), Direction.SouthWest)}: {ConsecutiveSquares.GetBoard(Direction.SouthWest, i.Square()):x}");
        //        //Console.WriteLine(Utils.DisplayAsBinaryBoard(ConsecutiveSquares.GetBoard(Direction.SouthWest, i.Square()), true));
        //        Console.WriteLine("=======================================================");
        //        Console.ReadKey();
        //    }
            
        //    return 0;
        //}

        static int Main(string[] args)
        {
            var mt = new MersenneTwister64(new ulong[]{ 0x12345UL, 0x23456UL, 0x34567UL, 0x45678UL }, 4);
            for (int i = 0; i < 1000; i++)
            {
                Console.Write($"{mt.NextULong()} " + (i % 5 == 4 ? "\r\n" : ""));
            }

            return 0;
        }
    }
}
