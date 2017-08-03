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
  is '����ʵʱλ�ú�״̬';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLENAME
  is '��������';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLECARD
  is '���ƺ�';
comment on column SH120DJK.VEHICLEREALSTATUS.VEHICLEDEPARTMENT
  is '������λ����';
comment on column SH120DJK.VEHICLEREALSTATUS.STATUS
  is '����״̬';
comment on column SH120DJK.VEHICLEREALSTATUS.JD
  is '����';
comment on column SH120DJK.VEHICLEREALSTATUS.WD
  is 'γ��';
comment on column SH120DJK.VEHICLEREALSTATUS.LASTTIME
  is '�����ϱ�ʱ��';
comment on column SH120DJK.VEHICLEREALSTATUS.READFLAG
  is '��ȡ��־';


spool off
