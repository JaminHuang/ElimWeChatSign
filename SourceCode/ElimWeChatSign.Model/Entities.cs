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

 
        private string _Avatar;

        /// <summary>
        /// Avatar,头像
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 200)]  
        [MySqlDbType(DbType.String)] 
        public string Avatar
        {
            get
            {
                return this._Avatar ?? string.Empty;
            }
            set
            {
                this._Avatar = value;
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
        /// Gender,性别(0:女;1:男)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int Gender {  get; set; }

        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int UserType {  get; set; }

 
        private string _Os;

        /// <summary>
        /// Os,设备操作系统
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 50)]  
        [MySqlDbType(DbType.String)] 
        public string Os
        {
            get
            {
                return this._Os ?? string.Empty;
            }
            set
            {
                this._Os = value;
            }
        }

 
        private string _OsVersion;

        /// <summary>
        /// OsVersion,设备系统版本
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 50)]  
        [MySqlDbType(DbType.String)] 
        public string OsVersion
        {
            get
            {
                return this._OsVersion ?? string.Empty;
            }
            set
            {
                this._OsVersion = value;
            }
        }

 
        private string _DeviceID;

        /// <summary>
        /// DeviceID,设备ID
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 200)]  
        [MySqlDbType(DbType.String)] 
        public string DeviceID
        {
            get
            {
                return this._DeviceID ?? string.Empty;
            }
            set
            {
                this._DeviceID = value;
            }
        }

 
        private string _DeviceToken;

        /// <summary>
        /// DeviceToken,友盟推送的设备唯一标识
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 100)]  
        [MySqlDbType(DbType.String)] 
        public string DeviceToken
        {
            get
            {
                return this._DeviceToken ?? string.Empty;
            }
            set
            {
                this._DeviceToken = value;
            }
        }

 
        private string _LoginIp;

        /// <summary>
        /// LoginIp,登录用户IP
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 50)]  
        [MySqlDbType(DbType.String)] 
        public string LoginIp
        {
            get
            {
                return this._LoginIp ?? string.Empty;
            }
            set
            {
                this._LoginIp = value;
            }
        }

 
        private string _AppVersion;

        /// <summary>
        /// AppVersion,使用app版本
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 50)]  
        [MySqlDbType(DbType.String)] 
        public string AppVersion
        {
            get
            {
                return this._AppVersion ?? string.Empty;
            }
            set
            {
                this._AppVersion = value;
            }
        }

 
        private string _AuthToken;

        /// <summary>
        /// AuthToken,授权Token
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 100)]  
        [MySqlDbType(DbType.String)] 
        public string AuthToken
        {
            get
            {
                return this._AuthToken ?? string.Empty;
            }
            set
            {
                this._AuthToken = value;
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
            _Avatar = new ExpressionBuilder("Avatar");
            _Email = new ExpressionBuilder("Email");
            _Gender = new ExpressionBuilder("Gender");
            _UserType = new ExpressionBuilder("UserType");
            _Os = new ExpressionBuilder("Os");
            _OsVersion = new ExpressionBuilder("OsVersion");
            _DeviceID = new ExpressionBuilder("DeviceID");
            _DeviceToken = new ExpressionBuilder("DeviceToken");
            _LoginIp = new ExpressionBuilder("LoginIp");
            _AppVersion = new ExpressionBuilder("AppVersion");
            _AuthToken = new ExpressionBuilder("AuthToken");
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
        private static ExpressionBuilder _Avatar;
        /// <summary>
        /// Avatar,头像
        /// </summary>
        public static ExpressionBuilder Avatar { get{return _Avatar;}}
        private static ExpressionBuilder _Email;
        /// <summary>
        /// Email,邮箱
        /// </summary>
        public static ExpressionBuilder Email { get{return _Email;}}
        private static ExpressionBuilder _Gender;
        /// <summary>
        /// Gender,性别(0:女;1:男)
        /// </summary>
        public static ExpressionBuilder Gender { get{return _Gender;}}
        private static ExpressionBuilder _UserType;
        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        public static ExpressionBuilder UserType { get{return _UserType;}}
        private static ExpressionBuilder _Os;
        /// <summary>
        /// Os,设备操作系统
        /// </summary>
        public static ExpressionBuilder Os { get{return _Os;}}
        private static ExpressionBuilder _OsVersion;
        /// <summary>
        /// OsVersion,设备系统版本
        /// </summary>
        public static ExpressionBuilder OsVersion { get{return _OsVersion;}}
        private static ExpressionBuilder _DeviceID;
        /// <summary>
        /// DeviceID,设备ID
        /// </summary>
        public static ExpressionBuilder DeviceID { get{return _DeviceID;}}
        private static ExpressionBuilder _DeviceToken;
        /// <summary>
        /// DeviceToken,友盟推送的设备唯一标识
        /// </summary>
        public static ExpressionBuilder DeviceToken { get{return _DeviceToken;}}
        private static ExpressionBuilder _LoginIp;
        /// <summary>
        /// LoginIp,登录用户IP
        /// </summary>
        public static ExpressionBuilder LoginIp { get{return _LoginIp;}}
        private static ExpressionBuilder _AppVersion;
        /// <summary>
        /// AppVersion,使用app版本
        /// </summary>
        public static ExpressionBuilder AppVersion { get{return _AppVersion;}}
        private static ExpressionBuilder _AuthToken;
        /// <summary>
        /// AuthToken,授权Token
        /// </summary>
        public static ExpressionBuilder AuthToken { get{return _AuthToken;}}
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
            this._Avatar = new ExpressionBuilder( prefix + "Avatar");
            this._Email = new ExpressionBuilder( prefix + "Email");
            this._Gender = new ExpressionBuilder( prefix + "Gender");
            this._UserType = new ExpressionBuilder( prefix + "UserType");
            this._Os = new ExpressionBuilder( prefix + "Os");
            this._OsVersion = new ExpressionBuilder( prefix + "OsVersion");
            this._DeviceID = new ExpressionBuilder( prefix + "DeviceID");
            this._DeviceToken = new ExpressionBuilder( prefix + "DeviceToken");
            this._LoginIp = new ExpressionBuilder( prefix + "LoginIp");
            this._AppVersion = new ExpressionBuilder( prefix + "AppVersion");
            this._AuthToken = new ExpressionBuilder( prefix + "AuthToken");
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
        private ExpressionBuilder _Avatar;
        /// <summary>
        /// Avatar,头像
        /// </summary>
        public ExpressionBuilder Avatar { get{return _Avatar;}}
        private ExpressionBuilder _Email;
        /// <summary>
        /// Email,邮箱
        /// </summary>
        public ExpressionBuilder Email { get{return _Email;}}
        private ExpressionBuilder _Gender;
        /// <summary>
        /// Gender,性别(0:女;1:男)
        /// </summary>
        public ExpressionBuilder Gender { get{return _Gender;}}
        private ExpressionBuilder _UserType;
        /// <summary>
        /// UserType,用户类型(0:普通;1:同工;9999:管理员)
        /// </summary>
        public ExpressionBuilder UserType { get{return _UserType;}}
        private ExpressionBuilder _Os;
        /// <summary>
        /// Os,设备操作系统
        /// </summary>
        public ExpressionBuilder Os { get{return _Os;}}
        private ExpressionBuilder _OsVersion;
        /// <summary>
        /// OsVersion,设备系统版本
        /// </summary>
        public ExpressionBuilder OsVersion { get{return _OsVersion;}}
        private ExpressionBuilder _DeviceID;
        /// <summary>
        /// DeviceID,设备ID
        /// </summary>
        public ExpressionBuilder DeviceID { get{return _DeviceID;}}
        private ExpressionBuilder _DeviceToken;
        /// <summary>
        /// DeviceToken,友盟推送的设备唯一标识
        /// </summary>
        public ExpressionBuilder DeviceToken { get{return _DeviceToken;}}
        private ExpressionBuilder _LoginIp;
        /// <summary>
        /// LoginIp,登录用户IP
        /// </summary>
        public ExpressionBuilder LoginIp { get{return _LoginIp;}}
        private ExpressionBuilder _AppVersion;
        /// <summary>
        /// AppVersion,使用app版本
        /// </summary>
        public ExpressionBuilder AppVersion { get{return _AppVersion;}}
        private ExpressionBuilder _AuthToken;
        /// <summary>
        /// AuthToken,授权Token
        /// </summary>
        public ExpressionBuilder AuthToken { get{return _AuthToken;}}
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

    #region Verify
    /// <summary>
    /// Verify,验证信息
    /// </summary>
    [DataContract]
    [Table(TableName="Verify",Label="Verify")] 
    public partial class Verify : Entity
    {
        
        public Verify()
        {
        }
        public Verify(string mobile)
        {
           this.Mobile = mobile;
        }

#region propertys
 
        private string _Mobile;

        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        [DataMember]
        [Column( IsPrimaryKey = true, Length = 20)]  
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

 
        private string _vCode;

        /// <summary>
        /// vCode,验证码
        /// </summary>
        [DataMember]
        [Column( AllowNull = true , Length = 20)]  
        [MySqlDbType(DbType.String)] 
        public string vCode
        {
            get
            {
                return this._vCode ?? string.Empty;
            }
            set
            {
                this._vCode = value;
            }
        }

        /// <summary>
        /// InsertTime,记录生成时间
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int64)] 
        public long InsertTime {  get; set; }

        /// <summary>
        /// Expire,有效时长
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int Expire {  get; set; }

        /// <summary>
        /// Type,验证类型(0:手机验证;1:邮箱验证)
        /// </summary>
        [DataMember]
        [Column( AllowNull = true )]  
        [MySqlDbType(DbType.Int32)] 
        public int Type {  get; set; }


        #endregion

        #region link objects
        
        #endregion
        public static bool Exists(string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Exists();
        }
        public static bool Exists(IDbSession session,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Exists(session);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Exists(session,tableMapping);
        }
        public static bool Exists(NameMapping tableMapping,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Exists(tableMapping);
        }
        public static bool Exists( IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.ExistsInner(conditionExpression);
        }
        public static bool Exists(IDbSession session, IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.ExistsInner(session,conditionExpression);
        }
        public static bool Exists(NameMapping tableMapping, IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.ExistsInner(tableMapping,conditionExpression);
        }
        public static bool Exists(IDbSession session,NameMapping tableMapping, IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.ExistsInner(session,tableMapping,conditionExpression);
        }
        public static int Delete(string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Delete();
        }
        public static int Delete(IDbSession session,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Delete(session);
        }
        public static int Delete(IDbSession session,NameMapping tableMapping,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Delete(session,tableMapping);
        }
        public static int Delete(NameMapping tableMapping,string mobile){
            Verify obj=new Verify();
            obj.Mobile = mobile;
            return obj.Delete(tableMapping);
        }
        public static int BatchDelete(IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.BatchDeleteInner(conditionExpression);
        }
        public static int BatchDelete(IDbSession session,IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.BatchDeleteInner(session,conditionExpression);
        }
        public static int BatchDelete(IDbSession session,NameMapping tableMapping,IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.BatchDeleteInner(session,tableMapping,conditionExpression);
        }
        public static int BatchDelete(NameMapping tableMapping,IConditionExpression conditionExpression){
            Verify obj=new Verify();
            return obj.BatchDeleteInner(tableMapping,conditionExpression);
        }
    }
    #endregion

    #region Verifys

    /// <summary>
    /// Verify集合,验证信息
    /// </summary>
    [DataContract]
    public partial class Verifys:EntityCollection<Verify>
    {
    }
    #endregion
 

    #region VerifyProperties
    public static class VerifyProperties
    {
    
        static VerifyProperties()
        {
            _ALL = new ExpressionBuilder("*");
            _Mobile = new ExpressionBuilder("Mobile");
            _vCode = new ExpressionBuilder("vCode");
            _InsertTime = new ExpressionBuilder("InsertTime");
            _Expire = new ExpressionBuilder("Expire");
            _Type = new ExpressionBuilder("Type");


        }

        private static ExpressionBuilder _ALL;
        public static ExpressionBuilder ALL { get{return _ALL;}}
        private static ExpressionBuilder _Mobile;
        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        public static ExpressionBuilder Mobile { get{return _Mobile;}}
        private static ExpressionBuilder _vCode;
        /// <summary>
        /// vCode,验证码
        /// </summary>
        public static ExpressionBuilder vCode { get{return _vCode;}}
        private static ExpressionBuilder _InsertTime;
        /// <summary>
        /// InsertTime,记录生成时间
        /// </summary>
        public static ExpressionBuilder InsertTime { get{return _InsertTime;}}
        private static ExpressionBuilder _Expire;
        /// <summary>
        /// Expire,有效时长
        /// </summary>
        public static ExpressionBuilder Expire { get{return _Expire;}}
        private static ExpressionBuilder _Type;
        /// <summary>
        /// Type,验证类型(0:手机验证;1:邮箱验证)
        /// </summary>
        public static ExpressionBuilder Type { get{return _Type;}}



    }
     #endregion
    #region VerifyDescriptor
    public class VerifyDescriptor
    {
     
        public VerifyDescriptor(string prefix)
        {
            this._Prefix = prefix; 
            this._ALL = new ExpressionBuilder(prefix + "*");
    
            this._Mobile = new ExpressionBuilder( prefix + "Mobile");
            this._vCode = new ExpressionBuilder( prefix + "vCode");
            this._InsertTime = new ExpressionBuilder( prefix + "InsertTime");
            this._Expire = new ExpressionBuilder( prefix + "Expire");
            this._Type = new ExpressionBuilder( prefix + "Type");
        }
        
        private string _Prefix;

        private ExpressionBuilder _ALL;
        public ExpressionBuilder ALL { get{return _ALL;}}
        private ExpressionBuilder _Mobile;
        /// <summary>
        /// Mobile,手机号码
        /// </summary>
        public ExpressionBuilder Mobile { get{return _Mobile;}}
        private ExpressionBuilder _vCode;
        /// <summary>
        /// vCode,验证码
        /// </summary>
        public ExpressionBuilder vCode { get{return _vCode;}}
        private ExpressionBuilder _InsertTime;
        /// <summary>
        /// InsertTime,记录生成时间
        /// </summary>
        public ExpressionBuilder InsertTime { get{return _InsertTime;}}
        private ExpressionBuilder _Expire;
        /// <summary>
        /// Expire,有效时长
        /// </summary>
        public ExpressionBuilder Expire { get{return _Expire;}}
        private ExpressionBuilder _Type;
        /// <summary>
        /// Type,验证类型(0:手机验证;1:邮箱验证)
        /// </summary>
        public ExpressionBuilder Type { get{return _Type;}}



    }
     #endregion

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
