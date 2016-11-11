using System;
using System.Runtime.CompilerServices;

namespace ElimWeChatSign.Core
{
    public class ResponseMessage
	{
        #region 属性
        /// <summary>
        /// 状态码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 响应对象
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public long ServerTime { get; set; }
        #endregion

        public ResponseMessage([CallerMemberName] string requestMember = "")
        {
            this.Code = ResponseCode.Success;
            this.ErrorMsg = "";
            this.Content = "";
            this.ServerTime = CreateTimestamp();
        }
        /// <summary>
        /// 根据指定时间返回时间戳
        /// </summary>
        /// <returns></returns>
        public static long CreateTimestamp()
        {
            DateTime dateTime = DateTime.UtcNow;
            var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dateTime - utcTime).TotalMilliseconds);
        }
    }

	/// <summary>
	/// 各操作代码
	/// </summary>
	public enum ResponseCode
	{
        /// <summary>
        /// 成功  8200
        /// </summary>
        Success = 8200,
        /// <summary>
        /// 用户不存在   8300
        /// </summary>
        UserNotExist = 8300,
        /// <summary>
        /// 用户无效    8301
        /// </summary>
        UserInvalid = 8301,
        /// <summary>
        /// Token无效     8302
        /// </summary>
        TokenInvalid = 8302,
        /// <summary>
        /// 姓名验证未通过
        /// </summary>
        UserNameError = 8303,
        /// <summary>
        /// 手机号码格式不对
        /// </summary>
        MobileError = 8304,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        NameOrPwdError = 8305,
		/// <summary>
		/// 验证码错误
		/// </summary>
		VCodeError = 8306,
        /// <summary>
        /// 未找到资源
        /// </summary>
        NotFound = 8404,
        /// <summary>
        /// 服务内部错误  8500
        /// </summary>
        ServerInternalError = 8500,
        /// <summary>
        /// 缺少参数
        /// </summary>
        MissParam = 8600,
        /// <summary>
        /// 参数值无效
        /// </summary>
        ParamValueInvalid = 8700,
        /// <summary>
        /// 接口返回对象为空
        /// </summary>
        ResDataIsEmpty = 8800,
        /// <summary>
        /// AES加密数据获取失败
        /// </summary>
        EncryptInvalid = 8900
    }
}
