using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.Model;

namespace ElimWeChatSign.IService
{
    /// <summary>
    /// 签到明细服务接口
    /// </summary>
    public interface IUserSignService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns></returns>
        UserSign AddOrUpdate(UserSign model);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        UserSign Get(string signId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        bool Delete(string signId);

        /// <summary>
        /// 根据用户标识获取列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="planId">计划标识</param>
        /// <returns></returns>
        List<UserSign> ListByUserId(string userId, string planId = ""); 
            
        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="userName">姓名(关键字)</param>
        /// <param name="signDate">签到日期</param>
        /// <returns></returns>
        List<UserSign> List(string userName = "", DateTime? signDate = null);

    }
}
