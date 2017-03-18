using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API
{
    public class LogClass
    {
        private string ReqStr = LogReq.LogReqStr;
        private string DesCryptStr = LogReq.DesCryptStr;
        /// <summary>
        /// 系统错误报告
        /// </summary>
        /// <param name="ex"></param>
        public string ExcLog(Exception ex)
        {
            return "\n当前时间：" + DateTime.Now +
                   "\n加密后入参：" + DesCryptStr +
                   "\n入参信息：" + ReqStr +
                   "\n异常信息：" + ex.Message +
                   "\n异常对象：" + ex.Source +
                   "\n调用堆栈：" + ex.StackTrace.Trim() +
                   "\n触发方法：" + ex.TargetSite +
                   "\n";
        }

        /// <summary>
        /// 自定义错误报告
        /// </summary>
        /// <param name="ex"></param>
        public string ExcLog(CustomerException ex)
        {
            return "\n当前时间：" + DateTime.Now +
                   "\n加密后入参：" + DesCryptStr +
                   "\n入参信息：" + ReqStr +
                   "\n异常代码：" + (int)ex.Code +
                   "\n异常信息：" + ex.Msg +
                   "\n";
        }
    }
}