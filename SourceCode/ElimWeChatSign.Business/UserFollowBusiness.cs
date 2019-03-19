using ElimWeChatSign.Model;
using ElimWeChatSign.Service;
using JaminHuang.Core;
using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Business
{
    /// <summary>
    /// 用户关怀业务逻辑
    /// </summary>
    public class UserFollowBusiness
    {
        public UserFollowService userFollowService = new UserFollowService();

        /// <summary>
        /// 添加关怀对象
        /// </summary>
        /// <param name="churchId">教会标识</param>
        /// <param name="userName">姓名</param>
        /// <param name="gender">性别</param>
        /// <param name="groupName">小组名称</param>
        /// <param name="followName">关怀对象</param>
        /// <returns></returns>
        public ResUserFollow Add(string churchId, string userName, int gender, string groupName, string followName)
        {
            if (userName.IsNull() || followName.IsNull())
                throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

            var model = new UserFollow
            {
                ChurchId = churchId,
                UserName = userName,
                GroupName = groupName,
                Gender = gender,
                FollowName = followName
            };

            var user = userFollowService.Add(model);

            //输出对象
            var resUserFollow = new ResUserFollow
            {
                FollowId = user.FollowId,
                UserName = user.UserName,
                Gender = user.Gender,
                GroupName = user.GroupName,
                FollowName = user.FollowName,
                Status = 0
            };

            return resUserFollow;
        }

        /// <summary>
        /// 判断该用户的关怀对象
        /// </summary>
        /// <param name="churchId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ResUserFollow GetUserFollow(string churchId, string userName, int gender, string groupName)
        {
            if (userName.IsNull())
                throw new CustomerException(ResponseCode.ParamValueInvalid, "参数值无效");

            var user = userFollowService.GetUserFollow(churchId, userName, gender, groupName);

            //输出对象
            var resUserFollow = new ResUserFollow();

            if (user != null)
            {
                resUserFollow.FollowId = user.FollowId;
                resUserFollow.FollowName = user.FollowName;
                resUserFollow.UserName = user.UserName;
                resUserFollow.Gender = user.Gender;
                resUserFollow.GroupName = user.GroupName;
                resUserFollow.Status = 1;
            }

            return resUserFollow;
        }
    }
}
