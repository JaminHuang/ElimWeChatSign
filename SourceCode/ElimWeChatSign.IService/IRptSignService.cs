using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.IService
{
    public interface IRptSignService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        RptSign AddOrUpdate(RptSign model);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        RptSign Get(string rptId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        bool Delete(string rptId);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<RptSign> List(string userName);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<RptSign> List();
    }
}
