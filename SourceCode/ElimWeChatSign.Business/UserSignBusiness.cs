using System;
using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 用户签到业务逻辑
	/// </summary>
    public class UserSignBusiness
    {
        private IUserSignService userSignService;

	    /// <summary>
	    /// 签到/打卡
	    /// </summary>
	    /// <param name="planId">计划标识</param>
	    /// <param name="userId">用户标识</param>
	    /// <param name="signType">签到类型</param>
	    public ResUserSign Add(string planId, string userId, int signType)
	    {
		    if (planId.IsNull() || userId.IsNull())
			    throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

		    var model = new UserSign
		    {
			    UserId = userId,
			    PlanId = planId,
			    SignDate = DateTime.Now,
			    SignType = signType
		    };

		    var us = userSignService.AddOrUpdate(model);
		    
			//输出对象
			var resUserSign = new ResUserSign
			{
				SignId = us.SignId,
				UserId = us.UserId,
				PlanId = us.PlanId,
				SignDate = us.SignDate.ToString("yyyy-MM-dd HH:mm:ss"),
				SignType = us.SignType,
				UpdateTime = us.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

		    return resUserSign;
	    }

	    /// <summary>
	    /// 修改签到
	    /// </summary>
	    /// <param name="signId">签到标识</param>
	    /// <param name="planId">计划标识</param>
	    /// <param name="userId">用户标识</param>
	    /// <param name="signType">签到类型</param>
	    public ResUserSign Update(string signId, string planId, string userId, int signType)
        {
            if (planId.IsNull() || signId.IsNull() || userId.IsNull())
	            throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

	        var model = userSignService.Get(signId);
	        model.PlanId = planId;
	        model.SignId = signId;
	        model.UserId = userId;
			model.SignDate = DateTime.Now;
	        model.SignType = signType;

	        var us = userSignService.AddOrUpdate(model);

			//输出对象
			var resUserSign = new ResUserSign
			{
				SignId = us.SignId,
				UserId = us.UserId,
				PlanId = us.PlanId,
				SignDate = us.SignDate.ToString("yyyy-MM-dd HH:mm:ss"),
				SignType = us.SignType,
				UpdateTime = us.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resUserSign;
        }

	    /// <summary>
	    /// 获取列表
	    /// </summary>
		/// <param name="userId">用户标识</param>
	    /// <param name="planId">计划标识[传空为获取全部]</param>
	    public List<ResUserSign> ListByUserId(string userId, string planId)
        {
            if (userId.IsNull())
	            throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

	        var list = userSignService.ListByUserId(userId, planId);

			//输出对象
			var resDate = list.Select(item => new ResUserSign
			{
				SignId = item.SignId,
				UserId = item.UserId,
				PlanId = item.PlanId,
				SignDate = item.SignDate.ToString("yyyy-MM-dd HH:mm:ss"),
				SignType = item.SignType,
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
        }

	    /// <summary>
	    /// 获取列表
	    /// </summary>
	    /// <param name="userName">用户姓名[传空获取所有]</param>
	    /// <param name="signDate">签到时间[签到时间]</param>
	    public List<ResUserSign> List(string userName, DateTime? signDate)
        {
            var list = userSignService.List(userName, signDate);

			//输出对象
			var resDate = list.Select(item => new ResUserSign
			{
				SignId = item.SignId,
				UserId = item.UserId,
				PlanId = item.PlanId,
				SignDate = item.SignDate.ToString("yyyy-MM-dd HH:mm:ss"),
				SignType = item.SignType,
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
        }
    }
}
