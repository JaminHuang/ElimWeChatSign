using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
    public class ReqUserSignAdd
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
        /// 签到类型[1:读经;2:读书]
        /// </summary>
        public int signType { get; set; }
        
    }

    public class ReqUserSignUpdate
    {
        /// <summary>
        /// 签到标识
        /// </summary>
        public string signId { get; set; }

        /// <summary>
        /// 计划标识
        /// </summary>
        public string planId { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 签到类型[1:读经;2:读书]
        /// </summary>
        public int signType { get; set; }
    }

    public class ReqUserSignListByUserId
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 计划标识
        /// </summary>
        public string planId { get; set; }
    }

    public class ReqUserSignList
    {
        /// <summary>
        /// 姓名[模糊]
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? signDate { get; set; }
    }
}
