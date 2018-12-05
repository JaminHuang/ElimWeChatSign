using Jil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Core.Model
{
    public class JilFormatter : MediaTypeFormatter
    {
        private readonly Options _jilOptions;

        public JilFormatter()
        {
            //要序列化的时间格式
            _jilOptions = new Options(dateFormat: DateTimeFormat.ISO8601);
            //媒体类型
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            //加入 UTF8Encoding 编码
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            //加入 UnicodeEncoding 编码
            SupportedEncodings.Add(new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true));
        }

        //判断是否反序列化类型
        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return true;
        }
        //判断是否序列化类型
        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return true;
        }

        //  异步反序列化一个指定类型的对象。
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            return Task.FromResult(DeserializeFromStream(type, readStream));
        }

        private object DeserializeFromStream(Type type, Stream readStream)
        {
            try
            {
                using (var reader = new StreamReader(readStream))
                {
                    return JSON.Deserialize(reader, type, _jilOptions);
                }
            }
            catch
            {
                return null;
            }
        }

        //  异步序列化一个指定类型的对象。
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
        {
            var streamWriter = new StreamWriter(writeStream);
            JSON.Serialize(value, streamWriter, _jilOptions);
            streamWriter.Flush();
            return Task.FromResult(writeStream);
        }
    }
}
