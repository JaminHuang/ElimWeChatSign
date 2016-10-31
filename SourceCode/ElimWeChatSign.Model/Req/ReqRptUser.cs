using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
    public class ReqRptUserAdd
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 计划标识
        /// </summary>
        public string planId { get; set; }

        /// <summary>
        /// 读经完成度
        /// </summary>
        public string bibleTask { get; set; }

        /// <summary>
        /// 读书完成度
        /// </summary>
        public string bookTask { get; set; }
    }

    public class ReqList
    {
        /// <summary>
        /// 姓名[模糊]
        /// </summary>
        public string userName { get; set; }
    }
}
