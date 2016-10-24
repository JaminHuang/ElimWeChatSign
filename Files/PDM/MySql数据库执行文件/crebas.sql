/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2016/10/24 11:16:33                          */
/*==============================================================*/


drop table if exists RptSign;

drop table if exists UserInfo;

drop table if exists UserPlan;

drop table if exists UserSign;

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
   Email                varchar(30) not null comment '邮箱',
   UserType             int not null comment '用户类型(0:普通;1:同工;9999:管理员)',
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

