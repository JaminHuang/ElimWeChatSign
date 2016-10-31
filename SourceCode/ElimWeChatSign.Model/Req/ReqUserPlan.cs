using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
    public class ReqAddUserPlan
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 读经计划
        /// </summary>
        public string biblePlan { get; set; }

        /// <summary>
        /// 读书计划
        /// </summary>
        public string bookPlan { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endDate { get; set; }
    }

    /// <summary>
    /// 修改计划
    /// </summary>
    public class ReqUpdatePlan
    {
        /// <summary>
        /// 计划标识
        /// </summary>
        public string planId { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 读经计划
        /// </summary>
        public string biblePlan { get; set; }

        /// <summary>
        /// 读书计划
        /// </summary>
        public string bookPlan { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endDate { get; set; }
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    public class ReqPlanListByUserId
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endDate { get; set; }
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    public class ReqPlanList
    {
        /// <summary>
        /// 姓名[模糊]
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endDate { get; set; }
    }
}
