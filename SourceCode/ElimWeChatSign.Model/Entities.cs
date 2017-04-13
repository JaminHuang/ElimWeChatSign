using System;
using System.Collections.Generic;
using System.ComponentModel;
using Titan;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ElimWeChatSign.Model
{
    [DataContract]
    public class EntityList<T>
    {
        private List<T> _items = new List<T>();
        [DataMember]
        public long TotalCount { get; set; }
        [DataMember]
        public List<T> Items
        {
            get { return _items; }
            set { _items = value; }
        }

    }
    /*
    <system.runtime.serialization>
        <dataContractSerializer>
          <declaredTypes>

          </declaredTypes>
        </dataContractSerializer>
      </system.runtime.serialization>

    */
    #region enums

    #endregion




    #region Gather
    /// <summary>
    /// Gather,聚会签到表
    /// </summary>
    [DataContract]
    [Table]
    public partial class Gather
    {

        public Gather()
        {

            SignTime = DateTime.Now;

        }
        #region propertys

        /// <summary>
        /// 签到标识,主键,
        /// </summary>
        [DataMember]
        [DisplayName("签到标识,主键")]
        [Column(IsPrimaryKey = true, Size = 30)]
        [Required(ErrorMessage = "签到标识,主键不允许空")]
        [MaxLength(30, ErrorMessage = "签到标识,主键不能超过30个字")]

        public string GatherId { get; set; }


        /// <summary>
        /// 姓名,
        /// </summary>
        [DataMember]
        [DisplayName("姓名")]
        [Column(Size = 30)]
        [Required(ErrorMessage = "姓名不允许空")]
        [MaxLength(30, ErrorMessage = "姓名不能超过30个字")]

        public string UserName { get; set; }


        /// <summary>
        /// 小组名称,
        /// </summary>
        [DataMember]
        [DisplayName("小组名称")]
        [Column(Size = 30)]
        [Required(ErrorMessage = "小组名称不允许空")]
        [MaxLength(30, ErrorMessage = "小组名称不能超过30个字")]

        public string GroupName { get; set; }


        /// <summary>
        /// 聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会),
        /// </summary>
        [DataMember]
        [DisplayName("聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)")]
        [Column()]
        [Required(ErrorMessage = "聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)不允许空")]

        public int GatherType { get; set; }


        /// <summary>
        /// IP地址,
        /// </summary>
        [DataMember]
        [DisplayName("IP地址")]
        [Column(Size = 30)]
        [Required(ErrorMessage = "IP地址不允许空")]
        [MaxLength(30, ErrorMessage = "IP地址不能超过30个字")]

        public string IpAddress { get; set; }


        /// <summary>
        /// 签到时间,
        /// </summary>
        [DataMember]
        [DisplayName("签到时间")]
        [Column()]
        [Required(ErrorMessage = "签到时间不允许空")]

        public DateTime SignTime { get; set; }


        #endregion

    }
    #endregion
    #region GatherProperties
    public static partial class Gather_
    {

        private static GatherDescriptor instance = new GatherDescriptor("");

        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get { return instance.ALL; } }


        /// <summary>
        /// 签到标识,主键,
        /// </summary>
        public static PropertyExpression GatherId { get { return instance.GatherId; } }
        /// <summary>
        /// 姓名,
        /// </summary>
        public static PropertyExpression UserName { get { return instance.UserName; } }
        /// <summary>
        /// 小组名称,
        /// </summary>
        public static PropertyExpression GroupName { get { return instance.GroupName; } }
        /// <summary>
        /// 聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会),
        /// </summary>
        public static PropertyExpression GatherType { get { return instance.GatherType; } }
        /// <summary>
        /// IP地址,
        /// </summary>
        public static PropertyExpression IpAddress { get { return instance.IpAddress; } }
        /// <summary>
        /// 签到时间,
        /// </summary>
        public static PropertyExpression SignTime { get { return instance.SignTime; } }




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
    #endregion
    #region GatherDescriptor
    public partial class GatherDescriptor : ObjectDescriptorBase
    {

        public GatherDescriptor(string prefix) : base(prefix)
        {

            this._GatherId = new PropertyExpression(prefix + "GatherId");
            this._UserName = new PropertyExpression(prefix + "UserName");
            this._GroupName = new PropertyExpression(prefix + "GroupName");
            this._GatherType = new PropertyExpression(prefix + "GatherType");
            this._IpAddress = new PropertyExpression(prefix + "IpAddress");
            this._SignTime = new PropertyExpression(prefix + "SignTime");
            ALL = new PropertyExpression[] { this._GatherId, this._UserName, this._GroupName, this._GatherType, this._IpAddress, this._SignTime };
        }


        private PropertyExpression _GatherId;
        /// <summary>
        /// 签到标识,主键,
        /// </summary>
        public PropertyExpression GatherId { get { return _GatherId; } }
        private PropertyExpression _UserName;
        /// <summary>
        /// 姓名,
        /// </summary>
        public PropertyExpression UserName { get { return _UserName; } }
        private PropertyExpression _GroupName;
        /// <summary>
        /// 小组名称,
        /// </summary>
        public PropertyExpression GroupName { get { return _GroupName; } }
        private PropertyExpression _GatherType;
        /// <summary>
        /// 聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会),
        /// </summary>
        public PropertyExpression GatherType { get { return _GatherType; } }
        private PropertyExpression _IpAddress;
        /// <summary>
        /// IP地址,
        /// </summary>
        public PropertyExpression IpAddress { get { return _IpAddress; } }
        private PropertyExpression _SignTime;
        /// <summary>
        /// 签到时间,
        /// </summary>
        public PropertyExpression SignTime { get { return _SignTime; } }



    }
    #endregion


    #region Gathers
    /// <summary>
    /// Gather,聚会签到表
    /// </summary>
    [DataContract]
    [Table]
    public partial class Gathers : EntityList<Gather>
    {

    }
    #endregion
}
