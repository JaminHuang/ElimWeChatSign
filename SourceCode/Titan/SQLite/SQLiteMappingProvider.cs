namespace Titan.SQLite
{
    public class SQLiteMappingProvider:MappingProviderBase
    {

         

        #region 单例模式
        private static SQLiteMappingProvider instance = new SQLiteMappingProvider();
        public static SQLiteMappingProvider Instance
        {
            get
            {
                return instance;
            }
        }

        private SQLiteMappingProvider()
        { 
        }
        #endregion


        public override IAttributeReader CreateAttributeReader()
        {
            return new SQLiteAttributeReader();
        }
    }
}
