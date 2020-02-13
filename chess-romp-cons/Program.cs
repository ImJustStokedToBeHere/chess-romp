﻿using System;
using Romp;

namespace RompCons
{
    class Program
    {
        static void Main(string[] args)
        {

            const string FILEPATH = @"C:\Users\JoeVidunas\source\repos\chess-romp\data\test.pgn";

            var reader = new GameRecordFileReader_PGN(
                new string[] {"Event",
                "Site",
                "White",
                "Black",
                "Result",
                "UTCDate",
                "UTCTime",
                "WhiteElo",
                "BlackElo",
                "WhiteRatingDiff",
                "BlackRatingDiff",
                "ECO",
                "Opening",
                "TimeControl",
                "Termination"}, FILEPATH);

            var got = reader.Get(true);
            Console.Write(got.ToString());
            got = reader.Get(false);
            Console.Write(got.ToString());
            got = reader.Get(false);
            Console.Write(got.ToString());


            Console.WriteLine("Hello World!");
        }
    }
}
