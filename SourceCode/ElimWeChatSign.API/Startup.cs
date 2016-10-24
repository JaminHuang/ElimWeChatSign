using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly:OwinStartup(typeof(ElimWeChatSign.API.Startup))]

namespace ElimWeChatSign.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
			var config = new HttpConfiguration();

			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional });

			app.UseCors(CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
	}
}