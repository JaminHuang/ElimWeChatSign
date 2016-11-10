namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 用户签到输出类
	/// </summary>
	public class ResUserSign
	{
		/// <summary>
		/// 签到标识
		/// </summary>
		public string SignId { get; set; }
		/// <summary>
		/// 用户标识
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 计划标识
		/// </summary>
		public string PlanId { get; set; }
		/// <summary>
		/// 签到类型[1:读经;2:读书]
		/// </summary>
		public int SignType { get; set; }
		/// <summary>
		/// 签到时间
		/// </summary>
		public string SignDate { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public string UpdateTime { get; set; }
	}
}
