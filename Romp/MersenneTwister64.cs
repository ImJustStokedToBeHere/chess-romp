using System;
using System.Collections.Generic;
using System.Text;

namespace Romp
{
    // This is ported from chess-romp/docs/MersenneTwister64.c. License is included there.
    // this is also not thread-safe in the least...may need to be addressed.
    class MersenneTwister64
    {
        private const int NN = 312;
        private const int MM = NN / 2;
        private const ulong MATRIX_A = 0xB5026F5AA96619E9UL;
        private const ulong UM = 0xFFFFFFFF80000000UL; // most significant 33 bits
        private const ulong LM = 0x000000007FFFFFFFUL;

        private ulong[] _mt = new ulong[NN];
        private int _mti = NN + 1;
        private ulong[] _mag = new ulong[] { 0UL, MATRIX_A };

        private void Init(ulong seed)
        {
            _mt[0] = seed;
            for (_mti = 1; _mti < NN; _mti++)
                _mt[_mti] = (6364136223846793005UL * (_mt[_mti - 1] ^ (_mt[_mti - 1] >> 62)) + (ulong)_mti);
        }

        public MersenneTwister64(ulong seed)
        {
            Init(seed);
        }

        public MersenneTwister64(ulong[] key, ulong keyLen)
        {
            Init(19650218UL);

            //ulong i = 0, j = 0, k = (NN > keyLen ? NN : keyLen);

            //for (; k > 0; k--)
            //{
            //    // _mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 62)) * 3935559000370003845UL)) + key[j] + j;
            //    var l = (_mt[i - 1] >> 62);
            //    var m = _mt[i - 1] ^ l;
            //    var n = m * 3935559000370003845UL;
            //    var o = _mt[i] ^ n;
            //    var p = o + key[j] + j;
            //    _mt[i] = p;



            //    i++;
            //    j++;

            //    if (i >= NN)
            //    {
            //        _mt[0] = _mt[NN - 1];
            //        i = 1;
            //    }

            //    if (j >= keyLen)
            //        j = 0;
            //}

            //for (k = NN; k > 0; k--)
            //{
            //    _mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 62)) * 2862933555777941757UL)) - i; /* non linear */
            //    i++;
            //    if (i >= NN)
            //    {
            //        _mt[0] = _mt[NN - 1];  
            //        i = 1;
            //    }
            //}

            //_mt[0] = 1UL << 63;

            ulong i, j, k;
            Init(19650218UL);
            i = 1;
            j = 0;
            k = (NN > keyLen ? NN : keyLen);

            for (; k > 0; k--)
            {
                _mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 62)) * 3935559000370003845UL))
                    + key[j] + j; /* non linear */
                i++;
                j++;

                if (i >= NN)
                {
                    _mt[0] = _mt[NN - 1];
                    i = 1;
                }

                if (j >= keyLen)
                    j = 0;
            }

            for (k = NN - 1; k > 0; k--)
            {
                _mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 62)) * 2862933555777941757UL)) - i; /* non linear */
                i++;
                if (i >= NN)
                {
                    _mt[0] = _mt[NN - 1];
                    i = 1;
                }
            }

            _mt[0] = 1UL << 63; /* MSB is 1; assuring non-zero initial array */ 

        }

    public ulong NextULong()
    {
        int i;
        ulong x;

        if (_mti >= NN)
        {
            if (_mti == NN + 1)
                Init(5489UL);

            for (i = 0; i < NN - MM; i++)
            {
                x = (_mt[i] & UM) | (_mt[i + 1] & LM);
                _mt[i] = _mt[i + MM] ^ (x >> 1) ^ _mag[(int)(x & 1UL)];
            }

            for (; i < NN - 1; i++)
            {
                x = (_mt[i] & UM) | (_mt[i + 1] & LM);
                _mt[i] = _mt[i + (MM - NN)] ^ (x >> 1) ^ _mag[(int)(x & 1UL)];
            }

            x = (_mt[NN - 1] & UM) | (_mt[0] & LM);
            _mt[NN - 1] = _mt[MM - 1] ^ (x >> 1) ^ _mag[(int)(x & 1UL)];

            _mti = 0;
        }

        x = _mt[_mti++];
        x ^= (x >> 29) & 0x5555555555555555UL;
        x ^= (x << 17) & 0x71D67FFFEDA60000UL;
        x ^= (x << 37) & 0xFFF7EEE000000000UL;
        x ^= (x >> 43);

        return x;
    }

    public long NextLong()
    {
        return (long)NextULong();
    }

    public double NextDouble()
    {
        return (NextULong() >> 11) * (1.0 / 9007199254740991.0);
    }


}
}
