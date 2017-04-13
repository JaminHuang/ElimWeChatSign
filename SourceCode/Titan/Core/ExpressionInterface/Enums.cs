 

namespace Titan
{
    
    /// <summary>
    /// 排序方式，升序还是降序
    /// </summary>  
    public enum OrderType
    {
        /// <summary>升序</summary> 
        Asc = 0,
        /// <summary>降序</summary> 
        Desc = 1
    }


    /// <summary>运算符</summary>
    /// <seealso>ConditionExpression
    ///   <cref>ConditionExpression</cref>
    /// </seealso>
    /// <example>以下是示例 <c>MyMethod</c> is a method in the <c>MyClass</c>
    /// <code>
    /// asdasdf---
    /// asdasd
    /// <B><font color="#FF0000">ttt</font></B>
    ///     777
    /// yyy
    /// uuu
    /// </code>
    /// <list type="bullet">
    /// <item>
    /// <description>Item 1.</description>
    /// </item>
    /// <item>
    /// <description>Item 2.</description>
    /// </item>
    /// </list>
    /// </example> 
    public enum ConditionOperator
    {
        /// <summary>
        /// 大于
        /// </summary> 
        GreaterThan = 1,


        /// <summary>
        /// 小于
        /// </summary> 
        LessThan = 2,


        /// <summary>
        /// 等于
        /// </summary> 
        Equal = 0,


        /// <summary>
        /// 大于等于
        /// </summary> 
        GreaterThanOrEqual = 3,


        /// <summary>
        /// 小于等于
        /// </summary> 
        LessThanOrEqual = 4,


        /// <summary>
        /// 不等于
        /// </summary> 
        NotEqual = 5,


        /// <summary>
        /// 相似，会自动在前后添加“%”
        /// </summary> 
        Like = 6,


        /// <summary>
        /// 不相似，会自动添加“%”
        /// </summary> 
        NotLike = 7,


        /// <summary>
        /// 包含
        /// </summary> 
        In = 8,


        /// <summary>
        /// 不包含
        /// </summary> 
        NotIn = 9,


        /// <summary>
        /// 左相似，会自动在尾部添加“%”
        /// </summary> 
        LeftLike = 10,


        /// <summary>
        /// 非左相似，会自动在尾部添加“%”
        /// </summary> 
        NotLeftLike = 11,


        /// <summary>
        /// 右相似，会自动在头部添加“%”
        /// </summary> 
        RightLike = 12,


        /// <summary>
        /// 非右相似，会自动在头部添加“%”
        /// </summary> 
        NotRightLike = 13,


        /// <summary>
        /// 全文索引like
        /// </summary>
        FullTextLike = 14,


        /// <summary>
        /// 全文索引not like
        /// </summary>
        NotFullTextLike = 15,


        /// <summary>
        /// 自定义，必须在ConditionField的FieldValue属性中指定运算符
        /// </summary> 
        Custom = 99
    }



    /// <summary>各条件关系</summary> 
    public enum ConditionRelation
    {
        /// <summary>And</summary> 
        And = 0,
        /// <summary>Or</summary> 
        Or = 1
    }



    public enum GroupFunction
    {
        None = 0,
        Count = 1,
        Sum = 2,
        Avg = 3,
        Max = 4,
        Min = 5
    }
}
