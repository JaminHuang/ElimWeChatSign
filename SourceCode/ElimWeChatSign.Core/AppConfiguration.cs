using System.Configuration;

namespace ElimWeChatSign.Core
{
	/// <summary>
	/// 配置信息处理类
	/// </summary>
	public static class AppConfiguration
	{
		/// <summary>
		/// 获取配置中指定Key的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetKey(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
