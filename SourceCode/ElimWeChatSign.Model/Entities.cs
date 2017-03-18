namespace ElimWeChatSign.Model
{
	using System;
    using System.Data;
    using System.Runtime.Serialization;
    using Titan;
    
/*
<system.runtime.serialization>
    <dataContractSerializer>
      <declaredTypes>
        <add type="Titan.BusinessEnumBase,Titan">
       </declaredTypes>
    </dataContractSerializer>
  </system.runtime.serialization>
*/

    #region Gather
    /// <summary>
    /// Gather,聚会签到表
    /// </summary>
    [DataContract]
    [Table(TableName="Gather",Label="Gather")] 
    public partial class Gather : Entity
    {
        
        public Gather()
        {
        }
        public Gather(string gatherId)
        {
           this.GatherId = gatherId;
        }

#region propertys
 
        private string _GatherId;

        /// <summary>
        /// GatherId,签到标识,主键
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string GatherId
        {
            get
            {
                return this._GatherId ?? string.Empty;
            }
            set
            {
                this._GatherId = value;
            }
        }

 
        private string _UserName;

        /// <summary>
        /// UserName,姓名
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string UserName
        {
            get
            {
                return this._UserName ?? string.Empty;
            }
            set
            {
                this._UserName = value;
            }
        }

 
        private string _GroupName;

        /// <summary>
        /// GroupName,小组名称
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string GroupName
        {
            get
            {
                return this._GroupName ?? string.Empty;
            }
            set
            {
                this._GroupName = value;
            }
        }

        /// <summary>
        /// GatherType,聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int GatherType {  get; set; }

 
        private string _IpAddress;

        /// <summary>
        /// IpAddress,IP地址
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string IpAddress
        {
            get
            {
                return this._IpAddress ?? string.Empty;
            }
            set
            {
                this._IpAddress = value;
            }
        }

        /// <summary>
        /// SignTime,签到时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime SignTime {  get; set; }


        #endregion

        #region link objects
        
        #endregion
        public static bool Exists(string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string gatherId){
            Gather obj=new Gather();
            obj.GatherId = gatherId;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            Gather obj=new Gather();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region Gathers

    /// <summary>
    /// Gather集合,聚会签到表
    /// </summary>
    [DataContract]
    public partial class Gathers:EntityCollection<Gather>
    {
    }
    #endregion
 

    #region GatherProperties
    public static class GatherProperties
    {
    
        static GatherProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _GatherId = new ExpressionBuilder("GatherId");
            _UserName = new ExpressionBuilder("UserName");
            _GroupName = new ExpressionBuilder("GroupName");
            _GatherType = new ExpressionBuilder("GatherType");
            _IpAddress = new ExpressionBuilder("IpAddress");
            _SignTime = new ExpressionBuilder("SignTime");


        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _GatherId;
        /// <summary>
        /// GatherId,签到标识,主键
        /// </summary>
        public static ExpressionBuilder GatherId { get{return _GatherId;}}
        private static ExpressionBuilder _UserName;
        /// <summary>
        /// UserName,姓名
        /// </summary>
        public static ExpressionBuilder UserName { get{return _UserName;}}
        private static ExpressionBuilder _GroupName;
        /// <summary>
        /// GroupName,小组名称
        /// </summary>
        public static ExpressionBuilder GroupName { get{return _GroupName;}}
        private static ExpressionBuilder _GatherType;
        /// <summary>
        /// GatherType,聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)
        /// </summary>
        public static ExpressionBuilder GatherType { get{return _GatherType;}}
        private static ExpressionBuilder _IpAddress;
        /// <summary>
        /// IpAddress,IP地址
        /// </summary>
        public static ExpressionBuilder IpAddress { get{return _IpAddress;}}
        private static ExpressionBuilder _SignTime;
        /// <summary>
        /// SignTime,签到时间
        /// </summary>
        public static ExpressionBuilder SignTime { get{return _SignTime;}}



    }
     #endregion
    #region GatherDescriptor
    public class GatherDescriptor
    {
     
        public GatherDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._GatherId = new ExpressionBuilder( prefix + "GatherId");
            this._UserName = new ExpressionBuilder( prefix + "UserName");
            this._GroupName = new ExpressionBuilder( prefix + "GroupName");
            this._GatherType = new ExpressionBuilder( prefix + "GatherType");
            this._IpAddress = new ExpressionBuilder( prefix + "IpAddress");
            this._SignTime = new ExpressionBuilder( prefix + "SignTime");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _GatherId;
        /// <summary>
        /// GatherId,签到标识,主键
        /// </summary>
        public ExpressionBuilder GatherId { get{return _GatherId;}}
        private ExpressionBuilder _UserName;
        /// <summary>
        /// UserName,姓名
        /// </summary>
        public ExpressionBuilder UserName { get{return _UserName;}}
        private ExpressionBuilder _GroupName;
        /// <summary>
        /// GroupName,小组名称
        /// </summary>
        public ExpressionBuilder GroupName { get{return _GroupName;}}
        private ExpressionBuilder _GatherType;
        /// <summary>
        /// GatherType,聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)
        /// </summary>
        public ExpressionBuilder GatherType { get{return _GatherType;}}
        private ExpressionBuilder _IpAddress;
        /// <summary>
        /// IpAddress,IP地址
        /// </summary>
        public ExpressionBuilder IpAddress { get{return _IpAddress;}}
        private ExpressionBuilder _SignTime;
        /// <summary>
        /// SignTime,签到时间
        /// </summary>
        public ExpressionBuilder SignTime { get{return _SignTime;}}



    }
     #endregion
}
