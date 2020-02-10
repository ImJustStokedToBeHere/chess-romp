using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Romp
{
    abstract class GameRecordFileReader
    {
        public readonly List<string> FieldNames = null;
        public readonly Stream FileData;
        protected readonly StreamReader _reader;

        
        public GameRecordFileReader(string[] fieldNames, Stream data)
        {
            FieldNames = new List<string>(fieldNames);
            FileData = data;
            _reader = new StreamReader(FileData);
        }

        public abstract GameRecord Get(bool readFromStart);

        protected void BadData(string msg, long errorPos)
        {            
            throw new InvalidDataException($"bad data: {msg}, file position: {errorPos}");
        }

    }
}
