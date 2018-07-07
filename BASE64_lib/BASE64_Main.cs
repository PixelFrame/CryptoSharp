﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteString_lib;

namespace BASE64_lib
{
	public class BASE64_Main
	{
		static readonly byte[]	base64Table = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 +/");
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

		public static String Encrypt(byte[] bPlain)
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
			return bsEnc.ToString();
		}

		public static String Encrypt(String strPlain, String coding)
		{
			byte[] bPlain = Encoding.GetEncoding(coding).GetBytes(strPlain);
			return Encrypt(bPlain);
		}

		public static String Encrypt(String strPlain, int coding)
		{
			byte[] bPlain = Encoding.GetEncoding(coding).GetBytes(strPlain);
			return Encrypt(bPlain);
		}

		public static byte[] Decode(String strEnc)
		{
			ByteString bsPlain = new ByteString();
			byte[] baEnc = Encoding.UTF8.GetBytes(strEnc);
			int current = 0, length = strEnc.Length, i = 0, bin = 0;
			byte ch;
			while (length-- > 0)
			{
				ch = baEnc[current++];
				if (ch == base64Pad)
				{
					if (current < strEnc.Length)
					{
						if (strEnc[current] != '=' && (i % 4) == 1)
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

		public static String DecodeEncoding(String strEnc, String coding)
		{
			return new String(Encoding.GetEncoding(coding).GetChars(Decode(strEnc)));
		}

		public static String DecodeEncoding(String strEnc, int coding)
		{
			return new String(Encoding.GetEncoding(coding).GetChars(Decode(strEnc)));
		}
	}
}