using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JaminHuang.Core
{
    public class Logger
    {
        //在网站根目录下创建日志目录
        public static string path = HttpContext.Current.Request.PhysicalApplicationPath + "log";

        public static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        
        public static void Debug(object className, string content)
        {
            WriteLog("DEBUG", className, content);
        }
        
        public static void Info(object className, string content)
        {
            WriteLog("INFO", className, content);
        }
        
        public static void Error(object className, string content)
        {
            WriteLog("ERROR", className, content);
        }
        
        public static void Monitor(object className, string content)
        {
            WriteLog("MONITOR", className, content);
        }
        
        public static void Limit(object className, string content)
        {
            WriteLog("LIMIT", className, content);
        }

        public static void Create(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(fileName))
                {
                    fs.Close();
                }
            }
        }
        
        protected static void WriteLog(string type, object className, string content)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                string filename = path + "/" + type.ToLower() + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名
                string write_content = time + " " + type + " " + className.ToString() + ": " + content + "\r\n";

                Create(filename);

                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 8, FileOptions.Asynchronous))
                {
                    Byte[] dataArray = Encoding.Default.GetBytes(write_content);
                    bool flag = true;
                    long slen = dataArray.Length;
                    long len = 0;
                    while (flag)
                    {
                        try
                        {
                            if (len >= fs.Length)
                            {
                                fs.Lock(len, slen);
                                lockDic[len] = slen;
                                flag = false;
                            }
                            else
                            {
                                len = fs.Length;
                            }
                        }
                        catch (Exception ex)
                        {
                            while (!lockDic.ContainsKey(len))
                            {
                                len += lockDic[len];
                            }
                        }
                    }
                    fs.Seek(len, SeekOrigin.Begin);
                    fs.Write(dataArray, 0, dataArray.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("写入日志失败");
            }
        }
    }
}
