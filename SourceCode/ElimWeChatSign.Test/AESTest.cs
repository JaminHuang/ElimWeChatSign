using System;
using System.Text;
using ElimWeChatSign.Core;
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
		/// 加密分析
		/// </summary>
		[TestMethod]
		public void AES_ClientEncry_Test()
		{
			var str = "123456";
			var strCYy = CryptographyUtil.AESEncryClient(str);
		}

		/// <summary>
		/// 解密分析
		/// </summary>
		[TestMethod]
		public void AES_ClientDecrp_Test()
		{
			var str = "KKua+3FBmTAAxnXZWzBSWw==";
			var strCYy = CryptographyUtil.AESDecryptClient(str);
		}
	}
}
