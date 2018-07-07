using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteString_lib
{
    public class ByteString
    {
		private List<byte> _byteStr;

		public ByteString()
		{
			_byteStr = new List<byte>();
		}

		public ByteString(byte[] byteStr)
		{
			_byteStr = new List<byte>(byteStr);
		}

		public static ByteString operator + (ByteString bsParam0, ByteString bsParam1)
		{
			bsParam0._byteStr.AddRange(bsParam1._byteStr);
			return bsParam0;
		}

		public static ByteString operator + (ByteString bsParam0, byte bParam1)
		{
			bsParam0._byteStr.Add(bParam1);
			return bsParam0;
		}

		public static ByteString operator + (ByteString bsParam0, byte[] baParam1)
		{
			bsParam0._byteStr.AddRange(baParam1);
			return bsParam0;
		}

		public byte this[int index]
		{
			get
			{
				return _byteStr[index];
			}
			set
			{
				_byteStr[index] = value;
			}
		}

		public void SetBytes(byte[] byteStr)
		{
			_byteStr.Clear();
			_byteStr.AddRange(byteStr);
		}

		public byte[] GetBytes()
		{
			return _byteStr.ToArray();
		}

		public override string ToString()
		{
			return new string(Encoding.UTF8.GetChars(_byteStr.ToArray()));
		}
	}
}
