using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Romp;

namespace RompCons
{
    class Program
    {
        static void one(List<int> v)
        {
            v.Add(1);
            Console.WriteLine(v.GetHashCode());
        }

        static void two(List<int> v)
        {
            v.Add(2);
            Console.WriteLine(v.GetHashCode());
        }



        static void Main(string[] args)
        {

            //const string FILEPATH = @"C:\Users\JoeVidunas\source\repos\chess-romp\data\test.pgn";

            //var reader = new GameRecordFileReader_PGN(
            //    new string[] {"Event",
            //    "Site",
            //    "White",
            //    "Black",
            //    "Result",
            //    "UTCDate",
            //    "UTCTime",
            //    "WhiteElo",
            //    "BlackElo",
            //    "WhiteRatingDiff",
            //    "BlackRatingDiff",
            //    "ECO",
            //    "Opening",
            //    "TimeControl",
            //    "Termination"}, FILEPATH);

            //var got = reader.Get(true);
            //Console.Write(got.ToString());
            //got = reader.Get(false);
            //Console.Write(got.ToString());
            //got = reader.Get(false);
            //Console.Write(got.ToString());

            ulong white = 0x00000000000011FF;
            white = 0x0101010101010100;

            
            Console.WriteLine("==================================================================");
            //Console.WriteLine(Utils.DisplayAsBinaryBoard(white, true));
            //foreach (Direction item in Enum.GetValues(typeof(Direction)))
            //{
            //    var i = (int)item;
            //    Console.WriteLine(i);
            //}

            var list = new List<int>();

            list.Add(0);
            one(list);
            two(list);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("==================================================================");         

        }
    }
}
