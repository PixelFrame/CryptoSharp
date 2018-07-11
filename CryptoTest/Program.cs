using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BASE64_lib;
using DES_lib;
using RSA_lib;

namespace CryptoTest
{
	[TestClass]
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleKeyInfo cki;
			do
			{
				Console.WriteLine("Please Enter The Item To Be Tested: ");
				Console.WriteLine("1: BASE64\n" +
								"2: DES_BASIC\n" +
								"3: PRIME_GEN\n" +
								"4: RSA_BASIC\n" +
								"5: DES_BLOCK");
				cki = Console.ReadKey();
				switch (cki.KeyChar)
				{
					case '1':
						Console.WriteLine("\n************TEST_START************");
						BASE64_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '2':
						Console.WriteLine("\n************TEST_START************");
						DES_BASIC_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '3':
						Console.WriteLine("\n************TEST_START************");
						PRIME_GEN_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '4':
						Console.WriteLine("\n************TEST_START************");
						RSA_BASIC_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '5':
						Console.WriteLine("\n************TEST_START************");
						DES_BLOCK_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					default:
						Console.WriteLine("\n************INVALID_INPUT************"); break;
				}
			} while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Enter);
		}
		[TestMethod]
		static void BASE64_TEST()
		{
			string str = "简体中文\n繁體中文\nカタカナ\nひらがな\nEnglish\n12345";
			string strEnc = BASE64_Main.EncryptS(str, "GBK");
			Console.Out.WriteLine("GBK: " + strEnc);
			Console.Out.WriteLine(BASE64_Main.DecodeS(strEnc, "GBK"));
			strEnc = BASE64_Main.EncryptS(str, 65001);
			Console.Out.WriteLine("UTF-8: " + strEnc);
			Console.Out.WriteLine(BASE64_Main.DecodeS(strEnc, 65001));
			strEnc = BASE64_Main.EncryptS(str, "UTF-16");
			Console.Out.WriteLine("UTF-16LE: " + strEnc);
			Console.Out.WriteLine(BASE64_Main.DecodeS(strEnc, "UTF-16"));
		}
		[TestMethod]
		static void DES_BASIC_TEST()
		{
			UInt64 plain = 0x0123456789ABCDEF;
			UInt64 cipher = DES_Main.Encrypt(plain, plain);			
			Console.Out.WriteLine("{0:X16}", cipher);
			Console.Out.WriteLine("{0:X16}", DES_Main.Decrypt(cipher, plain));
		}
		[TestMethod]
		static void PRIME_GEN_TEST()
		{
			PrimeGen primeGen = new PrimeGen();
			BigInteger prime = primeGen.Gen();
			Console.Out.WriteLine(prime);
		}
		[TestMethod]
		[ExpectedException(typeof(NotPrimeNumberException))]
		static void RSA_BASIC_TEST()
		{
			////////////////Generate RSA Key////////////////
			BigInteger[] RSA_key = RSA_Main.GenKey();
			Console.WriteLine("n: {0:D}\ne: {1:D}\nd: {2:D}", RSA_key[0], RSA_key[1], RSA_key[2]);
			
			//////////////Maximum Number Test///////////////
			byte[] testNumB = new byte[65];
			for (int i = 0; i < 65; ++i) testNumB[i] = 0xFF;
			BigInteger testNumMax = new BigInteger(testNumB);
			BigInteger[] testArr = new BigInteger[] { 123456789, 0xABCDEF, 1145141919810, 0xFFFFFFFF, testNumMax };

			//////////////Encrypt and Decrypt///////////////
			foreach (BigInteger bi in testArr)
			{
				Console.WriteLine("Result: " + RSA_Main.Decrypt(RSA_Main.Encrypt(bi, RSA_key[1], RSA_key[0]), RSA_key[2], RSA_key[0]));
			}

			//////Manual Param and Not Prime Exception//////
			try
			{
				RSA_Main.GenKey(3, 22);
			}
			catch(NotPrimeNumberException e)
			{
				Console.WriteLine("\n!----------------------------------------------------------------------------------!\n" +
					"!  NotPrimeNumberException: {0:D} is not a prime number.    !" +
					"\n!----------------------------------------------------------------------------------!", e.errNum);
			}
		}
		[TestMethod]
		static void DES_BLOCK_TEST()
		{
			string strPlain = 
				"C#[b] (/si: ʃɑːrp/) is a multi-paradigm programming language " +
				"encompassing strong typing, imperative, declarative, functional," +
				" generic, object-oriented (class-based), and component-oriented " +
				"programming disciplines. It was developed around 2000 by Micros" +
				"oft within its .NET initiative and later approved as a standard" +
				" by Ecma (ECMA-334) and ISO (ISO/IEC 23270:2006). C# is one of " +
				"the programming languages designed for the Common Language Infr" +
				"astructure.C# is a general-purpose, object-oriented programming " +
				"language. Its development team is led by Anders Hejlsberg. The m" +
				"ost recent version is C# 7.3, which was released in 2018 alongsi" +
				"de Visual Studio 2017 version 15.7.2.";
			byte[] baPlain = Encoding.Unicode.GetBytes(strPlain);
			ulong key = 0x0123456789ABCDEF;
			Mode[] modes = new Mode[] { Mode.ECB, Mode.CBC, Mode.CFB, Mode.OFB };
			foreach (Mode m in modes)
			{
				byte[] baCipher = DES_Main.EncryptBlockB(baPlain, key, key, m);
				byte[] baResult = DES_Main.DecryptBlockB(baCipher, key, key, m);
				Console.WriteLine(Encoding.Unicode.GetChars(baResult));
				Console.WriteLine("**************************************************");
			}
		}
	}
}
