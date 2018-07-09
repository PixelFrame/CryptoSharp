using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BASE64_lib;
using DES_lib;
using RSA_lib;

namespace CryptoTest
{
	class Program
	{
		static void Main(string[] args)
		{
			PRIME_GEN_TEST();
			Console.ReadKey();
		}

		static void BASE64_TEST()
		{
			string str = "中文\nEnglish\n12345";
			string strEnc = BASE64_Main.Encrypt(str, "BIG5");
			Console.Out.WriteLine(strEnc);
			Console.Out.WriteLine(BASE64_Main.DecodeEncoding(strEnc, "BIG5"));
		}

		static void DES_TEST()
		{
			UInt64 plain = 0x0123456789ABCDEF;
			UInt64 cipher = DES_Main.Encrypt(plain, plain);			
			Console.Out.WriteLine("{0:X16}", cipher);
			Console.Out.WriteLine("{0:X16}", DES_Main.Decrypt(cipher, plain));
		}

		static void PRIME_GEN_TEST()
		{
			PrimeGen primeGen = new PrimeGen();
			ulong prime = primeGen.Gen();
			Console.Out.WriteLine(prime);
		}
	}
}
