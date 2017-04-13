namespace Titan.SqlServer
{
    public class SqlServerMappingProvider:MappingProviderBase
    {

         

        #region 单例模式
        private static SqlServerMappingProvider instance = new SqlServerMappingProvider();
        public static SqlServerMappingProvider Instance
        {
            get
            {
                return instance;
            }
        }

        private SqlServerMappingProvider()
        { 
        }
        #endregion


        public override IAttributeReader CreateAttributeReader()
        {
            return new SqlServerAttributeReader();
        }
    }
}
