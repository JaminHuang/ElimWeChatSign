using ElimWeChatSign.IBusiness;
using JaminHuang.Core;
using JaminHuang.Util;
using Newtonsoft.Json;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElimWeChatSign.API.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// 签到对象
        /// </summary>
        protected IGatherBusiness _gather;

        /// <summary>
        /// 输出对象
        /// </summary>
        protected ResponseMessage res = new ResponseMessage();

        /// <summary>
        /// 输出数据对象
        /// </summary>
        protected Dictionary<string, object> _data = new Dictionary<string, object>();

        /// <summary>
        /// 配置
        /// </summary>
        protected Configuration cfx = ConfigHelper.GetInstance();

        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void setdata(string key, object value)
        {
            _data.Add(key, value);
        }

        /// <summary>
		/// 外部单独调用
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		protected Dictionary<string, object> DeserializeParamServer(byte[] bytes)
        {
            string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

            if (cfx["general"]["debug"].BoolValue)
            {
                return dic;
            }

            var reqParams = GetRequestStr(jsonStr);
            dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(reqParams);

            return dic;
        }

        /// <summary>
        /// 解密客户端加密请求
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        private string GetRequestStr(string toDecrypt)
        {
            var reqDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(toDecrypt);
            
            if (reqDic == null || !reqDic.ContainsKey("body"))
            {
                throw new CustomerException(ResponseCode.EncryptInvalid, "客户端加密字段获取失败");
            }
            //Aes解密
            var reqParams = CryptographyUtil.AESDecryptClient(reqDic["body"]);

            return reqParams;
        }
    }
}
