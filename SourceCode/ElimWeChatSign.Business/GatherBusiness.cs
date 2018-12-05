using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ElimWeChatSign.IBusiness;
using ElimWeChatSign.Model;
using ElimWeChatSign.Service;
using JaminHuang.Core;
using JaminHuang.Util;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 聚会签到业务逻辑
	/// </summary>
	public class GatherBusiness : IGatherBusiness
    {
		private GatherService gatherService = new GatherService();

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
        public ResGether Sign(string churchId, string gatherId, string userName, int gender, string groupName, int gatherType, string ipAddress)
		{
			if (userName.IsNull() || gatherType < 0)
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

            //判断用户今天是否已经签到
		    var isSign = gatherService.IsExitsSign(churchId, userName, DateTime.Now);

		    if (isSign)
		        throw new CustomerException(ResponseCode.ResDataIsEmpty, "该用户今天已经签到");

			var model = new Gather
			{
				GatherId = gatherId,
                ChurchId = churchId,
				UserName = userName,
				GroupName = groupName,
                Gender = gender,
				GatherType = gatherType,
				IpAddress = ipAddress,
				SignTime = DateTime.Now
			};

			var gather = gatherService.AddOrUpdate(model);

			//输出对象
			var resGather = new ResGether
			{
				GatherId = gather.GatherId,
				UserName = gather.UserName,
                Gender = gather.Gender,
				GroupName = gather.GroupName,
				GatherType = gather.GatherType,
				IpAddress = gather.IpAddress,
				SignTime = gather.SignTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resGather;
		}

        /// <summary>
        /// 获取签到列表[按天]
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="date">报表日期</param>
        /// <returns></returns>
        public List<ResGether> ListByDate(string churchId, string userName, string groupName, int gatherType, DateTime? date)
		{
			DateTime? startTime = null, endTime = null;
			if (date != null)
			{
				startTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", date.Value.Year, date.Value.Month, date.Value.Day));
				endTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 23:59:59", date.Value.Year, date.Value.Month, date.Value.Day));
			}

			var gList = gatherService.List(churchId, userName, groupName, gatherType, startTime, endTime);

			//输出对象
			var resDate = gList.Where(x => x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
			    Gender = item.Gender,
                GroupName = item.GroupName,
				GatherType = item.GatherType,
				IpAddress = item.IpAddress,
				SignTime = item.SignTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
		}

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
        public List<ResGether> ListByDate(string churchId, string userName, string groupName, int gatherType, DateTime? startTime, DateTime? endTime)
		{
			var gList = gatherService.List(churchId, userName, groupName, gatherType, startTime, endTime);

			//输出对象
			var resDate = gList.Where(x=>x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
			    Gender = item.Gender,
                GroupName = item.GroupName,
				GatherType = item.GatherType,
				IpAddress = item.IpAddress,
				SignTime = item.SignTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
		}

        /// <summary>
        /// 获取签到列表[时间段]
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="gatherType">聚会形式</param>
        /// <returns></returns>
        public List<ResGether> List(string churchId, string userName, string groupName, int gatherType)
		{
			var gList = gatherService.List(churchId, userName, groupName, gatherType, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date.AddDays(1));

			//输出对象
			var resDate = gList.Where(x => x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
			    Gender = item.Gender,
                GroupName = item.GroupName,
				GatherType = item.GatherType,
				IpAddress = item.IpAddress,
				SignTime = item.SignTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
		}

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
		public List<ResGetherUser> GatherUserList(string churchId, string userName, string groupName, int gatherType, DateTime? startTime = null, DateTime? endTime = null)
        {
            var gList = gatherService.List(churchId, userName, groupName, gatherType, startTime, endTime);

            //计算时间段内所有聚会次数
            var allCount = gList.Where(x=>x.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(y=>y.SignTime.Date).Count();

            var resDate = new List<ResGetherUser>();

            foreach (var item in gList.GroupBy(x=> new {x.UserName, x.Gender, x.GroupName, x.GatherType }))
            {
                var count = item.ToList().FindAll(a=>a.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(x=>x.SignTime.Date).Count(); //某时间段内总签到次数

                var model = new ResGetherUser
                {
                    UserId = ExtendUtil.GuidToString(),
                    UserName = item.Key.UserName,
                    Gender = item.Key.Gender,
                    GroupName = item.Key.GroupName,
                    GatherType = item.Key.GatherType,
                    Count = count,
                    AllCount = allCount,
                    SignRate = (Convert.ToDouble(count) / Convert.ToDouble(allCount)).ToString("p")
                };

                if (count != 0)
                {
                    resDate.Add(model);
                }
                
            }

            return resDate;
        }

        /// <summary>
        /// 删除签到
        /// </summary>
        /// <param name="gatherId">签到标识</param>
        /// <returns></returns>
        public string Delete(string gatherId)
		{
			if (gatherId.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			var rs = gatherService.Delete(gatherId);

			return rs ? "" : "删除失败";
		}

        /// <summary>
        /// 获取签到人数
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="type">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int GetSignCount(string churchId, int type, DateTime? startTime, DateTime? endTime)
		{
			var count = gatherService.GetSignCount(churchId, type, startTime, endTime);

			return count;
		}

        /// <summary>
        /// 获取签到人员名单
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="type">聚会形式</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public List<ResGatherList> GetSignNameList(string churchId, int type, DateTime date)
		{
			DateTime? startTime = null, endTime = null;
			startTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", date.Year, date.Month, date.Day));
			endTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 23:59:59", date.Year, date.Month, date.Day));
			var gList = gatherService.List(churchId, "", "", type, startTime, endTime);

		    //输出对象
		    var resDate = gList.GroupBy(x => new { x.UserName, x.Gender }).Select(item => new ResGatherList
            {
                UserName = item.Key.UserName,
                Gender = item.Key.Gender
		    }).ToList();

		    return resDate;
		}

        /// <summary>
        /// 统计某时间段小组聚会总人数
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public List<ResRptGatherGroup> GetGatherGroupList(string churchId, int gatherType, DateTime? startTime = null, DateTime? endTime = null)
        {
            var gList = gatherService.List(churchId, "", "", gatherType, startTime, endTime);

            var resDate = new List<ResRptGatherGroup>();

            foreach (var item in gList.Where(a => a.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(x => new { x.UserName, x.GroupName }))
            {
                resDate.Add(new ResRptGatherGroup
                {
                    GroupName = item.Key.GroupName,
                    GroupNum = item.Count()
                });
            }

            var res = new List<ResRptGatherGroup>();

            foreach (var model in resDate.GroupBy(x => x.GroupName))
            {
                res.Add(new ResRptGatherGroup
                {
                    GroupName = model.Key,
                    GroupNum = model.Count()
                });
            }

            return res;
        }

        /// <summary>
        /// 获取小组聚会报表
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="gatherType">聚会形式</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
	    public List<ResRptGather> GetRptGather(string churchId, int gatherType, DateTime? startTime = null, DateTime? endTime = null)
        {
            var gList = gatherService.List( churchId, "", "", gatherType, startTime, endTime);

            var resDate = new List<ResRptGather>();

            foreach (var item in gList.FindAll(x => x.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(y => y.SignTime.Date))
            {
                var rptDate = item.Key;
                var list = new List<ResRptGatherGroup>();

                foreach (var g in item.GroupBy(z => new { z.UserName, z.GroupName }))
                {
                    var model = new ResRptGatherGroup { GroupName = g.Key.GroupName, GroupNum = g.Count() };
                    list.Add(model);
                }

                resDate.Add(new ResRptGather { GatherDate = rptDate.ToString("yyyy-MM-dd"), List = list });
            }

            return resDate;
        }

        #region 辅助方法

        ///<summary> 
        /// 统计一段时间内有多少个星期日
        ///</summary> 
        ///<param   name= "AStart "> 开始日期 </param> 
        ///<param   name= "AEnd "> 结束日期 </param> 
        ///<param   name= "AWeek "> 星期几 </param> 
        ///<returns> 返回个数 </returns> 
        private int TotalWeeks(DateTime AStart, DateTime AEnd, DayOfWeek AWeek)
        {
            TimeSpan vTimeSpan = new TimeSpan(AEnd.Ticks - AStart.Ticks);
            int Result = (int)vTimeSpan.TotalDays / 7;
            for (int i = 0; i <= vTimeSpan.TotalDays % 7; i++)
                if (AStart.AddDays(i).DayOfWeek == AWeek)
                    return Result + 1;
            return Result;
        }

        #endregion
    }
}
