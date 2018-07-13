using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using ByteString_lib;

namespace RSA_lib
{
	/// <summary>
	/// ASN.1转换类
	/// </summary>
	class ASN1_Convert
	{
		/// <summary>
		/// int转ASN.1 byte数组
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public ByteString IntToBytes(int i)
		{
			byte[] num = IntByteConvert(BitConverter.GetBytes(i));
			byte[] len = IntByteConvert(BitConverter.GetBytes(num.Length));
			byte[] prefix = { 0x02 };
			ByteString asn = new ByteString(prefix);
			asn += len; asn += num;
			return asn;
		}
		/// <summary>
		/// BigInteger转ASN.1 byte数组
		/// </summary>
		/// <param name="bi"></param>
		/// <returns></returns>
		public ByteString BigIntToBytes(BigInteger bi)
		{
			byte[] num = IntByteConvert(bi.ToByteArray());
			byte[] len = IntByteConvert(BitConverter.GetBytes(num.Length + 1));
			byte lenLen = (byte)(len.Length + 0x80);
			byte[] prefix = { 0x02 };
			byte[] signPrefix = { 0x00 };
			ByteString asn = new ByteString(prefix);
			asn += lenLen; asn += len; asn += signPrefix; asn += num;
			return asn;
		}
		/// <summary>
		/// BigInteger转ASN.1 byte数组，指定数组长度，如果长度小于bi长度则无效
		/// </summary>
		/// <param name="bi"></param>
		/// <param name="targetLen"></param>
		/// <returns></returns>
		public ByteString BigIntToBytes(BigInteger bi, int targetLen)
		{
			byte[] num = IntByteConvert(bi.ToByteArray(), targetLen);
			byte[] len = IntByteConvert(BitConverter.GetBytes(num.Length + 1));
			byte lenLen = (byte)(len.Length + 0x80);
			byte[] prefix = { 0x02 };
			byte[] signPrefix = { 0x00 };
			ByteString asn = new ByteString(prefix);
			asn += lenLen; asn += len; asn += signPrefix; asn += num;
			return asn;
		}
		/// <summary>
		/// byte序列添加序列头0x30
		/// </summary>
		/// <param name="seq"></param>
		/// <returns></returns>
		public ByteString SeqToBytes(byte[] seq)
		{
			byte[] prefix = { 0x30 };
			byte[] len = IntByteConvert(BitConverter.GetBytes(seq.Length));
			byte lenLen = (byte)(len.Length + 0x80);
			ByteString asn = new ByteString(prefix);
			asn += lenLen; asn += len; asn += seq;
			return asn;
		}
		/// <summary>
		/// Little Endian转Big Endian
		/// </summary>
		/// <param name="baLen"></param>
		/// <returns></returns>
		private byte[] IntByteConvert(byte[] baLen)
		{
			ByteString bsConv = new ByteString();
			int len = baLen.Length;
			bool isFirst = true;
			while (len-- > 0)
			{
				if (isFirst && baLen[len] == 0x00) continue;
				else
				{
					bsConv += baLen[len];
					isFirst = false;
				}
			}
			if (bsConv.GetLength() == 0) bsConv += 0;
			return bsConv.GetBytes();
		}
		/// <summary>
		/// Little Endian转Big Endian，指定转换后长度
		/// </summary>
		/// <param name="baLen"></param>
		/// <param name="targetLen"></param>
		/// <returns></returns>
		private byte[] IntByteConvert(byte[] baLen, int targetLen)
		{
			ByteString bsConv = new ByteString(IntByteConvert(baLen));
			int addLen = targetLen - bsConv.GetLength();
			if (addLen < 0) return bsConv.GetBytes();
			while (addLen-- > 0)
			{
				ByteString bsZero = new ByteString();
				bsZero += 0x00;
				bsConv =  bsZero + bsConv;
			}
			return bsConv.GetBytes();
		}
	}
}
