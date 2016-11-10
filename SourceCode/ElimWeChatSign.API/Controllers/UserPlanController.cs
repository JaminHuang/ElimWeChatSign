using System;
using System.Threading.Tasks;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 用户计划
	/// </summary>
    public class UserPlanController : WarpperController
    {
		private UserPlanBusiness userPlanBusiness = new UserPlanBusiness();

		/// <summary>
		/// 添加计划
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Add()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "", biblePlan = "", bookPlan = "";
			DateTime startDate, endDate;
			if (dic != null && dic.ContainsKey("userId") && dic.ContainsKey("biblePlan") && dic.ContainsKey("bookPlan") && dic.ContainsKey("startDate") && dic.ContainsKey("endDate"))
			{
				userId = dic["userId"].ToString();
				biblePlan = dic["biblePlan"].ToString();
				bookPlan = dic["bookPlan"].ToString();
				startDate = DateTime.Parse(dic["startDate"].ToString());
				endDate = DateTime.Parse(dic["endDate"].ToString());
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userPlanBusiness.Add(userId, biblePlan, bookPlan, startDate, endDate);
			res.Content = result;
			return res;
		}

		/// <summary>
		/// 修改计划
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Update()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string planId = "", userId = "", biblePlan = "", bookPlan = "";
			DateTime? startDate = null, endDate = null;
			if (dic != null && dic.ContainsKey("planId") && dic.ContainsKey("userId") && dic.ContainsKey("biblePlan") && dic.ContainsKey("bookPlan"))
			{
				planId = dic["planId"].ToString();
				userId = dic["userId"].ToString();
				biblePlan = dic["biblePlan"].ToString();
				bookPlan = dic["bookPlan"].ToString();

				if (dic.ContainsKey("startDate")) { startDate = DateTime.Parse(dic["startDate"].ToString()); }
				if (dic.ContainsKey("startDate")) { endDate = DateTime.Parse(dic["endDate"].ToString()); } 
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userPlanBusiness.Update(planId, userId, biblePlan, bookPlan, startDate, endDate);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 根据用户搜索所有列表
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> ListByUserId()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "";
			DateTime? startDate = null, endDate = null;
			if (dic != null && dic.ContainsKey("userId"))
			{
				userId = dic["userId"].ToString();

				if (dic.ContainsKey("startDate")) { startDate = DateTime.Parse(dic["startDate"].ToString()); }
				if (dic.ContainsKey("startDate")) { endDate = DateTime.Parse(dic["endDate"].ToString()); } 
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userPlanBusiness.ListByUserId(userId, startDate, endDate);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 获取列表
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> List()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string userName = "";
			DateTime? startDate = null, endDate = null;
			if (dic != null && dic.ContainsKey("userName"))
			{
				userName = dic["userName"].ToString();

				if (dic.ContainsKey("startDate")) { startDate = DateTime.Parse(dic["startDate"].ToString()); }
				if (dic.ContainsKey("startDate")) { endDate = DateTime.Parse(dic["endDate"].ToString()); }
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userPlanBusiness.List(userName, startDate, endDate);

			res.Content = result;
			return res;
		}
    }
}
