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
}
