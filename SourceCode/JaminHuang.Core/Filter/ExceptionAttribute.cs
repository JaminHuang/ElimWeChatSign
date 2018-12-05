using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常捕捉
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            base.OnException(filterContext);

            var excetype = filterContext.Exception.GetType().Name;
            var res = new ResponseMessage();

            //自定义异常类型
            if (excetype == "CustomerException")
            {
                var ex = (CustomerException)filterContext.Exception;
                res.Code = (int)ex.Code;
                res.Data = "";
                res.Msg = ex.Msg ?? ex.Code.ToString();
            }
            //系统异常
            else
            {
                var ex = (Exception)filterContext.Exception;
                res.Code = (int)ResponseCode.ServerInternalError;
                res.Data = "";
                res.Msg = ex.Message;

                //写入错误日志
                Logger.Error(filterContext.Exception.Source, string.Format("捕获到异常：{0} 错误信息：{1} 请求接口：{2}", ex.TargetSite, ex.Message, filterContext.Request.RequestUri.AbsolutePath));
            }

            //讲异常返回数据格式重新封装
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
