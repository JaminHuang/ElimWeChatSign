using ElimWeChatSign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.IBusiness
{
    public interface IGatherBusiness : IDependency
    {
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="gatherId">签到标识[空为添加]</param>
        /// <param name="userName">姓名</param>
        /// <param name="gender">性别</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)</param>
        /// <param name="ipAddress">请求IP地址</param>
        /// <returns></returns>
        ResGether Sign(string churchId, string gatherId, string userName, int gender, string groupName, int gatherType, string ipAddress);

        /// <summary>
        /// 获取签到列表[按天]
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="date">报表日期</param>
        /// <returns></returns>
        List<ResGether> ListByDate(string churchId, string userName, string groupName, int gatherType, DateTime? date);

        /// <summary>
        /// 获取签到列表[时间段]
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        List<ResGether> ListByDate(string churchId, string userName, string groupName, int gatherType, DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 获取签到列表[时间段]
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式</param>
        /// <returns></returns>
        List<ResGether> List(string churchId, string userName, string groupName, int gatherType);

        /// <summary>
		/// 统计用户签到列表
		/// </summary>
        /// <param name="churchId">教会标识</param>
		/// <param name="userName">用户姓名</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="gatherType">聚会形式</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		List<ResGetherUser> GatherUserList(string churchId, string userName, string groupName, int gatherType, DateTime? startTime = null, DateTime? endTime = null);

        /// <summary>
        /// 删除签到
        /// </summary>
        /// <param name="gatherId">签到标识</param>
        /// <returns></returns>
        string Delete(string gatherId);

        /// <summary>
        /// 获取签到人数
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="type">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        int GetSignCount(string churchId, int type, DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 获取签到人员名单
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="type">聚会形式</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        List<ResGatherList> GetSignNameList(string churchId, int type, DateTime date);

        /// <summary>
        /// 统计某时间段小组聚会总人数
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        List<ResRptGatherGroup> GetGatherGroupList(string churchId, int gatherType, DateTime? startTime = null, DateTime? endTime = null);

        /// <summary>
        /// 获取小组聚会报表
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
	    List<ResRptGather> GetRptGather(string churchId, int gatherType, DateTime? startTime = null, DateTime? endTime = null);
    }
}
