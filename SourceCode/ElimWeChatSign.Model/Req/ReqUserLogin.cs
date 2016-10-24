using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
	public class ReqUserLogin
	{
		/// <summary>
		/// 手机号码
		/// </summary>
		public string mobile { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string pwd { get; set; }
	}
}
