set IDENTITY_INSERT dbo.TA_Person on;
delete from TA_Person where Prs_Barcode='GhadirDataInitialize'
insert into TA_Person(prs_ID,Prs_Active,Prs_Barcode,Prs_FirstName,Prs_LastName)
values(1,1,'GhadirDataInitialize','GHADIR','GHADIR')
set IDENTITY_INSERT dbo.TA_Person off;
select * from TA_Person

delete from TA_SecurityRole where role_ID=1
set IDENTITY_INSERT dbo.TA_SecurityRole on;
insert into TA_SecurityRole(role_ID,role_Name,role_Customcode,role_Active)
values(1,'GHADIRADMIN','1-1',1)
set IDENTITY_INSERT dbo.TA_securityRole off;


insert into TA_SecurityAuthorize(Athorize_RoleID,Athorize_ResourceID,Athorize_Allow)
select  1,resource_ID,1
from TA_SecurityResource

set IDENTITY_INSERT dbo.TA_SecurityUser on;
insert into TA_SecurityUser (user_ID,user_PersonID,user_RoleID,user_Active,user_UserName,user_Password,user_IsADAuthenticateActive)
values(1,1,1,1,'GHADIRUSERNAME','14KqSTmb8PqnPJNotzyXfemIA5Y=',0)
set IDENTITY_INSERT dbo.TA_SecurityUser off;

insert into TA_DataAccessDepartment (DataAccessDep_UserID,DataAccessDep_All)
values(1,1)
