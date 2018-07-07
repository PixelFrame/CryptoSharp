using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ByteString_lib;

namespace AES_lib
{
	class AES_Data
	{
		private ByteString _AES_Data_0 = new ByteString();
		private ByteString _AES_Data_1 = new ByteString();
		private ByteString _AES_Data_2 = new ByteString();
		private ByteString _AES_Data_3 = new ByteString();

		public AES_Data(byte[] baData)
		{
			for (int i = 0; i < baData.Length; ++i)
			{
				if		(i < 4)		_AES_Data_0 += baData[i];
				else if (i < 8)		_AES_Data_1 += baData[i];
				else if (i < 12)	_AES_Data_2 += baData[i];
				else if (i < 16)	_AES_Data_3 += baData[i];
			}
		}

		public byte[] GetData()
		{
			return (_AES_Data_0 + _AES_Data_1 + _AES_Data_2 + _AES_Data_3).GetBytes();
		}
	}
}
