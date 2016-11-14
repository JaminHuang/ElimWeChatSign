﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 聚会签到[单独接口]
	/// </summary>
    public class GatherController : WarpperController
    {
		private GatherBusiness gatherBusiness = new GatherBusiness();

		/// <summary>
		/// 签到
		/// </summary>
		/// param:
		/// userName:姓名
		/// groupName:小组[可空]
		/// gatherType:聚会形式
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Sign()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamServer(await this.Request.Content.ReadAsByteArrayAsync());
			string userName = "", groupName = "";
			int gatherType = -1;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());

				if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = gatherBusiness.Sign(userName, groupName, gatherType);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 签到列表[按时间统计]
		/// </summary>
		/// param:
		/// userName:姓名
		/// groupName:小组[可空]
		/// gatherType:聚会形式
		/// date:时间
		/// startTime:开始时间
		/// endTIme:结束时间
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> ListByDate()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamServer(await this.Request.Content.ReadAsByteArrayAsync());
			string userName = "", groupName = "";
			int gatherType = -1;
			DateTime? date = null, startTime = null, endTime = null;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());

				if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }

				object result;

				if (dic.ContainsKey("date"))
				{
					date = DateTime.Parse(dic["date"].ToString());
					result = gatherBusiness.ListByDate(userName, groupName, gatherType, date);

					res.Content = result;
					return res;
				}

				if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
				if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

				result = gatherBusiness.ListByDate(userName, groupName, gatherType, startTime, endTime);

				res.Content = result;
				return res;
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少参数");
		}

		/// <summary>
		/// 签到列表
		/// </summary>
		/// param:
		/// userName:姓名
		/// groupName:小组[可空]
		/// gatherType:聚会形式
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> List()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamServer(await this.Request.Content.ReadAsByteArrayAsync());
			string userName = "", groupName = "";
			int gatherType = -1;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());

				if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }

				var result = gatherBusiness.List(userName, groupName, gatherType);

				res.Content = result;
				return res;
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
		}
    }
}
