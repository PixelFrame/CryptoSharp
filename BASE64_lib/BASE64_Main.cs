using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteString_lib;

namespace BASE64_lib
{
	/// <summary>
	/// BASE64加解密主要类
	/// </summary>
	public class BASE64_Main
	{
		/// <summary>
		/// BASE64字符表
		/// </summary>
		static readonly byte[]	base64Table = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");
		static readonly byte	base64Pad = Encoding.UTF8.GetBytes("=")[0];
		static readonly byte[]	base64PadD = Encoding.UTF8.GetBytes("==");
		static readonly int[]	decodeTable =
		{
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -1, -1, -2, -2, -1, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-1, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, 62, -2, -2, -2, 63,
			52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -2, -2, -2, -2, -2, -2,
			-2,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -2, -2, -2, -2, -2,
			-2, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
			41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2,
			-2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2
		};
		/// <summary>
		/// BASE64加密方法 输入/输出byte数组
		/// </summary>
		/// <param name="bPlain"></param>
		/// <returns></returns>
		public static byte[] EncryptB(byte[] bPlain)
		{
			ByteString bsEnc = new ByteString();
			int bytes = bPlain.Length, current = 0;
			while (bytes > 2)
			{
				bsEnc += base64Table[bPlain[current] >> 2];
				bsEnc += base64Table[((bPlain[current] & 0x03) << 4) + (bPlain[current + 1] >> 4)];
				bsEnc += base64Table[((bPlain[current + 1] & 0x0f) << 2) + (bPlain[current + 2] >> 6)];
				bsEnc += base64Table[bPlain[current + 2] & 0x3f];
				current += 3;
				bytes -= 3;
			}
			if (bytes > 0)
			{
				bsEnc += base64Table[bPlain[current] >> 2];
				if (bytes % 3 == 1)
				{
					bsEnc += base64Table[(bPlain[current] & 0x03) << 4];
					bsEnc += base64PadD;
				}
				else if (bytes % 3 == 2)
				{
					bsEnc += base64Table[((bPlain[current] & 0x03) << 4) + (bPlain[current + 1] >> 4)];
					bsEnc += base64Table[(bPlain[current + 1] & 0x0f) << 2];
					bsEnc += base64Pad;
				}
			}
			return bsEnc.GetBytes();
		}
		/// <summary>
		/// BASE64加密方法 输入/输出字符串 不改变字符串编码
		/// </summary>
		/// <param name="strPlain">明文字符串</param>
		/// <param name="coding">字符串编码名称</param>
		/// <returns>BASE64字符串</returns>
		public static String EncryptS(String strPlain, String coding)
		{
			byte[] bPlain = Encoding.GetEncoding(coding).GetBytes(strPlain);
			return new String(Encoding.GetEncoding(coding).GetChars(EncryptB(bPlain)));
		}
		/// <summary>
		/// BASE64加密方法 输入/输出字符串 不改变字符串编码
		/// </summary>
		/// <param name="strPlain">明文字符串</param>
		/// <param name="coding">字符串编码编号</param>
		/// <returns>BASE64字符串</returns>
		public static String EncryptS(String strPlain, int coding)
		{
			byte[] bPlain = Encoding.GetEncoding(coding).GetBytes(strPlain);
			return new String(Encoding.GetEncoding(coding).GetChars(EncryptB(bPlain)));
		}
		/// <summary>
		/// BASE64解密方法 输入/输出byte数组
		/// </summary>
		/// <param name="baEnc"></param>
		/// <returns></returns>
		public static byte[] DecodeB(byte[] baEnc)
		{
			ByteString bsPlain = new ByteString();
			int current = 0, length = baEnc.Length, i = 0, bin = 0;
			byte ch;
			while (length-- > 0)
			{
				ch = baEnc[current++];
				if (ch == base64Pad)
				{
					if (current < baEnc.Length)
					{
						if (baEnc[current] != base64Pad && (i % 4) == 1)
						{
							return null;
						}
					}
					continue;
				}
				ch = (byte)decodeTable[ch];
				if (ch < 0)
				{
					continue;
				}

				switch (i % 4)
				{
					case 0:
						bin = ch << 2;
						break;
					case 1:
						bin |= ch >> 4;
						bsPlain += (byte)bin;
						bin = (ch & 0x0f) << 4;
						break;
					case 2:
						bin |= ch >> 2;
						bsPlain += (byte)bin;
						bin = (ch & 0x03) << 6;
						break;
					case 3:
						bin |= ch;
						bsPlain += (byte)bin;
						break;
				}
				i++;
			}
			return bsPlain.GetBytes();
		}
		/// <summary>
		/// BASE64解密方法 输入/输出字符串 不改变字符串编码
		/// </summary>
		/// <param name="strPlain">BASE64字符串</param>
		/// <param name="coding">字符串编码名称</param>
		/// <returns>明文字符串</returns>
		public static String DecodeS(String strEnc, String coding)
		{
			byte[] baEnc = Encoding.GetEncoding(coding).GetBytes(strEnc);
			return new String(Encoding.GetEncoding(coding).GetChars(DecodeB(baEnc)));
		}
		/// <summary>
		/// BASE64解密方法 输入/输出字符串 不改变字符串编码
		/// </summary>
		/// <param name="strPlain">BASE64字符串</param>
		/// <param name="coding">字符串编码编号</param>
		/// <returns>明文字符串</returns>
		public static String DecodeS(String strEnc, int coding)
		{
			byte[] baEnc = Encoding.GetEncoding(coding).GetBytes(strEnc);
			return new String(Encoding.GetEncoding(coding).GetChars(DecodeB(baEnc)));
		}
	}
}
