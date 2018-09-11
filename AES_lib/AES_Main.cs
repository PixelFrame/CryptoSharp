using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteString_lib;

using WORD = System.UInt32;

namespace AES_lib
{
    public class AES_Main
    {
		public static AES_Data_128 Encrypt(AES_Data_128 aesPlain, AES_Key_128 aesKey)
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

		public static AES_Data_128 Decrypt(AES_Data_128 aesCipher, AES_Key_128 aesKey)
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
	}
}
