using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using BASE64_lib;
using ByteString_lib;

namespace RSA_lib
{
	/// <summary>
	/// RSA密钥读取类
	/// </summary>
	public class RSA_Read
	{
		/// <summary>
		/// 读取PKCS#1格式RSA密钥
		/// </summary>
		/// <param name="baPKCS1"></param>
		/// <returns></returns>
		public static BigInteger[] PKCS1_Read(byte[] baPKCS1)
		{
			ByteString bsPKCS1 = new ByteString(baPKCS1);
			if (bsPKCS1[0] != 0x30) throw new RSAKeyFormatErrorException();
			bsPKCS1.RemoveAt(0);
			int len = ReadLengthAndRemove(ref bsPKCS1);
			List<BigInteger> RSA_Key = new List<BigInteger>();
			while (bsPKCS1.GetLength() != 0)
			{
				if (bsPKCS1[0] != 0x02) throw new RSAKeyFormatErrorException();
				bsPKCS1.RemoveAt(0);
				len = ReadLengthAndRemove(ref bsPKCS1);
				RSA_Key.Add(new BigInteger(ReadBytesAndRemove(ref bsPKCS1, len)));
			}
			return RSA_Key.ToArray();
		}
		/// <summary>
		/// 读取经过BASE64编码的PKCS#1格式RSA私钥
		/// </summary>
		/// <param name="baPKCS1"></param>
		/// <returns></returns>
		public static BigInteger[] PKCS1_Pri_Read(string strPKCS1)
		{
			try
			{
				strPKCS1 = strPKCS1.Remove(0, 32);
				strPKCS1 = strPKCS1.Remove(strPKCS1.LastIndexOf("\n-----END RSA PRIVATE KEY-----\n"));
				strPKCS1 = strPKCS1.Replace("\n", "");
			}
			catch(Exception inner)
			{
				throw new RSAKeyFormatErrorException(inner);
			}
			return PKCS1_Read(BASE64_Main.DecodeB(Encoding.UTF8.GetBytes(strPKCS1)));
		}
		/// <summary>
		/// 读取经过BASE64编码的PKCS#1格式RSA公钥
		/// </summary>
		/// <param name="baPKCS1"></param>
		/// <returns></returns>
		public static BigInteger[] PKCS1_Pub_Read(string strPKCS1)
		{
			try
			{
				strPKCS1 = strPKCS1.Remove(0, 31);
				strPKCS1 = strPKCS1.Remove(strPKCS1.LastIndexOf("\n-----END RSA PUBLIC KEY-----\n"));
				strPKCS1 = strPKCS1.Replace("\n", "");
			}
			catch (Exception inner)
			{
				throw new RSAKeyFormatErrorException(inner);
			}
			return PKCS1_Read(BASE64_Main.DecodeB(Encoding.UTF8.GetBytes(strPKCS1)));
		}
		/// <summary>
		/// 读取长度并删除
		/// </summary>
		/// <param name="bsPKCS1"></param>
		/// <returns></returns>
		private static int ReadLengthAndRemove(ref ByteString bsPKCS1)
		{
			int len = 0;
			if ((bsPKCS1[0] & 0x80) == 0x80)
			{
				int lenLen = bsPKCS1[0] & 0x7F;
				bsPKCS1.RemoveAt(0);
				while (lenLen-- > 0)
				{
					len |= bsPKCS1[0] << (8 * lenLen);
					bsPKCS1.RemoveAt(0);
				}
			}
			else
			{
				len = bsPKCS1[0];
				bsPKCS1.RemoveAt(0);
			}
			return len;
		}
		/// <summary>
		/// 读取内容并删除
		/// </summary>
		/// <param name="bsPKCS1"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		private static byte[] ReadBytesAndRemove(ref ByteString bsPKCS1, int len)
		{
			ByteString bsData = new ByteString();
			while (len-- > 0)
			{
				bsData += bsPKCS1[len];
				bsPKCS1.RemoveAt(len);
			}
			return bsData.GetBytes();
		}
	}
	/// <summary>
	/// RSA密钥格式错误异常
	/// </summary>
	public class RSAKeyFormatErrorException : ApplicationException
	{
		public string errInfo = "RSA Key Format Error.";
		public RSAKeyFormatErrorException() { }
		public RSAKeyFormatErrorException(Exception inner)
		{
			errInfo = string.Format("RSA Key Format Error: {0:S}.", inner.Message);
		}
	}
}
