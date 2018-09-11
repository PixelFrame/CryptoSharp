using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteString_lib
{
	/// <summary>
	/// Byte字符串类
	/// </summary>
    public class ByteString
    {
		/// <summary>
		/// 数据保存
		/// </summary>
		private List<byte> _byteStr;
		/// <summary>
		/// 默认构造方法
		/// </summary>
		public ByteString()
		{
			_byteStr = new List<byte>();
		}
		/// <summary>
		/// 通过byte数组构造方法
		/// </summary>
		/// <param name="byteStr"></param>
		public ByteString(byte[] byteStr)
		{
			_byteStr = new List<byte>(byteStr);
		}
		/// <summary>
		/// 指定初始长度的构造方法
		/// </summary>
		/// <param name="len">初始长度</param>
		public ByteString(int len)
		{
			_byteStr = new List<byte>(len);
		}
		/// <summary>
		/// 重载 + 同类型相加
		/// </summary>
		/// <param name="bsParam0"></param>
		/// <param name="bsParam1"></param>
		/// <returns></returns>
		public static ByteString operator + (ByteString bsParam0, ByteString bsParam1)
		{
			bsParam0._byteStr.AddRange(bsParam1._byteStr);
			return bsParam0;
		}
		/// <summary>
		/// 重载 + byte
		/// </summary>
		/// <param name="bsParam0"></param>
		/// <param name="bParam1"></param>
		/// <returns></returns>
		public static ByteString operator + (ByteString bsParam0, byte bParam1)
		{
			bsParam0._byteStr.Add(bParam1);
			return bsParam0;
		}
		/// <summary>
		/// 重载 + byte数组
		/// </summary>
		/// <param name="bsParam0"></param>
		/// <param name="baParam1"></param>
		/// <returns></returns>
		public static ByteString operator + (ByteString bsParam0, byte[] baParam1)
		{
			bsParam0._byteStr.AddRange(baParam1);
			return bsParam0;
		}
		/// <summary>
		/// 重载 下标访问
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
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
		/// <summary>
		/// 通过byte数组设置数据内容
		/// </summary>
		/// <param name="byteStr"></param>
		public void SetBytes(byte[] byteStr)
		{
			_byteStr.Clear();
			_byteStr.AddRange(byteStr);
		}
		/// <summary>
		/// 获取byte数组
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			return _byteStr.ToArray();
		}
		/// <summary>
		/// 获取数据长度
		/// </summary>
		/// <returns></returns>
		public int GetLength()
		{
			return _byteStr.Count;
		}
		/// <summary>
		/// 将byte转换为UTF-8字符串
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return new string(Encoding.UTF8.GetChars(_byteStr.ToArray()));
		}
		/// <summary>
		/// 删除指定位置
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public bool RemoveAt(int pos)
		{
			try
			{
				_byteStr.RemoveAt(pos);
				return true;
			}
			catch(ArgumentOutOfRangeException)
			{
				return false;
			}
		}
		/// <summary>
		/// 删除最后一个byte，如果长度为0则返回false
		/// </summary>
		/// <returns></returns>
		public bool RemoveLast()
		{
			if (_byteStr.Count > 0)
			{
				_byteStr.RemoveAt(_byteStr.Count - 1);
				return true;
			}
			return false;
		}
		/// <summary>
		/// 交换指定的两个位置
		/// </summary>
		/// <param name="idx0"></param>
		/// <param name="idx1"></param>
		public void Swap(int idx0, int idx1)
		{
			byte temp = _byteStr[idx1];
			_byteStr[idx1] = _byteStr[idx0];
			_byteStr[idx0] = temp;
		}
		/// <summary>
		/// 返回子串
		/// </summary>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public ByteString SubString(int startIndex, int length)
		{
			ByteString subString = new ByteString();
			try
			{
				while (length > 0)
				{
					subString += _byteStr[startIndex];
					startIndex++;
					length--;
				}
			}
			catch(ArgumentOutOfRangeException)
			{
				return new ByteString();
			}
			return subString;
		}
		/// <summary>
		/// ByteString与byte[]异或，返回结果永远以ByteString为主
		/// </summary>
		/// <param name="bsParam"></param>
		/// <param name="baParam"></param>
		/// <returns></returns>
		public static ByteString operator ^ (ByteString bsParam, byte[] baParam)
		{
			ByteString bsRes = new ByteString(bsParam.GetBytes());
			int count = bsParam.GetLength() < baParam.Length ? bsParam.GetLength() : baParam.Length;
			while(count-- > 0)
			{
				bsRes[count] ^= baParam[count];
			}
			return bsRes;
		}
		/// <summary>
		/// ByteString异或，返回结果以左侧为主
		/// </summary>
		/// <param name="bsParam0"></param>
		/// <param name="bsParam1"></param>
		/// <returns></returns>
		public static ByteString operator ^(ByteString bsParam0, ByteString bsParam1)
		{
			ByteString bsRes = new ByteString(bsParam0.GetBytes());
			int count = bsParam0.GetLength() < bsParam0.GetLength() ? bsParam0.GetLength() : bsParam1.GetLength();
			while (count-- > 0)
			{
				bsRes[count] ^= bsParam1[count];
			}
			return bsRes;
		}
	}
}
