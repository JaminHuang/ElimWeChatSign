using System.Collections.Generic;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.IService
{
	public interface IUserInfoSercive
	{
		/// <summary>
		/// 添加或修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		UserInfo AddOrUpdate(UserInfo model);

		/// <summary>
		/// 根据主键删除
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		bool Delete(string userId);

		/// <summary>
		/// 根据主键获取用户
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		UserInfo GetUserInfo(string userId);

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="pwd"></param>
		/// <returns></returns>
		UserInfo Login(string mobile, string pwd);

		/// <summary>
		/// 根据手机号判断是否存在
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		bool Exist(string mobile);

		/// <summary>
		/// 获取用户列表
		/// </summary>
		/// <returns></returns>
		List<UserInfo> List();
	}
}
