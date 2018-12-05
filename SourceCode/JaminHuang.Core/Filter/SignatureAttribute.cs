using JaminHuang.Core.Model;
using JaminHuang.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    public class SignatureAttribute : ActionFilterAttribute
    {
        private static IEnumerable<SignInfo> sign_list = new List<SignInfo>()
        {
            new SignInfo() { publickey = "open", privatekey = "tQGpUQ/L7rnWRc1vPUJawc7ZAHR4nIyJCJJTysiLtGAw8UYvMaN3/A==" },
            new SignInfo() { publickey = "www", privatekey = "aaX53gZpHngeB1O8Z3Ugpt906JzmZcIarD01oqznU/nfiZw452nkfr==" }
        };

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int TimeoutSeconds { get; set; }

        /// <summary>
        /// 不需要签名验证的接口
        /// </summary>
        protected List<string> no_signs = new List<string>()
        {

        };

        /// <summary>
        /// 不需要监控的接口
        /// </summary>
        protected List<string> no_monitors = new List<string>()
        {

        };

        public SignatureAttribute()
        {
            this.TimeoutSeconds = 60 * 60;
        }

        public SignatureAttribute(int seconds)
        {
            this.TimeoutSeconds = seconds;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var cfx = ConfigHelper.GetInstance();

            if (cfx["general"]["monitor"].BoolValue && !no_monitors.Contains(actionContext.Request.RequestUri.AbsolutePath))
            {
                Logger.Monitor(typeof(Action), "请求地址：" + actionContext.Request.RequestUri + " 入参：" + actionContext.Request.Content.ReadAsStringAsync().Result + "  请求头：" + GetLogRequestHeaders(actionContext.Request.Headers));
            }

            if (cfx["general"]["debug"].BoolValue)
            {
                return;
            }

            if ((!no_signs.Contains(actionContext.Request.RequestUri.AbsolutePath)))
            {
                SortedDictionary<string, string> b = new SortedDictionary<string, string>();
                string publickey = "";
                string nonce = "";
                long timestamp = 0;
                string signature = "";

                foreach (var a in actionContext.Request.Headers)
                {
                    switch (a.Key)
                    {
                        case "publickey":
                            publickey = a.Value.FirstOrDefault();
                            b.Add("privatekey", sign_list.FirstOrDefault(_ => _.publickey == publickey).privatekey);
                            b.Add(a.Key, a.Value.FirstOrDefault());
                            break;
                        case "nonce":
                            nonce = a.Value.FirstOrDefault();
                            b.Add(a.Key, a.Value.FirstOrDefault());
                            break;
                        case "timestamp":
                            timestamp = long.Parse(a.Value.FirstOrDefault());
                            b.Add(a.Key, a.Value.FirstOrDefault());
                            break;
                        case "signature":
                            signature = a.Value.FirstOrDefault();
                            break;
                    }
                }

                if (string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(publickey) || timestamp == 0)
                {
                    throw new Exception("缺少签名参数");
                }
                if (!sign_list.Any(_ => _.publickey == publickey))
                {
                    throw new Exception("公钥错误");
                }
                if (DateTime.UtcNow.ToTimestamp() - timestamp > TimeoutSeconds || timestamp - DateTime.UtcNow.ToTimestamp() > TimeoutSeconds)
                {
                    throw new Exception("请校准你的本机时间");
                }
                if (signature != ExtendUtil.Md5(string.Join("&", ExtendUtil.Kv(b))))
                {
                    throw new Exception("签名校验失败");
                }
            }
        }

        /// <summary>
        /// 获取请求的请求头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private string GetLogRequestHeaders(HttpRequestHeaders headers)
        {
            var model = new RequestHeaders();
            foreach (var item in headers)
            {
                switch (item.Key)
                {
                    case "publickey": model.publickey = item.Value.FirstOrDefault(); break;
                    case "nonce": model.nonce = item.Value.FirstOrDefault(); break;
                    case "timestamp": model.timestamp = item.Value.FirstOrDefault(); break;
                    case "signature": model.signature = item.Value.FirstOrDefault(); break;
                }
            }

            return JsonConvert.SerializeObject(model);
        }
    }
}
