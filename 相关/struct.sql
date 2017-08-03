-----------------------------------------------------
-- Export file for user SH120DJK                   --
-- Created by Administrator on 2017/7/27, 14:42:12 --
-----------------------------------------------------

spool struct.log

prompt
prompt Creating table VEHICLEREALSTATUS
prompt ================================
prompt
create table SH120DJK.VEHICLEREALSTATUS
(
  VEHICLENAME       VARCHAR2(40),
  VEHICLECARD       VARCHAR2(40),
  VEHICLEDEPARTMENT VARCHAR2(40),
  STATUS            VARCHAR2(40),
  JD                NUMBER(9,5),
  WD                NUMBER(9,5),
  LASTTIME          DATE,
  READFLAG          NUMBER
)
tablespace USERS_NEW
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on table SH120DJK.VEHICLEREALSTATUS
  is '车辆实时位置和状态';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLENAME
  is '车辆名称';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLECARD
  is '车牌号';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLEDEPARTMENT
  is '所属单位名称';
comment on column SH120DJK.VEHICLEREALSTATUS.STATUS
  is '车辆状态';
comment on column SH120DJK.VEHICLEREALSTATUS.JD
  is '经度';
comment on column SH120DJK.VEHICLEREALSTATUS.WD
  is '纬度';
comment on column SH120DJK.VEHICLEREALSTATUS.LASTTIME
  is '最新上报时间';
comment on column SH120DJK.VEHICLEREALSTATUS.READFLAG
  is '读取标志';


spool off
