using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
    public class ResUserFollow
    {
        /// <summary>
        /// 关怀标识
        /// </summary>
        public string FollowId { get; set; } = "";
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
		/// 小组名称
		/// </summary>
		public string GroupName { get; set; } = "";
        /// <summary>
        /// 关怀对象
        /// </summary>
        public string FollowName { get; set; } = "";
        /// <summary>
        /// 性别(0-女;1-男)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// 状态 0.新关怀 1.已关怀
        /// </summary>
        public int Status { get; set; }
    }
}
