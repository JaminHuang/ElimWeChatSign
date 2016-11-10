using System;
using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
    /// <summary>
    /// 用户计划业务逻辑
    /// </summary>
    public class UserPlanBusiness
    {
        private IUserPlanService userPlanService;

	    /// <summary>
	    /// 添加计划
	    /// </summary>
	    /// <param name="userId">用户标识</param>
	    /// <param name="biblePlan">读经计划</param>
	    /// <param name="bookPlan">读书计划</param>
	    /// <param name="startDate">开始时间</param>
	    /// <param name="endDate">结束时间</param>
	    public ResUserPlan Add(string userId, string biblePlan, string bookPlan, DateTime startDate, DateTime endDate)
	    {
		    if (userId.IsNull())
			    throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

		    var model = new UserPlan
		    {
			    UserId = userId,
			    BiblePlan = biblePlan,
			    BookPlan = bookPlan,
			    StartDate = startDate,
			    EndDate = endDate
		    };

		    var up = userPlanService.AddOrUpdate(model);

			//输出对象
			var resUserPlan = new ResUserPlan
			{
				PlanId = up.PlanId,
				UserId = up.UserId,
				BiblePlan = up.BiblePlan,
				BookPlan = up.BookPlan,
				StartDate = up.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
				EndDate = up.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
				UpdateTime = up.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

		    return resUserPlan;
	    }

	    /// <summary>
	    /// 修改计划
	    /// </summary>
		/// <param name="planId">计划标识</param>
	    /// <param name="userId">用户标识</param>
	    /// <param name="biblePlan">读经计划</param>
	    /// <param name="bookPlan">读书计划</param>
	    /// <param name="startDate">开始时间</param>
	    /// <param name="endDate">结束时间</param>
	    public ResUserPlan Update(string planId, string userId, string biblePlan, string bookPlan, DateTime? startDate, DateTime? endDate)
        {
            if (planId.IsNull() || userId.IsNull())
	            throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

	        var model = userPlanService.Get(planId);
	        if (!biblePlan.IsNull()) { model.BiblePlan = biblePlan; }
	        if (!bookPlan.IsNull()) { model.BookPlan = bookPlan; }
	        if (startDate != null) { model.StartDate = startDate.Value; }
	        if (endDate != null) { model.EndDate = endDate.Value; }

	        var up = userPlanService.AddOrUpdate(model);

			//输出对象
			var resUserPlan = new ResUserPlan
			{
				PlanId = up.PlanId,
				UserId = up.UserId,
				BiblePlan = up.BiblePlan,
				BookPlan = up.BookPlan,
				StartDate = up.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
				EndDate = up.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
				UpdateTime = up.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resUserPlan;
        }

	    /// <summary>
	    /// 根据用户搜索所有列表
	    /// </summary>
	    /// <param name="userId">用户标识</param>
	    /// <param name="startDate">开始时间[传空获取所有]</param>
	    /// <param name="endDate">结束时间[传空获取所有]</param>
	    public List<ResUserPlan> ListByUserId(string userId, DateTime? startDate, DateTime? endDate)
        {
            if (userId.IsNull())
	            throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

	        var list = userPlanService.ListByUserId(userId, startDate, endDate);

			//输出对象
			var resDate = list.Select(item => new ResUserPlan
			{
				PlanId = item.PlanId,
				UserId = item.UserId,
				BiblePlan = item.BiblePlan,
				BookPlan = item.BookPlan,
				StartDate = item.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
				EndDate = item.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
        }

	    /// <summary>
	    /// 获取列表
	    /// </summary>
	    /// <param name="userName">用户姓名[传空获取所有]</param>
	    /// <param name="startDate">开始时间[传空获取所有]</param>
	    /// <param name="endDate">结束时间[传空获取所有]</param>
	    public List<ResUserPlan> List(string userName, DateTime? startDate, DateTime? endDate)
        {
            var list = userPlanService.List(userName, startDate, endDate);

			//输出对象
			var resDate = list.Select(item => new ResUserPlan
			{
				PlanId = item.PlanId,
				UserId = item.UserId,
				BiblePlan = item.BiblePlan,
				BookPlan = item.BookPlan,
				StartDate = item.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
				EndDate = item.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
        }
    }
}
