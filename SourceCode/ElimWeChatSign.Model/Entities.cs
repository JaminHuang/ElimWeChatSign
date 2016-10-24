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
#region enums

#endregion



    #region UserInfo
    /// <summary>
    /// UserInfo,用户表
    /// </summary>
    [DataContract]
    [Table(TableName="UserInfo",Label="UserInfo")] 
    public partial class UserInfo : Entity
    {
        
        public UserInfo()
        {
        }
        public UserInfo(string userId)
        {
           this.UserId = userId;
        }

#region propertys
 
        private string _UserId;

        /// <summary>
        /// UserId,用户标识,主键
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string UserId
        {
            get
            {
                return this._UserId ?? string.Empty;
            }
            set
            {
                this._UserId = value;
            }
        }

 
        private string _UserName;

        /// <summary>
        /// UserName,用户名
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

 
        private string _Mobile;

        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string Mobile
        {
            get
            {
                return this._Mobile ?? string.Empty;
            }
            set
            {
                this._Mobile = value;
            }
        }

 
        private string _Password;

        /// <summary>
        /// Password,密码
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 50)]  
        [MySqlDbType(DbType.String)] 
        public string Password
        {
            get
            {
                return this._Password ?? string.Empty;
            }
            set
            {
                this._Password = value;
            }
        }

 
        private string _Email;

        /// <summary>
        /// Email,邮箱
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 30)]  
        [MySqlDbType(DbType.String)] 
        public string Email
        {
            get
            {
                return this._Email ?? string.Empty;
            }
            set
            {
                this._Email = value;
            }
        }

        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int UserType {  get; set; }

        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime UpdateTime {  get; set; }


        #endregion

        #region link objects
        
        #endregion
        public static bool Exists(string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string userId){
            UserInfo obj=new UserInfo();
            obj.UserId = userId;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            UserInfo obj=new UserInfo();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region UserInfos

    /// <summary>
    /// UserInfo集合,用户表
    /// </summary>
    [DataContract]
    public partial class UserInfos:EntityCollection<UserInfo>
    {
    }
    #endregion
 

    #region UserInfoProperties
    public static class UserInfoProperties
    {
    
        static UserInfoProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _UserId = new ExpressionBuilder("UserId");
            _UserName = new ExpressionBuilder("UserName");
            _Mobile = new ExpressionBuilder("Mobile");
            _Password = new ExpressionBuilder("Password");
            _Email = new ExpressionBuilder("Email");
            _UserType = new ExpressionBuilder("UserType");
            _UpdateTime = new ExpressionBuilder("UpdateTime");


        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识,主键
        /// </summary>
        public static ExpressionBuilder UserId { get{return _UserId;}}
        private static ExpressionBuilder _UserName;
        /// <summary>
        /// UserName,用户名
        /// </summary>
        public static ExpressionBuilder UserName { get{return _UserName;}}
        private static ExpressionBuilder _Mobile;
        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        public static ExpressionBuilder Mobile { get{return _Mobile;}}
        private static ExpressionBuilder _Password;
        /// <summary>
        /// Password,密码
        /// </summary>
        public static ExpressionBuilder Password { get{return _Password;}}
        private static ExpressionBuilder _Email;
        /// <summary>
        /// Email,邮箱
        /// </summary>
        public static ExpressionBuilder Email { get{return _Email;}}
        private static ExpressionBuilder _UserType;
        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        public static ExpressionBuilder UserType { get{return _UserType;}}
        private static ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public static ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



    }
     #endregion
    #region UserInfoDescriptor
    public class UserInfoDescriptor
    {
     
        public UserInfoDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._UserId = new ExpressionBuilder( prefix + "UserId");
            this._UserName = new ExpressionBuilder( prefix + "UserName");
            this._Mobile = new ExpressionBuilder( prefix + "Mobile");
            this._Password = new ExpressionBuilder( prefix + "Password");
            this._Email = new ExpressionBuilder( prefix + "Email");
            this._UserType = new ExpressionBuilder( prefix + "UserType");
            this._UpdateTime = new ExpressionBuilder( prefix + "UpdateTime");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识,主键
        /// </summary>
        public ExpressionBuilder UserId { get{return _UserId;}}
        private ExpressionBuilder _UserName;
        /// <summary>
        /// UserName,用户名
        /// </summary>
        public ExpressionBuilder UserName { get{return _UserName;}}
        private ExpressionBuilder _Mobile;
        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        public ExpressionBuilder Mobile { get{return _Mobile;}}
        private ExpressionBuilder _Password;
        /// <summary>
        /// Password,密码
        /// </summary>
        public ExpressionBuilder Password { get{return _Password;}}
        private ExpressionBuilder _Email;
        /// <summary>
        /// Email,邮箱
        /// </summary>
        public ExpressionBuilder Email { get{return _Email;}}
        private ExpressionBuilder _UserType;
        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        public ExpressionBuilder UserType { get{return _UserType;}}
        private ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



    }
     #endregion

    #region UserPlan
    /// <summary>
    /// UserPlan,计划表
    /// </summary>
    [DataContract]
    [Table(TableName="UserPlan",Label="UserPlan")] 
    public partial class UserPlan : Entity
    {
        
        public UserPlan()
        {
        }
        public UserPlan(string planId)
        {
           this.PlanId = planId;
        }

#region propertys
 
        private string _PlanId;

        /// <summary>
        /// PlanId,计划标识,主键
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string PlanId
        {
            get
            {
                return this._PlanId ?? string.Empty;
            }
            set
            {
                this._PlanId = value;
            }
        }

 
        private string _UserId;

        /// <summary>
        /// UserId,用户标识
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string UserId
        {
            get
            {
                return this._UserId ?? string.Empty;
            }
            set
            {
                this._UserId = value;
            }
        }

 
        private string _BiblePlan;

        /// <summary>
        /// BiblePlan,读经计划
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 2147483647)]  
        [MySqlDbType(DbType.String)] 
        public string BiblePlan
        {
            get
            {
                return this._BiblePlan ?? string.Empty;
            }
            set
            {
                this._BiblePlan = value;
            }
        }

 
        private string _BookPlan;

        /// <summary>
        /// BookPlan,读书计划
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 2147483647)]  
        [MySqlDbType(DbType.String)] 
        public string BookPlan
        {
            get
            {
                return this._BookPlan ?? string.Empty;
            }
            set
            {
                this._BookPlan = value;
            }
        }

        /// <summary>
        /// StartDate,开始时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime StartDate {  get; set; }

        /// <summary>
        /// EndDate,结束时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime EndDate {  get; set; }

        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime UpdateTime {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Relation("UserId,UserId")]
        public UserInfo UserInfo { get;  set;  } 



        #endregion
        public static bool Exists(string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string planId){
            UserPlan obj=new UserPlan();
            obj.PlanId = planId;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            UserPlan obj=new UserPlan();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region UserPlans

    /// <summary>
    /// UserPlan集合,计划表
    /// </summary>
    [DataContract]
    public partial class UserPlans:EntityCollection<UserPlan>
    {
    }
    #endregion
 

    #region UserPlanProperties
    public static class UserPlanProperties
    {
    
        static UserPlanProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _PlanId = new ExpressionBuilder("PlanId");
            _UserId = new ExpressionBuilder("UserId");
            _BiblePlan = new ExpressionBuilder("BiblePlan");
            _BookPlan = new ExpressionBuilder("BookPlan");
            _StartDate = new ExpressionBuilder("StartDate");
            _EndDate = new ExpressionBuilder("EndDate");
            _UpdateTime = new ExpressionBuilder("UpdateTime");


            _UserInfo = new UserInfoDescriptor("UserInfo.");
        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,计划标识,主键
        /// </summary>
        public static ExpressionBuilder PlanId { get{return _PlanId;}}
        private static ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public static ExpressionBuilder UserId { get{return _UserId;}}
        private static ExpressionBuilder _BiblePlan;
        /// <summary>
        /// BiblePlan,读经计划
        /// </summary>
        public static ExpressionBuilder BiblePlan { get{return _BiblePlan;}}
        private static ExpressionBuilder _BookPlan;
        /// <summary>
        /// BookPlan,读书计划
        /// </summary>
        public static ExpressionBuilder BookPlan { get{return _BookPlan;}}
        private static ExpressionBuilder _StartDate;
        /// <summary>
        /// StartDate,开始时间
        /// </summary>
        public static ExpressionBuilder StartDate { get{return _StartDate;}}
        private static ExpressionBuilder _EndDate;
        /// <summary>
        /// EndDate,结束时间
        /// </summary>
        public static ExpressionBuilder EndDate { get{return _EndDate;}}
        private static ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public static ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private static UserInfoDescriptor _UserInfo;
        public static UserInfoDescriptor UserInfo { get{return _UserInfo;}}
    }
     #endregion
    #region UserPlanDescriptor
    public class UserPlanDescriptor
    {
     
        public UserPlanDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._PlanId = new ExpressionBuilder( prefix + "PlanId");
            this._UserId = new ExpressionBuilder( prefix + "UserId");
            this._BiblePlan = new ExpressionBuilder( prefix + "BiblePlan");
            this._BookPlan = new ExpressionBuilder( prefix + "BookPlan");
            this._StartDate = new ExpressionBuilder( prefix + "StartDate");
            this._EndDate = new ExpressionBuilder( prefix + "EndDate");
            this._UpdateTime = new ExpressionBuilder( prefix + "UpdateTime");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,计划标识,主键
        /// </summary>
        public ExpressionBuilder PlanId { get{return _PlanId;}}
        private ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public ExpressionBuilder UserId { get{return _UserId;}}
        private ExpressionBuilder _BiblePlan;
        /// <summary>
        /// BiblePlan,读经计划
        /// </summary>
        public ExpressionBuilder BiblePlan { get{return _BiblePlan;}}
        private ExpressionBuilder _BookPlan;
        /// <summary>
        /// BookPlan,读书计划
        /// </summary>
        public ExpressionBuilder BookPlan { get{return _BookPlan;}}
        private ExpressionBuilder _StartDate;
        /// <summary>
        /// StartDate,开始时间
        /// </summary>
        public ExpressionBuilder StartDate { get{return _StartDate;}}
        private ExpressionBuilder _EndDate;
        /// <summary>
        /// EndDate,结束时间
        /// </summary>
        public ExpressionBuilder EndDate { get{return _EndDate;}}
        private ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private UserInfoDescriptor _UserInfo;
        public UserInfoDescriptor UserInfo 
        { 
            get
            {
                if(_UserInfo==null) _UserInfo=new UserInfoDescriptor(this._Prefix+"UserInfo.");
                return _UserInfo;
            }
        }
    }
     #endregion

    #region UserSign
    /// <summary>
    /// UserSign,用户签到表
    /// </summary>
    [DataContract]
    [Table(TableName="UserSign",Label="UserSign")] 
    public partial class UserSign : Entity
    {
        
        public UserSign()
        {
        }
        public UserSign(string signId)
        {
           this.SignId = signId;
        }

#region propertys
 
        private string _SignId;

        /// <summary>
        /// SignId,签到标识,主键
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string SignId
        {
            get
            {
                return this._SignId ?? string.Empty;
            }
            set
            {
                this._SignId = value;
            }
        }

 
        private string _UserId;

        /// <summary>
        /// UserId,用户标识
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string UserId
        {
            get
            {
                return this._UserId ?? string.Empty;
            }
            set
            {
                this._UserId = value;
            }
        }

 
        private string _PlanId;

        /// <summary>
        /// PlanId,签到计划
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string PlanId
        {
            get
            {
                return this._PlanId ?? string.Empty;
            }
            set
            {
                this._PlanId = value;
            }
        }

        /// <summary>
        /// SignType,签到类型(1:读经;2:读书)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int SignType {  get; set; }

        /// <summary>
        /// SignDate,签到时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime SignDate {  get; set; }

        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime UpdateTime {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Relation("UserId,UserId")]
        public UserInfo UserInfo { get;  set;  } 



        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Relation("PlanId,PlanId")]
        public UserPlan UserPlan { get;  set;  } 



        #endregion
        public static bool Exists(string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string signId){
            UserSign obj=new UserSign();
            obj.SignId = signId;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            UserSign obj=new UserSign();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region UserSigns

    /// <summary>
    /// UserSign集合,用户签到表
    /// </summary>
    [DataContract]
    public partial class UserSigns:EntityCollection<UserSign>
    {
    }
    #endregion
 

    #region UserSignProperties
    public static class UserSignProperties
    {
    
        static UserSignProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _SignId = new ExpressionBuilder("SignId");
            _UserId = new ExpressionBuilder("UserId");
            _PlanId = new ExpressionBuilder("PlanId");
            _SignType = new ExpressionBuilder("SignType");
            _SignDate = new ExpressionBuilder("SignDate");
            _UpdateTime = new ExpressionBuilder("UpdateTime");


            _UserInfo = new UserInfoDescriptor("UserInfo.");
            _UserPlan = new UserPlanDescriptor("UserPlan.");
        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _SignId;
        /// <summary>
        /// SignId,签到标识,主键
        /// </summary>
        public static ExpressionBuilder SignId { get{return _SignId;}}
        private static ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public static ExpressionBuilder UserId { get{return _UserId;}}
        private static ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,签到计划
        /// </summary>
        public static ExpressionBuilder PlanId { get{return _PlanId;}}
        private static ExpressionBuilder _SignType;
        /// <summary>
        /// SignType,签到类型(1:读经;2:读书)
        /// </summary>
        public static ExpressionBuilder SignType { get{return _SignType;}}
        private static ExpressionBuilder _SignDate;
        /// <summary>
        /// SignDate,签到时间
        /// </summary>
        public static ExpressionBuilder SignDate { get{return _SignDate;}}
        private static ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public static ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private static UserInfoDescriptor _UserInfo;
        public static UserInfoDescriptor UserInfo { get{return _UserInfo;}}
        private static UserPlanDescriptor _UserPlan;
        public static UserPlanDescriptor UserPlan { get{return _UserPlan;}}
    }
     #endregion
    #region UserSignDescriptor
    public class UserSignDescriptor
    {
     
        public UserSignDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._SignId = new ExpressionBuilder( prefix + "SignId");
            this._UserId = new ExpressionBuilder( prefix + "UserId");
            this._PlanId = new ExpressionBuilder( prefix + "PlanId");
            this._SignType = new ExpressionBuilder( prefix + "SignType");
            this._SignDate = new ExpressionBuilder( prefix + "SignDate");
            this._UpdateTime = new ExpressionBuilder( prefix + "UpdateTime");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _SignId;
        /// <summary>
        /// SignId,签到标识,主键
        /// </summary>
        public ExpressionBuilder SignId { get{return _SignId;}}
        private ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public ExpressionBuilder UserId { get{return _UserId;}}
        private ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,签到计划
        /// </summary>
        public ExpressionBuilder PlanId { get{return _PlanId;}}
        private ExpressionBuilder _SignType;
        /// <summary>
        /// SignType,签到类型(1:读经;2:读书)
        /// </summary>
        public ExpressionBuilder SignType { get{return _SignType;}}
        private ExpressionBuilder _SignDate;
        /// <summary>
        /// SignDate,签到时间
        /// </summary>
        public ExpressionBuilder SignDate { get{return _SignDate;}}
        private ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private UserInfoDescriptor _UserInfo;
        public UserInfoDescriptor UserInfo 
        { 
            get
            {
                if(_UserInfo==null) _UserInfo=new UserInfoDescriptor(this._Prefix+"UserInfo.");
                return _UserInfo;
            }
        }
        private UserPlanDescriptor _UserPlan;
        public UserPlanDescriptor UserPlan 
        { 
            get
            {
                if(_UserPlan==null) _UserPlan=new UserPlanDescriptor(this._Prefix+"UserPlan.");
                return _UserPlan;
            }
        }
    }
     #endregion

    #region RptSign
    /// <summary>
    /// RptSign,统计表
    /// </summary>
    [DataContract]
    [Table(TableName="RptSign",Label="RptSign")] 
    public partial class RptSign : Entity
    {
        
        public RptSign()
        {
        }
        public RptSign(string rptId)
        {
           this.RptId = rptId;
        }

#region propertys
 
        private string _RptId;

        /// <summary>
        /// RptId,报表标识
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string RptId
        {
            get
            {
                return this._RptId ?? string.Empty;
            }
            set
            {
                this._RptId = value;
            }
        }

 
        private string _UserId;

        /// <summary>
        /// UserId,用户标识
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string UserId
        {
            get
            {
                return this._UserId ?? string.Empty;
            }
            set
            {
                this._UserId = value;
            }
        }

 
        private string _PlanId;

        /// <summary>
        /// PlanId,计划标识
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string PlanId
        {
            get
            {
                return this._PlanId ?? string.Empty;
            }
            set
            {
                this._PlanId = value;
            }
        }

 
        private string _BibleTask;

        /// <summary>
        /// BibleTask,读经完成度
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string BibleTask
        {
            get
            {
                return this._BibleTask ?? string.Empty;
            }
            set
            {
                this._BibleTask = value;
            }
        }

 
        private string _BookTask;

        /// <summary>
        /// BookTask,读书完成度
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string BookTask
        {
            get
            {
                return this._BookTask ?? string.Empty;
            }
            set
            {
                this._BookTask = value;
            }
        }

        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.DateTime)] 
        public DateTime UpdateTime {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Relation("UserId,UserId")]
        public UserInfo UserInfo { get;  set;  } 



        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [Relation("PlanId,PlanId")]
        public UserPlan UserPlan { get;  set;  } 



        #endregion
        public static bool Exists(string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string rptId){
            RptSign obj=new RptSign();
            obj.RptId = rptId;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            RptSign obj=new RptSign();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region RptSigns

    /// <summary>
    /// RptSign集合,统计表
    /// </summary>
    [DataContract]
    public partial class RptSigns:EntityCollection<RptSign>
    {
    }
    #endregion
 

    #region RptSignProperties
    public static class RptSignProperties
    {
    
        static RptSignProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _RptId = new ExpressionBuilder("RptId");
            _UserId = new ExpressionBuilder("UserId");
            _PlanId = new ExpressionBuilder("PlanId");
            _BibleTask = new ExpressionBuilder("BibleTask");
            _BookTask = new ExpressionBuilder("BookTask");
            _UpdateTime = new ExpressionBuilder("UpdateTime");


            _UserInfo = new UserInfoDescriptor("UserInfo.");
            _UserPlan = new UserPlanDescriptor("UserPlan.");
        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _RptId;
        /// <summary>
        /// RptId,报表标识
        /// </summary>
        public static ExpressionBuilder RptId { get{return _RptId;}}
        private static ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public static ExpressionBuilder UserId { get{return _UserId;}}
        private static ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,计划标识
        /// </summary>
        public static ExpressionBuilder PlanId { get{return _PlanId;}}
        private static ExpressionBuilder _BibleTask;
        /// <summary>
        /// BibleTask,读经完成度
        /// </summary>
        public static ExpressionBuilder BibleTask { get{return _BibleTask;}}
        private static ExpressionBuilder _BookTask;
        /// <summary>
        /// BookTask,读书完成度
        /// </summary>
        public static ExpressionBuilder BookTask { get{return _BookTask;}}
        private static ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public static ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private static UserInfoDescriptor _UserInfo;
        public static UserInfoDescriptor UserInfo { get{return _UserInfo;}}
        private static UserPlanDescriptor _UserPlan;
        public static UserPlanDescriptor UserPlan { get{return _UserPlan;}}
    }
     #endregion
    #region RptSignDescriptor
    public class RptSignDescriptor
    {
     
        public RptSignDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._RptId = new ExpressionBuilder( prefix + "RptId");
            this._UserId = new ExpressionBuilder( prefix + "UserId");
            this._PlanId = new ExpressionBuilder( prefix + "PlanId");
            this._BibleTask = new ExpressionBuilder( prefix + "BibleTask");
            this._BookTask = new ExpressionBuilder( prefix + "BookTask");
            this._UpdateTime = new ExpressionBuilder( prefix + "UpdateTime");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _RptId;
        /// <summary>
        /// RptId,报表标识
        /// </summary>
        public ExpressionBuilder RptId { get{return _RptId;}}
        private ExpressionBuilder _UserId;
        /// <summary>
        /// UserId,用户标识
        /// </summary>
        public ExpressionBuilder UserId { get{return _UserId;}}
        private ExpressionBuilder _PlanId;
        /// <summary>
        /// PlanId,计划标识
        /// </summary>
        public ExpressionBuilder PlanId { get{return _PlanId;}}
        private ExpressionBuilder _BibleTask;
        /// <summary>
        /// BibleTask,读经完成度
        /// </summary>
        public ExpressionBuilder BibleTask { get{return _BibleTask;}}
        private ExpressionBuilder _BookTask;
        /// <summary>
        /// BookTask,读书完成度
        /// </summary>
        public ExpressionBuilder BookTask { get{return _BookTask;}}
        private ExpressionBuilder _UpdateTime;
        /// <summary>
        /// UpdateTime,最后修改时间
        /// </summary>
        public ExpressionBuilder UpdateTime { get{return _UpdateTime;}}



        private UserInfoDescriptor _UserInfo;
        public UserInfoDescriptor UserInfo 
        { 
            get
            {
                if(_UserInfo==null) _UserInfo=new UserInfoDescriptor(this._Prefix+"UserInfo.");
                return _UserInfo;
            }
        }
        private UserPlanDescriptor _UserPlan;
        public UserPlanDescriptor UserPlan 
        { 
            get
            {
                if(_UserPlan==null) _UserPlan=new UserPlanDescriptor(this._Prefix+"UserPlan.");
                return _UserPlan;
            }
        }
    }
     #endregion
}
