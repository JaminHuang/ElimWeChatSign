using System.Collections.Generic; 

namespace Titan
{
    /// <summary>
    /// 描述一个条件集合
    /// </summary>
    public interface IConditionExpressionCollection : IList<ICondition>, ICondition
    {
        ConditionRelation ConditionRelation { get; set; }
        
    }
}
