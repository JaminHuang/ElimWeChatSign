using ElimWeChatSign.Model;
using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan;

namespace ElimWeChatSign.Service
{
    /// <summary>
    /// 关怀相关服务
    /// </summary>
    public class UserFollowService : BaseService
    {
        /// <summary>
		/// 添加
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public UserFollow Add(UserFollow model)
        {
            model.FollowId = ExtendUtil.GuidToString();
            model.UpdateTime = DateTime.Now;
            Session.Insert(model);
            return model;
        }

        /// <summary>
        /// 判断指定用户是否已经有关怀对象
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">用户昵称</param>
        /// <param name="groupName">小组名称</param>
        /// <returns></returns>
        public UserFollow GetUserFollow(string churchId, string userName, int gender, string groupName)
        {
            var query = new QueryExpression();
            query.EntityType = typeof(UserFollow);
            query.Selects.Add(UserFollow_.ALL);
            query.Wheres.Add(UserFollow_.ChurchId.TEqual(churchId)
                        .And(UserFollow_.UserName.TEqual(userName)
                        .And(UserFollow_.Gender.TEqual(userName)
                        .And(UserFollow_.GroupName.TEqual(groupName)))));

            var uList = new List<UserFollow>();
            Session.SelectCollection(uList, query);

            return uList.FirstOrDefault();
        }
    }
}
