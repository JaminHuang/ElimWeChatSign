using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace JaminHuang.Util
{
    public static class ExtendUtil
    {
        #region 时间
        /// <summary>
        /// 返回时间戳
        /// </summary>
        /// <param name="utcNow">UTC时间</param>
        /// <returns></returns>
        public static long CreateTimestamp(this DateTime utcNow)
        {
            return utcNow.ToTimestamp();
        }
        /// <summary>
        /// 转成时间戳
        /// </summary>
        /// <param name="dateTime">now</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            TimeZone tz = TimeZone.CurrentTimeZone;
            var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((tz.ToUniversalTime(dateTime) - utcTime).TotalMilliseconds);
        }
        /// <summary>
        /// 时间戳转成日期
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp)
        {
            var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt = utcTime.AddMilliseconds(timestamp);
            return dt.ToLocalTime();
        }
        /// <summary>
        /// 返回当前时候的最后一秒
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToTodayLastTime(this DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }
        #endregion

        #region 授权Token
        /// <summary>
        /// 生成授权token
        /// </summary>
        /// <returns></returns>
        public static string GenerateAuthToken()
        {
            string guid = Guid.NewGuid().ToString();
            string date = DateTime.Now.ToString("yyMMdd");
            return guid.Substring(1, 1) + guid.Substring(3, 1) + guid.Substring(5, 1) +
                   guid.Substring(7, 1) + guid.Substring(9, 1)
                   + date.Substring(0, 1) + date.Substring(2, 1) + date.Substring(4, 1) +
                   guid.Substring(0, 1) + guid.Substring(2, 1) + guid.Substring(4, 1) +
                   guid.Substring(6, 1) + guid.Substring(8, 1)
                   + date.Substring(1, 1) + date.Substring(3, 1) + date.Substring(5, 1);
        }
        /// <summary>
        /// 解析授权token
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public static string AnalyzeAuthToken(string authToken)
        {
            //token的偶数位前五位 + 当前时间的奇数位前三位 + token的奇数位前五位 + 当前时间的偶数位前三位
            return authToken.Substring(3, 5) + authToken.Substring(0, 3) + authToken.Substring(11, 5) + authToken.Substring(8, 3);
        }
        /// <summary>
        /// 生成客户端token
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public static string GenerateClientAuthToken(string authToken)
        {
            //当前时间的奇数位前三位 + token的偶数位前五位 + 当前时间的偶数位前三位+ token的奇数位前五位。
            return authToken.Substring(5, 3) + authToken.Substring(0, 5) + authToken.Substring(13, 3) + authToken.Substring(8, 5);
        }
        #endregion

        #region 随机
        /// <summary>
        /// 产生指定位数的随机数
        /// </summary>
        /// <param name="randomLevel">位数</param>
        /// <returns></returns>
        public static string GetRandom(int randomLevel)
        {
            var str = "";
            var random = new Random();

            for (int i = 0; i < randomLevel; i++)
            {
                int num = random.Next(0, 9);
                str += num.ToString();
            }
            return str;
        }
        /// <summary>
        /// 16位随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidToString()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        #endregion

        #region Path

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        #endregion

        #region Other

        public static IEnumerable<string> Kv(SortedDictionary<string, string> a)
        {
            foreach (KeyValuePair<string, string> kv in a)
            {
                yield return string.Format("{0}={1}", kv.Key, kv.Value);
            }
        }

        #endregion

        #region MD5

        /// <summary>
        /// MD5转换
        /// </summary>
        /// <param name="toEncrypt">字符串</param>
        /// <returns></returns>
        public static string ToMd5(this string toEncrypt)
        {
            var str = CryptographyUtil.Md5Hash(toEncrypt);
            return str.Substring(0, 5) + str.Substring(str.Length - 6);
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string Md5(this string toEncrypt)
        {
            return CryptographyUtil.Md5NormalHash(toEncrypt);
        }

        #endregion

        #region IP

        /// <summary>
        /// 获得IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !RegexUtil.IsIP(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        /// <summary>
        /// 获得本地IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            return HttpContext.Current.Request.ServerVariables["Local_addr"].ToString();
        }

        /// <summary>
        /// 根据IP地址获得主机名称
        /// </summary>
        /// <param name="ip">主机的IP地址</param>
        /// <returns>主机名称</returns>
        public static string GetHostNameByIp(string ip)
        {
            ip = ip.Trim();
            if (ip == string.Empty)
                return string.Empty;
            try
            {
                // 是否 Ping 的通
                if (Ping(ip))
                {
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    return host.HostName;
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据主机名（域名）获得主机的IP地址
        /// </summary>
        /// <param name="hostName">主机名或域名</param>
        /// <example> GetIPByDomain("www.google.com");</example>
        /// <returns>主机的IP地址</returns>
        public static string GetIpByHostName(string hostName)
        {
            hostName = hostName.Trim();
            if (hostName == string.Empty)
                return string.Empty;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostName);
                return host.AddressList.GetValue(0).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 是否能 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {

            string[] userip = SplitString(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }

        #endregion

        #region String

        public static string ToUnicodeString(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }

        public static string FromUnicodeString(string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }

        public static String ClearSolr(String str)
        {
            if (str.Trim().Length > 0)
            {
                return str
                    .Replace("+", "")
                    .Replace("-", "")
                    .Replace("!", "")
                    .Replace("\\", "")
                    .Replace("*", "")
                    .Replace("?", "")
                    .Replace("|", "")
                    .Replace("&", "")
                    .Replace(";", "")
                    .Replace("~", "")
                    .Replace(":", "")
                    .Replace("/", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace(" ", "");
            }
            return str;
        }

        public static String EscapeXml(String str)
        {
            if (str.Trim().Length > 0)
            {
                return str
                    .Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("'", "&apos;");
            }
            return str;
        }

        public static String UnescapeXml(String str)
        {
            if (str.Trim().Length > 0)
            {
                return str
                    .Replace("&amp;", "&")
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .Replace("&quot;", "\"")
                    .Replace("&apos;", "'");
            }
            return str;
        }

        /// <summary>
        ///  随机生成字符串
        /// </summary>
        /// <param name="codeCount">字符串个数</param>
        /// <param name="baseString">备选字符组成的字符串</param>
        /// <returns>随机生成的字符串</returns>
        public static string GenerateCodeFromString(int codeCount, string baseString)
        {
            int number;
            int length = baseString.Length;
            StringBuilder result = new StringBuilder();
            System.Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                number = random.Next(length);
                result.Append(baseString.Substring(number, 1));
            }
            return result.ToString();

        }
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        /// <summary>
        /// 字符串是否在类似数据的字符串存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">长字符串</param>
        /// <param name="strsplit">分割符号</param>
        /// <returns></returns>
        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (stringarray == "" || stringarray == null)
            {
                return false;
            }

            str = str.ToLower();
            string[] stringArray = SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                //string t1 = str;
                //string t2 = stringArray[i];
                if (str.IndexOf(stringArray[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }
        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > str.Length)
                {
                    return "";
                }


            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }
        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
            //return GetSubStrings(p_SrcString, p_Length*2, p_TailString);
        }
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos) + p_TailString;
            }
            else
                result = str;

            return result;
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                    {
                        return "";
                    }
                    else
                    {
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                    }
                }
            }


            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }
        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }
        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 返回Email名称
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }
        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }
        /// <summary>
        /// 清理符号
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string CleanSymbol(string strHtml)
        {
            if (strHtml == null || strHtml == "")
                return strHtml;
            strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, @"[~!@#\$%\^&\*\(\)\+=\|\\\}\]\{\[:;<,>\?\/""'`]+", "", RegexOptions.IgnoreCase);
            return strHtml;
        }
        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!RegexUtil.IsEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                {
                    string[] tmp = { strContent };
                    return tmp;
                }
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
            {
                return new string[0] { };
            }
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];

            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            return ignoreRepeatItem ? DistinctStringArray(result, maxElementLength) : result;
        }
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int minElementLength, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            if (ignoreRepeatItem)
            {
                result = DistinctStringArray(result);
            }
            return PadStringArray(result, minElementLength, maxElementLength);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return SplitString(strContent, strSplit, ignoreRepeatItem, 0);
        }
        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }
        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            else
                return str.Substring(0, str.Length - 1);
        }
        /// <summary>
        /// 返回文件后缀
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFileExtName(string filename)
        {
            return Path.GetExtension(filename);
        }
        /// <summary>
        /// 去掉文件名后缀
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFileNoExtName(string filename)
        {
            string[] array = filename.Trim().Split('.');
            return array[0].ToString();
        }
        /// <summary>
        /// 返回用时间生成的文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFileNameByDate()
        {
            Random r = new Random();

            return DateTime.Now.Day.ToString() + "_" +
                    DateTime.Now.Hour.ToString() + "-" +
                    DateTime.Now.Minute.ToString() + "-" +
                    DateTime.Now.Second.ToString() + "_" +
                    r.Next(100, 10000).ToString();
        }
        /// <summary>
        /// UTF8 转 GB2312
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = utf8.GetBytes(str);
                byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                string result = gb2312.GetString(temp1);
                return result;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// GB2312 转 UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(str);
                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                string result = uft8.GetString(temp1);
                return result;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }
        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (RegexUtil.StrIsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        }
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="First">第一个数组</param>
        /// <param name="Second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static string[] MergerArray(string[] First, string[] Second)
        {
            string[] result = new string[First.Length + Second.Length];
            First.CopyTo(result, 0);
            Second.CopyTo(result, First.Length);
            return result;
        }
        /// <summary>
        /// 数组追加
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="str">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static string[] MergerArray(string[] Source, string str)
        {
            string[] result = new string[Source.Length + 1];
            Source.CopyTo(result, 0);
            result[Source.Length] = str;
            return result;
        }
        /// <summary>
        /// 获取距离
        /// </summary>
        /// <param name="distance">相离距离</param>
        /// <returns></returns>
        public static string GetDistanceString(double distance)
        {
            if (distance == 0)
            {
                return "0m";
            }

            string str = distance + "m";
            if (distance > 100000000)
                str = "10000km";
            else if (distance > 100000)
                str = (int)Math.Round((decimal)distance / 1000, 1) + "km";
            else if (distance > 1000)
                str = Math.Round((decimal)distance / 1000, 1) + "km";

            return str;
        }
        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="d">秒</param>
        /// <returns></returns>
        public static string GetOffString(long d)
        {
            if (d <= 0)
                return "已过期";
            else if (d > 12 * 30 * 24 * 60 * 60)
                return (d / (12 * 30 * 24 * 60 * 60)).ToString() + "年";
            else if (d > 30 * 24 * 60 * 60)
                return (d / (30 * 24 * 60 * 60)).ToString() + "月";
            else if (d > 24 * 60 * 60)
                return (d / (24 * 60 * 60)).ToString() + "天";
            else if (d > 60 * 60)
                return (d / (60 * 60)).ToString() + "小时";
            else if (d > 60)
                return (d / 60).ToString() + "分钟";
            else
                return (d).ToString() + "秒";
        }

        #endregion

        #region Array
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }
        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] PadStringArray(string[] strArray, int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                int t = maxLength;
                maxLength = minLength;
                minLength = t;
            }

            int iMiniStringCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                    continue;
                }
                if (strArray[i].Length > maxLength)
                {
                    strArray[i] = strArray[i].Substring(0, maxLength);
                }
                iMiniStringCount++;
            }

            string[] result = new string[iMiniStringCount];
            for (int i = 0, j = 0; i < strArray.Length && j < result.Length; i++)
            {
                if (strArray[i] != null && strArray[i] != string.Empty)
                {
                    result[j] = strArray[i];
                    j++;
                }
            }


            return result;
        }
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            string[] result = new string[h.Count];

            h.Keys.CopyTo(result, 0);

            return result;
        }
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }
        #endregion

        #region SQL

        public static string ReplaceString(string exp, string find, string replacement)
        {
            return Strings.Replace(exp, find, replacement, 1, -1, CompareMethod.Text);
        }

        public static string RemoveSpecString(string text)
        {
            return RemoveSpecString(text, -1);
        }

        public static string RemoveSpecString(string text, int len)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            text = ReplaceString(text, "'", "''");
            text = ReplaceString(text, "select", "select.", true);
            text = ReplaceString(text, "insert", "insert.", true);
            text = ReplaceString(text, "delete", "delete.", true);
            text = ReplaceString(text, "join", "join.", true);
            text = ReplaceString(text, "union", "union.", true);
            text = ReplaceString(text, "where", "where.", true);
            text = ReplaceString(text, "update", "update.", true);
            text = ReplaceString(text, "drop", "drop.", true);
            text = ReplaceString(text, "create", "create.", true);
            text = ReplaceString(text, "modify", "modify.", true);
            text = ReplaceString(text, "rename", "rename.", true);
            text = ReplaceString(text, "alter", "alter.", true);
            text = ReplaceString(text, "set", "set.", true);
            text = ReplaceString(text, "master", "master.", true);
            text = ReplaceString(text, "exec", "exec.", true);
            text = ReplaceString(text, "chr", "chr.", true);
            text = ReplaceString(text, "char", "char.", true);
            text = ReplaceString(text, "truncate", "truncate.", true);
            text = ReplaceString(text, "and", "and.", true);
            text = ReplaceString(text, "alter", "alter.", true);
            text = ReplaceString(text, "declare", "declare.", true);
            text = ReplaceString(text, "from", "from.", true);
            text = ReplaceString(text, "xp_cmdshell", "xp_cmdshell.", true);
            text = ReplaceString(text, "net user", "net user.", true);
            text = ReplaceString(text, "localgroup", "localgroup.", true);
            if (len != -1 && text.Length > len)
                text = text.Substring(1, len);
            return text;
        }

        private static string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        #endregion

    }
}
