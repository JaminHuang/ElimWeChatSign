using System;
using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;
using Titan;

namespace ElimWeChatSign.Service
{
    /// <summary>
    /// 用户相关服务
    /// </summary>
	public class UserInfoService : IUserInfoSercive
	{
		/// <summary>
		/// 添加或修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public UserInfo AddOrUpdate(UserInfo model)
		{
			if (model.UserId.IsNull())
			{
				model.UserId = ExtendUtil.GuidToString();
				model.UpdateTime = DateTime.Now;
				model.Insert();
			}
			else
			{
				model.UpdateTime = DateTime.Now;
				model.Update();
			}
			return model;
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool Delete(string userId)
		{
			var userInfo = new UserInfo(userId);
			return userInfo.Delete() > 0;
		}

		/// <summary>
		/// 根据主键获取用户
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public UserInfo GetUserInfo(string userId)
		{
			var userInfo = new UserInfo();
			return userInfo.Select() ? userInfo : null;
		}

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="pwd"></param>
		/// <returns></returns>
		public UserInfo Login(string mobile, string pwd)
		{
			var uiList = new UserInfos();
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile)
						.And(UserInfoProperties.Password.Equal(pwd)));
			uiList.Select(query);
			return uiList.FirstOrDefault();
		}

		/// <summary>
		/// 根据手机号判断是否存在
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public bool Exist(string mobile)
		{
			var uiList = new UserInfos();
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile));
			uiList.Select(query);
			return uiList.Items.Any();
		}

		/// <summary>
		/// 获取用户列表
		/// </summary>
		/// <returns></returns>
		public List<UserInfo> List()
		{
			var uiList = new UserInfos();
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			uiList.Select(query);
			return uiList.Items;
		}

	    /// <summary>
	    /// 根据条件搜索列表
	    /// </summary>
	    /// <param name="userName">姓名[模糊]</param>
	    /// <returns></returns>
	    public List<UserInfo> List(string userName)
	    {
            var uiList = new UserInfos();
            var query = new QueryExpression();
            query.Selects.Add(UserInfoProperties.ALL);
            query.Wheres.Add(UserInfoProperties.UserName.Like(userName));
            uiList.Select(query);
            return uiList.Items;
        }
    }
}
