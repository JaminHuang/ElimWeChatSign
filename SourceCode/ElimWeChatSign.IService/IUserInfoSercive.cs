using System.Collections.Generic;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.IService
{
    /// <summary>
    /// 用户相关服务接口
    /// </summary>
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
		/// 根据手机号判断是否存在
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		bool Exist(string mobile);

        /// <summary>
        /// 根据条件搜索列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
	    List<UserInfo> List(string userName);

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="pwd">用户密码</param>
		/// <param name="os">操作系统</param>
		/// <param name="osVersion">操作系统版本</param>
		/// <param name="deviceId">App设备号</param>
		/// <param name="appVersion">App版本号</param>
		/// <param name="deviceToken">友盟授权Token</param>
		/// <param name="loginIp">最后登录IP地址</param>
		/// <returns></returns>
		UserInfo Login(string mobile, string pwd, string os, string osVersion, string deviceId,
			string appVersion, string deviceToken, string loginIp);
	}
}
