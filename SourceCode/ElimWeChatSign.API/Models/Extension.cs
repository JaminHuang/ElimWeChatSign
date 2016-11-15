using System.Web;

namespace ElimWeChatSign.API
{
	/// <summary>
	/// API扩展公共类
	/// </summary>
	public static class Extension
	{
		/// <summary>
		/// 获取客户端IP地址
		/// </summary>
		/// <returns>若失败则返回回送地址</returns>
		public static string GetIp()
		{
			string userHostAddress = HttpContext.Current.Request.UserHostAddress;

			if (string.IsNullOrEmpty(userHostAddress))
			{
				userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}

			//最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
			if (!string.IsNullOrEmpty(userHostAddress) && IsIp(userHostAddress))
			{
				return userHostAddress;
			}
			return "127.0.0.1";
		}

		/// <summary>
		/// 检查IP地址格式
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		private static bool IsIp(string ip)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}
	}
}