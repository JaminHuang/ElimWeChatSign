﻿using System;
using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.Model;
using Titan;

namespace ElimWeChatSign.Service
{
	/// <summary>
	/// 聚会签到服务
	/// </summary>
	public class GatherService : BaseService
	{
		/// <summary>
		/// 添加或修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public Gather AddOrUpdate(Gather model)
		{
			if (model.GatherId.IsNull())
			{
				model.GatherId = ExtendUtil.GuidToString();
			    Session.Insert(model);
			}
			else
			{
				Session.Update(model);
			}
			return model;
		}

		/// <summary>
		/// 获取列表
		/// </summary>
		/// <param name="userName">姓名[模糊查询]</param>
		/// <param name="groupName">小组名称</param>
		/// <param name="type">聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		public List<Gather> List(string userName, string groupName, int type = -1, DateTime? startTime = null, DateTime? endTime = null)
		{
			var query = new QueryExpression();
            query.EntityType = typeof(Gather);
			query.Selects.Add(Gather_.ALL);
			if (!userName.IsNull())
				query.Wheres.Add(Gather_.UserName.TLike(userName));
			if (!groupName.IsNull())
				query.Wheres.Add(Gather_.GroupName.TEqual(groupName));
			if (type != -1)
				query.Wheres.Add(Gather_.GatherType.TEqual(type));
			if (startTime != null)
				query.Wheres.Add(Gather_.SignTime.TGreaterThan(startTime));
			if (endTime != null)
				query.Wheres.Add(Gather_.SignTime.TLessThan(endTime));

            query.OrderBys.Add(Gather_.SignTime.Desc);
            
			var gList = new List<Gather>();
		    Session.SelectCollection(gList, query);
			return gList;
		}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="gatherId"></param>
        /// <returns></returns>
	    public bool Delete(string gatherId)
	    {
	        var model = new Gather { GatherId = gatherId };
            return Session.Delete(model) > 0;
	    }

		/// <summary>
		/// 获取指定时间的签到人数
		/// </summary>
		/// <param name="type">聚会形式</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <returns></returns>
		public int GetSignCount(int type, DateTime? startTime, DateTime? endTime)
		{
			var query = new QueryExpression();
            query.EntityType = typeof(Gather);
            query.Selects.Add(Gather_.ALL);
			if (type != -1)
				query.Wheres.Add(Gather_.GatherType.TEqual(type));
			if (startTime != null)
				query.Wheres.Add(Gather_.SignTime.TGreaterThan(startTime));
			if (endTime != null)
				query.Wheres.Add(Gather_.SignTime.TLessThan(endTime));

			var gList = new List<Gather>();
		    Session.SelectCollection(gList, query);
			return gList.GroupBy(x=>x.UserName).Count();
		}
	}
}
