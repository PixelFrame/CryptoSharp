using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace RSA_lib
{
	/// <summary>
	/// 512位素数产生类
	/// </summary>
	public class PrimeGen
	{
		/// <summary>
		/// 3000以内的素数表
		/// 来源：https://en.wikipedia.org/wiki/List_of_prime_numbers#The_first_1000_prime_numbers
		/// </summary>
		private static readonly BigInteger[] _PRIME_LIST = new BigInteger[]
		{
			2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
			73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173,
			179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
			283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409,
			419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541,
			547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
			661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809,
			811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
			947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069,
			1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223,
			1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373,
			1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511,
			1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657,
			1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811,
			1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987,
			1993, 1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069, 2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129,
			2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213, 2221, 2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287,
			2293, 2297, 2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423,
			2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617,
			2621, 2633, 2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741,
			2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819, 2833, 2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903,
			2909, 2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999
		};
		/// <summary>
		/// 伪素数产生
		/// </summary>
		/// <returns></returns>
		private BigInteger PseudoPrime()
		{
			bool isPrime = false;
			BigInteger a = new BigInteger();
			while (!isPrime)
			{
				RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
				byte[] baCsp = new byte[65];
				csp.GetNonZeroBytes(baCsp);
				baCsp[64] = 0;
				isPrime = true;
				a = new BigInteger(baCsp);
				foreach (BigInteger b in _PRIME_LIST)
				{ 
					if (a % b == 0)
					{
						isPrime = false;
						break;
					}
				}
			}
			return a;
		}
		/// <summary>
		/// Rabin-Miller素性检测
		/// </summary>
		/// <param name="biN">被检数</param>
		/// <param name="iK">测试轮数 默认50</param>
		/// <returns>true：biN为素数，false：biN不是素数</returns>
		public bool RabinMiller(BigInteger biN, int iK = 50)
		{
			if ((biN & 1) == 0) return false;
			BigInteger s = 0, i = 1;
			BigInteger t = biN - 1;
			while ((t & 1) == 0)
			{
				t >>= 1;
				++s;
			}

			while (iK-- != 0)
			{
				RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
				byte[] baCsp = new byte[8];
				csp.GetNonZeroBytes(baCsp);
				BigInteger b = BitConverter.ToUInt64(baCsp, 0) % (biN - 2) + 2;
				BigInteger y = RSA_Math.RepeatMod(b, t, biN);
				if (y == 1)
					return true;
				while(y != biN-1)
				{
					if (i == s) return false;
					y = RSA_Math.RepeatMod(y, 2, biN);
					++i;
				}
			}
			return true;
		}
		/// <summary>
		/// 素数生成方法，可指定测试轮数
		/// </summary>
		/// <param name="iTestRound">测试轮数 默认50</param>
		/// <returns></returns>
		public BigInteger Gen(int iTestRound = 50)
		{
			BigInteger a = PseudoPrime();
			while(!RabinMiller(a, iTestRound))
			{
				a = PseudoPrime();
			}
			return a;
		}
	}
}
