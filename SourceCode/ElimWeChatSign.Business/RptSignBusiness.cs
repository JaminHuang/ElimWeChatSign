using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Model;
using ElimWeChatSign.Service;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 签到报表业务逻辑
	/// </summary>
    public class RptSignBusiness
    {
        private RptSignService rptSignService = new RptSignService();

	    /// <summary>
	    /// 获取列表
	    /// </summary>
	    /// <param name="userName">用户姓名[模糊]</param>
	    public List<ResRptSign> List(string userName)
        {
            var list = rptSignService.List(userName);
			//输出对象
			var resDate = list.Select(item => new ResRptSign
			{
				PlanId = item.PlanId,
				UserId = item.UserId,
				BibleTask = item.BibleTask,
				BookTask = item.BookTask,
				UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();

			return resDate;
        }
    }
}
