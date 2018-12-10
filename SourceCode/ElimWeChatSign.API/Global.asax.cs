using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using JaminHuang.Core;
using JaminHuang.Core.Model;
using JaminHuang.Util;
using Newtonsoft.Json;

namespace ElimWeChatSign.API
{
    public class WebApiApplication : HttpApplication
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //限制访问
            config.Filters.Add(new WhiteListAttribute(WhiteListSite.WWW));
            config.Filters.Add(new AntiSqlInjectAttribute());
            config.Filters.Add(new SignatureAttribute());
            config.Filters.Add(new ExceptionAttribute());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters[0] = new JilFormatter();
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
								Code = (int)ResponseCode.NotFound,
								Data = string.Empty,
								Msg = string.Empty,
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
								Code = (int)ResponseCode.ServerInternalError,
								Data = string.Empty,
								Msg = string.Empty,
								ServerTime = DateTime.UtcNow.CreateTimestamp()
							}));
					Response.Flush();
					Server.ClearError();
					return;
			}
		}
    }
}
