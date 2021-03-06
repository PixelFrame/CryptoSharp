﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BASE64_lib;
using DES_lib;
using RSA_lib;
using AES_lib;

namespace CryptoTest
{
	[TestClass]
	class Program
	{
		[TestInitialize]
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
								"5: DES_BLOCK\n" +
								"6: RSA_PKCS#1\n" +
								"7: RSA_PKCS#1_RW\n" +
								"8: AES_BASIC\n" + 
								"9: AES_BLOCK");
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
					case '6':
						Console.WriteLine("\n************TEST_START************");
						RSA_PKCS1_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '7':
						Console.WriteLine("\n************TEST_START************");
						RSA_PKCS1_RW_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '8':
						Console.WriteLine("\n************TEST_START************");
						AES_BASIC_TEST();
						Console.WriteLine("\n************TEST_FINISHED************");
						break;
					case '9':
						Console.WriteLine("\n************TEST_START************");
						AES_BLOCK_TEST();
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
			BigInteger[] RSA_key = RSA_Gen.GenKey();
			Console.WriteLine("n: {0:D}\ne: {1:D}\nd: {2:D}", RSA_key[0], RSA_key[1], RSA_key[2]);
			
			//////////////Maximum Number Test///////////////
			byte[] testNumB = new byte[65];
			for (int i = 0; i < 64; ++i) testNumB[i] = 0xFF;
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
				RSA_Gen.GenKey(3, 22);
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
			DES_lib.Mode[] modes = new DES_lib.Mode[] { DES_lib.Mode.ECB, DES_lib.Mode.CBC, DES_lib.Mode.CFB, DES_lib.Mode.OFB, DES_lib.Mode.PCBC };
			foreach (DES_lib.Mode m in modes)
			{
				byte[] baCipher = DES_Main.EncryptBlockB(baPlain, key, key, m);
				byte[] baResult = DES_Main.DecryptBlockB(baCipher, key, key, m);
				Console.WriteLine(Encoding.Unicode.GetChars(baResult));
				Console.WriteLine("**************************************************");
			}
		}
		[TestMethod]
		[ExpectedException(typeof(RSAKeyFormatErrorException))]
		static void RSA_PKCS1_TEST()
		{
			string str_key = RSA_Gen.PKCS1_Pri_Gen_S();
			Console.WriteLine(str_key);
			BigInteger[] RSA_key = RSA_Read.PKCS1_Pri_Read(str_key);
			foreach(BigInteger bi in RSA_key)
			{
				Console.WriteLine("{0:X}", bi);
			}
			string str_pub_key = RSA_Gen.PKCS1_Pub_Gen_S(str_key);
			Console.WriteLine(str_pub_key);

			Console.WriteLine("************************************************");
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
			string strCipher = RSA_Main.Encrypt_PKCS1_S(str_pub_key, strPlain, "UTF-8");
			Console.WriteLine(strCipher);
			Console.WriteLine("");
			string strResult = RSA_Main.Decrypt_PKCS1_S(str_key, strCipher, "UTF-8");
			Console.WriteLine(strResult);

			str_key = "NOT A PKCS#1 KEY.";
			try
			{
				RSA_Read.PKCS1_Pri_Read(str_key);
			}
			catch (RSAKeyFormatErrorException e)
			{
				Console.WriteLine(e.errInfo);
			}
		}
		[TestMethod]
		static void RSA_PKCS1_RW_TEST()
		{
			string strPath = @"D:\RSA_PRI.pem";
			if(!RSA_Write.PKCS1_Pri_Write(strPath))
			{
				Console.WriteLine("CAN NOT WRITE FILE");
				return;
			}
			string strPKCS1_Pri = new string(Encoding.ASCII.GetChars(File.ReadAllBytes(strPath)));
			strPath = @"D:\RSA_PUB.pem";
			if(!RSA_Write.PKCS1_Pub_Write(strPath, strPKCS1_Pri))
			{
				Console.WriteLine("CAN NOT WRITE FILE");
				return;
			}
			Console.WriteLine("SUCCESSFULLY WRITTEN FILE");
		}
		[TestMethod]
		static void AES_BASIC_TEST()
		{
			byte[] data = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10 };
			byte[] key = { 0x0f, 0x15, 0x71, 0xc9, 0x47, 0xd9, 0xe8, 0x59, 0x0c, 0xb7, 0xad, 0xd6, 0xaf, 0x7f, 0x67, 0x98 };
			byte[] cipher = AES_Main.Encrypt(data, key);
			byte[] res = AES_Main.Decrypt(cipher, key);
			Console.WriteLine("INPUT DATA: " + Encoding.ASCII.GetString(data));
			Console.WriteLine("KEY: " + Encoding.ASCII.GetString(key));
			Console.WriteLine("RESULT: " + Encoding.ASCII.GetString(res));

		}
		[TestMethod]
		static void AES_BLOCK_TEST()
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
			byte[] baKey = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
			AES_lib.Mode[] modes = new AES_lib.Mode[] { AES_lib.Mode.ECB, AES_lib.Mode.CBC, AES_lib.Mode.OFB, AES_lib.Mode.PCBC };
			foreach (AES_lib.Mode m in modes)
			{
				byte[] baCipher = AES_Main.EncryptBlock(baPlain, baKey, baKey, m);
				byte[] baResult = AES_Main.DecryptBlock(baCipher, baKey, baKey, m);
				Console.WriteLine(Encoding.Unicode.GetChars(baResult));
				Console.WriteLine("**************************************************");
			}
		}
	}
}
