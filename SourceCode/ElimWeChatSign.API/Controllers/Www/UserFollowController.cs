using ElimWeChatSign.Business;
using JaminHuang.Core;
using JaminHuang.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElimWeChatSign.API.Controllers.Www
{
    /// <summary>
    /// 关怀相关接口
    /// </summary>
    [RoutePrefix("www/userfollow")]
    public class UserFollowController : WwwBaseController
    {
        private UserFollowBusiness _userFollow = new UserFollowBusiness();

        /// <summary>
        /// 添加关怀对象
        /// </summary>
        /// param:
        /// churchId:教会标识
        /// userName:姓名
        /// gender:性别
        /// groupName:小组
        /// followName:关怀对象
        /// <returns></returns>
        [Route("add"), HttpPost]
        public async Task<ResponseMessage> Add()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string userName = "", groupName = "", churchId = "", followName = "";
            int gender = 3;
            if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("groupName") && dic.ContainsKey("followName") && dic.ContainsKey("churchId")
                     && dic.ContainsKey("gender"))
            {
                userName = dic["userName"].ToString();
                groupName = dic["groupName"].ToString();
                followName = dic["followName"].ToString();
                gender = int.Parse(dic["gender"].ToString());
                churchId = dic["churchId"].ToString();
            }
            else
            {
                throw new CustomerException(ResponseCode.MissParam, "缺少参数");
            }
            var result = _userFollow.Add(churchId, userName, gender, groupName, followName);

            res.Data = result;
            return res;
        }

        /// <summary>
        /// 获取用户关怀对象
        /// </summary>
        /// param:
        /// churchId:教会标识
        /// userName:姓名
        /// gender:性别
        /// groupName:小组
        /// <returns></returns>
        [Route("get/userfollow"), HttpPost]
        public async Task<ResponseMessage> GetUserFollow()
        {
            var dic = DeserializeParamServer(await Request.Content.ReadAsByteArrayAsync());
            string userName = "", groupName = "", churchId = "";
            int gender = 3;
            if (dic != null && dic.ContainsKey("userName") && dic.ContainsKey("groupName") && dic.ContainsKey("gender") && dic.ContainsKey("churchId"))
            {
                userName = dic["userName"].ToString();
                groupName = dic["groupName"].ToString();
                gender = int.Parse(dic["gender"].ToString());
                churchId = dic["churchId"].ToString();
            }
            else
            {
                throw new CustomerException(ResponseCode.MissParam, "缺少参数");
            }
            var result = _userFollow.GetUserFollow(churchId, userName, gender, groupName);

            res.Data = result;
            return res;
        }
    }
}
