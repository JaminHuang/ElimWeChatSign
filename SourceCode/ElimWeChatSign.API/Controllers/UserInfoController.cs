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

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
		[HttpPost]
		public IHttpActionResult Login(ReqUserLogin req)
		{
			userInfoBusiness.Login(req, ref responseMessage);
			return Json(responseMessage);
		}
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
		[HttpPost]
		public IHttpActionResult List()
		{
			userInfoBusiness.List(ref responseMessage);
			return Json(responseMessage);
		}
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
		[HttpPost]
		public IHttpActionResult Reg(ReqRegUser req)
		{
			userInfoBusiness.Reg(req, ref responseMessage);
			return Json(responseMessage);
		}
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Update(ReqUpdateUser req)
        {
            userInfoBusiness.Update(req, ref responseMessage);
            return Json(responseMessage);
        }


    }
}
