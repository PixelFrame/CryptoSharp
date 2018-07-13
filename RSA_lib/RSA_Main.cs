using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using ByteString_lib;

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
		/// <summary>
		/// 通过PKCS#1公钥进行加密，可以指定密文字符串分组间的分隔符，默认为'&'
		/// </summary>
		/// <param name="PKCS1_Pub_Key"></param>
		/// <param name="strPlain"></param>
		/// <param name="strCoding"></param>
		/// <param name="chSplit"></param>
		/// <returns></returns>
		public static string Encrypt_PKCS1_S(string PKCS1_Pub_Key, string strPlain, string strCoding, char chSplit = '&')
		{
			RSA_Plain rsaPlain = new RSA_Plain(strPlain, strCoding);
			BigInteger[] RSA_PubKey = RSA_Read.PKCS1_Pub_Read(PKCS1_Pub_Key);
			string RSA_Cipher = "";
			List<BigInteger> RSA_Cipher_Origin = new List<BigInteger>();
			foreach (BigInteger biPlain in rsaPlain.bilData)
			{
				BigInteger biCipher = Encrypt(biPlain, RSA_PubKey[1], RSA_PubKey[0]);
				RSA_Cipher_Origin.Add(biCipher);
				RSA_Cipher += (biCipher).ToString() + chSplit;
			}
			RSA_Cipher = RSA_Cipher.Remove(RSA_Cipher.Length - 1);
			return RSA_Cipher;
		}
		/// <summary>
		/// 通过PKCS#1私钥进行加密，可以指定密文字符串分组间的分隔符，默认为'&'
		/// </summary>
		/// <param name="PKCS1_Pri_Key"></param>
		/// <param name="strCipher"></param>
		/// <param name="strCoding"></param>
		/// <param name="chSplit"></param>
		/// <returns></returns>
		public static string Decrypt_PKCS1_S(string PKCS1_Pri_Key, string strCipher, string strCoding, char chSplit = '&')
		{
			RSA_Cipher rsaCipher = new RSA_Cipher(strCipher, chSplit);
			BigInteger[] RSA_PriKey = RSA_Read.PKCS1_Pri_Read(PKCS1_Pri_Key);
			ByteString bsPlain = new ByteString();
			foreach (BigInteger biCipher in rsaCipher.bilData)
			{
				BigInteger biPlain = Decrypt(biCipher, RSA_PriKey[3], RSA_PriKey[1]);
				bsPlain += biPlain.ToByteArray();
			}
			return new string(Encoding.GetEncoding(strCoding).GetChars(bsPlain.GetBytes()));
		}
	}
}
