using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    public class CacheFilter : ActionFilterAttribute
    {
        // 缓存时间 /秒  
        private int _timespan;
        // 客户端缓存时间 /秒  
        private int _clientTimeSpan;
        // 是否为匿名用户缓存  
        private bool _anonymousOnly;
        // 是否客户端缓存
        private bool _isClientCache;
        // 缓存索引键  
        private string _cachekey;
        // 缓存仓库  
        private static readonly ObjectCache WebApiCache = MemoryCache.Default;

        /// <summary>  
        /// 缓存设置
        /// </summary>  
        /// <param name="timespan">缓存时间</param>  
        /// <param name="clientTimeSpan">客户端缓存时间</param>  
        /// <param name="anonymousOnly">是否为匿名用户缓存</param>  
        /// <param name="isClientCache">是否客户端缓存</param>  
        public CacheFilter(int timespan, int clientTimeSpan = 5, bool anonymousOnly = false, bool isClientCache = false)
        {
            _timespan = timespan;
            _clientTimeSpan = clientTimeSpan;
            _anonymousOnly = anonymousOnly;
            _isClientCache = isClientCache;
        }

        //是否缓存  
        private bool _isCacheable(HttpActionContext ac)
        {
            if (_timespan > 0 && _clientTimeSpan > 0)
            {
                HttpContextBase context = (HttpContextBase)ac.Request.Properties["MS_HttpContext"];//获取传统context  
                HttpRequestBase request = context.Request;//定义传统request对象     
                if (_anonymousOnly)
                    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                        return false;
                //POST请求
                if (ac.Request.Method == HttpMethod.Post)
                {
                    _cachekey = string.Join(":", request.FilePath, request.Form.ToString());
                    return true;
                }
                //GET请求
                if (ac.Request.Method == HttpMethod.Get)
                {
                    _cachekey = string.Join(":", request.FilePath, "get");
                    return true;
                }
                return false;
            }
            else
            {
                throw new Exception("缓存设置出错");
            }
        }

        private CacheControlHeaderValue SetClientCache()
        {
            var cachecontrol = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(_clientTimeSpan),
                MustRevalidate = true
            };
            return cachecontrol;
        }

        /// <summary>  
        /// Action调用前执行的方法  
        /// </summary>  
        /// <param name="ac"></param>  
        public override void OnActionExecuting(HttpActionContext ac)
        {
            if (ac != null)
            {
                if (_isCacheable(ac) && WebApiCache.Contains(_cachekey))
                {
                    var val = (string)WebApiCache.Get(_cachekey); //获取缓存的数据
                    if (val != null)
                    {
                        ac.Response = ac.Request.CreateResponse();
                        ac.Response.Content = new StringContent(val);
                        var contenttype = (MediaTypeHeaderValue)WebApiCache.Get(_cachekey + ":response-ct") ??
                                          new MediaTypeHeaderValue(_cachekey.Split(':')[1]);
                        ac.Response.Content.Headers.ContentType = contenttype;
                        ac.Response.Headers.CacheControl = SetClientCache();
                        ac.Response.StatusCode = System.Net.HttpStatusCode.NotModified;
                        return;
                    }
                }
            }
            else
            {
                throw new Exception("缓存设置出错");
            }
        }


        /// <summary>  
        /// Action调用后执行方法  
        /// </summary>  
        /// <param name="actionExecutedContext"></param>  
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (_cachekey != null && !WebApiCache.Contains(_cachekey))
            {
                var body = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                WebApiCache.Add(_cachekey, body, DateTime.Now.AddSeconds(_timespan));
                WebApiCache.Add(_cachekey + ":response-ct", actionExecutedContext.Response.Content.Headers.ContentType, DateTime.Now.AddSeconds(_timespan));
            }
            if (_isCacheable(actionExecutedContext.ActionContext) && _isClientCache)
                actionExecutedContext.ActionContext.Response.Headers.CacheControl = SetClientCache();
        }
    }
}
