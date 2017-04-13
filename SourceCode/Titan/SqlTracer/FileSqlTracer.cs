using System;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Titan.SqlTracer
{
    public class FileSqlTracer:ISqlTracer
    {
         
        /// <summary>
        /// 日志保存的文件名，文件名中可以包含动态系统时间。
        /// 例如：
        /// "c:\\sqllog\\{yyyyMMdd_HH}.txt"，表示在c盘sqllog目录下每小时生成一个文件
        /// "c:\\sqllog\\{yyyyMMdd_HHmm}.txt"，表示在c盘sqllog目录下每分钟生成一个文件
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// 根据当前系统时间和配置的FileName属性获取真实文件名
        /// </summary>
        /// <returns></returns>
        private string GetActualFileName()
        {
            string file = FileName;
            Regex regex = new Regex("[\\{]{1}[^\\{\\}]*[\\}]{1}");
            MatchCollection ms = regex.Matches(file);
            if (ms != null)
            {
                foreach (Match m in ms)
                {
                    DateTime now = DateTime.Now;
                    file = file.Replace(m.Value, now.ToString(m.Value.Substring(1,m.Length-2)));
                }
            }
            return file;
        }
        private string CreateLogText(IDbCommand command)
        {
            
            if (command == null) return null;

            StringBuilder sb = new StringBuilder();
            sb.Append("Time:");
            sb.Append(DateTime.Now);
            sb.Append("\r\n");

            sb.Append("CommandText:(");
            sb.Append(command.CommandType);
            sb.Append(")");
            sb.Append(command.CommandText);
            sb.Append("\r\n");
            if (command.Parameters != null)
            {
                sb.Append("Parameters(Count=");
                sb.Append(command.Parameters.Count);
                sb.Append("):\r\n");
                foreach (IDataParameter parameter in command.Parameters)
                {
                    sb.Append("\t");
                    sb.Append(parameter.ParameterName);
                    sb.Append("\t");
                    sb.Append(parameter.DbType);
                    sb.Append("\t");
                    sb.Append(parameter.Direction);
                    sb.Append("\t");
                    sb.Append(parameter.Value);
                    sb.Append("\r\n");
                }
            }
            return sb.ToString();
        }

        private readonly object _lockObject = new object();
        private string _currentFileName = "";
        private StreamWriter _writer;
        private FileStream _fs;
        public void Trace(IDbCommand command)
        { 
            string fileName = GetActualFileName();  
            if (fileName != _currentFileName)
            {
                lock (_lockObject)
                {
                    if (fileName != _currentFileName)
                    {
                        if (_writer != null)
                        {
                            //说明上次已经打开需要先关闭
                            try
                            {
                                _writer.Close();
                                _fs.Close();
                            }
                            catch { }
                            _writer = null;
                            _fs = null;
                        }
                        FileInfo fi = new FileInfo(fileName);
                        if (!fi.Directory.Exists) fi.Directory.Create();
                        _fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite); 
                        _writer = new StreamWriter(_fs,Encoding.UTF8);
                        _currentFileName = fileName;
                    }
                }
            }

            string text = CreateLogText(command);
            lock (_lockObject)
            {
                _writer.WriteLine(text);
                _writer.Flush();//设置了AutoFlush=true就不用flush
            }
             
        }


        //public void Initialize(IDictionary<string, string> parameters)
        //{
        //    //定义一个不区分大小写的集合
        //    Dictionary<string, string> noneRepeatedParameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        //    foreach (KeyValuePair<string, string> kv in parameters)
        //    {
        //        if (!noneRepeatedParameters.ContainsKey(kv.Key))
        //        {
        //            noneRepeatedParameters.Add(kv.Key, kv.Value);
        //        }
        //    }
        //    if (parameters.ContainsKey("FileName"))
        //    {
        //        FileName = parameters["FileName"];
        //    }
        //    else
        //    {
        //        throw ExceptionFactory.SettingsPropertyNotFound("FileName");
        //    } 
        //}
    }
}
