using System;
using System.Collections.Generic;

namespace Romp
{
    internal class GameRecord
    {
        public readonly List<string> FieldNames;
        public readonly List<string> FieldValues;
        public readonly string GameString;
        public readonly long FilePos;
        public readonly int RecordIndex;

        public GameRecord(List<string> fieldNames, List<string> values, string gameStr, long filePos, int recordIdx)
        {
            FieldNames = fieldNames;
            FieldValues = values;
            GameString = gameStr;
            FilePos = filePos;
            RecordIndex = recordIdx;
        }

        public override string ToString() => throw new NotImplementedException();
    }
}
