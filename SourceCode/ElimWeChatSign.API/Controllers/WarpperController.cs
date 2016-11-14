using System.Collections.Generic;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
    public class WarpperController : ApiController
    {
		private UserInfoBusiness userInfoBusiness = new UserInfoBusiness();

		/// <summary>
		/// 用户授权AuthToken
		/// </summary>
		public string AuthToken { get; set; }
		/// <summary>
		/// 操作系统
		/// </summary>
		public string Os { get; set; }
		/// <summary>
		/// 操作系统版本
		/// </summary>
		public string OsVersion { get; set; }
		/// <summary>
		/// App版本号
		/// </summary>
		public string AppVersion { get; set; }
		/// <summary>
		/// App设备号
		/// </summary>
		public string DeviceId { get; set; }
		/// <summary>
		/// 友盟授权Token
		/// </summary>
		public string DeviceToken { get; set; }
		/// <summary>
		/// 登录IP地址
		/// </summary>
		public string LoginIp { get; set; }

		/// <summary>
		/// 除登录外其他接口访问
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		protected Dictionary<string, object> DeserializeParam(byte[] bytes)
		{
			string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
			var reqDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
			if (reqDic == null || !reqDic.ContainsKey("body"))
			{
				throw new CustomerException(ResponseCode.EncryptInvalid, "客户端加密字段获取失败");
			}
			//Aes解密
			jsonStr = CryptographyUtil.AESDecryptClient(reqDic["body"]);
			var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

			//判断接口必填参数是否都有:deviceID,os,osVersion,appVersion,authToken
			if (dic != null
				&& dic.ContainsKey("os")
				&& dic.ContainsKey("osVersion")
				&& dic.ContainsKey("appVersion")
				&& dic.ContainsKey("deviceId")
				&& dic.ContainsKey("deviceToken")
				&& dic.ContainsKey("loginIp")
				&& dic.ContainsKey("authToken"))
			{
				AuthToken = dic["authToken"].ToString();

				//判断Token是否失效
				if (!userInfoBusiness.IsUserIdExistByAuthToken(AuthToken))
				{
					return dic;
				}
				throw new CustomerException(ResponseCode.TokenInvalid, "登录Token失效");
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少必传参数");
		}

		/// <summary>
		/// 用户登录解析
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		protected Dictionary<string, object> DeserializeParamForLogin(byte[] bytes)
		{
			string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
			var reqDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
			if (reqDic == null || !reqDic.ContainsKey("body"))
			{
				throw new CustomerException(ResponseCode.EncryptInvalid, "客户端加密字段获取失败");
			}
			//Aes解密
			jsonStr = CryptographyUtil.AESDecryptClient(reqDic["body"]);

			var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

			//判断接口必填参数是否都有:deviceID,os,osVersion,appVersion,loginIp
			if (dic != null && dic.ContainsKey("deviceId")
				&& dic.ContainsKey("os")
				&& dic.ContainsKey("osVersion")
				&& dic.ContainsKey("appVersion")
				&& dic.ContainsKey("deviceToken")
				&& dic.ContainsKey("loginIp"))
			{
				Os = dic["os"].ToString();
				OsVersion = dic["osVersion"].ToString();
				AppVersion = dic["appVersion"].ToString();
				DeviceId = dic["deviceId"].ToString();
				DeviceToken = dic["deviceToken"].ToString();
				LoginIp = dic["loginIp"].ToString();

				return dic;
			}
			throw new CustomerException(ResponseCode.MissParam, "缺少必传参数");
		}

		/// <summary>
		/// 服务器单独调用
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		protected Dictionary<string, object> DeserializeParamServer(byte[] bytes)
		{
			string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
			//var reqDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
			//if (reqDic == null || !reqDic.ContainsKey("body"))
			//{
			//	throw new CustomerException(ResponseCode.EncryptInvalid, "客户端加密字段获取失败");
			//}
			////Aes解密
			//jsonStr = CryptographyUtil.AESDecryptServer(reqDic["body"]);
			var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

			return dic;
		}
    }
}
