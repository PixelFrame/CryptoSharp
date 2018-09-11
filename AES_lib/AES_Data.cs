using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ByteString_lib;
using WORD = System.UInt32;

namespace AES_lib
{
	/// <summary>
	/// AES-128数据类型，使用4个UInt32保存数据
	/// </summary>
	class AES_Data_128
	{
		public WORD AES_Data_0;
		public WORD AES_Data_1;
		public WORD AES_Data_2;
		public WORD AES_Data_3;
		private static readonly WORD[] MixMatrix = { 0x02030101, 0x01020301, 0x01010203, 0x03010102 };
		private static readonly WORD[] MixMatrix_T = { 0x0E0B0D09, 0x090E0B0D, 0x0D090E0B, 0x0B0D090E };
		/// <summary>
		/// 使用byte数组的构造方法
		/// </summary>
		/// <param name="baData"></param>
		public AES_Data_128(byte[] baData)
		{
			byte[] baToken = new byte[4];
			Array.ConstrainedCopy(baData, 0, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_0 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 4, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_1 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 8, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_2 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 12, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_3 = BitConverter.ToUInt32(baToken, 0);
		}
		/// <summary>
		/// 复制构造方法
		/// </summary>
		/// <param name="aesData"></param>
		public AES_Data_128(AES_Data_128 aesData)
		{
			AES_Data_0 = aesData.AES_Data_0;
			AES_Data_1 = aesData.AES_Data_1;
			AES_Data_2 = aesData.AES_Data_2;
			AES_Data_3 = aesData.AES_Data_3;
		}
		/// <summary>
		/// 通过byte数组更新数据
		/// </summary>
		/// <param name="baData"></param>
		private void UpdateData(byte[] baData)
		{
			byte[] baToken = new byte[4];
			Array.ConstrainedCopy(baData, 0, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_0 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 4, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_1 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 8, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_2 = BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baData, 12, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Data_3 = BitConverter.ToUInt32(baToken, 0);
		}
		/// <summary>
		/// 将数据转换为byte数组输出
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			ByteString bsData = new ByteString();
			bsData += BitConverter.GetBytes(AES_Data_0).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_1).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_2).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_3).Reverse().ToArray();
			return bsData.GetBytes();
		}
		/// <summary>
		/// 将数据转换为ByteString
		/// </summary>
		/// <returns></returns>
		private ByteString GetByteString()
		{
			ByteString bsData = new ByteString();
			bsData += BitConverter.GetBytes(AES_Data_0).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_1).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_2).Reverse().ToArray();
			bsData += BitConverter.GetBytes(AES_Data_3).Reverse().ToArray();
			return bsData;
		}
		/// <summary>
		/// 字节替换
		/// </summary>
		public void Swap_S()
		{
			AES_Data_0 = AES_Table.Swap_S(AES_Data_0);
			AES_Data_1 = AES_Table.Swap_S(AES_Data_1);
			AES_Data_2 = AES_Table.Swap_S(AES_Data_2);
			AES_Data_3 = AES_Table.Swap_S(AES_Data_3);
		}
		/// <summary>
		/// 反字节替换
		/// </summary>
		public void Swap_S_T()
		{
			AES_Data_0 = AES_Table.Swap_S_T(AES_Data_0);
			AES_Data_1 = AES_Table.Swap_S_T(AES_Data_1);
			AES_Data_2 = AES_Table.Swap_S_T(AES_Data_2);
			AES_Data_3 = AES_Table.Swap_S_T(AES_Data_3);
		}
		/// <summary>
		/// 重载+，供密钥相加使用
		/// </summary>
		/// <param name="aesParam"></param>
		/// <param name="wlParam"></param>
		/// <returns></returns>
		public static AES_Data_128 operator + (AES_Data_128 aesParam, List<WORD> wlParam)
		{
			aesParam.AES_Data_0 ^= wlParam[0];
			aesParam.AES_Data_1 ^= wlParam[1];
			aesParam.AES_Data_2 ^= wlParam[2];
			aesParam.AES_Data_3 ^= wlParam[3];
			return aesParam;
		}
		/// <summary>
		/// 行移位
		/// </summary>
		public void RowSwap()
		{
			ByteString bsData = GetByteString();

			ByteString bsRes = new ByteString();
			bsRes += bsData[0];
			bsRes += bsData[5];
			bsRes += bsData[10];
			bsRes += bsData[15];
			bsRes += bsData[4];
			bsRes += bsData[9];
			bsRes += bsData[14];
			bsRes += bsData[3];
			bsRes += bsData[8];
			bsRes += bsData[13];
			bsRes += bsData[2];
			bsRes += bsData[7];
			bsRes += bsData[12];
			bsRes += bsData[1];
			bsRes += bsData[6];
			bsRes += bsData[11];

			UpdateData(bsRes.GetBytes());
		}
		/// <summary>
		/// 反行移位
		/// </summary>
		public void RowSwap_T()
		{
			ByteString bsData = GetByteString();

			ByteString bsRes = new ByteString();
			bsRes += bsData[0];
			bsRes += bsData[13];
			bsRes += bsData[10];
			bsRes += bsData[7];
			bsRes += bsData[4];
			bsRes += bsData[1];
			bsRes += bsData[14];
			bsRes += bsData[11];
			bsRes += bsData[8];
			bsRes += bsData[5];
			bsRes += bsData[2];
			bsRes += bsData[15];
			bsRes += bsData[12];
			bsRes += bsData[9];
			bsRes += bsData[6];
			bsRes += bsData[3];

			UpdateData(bsRes.GetBytes());
		}
		/// <summary>
		/// 列混淆
		/// </summary>
		public void ColMix()
		{
			ByteString baRes = new ByteString();
			foreach(WORD wMixToken in MixMatrix)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_0, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_1, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_2, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_3, wMixToken);
			}
			UpdateData(baRes.GetBytes());
		}
		/// <summary>
		/// 反列混淆
		/// </summary>
		public void ColMix_T()
		{
			ByteString baRes = new ByteString();
			foreach (WORD wMixToken in MixMatrix_T)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_0, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix_T)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_1, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix_T)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_2, wMixToken);
			}
			foreach (WORD wMixToken in MixMatrix_T)
			{
				baRes += AES_Math.Multiply_GF2_8(AES_Data_3, wMixToken);
			}
			UpdateData(baRes.GetBytes());
		}
		/// <summary>
		/// 重载^，供部分操作模式中使用
		/// </summary>
		/// <param name="aesParam"></param>
		/// <param name="baParam"></param>
		/// <returns></returns>
		public static AES_Data_128 operator ^ (AES_Data_128 aesParam, byte[] baParam)
		{
			byte[] baToken = new byte[4];
			Array.ConstrainedCopy(baParam, 0, baToken, 0, 4);
			Array.Reverse(baToken);
			aesParam.AES_Data_0 ^= BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baParam, 4, baToken, 0, 4);
			Array.Reverse(baToken);
			aesParam.AES_Data_1 ^= BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baParam, 8, baToken, 0, 4);
			Array.Reverse(baToken);
			aesParam.AES_Data_2 ^= BitConverter.ToUInt32(baToken, 0);
			Array.ConstrainedCopy(baParam, 12, baToken, 0, 4);
			Array.Reverse(baToken);
			aesParam.AES_Data_3 ^= BitConverter.ToUInt32(baToken, 0);
			return aesParam;
		}

	}

	/// <summary>
	/// AES-128密钥数据类型，44个轮密钥使用List&lt;UInt32&rt;保存
	/// </summary>
	class AES_Key_128
	{
		private readonly static WORD[] Rcon = { 0x01000000, 0x02000000,
												0x04000000, 0x08000000,
												0x10000000, 0x20000000,
												0x40000000, 0x80000000,
												0x1B000000, 0x36000000 };
		public List<WORD> AES_Key = new List<WORD>();
		public AES_Key_128(byte[] baKey)
		{
			WORD temp;
			byte[] baToken = new byte[4];
			Array.ConstrainedCopy(baKey, 0, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Key.Add(BitConverter.ToUInt32(baToken, 0));
			Array.ConstrainedCopy(baKey, 4, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Key.Add(BitConverter.ToUInt32(baToken, 0));
			Array.ConstrainedCopy(baKey, 8, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Key.Add(BitConverter.ToUInt32(baToken, 0));
			Array.ConstrainedCopy(baKey, 12, baToken, 0, 4);
			Array.Reverse(baToken);
			AES_Key.Add(BitConverter.ToUInt32(baToken, 0));

			for (int i = 4; i < 44; ++i)
			{
				temp = AES_Key[i - 1];
				if (i % 4 == 0) temp = G_Conversion(temp, i / 4 - 1);
				temp ^= AES_Key[i - 4];
				AES_Key.Add(temp);
			}
		}
		/// <summary>
		/// 循环左移1byte
		/// </summary>
		/// <param name="wParam"></param>
		/// <returns></returns>
		private WORD RoundShiftLeft1Byte(WORD wParam)
		{
			return (wParam << 8) ^ (wParam >> 24);
		}
		/// <summary>
		/// 轮密钥产生 g变换
		/// </summary>
		/// <param name="wParam"></param>
		/// <param name="round"></param>
		/// <returns></returns>
		private WORD G_Conversion(WORD wParam, int round)
		{
			wParam = RoundShiftLeft1Byte(wParam);
			return AES_Table.Swap_S(wParam) ^ Rcon[round];
		}
	}
}
