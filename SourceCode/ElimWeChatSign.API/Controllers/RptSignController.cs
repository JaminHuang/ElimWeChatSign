using System.Threading.Tasks;
using System.Web.Http;
using ElimWeChatSign.Business;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API.Controllers
{
	/// <summary>
	/// 签到报表
	/// </summary>
    public class RptSignController : WarpperController
    {
		private RptSignBusiness rptSignBusiness = new RptSignBusiness();

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
			var result = rptSignBusiness.List(userName);

			res.Content = result;
			return res;
		}
    }
}
