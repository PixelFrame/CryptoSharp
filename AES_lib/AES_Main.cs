using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteString_lib;

using WORD = System.UInt32;

namespace AES_lib
{
	/// <summary>
	/// 分组操作模式
	///		0 ECB 电码本
	///		1 CBC 密文分组链接
	///		2 CFB 密文反馈(未实现)
	///		3 OFB 输出反馈
	///		4 PCBC 扩散密文分组链接
	///		5 CTR 计数器(未实现)
	/// </summary>
	public enum Mode { ECB = 0, CBC = 1, CFB = 2, OFB = 3, PCBC = 4, CTR = 5 };
	/// <summary>
	/// AES加解密主要类
	/// </summary>
	public class AES_Main
    {
		/// <summary>
		/// 基本加密方法，输入输出类型为内部类型
		/// </summary>
		/// <param name="aesPlain"></param>
		/// <param name="aesKey"></param>
		/// <returns></returns>
		static AES_Data_128 Encrypt(AES_Data_128 aesPlain, AES_Key_128 aesKey)
		{
			AES_Data_128 aesRes = new AES_Data_128(aesPlain);
			List<WORD> AES_RoundKey = new List<WORD>();
			AES_RoundKey.Add(aesKey.AES_Key[0]);
			AES_RoundKey.Add(aesKey.AES_Key[1]);
			AES_RoundKey.Add(aesKey.AES_Key[2]);
			AES_RoundKey.Add(aesKey.AES_Key[3]);
			aesRes += AES_RoundKey;
			for (int round = 1; round < 10; ++round)
			{
				aesRes.Swap_S();
				aesRes.RowSwap();
				aesRes.ColMix();
				AES_RoundKey.Clear();
				AES_RoundKey.Add(aesKey.AES_Key[4 * round]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 1]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 2]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 3]);
				aesRes += AES_RoundKey;
			}
			aesRes.Swap_S();
			aesRes.RowSwap();
			AES_RoundKey.Clear();
			AES_RoundKey.Add(aesKey.AES_Key[40]);
			AES_RoundKey.Add(aesKey.AES_Key[41]);
			AES_RoundKey.Add(aesKey.AES_Key[42]);
			AES_RoundKey.Add(aesKey.AES_Key[43]);
			aesRes += AES_RoundKey;
			return aesRes;
		}
		/// <summary>
		/// 对基本加密方法的封装，输入输出为byte数组
		/// </summary>
		/// <param name="baPlain"></param>
		/// <param name="baKey"></param>
		/// <returns></returns>
		public static byte[] Encrypt(byte[] baPlain, byte[] baKey)
		{
			AES_Data_128 aesPlain = new AES_Data_128(baPlain);
			AES_Key_128 aesKey = new AES_Key_128(baKey);
			return Encrypt(aesPlain, aesKey).GetBytes();
		}
		/// <summary>
		/// 基本解密方法，输入输出类型为内部类型
		/// </summary>
		/// <param name="aesCipher"></param>
		/// <param name="aesKey"></param>
		/// <returns></returns>
		static AES_Data_128 Decrypt(AES_Data_128 aesCipher, AES_Key_128 aesKey)
		{
			AES_Data_128 aesRes = new AES_Data_128(aesCipher);
			List<WORD> AES_RoundKey = new List<WORD>();
			AES_RoundKey.Add(aesKey.AES_Key[40]);
			AES_RoundKey.Add(aesKey.AES_Key[41]);
			AES_RoundKey.Add(aesKey.AES_Key[42]);
			AES_RoundKey.Add(aesKey.AES_Key[43]);
			aesRes += AES_RoundKey;
			for (int round = 9; round > 0; --round)
			{
				aesRes.RowSwap_T();
				aesRes.Swap_S_T();
				AES_RoundKey.Clear();
				AES_RoundKey.Add(aesKey.AES_Key[4 * round]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 1]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 2]);
				AES_RoundKey.Add(aesKey.AES_Key[4 * round + 3]);
				aesRes += AES_RoundKey;
				aesRes.ColMix_T();
			}
			aesRes.RowSwap_T();
			aesRes.Swap_S_T();
			AES_RoundKey.Clear();
			AES_RoundKey.Add(aesKey.AES_Key[0]);
			AES_RoundKey.Add(aesKey.AES_Key[1]);
			AES_RoundKey.Add(aesKey.AES_Key[2]);
			AES_RoundKey.Add(aesKey.AES_Key[3]);
			aesRes += AES_RoundKey;
			return aesRes;
		}
		/// <summary>
		/// 对基本解密方法的封装，输入输出为byte数组
		/// </summary>
		/// <param name="baCipher"></param>
		/// <param name="baKey"></param>
		/// <returns></returns>
		public static byte[] Decrypt(byte[] baCipher, byte[] baKey)
		{
			AES_Data_128 aesPlain = new AES_Data_128(baCipher);
			AES_Key_128 aesKey = new AES_Key_128(baKey);
			return Decrypt(aesPlain, aesKey).GetBytes();
		}
		/// <summary>
		/// 分组加密方法
		/// </summary>
		/// <param name="baPlain"></param>
		/// <param name="baKey"></param>
		/// <param name="baIV"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static byte[] EncryptBlock(byte[] baPlain, byte[] baKey, byte[] baIV, Mode mode)
		{
			List<byte> blCipher = new List<byte>();
			ByteString bsPlain = new ByteString(baPlain);
			AES_Key_128 aesKey = new AES_Key_128(baKey);
			int part, rem, count;
			switch (mode)
			{
				case Mode.ECB:
					part = (int)Math.Ceiling(baPlain.Length / 16.0);
					count = part;
					rem = 16 - (baPlain.Length % 16);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(bsPlain.SubString((part - count) * 16, 16).GetBytes());
						blCipher.AddRange(Encrypt(aesToken, aesKey).GetBytes());
						--count;
					}
					break;
				case Mode.CBC:
					part = (int)Math.Ceiling(baPlain.Length / 16.0);
					count = part;
					rem = 16 - (baPlain.Length % 16);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(bsPlain.SubString((part - count) * 16, 16).GetBytes());
						aesToken ^= baIV;
						baIV = Encrypt(aesToken, aesKey).GetBytes();
						blCipher.AddRange(baIV);
						--count;
					}
					break;
				case Mode.CFB:
					throw new ModeNotSupportedException("CFB");
				case Mode.OFB:
					part = (int)Math.Ceiling(baPlain.Length / 16.0);
					count = part;
					rem = 16 - (baPlain.Length % 16);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(baIV);
						baIV = Encrypt(aesToken, aesKey).GetBytes();
						blCipher.AddRange((bsPlain.SubString((part - count) * 16, 16) ^ baIV).GetBytes());
						--count;
					}
					break;
				case Mode.PCBC:
					part = (int)Math.Ceiling(baPlain.Length / 16.0);
					count = part;
					rem = 16 - (baPlain.Length % 16);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						ByteString bsPlainToken = bsPlain.SubString((part - count) * 16, 16);
						AES_Data_128 aesToken = new AES_Data_128((bsPlainToken ^ baIV).GetBytes());
						baIV = Encrypt(aesToken, aesKey).GetBytes();
						blCipher.AddRange(baIV);
						baIV = (bsPlainToken ^ baIV).GetBytes();
						--count;
					}
					break;
				case Mode.CTR:
					throw new ModeNotSupportedException("CTR");
				default:
					throw new ModeNotSupportedException("UNKNOWN");
			}
			return blCipher.ToArray();
		}
		/// <summary>
		/// 分组解密方法
		/// </summary>
		/// <param name="baCipher"></param>
		/// <param name="baKey"></param>
		/// <param name="baIV"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static byte[] DecryptBlock(byte[] baCipher, byte[] baKey, byte[] baIV, Mode mode)
		{
			List<byte> blPlain = new List<byte>();
			ByteString bsCipher = new ByteString(baCipher);
			AES_Key_128 aesKey = new AES_Key_128(baKey);
			int part, rem, count;
			switch (mode)
			{
				case Mode.ECB:
					part = (int)Math.Ceiling(baCipher.Length / 16.0);
					count = part;
					rem = 16 - (baCipher.Length % 16);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(bsCipher.SubString((part - count) * 16, 16).GetBytes());
						blPlain.AddRange(Decrypt(aesToken, aesKey).GetBytes());
						--count;
					}
					break;
				case Mode.CBC:
					part = (int)Math.Ceiling(baCipher.Length / 16.0);
					count = part;
					rem = 16 - (baCipher.Length % 16);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(bsCipher.SubString((part - count) * 16, 16).GetBytes());
						blPlain.AddRange((Decrypt(aesToken, aesKey)^baIV).GetBytes());
						baIV = aesToken.GetBytes();
						--count;
					}
					break;
				case Mode.CFB:
					throw new ModeNotSupportedException("CFB");
				case Mode.OFB:
					part = (int)Math.Ceiling(baCipher.Length / 16.0);
					count = part;
					rem = 16 - (baCipher.Length % 16);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						AES_Data_128 aesToken = new AES_Data_128(baIV);
						baIV = Encrypt(aesToken, aesKey).GetBytes();
						blPlain.AddRange((bsCipher.SubString((part - count) * 16, 16) ^ baIV).GetBytes());
						--count;
					}
					break;
				case Mode.PCBC:
					part = (int)Math.Ceiling(baCipher.Length / 16.0);
					count = part;
					rem = 16 - (baCipher.Length % 16);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						ByteString bsCipherToken = bsCipher.SubString((part - count) * 16, 16);
						AES_Data_128 aesToken = new AES_Data_128(bsCipherToken.GetBytes());
						ByteString bsPlainToken = new ByteString((Decrypt(aesToken, aesKey) ^ baIV).GetBytes());
						blPlain.AddRange(bsPlainToken.GetBytes());
						baIV = (bsCipherToken ^ bsPlainToken).GetBytes();
						--count;
					}
					break;
				case Mode.CTR:
					throw new ModeNotSupportedException("CTR");
				default:
					throw new ModeNotSupportedException("UNKNOWN");
			}
			return blPlain.ToArray();
		}
	}
	/// <summary>
	/// 未支持的操作模式异常
	/// </summary>
	class ModeNotSupportedException : ApplicationException
	{
		public string error;
		public ModeNotSupportedException(string message) : base(message)
		{
			error = "Mode \"" + message + "\" is NOT SUPPORTED in AES Encryption.";
		}
	}
}
