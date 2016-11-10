namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 用户计划输出类
	/// </summary>
	public class ResUserPlan
	{
		/// <summary>
		/// 计划标识
		/// </summary>
		public string PlanId { get; set; }
		/// <summary>
		/// 用户标识
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 读经计划
		/// </summary>
		public string BiblePlan { get; set; }
		/// <summary>
		/// 读书计划
		/// </summary>
		public string BookPlan { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public string StartDate { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public string EndDate { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public string UpdateTime { get; set; }
	}
}
