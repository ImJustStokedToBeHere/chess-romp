using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Romp
{
    // NOT FINISHED

    class PGN_Record
    {

    }

    class PGN_FileReader
    {
        PGN_FileReader(string fieldNames, string fileName)
            : this(fieldNames, new FileStream(fileName, FileMode.Open))
        {

        }

        PGN_FileReader(string fieldNames, FileStream file)
        {
            _fieldNames = fieldNames;
            _file = file;
            _pos = file.Position;
        }

        private string _fieldNames;
        private FileStream _file;
        private long _pos;

    }
}
