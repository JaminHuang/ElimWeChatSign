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
    public class UserSignController : Base
    {
        private UserSignBusiness userSignBusiness = new UserSignBusiness();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Add(ReqUserSignAdd req)
        {
            userSignBusiness.Add(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Update(ReqUserSignUpdate req)
        {
            userSignBusiness.Update(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ListByUserId(ReqUserSignListByUserId req)
        {
            userSignBusiness.ListByUserId(req, ref responseMessage);
            return Json(responseMessage);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult List(ReqUserSignList req)
        {
            userSignBusiness.List(req, ref responseMessage);
            return Json(responseMessage);
        }
    }
}
