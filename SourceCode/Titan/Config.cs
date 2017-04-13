namespace Titan
{
    public static class Config
    {
        private static bool useExpressionAnalyseCache = true;

        /// <summary>
        /// 分析QueryExpression时是否启用缓存
        /// </summary>
        public static bool UseExpressionAnalyseCache
        {
            get { return useExpressionAnalyseCache; }
            set { useExpressionAnalyseCache = value; }
        }
    }
}
