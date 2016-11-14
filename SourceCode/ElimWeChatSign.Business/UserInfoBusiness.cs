using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.Model;
using ElimWeChatSign.Service;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 用户相关业务逻辑
	/// </summary>
	public class UserInfoBusiness
	{
		private UserInfoService userInfoSercive = new UserInfoService();

		/// <summary>
		/// 获取用户列表
		/// </summary>
		/// <param name="userName">传空获取全部</param>
		/// <returns></returns>
		public List<ResUserInfo> List(string userName)
		{
			var uList = userInfoSercive.List(userName);

			//输出对象
			var resDate = uList.Select(item => new ResUserInfo
			{
				UserId = item.UserId,
				Email = item.Email,
				Mobile = item.Mobile,
				UserName = item.UserName,
				UserType = item.UserType,
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;

		}

		/// <summary>
		/// 修改用户信息
		/// </summary>
		/// <param name="userId">用户标识</param>
		/// <param name="userName">用户姓名</param>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">登录密码</param>
		/// <param name="email">邮箱</param>
		/// <param name="userType">类型</param>
		public ResUserInfo Update(string userId, string userName, string mobile, string password, string email, int userType)
		{
			if (userId.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数无效");

			var model = new UserInfo
			{
				UserId = userId,
				UserName = userName,
				Mobile = mobile,
				Password = password,
				Email = email,
				UserType = userType
			};
			var userInfo = userInfoSercive.AddOrUpdate(model);

			//输出对象
			var resUserInfo = new ResUserInfo
			{
				UserId = userInfo.UserId,
				UserName = userInfo.UserName,
				Email = userInfo.Email,
				Mobile = userInfo.Mobile,
				UserType = userInfo.UserType,
				UpdateTime = userInfo.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resUserInfo;
		}

		/// <summary>
		/// 获取用户信息
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public ResUserInfo Get(string userId)
		{
			if (userId.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数无效");

			var userInfo = userInfoSercive.GetUserInfo(userId);

			//输出对象
			var resUserInfo = new ResUserInfo
			{
				UserId = userInfo.UserId,
				UserName = userInfo.UserName,
				Email = userInfo.Email,
				Mobile = userInfo.Mobile,
				UserType = userInfo.UserType,
				UpdateTime = userInfo.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resUserInfo;
		}

		/// <summary>
		/// 判断根据授权Token判断用户是否存在
		/// </summary>
		/// <param name="authToken">授权Token</param>
		/// <returns>不存在返回true</returns>
		public bool IsUserIdExistByAuthToken(string authToken)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			var resule = userInfoSercive.IsUserIdExistByAuthToken(authToken);
			return resule;
		}

		/// <summary>
		/// 获取验证码
		/// </summary>
		/// <param name="mobile">手机号码</param>
		public string SendVCode(string mobile)
		{
			//参数判断
			if (string.IsNullOrEmpty(mobile))
				throw new CustomerException(ResponseCode.MobileError, "手机号码格式不正确");

			var vCode = userInfoSercive.SendVCode(mobile);

			return vCode;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">用户密码[AES加密后]</param>
		/// <param name="os">操作系统</param>
		/// <param name="osVersion">操作系统版本</param>
		/// <param name="deviceId">App设备号</param>
		/// <param name="appVersion">App版本号</param>
		/// <param name="deviceToken">友盟授权Token</param>
		/// <param name="loginIp">最后登录IP地址</param>
		public ResUserInfo Login(string mobile, string password, string os, string osVersion, string deviceId,
			string appVersion, string deviceToken, string loginIp)
		{
			if (mobile.IsNull() || password.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			var cryPwd = password.ToMd5();//MD5加密

			var userInfo = userInfoSercive.Login(mobile, cryPwd, os, osVersion, deviceId, appVersion, deviceToken, loginIp);

			//输出对象
			var resUserInfo = new ResUserInfo
			{
				UserId = userInfo.UserId,
				UserName = userInfo.UserName,
				Mobile = userInfo.Mobile,
				Avatar = userInfo.Avatar,
				Gender = userInfo.Gender,
				Email = userInfo.Email,
				UserType = userInfo.UserType,
				Os = userInfo.Os,
				OsVersion = userInfo.OsVersion,
				AppVersion = appVersion,
				UpdateTime = userInfo.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

			return resUserInfo;
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">密码[AES加密]</param>
		/// <param name="userName">用户姓名</param>
		/// <param name="vCode">验证码</param>
		/// <param name="os">操作系统</param>
		/// <param name="osVersion">操作系统版本</param>
		/// <param name="deviceId">App设备号</param>
		/// <param name="appVersion">App版本号</param>
		/// <param name="deviceToken">友盟授权Token</param>
		/// <param name="loginIp">最后登录IP地址</param>
		public string Reg(string mobile, string password, string userName, string vCode, string os, string osVersion,
			string deviceId, string appVersion, string deviceToken, string loginIp)
		{
			if (mobile.IsNull() || password.IsNull() || userName.IsNull() || vCode.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");
			if (!mobile.IsMobileNum())
				throw new CustomerException(ResponseCode.MobileError, "手机号码格式不正确");
			if (!userName.IsNameCn())
				throw new CustomerException(ResponseCode.UserNameError, "姓名格式不正确");

			var isExist = userInfoSercive.Exist(mobile);

			//判断该用户是否已存在
			if (isExist)
				throw new CustomerException(ResponseCode.UserInvalid, "用户名已存在");

			var cryPwd = password.ToMd5();//MD5加密
			var res = userInfoSercive.Reg(mobile, cryPwd, userName, vCode, os, osVersion, deviceId, appVersion, deviceToken, loginIp);

			return res;

		}

		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="oldPassword">原密码：md5之前</param>
		/// <param name="newPassword">新密码：md5之前</param>
		/// <returns></returns>
		public string UpdatePassword(string mobile, string oldPassword, string newPassword)
		{
			//判断参数
			if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			oldPassword = oldPassword.ToMd5();
			newPassword = newPassword.ToMd5();

			return userInfoSercive.UpdatePassword(mobile, oldPassword, newPassword);
		}

		/// <summary>
		/// 密码重置
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">新密码：md5之前</param>
		/// <param name="vCode">验证码</param>
		/// <returns></returns>
		public string ResetPassword(string mobile, string password, string vCode)
		{
			//判断参数
			if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(vCode) || string.IsNullOrEmpty(password))
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			if (password.Length < 6)
				throw new CustomerException(ResponseCode.NameOrPwdError, "密码长度至少6位");

			password = password.ToMd5();

			return userInfoSercive.ResetPassword(mobile, password, vCode);
		}

		/// <summary>
		/// 用户退出登录
		/// </summary>
		/// <param name="authToken">授权Token</param>
		/// <returns></returns>
		public string ExitLogin(string authToken)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			var result = userInfoSercive.ExitLogin(authToken);

			return result;
		}
	}
}
