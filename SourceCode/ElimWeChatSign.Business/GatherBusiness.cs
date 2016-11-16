using System;
using System.Collections.Generic;
using System.Linq;
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
			var resDate = gList.Select(item => new ResGether
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
			var resDate = gList.Select(item => new ResGether
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
			var resDate = gList.Select(item => new ResGether
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
	}
}
