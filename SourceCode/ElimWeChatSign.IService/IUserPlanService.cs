using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.IService
{
    /// <summary>
    /// 用户计划服务接口
    /// </summary>
    public interface IUserPlanService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        UserPlan AddOrUpdate(UserPlan model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="planId">主键</param>
        /// <returns></returns>
        bool Delete(string planId);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="planId">主键</param>
        /// <returns></returns>
        UserPlan Get(string planId);

        /// <summary>
        /// 获取某用户的计划列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        List<UserPlan> ListByUserId(string userId, DateTime? startDate, DateTime? endDate = null);

        /// <summary>
        /// 查询所有计划列表
        /// </summary>
        /// <param name="userName">姓名</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        List<UserPlan> List(string userName = "", DateTime? startDate = null, DateTime? endDate = null);
    }
}
