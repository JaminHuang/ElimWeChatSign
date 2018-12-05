using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Core
{
    public class ResponseMessage
    {
        #region 属性
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 响应对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public long ServerTime { get; set; }
        #endregion

        public ResponseMessage([CallerMemberName] string requestMember = "")
        {
            this.Code = (int)ResponseCode.Success;
            this.Msg = "";
            this.Data = "";
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
        /// 失败   8300
        /// </summary>
        Fail = 8300,
        /// <summary>
        /// IP地址错误
        /// </summary>
        IpAddressError = 8400,
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
