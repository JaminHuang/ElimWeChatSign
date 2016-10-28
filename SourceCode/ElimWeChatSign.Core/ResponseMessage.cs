using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Core
{
	public class ResponseMessage
	{
		public ResponseMessage()
		{
			ServerTime = DateTime.UtcNow.CreateTimestamp();
			Code = ResponseCode.Fail;
			Content = "";
		}
		#region 属性
		/// <summary>
		/// 状态码
		/// </summary>
		public ResponseCode Code { get; set; }
		/// <summary>
		/// 响应对象
		/// </summary>
		public object Content { get; set; }
		/// <summary>
		/// 当前系统时间
		/// </summary>
		public long ServerTime { get; set; }
		#endregion
	}

	/// <summary>
	/// 各操作代码
	/// </summary>
	public enum ResponseCode
	{
		/// <summary>
		/// 操作成功
		/// </summary>
		Success = 1000,
		/// <summary>
		/// 操作失败
		/// </summary>
		Fail = 1100,
		/// <summary>
		/// 请求参数无效
		/// </summary>
		RequestParamInvalid = 1101,
		/// <summary>
		/// 缺少必填参数
		/// </summary>
		RequireParamLack = 1102,
		/// <summary>
		/// Token失效
		/// </summary>
		TokenInvalid = 1103,
		/// <summary>
		/// 部分批量操作失败
		/// </summary>
		BatchActionFail = 1104,
		/// <summary>
		/// 实体不存在
		/// </summary>
		EntityNotExist = 1105,
		/// <summary>
		/// 账号/实体已存在
		/// </summary>
		EntityExist = 1106,
		/// <summary>
		/// 账号已绑定
		/// </summary>
		OpenIdBound = 1107,
		/// <summary>
		/// 账号或密码有误
		/// </summary>
		NameOrPwdError = 1108
	}
}
