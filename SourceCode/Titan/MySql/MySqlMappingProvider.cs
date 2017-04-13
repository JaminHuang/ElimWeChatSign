namespace Titan.MySql
{
    public class MySqlMappingProvider:MappingProviderBase
    {

         

        #region 单例模式
        private static readonly MySqlMappingProvider instance = new MySqlMappingProvider();
        public static MySqlMappingProvider Instance
        {
            get
            {
                return instance;
            }
        }

        private MySqlMappingProvider()
        { 
        }
        #endregion


        public override IAttributeReader CreateAttributeReader()
        {
            return new MySqlAttributeReader();
        }
    }
}
