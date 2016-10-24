using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
	public class ResUserInfo
	{
		/// <summary>
		/// 用户标识
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 用户邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 用户类型
		/// </summary>
		public int UserType { get; set; }
	}
}
