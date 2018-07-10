using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA_lib
{
	class RSA_Math
	{
		public static BigInteger RepeatMod(BigInteger ulBase, BigInteger ulExponent, BigInteger ulMod)
		{
			BigInteger a = 1;
			while (ulExponent != 0)
			{
				if ((ulExponent & 1) != 0)
				{
					a = (a * ulBase) % ulMod;
				}
				ulBase = (ulBase * ulBase) % ulMod;
				ulExponent = ulExponent >> 1;
			}
			return a;
		}

		public static BigInteger SteinGCD(BigInteger biParam0, BigInteger biParam1)
		{
			BigInteger a = biParam0 > biParam1 ? biParam0 : biParam1;
			BigInteger b = biParam0 < biParam1 ? biParam0 : biParam1;
			BigInteger t, r = 1;
			BigInteger bigOne = 1;
			if (biParam0 == biParam1) return biParam0;
			else
			{
				while (((a & bigOne) == 0) && ((b & bigOne) == 0))
				{
					r <<= 1;
					a >>= 1;
					b >>= 1;
				}
				if ((a & bigOne) == 0)
				{
					t = a;
					a = b;
					b = t;
				}
				while (b != 0)
				{
					while ((b & bigOne) == 0)
					{
						b >>= 1;
					}
					if (b < a)
					{
						t = a;
						a = b;
						b = t;
					}
					b = (b - a) >> 1;
				}
				return r * a;
			}
		}

		public static BigInteger ExEuclid(BigInteger biParam0, BigInteger biParam1)
		{
			BigInteger m, e, i, j, x, y;
			long xx, yy;
			m = biParam1; e = biParam0; x = 0; y = 1; xx = 1; yy = 1;
			while (e != 0)
			{
				i = m / e; j = m % e;
				m = e; e = j; j = y; y *= i;
				if (xx == yy)
				{
					if (x > y)
						y = x - y;
					else
					{
						y -= x;
						yy = 0;
					}
				}
				else
				{
					y += x;
					xx = 1 - xx;
					yy = 1 - yy;
				}
				x = j;
			}
			if (xx == 0)
				x = biParam1 - x;
			return x;
		}

	}
}
