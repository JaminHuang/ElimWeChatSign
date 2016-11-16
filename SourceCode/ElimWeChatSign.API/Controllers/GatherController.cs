using System;
using System.Threading.Tasks;
using System.Web;
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
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			string gatherId = "", userName = "", groupName = "";
			string ipAddress = Extension.GetIp();//获取请求IP地址
			int gatherType = -1;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());

				if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }
				if (dic.ContainsKey("gatherId")) { gatherId = dic["gatherId"].ToString(); }
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = gatherBusiness.Sign(gatherId, userName, groupName, gatherType, ipAddress);

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
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
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
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
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

        /// <summary>
        /// 删除签到
        /// </summary>
        /// param:
        /// gatherId:姓名
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseMessage> Delete()
        {
            var res = new ResponseMessage();
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string gatherId = "";
            if (dic != null && dic.ContainsKey("gatherId"))
            {
                gatherId = dic["gatherId"].ToString();

                var result = gatherBusiness.Delete(gatherId);

                res.Content = result;
                return res;
            }
            throw new CustomerException(ResponseCode.MissParam, "缺少参数");
        }

		/// <summary>
		/// 获取签到人数
		/// </summary>
		/// param:
		/// gatherType:聚会形式
		/// startTime:开始时间
		/// endTIme:结束时间
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> GetSignCount()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			DateTime? date = null, startTime = null, endTime = null;
			if (dic != null && dic.ContainsKey("gatherType"))
			{
				var gatherType = int.Parse(dic["gatherType"].ToString());

				if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
				if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

				var result = gatherBusiness.GetSignCount(gatherType, startTime, endTime);

				res.Content = result;
				return res;
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少参数");
		}
    }
}
