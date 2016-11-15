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
   GatherId             varchar(30) not null comment 'ǩ����ʶ,����',
   UserName             varchar(30) not null comment '����',
   GroupName            varchar(30) not null comment 'С������',
   GatherType           int not null comment '�ۻ���ʽ(0:���վۻ�;1:ѧ��С��ۻ�;2:��ҵ����С��ۻ�;3:�����)',
   IpAddress            varchar(30) not null comment 'IP��ַ',
   SignTime             datetime not null comment 'ǩ��ʱ��',
   primary key (GatherId)
);

alter table Gather comment '�ۻ�ǩ����';

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
   Password             varchar(50) not null comment '����',
   Avatar               varchar(200) not null comment 'ͷ��',
   Email                varchar(30) not null comment '����',
   Gender               int not null comment '�Ա�(0:Ů;1:��)',
   UserType             int not null comment '�û�����(0:��ͨ;1:ͬ��;9999:����Ա)',
   Os                   varchar(50) not null comment '�豸����ϵͳ',
   OsVersion            varchar(50) not null comment '�豸ϵͳ�汾',
   DeviceID             varchar(200) not null comment '�豸ID',
   DeviceToken          varchar(100) not null comment '�������͵��豸Ψһ��ʶ',
   LoginIp              varchar(50) not null comment '��¼�û�IP',
   AppVersion           varchar(50) not null comment 'ʹ��app�汾',
   AuthToken            varchar(100) not null comment '��ȨToken',
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

/*==============================================================*/
/* Table: Verify                                                */
/*==============================================================*/
create table Verify
(
   Mobile               varchar(20) not null comment '�ֻ�����',
   vCode                varchar(20) not null comment '��֤��',
   InsertTime           bigint not null comment '��¼����ʱ��',
   Expire               int not null comment '��Чʱ��',
   Type                 int not null comment '��֤����(0:�ֻ���֤;1:������֤)',
   primary key (Mobile)
);

alter table Verify comment '��֤��Ϣ';

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

