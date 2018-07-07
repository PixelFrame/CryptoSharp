using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QWORD = System.UInt64;
using DWORD = System.UInt32;

namespace DES_lib
{
    class DES_Convert
    {
		public QWORD BinStringToQWORD(String str)
		{
			QWORD output = 0;
			for (int i = 0; i < str.Length; ++i)
			{
				if (str[i] == '1') output ^= ((QWORD)0x1 << (63 - i));
			}
			return output;
		}

		public QWORD HexStringToQWORD(String str)
		{
			QWORD output = 0;
			for (int i = 0; i < str.Length; ++i)
			{
				if (str[i] == '1') output ^= ((QWORD)0x1 << ((15 - i) * 4));
				else if (str[i] == '2') output ^= ((QWORD)0x2 << ((15 - i) * 4));
				else if (str[i] == '3') output ^= ((QWORD)0x3 << ((15 - i) * 4));
				else if (str[i] == '4') output ^= ((QWORD)0x4 << ((15 - i) * 4));
				else if (str[i] == '5') output ^= ((QWORD)0x5 << ((15 - i) * 4));
				else if (str[i] == '6') output ^= ((QWORD)0x6 << ((15 - i) * 4));
				else if (str[i] == '7') output ^= ((QWORD)0x7 << ((15 - i) * 4));
				else if (str[i] == '8') output ^= ((QWORD)0x8 << ((15 - i) * 4));
				else if (str[i] == '9') output ^= ((QWORD)0x9 << ((15 - i) * 4));
				else if (str[i] == 'A' || str[i] == 'a') output ^= ((QWORD)0xa << ((15 - i) * 4));
				else if (str[i] == 'B' || str[i] == 'b') output ^= ((QWORD)0xb << ((15 - i) * 4));
				else if (str[i] == 'C' || str[i] == 'c') output ^= ((QWORD)0xc << ((15 - i) * 4));
				else if (str[i] == 'D' || str[i] == 'd') output ^= ((QWORD)0xd << ((15 - i) * 4));
				else if (str[i] == 'E' || str[i] == 'e') output ^= ((QWORD)0xe << ((15 - i) * 4));
				else if (str[i] == 'F' || str[i] == 'f') output ^= ((QWORD)0xf << ((15 - i) * 4));
			}
			return output;
		}

		public static DWORD RoundShiftLeft28(DWORD CD, int shift)
		{
			DWORD res = CD << shift;
			res |= CD >> (28 - shift);
			return res;
		}
	}
}
