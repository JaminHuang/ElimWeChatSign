namespace Titan.Oracle
{
    public class OracleMappingProvider:MappingProviderBase
    {

         

        #region 单例模式
        private static OracleMappingProvider instance = new OracleMappingProvider();
        public static OracleMappingProvider Instance
        {
            get
            {
                return instance;
            }
        }

        private OracleMappingProvider()
        { 
        }
        #endregion


        public override IAttributeReader CreateAttributeReader()
        {
            return new OracleAttributeReader();
        }
    }
}
