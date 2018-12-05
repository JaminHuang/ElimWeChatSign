using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    public class WhiteListAttribute : ActionFilterAttribute
    {
        public WhiteListSite site { get; set; }

        public WhiteListAttribute(WhiteListSite site)
        {
            this.site = site;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var cfx = ConfigHelper.GetInstance();
            if (cfx["general"]["debug"].BoolValue)
            {
                return;
            }
            switch (site)
            {
                case WhiteListSite.WWW:
                    if (cfx["attribute"]["www"].StringValueArray.Contains(ExtendUtil.GetIP()))
                    {
                        throw new Exception("没有权限访问接口 " + ExtendUtil.GetIP());
                    }
                    break;
                    break;
                case WhiteListSite.OPEN:
                    if (cfx["attribute"]["open"].StringValueArray.Contains(ExtendUtil.GetIP()))
                    {
                        throw new Exception("没有权限访问接口 " + ExtendUtil.GetIP());
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 访问站
    /// </summary>
    public enum WhiteListSite
    {
        WWW,
        OPEN
    }
}
