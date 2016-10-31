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
    public class RptSignService : IRptSignService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RptSign AddOrUpdate(RptSign model)
        {
            if (model.RptId.IsNull())
            {
                model.RptId = ExtendUtil.GuidToString();
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
        /// <param name="rptId"></param>
        /// <returns></returns>
        public RptSign Get(string rptId)
        {
            var rs = new RptSign(rptId);
            return rs.Select() ? rs : null;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        public bool Delete(string rptId)
        {
            var rs = new RptSign(rptId);
            return rs.Delete() > 0;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userName">姓名[模糊]</param>
        /// <returns></returns>
        public List<RptSign> List(string userName)
        {
            var query = new QueryExpression();
            query.Selects.Add(RptSignProperties.ALL);
            query.Wheres.Add(RptSignProperties.UserInfo.UserName.Like(userName));

            var rsList = new RptSigns();
            rsList.Select(query);

            return rsList.Items;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RptSign> List()
        {
            var query = new QueryExpression();
            query.Selects.Add(RptSignProperties.ALL);

            var rsList = new RptSigns();
            rsList.Select(query);

            return rsList.Items;
        }
    }
}
