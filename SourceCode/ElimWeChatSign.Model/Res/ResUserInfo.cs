namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 用户信息输出类
	/// </summary>
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
		/// 用户头像地址
		/// </summary>
		public string Avatar { get; set; }
		/// <summary>
		/// 性别(0:Male;1:Female)
		/// </summary>
		public int Gender { get; set; }
		/// <summary>
		/// 用户邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 用户类型[0:普通;1:同工;9999:管理员]
		/// </summary>
		public int UserType { get; set; }
		/// <summary>
		/// 操作系统
		/// </summary>
		public string Os { get; set; }
		/// <summary>
		/// 操作系统版本
		/// </summary>
		public string OsVersion { get; set; }
		/// <summary>
		/// 设备UUID
		/// </summary>
		public string DeviceId { get; set; }
		/// <summary>
		/// app版本
		/// </summary>
		public string AppVersion { get; set; }
		/// <summary>
		/// 授权Token,用于登陆,退出
		/// </summary>
		public string AuthToken { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public string UpdateTime { get; set; }
	}
}
