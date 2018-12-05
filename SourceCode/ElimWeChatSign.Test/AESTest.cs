using System;
using System.Text;
using JaminHuang.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElimWeChatSign.Test
{
	/// <summary>
	/// AES加密相关
	/// </summary>
	[TestClass]
	public class AESTest
	{
		/// <summary>
		/// 向量转换
		/// </summary>
		[TestMethod]
		public void IvToString_Test()
		{
			byte[] ServerAuthTokenIv = { 0x38, 0x31, 0x37, 0x34, 0x36, 0x33, 0x35, 0x33, 0x32, 0x31, 0x34, 0x38, 0x37, 0x36, 0x35, 0x32 };
			var ivStr = UnicodeEncoding.UTF8.GetString(ServerAuthTokenIv);
		}

        /// <summary>
		/// 随机生成
		/// </summary>
		[TestMethod]
        public void Guid_Test()
        {
            var str = ExtendUtil.GuidToString();
            Console.WriteLine(str);
        }

        /// <summary>
        /// 加密分析
        /// </summary>
        [TestMethod]
		public void AES_ClientEncry_Test()
		{
			var str = "Data Source=103.45.8.16;database=elimsign;uid=jaminhuang;Password=opN9d1t3AUQ#Kzyy;";
			var strCry = CryptographyUtil.AESEncryServer(str);
            Console.WriteLine(strCry);
		}

		/// <summary>
		/// 解密分析
		/// </summary>
		[TestMethod]
		public void AES_ClientDecrp_Test()
		{
			var str = "ufYIXTGVmOkXMMcbIduJCm+GZAzR4nevgSFD3sxGQkDBf1Os/rpsI24ey8UI4twjecObucHKVR/2OEWUD1uFGqgCX2Vbcbv7MIvMKoopOTc=";
			var strCry = CryptographyUtil.AESDecryptServer(str);
		}
	}
}
