using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Core.Model
{
    public class SignInfo
    {
        public string publickey { get; set; } = "";
        public string privatekey { get; set; } = "";
    }

    /// <summary>
    /// API请求头
    /// </summary>
    public class RequestHeaders
    {
        /// <summary>
        /// 公钥
        /// </summary>
        public string publickey { get; set; } = "";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce { get; set; } = "";
        /// <summary>
        /// 当前时间戳
        /// </summary>
        public string timestamp { get; set; } = "";
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; } = "";
    }
}
