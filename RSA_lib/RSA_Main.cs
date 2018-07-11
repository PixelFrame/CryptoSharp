using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA_lib
{
	/// <summary>
	/// RSA加密主要类 需要System.Numerics.BigInteger类支持
	/// </summary>
	public class RSA_Main
	{
		/// <summary>
		/// RSA密钥生成
		/// </summary>
		/// <returns>[n, e, d]</returns>
		public static BigInteger[] GenKey()
		{
			PrimeGen pgen = new PrimeGen();
			BigInteger p = pgen.Gen();
			BigInteger q = pgen.Gen();
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = pgen.Gen();
			e %= r;
			while (RSA_Math.SteinGCD(e, r)!=1)
			{
				e = pgen.Gen();
				e %= r;
			}
			BigInteger d = RSA_Math.ExEuclid(e, r);
			return new BigInteger[] { N, e, d };
		}
		/// <summary>
		/// 测试用密钥生成方法 除密钥外还返回了大素数p和q
		/// </summary>
		/// <returns>[n e d p q]</returns>
		public static BigInteger[] GenKeyTest()
		{
			PrimeGen pgen = new PrimeGen();
			BigInteger p = pgen.Gen();
			BigInteger q = pgen.Gen();
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = pgen.Gen();
			e %= r;
			while (RSA_Math.SteinGCD(e, r) != 1)
			{
				e = pgen.Gen();
				e %= r;
			}
			BigInteger d = RSA_Math.ExEuclid(e, r);
			return new BigInteger[] { N, e, d, p, q };
		}
		/// <summary>
		/// 人为素数的密钥生成方法 如果参数不能通过素性检测则抛出异常
		/// </summary>
		/// <param name="p"></param>
		/// <param name="q"></param>
		/// <returns></returns>
		public static BigInteger[] GenKey(ulong p, ulong q)
		{
			PrimeGen pgen = new PrimeGen();
			if (!pgen.RabinMiller(p, 100)) throw new NotPrimeNumberException(p.ToString());
			if (!pgen.RabinMiller(q, 100)) throw new NotPrimeNumberException(q.ToString());
			BigInteger N = p * q;
			BigInteger r = (p - 1) * (q - 1);
			BigInteger e = pgen.Gen();
			e %= r;
			while (RSA_Math.SteinGCD(e, r) != 1)
			{
				e = pgen.Gen();
				e %= r;
			}
			BigInteger d = RSA_Math.ExEuclid(e, r);
			return new BigInteger[] { N, e, d };
		}
		/// <summary>
		/// RSA加密基本方法
		/// </summary>
		/// <param name="biPlain"></param>
		/// <param name="e"></param>
		/// <param name="N"></param>
		/// <returns></returns>
		public static BigInteger Encrypt(BigInteger biPlain, BigInteger e, BigInteger N)
		{
			return RSA_Math.RepeatMod(biPlain, e, N);
		}
		/// <summary>
		/// RSA解密基本方法
		/// </summary>
		/// <param name="biCipher"></param>
		/// <param name="d"></param>
		/// <param name="N"></param>
		/// <returns></returns>
		public static BigInteger Decrypt(BigInteger biCipher, BigInteger d, BigInteger N)
		{
			return RSA_Math.RepeatMod(biCipher, d, N);
		}
	}
	/// <summary>
	/// 非素数异常，消息内容是未通过检测的数
	/// </summary>
	public class NotPrimeNumberException : ApplicationException
	{
		public string errNum;
		public NotPrimeNumberException(string message) : base(message)
		{
			errNum = message;
		}
	}
}
