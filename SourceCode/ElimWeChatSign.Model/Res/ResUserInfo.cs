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
		/// 用户邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 用户类型[0:普通;1:同工;9999:管理员]
		/// </summary>
		public int UserType { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public string UpdateTime { get; set; }
	}
}
