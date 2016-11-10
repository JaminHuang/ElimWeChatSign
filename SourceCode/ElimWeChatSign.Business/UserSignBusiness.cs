using System;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.Business
{
    public class UserSignBusiness
    {
        private IUserSignService userSignService;
        /// <summary>
        /// 签到/打卡
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Add(ReqUserSignAdd req, ref ResponseMessage res)
        {
            if (req.planId.IsNull() || req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var model = new UserSign
                {
                    UserId = req.userId,
                    PlanId = req.planId,
                    SignDate = DateTime.Now,
                    SignType = req.signType
                };

                var us = userSignService.AddOrUpdate(model);
                res.Code = ResponseCode.Success;
                res.Content = us;
            }
        }

        /// <summary>
        /// 修改签到
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void Update(ReqUserSignUpdate req, ref ResponseMessage res)
        {
            if (req.planId.IsNull() || req.signId.IsNull() || req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var model = userSignService.Get(req.signId);
                model.PlanId = req.planId;
                model.SignId = req.signId;
                model.UserId = req.userId;
                model.SignType = req.signType;

                var us = userSignService.AddOrUpdate(model);
                res.Code = ResponseCode.Success;
                res.Content = us;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void ListByUserId(ReqUserSignListByUserId req, ref ResponseMessage res)
        {
            if (req.userId.IsNull())
            {
                res.Code = ResponseCode.ParamValueInvalid;
                res.Content = "缺少必填参数";
            }
            else
            {
                var list = userSignService.ListByUserId(req.userId, req.planId);
                res.Code = ResponseCode.Success;
                res.Content = list;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        public void List(ReqUserSignList req, ref ResponseMessage res)
        {
            var list = userSignService.List(req.userName, req.signDate);
            res.Code = ResponseCode.Success;
            res.Content = list;
        }
    }
}
