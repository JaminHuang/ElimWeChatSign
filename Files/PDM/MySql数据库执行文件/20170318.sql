/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2017/3/18 ������ 12:11:31                       */
/*==============================================================*/


drop table if exists Gather;

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

