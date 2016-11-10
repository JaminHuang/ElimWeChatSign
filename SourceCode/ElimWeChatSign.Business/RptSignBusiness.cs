using System.Collections.Generic;
using System.Linq;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
	/// <summary>
	/// 签到报表业务逻辑
	/// </summary>
    public class RptSignBusiness
    {
        private IRptSignService rptSignService;

	    /// <summary>
	    /// 添加报表
	    /// </summary>
	    /// <param name="userId">用户标识</param>
	    /// <param name="planId">计划标识</param>
	    /// <param name="bibleTask">读经完成度</param>
	    /// <param name="bookTask">读书完成度</param>
	    public ResRptSign Add(string userId, string planId, string bibleTask, string bookTask)
	    {
		    if (userId.IsNull() || planId.IsNull())
			    throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

		    var model = new RptSign
		    {
			    UserId = userId,
			    PlanId = planId,
			    BibleTask = bibleTask,
			    BookTask = bookTask
		    };
		    var rs = rptSignService.AddOrUpdate(model);

			//输出对象
			var resPrtSign = new ResRptSign
			{
				RptId = rs.RptId,
				PlanId = rs.PlanId,
				BibleTask = rs.BibleTask,
				BookTask = rs.BookTask,
				UpdateTime = rs.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

		    return resPrtSign;
	    }

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
