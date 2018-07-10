using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA_lib
{
	public class RSA_Main
	{
		public static BigInteger[] GenKey()
		{
			PrimeGen pgen = new PrimeGen();
			BigInteger p = new BigInteger(pgen.Gen());
			BigInteger q = new BigInteger(pgen.Gen());
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = new BigInteger(pgen.Gen());
			e %= r;
			while (RSA_Math.SteinGCD(e, r)!=1)
			{
				e = new BigInteger(pgen.Gen());
				e %= r;
			}
			BigInteger d = RSA_Math.ExEuclid(e, r);
			return new BigInteger[] { N, e, d };
		}

		/*public static BigInteger[] GenKeyTest()
		{
			PrimeGen pgen = new PrimeGen();
			BigInteger p = new BigInteger(pgen.Gen());
			BigInteger q = new BigInteger(pgen.Gen());
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = new BigInteger(pgen.Gen());
			e %= r;
			while (SteinGCD(e, r) != 1)
			{
				e = new BigInteger(pgen.Gen());
				e %= r;
			}
			BigInteger d = ExEuclid(e, r);
			return new BigInteger[] { N, e, d, p, q };
		}

		public static BigInteger[] GenKey(BigInteger p, BigInteger q)
		{
			PrimeGen pgen = new PrimeGen();
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = new BigInteger(pgen.Gen());
			e %= r;
			while (SteinGCD(e, r) != 1)
			{
				e = new BigInteger(pgen.Gen());
				e %= r;
			}
			BigInteger d = ExEuclid(e, r);
			return new BigInteger[] { N, e, d };
		}
		*/

		public static BigInteger Encrypt(BigInteger biPlain, BigInteger e, BigInteger N)
		{
			return RSA_Math.RepeatMod(biPlain, e, N);
		}

		public static BigInteger Decrypt(BigInteger biCipher, BigInteger d, BigInteger N)
		{
			return RSA_Math.RepeatMod(biCipher, d, N);
		}

	}
}
