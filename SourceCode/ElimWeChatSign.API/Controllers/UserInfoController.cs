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

		/// <summary>
		/// 注册
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ResponseMessage> Reg()
		{
			var res = new ResponseMessage();
			var dic = DeserializeParam(await this.Request.Content.ReadAsByteArrayAsync());
			string mobile = "", password = "", userName = "";
			if (dic != null && dic.ContainsKey("mobile") && dic.ContainsKey("password") && dic.ContainsKey("userName"))
			{
				mobile = dic["mobile"].ToString();
				password = dic["password"].ToString();
				userName = dic["userName"].ToString();
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Reg(mobile, password, userName);

			res.Content = result;
			return res;
		}

		/// <summary>
		/// 登录
		/// </summary>
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
			var result = userInfoBusiness.Login(mobile, password);

			res.Content = result;
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
			var dic = DeserializeParamForLogin(await this.Request.Content.ReadAsByteArrayAsync());
			string userId = "", userName = "", mobile = "", password = "", email = "";
			int userType = 0;
			if (dic != null && dic.ContainsKey("userId") && dic.ContainsKey("userName") && dic.ContainsKey("mobile") && dic.ContainsKey("password") && dic.ContainsKey("email"))
			{
				userId = dic["userId"].ToString();
				userName = dic["userName"].ToString();
				mobile = dic["mobile"].ToString();
				password = dic["password"].ToString();
				email = dic["email"].ToString();
				userType = int.Parse(dic["userType"].ToString());
			}
			else
			{
				throw new CustomerException(ResponseCode.MissParam, "缺少参数");
			}
			var result = userInfoBusiness.Update(userId, userName, mobile, password, email, userType);

			res.Content = result;
			return res;
		}
    }
}
