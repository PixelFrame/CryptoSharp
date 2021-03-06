﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteString_lib;

using QWORD = System.UInt64;
using DWORD = System.UInt32;

namespace DES_lib
{
	/// <summary>
	/// 分组操作模式
	///		0 ECB 电码本
	///		1 CBC 密文分组链接
	///		2 CFB 密文反馈
	///		3 OFB 输出反馈
	///		4 PCBC 扩散密文分组链接
	///		5 CTR 计数器(未实现)
	/// </summary>
	public enum Mode { ECB = 0, CBC = 1, CFB = 2, OFB = 3, PCBC = 4, CTR = 5};
	/// <summary>
	/// DES加解密主要类
	/// </summary>
	public class DES_Main
    {
		/// <summary>
		/// DES主要加密方法 输入输出64位
		/// </summary>
		/// <param name="qwPlain">64bit明文</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <returns>64bit密文</returns>
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
		/// <summary>
		/// DES主要解密方法 输入输出64位
		/// </summary>
		/// <param name="qwCipher">64bit密文</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <returns>64bit明文</returns>
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
		/// <summary>
		/// DES分组加密方法 输入byte数组 返回ulong数组 CFB模式返回ulong的有效位数仅为8位
		/// </summary>
		/// <param name="baPlain">明文byte数组</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <param name="qwIV">64bit初始化向量，ECB模式无效</param>
		/// <param name="mode">操作模式</param>
		/// <returns>密文byte数组</returns>
		public static QWORD[] EncryptBlockQ(byte[] baPlain, QWORD qwKey, QWORD qwIV, Mode mode)
		{
			List<QWORD> qwlCipher = new List<QWORD>();
			ByteString bsPlain = new ByteString(baPlain);
			int part, rem, count;
			switch(mode)
			{
				case Mode.ECB:
					part = (int)Math.Ceiling(bsPlain.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsPlain.GetLength() % 8);
					bsPlain += new byte[rem];
					while(count > 0)
					{
						QWORD token = BitConverter.ToUInt64(bsPlain.GetBytes(), (part - count) * 8);
						qwlCipher.Add(Encrypt(token, qwKey));
						--count;
					}
					break;
				case Mode.CBC:
					part = (int)Math.Ceiling(bsPlain.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsPlain.GetLength() % 8);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						QWORD token = BitConverter.ToUInt64(bsPlain.GetBytes(), (part - count) * 8) ^ qwIV;
						qwIV = Encrypt(token, qwKey);
						qwlCipher.Add(qwIV);
						--count;
					}
					break;
				case Mode.CFB:
					foreach(byte token in baPlain)
					{
						QWORD qwEnc = Encrypt(qwIV, qwKey);
						QWORD bEnc = (qwEnc >> 56) ^ token;
						qwIV = (qwIV << 8) ^ bEnc;
						qwlCipher.Add(bEnc);
					}
					break;
				case Mode.OFB:
					part = (int)Math.Ceiling(bsPlain.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsPlain.GetLength() % 8);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						qwIV = Encrypt(qwIV, qwKey);
						QWORD token = BitConverter.ToUInt64(bsPlain.GetBytes(), (part - count) * 8) ^ qwIV;
						qwlCipher.Add(token);
						--count;
					}
					break;
				case Mode.PCBC:
					part = (int)Math.Ceiling(bsPlain.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsPlain.GetLength() % 8);
					bsPlain += new byte[rem];
					while (count > 0)
					{
						QWORD qwPlain = BitConverter.ToUInt64(bsPlain.GetBytes(), (part - count) * 8);
						qwIV = Encrypt(qwPlain ^ qwIV, qwKey);
						qwlCipher.Add(qwIV);
						qwIV ^= qwPlain;
						--count;
					}
					break;
			}
			return qwlCipher.ToArray();
		}
		/// <summary>
		/// DES分组加密方法 输入byte数组 返回byte数组
		/// </summary>
		/// <param name="baPlain">输入byte数组</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <param name="qwIV">64bit初始化向量，ECB模式无效</param>
		/// <param name="mode">操作模式</param>
		/// <returns>密文byte数组</returns>
		public static byte[] EncryptBlockB(byte[] baPlain, QWORD qwKey, QWORD qwIV, Mode mode)
		{
			if (mode == Mode.CFB) return DES_Convert.QWORDToBytes_CFB(EncryptBlockQ(baPlain, qwKey, qwIV, mode));
			return DES_Convert.QWORDToBytes(EncryptBlockQ(baPlain, qwKey, qwIV, mode));
		}
		/// <summary>
		/// DES分组解密方法 输入byte数组 输出ulong数组 CFB模式返回ulong的有效位数仅为8位
		/// </summary>
		/// <param name="baCipher">明文byte数组</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <param name="qwIV">64bit初始化向量，ECB模式无效</param>
		/// <param name="mode">操作模式</param>
		/// <returns>密文ulong数组</returns>
		public static QWORD[] DecryptBlockQ(byte[] baCipher, QWORD qwKey, QWORD qwIV, Mode mode)
		{
			List<QWORD> qwlPlain = new List<QWORD>();
			ByteString bsCipher = new ByteString(baCipher);
			int part, rem, count;
			switch(mode)
			{
				case Mode.ECB:
					part = (int)Math.Ceiling(bsCipher.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsCipher.GetLength() % 8);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						QWORD token = BitConverter.ToUInt64(bsCipher.GetBytes(), (part - count) * 8);
						qwlPlain.Add(Decrypt(token, qwKey));
						--count;
					}
					break;
				case Mode.CBC:
					part = (int)Math.Ceiling(bsCipher.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsCipher.GetLength() % 8);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						QWORD token = BitConverter.ToUInt64(bsCipher.GetBytes(), (part - count) * 8);
						QWORD qwPlain = qwIV ^ Decrypt(token, qwKey);
						qwlPlain.Add(qwPlain);
						qwIV = token;
						--count;
					}
					break;
				case Mode.CFB:
					foreach (byte token in baCipher)
					{
						QWORD qwEnc = Encrypt(qwIV, qwKey);
						QWORD bEnc = (qwEnc >> 56) ^ token;
						qwIV = (qwIV << 8) ^ token;
						qwlPlain.Add(bEnc);
					}
					break;
				case Mode.OFB:
					part = (int)Math.Ceiling(bsCipher.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsCipher.GetLength() % 8);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						qwIV = Encrypt(qwIV, qwKey);
						QWORD token = BitConverter.ToUInt64(bsCipher.GetBytes(), (part - count) * 8) ^ qwIV;
						qwlPlain.Add(token);
						--count;
					}
					break;
				case Mode.PCBC:
					part = (int)Math.Ceiling(bsCipher.GetLength() / 8.0);
					count = part;
					rem = 8 - (bsCipher.GetLength() % 8);
					bsCipher += new byte[rem];
					while (count > 0)
					{
						QWORD token = BitConverter.ToUInt64(bsCipher.GetBytes(), (part - count) * 8);
						QWORD qwPlain = qwIV ^ Decrypt(token, qwKey);
						qwlPlain.Add(qwPlain);
						qwIV = token ^ qwPlain;
						--count;
					}
					break;
			}
			return qwlPlain.ToArray();
		}
		/// <summary>
		/// DES分组解密方法 输入byte数组 返回byte数组
		/// </summary>
		/// <param name="baCipher">明文byte数组</param>
		/// <param name="qwKey">64bit密钥</param>
		/// <param name="qwIV">64bit初始化向量，ECB模式无效</param>
		/// <param name="mode">操作模式</param>
		/// <returns>密文byte数组</returns>
		public static byte[] DecryptBlockB(byte[] baCipher, QWORD qwKey, QWORD qwIV, Mode mode)
		{
			if (mode == Mode.CFB) return DES_Convert.QWORDToBytes_CFB(DecryptBlockQ(baCipher, qwKey, qwIV, mode));
			return DES_Convert.QWORDToBytes(DecryptBlockQ(baCipher, qwKey, qwIV, mode));
		}
	}
}
