namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 签到统计输出类
	/// </summary>
	public class ResRptSign
	{
		/// <summary>
		/// 报表标识
		/// </summary>
		public string RptId { get; set; }
		/// <summary>
		/// 用户标识
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 计划标识
		/// </summary>
		public string PlanId { get; set; }
		/// <summary>
		/// 读经完成度
		/// </summary>
		public string BibleTask { get; set; }
		/// <summary>
		/// 读书完成度
		/// </summary>
		public string BookTask { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public string UpdateTime { get; set; }
	}
}
