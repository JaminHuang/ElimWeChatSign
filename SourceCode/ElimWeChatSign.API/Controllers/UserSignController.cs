using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 用户签到接口
	/// </summary>
	public class UserSignController : WarpperController
	{
		private UserSignBusiness userSignBusiness = new UserSignBusiness();

		/// <summary>
		/// 签到/打卡
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Add()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string planId = "", userId = "";
			int signType = 0;
			if (dic != null && dic.ContainsKey("planId") && dic.ContainsKey("userId") && dic.ContainsKey("signType"))
			{
				planId = dic["planId"].ToString();
				userId = dic["userId"].ToString();
				signType = int.Parse(dic["signType"].ToString());
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userSignBusiness.Add(planId, userId, signType);
			res.Content = result;
			return res;
		}

		/// <summary>
		/// 修改签到
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Update()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string signId = "", planId = "", userId = "";
			int signType = 0;
			if (dic != null && dic.ContainsKey("signId") && dic.ContainsKey("planId") && dic.ContainsKey("userId") && dic.ContainsKey("signType"))
			{
				signId = dic["signId"].ToString();
				planId = dic["planId"].ToString();
				userId = dic["userId"].ToString();
				signType = int.Parse(dic["signType"].ToString());
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userSignBusiness.Update(signId, planId, userId, signType);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 获取列表
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> ListByUserId()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "", planId = "";
			if (dic != null && dic.ContainsKey("userId"))
			{
				userId = dic["userId"].ToString();

				if (dic.ContainsKey("planId")) { planId = dic["planId"].ToString(); }
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userSignBusiness.ListByUserId(userId, planId);

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
			DateTime? signDate = null;
			if (dic != null && dic.ContainsKey("userName")) { userName = dic["userName"].ToString(); }
			if (dic != null && dic.ContainsKey("startDate")) { signDate = DateTime.Parse(dic["signDate"].ToString()); }
			var result = userSignBusiness.List(userName, signDate);

			res.Content = result;
			return res;
		}
	}
}
