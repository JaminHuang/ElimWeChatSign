using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.API.Controllers
{
    public class RptSignController : Base
    {
        private RptSignBusiness rptSignBusiness = new RptSignBusiness();

        /// <summary>
        /// 添加报表
        /// </summary>
        /// <param name="req"></param>
        [HttpPost]
        public IHttpActionResult Add(ReqRptUserAdd req)
        {
            rptSignBusiness.Add(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        [HttpPost]
        public IHttpActionResult List(ReqRptList req)
        {
            rptSignBusiness.List(req,ref responseMessage);
            return Json(responseMessage);
        }
    }
}
