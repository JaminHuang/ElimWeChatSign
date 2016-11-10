using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
    public class UserInfoBusiness
	{
		private IUserInfoSercive userInfoSercive;

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">用户密码</param>
		public ResUserInfo Login(string mobile, string password)
		{
			if (mobile.IsNull() || password.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

			var cryPassword = CryptographyUtil.AESEncryServer(password);
			var userInfo = userInfoSercive.Login(mobile, cryPassword);

			if (userInfo == null)
				throw new CustomerException(ResponseCode.NameOrPwdError, "用户名和帐号密码错误");

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
		/// 注册
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="password">密码</param>
		/// <param name="userName">用户姓名</param>
		public ResUserInfo Reg(string mobile, string password, string userName)
		{
			if (mobile.IsNull() || password.IsNull() || userName.IsNull())
				throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");
			if (!mobile.IsMobileNum())
				throw new CustomerException(ResponseCode.MobileError, "手机号码格式不正确");
			if (!userName.IsNameCn())
				throw new CustomerException(ResponseCode.UserNameError, "姓名格式不正确");

			var isExist = userInfoSercive.Exist(mobile);

			//判断该用户是否已存在
			if (isExist)
				throw new CustomerException(ResponseCode.UserInvalid, "用户名已存在");
			
			var cryPassword = CryptographyUtil.AESEncryServer(password);
			var model = new UserInfo
			{
				UserName = userName,
				Mobile = mobile,
				Password = cryPassword,
				Email = "",
				UserType = 1
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
	}
}
