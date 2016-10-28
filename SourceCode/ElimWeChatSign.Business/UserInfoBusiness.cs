using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;
using ElimWeChatSign.Service;

namespace ElimWeChatSign.Business
{
	public class UserInfoBusiness
	{
		IUserInfoSercive userInfoSercive = new UserInfoService();

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="req"></param>
		/// <param name="res"></param>
		public void Login(ReqUserLogin req, ref ResponseMessage res)
		{
			if (req.mobile.IsNull() || req.password.IsNull())
			{
				res.Code = ResponseCode.RequireParamLack;
				res.Content = "缺少必填参数";
			}
			else
			{
				var cryPassword = CryptographyUtil.AESEncryServer(req.password);
				var userInfo = userInfoSercive.Login(req.mobile, cryPassword);
				if (userInfo == null)
				{
					res.Code = ResponseCode.NameOrPwdError;
					res.Content = "用户名和帐号密码错误";
				}
				res.Code = ResponseCode.Success;
			}
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="req"></param>
		/// <param name="res"></param>
		public void Reg(ReqRegUser req, ref ResponseMessage res)
		{
			if (req.mobile.IsNull() || req.password.IsNull() || req.userName.IsNull())
			{
				res.Code = ResponseCode.RequireParamLack;
				res.Content = "缺少必填参数";
			}
			else
			{
				if (!req.mobile.IsMobileNum())
				{
					res.Code = ResponseCode.RequestParamInvalid;
					res.Content = "手机号码格式不正确";
					return;
				}
				if (!req.userName.IsNameCn())
				{
					res.Code = ResponseCode.RequestParamInvalid;
					res.Content = "姓名格式不正确";
					return;
				}
				var cryPassword = CryptographyUtil.AESEncryServer(req.password);
				var model = new UserInfo
				{
					UserName = req.userName,
					Mobile = req.mobile,
					Password = cryPassword,
					Email = "",
					UserType = 1
				};
				var userInfo = userInfoSercive.AddOrUpdate(model);
				res.Code = ResponseCode.Success;
				res.Content = userInfo;
			}

		}

		/// <summary>
		/// 获取用户列表
		/// </summary>
		/// <param name="res"></param>
		public void List(ref ResponseMessage res)
		{
			var uList = userInfoSercive.List();
			var resDate = uList.Select(item => new ResUserInfo
			{
				UserId = item.UserId,
				Email = item.Email,
				Mobile = item.Mobile,
				UserName = item.UserName,
				UserType = item.UserType
			}).ToList();
			res.Code = ResponseCode.Success;
			res.Content = resDate;
		}

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Update(ReqUpdateUser req, ref ResponseMessage res)
        {

        }
	}
}
