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
    public class RptSignController : Base
    {
        private RptSignBusiness rptSignBusiness = new RptSignBusiness();

        [HttpPost]
        public void Add(ReqRptUserAdd req)
        {
            rptSignBusiness.Add(req, ref responseMessage);
        }
    }
}
