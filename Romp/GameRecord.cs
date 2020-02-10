using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    class GameRecord
    {
        public GameRecord(List<string> fieldNames, List<string> values, string gameStr, long filePos, int recordIdx)
        {
            FieldNames = fieldNames;
            FieldValues = values;
            GameString = gameStr;
            FilePos = filePos;
            RecordIndex = recordIdx;
        }

        public override string ToString() => throw new NotImplementedException();

        public readonly List<string> FieldNames;
        public readonly List<string> FieldValues;
        public readonly string GameString;
        public readonly long FilePos;
        public readonly int RecordIndex;
    }
}
