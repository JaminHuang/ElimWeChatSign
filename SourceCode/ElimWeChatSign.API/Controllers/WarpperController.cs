using System.Collections.Generic;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// API基类
	/// </summary>
    public class WarpperController : ApiController
    {
		/// <summary>
		/// 外部单独调用
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		protected Dictionary<string, object> DeserializeParamServer(byte[] bytes)
		{
            string jsonStr = GetRequestStr(bytes);
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

			return dic;
		}

        /// <summary>
        /// 解密客户端加密请求
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string GetRequestStr(byte[] bytes)
        {
            string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
            var reqDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
            if (reqDic == null || !reqDic.ContainsKey("body"))
            {
                throw new CustomerException(ResponseCode.EncryptInvalid, "客户端加密字段获取失败");
            }
            //Aes解密
            jsonStr = CryptographyUtil.AESDecryptClient(reqDic["body"]);

            return jsonStr;
        }
    }
}
