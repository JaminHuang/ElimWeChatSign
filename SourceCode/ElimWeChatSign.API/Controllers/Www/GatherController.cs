using ElimWeChatSign.IBusiness;
using JaminHuang.Core;
using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElimWeChatSign.API.Controllers
{
    /// <summary>
    /// 聚会签到[单独接口]
    /// </summary>
    [RoutePrefix("www/gather")]
    public class GatherController : WwwBaseController
    {
		public GatherController(IGatherBusiness gather)
        {
            _gather = gather;
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// param:
        /// userName:姓名
        /// groupName:小组[可空]
        /// gatherType:聚会形式
        /// <returns></returns>
        [Route("sign"), HttpPost]
		public async Task<ResponseMessage> Sign()
		{
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			string gatherId = "", userName = "", groupName = "", churchId = "";
			string ipAddress = ExtendUtil.GetIP();//获取请求IP地址
			int gatherType = -1, gender = 3;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType") && dic.ContainsKey("gender") && dic.ContainsKey("churchId"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());
				gender = int.Parse(dic["gender"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }
				if (dic.ContainsKey("gatherId")) { gatherId = dic["gatherId"].ToString(); }
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = _gather.Sign(churchId, gatherId, userName, gender, groupName, gatherType, ipAddress);

			res.Data = result;
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
		[Route("date/list"), HttpPost]
		public async Task<ResponseMessage> ListByDate()
		{
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			string userName = "", groupName = "", churchId = "";
            int gatherType = -1;
			DateTime? date = null, startTime = DateTime.Now.Date.AddMonths(-1), endTime = DateTime.Now.Date.AddDays(1);
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType") && dic.ContainsKey("churchId"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }

				object result;

				if (dic.ContainsKey("date"))
				{
					date = DateTime.Parse(dic["date"].ToString());
					result = _gather.ListByDate(churchId, userName, groupName, gatherType, date);

					res.Data = result;
					return res;
				}

				if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
				if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

				result = _gather.ListByDate(churchId, userName, groupName, gatherType, startTime, endTime);

				res.Data = result;
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
		[Route("list"), HttpPost]
		public async Task<ResponseMessage> List()
		{
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			string userName = "", groupName = "", churchId = "";
            int gatherType = -1;
			if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType") && dic.ContainsKey("churchId"))
			{
				userName = dic["userName"].ToString();
				gatherType = int.Parse(dic["gatherType"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }

				var result = _gather.List(churchId, userName, groupName, gatherType);

				res.Data = result;
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
        [Route("delete"), HttpPost]
        public async Task<ResponseMessage> Delete()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string gatherId = "";
            if (dic != null && dic.ContainsKey("gatherId"))
            {
                gatherId = dic["gatherId"].ToString();

                var result = _gather.Delete(gatherId);

                res.Data = result;
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
		[Route("count"), HttpPost]
		public async Task<ResponseMessage> GetSignCount()
		{
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			DateTime? startTime = DateTime.Today, endTime = DateTime.Today.AddDays(1);
			if (dic != null && dic.ContainsKey("gatherType") && dic.ContainsKey("churchId"))
			{
				var gatherType = int.Parse(dic["gatherType"].ToString());
                var churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
				if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

				var result = _gather.GetSignCount(churchId, gatherType, startTime, endTime);


				res.Data = result;
				return res;
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少参数");
		}

		/// <summary>
		/// 获取签到人员名单
		/// </summary>
		/// param:
		/// gatherType:聚会形式
		/// date:日期
		/// <returns></returns>
		[Route("name/list"), HttpPost]
		public async Task<ResponseMessage> GetSignNameList()
		{
			var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
			DateTime date = DateTime.Today;
			if (dic != null && dic.ContainsKey("gatherType") && dic.ContainsKey("date") && dic.ContainsKey("churchId"))
			{
				var gatherType = int.Parse(dic["gatherType"].ToString());
				date = DateTime.Parse(dic["date"].ToString());
                var churchId = dic["churchId"].ToString();

                var result = _gather.GetSignNameList(churchId, gatherType, date);

				res.Data = result;
				return res;
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少参数");
		}

        /// <summary>
		/// 获取用户签到列表
		/// </summary>
		/// param:
		/// userName:姓名
		/// groupName:小组[可空]
		/// gatherType:聚会形式
		/// date:时间
		/// startTime:开始时间
		/// endTIme:结束时间
		/// <returns></returns>
		[Route("user/list"), HttpPost]
        public async Task<ResponseMessage> GatherUserList()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string userName = "", groupName = "", churchId = "";
            int gatherType = -1;
            DateTime? date = null, startTime = null, endTime = null;
            if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("gatherType") && dic.ContainsKey("churchId"))
            {
                userName = dic["userName"].ToString();
                gatherType = int.Parse(dic["gatherType"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("groupName")) { groupName = dic["groupName"].ToString(); }
                if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
                if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

                var result = _gather.GatherUserList(churchId, userName, groupName, gatherType, startTime, endTime);

                res.Data = result;
                return res;
            }
            throw new CustomerException(ResponseCode.MissParam, "缺少参数");
        }

        /// <summary>
		/// 获取某时间段小组聚会总人数
		/// </summary>
		/// param:
		/// gatherType:聚会形式
		/// date:时间
		/// startTime:开始时间
		/// endTIme:结束时间
		/// <returns></returns>
		[Route("group/list"), HttpPost]
        public async Task<ResponseMessage> GetGatherGroupList()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string groupName = "", churchId = "";
            int gatherType = -1;
            DateTime? date = null, startTime = null, endTime = null;
            if (dic != null && dic.ContainsKey("gatherType"))
            {
                gatherType = int.Parse(dic["gatherType"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
                if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

                var result = _gather.GetGatherGroupList(churchId, gatherType, startTime, endTime);

                res.Data = result;
                return res;
            }
            throw new CustomerException(ResponseCode.MissParam, "缺少参数");
        }

        /// <summary>
		/// 获取聚会小组签到报表
		/// </summary>
		/// param:
		/// gatherType:聚会形式
		/// date:时间
		/// startTime:开始时间
		/// endTIme:结束时间
		/// <returns></returns>
		[Route("group/rpt"), HttpPost]
        public async Task<ResponseMessage> GetRptGather()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string groupName = "", churchId = "";
            int gatherType = -1;
            DateTime? date = null, startTime = null, endTime = null;
            if (dic != null && dic.ContainsKey("gatherType"))
            {
                gatherType = int.Parse(dic["gatherType"].ToString());
                churchId = dic["churchId"].ToString();

                if (dic.ContainsKey("startTime")) { startTime = DateTime.Parse(dic["startTime"].ToString()); }
                if (dic.ContainsKey("endTime")) { endTime = DateTime.Parse(dic["endTime"].ToString()); }

                var result = _gather.GetRptGather(churchId, gatherType, startTime, endTime);

                res.Data = result;
                return res;
            }
            throw new CustomerException(ResponseCode.MissParam, "缺少参数");
        }
    }
}
