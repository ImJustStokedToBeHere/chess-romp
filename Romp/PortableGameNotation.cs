using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Romp
{
    internal class GameRecordFileReader_PGN : GameRecordFileReader
    {
        private int _recordIdx = 0;

        public GameRecordFileReader_PGN(string[] fieldNames, string fileName)
            : this(fieldNames, new FileStream(fileName, FileMode.Open)) { }

        public GameRecordFileReader_PGN(string[] fieldNames, FileStream file)
            : base(fieldNames, file)
        {

        }

        public override GameRecord Get(bool readFromStart)
        {
            if (readFromStart && FileData.CanSeek)
                FileData.Seek(0, SeekOrigin.Begin);

            var prevPos = FileData.Position;
            string gameStr = String.Empty;
            var values = new string[FieldNames.Count];
            var idx = 0;

            while (!_reader.EndOfStream)
            {
                char ch = (char)_reader.Read();
                if (ch == '[')
                {
                    string name = "";
                    // read until the next unread character is a space
                    while (_reader.Peek() != ' ' && !_reader.EndOfStream)
                        name += (char)_reader.Read();

                    // check that the field name is one which we actually want to import or validate
                    // TODOTODO: don't require field names
                    var result = FieldNames.Find((string s) => { return s == name; });

                    if (result != "")
                    {
                        idx = FieldNames.IndexOf(result);
                    }
                    else
                    {
                        BadData($"invalid field name: { name}", FileData.Position);
                    }

                    // eat the space folowing the field name
                    _ = (char)_reader.Read();
                    if (_reader.Peek() != '\"')
                        BadData($"badly formatted field value for: {name}", FileData.Position);

                    // eat the first quote
                    _ = (char)_reader.Read();

                    string value = String.Empty;
                    while (_reader.Peek() != '\"' && !_reader.EndOfStream)
                        values[idx] += (char)_reader.Read();

                    // eat the last quote
                    _ = (char)_reader.Read();

                    if (_reader.Peek() != ']')
                        BadData("badly formatted line, missing close bracket", FileData.Position);

                    // eat the closing ]
                    _ = (char)_reader.Read();

                    if (_reader.Peek() == '\r')
                        _ = (char)_reader.Read(); // eat the carriage return

                    if (_reader.Peek() != '\n')
                        BadData("badly formatted line, missing new line", FileData.Position);

                    // eat the new line
                    _ = (char)_reader.Read();
                }
                else if (ch == '\n' || ch == '\r')
                {
                    // there shouldn't be carraige returns, but we will eat them just in case
                    if (ch == '\r')
                        _ = (char)_reader.Read();

                    gameStr = _reader.ReadLine();

                    // just incase the last new line is trimmed from the file
                    if (!_reader.EndOfStream)
                        _reader.ReadLine();

                    break;
                }
                else
                {
                    continue;
                }
            }

            return new GameRecord_PGN(FieldNames, new List<string>(values), gameStr, prevPos, _recordIdx++);
        }
    }

    internal class GameRecord_PGN : GameRecord
    {
        public GameRecord_PGN(List<string> fieldNames, List<string> values, string gameStr, long filePos, int recordIdx)
            : base(fieldNames, values, gameStr, filePos, recordIdx) { }


        //public GameRecord_PGN(GameRecord rec)
        //    : base(new List<string>(rec.FieldNames.ToArray()),
        //          new List<string>(rec.FieldValues.ToArray()),
        //          new string(rec.GameString),
        //          rec.FilePos,
        //          rec.RecordIndex)
        //{ }

        // rebuild the PGN formatted string from the data
        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < FieldNames.Count; i++)
                result.AppendLine($"[{FieldNames[i]} \"{FieldValues[i]}\"]");

            result.AppendLine();
            result.AppendLine(GameString);
            result.AppendLine();

            return result.ToString();
        }
    }
}
