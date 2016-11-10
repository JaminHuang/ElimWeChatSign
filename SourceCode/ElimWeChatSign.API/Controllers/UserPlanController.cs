using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.API.Controllers
{
    public class UserPlanController : Base
    {
        private UserPlanBusiness userPlanBusiness = new UserPlanBusiness();

        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Add(ReqAddUserPlan req)
        {
            userPlanBusiness.Add(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 修改计划
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Update(ReqUpdatePlan req)
        {
            userPlanBusiness.Update(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 根据用户搜索所有列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ListByUserId(ReqPlanListByUserId req)
        {
            userPlanBusiness.ListByUserId(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult List(ReqPlanList req)
        {
            userPlanBusiness.List(req, ref responseMessage);
            return Json(responseMessage);
        }
    }
}
