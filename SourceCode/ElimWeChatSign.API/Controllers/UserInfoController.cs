using System.Threading.Tasks;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 用户相关
	/// </summary>
    public class UserInfoController : WarpperController
    {
		private UserInfoBusiness userInfoBusiness = new UserInfoBusiness();

		/// <summary>
		/// 发送验证码
		/// </summary>
		/// param:
		/// mobile:手机号码
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> SendVCode()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamServer(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "";
			if (dic != null && dic.ContainsKey("mobile"))
			{
				mobile = dic["mobile"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.SendVCode(mobile);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// param:
		/// mobile:手机号码
		/// password:密码
		/// userName:用户姓名
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Reg()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "", password = "", userName = "", vCode = "";
			if (dic != null && dic.ContainsKey("mobile") && dic.ContainsKey("password") && dic.ContainsKey("userName") && dic.ContainsKey("vCode"))
			{
				mobile = dic["mobile"].ToString();
				password = dic["password"].ToString();
				userName = dic["userName"].ToString();
				vCode = dic["vCode"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Reg(mobile, password, userName, vCode, Os, OsVersion, DeviceId, AppVersion, DeviceToken, LoginIp);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// param：
		/// mobile：手机号码
		/// password：密码
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Login()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "", password = "";
			if (dic != null && dic.ContainsKey("mobile") && dic.ContainsKey("password"))
			{
				mobile = dic["mobile"].ToString();
				password = dic["password"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Login(mobile, password, Os, OsVersion, DeviceId, AppVersion, DeviceToken, LoginIp);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 修改密码
		/// </summary>
		/// param:
		/// mobile：手机号码
		/// oldPassword：旧密码
		/// newPassword：新密码
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> UpdatePassword()
		{
			var res = new ResponseMessage();
			var dic = this.DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "", oldPassword = "", newPassword = "";
			if (null != dic && dic.ContainsKey("mobile") && dic.ContainsKey("oldPassword") && dic.ContainsKey("newPassword"))
			{
				mobile = dic["mobile"].ToString();
				oldPassword = dic["oldPassword"].ToString();
				newPassword = dic["newPassword"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam);
			}

			var result = userInfoBusiness.UpdatePassword(mobile, oldPassword, newPassword);
			res.Content = result;
			return res;

		}

		/// <summary>
		/// 密码重置
		/// </summary>
		/// param:
		/// mobile,手机号码
		/// password,密码
		/// vCode,验证码
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> ResetPassword()
		{
			var res = new ResponseMessage();
			var dic = this.DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "", password = "", vCode = "";
			if (null != dic && dic.ContainsKey("mobile") && dic.ContainsKey("password") && dic.ContainsKey("vCode"))
			{
				mobile = dic["mobile"].ToString();
				password = dic["password"].ToString();
				vCode = dic["vCode"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}

			var result = userInfoBusiness.ResetPassword(mobile, password, vCode);
			res.Content = result;
			return res;

		}

		/// <summary>
		/// 退出登录
		/// </summary>
		/// param:
		/// authToken:用户授权Token,非空
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> ExitLogin()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			res.Content = userInfoBusiness.ExitLogin(AuthToken);
			return res;
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Update()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "", userName = "", avatar="", mobile = "", email = "";
			int userType = -1;
			if (dic != null && dic.ContainsKey("userId") && dic.ContainsKey("userName") && dic.ContainsKey("mobile")
                && dic.ContainsKey("avatar") && dic.ContainsKey("email") && dic.ContainsKey("userType"))
			{
				userId = dic["userId"].ToString();
				userName = dic["userName"].ToString();
                avatar = dic["avatar"].ToString();
				mobile = dic["mobile"].ToString();
				email = dic["email"].ToString();
				userType = int.Parse(dic["userType"].ToString());
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Update(userId, userName, avatar, mobile, email, userType);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 获取用户信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Get()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "";
			if (dic != null && dic.ContainsKey("userId"))
			{
				userId = dic["userId"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Get(userId);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 获取列表
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> List()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string userName = "";
			if (dic != null && dic.ContainsKey("userName"))
			{
				userName = dic["userName"].ToString();
			}
			var result = userInfoBusiness.List(userName);

			res.Content = result;
			return res;
		}
    }
}
