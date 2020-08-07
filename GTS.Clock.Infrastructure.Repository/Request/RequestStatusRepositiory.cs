using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.RequestFlow;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class RequestStatusRepositiory : RepositoryBase<RequestStatus>
    {
        public RequestStatusRepositiory(bool disconectly)
            : base(disconectly)
        { }

        public IList<RegisteredRequestsFlowLevel> GetRequestLevelsByPendingFlow(decimal requestId, decimal personnelId)
        {
            string SQLCommand = "";
            IList<RegisteredRequestsFlowLevel> RegisteredRequestsFlowLevelsList = null;
            SQLCommand = @"  
                            declare @RequestID numeric(18,0), @PersonID numeric(18,0)
                            set @RequestID = :requestId
                            set @PersonID = :personnelId      
                select MngFlow.mngrFlow_ID as ManagerFlowID, MngFlow.mngrFlow_ManagerID as ManagerID, 
                ManagerName = 
                CASE isnull(outManager.organPrsLastName,'') 
	                WHEN '' THEN  isnull(outManager.prsFirstName,'') + ' ' + isnull(outManager.prsLastName,'') 
	                ELSE isnull(outManager.organPrsFirstName,'') + ' ' + isnull(outManager.organPrsLastName,'')
                END
                from 
	                                                            (select  managerFlow.mngrFlow_ID, managerFlow.mngrFlow_FlowID, managerFlow.mngrFlow_ManagerID
	                                                            from TA_Request request 
		                                                        inner join TA_PrecardAccessGroupDetail precardAccessGroupDetail
		                                                        on request.request_PrecardID = precardAccessGroupDetail.accessGrpDtl_PrecardId
		                                                        inner join TA_PrecardAccessGroup precardAccessGroup
		                                                        on precardAccessGroupDetail.accessGrpDtl_AccessGrpId = precardAccessGroup.accessGrp_ID
		                                                        inner join TA_Flow flow
		                                                        on precardAccessGroup.accessGrp_ID = flow.Flow_AccessGroupID
		                                                        inner join TA_ManagerFlow managerFlow 
		                                                        on flow.Flow_ID = managerFlow.mngrFlow_FlowID 
		                                                        where request.request_ID = @RequestID and managerFlow.mngrFlow_Level = 1 and managerFlow.mngrFlow_Active = 1 and flow.Flow_ActiveFlow = 1 and flow.Flow_MainFlow = 1 and flow.flow_Deleted = 0) MngFlow
		                                                        inner join
		                                                        (select underManagment.underMng_FlowID from TA_UnderManagment underManagment 
		                                                        where underManagment.underMng_PersonID = @PersonID and underManagment.underMng_Contains = 1 and underManagment.underMng_PersonID is not null
		                                                        union
		                                                        select underMng_FlowID from
							                                                            (select underManagment.underMng_FlowID, underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
								                                                        inner join TA_Person person
								                                                        on  person.prs_IsDeleted=0 AND person.Prs_DepartmentId =  underManagment.underMng_DepartmentID  
								                                                        inner join TA_Request request
								                                                        on person.Prs_ID = request.request_PersonID
								                                                        where request.request_ID = @RequestID and underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 1 
								                                                        and person.Prs_ID not in
													                                                            (select underMng.underMng_PersonID from TA_UnderManagment underMng        
													                                                            where underMng.underMng_Contains = 0 and underMng.underMng_PersonID is not null and underMng.underMng_DepartmentID != underMng_DepartmentID))UnderMng
		                                                        union
                                                                select UnderMngDep.underMng_FlowID from
										                               (select underManagment.underMng_FlowID, underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
											                            inner join TA_Department department
											                            on department.dep_ID =  underManagment.underMng_DepartmentID 
											                            where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 1 and underManagment.underMng_ContainInnerChilds = 1) UnderMngDep
											                            inner join TA_Person person 
											                            on person.prs_IsDeleted=0 AND person.Prs_DepartmentId in
																	                               (select department.dep_ID from TA_Department department																		 
																		                            where department.dep_ParentPath LIKE '%,' + convert(nvarchar(max), UnderMngDep.underMng_DepartmentID) + ',%')
																	                               or
																		                            person.Prs_DepartmentId = UnderMngDep.underMng_DepartmentID
										                               inner join TA_Request request
										                               on request.request_PersonID = person.Prs_ID
										                               where person.Prs_ID not in
														                                       (select underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
																                                inner join TA_Person person
																                                on person.prs_IsDeleted=0 AND person.Prs_DepartmentId in
																					                             (select department.dep_ID FROM TA_Department department																				                                                         
																					                              where department.dep_ParentPath LIKE '%,' + convert(nvarchar(max), underManagment.underMng_DepartmentID) + ',%'
																					                              or
																					                              person.Prs_DepartmentId = underManagment.underMng_DepartmentID)
																	                            where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 0 and underManagment.underMng_ContainInnerChilds = 1)																						  																				                   
										                                                   and person.Prs_ID not in											    
																                                                 (select person.Prs_ID from TA_UnderManagment underManagment 
																                                                  inner join TA_Person person
																                                                  on person.prs_IsDeleted=0 AND person.Prs_DepartmentId = underManagment.underMng_DepartmentID 										  										                                               										                         																   
																                                                  where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 0 and underManagment.underMng_ContainInnerChilds = 0 and Prs_PrsDtlID != @PersonID)																					    
										                                                   and request.request_ID = @RequestID) UndMng	                               
		                                                on MngFlow.mngrFlow_FlowID = UndMng.underMng_FlowID
                                                        /*inner join TA_Manager manager
                                                        on MngFlow.mngrFlow_ManagerID = manager.MasterMng_ID
                                                        inner join TA_Person person
                                                        on person.prs_IsDeleted=0 AND manager.MasterMng_PersonID = person.Prs_ID*/
                                                        join (select mng.MasterMng_ID,prs1.Prs_ID prsID,prs1.Prs_FirstName prsFirstName,prs1.Prs_LastName prsLastName,prs2.Prs_ID organPrsID,prs2.Prs_FirstName organPrsFirstName,prs2.Prs_LastName organPrsLastName
														from TA_Manager mng
														left outer join TA_Person prs1 on prs1.prs_IsDeleted=0 AND  mng.MasterMng_PersonID = prs1.Prs_ID
														left outer join TA_OrganizationUnit organ on organ_ID=mng.MasterMng_OrganizationUnitID
														left outer join TA_Person prs2 on prs2.prs_IsDeleted=0 AND  organ.organ_PersonID = prs2.Prs_ID
													) outManager
														on MngFlow.mngrFlow_ManagerID = outManager.MasterMng_ID
                                                        where UndMng.underMng_FlowID not in 
							                                                                (select distinct underManagment.underMng_FlowID from TA_UnderManagment underManagment	
							                                                                where underManagment.underMng_Contains = 0 and underManagment.underMng_PersonID is not null and underManagment.underMng_PersonID = @PersonID)";
            RegisteredRequestsFlowLevelsList = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                   .SetResultTransformer(Transformers.AliasToBean(typeof(RegisteredRequestsFlowLevel)))
                                                   .SetParameter("personnelId", personnelId)
                                                   .SetParameter("requestId", requestId)
                                                   .List<RegisteredRequestsFlowLevel>();
            return RegisteredRequestsFlowLevelsList;
        }

        /// <summary>
        /// با دریافت یک شناسه پرسنل و کد بخش , مدیران جریان را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<RegisteredRequestsFlowLevel> Excude_GetRequestLevels(decimal departmentId, decimal personId)
        {
            string SQLCommand = "";
            IList<RegisteredRequestsFlowLevel> RegisteredRequestsFlowLevelsList = null;

            SQLCommand = @"
                            
declare @depId numeric(18,0), @PersonID numeric(18,0)
set @depId=:departmentId
set @PersonID = :personId

select allmngFlow.mngrFlow_ID ManagerFlowID,allmngFlow.mngrFlow_ManagerID ManagerID,allmngFlow.mngrFlow_FlowID FlowID,allmngFlow.mngrFlow_Level ManagerLevel
,ManagerName = 
CASE isnull(outManager.organPrsLastName,'') 
	WHEN '' THEN  isnull(outManager.prsFirstName,'') + ' ' + isnull(outManager.prsLastName,'') 
	ELSE isnull(outManager.organPrsFirstName,'') + ' ' + isnull(outManager.organPrsLastName,'')
END
FROM
(select MngFlow.mngrFlow_ID as ManagerFlowID, MngFlow.mngrFlow_ManagerID as ManagerID,mngrFlow_FlowID as FlowId,  isnull(manager.organPrsFirstName,'')+isnull(manager.prsFirstName,'') + ' ' + isnull(manager.organPrsLastName,'')+isnull(manager.prsLastName,'') as ManagerName from 
	                                (select  managerFlow.mngrFlow_ID, managerFlow.mngrFlow_FlowID, managerFlow.mngrFlow_ManagerID	                        
		                            from TA_Flow flow		                            
		                            inner join TA_ManagerFlow managerFlow 
		                            on flow.Flow_ID = managerFlow.mngrFlow_FlowID and flow_MainFlow=1
		                            where managerFlow.mngrFlow_Level = 1 and managerFlow.mngrFlow_Active = 1 and flow.Flow_ActiveFlow = 1 and flow.flow_Deleted = 0) MngFlow
		                            inner join
		                            (select underManagment.underMng_FlowID from TA_UnderManagment underManagment 
		                            where underManagment.underMng_PersonID = @PersonID and underManagment.underMng_Contains = 1 and underManagment.underMng_PersonID is not null
		                            union
		                            select underMng_FlowID from
							                                (select underManagment.underMng_FlowID, underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
								                            inner join TA_Person person
								                            on  person.prs_IsDeleted=0 AND person.Prs_DepartmentId =  underManagment.underMng_DepartmentID 
								                            inner join TA_Request request
								                            on person.Prs_ID = request.request_PersonID
								                            where underManagment.underMng_DepartmentID = @depId and underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 1 
								                            and person.Prs_ID not in
													                                (select underManagment.underMng_PersonID from TA_UnderManagment underManagment        
													                                where underManagment.underMng_Contains = 0 and underManagment.underMng_PersonID is not null))UnderMng
		                            union
                                    select UnderMngDep.underMng_FlowID from
										   (select underManagment.underMng_FlowID, underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
											inner join TA_Department department
											on department.dep_ID =  underManagment.underMng_DepartmentID 
											where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 1 and underManagment.underMng_ContainInnerChilds = 1) UnderMngDep
											inner join TA_Person person 
											on  person.prs_IsDeleted=0 AND person.Prs_DepartmentId in
																	   (select department.dep_ID from TA_Department department																		 
																		where department.dep_ParentPath LIKE '%,' + convert(nvarchar(max), UnderMngDep.underMng_DepartmentID) + ',%')
																	   or
																		person.Prs_DepartmentId = UnderMngDep.underMng_DepartmentID
										   inner join TA_Request request
										   on request.request_PersonID = person.Prs_ID
										   where person.Prs_ID not in
														           (select underManagment.underMng_DepartmentID from TA_UnderManagment underManagment
																    inner join TA_Person person
																    on  person.prs_IsDeleted=0 AND person.Prs_DepartmentId in
																					 (select department.dep_ID FROM TA_Department department																				                                                         
																					  where department.dep_ParentPath LIKE '%,' + convert(nvarchar(max), underManagment.underMng_DepartmentID) + ',%'
																					  or
																					  person.Prs_DepartmentId = underManagment.underMng_DepartmentID)
																	where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 0 and underManagment.underMng_ContainInnerChilds = 1)																						  																				                   
										                       and person.Prs_ID not in											    
																                     (select person.Prs_ID from TA_UnderManagment underManagment 
																                      inner join TA_Person person
																                      on  person.prs_IsDeleted=0 AND person.Prs_DepartmentId = underManagment.underMng_DepartmentID 										  										                                               										                         																   
																                      where underManagment.underMng_PersonID is null and underManagment.underMng_Contains = 0 and underManagment.underMng_ContainInnerChilds = 0 and Prs_PrsDtlID != @PersonID)																					    
										                       and person.Prs_DepartmentId = @depId) UndMng	                               
		                    on MngFlow.mngrFlow_FlowID = UndMng.underMng_FlowID                        
                            
                             join (select mng.MasterMng_ID,prs1.Prs_ID prsID,prs1.Prs_FirstName prsFirstName,prs1.Prs_LastName prsLastName,prs2.Prs_ID organPrsID,prs2.Prs_FirstName organPrsFirstName,prs2.Prs_LastName organPrsLastName
								from TA_Manager mng
								left outer join TA_Person prs1 on prs1.prs_IsDeleted=0 AND  mng.MasterMng_PersonID = prs1.Prs_ID
								left outer join TA_OrganizationUnit organ on organ_ID=mng.MasterMng_OrganizationUnitID
								left outer join TA_Person prs2 on prs2.prs_IsDeleted=0 AND  organ.organ_PersonID = prs2.Prs_ID
                            ) manager
                            on MngFlow.mngrFlow_ManagerID = manager.MasterMng_ID
                            where UndMng.underMng_FlowID not in 
							                                    (select distinct underManagment.underMng_FlowID from TA_UnderManagment underManagment	
							                                    where underManagment.underMng_Contains = 0 and underManagment.underMng_PersonID is not null and underManagment.underMng_PersonID = @PersonID))as mngflow
join TA_ManagerFlow as allmngFlow on mngflow.FlowId=allmngFlow.mngrFlow_FlowID and allmngFlow.mngrFlow_Active=1
join (select mng.MasterMng_ID,prs1.Prs_ID prsID,prs1.Prs_FirstName prsFirstName,prs1.Prs_LastName prsLastName,prs2.Prs_ID organPrsID,prs2.Prs_FirstName organPrsFirstName,prs2.Prs_LastName organPrsLastName
								from TA_Manager mng
								left outer join TA_Person prs1 on prs1.prs_IsDeleted=0 AND  mng.MasterMng_PersonID = prs1.Prs_ID
								left outer join TA_OrganizationUnit organ on organ_ID=mng.MasterMng_OrganizationUnitID
								left outer join TA_Person prs2 on prs2.prs_IsDeleted=0 AND  organ.organ_PersonID = prs2.Prs_ID
                            ) outManager
on allmngFlow.mngrFlow_ManagerID = outManager.MasterMng_ID
order by allmngFlow.mngrFlow_Level
";
            RegisteredRequestsFlowLevelsList = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                   .SetResultTransformer(Transformers.AliasToBean(typeof(RegisteredRequestsFlowLevel)))
                                                   .SetParameter("personId", personId)
                                                   .SetParameter("departmentId", departmentId)
                                                   .List<RegisteredRequestsFlowLevel>();

            return RegisteredRequestsFlowLevelsList;
        }

        /// <summary>
        /// با دریافت یک شناسه پرسنل و کد بخش , مدیران جریان را برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<RegisteredRequestsFlowLevel> GetRequestLevels(decimal departmentId, decimal personId)
        {
            string SQLCommand = "";
            IList<RegisteredRequestsFlowLevel> RegisteredRequestsFlowLevelsList = null;

            SQLCommand = @"
                            
declare @depId numeric(18,0), @PersonID numeric(18,0)
set @depId=:departmentId
set @PersonID = :personId


select  mngrFlow_ID ManagerFlowID,mngrFlow_ManagerID ManagerID,mngrFlow_FlowID FlowID,mngrFlow_Level ManagerLevel
,ManagerName = 
CASE isnull(outManager.organPrsLastName,'') 
	WHEN '' THEN  isnull(outManager.prsFirstName,'') + ' ' + isnull(outManager.prsLastName,'') 
	ELSE isnull(outManager.organPrsFirstName,'') + ' ' + isnull(outManager.organPrsLastName,'')
END 
from
(
select top(1) undermng.underMng_FlowID from 
(
select top(1)* from TA_UnderManagment 
join ta_flow on flow_ID=underMng_FlowID and Flow_ActiveFlow=1 and flow_mainflow=1 and flow_Deleted = 0
where underMng_Contains=1 and underMng_PersonID=@PersonID 
union
select top(1)* from TA_UnderManagment under1 
join ta_flow on flow_ID=underMng_FlowID and Flow_ActiveFlow=1 and flow_mainflow=1 and flow_Deleted = 0
where underMng_Contains=1 and underMng_PersonID is null and underMng_DepartmentID=@depId 
		and not exists(select underMng_ID from TA_UnderManagment under2 where underMng_Contains=0 --and underMng_PersonID is null
						 and under2.underMng_FlowID=under1.underMng_FlowID and underMng_PersonID=@PersonID)
union
select top(1)* from TA_UnderManagment under1 
join ta_flow on flow_ID=underMng_FlowID and Flow_ActiveFlow=1 and flow_mainflow=1 and flow_Deleted = 0
where underMng_Contains=1 and underMng_PersonID is null 
		and underMng_ContainInnerChilds=1 
		and exists(select dep_ParentPath from TA_Department where dep_ID =@depId and dep_ParentPath like '%,'+convert(varchar(10), under1.underMng_DepartmentID )+',%')
		) as undermng
) underflow
join TA_ManagerFlow on underflow.underMng_FlowID=mngrFlow_FlowID and mngrFlow_Active=1
join (select mng.MasterMng_ID,prs1.Prs_ID prsID,prs1.Prs_FirstName prsFirstName,prs1.Prs_LastName prsLastName,prs2.Prs_ID organPrsID,prs2.Prs_FirstName organPrsFirstName,prs2.Prs_LastName organPrsLastName
								from TA_Manager mng
								left outer join TA_Person prs1 on prs1.prs_IsDeleted=0 AND  mng.MasterMng_PersonID = prs1.Prs_ID
								left outer join TA_OrganizationUnit organ on organ_ID=mng.MasterMng_OrganizationUnitID
								left outer join TA_Person prs2 on prs2.prs_IsDeleted=0 AND  organ.organ_PersonID = prs2.Prs_ID
                            ) outManager
on mngrFlow_ManagerID = outManager.MasterMng_ID
order by mngrFlow_Level";
            RegisteredRequestsFlowLevelsList = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                   .SetResultTransformer(Transformers.AliasToBean(typeof(RegisteredRequestsFlowLevel)))
                                                   .SetParameter("personId", personId)
                                                   .SetParameter("departmentId", departmentId)
                                                   .List<RegisteredRequestsFlowLevel>();

            return RegisteredRequestsFlowLevelsList;
        }

        public void DeleteUnConfirmedRequestStates()
        {
            string SQLCommand = @"delete from TA_RequestStatus 
                                where  
                                reqStat_MnagerFlowID in 
                                (	
                                select mngrFlow_ID from  TA_ManagerFlow 
                                join ta_flow on flow_id=mngrFlow_FlowID
                                where isnull(Flow_Deleted,0)=1
                                )
                                and
                                isnull(reqStat_RequestID,0)
	                                not in (select isnull(rs.reqStat_RequestID,0) from TA_RequestStatus rs where isnull(rs.reqStat_EndFlow,0)=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                              .ExecuteUpdate();

        }

        public void DeleteSuspendRequestStates(decimal flowId)
        {
            string SQLCommand = @"delete from TA_RequestStatus 
                                where  
                                 reqStat_MnagerFlowID in 
                                (	
                                select mngrFlow_ID from  TA_ManagerFlow 
                                where mngrFlow_FlowID=:flowId
                                )                               
                                and
                                isnull(reqStat_RequestID,0)
	                                not in (select isnull(rs.reqStat_RequestID,0) from TA_RequestStatus rs where isnull(rs.reqStat_EndFlow,0)=1)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("flowId", flowId)
                              .ExecuteUpdate();

        }

        public override string TableName
        {
            get { return "TA_RequestStatus"; }
        }
    }
}
