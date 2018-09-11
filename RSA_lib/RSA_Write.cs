using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSA_lib
{
	public class RSA_Write
	{
		public static bool PKCS1_Pri_Write(string strPath, bool isOverwrite = true)
		{
			if (File.Exists(strPath) && !isOverwrite) return false;
			File.WriteAllBytes(strPath, Encoding.ASCII.GetBytes(RSA_Gen.PKCS1_Pri_Gen_S()));
			return true;
		}

		public static bool PKCS1_Pub_Write(string strPath, string strPKCS1PriKey, bool isOverwrite = true)
		{
			if (File.Exists(strPath) && !isOverwrite) return false;
			File.WriteAllBytes(strPath, Encoding.ASCII.GetBytes(RSA_Gen.PKCS1_Pub_Gen_S(strPKCS1PriKey)));
			return true;
		}
	}
}
