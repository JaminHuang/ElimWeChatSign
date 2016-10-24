using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 用户相关接口
	/// </summary>
    public class UserInfoController : Base
    {
		private UserInfoBusiness userInfoBusiness = new UserInfoBusiness();

		[HttpPost]
		public IHttpActionResult Login(ReqUserLogin req)
		{
			userInfoBusiness.Login(req, ref responseMessage);
			return Json(responseMessage);
		}

		[HttpPost]
		public IHttpActionResult List()
		{
			userInfoBusiness.List(ref responseMessage);
			return Json(responseMessage);
		}
    }
}
