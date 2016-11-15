/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2016/11/15 15:00:50                          */
/*==============================================================*/


drop table if exists Gather;

drop table if exists Verify;

drop table if exists RptSign;

drop table if exists UserSign;

drop table if exists UserPlan;

drop table if exists UserInfo;

/*==============================================================*/
/* Table: Gather                                                */
/*==============================================================*/
create table Gather
(
   GatherId             varchar(30) not null comment '签到标识,主键',
   UserName             varchar(30) not null comment '姓名',
   GroupName            varchar(30) not null comment '小组名称',
   GatherType           int not null comment '聚会形式(0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会)',
   IpAddress            varchar(30) not null comment 'IP地址',
   SignTime             datetime not null comment '签到时间',
   primary key (GatherId)
);

alter table Gather comment '聚会签到表';

/*==============================================================*/
/* Table: RptSign                                               */
/*==============================================================*/
create table RptSign
(
   RptId                varchar(20) not null comment '报表标识',
   UserId               varchar(20) not null comment '用户标识',
   PlanId               varchar(20) not null comment '计划标识',
   BibleTask            varchar(20) not null comment '读经完成度',
   BookTask             varchar(20) not null comment '读书完成度',
   UpdateTime           datetime not null comment '最后修改时间',
   primary key (RptId)
);

alter table RptSign comment '统计表';

/*==============================================================*/
/* Table: UserInfo                                              */
/*==============================================================*/
create table UserInfo
(
   UserId               varchar(20) not null comment '用户标识,主键',
   UserName             varchar(30) not null comment '用户名',
   Mobile               varchar(30) not null comment '手机号码',
   Password             varchar(50) not null comment '密码',
   Avatar               varchar(200) not null comment '头像',
   Email                varchar(30) not null comment '邮箱',
   Gender               int not null comment '性别(0:女;1:男)',
   UserType             int not null comment '用户类型(0:普通;1:同工;9999:管理员)',
   Os                   varchar(50) not null comment '设备操作系统',
   OsVersion            varchar(50) not null comment '设备系统版本',
   DeviceID             varchar(200) not null comment '设备ID',
   DeviceToken          varchar(100) not null comment '友盟推送的设备唯一标识',
   LoginIp              varchar(50) not null comment '登录用户IP',
   AppVersion           varchar(50) not null comment '使用app版本',
   AuthToken            varchar(100) not null comment '授权Token',
   UpdateTime           datetime not null comment '最后修改时间',
   primary key (UserId)
);

alter table UserInfo comment '用户表';

/*==============================================================*/
/* Table: UserPlan                                              */
/*==============================================================*/
create table UserPlan
(
   PlanId               varchar(20) not null comment '计划标识,主键',
   UserId               varchar(20) not null comment '用户标识',
   BiblePlan            text not null comment '读经计划',
   BookPlan             text not null comment '读书计划',
   StartDate            datetime not null comment '开始时间',
   EndDate              datetime not null comment '结束时间',
   UpdateTime           datetime not null comment '最后修改时间',
   primary key (PlanId)
);

alter table UserPlan comment '计划表';

/*==============================================================*/
/* Table: UserSign                                              */
/*==============================================================*/
create table UserSign
(
   SignId               varchar(20) not null comment '签到标识,主键',
   UserId               varchar(20) not null comment '用户标识',
   PlanId               varchar(20) not null comment '签到计划',
   SignType             int not null comment '签到类型(1:读经;2:读书)',
   SignDate             datetime not null comment '签到时间',
   UpdateTime           datetime not null comment '最后修改时间',
   primary key (SignId)
);

alter table UserSign comment '用户签到表';

/*==============================================================*/
/* Table: Verify                                                */
/*==============================================================*/
create table Verify
(
   Mobile               varchar(20) not null comment '手机号码',
   vCode                varchar(20) not null comment '验证码',
   InsertTime           bigint not null comment '记录生成时间',
   Expire               int not null comment '有效时长',
   Type                 int not null comment '验证类型(0:手机验证;1:邮箱验证)',
   primary key (Mobile)
);

alter table Verify comment '验证信息';

alter table RptSign add constraint `FK_RptSign.UserInfo` foreign key (UserId)
      references UserInfo (UserId) on delete restrict on update restrict;

alter table RptSign add constraint `FK_RptSign.UserPlan` foreign key (PlanId)
      references UserPlan (PlanId) on delete restrict on update restrict;

alter table UserPlan add constraint `FK_UserPlan.UserInfo` foreign key (UserId)
      references UserInfo (UserId) on delete restrict on update restrict;

alter table UserSign add constraint `FK_UserSign.UserInfo` foreign key (UserId)
      references UserInfo (UserId) on delete restrict on update restrict;

alter table UserSign add constraint `FK_UserSign.UserPlan` foreign key (PlanId)
      references UserPlan (PlanId) on delete restrict on update restrict;

