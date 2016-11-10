using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
    /// <summary>
    /// 用户计划业务逻辑
    /// </summary>
    public class UserPlanBusiness
    {
        private IUserPlanService userPlanService;

        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Add(ReqAddUserPlan req, ref ResponseMessage res)
        {
            if (req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var model = new UserPlan
                {
                    UserId = req.userId,
                    BiblePlan = req.biblePlan,
                    BookPlan = req.bookPlan,
                    StartDate = req.startDate,
                    EndDate = req.endDate
                };

                var up = userPlanService.AddOrUpdate(model);
                res.Code = ResponseCode.Success;
                res.Content = up;
            }
        }

        /// <summary>
        /// 修改计划
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Update(ReqUpdatePlan req, ref ResponseMessage res)
        {
            if (req.planId.IsNull() || req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var model = userPlanService.Get(req.planId);
                if (!req.biblePlan.IsNull()) { model.BiblePlan = req.biblePlan; }
                if (!req.bookPlan.IsNull()) { model.BookPlan = req.bookPlan; }
                if (req.startDate != null) { model.StartDate = req.startDate.Value; }
                if (req.endDate != null) { model.EndDate = req.endDate.Value; }

                var up = userPlanService.AddOrUpdate(model);
                res.Code = ResponseCode.Success;
                res.Content = up;
            }
        }

        /// <summary>
        /// 根据用户搜索所有列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void ListByUserId(ReqPlanListByUserId req, ref ResponseMessage res)
        {
            if (req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var list = userPlanService.ListByUserId(req.userId, req.startDate, req.endDate);
                res.Code = ResponseCode.Success;
                res.Content = list;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void List(ReqPlanList req, ref ResponseMessage res)
        {
            var list = userPlanService.List(req.userName, req.startDate, req.endDate);
            res.Code = ResponseCode.Success;
            res.Content = list;
        }
    }
}
