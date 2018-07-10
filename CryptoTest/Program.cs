using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using BASE64_lib;
using DES_lib;
using RSA_lib;

namespace CryptoTest
{
	class Program
	{
		static void Main(string[] args)
		{
			RSA_BASIC_TEST();
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

		static void RSA_BASIC_TEST()
		{
			BigInteger[] RSA_key = RSA_Main.GenKey();
			Console.WriteLine("n: {0:D}\te: {0:D}\td: {0:D}", RSA_key[0], RSA_key[1], RSA_key[2]);
			BigInteger[] testArr = new BigInteger[] { 10000000, 111111111, 2222222222, 9999999 };
			foreach (BigInteger bi in testArr)
			{
				Console.WriteLine(RSA_Main.Decrypt(RSA_Main.Encrypt(bi, RSA_key[1], RSA_key[0]), RSA_key[2], RSA_key[0]));
			}
		}
	}
}
