using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 签到输出
	/// </summary>
	public class ResGether
	{
		/// <summary>
		/// 签到标识
		/// </summary>
		public string GatherId { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 小组名称
		/// </summary>
		public string GroupName { get; set; }
        /// <summary>
        /// 性别(0-女;1-男)
        /// </summary>
        public int? Gender { get; set; }
		/// <summary>
		/// 聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)
		/// </summary>
		public int? GatherType { get; set; }
		/// <summary>
		/// IP地址
		/// </summary>
		public string IpAddress { get; set; }
		/// <summary>
		/// 签到时间
		/// </summary>
		public string SignTime { get; set; }
	}

    public class ResGatherList
    {
        /// <summary>
		/// 签到标识
		/// </summary>
		public string GatherId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 性别(0-女;1-男)
        /// </summary>
        public int? Gender { get; set; }

    }

    /// <summary>
	/// 签到输出
	/// </summary>
	public class ResGetherUser
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
        /// 小组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 性别(0-女;1-男)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// 聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)
        /// </summary>
        public int? GatherType { get; set; }
        /// <summary>
        /// 签到总次数
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// 签到总次数
        /// </summary>
        public int? AllCount { get; set; }
        /// <summary>
        /// 出勤率
        /// </summary>
        public string SignRate { get; set; }
    }

    /// <summary>
    /// 小组总报表
    /// </summary>
    public class ResRptGatherGroup
    {
        /// <summary>
        /// 小组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 小组签到人数
        /// </summary>
        public int GroupNum { get; set; }
    }

    /// <summary>
    /// 小组报表
    /// </summary>
    public class ResRptGather
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string GatherDate { get; set; }

        /// <summary>
        /// 小组详情
        /// </summary>
        public List<ResRptGatherGroup> List { get; set; }
    }
}
