using System.Collections.Generic;
using System.Text;

namespace Romp
{
    internal class Option
    {
        public const bool BUTTON_PRESSED = true;

        public delegate void OnChangeEventHandler(object sender, OnChangeEventArgs args);

        private event OnChangeEventHandler OnChangeEvent;

        public enum OptionType
        {
            Check,
            Spin,
            Combo,
            Button,
            String
        }

        public class OnChangeEventArgs
        {
            public OnChangeEventArgs(string oldStr, bool oldBool, int oldInt)
            {
                _valueStr = oldStr;
                _valueBool = oldBool;
                _valueInt = oldInt;
            }

            public string _valueStr;
            public bool _valueBool;
            public int _valueInt;
        }

        public static readonly Dictionary<string, Option> OptionsLookup = new Dictionary<string, Option>
            {
                { "ownbook", new Option(false, null) },
                { "bookpath", new Option("book.bin", null) },
                { "ponder", new Option(false, null) },
            };


        public readonly int MinValue;
        public readonly int MaxValue;

        private readonly OptionType _type;
        private string _valueStr;
        private bool _valueBool;
        private int _valueInt;


        public Option(bool initial, OnChangeEventHandler changeFunc)
            : this(OptionType.Check, changeFunc)
        {
            _valueBool = initial;
        }

        public Option(OnChangeEventHandler changeFunc)
            : this(OptionType.Button, changeFunc)
        {
            _valueBool = BUTTON_PRESSED;
        }

        public Option(int initial, int min, int max, OnChangeEventHandler changeFunc)
            : this(OptionType.Spin, changeFunc)
        {
            _valueInt = initial;
            MinValue = min;
            MaxValue = max;
        }

        public Option(string initial, OnChangeEventHandler changeFunc)
            : this(OptionType.String, changeFunc)
        {
            _valueStr = initial;
        }

        protected Option(OptionType type, OnChangeEventHandler changeFunc)
        {
            _type = type;
            OnChangeEvent += changeFunc;
        }

        public override string ToString() => _type switch
        {
            OptionType.String => _valueStr,
            OptionType.Check => _valueBool.ToString(),
            OptionType.Spin => _valueInt.ToString(),
            OptionType.Button => _valueInt.ToString(),
            _ => _valueStr
        };

        public string ValueString
        {
            get
            {
                return _valueStr;
            }

            set
            {
                if (value != _valueStr)
                {
                    ValueChanged(_valueStr, _valueBool, _valueInt);
                    _valueStr = value;
                }
            }
        }

        public bool ValueBool
        {
            get
            {
                return _valueBool;
            }

            set
            {
                if (value != _valueBool)
                {
                    ValueChanged(_valueStr, _valueBool, _valueInt);
                    _valueBool = value;
                }
            }
        }

        public int ValueInt
        {
            get
            {
                return _valueInt;
            }

            set
            {
                if (value != _valueInt)
                {
                    ValueChanged(_valueStr, _valueBool, _valueInt);
                    _valueInt = value;
                }
            }
        }

        public OptionType Type => _type;

        public static string GetInfoStr()
        {
            var strBld = new StringBuilder();

            foreach (var item in OptionsLookup)
            {
                strBld.AppendLine($"");
            }

            return strBld.ToString();
        }

        private void ValueChanged(string oldStr, bool oldBool, int oldInt)
        {
            OnChangeEvent?.Invoke(this, new OnChangeEventArgs(oldStr, oldBool, oldInt));
        }
    }
}
