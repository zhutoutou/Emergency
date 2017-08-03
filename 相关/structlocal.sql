-----------------------------------------------------
-- Export file for user SZ120NEW                   --
-- Created by Administrator on 2017/7/27, 14:56:25 --
-----------------------------------------------------

spool structlocal.log

prompt
prompt Creating view V_ALARM_EVENT
prompt ===========================
prompt
create or replace view sz120new.v_alarm_event as
select
(select TO_CHAR(sysdate, 'YYYYMMDD') from dual) as DATATIME,
(select count(*) from hjjlb where lsh> TO_CHAR(sysdate-30, 'YYYYMMDD')||'000000'||'00000' and lsh<TO_CHAR(sysdate+1, 'YYYYMMDD')||'000000'||'00000') as callcount,
(select count(*) from sljlb where lsh> TO_CHAR(sysdate-30, 'YYYYMMDD')||'000000'||'00000' and lsh<TO_CHAR(sysdate+1, 'YYYYMMDD')||'000000'||'00000') as dealcount,
(select count(*) from ccxxb where lsh> TO_CHAR(sysdate-30, 'YYYYMMDD')||'000000'||'00000' and lsh<TO_CHAR(sysdate+1, 'YYYYMMDD')||'000000'||'00000') as dispatchcount,
(select count(*) from hzxxb where lsh> TO_CHAR(sysdate-30, 'YYYYMMDD')||'000000'||'00000' and lsh<TO_CHAR(sysdate+1, 'YYYYMMDD')||'000000'||'00000') as paientcount
from dual
/

prompt
prompt Creating view V_ALARM_VEHICLEREALSTATUS
prompt =======================================
prompt
create or replace view sz120new.v_alarm_vehiclerealstatus as
select cl.mc as vehiclename,cl.id as CLID,cl.cph as vehiclecard,cl.ssdw as vehicledepartment,cl.clzt as status
,dq.jd as jd,dq.wd as wd from clxxb cl,dqclztxxb dq where cl.id=dq.id
/

prompt
prompt Creating procedure PROCALARM_EVENT
prompt ==================================
prompt
create or replace procedure sz120new.PROCALARM_EVENT
--急救事件信息
(ret out number,msg out varchar2)
IS
     is_update number;
     vc_datatime varchar2(8);
     n_callcount number;
     n_dealcount number;
     n_dispatchcount number;
     n_paientcount number;
     d_SJ date;

begin
     ret:=0;
     d_SJ:=sysdate;
     select datatime,callcount,dealcount,dispatchcount,paientcount into 
     vc_datatime,n_callcount,n_dealcount,n_dispatchcount,n_paientcount from V_ALARM_EVENT;

     select count(*) into is_update from  ALARM_event_info@djk where datatime=vc_datatime;

     if is_update>0
     then
        update ALARM_event_info@djk set callcount=n_callcount,dealcount=n_dealcount,
        dispatchcount=n_dispatchcount，paientcount=n_paientcount,lasttime=d_SJ,readflag=1
        where datatime=vc_datatime;
        ret:=1;
        Msg:=TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS')||' 成功，更新记录。';
     else
        insert into ALARM_event_info@djk (datatime,callcount,dealcount,dispatchcount,paientcount,lasttime,readflag) values
        (vc_datatime,n_callcount,n_dealcount,n_dispatchcount,n_paientcount,d_SJ,1);
        ret:=1;
        Msg:=TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS')||' 成功，插入记录。';
     end if;
     commit;

     exception
        when others then
        Msg := TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS') ||' 失败，错误代码'||sqlerrm(sqlcode);
        ret := 0;
        rollback;
end PROCALARM_EVENT;
/

prompt
prompt Creating procedure PROCALARM_VEHICLEHISTROYSTATE
prompt ================================================
prompt
create or replace procedure sz120new.PROCalarm_vehiclehistroystate
--车辆历史状态信息
(vc_clID varchar2,vc_lsh varchar2,vc_ccxh varchar2,ret out number,msg out varchar2)
IS
     vc_vehicletime CCXXB.CS%type;
     vc_vehiclename CLXXB.MC%type;
     vc_vehiclecard CLXXB.CPH%type;
     vc_vehicledepartment CLXXB.SSDW%type;
     vc_status CLXXB.CLZT%type;
     n_jd DQCLZTXXB.JD%type;
     n_wd DQCLZTXXB.WD%type;
     d_SJ date;

begin
     ret:=0;
     d_SJ:=sysdate;
     select vehiclename,vehiclecard,vehicledepartment,status,jd,wd
     into vc_vehiclename,vc_vehiclecard,vc_vehicledepartment,
          vc_status,n_jd,n_wd from v_alarm_vehiclerealstatus
     where CLID = vc_clID and rownum=1;

     if vc_vehiclename is not null
     then
        --如果没有cs去查询获取车次
        if vc_ccxh ='' and vc_lsh <> '' 
        then
           select max(cs) into vc_vehicletime from ccxxb where lsh = vc_lsh and clid = vc_vehiclename;
        else
           vc_vehicletime :='';
        end if;
        
        insert into vehiclehistroystate@djk (vehiclename,vehiclecard,vehicledepartment,jd,wd,lsh,ccxh,reporttime,readflag) values
        (vc_vehiclename,vc_vehiclecard,vc_vehicledepartment,n_jd,n_wd,vc_lsh,vc_vehicletime,d_SJ,1);
        ret:=1;
        Msg:= TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS')|| MSG ||' 成功，插入记录,名称：'||vc_vehiclename||'。';
     end if;
     commit;



     exception
        when others then
        msg := TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS') ||' 失败，错误代码'||vc_vehiclename||sqlerrm(sqlcode);
        ret := 0;
        rollback;
end PROCalarm_vehiclehistroystate;
/

prompt
prompt Creating procedure PROCALARM_VEHICLEREALSTATUS
prompt ==============================================
prompt
create or replace procedure sz120new.PROCalarm_vehiclerealstatus
--车辆实时状态信息
(ret out number,msg out varchar2)
IS
     vc_vehiclename CLXXB.MC%type;
     vc_vehiclecard CLXXB.CPH%type;
     vc_vehicledepartment CLXXB.SSDW%type;
     vc_status CLXXB.CLZT%type;
     n_jd DQCLZTXXB.JD%type;
     n_wd DQCLZTXXB.WD%type;
     d_SJ date;
     cursor c_alarm_vehiclerealstatus is select vehiclename,vehiclecard,vehicledepartment,status,jd,wd from v_alarm_vehiclerealstatus;
     is_update number;
begin
     ret:=0;
     d_SJ:=sysdate;

     open c_alarm_vehiclerealstatus;
     loop
          Msg:='';
          fetch c_alarm_vehiclerealstatus into vc_vehiclename,vc_vehiclecard,vc_vehicledepartment,
          vc_status,n_jd,n_wd;
          exit when c_alarm_vehiclerealstatus%notfound;
          select count(*) into is_update from vehiclerealstatus@djk where vehiclename=vc_vehiclename;
          if is_update=1 then
             update vehiclerealstatus@djk set vehiclecard=vc_vehiclecard,vehicledepartment=vc_vehicledepartment,
             status=vc_status,jd=n_jd,wd=n_wd,lasttime=d_SJ,readflag=1
             where vehiclename=vc_vehiclename;
             ret:=1;
             Msg:=TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS')||' 成功，更新记录,名称：'||vc_vehiclename||'。';
          else
             if is_update>1 then
                delete from vehiclerealstatus@djk where vehiclename=vc_vehiclename;
                Msg:='存在多余记录，已删除，';
             end if;
             insert into vehiclerealstatus@djk (vehiclename,vehiclecard,vehicledepartment,status,jd,wd,lasttime,readflag) values
             (vc_vehiclename,vc_vehiclecard,vc_vehicledepartment,vc_status,n_jd,n_wd,d_SJ,1);
             ret:=1;
            Msg:= TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS')|| MSG ||' 成功，插入记录,名称：'||vc_vehiclename||'。';
          end if;
     commit;
     end loop;
     close c_alarm_vehiclerealstatus;
     

     exception
        when others then
        msg := TO_CHAR(d_SJ, 'YYYY-MM-DD HH24:MI:SS') ||' 失败，错误代码'||vc_vehiclename||sqlerrm(sqlcode);
        ret := 0;
        rollback;
end PROCalarm_vehiclerealstatus;
/


spool off
