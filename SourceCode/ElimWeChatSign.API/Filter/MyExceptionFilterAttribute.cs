using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API
{
	public class MyExceptionFilterAttribute : ExceptionFilterAttribute
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

            //构建日志对象
            var log = new LogClass();

            //自定义异常类型
            if (excetype == "CustomerException")
			{
				var ex = (CustomerException)filterContext.Exception;
				res.Code = ex.Code;
				res.Content = "";
				res.ErrorMsg = ex.Msg ?? ex.Code.ToString();

                //写入日志
                var errInfo = log.ExcLog(ex);
                LoggerHelper.Error(errInfo);
            }
			//系统异常
			else
			{
				var ex = (Exception)filterContext.Exception;
				res.Code = ResponseCode.ServerInternalError;
				res.Content = "";
				res.ErrorMsg = ex.Message;

                //写入日志
                var errInfo = log.ExcLog(ex);
                LoggerHelper.Error(errInfo);
            }

			//讲异常返回数据格式重新封装
			filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, res);
		}
	}
}