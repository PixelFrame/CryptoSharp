using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ByteString_lib;

namespace RSA_lib
{
	public class RSA_Gen
	{

		/// <summary>
		/// RSA密钥生成方法
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
			while (RSA_Math.SteinGCD(e, r) != 1)
			{
				e = pgen.Gen();
				e %= r;
			}
			BigInteger d = RSA_Math.ExEuclid(e, r);
			return new BigInteger[] { N, e, d };
		}
		/// <summary>
		/// RSA密钥生成方法 除密钥外还返回了大素数p和q
		/// </summary>
		/// <returns>[n e d p q]</returns>
		public static BigInteger[] GenKeyAdd()
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
		/// 按PKCS#1格式生成RSA私钥
		/// </summary>
		public static byte[] PKCS1_Pri_Gen()
		{
			BigInteger[] RSA_Key = GenKeyAdd();
			BigInteger exponent1 = RSA_Key[2] % (RSA_Key[3] - 1);
			BigInteger exponent2 = RSA_Key[2] % (RSA_Key[4] - 1);
			BigInteger coefficient = (RSA_Key[3] - 1) % RSA_Key[4];

			ByteString priKey = new ByteString();
			ASN1_Convert asn1 = new ASN1_Convert();
			priKey += asn1.IntToBytes(0);
			priKey += asn1.BigIntToBytes(RSA_Key[0]);   // n
			priKey += asn1.BigIntToBytes(RSA_Key[1]);   // e
			priKey += asn1.BigIntToBytes(RSA_Key[2]);   // d
			priKey += asn1.BigIntToBytes(RSA_Key[3]);   // p
			priKey += asn1.BigIntToBytes(RSA_Key[4]);   // q
			priKey += asn1.BigIntToBytes(exponent1);    // exponent1
			priKey += asn1.BigIntToBytes(exponent2);    // exponent2
			priKey += asn1.BigIntToBytes(coefficient);  // coefficient
			priKey = asn1.SeqToBytes(priKey.GetBytes());

			return priKey.GetBytes();
		}
		/// <summary>
		/// 按PKCS#1格式生成RSA公钥
		/// </summary>
		/// <param name="biN"></param>
		/// <param name="biE"></param>
		public static void PKCS1_Pub_Gen()
		{

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
