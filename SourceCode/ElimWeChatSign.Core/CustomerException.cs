using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Core
{
	/// <summary>
	/// 自定义错误异常
	/// </summary>
	public class CustomerException : Exception
	{
		public CustomerException(ResponseCode code)
		{
			this.Code = code;
		}

		public CustomerException(ResponseCode code, string msg)
		{
			this.Code = code;
			this.Msg = msg;
		}

		private string _msg;
		/// <summary>
		/// 错误代码
		/// </summary>
		public ResponseCode Code { get; private set; }
		/// <summary>
		/// 错误信息
		/// </summary>
		public string Msg
		{
			get { return _msg; }
			set { _msg = value ?? ""; }
		}

	}
}
