using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using ElimWeChatSign.Core;
using Newtonsoft.Json;

namespace ElimWeChatSign.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
			// 在应用程序启动时运行的代码
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			//路由地址配置
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			//配置LogNet4
			log4net.Config.XmlConfigurator.Configure();

			FilterConfig.RegisterGlobalFilters(new HttpFilterCollection());
        }

		/// <summary>
		/// 应用程序出错执行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_Error(object sender, EventArgs e)
		{
			var ex = Server.GetLastError();
			var exception = ex as HttpException;
			if (null == exception)
			{
				return;
			}
			var httpCode = exception.GetHttpCode();
			switch (httpCode)
			{
				case 404:
					Response.Clear();
					Response.ContentType = "application/json; charset=utf-8";
					Response.Write(
						JsonConvert.SerializeObject(
							new ResponseMessage
							{
								Code = ResponseCode.NotFound,
								Content = string.Empty,
								ErrorMsg = string.Empty,
								ServerTime = DateTime.UtcNow.CreateTimestamp()
							}));
					Response.Flush();
					Server.ClearError();
					return;
				case 500:
					Response.Clear();
					Response.ContentType = "application/json; charset=utf-8";
					Response.Write(
						JsonConvert.SerializeObject(
							new ResponseMessage
							{
								Code = ResponseCode.ServerInternalError,
								Content = string.Empty,
								ErrorMsg = string.Empty,
								ServerTime = DateTime.UtcNow.CreateTimestamp()
							}));
					Response.Flush();
					Server.ClearError();
					return;
			}
		}
    }
}
