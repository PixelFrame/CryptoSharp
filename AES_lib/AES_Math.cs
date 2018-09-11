using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WORD = System.UInt32;

namespace AES_lib
{
	class AES_Math
	{
		public static byte Multiply_GF2_8(byte a, byte b)
		{
			byte[] temp = new byte[]{ a, a, a, a, a, a, a, a };
			byte tempmultiply = 0x00;
			int i = 0;
			for (i = 1; i < 8; i++)
			{
				temp[i] = XTIME(temp[i - 1]);
			}
			tempmultiply = (byte)((b & 0x01) * a);
			for (i = 1; i <= 7; i++)
			{
				tempmultiply ^= (byte)(((b >> i) & 0x01) * temp[i]);
			}
			return tempmultiply;
		}

		public static byte Multiply_GF2_8(WORD wParam0, WORD wParam1)
		{
			byte[] baParam0 = BitConverter.GetBytes(wParam0);
			byte[] baParam1 = BitConverter.GetBytes(wParam1);
			byte res = 0;
			for (int i = 0; i < 4; ++i)
			{
				res ^= Multiply_GF2_8(baParam0[i], baParam1[i]);
			}
			return res;
		}

		static byte XTIME(byte x)
		{
			return (byte)((x << 1) ^ ((x & 0x80) != 0 ? 0x1b : 0x00));
		}
	}
}
