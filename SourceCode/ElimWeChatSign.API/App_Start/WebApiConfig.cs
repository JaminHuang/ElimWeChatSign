using System;
using System.Net.Http.Headers;
using System.Web.Http;
using CacheCow.Server;
using CacheCow.Server.CacheControlPolicy;
using CacheCow.Server.CacheRefreshPolicy;

namespace ElimWeChatSign.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

			//自定义过滤器
			config.Filters.Add(new MyExceptionFilterAttribute());

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional });



			//配置接口缓存
			var cachingHandler = new CachingHandler(new HttpConfiguration(), "Accept", "Accept-Encoding");
			cachingHandler.CacheControlHeaderProvider =
				new AttributeBasedCacheControlPolicy(
					new CacheControlHeaderValue()
					{
						NoCache = true,
						Private = true,
						NoStore = true
					}).GetCacheControl;

			cachingHandler.CacheRefreshPolicyProvider =
				new AttributeBasedCacheRefreshPolicy(TimeSpan.FromSeconds(15)).GetCacheRefreshPolicy;

			config.MessageHandlers.Add(cachingHandler);
        }
    }
}
