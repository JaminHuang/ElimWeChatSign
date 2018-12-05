using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    /// <summary>
    /// 访问限制
    /// </summary>
    public class LimitAttribute : ActionFilterAttribute
    {
        public int second { get; set; }
        public int count { get; set; }
        public int banned { get; set; }

        public LimitAttribute()
        {
            this.second = 3;
            this.count = 10;
            this.banned = 30;
        }
        public LimitAttribute(int second, int count, int banned)
        {
            this.second = second;
            this.count = count;
            this.banned = banned;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var cfx = ConfigHelper.GetInstance();
            if (cfx["general"]["debug"].BoolValue)
            {
                return;
            }
            var qs = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
            if (!qs.AllKeys.Contains("token"))
            {
                return;
            }
            string key = "Limiting.Token:" + qs["token"];

            if (Caching.CacheClient.ListLength<string>(key) < count)
            {
                Caching.CacheClient.ListLeftPush(key, DateTime.Now.ToTimestamp().ToString(), DateTime.Now.ToTodayLastTime());
                return;
            }
            else
            {
                long time = long.Parse(Caching.CacheClient.ListGetByIndex<string>(key, -1));
                if (DateTime.Now.ToTimestamp() - time < second)
                {
                    Logger.Limit(typeof(LimitAttribute), qs["token"] + " " + actionContext.Request.RequestUri.AbsoluteUri + " " + ExtendUtil.GetIP());
                    Caching.CacheClient.ListTrim<string>(key, 0, count - 2);
                    Caching.CacheClient.ListRightPush(key, (DateTime.Now.ToTimestamp() + banned).ToString(), DateTime.Now.ToTodayLastTime());
                    throw new Exception("访问频率超过了限制，请稍后再试");
                }
                else
                {
                    Caching.CacheClient.ListLeftPush(key, DateTime.Now.ToTimestamp().ToString(), DateTime.Now.ToTodayLastTime());
                    Caching.CacheClient.ListTrim<string>(key, 0, count - 1);
                    return;
                }
            }
        }
    }
}
