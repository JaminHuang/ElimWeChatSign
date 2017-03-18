using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ElimWeChatSign.Core;
using ElimWeChatSign.Model;
using ElimWeChatSign.Service;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 聚会签到业务逻辑
	/// </summary>
	public class GatherBusiness
	{
		private GatherService gatherService = new GatherService();

		/// <summary>
		/// 签到
		/// </summary>
		/// <param name="gatherId">签到标识[空为添加]</param>
		/// <param name="userName"></param>
		/// <param name="groupName"></param>
		/// <param name="gatherType">聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)</param>
		/// <param name="ipAddress">请求IP地址</param>
		/// <returns></returns>
		public ResGether Sign(string gatherId, string userName, string groupName, int gatherType, string ipAddress)
		{
			if (userName.IsNull() || gatherType < 0)
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			//判断IP是否为规定地址
			//var allowIp = AppConfiguration.GetKey("AllowIP");
			//if (allowIp != ipAddress)
			//	throw new CustomerException(ResponseCode.IpAddressError, "该IP不允许签到");

			var model = new Gather
			{
				GatherId = gatherId,
				UserName = userName,
				GroupName = groupName,
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
		/// <param name="userName">用户姓名</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="gatherType">聚会形式</param>
		/// <param name="date">报表日期</param>
		/// <returns></returns>
		public List<ResGether> ListByDate(string userName, string groupName, int gatherType, DateTime? date)
		{
			DateTime? startTime = null, endTime = null;
			if (date != null)
			{
				startTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", date.Value.Year, date.Value.Month, date.Value.Day));
				endTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 23:59:59", date.Value.Year, date.Value.Month, date.Value.Day));
			}

			var gList = gatherService.List(userName, groupName, gatherType, startTime, endTime);

			//输出对象
			var resDate = gList.Where(x => x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
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
		/// <param name="userName">用户姓名</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="gatherType">聚会形式</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		public List<ResGether> ListByDate(string userName, string groupName, int gatherType, DateTime? startTime, DateTime? endTime)
		{
			var gList = gatherService.List(userName, groupName, gatherType, startTime, endTime);

			//输出对象
			var resDate = gList.Where(x=>x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
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
		/// <param name="userName">用户姓名</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="gatherType">聚会形式</param>
		/// <returns></returns>
		public List<ResGether> List(string userName, string groupName, int gatherType)
		{
			var gList = gatherService.List(userName, groupName, gatherType, null, null);

			//输出对象
			var resDate = gList.Where(x => x.SignTime.DayOfWeek == DayOfWeek.Sunday).Select(item => new ResGether
			{
				GatherId = item.GatherId,
				UserName = item.UserName,
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
		/// <param name="userName">用户姓名</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="gatherType">聚会形式</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		public List<ResGetherUser> GatherUserList(string userName, string groupName, int gatherType, DateTime? startTime = null, DateTime? endTime = null)
        {
            var gList = gatherService.List(userName, groupName, gatherType, startTime, endTime);

            //计算时间段内所有聚会次数
            //if(startTime == null) { startTime = DateTime.Parse("2016/11/20"); }
            //if(endTime == null) { endTime = DateTime.Now.Date; }

            //var allCount = TotalWeeks(startTime.Value, endTime.Value, DayOfWeek.Sunday);
            var allCount = gList.Where(x=>x.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(y=>y.SignTime.Date).Count();

            var resDate = new List<ResGetherUser>();

            foreach (var item in gList.GroupBy(x=> new {x.UserName, x.GroupName, x.GatherType }))
            {
                var count = item.ToList().FindAll(a=>a.SignTime.DayOfWeek == DayOfWeek.Sunday).GroupBy(x=>x.SignTime.Date).Count(); //某时间段内总签到次数

                var model = new ResGetherUser
                {
                    UserId = ExtendUtil.GuidToString(),
                    UserName = item.Key.UserName,
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
		/// <param name="type">聚会形式</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		public int GetSignCount(int type, DateTime? startTime, DateTime? endTime)
		{
			var count = gatherService.GetSignCount(type, startTime, endTime);

			return count;
		}

		/// <summary>
		/// 获取签到人员名单
		/// </summary>
		/// <param name="type">聚会形式</param>
		/// <param name="date">日期</param>
		/// <returns></returns>
		public List<string> GetSignNameList(int type, DateTime date)
		{
			DateTime? startTime = null, endTime = null;
			startTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", date.Year, date.Month, date.Day));
			endTime = Convert.ToDateTime(string.Format("{0}/{1}/{2} 23:59:59", date.Year, date.Month, date.Day));
			var gList = gatherService.List("", "", type, startTime, endTime);

			var nList = gList.GroupBy(x => x.UserName).Select(item => item.Key).ToList();

			return nList;
		}

        #region 辅助方法

        ///<summary> 
        /// 统计一段时间内有多少个星期几 
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
