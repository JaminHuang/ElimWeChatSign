using SharpConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElimWeChatSign.Core
{
    public class ConfigHelper
    {
        private static Configuration instance;
        private static object lockHelper = new object();

        private static void CreateInstance()
        {
            instance = Configuration.LoadFromFile(ExtendUtil.GetMapPath("~/Config/base.cfx"));
        }

        public static Configuration GetInstance()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        CreateInstance();
                    }
                    return instance;
                }
            }
            return instance;
        }

        public static void Dispose()
        {
            instance = null;
        }
    }
}
