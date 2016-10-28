using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.Core;
using ElimWeChatSign.IService;
using ElimWeChatSign.Model;
using Titan;

namespace ElimWeChatSign.Service
{
    /// <summary>
    /// 签到明细服务
    /// </summary>
    public class UserSignService : IUserSignService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public UserSign AddOrUpdate(UserSign model)
        {
            if (model.SignId.IsNull())
            {
                model.SignId = ExtendUtil.GuidToString();
                model.UpdateTime = DateTime.Now;
                model.Insert();
            }
            else
            {
                model.UpdateTime = DateTime.Now;
                model.Update();
            }
            return model;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        public UserSign Get(string signId)
        {
            var us = new UserSign(signId);
            return us.Select() ? us : null;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        public bool Delete(string signId)
        {
            var us = new UserSign(signId);
            return us.Delete() > 0;
        }

        /// <summary>
        /// 根据用户标识获取列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="planId">计划标识</param>
        /// <returns></returns>
        public List<UserSign> ListByUserId(string userId, string planId = "")
        {
            var query = new QueryExpression();
            query.Selects.Add(UserSignProperties.ALL);
            query.Wheres.Add(UserSignProperties.UserId.Equal(userId));
            if (!planId.IsNull())
                query.Wheres.Add(UserSignProperties.PlanId.Equal(planId));

            var usList = new UserSigns();
            usList.Select(query);

            return usList.Items;
        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="userName">姓名(关键字)</param>
        /// <param name="signDate">签到日期</param>
        /// <returns></returns>
        public List<UserSign> List(string userName = "", DateTime? signDate = null)
        {
            var query = new QueryExpression();
            query.Selects.Add(UserSignProperties.ALL);
            if (!userName.IsNull())
                query.Wheres.Add(UserSignProperties.UserInfo.UserName.Equal(userName));
            if (signDate != null)
            {
                var sDate = signDate.Value.Date;
                var eDate = signDate.Value.AddDays(1).AddSeconds(-1);

                query.Wheres.Add(UserSignProperties.SignDate.GreaterEqual(sDate)
                            .And(UserSignProperties.SignDate.LessEqual(eDate)));
            }
            var usList = new UserSigns();
            usList.Select(query);

            return usList.Items;
        }
    }
}
