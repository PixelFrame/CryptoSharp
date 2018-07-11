using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using ByteString_lib;

namespace RSA_lib
{
	class ASN1_Convert
	{
		public ByteString IntToBytes(int i)
		{
			byte[] num = IntByteConvert(BitConverter.GetBytes(i));
			byte[] len = IntByteConvert(BitConverter.GetBytes(num.Length));
			byte[] prefix = { 0x02 };
			ByteString asn = new ByteString(prefix);
			asn += len; asn += num;
			return asn;
		}

		public ByteString BigIntToBytes(BigInteger bi)
		{
			byte[] num = IntByteConvert(bi.ToByteArray());
			byte[] len = IntByteConvert(BitConverter.GetBytes(num.Length));
			byte lenLen = (byte)(len.Length + 0x80);
			byte[] prefix = { 0x02 };
			ByteString asn = new ByteString(prefix);
			asn += lenLen; asn += len; asn += num;
			return asn;
		}

		public ByteString SeqToBytes(byte[] seq)
		{
			byte[] prefix = { 0x30 };
			byte[] len = IntByteConvert(BitConverter.GetBytes(seq.Length));
			byte lenLen = (byte)(len.Length + 0x80);
			ByteString asn = new ByteString(prefix);
			asn += lenLen; asn += len; asn += seq;
			return asn;
		}

		private byte[] IntByteConvert(byte[] baLen)
		{
			ByteString bsConv = new ByteString();
			int len = baLen.Length;
			bool isFirst = true;
			while (len-- > 0)
			{
				if (isFirst && baLen[len] == 0x00) continue;
				else bsConv += baLen[len];
			}
			if (bsConv.GetLength() == 0) bsConv += 0;
			return bsConv.GetBytes();
		}
	}
}
