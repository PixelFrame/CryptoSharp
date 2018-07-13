using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace RSA_lib
{
	/// <summary>
	/// 明文数据分割，每127-byte(1016-bit)为一组，保证不会大于n(1024-bit)
	/// </summary>
	class RSA_Plain
	{
		public List<BigInteger> bilData;
		public RSA_Plain(string strData, string strCoding)
		{
			bilData = new List<BigInteger>();
			byte[] baData = Encoding.GetEncoding(strCoding).GetBytes(strData);
			int rem = baData.Length % 127;
			int round = (rem == 0) ? (baData.Length / 127) : (baData.Length / 127 + 1);
			for (int i = 0; i < round; ++i)
			{
				if (i != round - 1)
				{
					byte[] baToken = new byte[128];
					for (int j = 0; j < 127; ++j)
					{
						baToken[j] = baData[i * 127 + j];
					}
					bilData.Add(new BigInteger(baToken));
				}
				else
				{
					byte[] baToken = new byte[rem+1];
					for (int j = 0; j < rem; ++j)
					{
						baToken[j] = baData[i * 127 + j];
					}
					bilData.Add(new BigInteger(baToken));
				}
			}
		}
	}
	/// <summary>
	/// 密文数据，指定字符串分割符，默认为"&"
	/// </summary>
	class RSA_Cipher
	{
		public List<BigInteger> bilData;
		public RSA_Cipher(string strData, char chSplit = '&')
		{
			bilData = new List<BigInteger>();
			string[] splitData = strData.Split(chSplit);
			foreach (string strToken in splitData)
			{
				bilData.Add(BigInteger.Parse(strToken));
			}
		}
	}
}
