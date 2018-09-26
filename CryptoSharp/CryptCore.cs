using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES_lib;
using DES_lib;
using BASE64_lib;
using RSA_lib;

namespace CryptoSharp
{
	class CryptCore
	{
		public byte[] DES_Crypt(bool action, byte[] baData, ulong ulKey, ulong ulIV, int mode)
		{
			if (action) return DES_Main.DecryptBlockB(baData, ulKey, ulIV, (DES_lib.Mode)mode);
			else return DES_Main.EncryptBlockB(baData, ulKey, ulIV, (DES_lib.Mode)mode);
		}

		public byte[] AES_Crypt(bool action, byte[] baData, byte[] baKey, byte[] baIV, int mode)
		{
			if (action) return AES_Main.DecryptBlock(baData, baKey, baIV, (AES_lib.Mode)mode);
			else return AES_Main.EncryptBlock(baData, baKey, baIV, (AES_lib.Mode)mode);
		}

		public string BASE64_Convert(bool action, string strData)
		{
			if (action) return BASE64_Main.DecodeS(strData, "UTF-8");
			else return BASE64_Main.EncryptS(strData, "UTF-8");
		}

		public string BASE64_Convert(bool action, byte[] baData)
		{
			if (action) return BASE64_Main.DecodeS(baData, "UTF-8");
			else return BASE64_Main.EncryptS(baData, "UTF-8");
		}
	}
}
