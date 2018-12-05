using SharpConfig;

namespace JaminHuang.Util
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
