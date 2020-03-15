using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    static class Utils
    {
        public static string ReverseString(string s)
        {
            var chars = new char[s.Length];
            int fwd = 0;
            for (int i = s.Length; i >= 0; i--)
                chars[fwd++] = s[i];

            return new string(chars);
        }

        // HACK: this is probably the slowest way I could possibly do this but it's not going to be used very often
        public static string DisplayAsBinaryBoard(ulong board, bool insertSpaces)
        {
            var binary = Convert.ToString((long)board, 2).PadLeft(64, '.');
            var sb = new StringBuilder();

            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    string s = binary.Substring(i, 8).Replace('0', '.');
                    string s2 = String.Empty;

                    for (int j = 7; j > -1; j--)
                    {
                        if (j == 0 || !insertSpaces)
                            s2 += s[j];
                        else
                            s2 += s[j] + " ";
                    }

                    sb.AppendLine(s2);

                }
            }

            return sb.ToString();
        }

        public static bool SwitchStrToBool(in string s)
        {
            if (s.ToLowerInvariant() == "on")
                return true;
            else if (s.ToLowerInvariant() == "off")
                return false;
            else
                throw new ArgumentException("switch must be set to \"on\" or \"off\"");
        }
    }
}
