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
			if (req.mobile.IsNull() || req.pwd.IsNull())
			{
				res.Code = ResponseCode.RequireParamLack;
				res.Content = "缺少必填参数";
			}
			else
			{
				var cryPassword = CryptographyUtil.AESEncryServer(req.pwd);
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
	}
}
