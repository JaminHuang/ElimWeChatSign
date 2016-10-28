using System;
using System.Text.RegularExpressions;

namespace ElimWeChatSign.Core
{
	/// <summary>
	/// 正则表达式，验证公共类
	/// </summary>
	public static class RegexUtil
	{
		/// <summary>
		/// 中文姓名验证
		/// </summary>
		/// <param name="strInput">待验证的字符串</param>
		/// <returns>是中文返回true</returns>
		public static bool IsNameCn(this string strInput)
		{
			var reg = new Regex("[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*");
			return reg.IsMatch(strInput);
		}

		/// <summary>
		/// 手机号验证
		/// </summary>
		/// <param name="strInput">待验证的字符串</param>
		/// <returns></returns>
		public static bool IsMobileNum(this string strInput)
		{
			var reg = new Regex(@"^1[34578]\d{9}$");
			return reg.IsMatch(strInput);
		}

		#region 身份证验证
		public static bool CheckIdCard(this string idNum)
		{
			switch (idNum.Length)
			{
				case 18: return CheckIdCard18(idNum);
				case 15: return CheckIdCard15(idNum);
				default: return false;
			}
		}

		private static bool CheckIdCard18(string idNum)
		{
			long n = 0;
			if (long.TryParse(idNum.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(idNum.Replace('x', '0').Replace('X', '0'), out n) == false)
			{
				return false;//数字验证
			}
			string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (address.IndexOf(idNum.Remove(2), StringComparison.Ordinal) == -1)
			{
				return false;//省份验证
			}
			string birth = idNum.Substring(6, 8).Insert(6, "-").Insert(4, "-");
			DateTime time = new DateTime();
			if (DateTime.TryParse(birth, out time) == false)
			{
				return false;//生日验证
			}

			string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
			string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
			char[] Ai = idNum.Remove(17).ToCharArray();
			int sum = 0;
			for (int i = 0; i < 17; i++)
			{
				sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
			}
			int y = -1;
			Math.DivRem(sum, 11, out y);
			if (arrVarifyCode[y] != idNum.Substring(17, 1).ToLower())
			{
				return false;//校验码验证
			}
			return true;//符合GB11643-1999标准
		}

		private static bool CheckIdCard15(string idNum)
		{
			long n = 0;
			if (long.TryParse(idNum, out n) == false || n < Math.Pow(10, 14))
			{
				return false;//数字验证
			}
			string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (address.IndexOf(idNum.Remove(2)) == -1)
			{
				return false;//省份验证
			}
			string birth = idNum.Substring(6, 6).Insert(4, "-").Insert(2, "-");
			DateTime time = new DateTime();
			if (DateTime.TryParse(birth, out time) == false)
			{
				return false;//生日验证
			}
			return true;//符合15位身份证标准
		}
		#endregion
	}
}
