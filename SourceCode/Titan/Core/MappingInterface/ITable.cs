using System.Collections.Generic;

namespace Titan
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITable
    {


        #region 属性


        /// <summary>
        /// 数据库中的表名称，如“[Customer]”
        /// </summary>
        string TableName { get; set; }

        /// <summary>
        /// 以属性名作为键
        /// </summary>
        Dictionary<string, IColumn> Columns { get; set; }

        /// <summary>
        /// 以属性名作为键
        /// </summary>
        Dictionary<string, IRelation> Relations { get; set; }

        #endregion


        #region 缓存 


        string[] NonPrimaryKeyProperties { get; }
        string[] InsertProperties { get; }
        string[] PrimaryProperties { get; }


        #endregion
         

        /// <summary>
        /// 创建缓存
        /// </summary>
        void CreateCache();


        #region group
        //IGroupColumn GetGroupColumn(string forProperty, GroupFunction groupFunction);
        //void AddGroupColumn(IGroupColumn groupColumn);
        //bool ContainsGroupColumn(string forProperty, GroupFunction groupFunction);

        ///// <summary>
        ///// 以propertyName+"_"+groupfunction作为主键
        ///// </summary>
        //Dictionary<string, IGroupColumn> GroupColumns { get; set; }
        Dictionary<IPropertyExpression,IGroupColumn> GroupColumns { get; set; }
        #endregion
    }

}
