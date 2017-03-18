/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2017/3/18 星期六 12:11:31                       */
/*==============================================================*/


drop table if exists Gather;

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

