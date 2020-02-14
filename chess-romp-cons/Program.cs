using System;
using System.Linq;
using System.Text;
using Romp;

namespace RompCons
{
    class Program
    {
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
            Console.WriteLine("==================================================================");

            Console.WriteLine(Utils.DisplayAsBinaryBoard(white));

            Console.WriteLine("==================================================================");

            Console.WriteLine(Utils.DisplayAsBinaryBoard(Utils.VerticalFlip(white)));

            Console.WriteLine("==================================================================");
            
        }
    }
}
