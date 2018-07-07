using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QWORD = System.UInt64;
using DWORD = System.UInt32;

namespace DES_lib
{
    public class DES_Main
    {
		public static QWORD Encrypt(QWORD qwPlain, QWORD qwKey)
		{
			QWORD L0R0 = DES_Table.Swap_IP(qwPlain);
			QWORD T = DES_Table.Swap_PC1(qwKey);
			DWORD C = (DWORD)((T & 0xFFFFFFF000000000) >> 32);
			DWORD D = (DWORD)((T & 0x0000000FFFFFFF00) >> 4);
			DWORD L = (DWORD)(L0R0 >> 32);
			DWORD R = (DWORD)(L0R0);
			int[] shiftTable =
			{
				1, 1, 2, 2,
				2, 2, 2, 2,
				1, 2, 2, 2,
				2, 2, 2, 1
			};
			for (int round = 0; round < 16; ++round)
			{
				C = DES_Convert.RoundShiftLeft28(C, shiftTable[round]);
				D = DES_Convert.RoundShiftLeft28(D, shiftTable[round]);
				T = ((QWORD)(C & 0xFFFFFFF0) << 32) | ((QWORD)D << 4);
				QWORD Ki = DES_Table.Swap_PC2(T);
				DWORD temp = R;
				R = DES_Table.Swap_P(DES_Table.Swap_S(DES_Table.Expand_R(R) ^ Ki)) ^ L;
				L = temp;
			}
			QWORD R16L16 = ((QWORD)R << 32) | (QWORD)L;
			QWORD qwCipher = DES_Table.Swap_T_IP(R16L16);
			return qwCipher;
		}

		public static QWORD Decrypt(QWORD qwCipher, QWORD qwKey)
		{
			QWORD L16R16 = DES_Table.Swap_IP(qwCipher);
			DWORD L = (DWORD) L16R16;
			DWORD R = (DWORD) (L16R16 >> 32);
			QWORD T = DES_Table.Swap_PC1(qwKey);
			DWORD C = (DWORD) ((T & 0xFFFFFFF000000000) >> 32);
			DWORD D = (DWORD) ((T & 0x0000000FFFFFFF00) >> 4);
			int[] shiftTable =
			{
				1, 1, 2, 2,
				2, 2, 2, 2,
				1, 2, 2, 2,
				2, 2, 2, 1
			};
			List<QWORD> lsKey = new List<QWORD>();
			for (int round = 0; round < 16; ++round)
			{
				C = DES_Convert.RoundShiftLeft28(C, shiftTable[round]);
				D = DES_Convert.RoundShiftLeft28(D, shiftTable[round]);
				T = ((QWORD)(C & 0xFFFFFFF0) << 32) | ((QWORD)D << 4);
				lsKey.Add(DES_Table.Swap_PC2(T));
			}
			for (int round = 15; round >= 0; --round)
			{
				DWORD temp = R;
				R = L;
				L = DES_Table.Swap_P(DES_Table.Swap_S(DES_Table.Expand_R(R) ^ lsKey[round])) ^ temp;
			}
			QWORD L0R0 = ((QWORD)L << 32) | (QWORD)R;
			QWORD qwPlain = DES_Table.Swap_T_IP(L0R0);
			return qwPlain;
		}
    }
}
