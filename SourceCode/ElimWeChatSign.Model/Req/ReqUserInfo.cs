using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Model
{
	/// <summary>
	/// 登录请求
	/// </summary>
	public class ReqUserLogin
	{
		/// <summary>
		/// 手机号码
		/// </summary>
		public string mobile { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string password { get; set; }
	}

	/// <summary>
	/// 用户注册
	/// </summary>
	public class ReqRegUser
	{
		/// <summary>
		/// 手机号码
		/// </summary>
		public string mobile { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string password { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string userName { get; set; }
	}

    /// <summary>
    /// 修改用户信息
    /// </summary>
    public class ReqUpdateUser
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string userType { get; set; }
    }
}
