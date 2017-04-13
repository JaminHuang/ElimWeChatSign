using System;
using System.Collections.Generic;

namespace Titan
{
    /// <summary>
    /// 
    /// </summary>
    public class TableBase : ITable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TableBase() 
        {
            Columns = new Dictionary<string, IColumn>(StringComparer.OrdinalIgnoreCase);
            Relations = new Dictionary<string, IRelation>(StringComparer.OrdinalIgnoreCase);
             
            PrimaryProperties = new string[0]; 
            NonPrimaryKeyProperties = new string[0];
            InsertProperties = new string[0];


            GroupColumns = new Dictionary<IPropertyExpression, IGroupColumn>();
        }


        #region 属性

         
        public string TableName { get; set; }
        public Dictionary<string, IColumn> Columns { get; set; }
        public Dictionary<string, IRelation> Relations { get; set; }

        #endregion


        #region 缓存 
        public string[] PrimaryProperties { get; private set; }
        public string[] NonPrimaryKeyProperties { get; private set; }
        public string[] InsertProperties { get; private set; }

        #endregion

       

        //public void LoadMapping(EntityType entityType)
        //{
        //    ReadAttribute(entityType);

        //    //必须要查找出RelationColumn的ColumnName 
        //    foreach (IRelation relation in Relations.Values)
        //    {
        //        foreach (IRelationColumn relationColumn in relation.RelationColumns)
        //        {
        //            string columnName = Columns[relationColumn.PropertyName].ColumnName;

        //            TableBase<A, TC, TR> table = new TableBase<A, TC, TR>();
        //            table.ReadAttribute(relation.PropertyAdapter.PropertyType);
        //            string foreignColumnName = table.Columns[relationColumn.ForeignPropertyName].ColumnName;
        //            relationColumn.ColumnName = columnName;
        //            relationColumn.ForeignColumnName = foreignColumnName;
        //        }
        //    }


        //    CreateCache();
        //}


      
         

        public virtual void CreateCache()
        {

            //生成缓存  
            List<string> tmpPrimaryColumns = new List<string>(Columns.Count);
            List<string> tmpNoneKeyProperties = new List<string>(Columns.Count);
            List<string> tmpInsertProperties = new List<string>(Columns.Count);
            foreach (KeyValuePair<string,IColumn> kv in Columns)
            {
                if (kv.Value.InsertBehavior.Generate)
                { 
                    tmpInsertProperties.Add(kv.Key);
                }

                if (kv.Value.IsPrimaryKey)
                {
                    //以下是主键字段
                    tmpPrimaryColumns.Add(kv.Key); 
                }
                else
                {
                    //以下是非主键字段
                    //PropertyExpression a = new PropertyExpression(column.PropertyAdapter.PropertyName);
                    tmpNoneKeyProperties.Add(kv.Key);
                }
            }
            PrimaryProperties = tmpPrimaryColumns.ToArray();
            InsertProperties = tmpInsertProperties.ToArray(); 
            NonPrimaryKeyProperties = tmpNoneKeyProperties.ToArray();
        }

        #region group
        //public virtual IGroupColumn GetGroupColumn(string forProperty, GroupFunction groupFunction)
        //{
        //    return GroupColumns[Util.ToGroupPropertyKey(forProperty, groupFunction)];
        //}
        //public virtual void AddGroupColumn(IGroupColumn groupColumn)
        //{
        //    GroupColumns.Add(Util.ToGroupPropertyKey(groupColumn.PropertyName, groupColumn.GroupFunction), groupColumn);
        //}
        //public virtual bool ContainsGroupColumn(string forProperty, GroupFunction groupFunction)
        //{
        //    return GroupColumns.ContainsKey(Util.ToGroupPropertyKey(forProperty, groupFunction));
        //}
        ///// <summary>
        ///// 以propertyName+"_"+groupfunction作为主键
        ///// </summary>
        //public Dictionary<string, IGroupColumn> GroupColumns { get; set; }
        public Dictionary<IPropertyExpression, IGroupColumn> GroupColumns { get; set; }
        #endregion
    }

}
