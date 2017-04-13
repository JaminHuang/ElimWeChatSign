using System.Data;
using Titan.ExpressionAnalyse;

namespace Titan
{
    public class SelectionResult
    {
        public int TotalMatchedCount { get; set; }
        public IDataReader DataReader { get; set; }
        public ObjectFiller ObjectFiller { get; set; }
    }
}
