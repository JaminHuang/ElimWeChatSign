using System;

namespace ElimWeChatSign.API
{
	public static class LoggerHelper
	{
		private static readonly log4net.ILog LogInfo = log4net.LogManager.GetLogger("LogInfo");
		private static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("LogError");

		/// <summary>
		/// 记录Error日志
		/// </summary>
		/// <param name="errorMsg"></param>
		/// <param name="ex"></param>
		public static void Error(string errorMsg, Exception ex = null)
		{
			if (ex != null)
			{
				LogError.Error(errorMsg, ex);
			}
			else
			{
				LogError.Error(errorMsg);
			}
		}

		/// <summary>
		/// 记录Info日志
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="ex"></param>
		public static void Info(string msg, Exception ex = null)
		{
			if (ex != null)
			{
				LogInfo.Info(msg, ex);
			}
			else
			{
				LogInfo.Info(msg);
			}
		}
	}
}