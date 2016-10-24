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
   RptId                varchar(20) not null comment '�����ʶ',
   UserId               varchar(20) not null comment '�û���ʶ',
   PlanId               varchar(20) not null comment '�ƻ���ʶ',
   BibleTask            varchar(20) not null comment '������ɶ�',
   BookTask             varchar(20) not null comment '������ɶ�',
   UpdateTime           datetime not null comment '����޸�ʱ��',
   primary key (RptId)
);

alter table RptSign comment 'ͳ�Ʊ�';

/*==============================================================*/
/* Table: UserInfo                                              */
/*==============================================================*/
create table UserInfo
(
   UserId               varchar(20) not null comment '�û���ʶ,����',
   UserName             varchar(30) not null comment '�û���',
   Mobile               varchar(30) not null comment '�ֻ�����',
   Email                varchar(30) not null comment '����',
   UserType             int not null comment '�û�����(0:��ͨ;1:ͬ��;9999:����Ա)',
   UpdateTime           datetime not null comment '����޸�ʱ��',
   primary key (UserId)
);

alter table UserInfo comment '�û���';

/*==============================================================*/
/* Table: UserPlan                                              */
/*==============================================================*/
create table UserPlan
(
   PlanId               varchar(20) not null comment '�ƻ���ʶ,����',
   UserId               varchar(20) not null comment '�û���ʶ',
   BiblePlan            text not null comment '�����ƻ�',
   BookPlan             text not null comment '����ƻ�',
   StartDate            datetime not null comment '��ʼʱ��',
   EndDate              datetime not null comment '����ʱ��',
   UpdateTime           datetime not null comment '����޸�ʱ��',
   primary key (PlanId)
);

alter table UserPlan comment '�ƻ���';

/*==============================================================*/
/* Table: UserSign                                              */
/*==============================================================*/
create table UserSign
(
   SignId               varchar(20) not null comment 'ǩ����ʶ,����',
   UserId               varchar(20) not null comment '�û���ʶ',
   PlanId               varchar(20) not null comment 'ǩ���ƻ�',
   SignType             int not null comment 'ǩ������(1:����;2:����)',
   SignDate             datetime not null comment 'ǩ��ʱ��',
   UpdateTime           datetime not null comment '����޸�ʱ��',
   primary key (SignId)
);

alter table UserSign comment '�û�ǩ����';

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

