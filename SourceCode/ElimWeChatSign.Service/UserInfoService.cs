using System;
using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.Model;
using Titan;

namespace ElimWeChatSign.Service
{
	/// <summary>
	/// 用户相关服务
	/// </summary>
	public class UserInfoService
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

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="mobile">手机号码</param>
		/// <param name="pwd">用户密码[MD5后]</param>
		/// <param name="os">操作系统</param>
		/// <param name="osVersion">操作系统版本</param>
		/// <param name="deviceId">App设备号</param>
		/// <param name="appVersion">App版本号</param>
		/// <param name="deviceToken">友盟授权Token</param>
		/// <param name="loginIp">最后登录IP地址</param>
		/// <returns></returns>
		public UserInfo Login(string mobile, string pwd, string os, string osVersion, string deviceId,
			string appVersion, string deviceToken, string loginIp)
		{
			using (var cn = DbSessionHelper.OpenSession())
			{
				cn.BeginTransaction();
				try
				{
					var uiList = new UserInfos();
					var query = new QueryExpression();
					query.Selects.Add(UserInfoProperties.ALL);
					query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile)
								.And(UserInfoProperties.Password.Equal(pwd)));
					uiList.Select(cn, query);

					var userInfo = uiList.FirstOrDefault();
					if (userInfo == null)
						throw new CustomerException(ResponseCode.NameOrPwdError, "用户名和密码错误");

					//重新赋值更新
					userInfo.Os = os;
					userInfo.OsVersion = osVersion;
					userInfo.DeviceID = deviceId;
					userInfo.AppVersion = appVersion;
					userInfo.DeviceToken = deviceToken;
					userInfo.LoginIp = loginIp;
					userInfo.AuthToken = ExtendUtil.GenerateAuthToken();
					userInfo.UpdateTime = DateTime.Now;

					userInfo.Update(cn, UserInfoProperties.ALL);

					cn.Commit();
					return userInfo;
				}
				catch (CustomerException ex)
				{
					cn.Rollback();
					throw new CustomerException(ex.Code, ex.Msg);
				}
				catch (Exception ex)
				{
					cn.Rollback();
					throw new CustomerException(ResponseCode.ServerInternalError, "服务器内部错误:" + ex.Message);
				}
			}
		}

		/// <summary>
		/// 注册用户
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
			//判断用户验证码是否有效
			var vCodeQuery = new QueryExpression();
			vCodeQuery.Selects.Add(VerifyProperties.ALL);
			vCodeQuery.Wheres.Add(VerifyProperties.Mobile.Equal(mobile)
							 .And(VerifyProperties.Type.Equal("0")
							 .And(VerifyProperties.vCode.Equal(vCode))));
			var verifys = new Verifys();
			verifys.Select(vCodeQuery);
			if (verifys.Count < 1)
				throw new CustomerException(ResponseCode.VCodeError, "验证码错误");

			var verifyList = verifys.OrderByDescending(x => x.InsertTime.ToDateTime());
			var verfity = verifyList.ToList()[0];
			if (verfity.InsertTime.ToDateTime().AddMinutes(verfity.Expire) < DateTime.Now)
				throw new CustomerException(ResponseCode.VCodeError, "验证码失效");

			//根据登录名,获取登陆用户信息
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile));
			var userInfos = new UserInfos();
			userInfos.Select(query);

			//若已含有用户信息
			if (userInfos.Count > 0)
				throw new CustomerException(ResponseCode.UserInvalid, "用户已存在");

			var userInfo = new UserInfo
			{
				UserName = "",
				Password = password,
				Avatar = "",
				Gender = 0,
				Mobile = mobile,
				Os = os,
				OsVersion = osVersion,
				DeviceID = deviceId,
				DeviceToken = deviceToken,
				LoginIp = loginIp,
				AppVersion = appVersion,
				AuthToken = ExtendUtil.GenerateAuthToken(),
				UpdateTime = DateTime.Now
			};
			userInfo.Insert();
			return "";
		}

		/// <summary>
		/// 获取验证码
		/// </summary>
		/// <param name="mobile">手机号码</param>
		public string SendVCode(string mobile)
		{
			//随机生成6位数的验证码
			var vCode = ExtendUtil.GetRandom(6);

			//TODO:发送短信
			var msgContent = string.Format("验证码：{0},请您在5分钟内填写。如非本人操作，请忽略本短信。", vCode);

			//查询数据库中是否有该手机用户的验证信息
			var query = new QueryExpression();
			query.Selects.Add(VerifyProperties.ALL);
			query.Wheres.Add(VerifyProperties.Mobile.Equal(mobile));
			var verfities = new Verifys();
			verfities.Select(query);
			if (verfities.Count < 1)
			{
				//如果是新账户则插入验证信息
				var verfity = new Verify { Mobile = mobile, vCode = vCode, InsertTime = DateTime.Now.ToTimestamp(), Expire = 5, Type = 0 };
				verfity.Insert();
			}
			else
			{
				var v = verfities[0];
				v.vCode = vCode;
				v.Expire = 5;
				v.InsertTime = DateTime.Now.ToTimestamp();
				v.Update("vCode", "Expire", "InsertTime");
			}

			return vCode;
		}

		/// <summary>
		/// 用户退出登录
		/// </summary>
		/// <param name="authToken">授权Token</param>
		/// <returns></returns>
		public string ExitLogin(string authToken)
		{
			//根据授权Token,获取登陆用户信息
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.AuthToken.Equal(authToken));
			var userInfos = new UserInfos();
			userInfos.Select(query);

			if (userInfos.Count < 1)
			{
				return "";
			}

			//获取注销Token
			var userInfo = userInfos[0];
			userInfo.AuthToken = "";
			userInfo.UpdateTime = DateTime.Now;
			userInfo.Update(UserInfoProperties.ALL);
			return "";
		}

		/// <summary>
		/// 判断根据授权Token判断用户是否存在
		/// </summary>
		/// <param name="authToken">授权Token</param>
		/// <returns>不存在返回true</returns>
		public bool IsUserIdExistByAuthToken(string authToken)
		{
			var rs = true;
			if (string.IsNullOrEmpty(authToken))
				throw new CustomerException(ResponseCode.TokenInvalid, "授权Token无效");

			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.AuthToken.Equal(authToken));
			var userInfos = new UserInfos();
			userInfos.Select(query);

			if (userInfos.Count == 1)
			{
				rs = false;
			}

			return rs;
		}

		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="oldPassword">原始密码：md5后</param>
		/// <param name="newPassword">新密码：md5后</param>
		/// <returns></returns>
		public string UpdatePassword(string mobile, string oldPassword, string newPassword)
		{
			//根据登录名,获取登陆用户信息
			var query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile));
			query.Wheres.Add(UserInfoProperties.Password.Equal(oldPassword));
			var userInfos = new UserInfos();
			userInfos.Select(query);
			if (userInfos.Count < 1)
				throw new CustomerException(ResponseCode.NameOrPwdError, "用户不存在或者密码错误");

			var user = userInfos[0];
			user.Password = newPassword;
			query = new QueryExpression();
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile));
			user.Update("Password");

			return "";
		}

		/// <summary>
		/// 重置密码
		/// </summary>
		/// <param name="mobile"></param>
		/// <param name="password"></param>
		/// <param name="vCode"></param>
		/// <returns></returns>
		public string ResetPassword(string mobile, string password, string vCode)
		{
			//手机号码验证
			if (!mobile.IsMobileNum())
				throw new CustomerException(ResponseCode.MobileError, "手机号码无效");

			//判断用户验证码是否有效
			QueryExpression vCodeQuery = new QueryExpression();
			vCodeQuery.Selects.Add(VerifyProperties.ALL);
			vCodeQuery.Wheres.Add(VerifyProperties.Mobile.Equal(mobile)
							 .And(VerifyProperties.Type.Equal("0")
							 .And(VerifyProperties.vCode.Equal(vCode))));
			var verifys = new Verifys();
			verifys.Select(vCodeQuery);
			if (verifys.Count < 1)
				throw new CustomerException(ResponseCode.VCodeError, "验证码错误");

			var verifyList = verifys.OrderByDescending(x => x.InsertTime.ToDateTime());
			var verfity = verifyList.ToList()[0];
			if (verfity.InsertTime.ToDateTime().AddMinutes(verfity.Expire) < DateTime.Now)
				throw new CustomerException(ResponseCode.VCodeError, "验证码失效");

			//根据登录名,获取登陆用户信息
			QueryExpression query = new QueryExpression();
			query.Selects.Add(UserInfoProperties.ALL);
			query.Wheres.Add(UserInfoProperties.Mobile.Equal(mobile));
			var userInfos = new UserInfos();
			userInfos.Select(query);
			if (userInfos.Count < 1)
				throw new CustomerException(ResponseCode.UserInvalid, "用户不存在");

			var user = userInfos[0];
			user.Password = password;
			user.UpdateTime = DateTime.Now;
			user.Update("Password", "UpdateTime");

			return "";
		}
	}
}
