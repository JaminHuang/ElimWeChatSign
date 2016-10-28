using System;
using System.Collections.Generic;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;
using Titan;

namespace ElimWeChatSign.Service
{
    public class UserPlanService : IUserPlanService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public UserPlan AddOrUpdate(UserPlan model)
        {
            if (model.PlanId.IsNull())
            {
                model.PlanId = ExtendUtil.GuidToString();
                model.UpdateTime = DateTime.Now;
                model.Insert();
            }
            else
            {
                model.UpdateTime = DateTime.Now;
                model.Update();
            }
            return model;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="planId">主键</param>
        /// <returns></returns>
        public bool Delete(string planId)
        {
            var up = new UserPlan(planId);
            return up.Delete() > 0;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="planId">主键</param>
        /// <returns></returns>
        public UserPlan Get(string planId)
        {
            var up = new UserPlan(planId);
            return up.Select() ? up : null;
        }

        /// <summary>
        /// 获取某用户的计划列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<UserPlan> ListByUserId(string userId, DateTime? startDate, DateTime? endDate = null)
        {
            var query = new QueryExpression();
            query.Selects.Add(UserPlanProperties.ALL);
            query.Wheres.Add(UserPlanProperties.UserId.Equal(userId));
            if (startDate != null)
                query.Wheres.Add(UserPlanProperties.StartDate.GreaterEqual(startDate));
            if (endDate != null)
                query.Wheres.Add(UserPlanProperties.EndDate.LessEqual(endDate));

            var upList = new UserPlans();
            upList.Select(query);

            return upList.Items;
        }

        /// <summary>
        /// 查询所有计划列表
        /// </summary>
        /// <param name="userName">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<UserPlan> List(string userName = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = new QueryExpression();
            query.Selects.Add(UserPlanProperties.ALL);
            if (!userName.IsNull())
                query.Wheres.Add(UserPlanProperties.UserInfo.UserName.Like(userName));
            if (startDate != null)
                query.Wheres.Add(UserPlanProperties.StartDate.GreaterEqual(startDate));
            if (endDate != null)
                query.Wheres.Add(UserPlanProperties.EndDate.LessEqual(endDate));

            var upList = new UserPlans();
            upList.Select(query);

            return upList.Items;
        }
    }
}
