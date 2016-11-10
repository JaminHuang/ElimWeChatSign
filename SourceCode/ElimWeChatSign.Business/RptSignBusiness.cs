using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
    public class RptSignBusiness
    {
        private IRptSignService rptSignService;

        /// <summary>
        /// 添加报表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Add(ReqRptUserAdd req, ref ResponseMessage res)
        {
            if (req.userId.IsNull() || req.planId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var model = new RptSign
                {
                    UserId = req.userId,
                    PlanId = req.planId,
                    BibleTask = req.bibleTask,
                    BookTask = req.bookTask
                };
                var rs = rptSignService.AddOrUpdate(model);
                res.Code = ResponseCode.Success;
                res.Content = rs;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void List(ReqRptList req, ref ResponseMessage res)
        {
            var list = rptSignService.List(req.userName);
            res.Code = ResponseCode.Success;
            res.Content = list;
        }
    }
}
