using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Core
{
	public static class ExtendUtil
	{
		#region 时间戳
		/// <summary>
		/// 返回时间戳
		/// </summary>
		/// <param name="utcNow">UTC时间</param>
		/// <returns></returns>
		public static long CreateTimestamp(this DateTime utcNow)
		{
			return utcNow.ToTimestamp();
		}
		/// <summary>
		/// 转成时间戳
		/// </summary>
		/// <param name="dateTime">now</param>
		/// <returns></returns>
		public static long ToTimestamp(this DateTime dateTime)
		{
			TimeZone tz = TimeZone.CurrentTimeZone;
			var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64((tz.ToUniversalTime(dateTime) - utcTime).TotalMilliseconds);
		}
		/// <summary>
		/// 时间戳转成日期
		/// </summary>
		/// <param name="timestamp">时间戳</param>
		/// <returns></returns>
		public static DateTime ToDateTime(this long timestamp)
		{
			var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var dt = utcTime.AddMilliseconds(timestamp);
			return dt.ToLocalTime();
		}
		#endregion

		#region 授权Token
		/// <summary>
		/// 生成授权token
		/// </summary>
		/// <returns></returns>
		public static string GenerateAuthToken()
		{
			string guid = Guid.NewGuid().ToString();
			string date = DateTime.Now.ToString("yyMMdd");
			return guid.Substring(1, 1) + guid.Substring(3, 1) + guid.Substring(5, 1) +
				   guid.Substring(7, 1) + guid.Substring(9, 1)
				   + date.Substring(0, 1) + date.Substring(2, 1) + date.Substring(4, 1) +
				   guid.Substring(0, 1) + guid.Substring(2, 1) + guid.Substring(4, 1) +
				   guid.Substring(6, 1) + guid.Substring(8, 1)
				   + date.Substring(1, 1) + date.Substring(3, 1) + date.Substring(5, 1);
		}
		/// <summary>
		/// 解析授权token
		/// </summary>
		/// <param name="authToken"></param>
		/// <returns></returns>
		public static string AnalyzeAuthToken(string authToken)
		{
			//token的偶数位前五位 + 当前时间的奇数位前三位 + token的奇数位前五位 + 当前时间的偶数位前三位
			return authToken.Substring(3, 5) + authToken.Substring(0, 3) + authToken.Substring(11, 5) + authToken.Substring(8, 3);
		}
		/// <summary>
		/// 生成客户端token
		/// </summary>
		/// <param name="authToken"></param>
		/// <returns></returns>
		public static string GenerateClientAuthToken(string authToken)
		{
			//当前时间的奇数位前三位 + token的偶数位前五位 + 当前时间的偶数位前三位+ token的奇数位前五位。
			return authToken.Substring(5, 3) + authToken.Substring(0, 5) + authToken.Substring(13, 3) + authToken.Substring(8, 5);
		}
		#endregion

		#region 随机
		/// <summary>
		/// 产生指定位数的随机数
		/// </summary>
		/// <param name="randomLevel">位数</param>
		/// <returns></returns>
		public static string GetRandom(int randomLevel)
		{
			var str = "";
			var random = new Random();

			for (int i = 0; i < randomLevel; i++)
			{
				int num = random.Next(0, 9);
				str += num.ToString();
			}
			return str;
		}
		/// <summary>
		/// 16位随机字符串
		/// </summary>
		/// <returns></returns>
		public static string GuidToString()
		{
			long i = 1;
			foreach (byte b in Guid.NewGuid().ToByteArray())
				i *= ((int)b + 1);
			return string.Format("{0:x}", i - DateTime.Now.Ticks);
		}
		#endregion

		/// <summary>
		/// MD5转换
		/// </summary>
		/// <param name="toEncrypt">字符串</param>
		/// <returns></returns>
		public static string ToMd5(this string toEncrypt)
		{
			var str = CryptographyUtil.Md5Hash(toEncrypt);
			return str.Substring(0, 5) + str.Substring(str.Length - 6);
		}

		public static bool IsNull(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}
	}
}
