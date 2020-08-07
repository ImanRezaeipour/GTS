using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Linq.Expressions;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Transform;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using System.IO;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.UIValidation;


namespace GTS.Clock.Infrastructure.Repository
{
    public class RequestRepository : RepositoryBase<GTS.Clock.Model.RequestFlow.Request>, IRequestRepository
    {
        public override string TableName
        {
            get { return "TA_Flow"; }
        }

        public RequestRepository(bool disconectly)
            : base(disconectly)
        { }

        #region IRequestRepository Members

        /// <summary>
        /// آیا یک محل ماموریت جایی استفاده شده است
        /// </summary>
        /// <param name="placeId"></param>
        /// <returns></returns>
        public bool IsDutyPlaceUsed(decimal placeId)
        {
            string SQLCommand = @"select Count(*) from TA_RequestDetail
                                  where reqDtl_DutyPositionID in (:placeId)";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                              .SetParameter("placeId", placeId);
            int resultCount = Query.UniqueResult<int>();
            return resultCount > 0 ? true : false;

        }

        /// <summary>
        /// یک نوع خاص روزانه از درخواستهای ثبت شده را در تاریخی معین برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllHourlyRequestByType(decimal personId, DateTime date, PrecardGroupsName key)
        {
            string HQLCommand = @"SELECT req FROM Request as req
                                INNER JOIN req.Precard AS p
                                INNER JOIN p.PrecardGroup AS g
                                WHERE g.LookupKey=:key
                                AND p.IsHourly=1
                                AND req.FromDate<=:thedate
                                AND req.ToDate>=:thedate
                                AND req.Person.ID=:personId";
            IList<GTS.Clock.Model.RequestFlow.Request> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("key", key.ToString())
                               .SetParameter("thedate", date)
                               .SetParameter("personId", personId)
                               .List<GTS.Clock.Model.RequestFlow.Request>();
            return list;
        }

        /// <summary>
        /// یک نوع خاص ساعتی از درخواستهای ثبت شده را در تاریخی معین برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllHourlyRequestByType(decimal personId, DateTime date, PrecardGroupsName key1, PrecardGroupsName key2, PrecardGroupsName key3)
        {
            string HQLCommand = @"SELECT req FROM Request as req
                                INNER JOIN req.Precard AS p
                                INNER JOIN p.PrecardGroup AS g
                                WHERE (g.LookupKey=:key1 OR g.LookupKey=:key2 OR g.LookupKey=:key3)
                                AND p.IsHourly=1
                                AND req.FromDate=:thedate
                                AND req.Person.ID=:personId";
            IList<GTS.Clock.Model.RequestFlow.Request> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("key1", key1.ToString())
                               .SetParameter("key2", key2.ToString())
                                .SetParameter("key3", key3.ToString())
                               .SetParameter("thedate", date)
                               .SetParameter("personId", personId)
                               .List<GTS.Clock.Model.RequestFlow.Request>();
            return list;
        }

        /// <summary>
        /// یک نوع خاص روزانه از درخواستهای ثبت شده را در تاریخی معین برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllDailyRequestByType(decimal personId, DateTime date, PrecardGroupsName key)
        {
            string HQLCommand = @"SELECT req FROM Request as req
                                INNER JOIN req.Precard AS p
                                INNER JOIN p.PrecardGroup AS g
                                WHERE g.LookupKey=:key
                                AND p.IsDaily=1
                                AND req.FromDate<=:thedate
                                AND req.ToDate>=:thedate
                                AND req.Person.ID=:personId";
            IList<GTS.Clock.Model.RequestFlow.Request> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("key", key.ToString())
                               .SetParameter("thedate", date)
                               .SetParameter("personId", personId)
                               .List<GTS.Clock.Model.RequestFlow.Request>();
            return list;
        }

        /// <summary>
        /// یک نوع خاص روزانه از درخواستهای ثبت شده را در تاریخی معین برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllDailyRequestByType(decimal personId, DateTime date, PrecardGroupsName key1, PrecardGroupsName key2, PrecardGroupsName key3)
        {
            string HQLCommand = @"SELECT req FROM Request as req
                                INNER JOIN req.Precard AS p
                                INNER JOIN p.PrecardGroup AS g
                                WHERE (g.LookupKey=:key1 OR g.LookupKey=:key2 OR g.LookupKey=:key3)
                                AND p.IsDaily=1
                                AND req.FromDate<=:thedate
                                AND req.ToDate>=:thedate
                                AND req.Person.ID=:personId";
            IList<GTS.Clock.Model.RequestFlow.Request> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("key1", key1.ToString())
                               .SetParameter("key2", key2.ToString())
                               .SetParameter("key3", key3.ToString())
                               .SetParameter("thedate", date)
                               .SetParameter("personId", personId)
                               .List<GTS.Clock.Model.RequestFlow.Request>();
            return list;
        }

        /// <summary>
        /// درخواستهای یک شخص را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllRequest(decimal personId, DateTime fromDate, DateTime toDate, RequestState status)
        {
            GTS.Clock.Model.RequestFlow.Request req = null;
            RequestStatus reqStatus = null;
            RequestStatus existsReqStatus = null;
            IList<GTS.Clock.Model.RequestFlow.Request> list = null;
            if (status == RequestState.UnKnown)
            {
                list = base.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Left.JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                                  .Where(() => req.FromDate >= fromDate)
                                                  .And(() => req.ToDate <= toDate)
                                                  .And(() => req.Person.ID == personId)
                                                  .List<GTS.Clock.Model.RequestFlow.Request>();
            }
            else
            {
                if (status == RequestState.Confirmed)
                {
                    list = base.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                 .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                                 .Where(() => req.FromDate >= fromDate)
                                                 .And(() => req.ToDate <= toDate)
                                                 .And(() => req.Person.ID == personId)
                                                 .Where(() => reqStatus.Confirm && reqStatus.EndFlow)
                                                 .List<GTS.Clock.Model.RequestFlow.Request>();
                }
                else if (status == RequestState.Unconfirmed)
                {
                    list = base.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                   .JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                                   .Where(() => req.FromDate >= fromDate)
                                                   .And(() => req.ToDate <= toDate)
                                                   .And(() => req.Person.ID == personId)
                                                   .Where(() => !reqStatus.Confirm && reqStatus.EndFlow)
                                                   .List<GTS.Clock.Model.RequestFlow.Request>();

                }
                else if (status == RequestState.UnderReview)
                {
                    var existing = QueryOver.Of<RequestStatus>(() => existsReqStatus)
                        .Where(() => existsReqStatus.EndFlow == true)
                        .And(() => existsReqStatus.Request.ID == req.ID)
                        .Select(x => x.ID);

                    list = base.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => req)
                                                  .Left.JoinAlias(() => req.RequestStatusList, () => reqStatus)
                                                  .Where(() => req.FromDate >= fromDate)
                                                  .And(() => req.ToDate <= toDate)
                                                  .And(() => req.Person.ID == personId)
                                                  .WithSubquery.WhereNotExists(existing)
                                                  .List<GTS.Clock.Model.RequestFlow.Request>();
                }
            }
            return list;
        }

        #region kartabl

        /// <summary>
        /// درخواست های افراد را برمیگرداند
        /// </summary>
        /// <param name="personIds">لیست افراد</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="requestType">نوع درخواست</param>
        /// <param name="pageIndex">ایندکس صفجه</param>
        /// <param name="pageSize">تعداد آیتم هر صفحه</param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllManagerKartabl(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, int pageIndex, int pageSize, decimal[] quciSearchInUnderManagments, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLCommandPaging = "";
            string SQLOrderby = "";
            string extraJoin = "";
            IList<InfoRequest> list = null;

            #region Query

            SQLCommand = @"
declare @managerId decimal(28,5),@fromDate datetime,@toDate datetime,@start int,@end int
set @managerId=:managerId
set @fromDate=:fromDate
set @toDate=:toDate
set @start=:start
set @end=:end

SELECT Number, ID,request_PrecardID PrecardID,request_PersonID PersonID,
FromDate,ToDate,FromTime,ToTime,
TimeDuration,Description,RegisterDate,UserID,AttachmentFile,
OperatorUser,mngrFlowID
,Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, Precrd_Monthly IsMonthly, Prs_FirstName ApplicantFirstName , Prs_LastName ApplicantLastName,Prs_Barcode PersonCode, prs.PrsDtl_Image PersonImage
, precardGroup.PishcardGrp_LookupKey LookupKey
 FROM
(SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime, request_AttachmentFile AttachmentFile,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
request_OperatorUser as OperatorUser,
MngFlow.mngrFlow_ID mngrFlowID,request_PersonID
,request_PrecardID
FROM (SELECT mngrFlow_ID,mngrFlow_FlowID ,mngrFlow_Level
      FROM TA_ManagerFlow 
      WHERE mngrFlow_ManagerID = @managerId  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons						
INNER JOIN	(SELECT request_ID,request_FromDate,request_FromTime,request_ToDate,request_ToTime,request_TimeDuration,request_RegisterDate,request_PersonID ,request_PrecardID
					,request_Description,request_UserID,request_OperatorUser, request_AttachmentFile
             FROM TA_Request             
             WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = /*UnderMgn*/UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId


WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT mngrFlow_ID 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID 
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and  reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	
			";

            extraJoin = @"
                        INNER JOIN (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
                        INNER JOIN TA_Precard on Precrd_ID=result1.request_PrecardID   
                        INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID    ";
            #endregion

            #region Order By
            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderby = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderby = " order by ApplicantFirstName";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderby = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderby = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderby = " order by PrecardName";
                    break;
            }

            #endregion

            #region Pagging
            SQLCommandPaging = @" ) as window
                                where window.number>=@start and window.number<=@end
                                ) as result1 ";
            #endregion

            IQuery Query = null;

            #region Quick Search UnderManagment
            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                SQLCommand += @" AND
                  request_PersonId in (:quciSearchInUnderManagments)";
            }
            #endregion

            #region Request Type
            if (requestType == RequestType.Hourly)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)";

                SQLCommand += condition + SQLCommandPaging + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)";

                SQLCommand += condition + SQLCommandPaging + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition + SQLCommandPaging + extraJoin + SQLOrderby; ;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else if (requestType == RequestType.Imperative)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition + SQLCommandPaging + extraJoin + SQLOrderby; ;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.imperative);

            }
            else
            {
                SQLCommand += SQLCommandPaging + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);
            }
            #endregion

            if (Query != null)
            {
                Query = Query
                    .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                    .SetParameter("managerId", managerId)
                    .SetParameter("fromDate", fromDate)
                    .SetParameter("toDate", toDate)
                    .SetParameter("start", pageIndex * pageSize + 1)
                    .SetParameter("end", (pageIndex + 1) * pageSize);

                if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
                {
                    Query = Query.SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));
                }

                list = Query.List<InfoRequest>();
            }

            return list;

        }

        /// <summary>
        /// درخواست های افراد را برمیگرداند
        /// </summary>
        /// <param name="managerId">شناسه مدیر</param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllManagerKartabl(decimal managerId, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLOrderby = "";
            IList<InfoRequest> list = null;


            #region Query
            SQLCommand = @"
declare @managerId decimal(28,5)
set @managerId=:managerId

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,req.Prs_FirstName + ' ' + req.Prs_LastName Applicant,req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID
FROM (SELECT * 
      FROM TA_ManagerFlow 
      WHERE mngrFlow_ManagerID = @managerId  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
            Where Flow.flow_Deleted = 0						
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons					
INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   

INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
   
    AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	
 ) as window

			";
            #endregion

            #region order by
            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderby = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderby = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderby = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderby = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderby = " order by PrecardName";
                    break;
            }

            #endregion

            IQuery Query = null;
            SQLCommand += SQLOrderby;
            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            if (Query != null)
            {
                Query = Query
                    .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                    .SetParameter("managerId", managerId);


                list = Query.List<InfoRequest>();
            }

            return list;

        }

        /// <summary>
        /// درخواستهای افراد برای جانشین را برمیگرداند
        /// بدلیل لزوم فیلتر بر روی تاریخ جانشین و سطح دسترسی جانشین مجبور به ایجاد کوئری مجزا هشتیم
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType">نوع درخواست</param>
        /// <param name="managerId">شناسه مدیریت جانشین - جانشین ممکن است خود جانشین باشد</param>
        /// <param name="personId">شناسه جانشین</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllSubstituteKartabl(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, decimal personId, int pageIndex, int pageSize, decimal[] quciSearchInUnderManagments, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLCommandPaging = "";
            string SQLOrderby = "";
            string extension = "";
            IList<InfoRequest> list = null;

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderby = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderby = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderby = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderby = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderby = " order by PrecardName";
                    break;
            }

            if (requestType == RequestType.Hourly)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)";
            }
            else if (requestType == RequestType.Daily)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)";

            }
            else if (requestType == RequestType.OverWork)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";
            }
            else if (requestType == RequestType.Imperative)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";
            }

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                extension += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            SQLCommand = @"
declare @managerId decimal(28,5),@personId decimal(28,5),@fromDate datetime,@toDate datetime,@start int,@end int
set @managerId=:managerId
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate
set @start=:start
set @end=:end

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by window1.ID) as Number,* FROM 
(
SELECT  request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,req.Prs_FirstName ApplicantFirstName, req.Prs_LastName ApplicantLastName,req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID, Req.PrsDtl_Image PersonImage
FROM (SELECT * 
      FROM TA_ManagerFlow  where mngrFlow_Active=1
     ) MngFlow
INNER JOIN TA_Substitute as sub
ON sub_ManagerId=MngFlow.mngrFlow_ManagerID AND sub_PersonId=@personId
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID	
            Where Flow.flow_Deleted = 0		
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons 
INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN 
             (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0             
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
    AND
   Req.request_RegisterDate>=sub.sub_FromDate
    AND
   Req.request_RegisterDate<=sub.sub_ToDate
   
INNER JOIN TA_SubstitutePrecardAccess 
ON subaccess_PrecardId=request_PrecardID AND subaccess_SubstituteId=sub.sub_ID   

INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )

      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	

" +
  extension
+ @"
-----------------------------------------------------------------------							
UNION
-----------------------------------------------------------------------
SELECT request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,Prs_FirstName ApplicantFirstName, Prs_LastName ApplicantLastName,prs.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID, prs.PrsDtl_Image PersonImage
FROM (SELECT * 
      FROM TA_ManagerFlow 
      WHERE mngrFlow_ManagerID = @managerId  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons
INNER JOIN	(SELECT * 
             FROM TA_Request 
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
INNER JOIN (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=Req.request_PersonID AND prs.prs_IsDeleted=0 
INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                        where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )

      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )			"
            + extension;



            SQLCommandPaging = @" ) as window1 ) as window2
                                where window2.number>=@start and window2.number<=@end";

            IQuery Query = null;


            if (requestType == RequestType.Hourly)
            {
                SQLCommand += SQLCommandPaging + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                SQLCommand += SQLCommandPaging + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                SQLCommand += SQLCommandPaging + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else
            {
                SQLCommand += SQLCommandPaging + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);
            }

            if (Query != null)
            {
                Query = Query
                    .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                    .SetParameter("managerId", managerId)
                    .SetParameter("personId", personId)
                    .SetParameter("fromDate", fromDate)
                    .SetParameter("toDate", toDate)
                    .SetParameter("start", pageIndex * pageSize + 1)
                    .SetParameter("end", (pageIndex + 1) * pageSize);

                if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
                {
                    Query = Query
                         .SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));

                }

                list = Query.List<InfoRequest>();
            }

            return list;

        }

        /// <summary>
        /// درخواستهای افراد برای جانشین را برمیگرداند
        /// جهت استفاده در سرویس اطلاع رسانی
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType">نوع درخواست</param>
        /// <param name="managerId">شناسه مدیریت جانشین - جانشین ممکن است خود جانشین باشد</param>
        /// <param name="personId">شناسه جانشین</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllSubstituteKartabl(decimal managerId, decimal personId, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLOrderby = "";
            IList<InfoRequest> list = null;

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderby = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderby = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderby = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderby = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderby = " order by PrecardName";
                    break;
            }


            SQLCommand = @"
declare @managerId decimal(28,5),@personId decimal(28,5)
set @managerId=:managerId
set @personId=:personId

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by window1.ID) as Number,* FROM 
(
SELECT  request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,req.Prs_FirstName ApplicantFirstName, req.Prs_LastName ApplicantLastName,req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID
FROM (SELECT * 
      FROM TA_ManagerFlow  where mngrFlow_Active=1
     ) MngFlow
INNER JOIN TA_Substitute as sub
ON sub_ManagerId=MngFlow.mngrFlow_ManagerID AND sub_PersonId=@personId
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID						
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons 
INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0                          
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
    AND
   Req.request_RegisterDate>=sub.sub_FromDate
    AND
   Req.request_RegisterDate<=sub.sub_ToDate
   
INNER JOIN TA_SubstitutePrecardAccess 
ON subaccess_PrecardId=request_PrecardID AND subaccess_SubstituteId=sub.sub_ID   

INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID 
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )

      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	

-----------------------------------------------------------------------							
UNION
-----------------------------------------------------------------------
SELECT request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,Prs_FirstName ApplicantFirstName, Prs_LastName ApplicantLastName,req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID
FROM (SELECT * 
      FROM TA_ManagerFlow 
      WHERE mngrFlow_ManagerID = @managerId  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID	
            Where Flow.flow_Deleted = 0					
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons
INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )

      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )			
) as window1 ) as window2 "
                       ;


            IQuery Query = null;
            SQLCommand += SQLOrderby;
            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            if (Query != null)
            {
                Query = Query
                    .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                    .SetParameter("managerId", managerId)
                    .SetParameter("personId", personId);


                list = Query.List<InfoRequest>();
            }
            return list;
        }

        /// <summary>
        /// آیتم های کارتابل را برمیگرداند
        /// به مدیر و جانشین توجه نمیکند و لیستی از شناسه مدیر را دریافت میکند
        /// توسط کلاس استفاده کننده باید موارد (صفحه بندی - اعمال فیلتر تاریخ جانشین - دسترسی جانشین به پیشکارتها) اعمال گردد
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="managerList">شامل شناسه مدیر و مدیرانی که جانشین آنها است</param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllKartablItems(DateTime fromDate, DateTime toDate, RequestType requestType, IList<decimal> managerList, KartablOrderBy orderby)
        {
            if (Utility.Utility.IsEmpty(managerList))
                return new List<InfoRequest>();
            string SQLCommand = "";            
            string SQLOrderby = "";
            string extraJoin = "";
            IList<InfoRequest> list = null;

            #region Query

            SQLCommand = @"
declare @fromDate datetime,@toDate datetime

set @fromDate=:fromDate
set @toDate=:toDate


SELECT Number, ID,mngrFlow_ManagerID ManagerID,request_PrecardID PrecardID,request_PersonID PersonID,
FromDate,ToDate,FromTime,ToTime,
TimeDuration,Description,RegisterDate,UserID,AttachmentFile,
OperatorUser,mngrFlowID
,Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, Precrd_Monthly IsMonthly, Prs_FirstName ApplicantFirstName , Prs_LastName ApplicantLastName,Prs_Barcode PersonCode, prs.PrsDtl_Image PersonImage
, precardGroup.PishcardGrp_LookupKey LookupKey
 FROM
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number,MngFlow.mngrFlow_ManagerID, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime, request_AttachmentFile AttachmentFile,
isnull(request_TimeDuration,0) TimeDuration,isnull(request_Description,'') Description,request_RegisterDate RegisterDate,request_UserID UserID,
isnull(request_OperatorUser,'') as OperatorUser,
MngFlow.mngrFlow_ID mngrFlowID,request_PersonID
,request_PrecardID
FROM (SELECT * FROM TA_ManagerFlow where mngrFlow_Active=1  AND mngrFlow_ManagerID in (:managerList)
      ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID	
            Where Flow.flow_Deleted = 0		
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons						
INNER JOIN	(SELECT request_ID,request_FromDate,request_FromTime,request_ToDate,request_ToTime,request_TimeDuration,request_RegisterDate,request_PersonID ,request_PrecardID
					,request_Description,request_UserID,request_OperatorUser, request_AttachmentFile
             FROM TA_Request             
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId


WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT mngrFlow_ID 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
-- the 3 below condition done in one
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
							 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 (reqStat_EndFlow = 1  OR reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
							 OR otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID)
			                )



  /*    Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID 
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	*/
			";

            extraJoin = @"
                        INNER JOIN (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
                        INNER JOIN TA_Precard on Precrd_ID=result1.request_PrecardID   
                        INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID    ";
            #endregion

            #region Order By
            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderby = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderby = " order by ApplicantFirstName";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderby = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderby = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderby = " order by PrecardName";
                    break;
            }

            #endregion

            IQuery Query = null;

            #region Request Type
            if (requestType == RequestType.Hourly)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)
                    ) as result1";

                SQLCommand += condition + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)
                    ) as result1";

                SQLCommand += condition + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))
                    ) as result1";

                SQLCommand += condition + extraJoin + SQLOrderby; ;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else if (requestType == RequestType.Imperative)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))
                    ) as result1";

                SQLCommand += condition + extraJoin + SQLOrderby; ;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.imperative);

            }
            else
            {
                SQLCommand += ") as result1" + extraJoin + SQLOrderby;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);
            }
            #endregion

            if (Query != null)
            {
                Query = Query
                    .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                    .SetParameterList("managerList", managerList.ToArray())
                    .SetParameter("fromDate", fromDate)
                    .SetParameter("toDate", toDate);               

                list = Query.List<InfoRequest>();
            }

            return list;

        }


        #endregion

        #region Reviewed Requests

        /// <summary>
        /// تعداد درخواستهای بررسی شده را برکیگرداند
        /// برای مدیر وجانشین یکسان است
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestState"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public int GetAllManagerReviewdCount(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal managerId, decimal[] quciSearchInUnderManagments)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
declare @managerID decimal(28,5),@fromDate datetime,@toDate datetime
set @managerId=:managerID
set @fromDate=:fromDate
set @toDate=:toDate


SELECT Count(request_ID)
				FROM (SELECT * 
					  FROM TA_ManagerFlow 
					  WHERE mngrFlow_ManagerID = @managerID
					 ) MngFlow
				INNER JOIN TA_RequestStatus reqSt ON MngFlow.mngrFlow_ID=reqStat_MnagerFlowID						
				INNER JOIN	(SELECT * 
							 FROM TA_Request 
							 WHERE request_FromDate >= @fromDate
									AND 
								   request_ToDate <= @toDate
							) Req
				ON Req.request_ID = reqStat_RequestID				
				
		               ";

            IQuery Query = null;

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                SQLCommand += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),0)=1
		                            AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),1)=0
		                                AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";


                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	WHERE EXISTS(select * from TA_RequestStatus where reqStat_RequestID=reqSt.reqStat_RequestID AND reqStat_IsDeleted=1)";

                SQLCommand += condition;
            }
            else
            {
                string condition = @"	WHERE reqStat_Confirm=1 OR reqStat_Confirm=0";

                SQLCommand += condition;
            }


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("managerID", managerId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                Query = Query
                     .SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));

            }
            resultCount = Query.UniqueResult<int>();

            return resultCount;

        }

        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="personIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllManagerReviewd(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal managerId, int pageIndex, int pageSize, decimal[] quciSearchInUnderManagments, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLOrderBy = "";
            IList<InfoRequest> list = null;

            //Retrive by request status
            SQLCommand = @"
SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number,  request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,request_AttachmentFile AttachmentFile,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, 
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,Precrd_Monthly IsMonthly,Req.Prs_FirstName + ' ' + Req.Prs_LastName Applicant,Req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser, Req.PrsDtl_Image PersonImage,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
isnull((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) IsDeleted
FROM (SELECT * 
					  FROM TA_ManagerFlow 
					  WHERE mngrFlow_ManagerID = :managerID
					 ) MngFlow
				INNER JOIN TA_RequestStatus reqSt ON MngFlow.mngrFlow_ID=reqStat_MnagerFlowID						
				INNER JOIN	(SELECT * 
							 FROM TA_Request 
				             INNER JOIN 
                             (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
							 WHERE ( request_FromDate >= :fromDate
                                    AND 
                                   request_FromDate <= :toDate )
                                   OR
                                   ( request_ToDate >= :fromDate
                                    AND 
                                   request_ToDate <= :toDate )
							) Req
				ON Req.request_ID = reqStat_RequestID
				INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
				INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID				
		               ";

            IQuery Query = null;

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                SQLCommand += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),0)=1
		                            AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),1)=0
                                        AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	WHERE EXISTS(select * from TA_RequestStatus where reqStat_RequestID=reqSt.reqStat_RequestID AND reqStat_IsDeleted=1)";

                SQLCommand += condition;
            }
            else
            {
                string condition = @"	WHERE reqStat_Confirm=1 OR reqStat_Confirm=0";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
                            where window.number>=:start and window.number<=:end";

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderBy = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderBy = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderBy = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderBy = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderBy = " order by PrecardName";
                    break;
            }
            SQLCommand += SQLOrderBy;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                              .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                              .SetParameter("managerID", managerId)
                              .SetParameter("fromDate", fromDate)
                              .SetParameter("toDate", toDate)
                              .SetParameter("start", pageIndex * pageSize + 1)
                              .SetParameter("end", (pageIndex + 1) * pageSize);

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                Query = Query.SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));
            }

            list = Query.List<InfoRequest>();

            return list;
        }

        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="personIds"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllSubstituteReviewd(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal managerId, int pageIndex, int pageSize, decimal[] quciSearchInUnderManagments, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLOrderBy = "";
            IList<InfoRequest> list = null;

            //Retrive by request status
            SQLCommand = @"
SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number,  request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,Req.Prs_FirstName + ' ' + Req.Prs_LastName Applicant,Req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
isnull((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) IsDeleted
FROM (SELECT * 
					  FROM TA_ManagerFlow 
					  WHERE mngrFlow_ManagerID = :managerID
					 ) MngFlow
				INNER JOIN TA_RequestStatus reqSt ON MngFlow.mngrFlow_ID=reqStat_MnagerFlowID						
				INNER JOIN	(SELECT * 
							 FROM TA_Request 
				             INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
							 WHERE ( request_FromDate >= :fromDate
                                    AND 
                                   request_FromDate <= :toDate )
                                   OR
                                   ( request_ToDate >= :fromDate
                                    AND 
                                   request_ToDate <= :toDate )
							) Req
				ON Req.request_ID = reqStat_RequestID
				INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
				INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID				
		               ";

            IQuery Query = null;

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                SQLCommand += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),0)=1
		                            AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),1)=0
                                        AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	WHERE EXISTS(select * from TA_RequestStatus where reqStat_RequestID=reqSt.reqStat_RequestID AND reqStat_IsDeleted=1)";

                SQLCommand += condition;
            }
            else
            {
                string condition = @"	WHERE reqStat_Confirm=1 OR reqStat_Confirm=0";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
                            where window.number>=:start and window.number<=:end";

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderBy = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderBy = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderBy = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderBy = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderBy = " order by PrecardName";
                    break;
            }
            SQLCommand += SQLOrderBy;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                              .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                              .SetParameter("managerID", managerId)
                              .SetParameter("fromDate", fromDate)
                              .SetParameter("toDate", toDate)
                              .SetParameter("start", pageIndex * pageSize + 1)
                              .SetParameter("end", (pageIndex + 1) * pageSize);

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                Query = Query.SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));
            }

            list = Query.List<InfoRequest>();

            return list;
        }

        /// <summary>
        /// آیتم های تاریخچه کارتابل را برمیگرداند
        /// به مدیر و جانشین توجه نمیکند و لیستی از شناسه مدیر را دریافت میکند
        /// توسط کلاس استفاده کننده باید موارد (صفحه بندی - اعمال فیلتر تاریخ جانشین - دسترسی جانشین به پیشکارتها) اعمال گردد
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="managerList"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllReviewdKartablItems(DateTime fromDate, DateTime toDate, RequestState requestStatus, IList<decimal> managerList, KartablOrderBy orderby)
        {
            string SQLCommand = "";
            string SQLOrderBy = "";
            IList<InfoRequest> list = null;
         
            SQLCommand = @"
declare @fromDate datetime,@toDate datetime
set @fromDate=:fromDate
set @toDate=:toDate

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number,MngFlow.mngrFlow_ManagerID ManagerID,  request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,request_AttachmentFile AttachmentFile,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, 
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily,Precrd_Monthly IsMonthly,Req.Prs_FirstName + ' ' + Req.Prs_LastName Applicant,Req.Prs_Barcode PersonCode,request_OperatorUser as OperatorUser, Req.PrsDtl_Image PersonImage,
precardGroup.PishcardGrp_LookupKey LookupKey,MngFlow.mngrFlow_ID mngrFlowID,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
isnull((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 and ISNULL(reqStat_IsDeleted,0)=1),0) IsDeleted
FROM 
    (SELECT * FROM TA_ManagerFlow where mngrFlow_Active=1  AND mngrFlow_ManagerID in (:managerList)
      ) MngFlow
	INNER JOIN TA_RequestStatus reqSt ON MngFlow.mngrFlow_ID=reqStat_MnagerFlowID						
	INNER JOIN	(SELECT * 
				 FROM TA_Request 
				 INNER JOIN 
                     (Select TA_Person.*, TA_PersonDetail.PrsDtl_Image from TA_Person inner join TA_PersonDetail  on TA_Person.Prs_ID = TA_PersonDetail.PrsDtl_ID) prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
                   WHERE ( request_FromDate >= @fromDate
                           AND 
                      request_FromDate <= @toDate )
                         OR
                     ( request_ToDate >= @fromDate
                          AND 
                      request_ToDate <= @toDate )
							) Req
				ON Req.request_ID = reqStat_RequestID
				INNER JOIN TA_Precard on Precrd_ID=Req.request_PrecardID   
				INNER JOIN TA_PrecardGroups as precardGroup on precardGroup.PishcardGrp_ID=Precrd_pshcardGroupID				
		               ";

            IQuery Query = null;        

            if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),0)=1
		                            AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	WHERE ISNULL((select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc),1)=0
                                        AND ISNULL((select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 and ISNULL(reqStat_IsDeleted,0)=1),0) = 0 ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	WHERE EXISTS(select * from TA_RequestStatus where reqStat_RequestID=reqSt.reqStat_RequestID AND reqStat_EndFlow=1 AND reqStat_IsDeleted=1)";

                SQLCommand += condition;
            }
            else
            {
                string condition = @"	WHERE reqStat_Confirm=1 OR reqStat_Confirm=0";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window";

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    SQLOrderBy = " order by PersonCode";
                    break;
                case KartablOrderBy.PersonName:
                    SQLOrderBy = " order by Applicant";
                    break;
                case KartablOrderBy.RegisteredBy:
                    SQLOrderBy = " order by FromDate";
                    break;
                case KartablOrderBy.RequestDate:
                    SQLOrderBy = " order by RegisterDate";
                    break;
                case KartablOrderBy.RequestSubject:
                    SQLOrderBy = " order by PrecardName";
                    break;
            }
            SQLCommand += SQLOrderBy;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                              .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                              .SetParameterList("managerList", managerList)
                              .SetParameter("fromDate", fromDate)
                              .SetParameter("toDate", toDate);         

            list = Query.List<InfoRequest>();

            return list;
        }


        #endregion

        #region Registerd Request

        /// <summary>
        /// درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="personId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllUserRequest(DateTime reviewfromDate, DateTime reviewtoDate, RequestState requestStatus, decimal personId)
        {
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"
declare @personId numeric(18,0),@fromDate datetime,@toDate datetime
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate
SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,prs.prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) ManagerDescription
							 FROM TA_Request Req
                              INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0          
                              Inner join TA_Precard on Precrd_ID=request_PrecardID
                              Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       
							  WHERE request_PersonID=@personId AND precardGroup.PishcardGrp_LookupKey<>'imperative' AND
                              (( request_FromDate >= :fromDate
                                        AND 
                                      request_FromDate <= :toDate )
                                       OR
                                    ( request_ToDate >= :fromDate
                                        AND 
                                      request_ToDate <= :toDate ))	      			
		               ";


            IQuery Query = null;

            if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_IsDeleted=1
                                                                          
                                    )
                                    ";
                //AND CONVERT(DATE,reqStat_Date) >= @fromDate 
                //                    AND CONVERT(DATE,reqStat_Date) <= @toDate 
                SQLCommand += condition;
            }

            else if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_IsDeleted=1)
                                       
                                    )";
                //AND CONVERT(DATE,reqStat_Date) >= @fromDate 
                //                    AND CONVERT(DATE,reqStat_Date) <= @toDate 

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=0
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_IsDeleted=1)                                    
                                       
                                    )
                                    ";
                //AND CONVERT(DATE,reqStat_Date) >= @fromDate 
                //                    AND CONVERT(DATE,reqStat_Date) <= @toDate 
                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnderReview)
            {
                string condition = @"	AND request_ID not in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_EndFlow=1
                                   
                                    )";
                 //AND CONVERT(DATE,reqStat_Date) >= @fromDate 
                 //                   AND CONVERT(DATE,reqStat_Date) <= @toDate 

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnKnown)
            {
                string condition = @"";
                //AND CONVERT(DATE,reqStat_Date) >= @fromDate 
                //                    AND CONVERT(DATE,reqStat_Date) <= @toDate  
                SQLCommand += condition;
            }

            SQLCommand += @" ) as window ";


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameter("personId", personId)
                .SetParameter("fromDate", reviewfromDate)
                .SetParameter("toDate", reviewtoDate);
            list = Query.List<InfoRequest>();

            return list;
        }

        /// <summary>
        /// درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="personId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllUserRequestByPagging(DateTime reviewfromDate, DateTime reviewtoDate, RequestState requestStatus, decimal personId, int pageIndex, int pageSize)
        {
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"
SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,prs.prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) ManagerDescription,
(select top(1)reqStat_MnagerFlowID from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) mngrFlowID
							 FROM TA_Request Req
                              INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0          
                              Inner join TA_Precard on Precrd_ID=request_PrecardID
                              Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       
							  WHERE request_PersonID=:personId AND precardGroup.PishcardGrp_LookupKey<>'imperative' AND
                                    (( request_FromDate >= :fromDate
                                        AND 
                                      request_FromDate <= :toDate )
                                       OR
                                    ( request_ToDate >= :fromDate
                                        AND 
                                      request_ToDate <= :toDate ))				
		               ";


            IQuery Query = null;

            if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_EndFlow=1
                                    AND reqStat_IsDeleted=1
                                    )
                                    ";

                SQLCommand += condition;
            }

            else if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_EndFlow=1 AND reqStat_IsDeleted=1)
                                    )";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=0
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_EndFlow=1 AND reqStat_IsDeleted=1)                                    
                                    )
                                    ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnderReview)
            {
                string condition = @"	AND request_ID not in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_EndFlow=1
                                    )";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
                            where window.number>=:start and window.number<=:end	";


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameter("personId", personId)
                .SetParameter("fromDate", reviewfromDate)
                .SetParameter("toDate", reviewtoDate)
                .SetParameter("start", pageIndex * pageSize + 1)
                .SetParameter("end", (pageIndex + 1) * pageSize);
            list = Query.List<InfoRequest>();

            return list;
        }


        /// <summary>
        /// درخواستهای فیلتر درخواستهای ثبت شده
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="submiter"></param>
        /// <param name="personId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllUserFilterRequestByPagging(DateTime? fromDate, DateTime? toDate, RequestType? requestType, RequestSubmiter? submiter, decimal personId, int pageIndex, int pageSize)
        {
            if (submiter == RequestSubmiter.OPERATOR)
            {
                throw new NotImplementedException();
            }
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName Applicant,'' PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) ManagerDescription,
(select top(1)reqStat_MnagerFlowID from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) mngrFlowID
							 FROM TA_Request Req
                             INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
							 join TA_Precard on Precrd_ID=request_PrecardID
							 join TA_PrecardGroups precardGroup on PishcardGrp_ID=Precrd_pshcardGroupID
							 WHERE request_PersonID=:personId AND precardGroup.PishcardGrp_LookupKey<>'imperative'
		               ";

            IQuery Query = null;

            if (fromDate != null)
            {
                string condition = @"	AND 
								     request_FromDate >= :fromDate 
                                    ";

                SQLCommand += condition;
            }

            if (toDate != null)
            {
                string condition = @"	AND 
								     request_ToDate <= :toDate 
                                    ";

                SQLCommand += condition;
            }
            if (requestType == RequestType.OverWork)
            {
                string condition = @"	AND 
                                        PishcardGrp_LookupKey = '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"	AND 
                                        Precrd_Daily = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Hourly)
            {
                string condition = @"	AND 
                                        Precrd_Hourly = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
                            where window.number>=:start and window.number<=:end	";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameter("personId", personId)
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize);

            if (fromDate != null)
                Query.SetParameter("fromDate", fromDate);
            if (toDate != null)
                Query.SetParameter("toDate", toDate);
            Query.SetParameter("start", pageIndex * pageSize + 1);
            Query.SetParameter("end", (pageIndex + 1) * pageSize);

            list = Query.List<InfoRequest>();

            return list;
        }
     
        /// <summary>
        /// درخواستهای ثبت شده اپراتور را برمیگرداند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="personId">شناسه پرسنلی اپراتور</param>
        /// <param name="undermanagmentList">افراد تحت مدیریت</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllOperatorRequestByPagging(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal personId, int pageIndex, int pageSize)
        {
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"
declare @prsID decimal(28,5),@fromDate datetime,@toDate datetime

set @prsID=:personId
set @fromDate=:fromDate
set @toDate=:toDate

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,'' PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID and reqStat_EndFlow=1 and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID order by reqStat_ID desc) ManagerDescription,
(select top(1)reqStat_MnagerFlowID from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID order by reqStat_ID desc) mngrFlowID
FROM 
	(
	    Select * From 
	                 (
	                    SELECT distinct req.*  
	                    FROM 	    
		                    (SELECT opr_FlowId 
		                     FROM TA_Operator 
		                     WHERE opr_PersonId = @prsID  AND opr_Active=1
		                    ) opr
		                INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
				                    FROM TA_Flow Flow
				                    INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
				                    ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
                                    Where Flow.flow_Deleted = 0			
			                       ) AS UnderMgn
		                ON opr.opr_FlowId = UnderMgn.Flow_Id							
		                CROSS APPLY 
		                (
			               select Prs_Id from (
			               select * from [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID)
			            ) unders
		             ) UndermanagmentsPersons

	             	   INNER JOIN (SELECT * 
					               FROM TA_Request 
					               WHERE request_FromDate >= @fromDate 
						           AND 
					               request_ToDate <= @toDate 
				                  ) Req
		               ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
		               AND Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId  
        ) req 
	    union
	    SELECT * 
		         FROM TA_Request 
			     WHERE request_FromDate >= @fromDate 
			           AND 
			           request_ToDate <= @toDate 
					   AND 
					   request_PersonID = @prsID				
   ) oprRequests             
             
INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0
INNER JOIN TA_Precard on Precrd_ID=request_PrecardID
INNER JOIN TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID							 
WHERE request_FromDate >= @fromDate
	AND request_ToDate <= @toDate
	AND precardGroup.PishcardGrp_LookupKey<>'imperative'
	
		               ";
            IQuery Query = null;

            if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_EndFlow=1
                                    AND reqStat_IsDeleted=1
                                    )
                                    ";

                SQLCommand += condition;
            }

            else if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID AND reqStat_EndFlow=1 AND reqStat_IsDeleted=1)
                                    )";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_Confirm=0
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID AND reqStat_EndFlow=1 AND reqStat_IsDeleted=1)
                                    )";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnderReview)
            {
                string condition = @"	AND request_ID not in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_EndFlow=1
                                    )";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
where window.number>=:start and window.number<=:end	";


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameter("personId", personId)
                .SetParameter("fromDate", fromDate)
                .SetParameter("toDate", toDate)
                .SetParameter("start", pageIndex * pageSize + 1)
                .SetParameter("end", (pageIndex + 1) * pageSize);
            list = Query.List<InfoRequest>();

            return list;
        }
  
        /// <summary>
        /// درخواستهای اپراتور فیلتر درخواستهای ثبت شده
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="submiter"></param>
        /// <param name="personId">شناسه پرسنلی اپراتور</param>
        /// <param name="userId">شناسه کاربری اپراتور</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<InfoRequest> GetAllOperatorRequestByPagging(DateTime? fromDate, DateTime? toDate, RequestType? requestType, RequestSubmiter? submiter, decimal personId, decimal userId, decimal underManagmentPersonId, int pageIndex, int pageSize)
        {
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"

declare @prsID decimal(28,5),@fromDate datetime,@toDate datetime 
set @prsID=:personId
set @fromDate=:fromDate
set @toDate=:toDate

SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID, request_AttachmentFile AttachmentFile,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,'' PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID order by reqStat_ID desc) ManagerDescription,
(select top(1)reqStat_MnagerFlowID from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID order by reqStat_ID desc) mngrFlowID
FROM 
	(
	    Select * From 
	                 (
	                    SELECT distinct req.*  
	                    FROM 	    
		                    (SELECT opr_FlowId 
		                     FROM TA_Operator 
		                     WHERE opr_PersonId = @prsID  AND opr_Active=1
		                    ) opr
		                INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
				                    FROM TA_Flow Flow
				                    INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
				                    ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
                                    Where Flow.flow_Deleted = 0			
			                       ) AS UnderMgn
		                ON opr.opr_FlowId = UnderMgn.Flow_Id							
		                CROSS APPLY 
		                (
			               select Prs_Id from (
			               select * from [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID)
			            ) unders
		             ) UndermanagmentsPersons

	             	   INNER JOIN (SELECT * 
					               FROM TA_Request 
					               WHERE request_FromDate >= @fromDate 
						           AND 
					               request_ToDate <= @toDate 
				                  ) Req
		               ON Req.request_PersonID = UndermanagmentsPersons.prs_id			
		               AND Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId";
                       if(underManagmentPersonId > 0)
                           SQLCommand += @" AND
                                            Req.request_PersonID = :underManagmentPersonId ";
        SQLCommand += @"
        ) req ";
        if(underManagmentPersonId <= 0)
           SQLCommand += @"
	                       union
	                       SELECT * 
		                           FROM TA_Request 
			                       WHERE request_FromDate >= @fromDate 
			                       AND 
			                       request_ToDate <= @toDate
                                   AND					    
					               request_PersonID = @prsID ";
        SQLCommand += @"
   ) oprRequests             
             
INNER JOIN TA_Person as prs on prs.Prs_ID = oprRequests.request_PersonID AND prs.prs_IsDeleted=0
INNER JOIN TA_Precard on Precrd_ID=request_PrecardID
INNER JOIN TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID							 
WHERE precardGroup.PishcardGrp_LookupKey<>'imperative'   
		               ";

            IQuery Query = null;

            if (submiter != null)
            {
                string condition = "";
                if (submiter == RequestSubmiter.USER)
                {
                    condition = @"	AND 
								     request_userId = :userId
                                    ";
                }
                else if(submiter == RequestSubmiter.OPERATOR)
                {
                    condition = @"	AND 
								     request_UserId <> :userId 
                                    ";
                }
                SQLCommand += condition;
            }
            if (requestType == RequestType.OverWork)
            {
                string condition = @"	AND 
                                        PishcardGrp_LookupKey = '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"	AND 
                                        Precrd_Daily = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Hourly)
            {
                string condition = @"	AND 
                                        Precrd_Hourly = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }

            SQLCommand += @" ) as window
                            where window.number>=:start and window.number<=:end	";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameter("personId", personId);

            Query.SetParameter("fromDate", fromDate);
            Query.SetParameter("toDate", toDate);
            if (underManagmentPersonId > 0)
                Query.SetParameter("underManagmentPersonId", underManagmentPersonId);
            if (submiter != null)
                Query.SetParameter("userId", userId);

            Query.SetParameter("start", pageIndex * pageSize + 1);
            Query.SetParameter("end", (pageIndex + 1) * pageSize);

            list = Query.List<InfoRequest>();

            return list;
        }

        public IList<InfoRequest> GetAllRequestMinOneLevelConfirm(IList<decimal> personIds, IList<decimal> precardIds, DateTime date, SentryPermitsOrderBy orderby) 
        {
            string SQLCommand = "";
            IList<InfoRequest> list = null;

            SQLCommand = @"
declare @fromDate datetime,@toDate datetime
set @fromDate=:fromDate
set @toDate=:toDate
SELECT * FROM 
(
SELECT ROW_NUMBER() Over(order by request_FromDate,request_FromTime,request_ID) as Number, request_ID ID,request_PrecardID PrecardID,request_PersonID PersonID ,
request_FromDate FromDate,request_ToDate ToDate,request_FromTime FromTime,request_ToTime ToTime,
request_TimeDuration TimeDuration,request_Description Description,request_RegisterDate RegisterDate,request_UserID UserID,
Precrd_Name PrecardName,Precrd_Hourly IsHourly,Precrd_Daily IsDaily, prs.Prs_FirstName+' ' + prs.Prs_LastName  Applicant,prs.prs_Barcode PersonCode,request_OperatorUser as OperatorUser,
precardGroup.PishcardGrp_LookupKey LookupKey,(select top(1)reqStat_Confirm from TA_RequestStatus where reqStat_RequestID=Req.request_ID and reqStat_EndFlow=1 order by reqStat_ID desc) Confirm,
(select top(1)reqStat_IsDeleted from TA_RequestStatus where reqStat_RequestID=Req.request_ID and ISNULL(reqStat_IsDeleted,0)=1) IsDeleted,
(select top(1)reqStat_Description from TA_RequestStatus where reqStat_RequestID=Req.request_ID order by reqStat_ID desc) ManagerDescription
							 FROM TA_Request Req
                              INNER JOIN TA_Person as prs on prs.Prs_ID=request_PersonID AND prs.prs_IsDeleted=0          
                              Inner join TA_Precard on Precrd_ID=request_PrecardID
                              Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       
							  WHERE precardGroup.PishcardGrp_LookupKey<>'imperative'
                                 and 
					            (( request_FromDate >= @fromDate
                                AND 
                               request_FromDate <= @toDate )
                               OR
                               ( request_ToDate >= @fromDate
                                AND 
                               request_ToDate <= @toDate ))
                                    and request_ID in (select reqStat_RequestID from TA_RequestStatus where reqStat_RequestID  is not null  and reqStat_Confirm=1)
                                    and 
                                    request_ID not in (select reqStat_RequestID from TA_RequestStatus where reqStat_RequestID  is not null  and (reqStat_Confirm=0 or reqStat_IsDeleted=1))
                                    and request_personId in (:personList)
                                    and request_precardId in (:precardList)
                                    			
		               ";


            IQuery Query = null;
            
            SQLCommand += @" ) as window
                            ";
            switch (orderby)
            {
                case SentryPermitsOrderBy.PersonCode:
                    SQLCommand += " order by PersonCode ";
                    break;
                case SentryPermitsOrderBy.PermitSubject:
                    SQLCommand += " order by PrecardID ";
                    break;
                case SentryPermitsOrderBy.PersonName:
                    SQLCommand += " order by Applicant ";
                    break;
            }

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetResultTransformer(Transformers.AliasToBean(typeof(InfoRequest)))
                .SetParameterList("personList", this.CheckListParameter(personIds))
                .SetParameterList("precardList", this.CheckListParameter(precardIds))
                .SetParameter("fromDate", date)
                .SetParameter("toDate", date);
            list = Query.List<InfoRequest>();

            return list;
        }
        #endregion

        #region Count

        /// <summary>
       /// تعداد آیتم کارتابل برای لیستی از مدیران
       /// kartal summry
       /// </summary>
       /// <param name="fromDate"></param>
       /// <param name="toDate"></param>
       /// <param name="requestType"></param>
       /// <param name="managerList">شامل شناسه مدیر و مدیرانی که جانشین آنها است</param>
       /// <returns></returns>
        public int GetAllManagerKartablItemsCount(DateTime fromDate, DateTime toDate, IList<decimal> managerList)
        {
            string SQLCommand = "";
            int resultCount = 0;


            #region Query
            SQLCommand = @"
declare @fromDate datetime,@toDate datetime

set @fromDate=:fromDate
set @toDate=:toDate


SELECT count(*)
FROM (SELECT * FROM TA_ManagerFlow where mngrFlow_Active=1  AND mngrFlow_ManagerID in (:managerList)
      ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID	
            Where Flow.flow_Deleted = 0		
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons						
INNER JOIN	(SELECT request_ID,request_FromDate,request_FromTime,request_ToDate,request_ToTime,request_TimeDuration,request_RegisterDate,request_PersonID ,request_PrecardID
					,request_Description,request_UserID,request_OperatorUser, request_AttachmentFile
             FROM TA_Request             
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId


WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT mngrFlow_ID 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )

         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
							 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 (reqStat_EndFlow = 1  OR reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
							 OR otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID)
			                )
			"; 
            #endregion

            IQuery Query = null;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);
            Query = Query.SetTimeout(120);
            if (Query != null)
            {
                Query = Query
                     .SetParameterList("managerList", managerList.ToArray())
                     .SetParameter("fromDate", fromDate)
                     .SetParameter("toDate", toDate);

                resultCount = Query.UniqueResult<int>();
            }
            return resultCount;

        }

        /// <summary>
        /// تعداد آیتم کارتابل برای جانشین
        /// دسترسی جانشین اعمال میگردد
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="substitutePersonId">شناسه جانشین</param>
        /// <returns></returns>
        public int GetAllSubstituteKartablItemsCount(DateTime fromDate, DateTime toDate, RequestType requestType, decimal substitutePersonId)
        {           
            string SQLCommand = "";
            int resultCount = 0;


            #region Query
            SQLCommand = @"
declare @fromDate datetime,@toDate datetime,@personId numeric
set @fromDate=:fromDate
set @toDate=:toDate
set @personId=:personId

SELECT Count(request_ID)
FROM (SELECT * FROM TA_ManagerFlow where mngrFlow_Active=1 
      ) MngFlow
INNER JOIN TA_Substitute as sub
ON sub_ManagerId=MngFlow.mngrFlow_ManagerID AND sub_PersonId=@personId
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
            Where Flow.flow_Deleted = 0			
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons
INNER JOIN	(SELECT * 
             FROM TA_Request   
             INNER JOIN 
             TA_Person  prs on prs.Prs_ID=request_PersonID AND prs.prs_Active=1 AND prs.prs_IsDeleted=0             
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID =  UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
    AND
   Req.request_RegisterDate>=sub.sub_FromDate
    AND
   Req.request_RegisterDate<=sub.sub_ToDate
   
INNER JOIN TA_SubstitutePrecardAccess 
ON subaccess_PrecardId=request_PrecardID AND subaccess_SubstituteId=sub.sub_ID  
WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT mngrFlow_ID 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID 
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	
			";
            #endregion

            IQuery Query = null;

            #region Request Types
            if (requestType == RequestType.Hourly)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else if (requestType == RequestType.Imperative)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.imperative);

            }
            else
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            }
            #endregion

            if (Query != null)
            {
                Query = Query
                     .SetParameter("personId", substitutePersonId)
                     .SetParameter("fromDate", fromDate)
                     .SetParameter("toDate", toDate);

                resultCount = Query.UniqueResult<int>();
            }
            return resultCount;

        }


        /// <summary>
        /// تعداد درخواست های کارتابل را برمیگرداند
        /// </summary>
        /// <param name="personIds">لیست افراد</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="requestType">نوع درخواست</param>       
        /// <returns></returns>
        public int GetAllManagerKartablCount(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, decimal[] quciSearchInUnderManagments)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
declare @managerID decimal(28,5),@fromDate datetime,@toDate datetime
set @managerID=:managerId
set @fromDate=:fromDate
set @toDate=:toDate


SELECT Count(request_ID)
FROM (SELECT * 
      FROM TA_ManagerFlow 
      WHERE mngrFlow_ManagerID = @managerID  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons
INNER JOIN	(SELECT * 
             FROM TA_Request             
             WHERE request_FromDate >= @fromDate 
                    AND 
                   request_ToDate <= @toDate 
            ) Req
ON Req.request_PersonID =  UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                        where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )

      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_EndFlow = 1 
			                  )	
			";

            IQuery Query = null;

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                SQLCommand += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            if (requestType == RequestType.Hourly)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                string condition = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else if (requestType == RequestType.Imperative)
            {
                string condition = @"    AND
                  request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";

                SQLCommand += condition;
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.imperative);

            }
            else
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            }

            if (Query != null)
            {
                Query = Query
                     .SetParameter("managerId", managerId)
                     .SetParameter("fromDate", fromDate)
                     .SetParameter("toDate", toDate);
                if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
                {
                    Query = Query
                         .SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));

                }

                resultCount = Query.UniqueResult<int>();
            }
            return resultCount;

        }

        /// <summary>
        /// تعداد درخواست های کارتابل برای جانشین را برمیگرداند
        /// </summary>
        /// <param name="personIds">لیست افراد</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="requestType">نوع درخواست</param>       
        /// <param name="managerId"> شناسه مدیر که جانشین هم هست</param>       
        /// <param name="personId">شناسه شخص که جانشین است</param>       
        /// <returns></returns>
        public int GetAllSubstituteKartablCount(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, decimal personId, decimal[] quciSearchInUnderManagments)
        {
            string SQLCommand = "";
            string extension = "";
            int resultCount = 0;


            #region Request Types
            if (requestType == RequestType.Hourly)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Hourly=1)";
            }
            else if (requestType == RequestType.Daily)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey<>:GroupName )
                                                    AND Precrd_Daily=1)";
            }
            else if (requestType == RequestType.OverWork)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";
            }
            else if (requestType == RequestType.Imperative)
            {
                extension = @"    AND
                  Req.request_PrecardID in ( SELECT Precrd_ID FROM TA_Precard 
							                WHERE Precrd_pshcardGroupID in
							                (
								                SELECT PishcardGrp_ID 
								                FROM TA_PrecardGroups 
								                WHERE PishcardGrp_IntLookupKey=:GroupName ))";
            } 
            #endregion

            if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
            {
                extension += @" AND
                  Req.request_PersonId in (:quciSearchInUnderManagments)";
            }

            SQLCommand = @"
declare @managerID decimal(28,5),@personId decimal(28,5),@fromDate datetime,@toDate datetime
set @managerID=:managerId
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate

select COUNT(*) from
(SELECT Req.request_ID
FROM (SELECT * 
      FROM TA_ManagerFlow where mngrFlow_Active=1
     ) MngFlow
INNER JOIN TA_Substitute as sub
ON sub_ManagerId=MngFlow.mngrFlow_ManagerID AND sub_PersonId=@personId
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons

INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN 
             TA_Person  prs on prs.Prs_ID=request_PersonID AND prs.prs_Active=1 AND prs.prs_IsDeleted=0
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId
    AND
   Req.request_RegisterDate>=sub.sub_FromDate
    AND
   Req.request_RegisterDate<=sub.sub_ToDate
   
INNER JOIN TA_SubstitutePrecardAccess 
ON subaccess_PrecardId=request_PrecardID AND subaccess_SubstituteId=sub.sub_ID   

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 (reqStat_MnagerFlowID = MngFlow.mngrFlow_ID OR reqStat_EndFlow = 1)
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
/*			                  
      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_EndFlow = 1 AND reqStat_RequestId = Req.request_ID 
			                 
			                  )		*/
"
+ extension +
@" 
			                
-----------------------------------------------------------------------							
UNION
-----------------------------------------------------------------------
SELECT Req.request_ID
FROM (SELECT * 
      FROM TA_ManagerFlow
      WHERE mngrFlow_ManagerID=@ManagerID  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons

INNER JOIN	(SELECT * 
             FROM TA_Request 
             INNER JOIN 
             TA_Person  prs on prs.Prs_ID=request_PersonID AND prs.prs_Active=1 AND prs.prs_IsDeleted=0
              WHERE 
					isnull(request_EndFlow ,0) = 0
					AND
					(( request_FromDate >= @fromDate
                    AND 
                   request_FromDate <= @toDate )
                   OR
                   ( request_ToDate >= @fromDate
                    AND 
                   request_ToDate <= @toDate ))
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                        where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 reqStat_MnagerFlowID = MngFlow.mngrFlow_ID
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
" + extension +
@" 
) as resut


			";

            IQuery Query = null;


            if (requestType == RequestType.Hourly)
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.Daily)
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                .SetParameter("GroupName", (int)PrecardGroupsName.overwork);
            }
            else if (requestType == RequestType.OverWork)
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.overwork);

            }
            else if (requestType == RequestType.Imperative)
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                 .SetParameter("GroupName", (int)PrecardGroupsName.imperative);

            }
            else
            {
                Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);
            }

            if (Query != null)
            {
                Query = Query
                     .SetParameter("managerId", managerId)
                     .SetParameter("personId", personId)
                     .SetParameter("fromDate", fromDate)
                     .SetParameter("toDate", toDate);
                if (quciSearchInUnderManagments != null && quciSearchInUnderManagments.Length > 0)
                {
                    Query = Query
                         .SetParameterList("quciSearchInUnderManagments", base.CheckListParameter(quciSearchInUnderManagments));

                }
                resultCount = Query.UniqueResult<int>();
            }
            return resultCount;

        }

        /// <summary>
        /// تعداد درخواست های کارتابل برای تنها جانشین را برمیگرداند
        /// یعنی اگر شخص مدیر باشد کاری ندارد و همچنین تنها جانشین یک مدیر 
        /// را پشتیبانی میکند
        /// جهت نمایش در خلاصه کارتابل
        /// </summary>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="managerId"> شناسه مدیر که جانشین هم هست</param>       
        /// <param name="personId">شناسه شخص که جانشین است</param>       
        /// <returns></returns>
        public int GetAllSubstituteKartablCount(DateTime fromDate, DateTime toDate, decimal managerId, decimal personId)
        {
            string SQLCommand = "";
            int resultCount = 0;

            SQLCommand = @"
declare @managerID decimal(28,5),@personId decimal(28,5),@fromDate datetime,@toDate datetime
set @managerID=:managerId
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate

SELECT COUNT(*)
FROM (SELECT * 
      FROM TA_ManagerFlow
      WHERE mngrFlow_ManagerID=@ManagerID  AND mngrFlow_Active=1
     ) MngFlow
CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (MngFlow.mngrFlow_FlowID) as UndermanagmentsPersons
INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
		    FROM TA_Flow Flow
			INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
			ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID			
            Where Flow.flow_Deleted = 0
		   ) AS UnderMgn
ON MngFlow.mngrFlow_FlowID = UnderMgn.Flow_Id							
--CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID) as UndermanagmentsPersons

INNER JOIN	(SELECT request_ID,request_PersonID,request_PrecardID
             FROM   (select * FROM TA_Substitute  WHERE sub_ManagerId=@managerID AND sub_PersonId=@personId ) as subs
             JOIN TA_SubstitutePrecardAccess ON subs.sub_ID=subaccess_SubstituteId 
             JOIN TA_Request as reqq ON request_PrecardID=subaccess_PrecardId 
             INNER JOIN 
             TA_Person  prs on prs.Prs_ID=reqq.request_PersonID AND prs.prs_Active=1 AND prs.prs_IsDeleted=0
             WHERE 
                    isnull(request_endflow,0) = 0 AND
                    (( request_FromDate >= @fromDate
                                        AND 
                                      request_FromDate <= @toDate )
                                       OR
                                    ( request_ToDate >= @fromDate
                                        AND 
                                      request_ToDate <= @toDate ))
                   AND
                   request_RegisterDate>=subs.sub_FromDate
				   AND
				   request_RegisterDate<=subs.sub_ToDate
            ) Req
ON Req.request_PersonID = UndermanagmentsPersons.prs_id
    AND
   Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId

WHERE (Req.request_ID IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts
		                  INNER JOIN (SELECT * 
					                  FROM TA_ManagerFlow 
					                  WHERE mngrFlow_FlowID = MngFlow.mngrFlow_FlowID
							                 AND
						                    mngrFlow_Level = MngFlow.mngrFlow_Level - 1
						             ) InnerMngFlow
		                  ON ReqSts.reqStat_MnagerFlowID = InnerMngFlow.mngrFlow_ID					 							   
				                AND
			                 ReqSts.reqStat_Confirm = 1
                            where ReqSts.reqStat_RequestID is not null
						 )
         OR
       MngFlow.mngrFlow_Level = 1
      )
         AND
      Req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID AND
			                 (reqStat_MnagerFlowID = MngFlow.mngrFlow_ID OR reqStat_EndFlow = 1)
			                )
        AND
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 JOIN TA_ManagerFlow as otherMngFlw on otherMngFlw.mngrFlow_ID=reqStat_MnagerFlowID
								    WHERE reqStat_RequestID is not null and reqStat_RequestId = Req.request_ID 
									AND otherMngFlw.mngrFlow_FlowID <> MngFlow.mngrFlow_FlowID
			                  )
    /*			                  
      AND/*changed manager flow*/
	req.request_ID NOT IN (SELECT reqStat_RequestID
			                 FROM TA_RequestStatus 
			                 Where reqStat_RequestID is not null and reqStat_EndFlow = 1 AND reqStat_RequestId = Req.request_ID 
			                 
			                  )		*/
			";

            IQuery Query = null;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand);


            if (Query != null)
            {
                Query = Query
                     .SetParameter("managerId", managerId)
                     .SetParameter("personId", personId)
                     .SetParameter("fromDate", fromDate)
                     .SetParameter("toDate", toDate);

                resultCount = Query.UniqueResult<int>();
            }
            return resultCount;

        }

        /// <summary>
        /// تعداد درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public int GetAllUserRequestCount(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal personId)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
declare @personId decimal(28,5),@fromDate datetime,@toDate datetime
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate


SELECT COUNT(*) 
							 FROM TA_Request Req
                             INNER JOIN 
                             TA_Person  prs on prs.Prs_ID=request_PersonID AND prs.prs_Active=1 AND prs.prs_IsDeleted=0
                             Inner join TA_Precard on Precrd_ID=request_PrecardID
                             Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       
							 WHERE request_PersonID=:personId AND precardGroup.PishcardGrp_LookupKey<>'imperative' AND
                                    (( request_FromDate >= @fromDate
                                        AND 
                                      request_FromDate <= @toDate )
                                       OR
                                    ( request_ToDate >= @fromDate
                                        AND 
                                      request_ToDate <= @toDate ))
                                 AND precardGroup.PishcardGrp_LookupKey<>'imperative'	";

            IQuery Query = null;

            if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_IsDeleted=1
                                    )
                                    ";

                SQLCommand += condition;
            }

            else if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_IsDeleted=1)                                    
                                    )
                                    ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	AND request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_Confirm=0
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=Req.request_ID AND reqStat_IsDeleted=1)                                    
                                    )
                                    ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnderReview)
            {
                string condition = @"	AND request_ID not in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=Req.request_ID
                                    AND reqStat_EndFlow=1
                                    )";

                SQLCommand += condition;
            }


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("personId", personId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);
            resultCount = Query.UniqueResult<int>();

            return resultCount;

        }

        /// <summary>
        /// تعداد درخواستها در فیلتر گزارشات ثبت شده
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="submiter"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public int GetAllUserRequestCount(DateTime? fromDate, DateTime? toDate, RequestType? requestType, RequestSubmiter? submiter, decimal personId)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
declare @personId decimal(28,5),@fromDate datetime,@toDate datetime
set @personId=:personId
set @fromDate=:fromDate
set @toDate=:toDate


SELECT COUNT(*) 
							 FROM TA_Request Req
                             join TA_Precard on Precrd_ID=request_PrecardID
                             join TA_PrecardGroups precardGroup on PishcardGrp_ID=Precrd_pshcardGroupID
							 WHERE request_PersonID=@personId AND precardGroup.PishcardGrp_LookupKey<>'imperative'

		               ";

            IQuery Query = null;

            if (fromDate != null)
            {
                string condition = @"	AND 
								     request_FromDate >= @fromDate 
                                    ";

                SQLCommand += condition;
            }

            if (toDate != null)
            {
                string condition = @"	AND 
								     request_ToDate <= @toDate 
                                    ";

                SQLCommand += condition;
            }
            if (requestType == RequestType.OverWork)
            {
                string condition = @"	AND 
                                        PishcardGrp_LookupKey = '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"	AND 
                                        Precrd_Daily = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Hourly)
            {
                string condition = @"	AND 
                                        Precrd_Hourly = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("personId", personId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);
            resultCount = Query.UniqueResult<int>();

            return resultCount;
        }
   
        /// <summary>
        /// تعداد درخواستهای ثبت شده اپراتور را برمیگرداند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestStatus"></param>
        /// <param name="personId">شناسه پرسنلی اپراتور</param>
        /// <param name="userId">شناسه کاربری اپراتور</param>
        /// <returns></returns>
        public int GetAllOperatorRequestCount(DateTime fromDate, DateTime toDate, RequestState requestStatus, decimal personId)
        {
            string SQLCommand = "";
            int resultCount = 0;

            SQLCommand = @"
declare @prsID decimal(28,5),@fromDate datetime,@toDate datetime
set @prsID=:personId
set @fromDate=:fromDate
set @toDate=:toDate

SELECT COUNT(*)
FROM 
	(
	    Select * From 
	                 (
	                    SELECT distinct req.*  
	                    FROM 	    
		                    (SELECT opr_FlowId 
		                     FROM TA_Operator 
		                     WHERE opr_PersonId = @prsID  AND opr_Active=1
		                    ) opr
		                INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
				                    FROM TA_Flow Flow
				                    INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
				                    ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
                                    Where Flow.flow_Deleted = 0			
			                       ) AS UnderMgn
		                ON opr.opr_FlowId = UnderMgn.Flow_Id							
		                CROSS APPLY 
		                (
			               select Prs_Id from (
			               select * from [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID)
			            ) unders
		             ) UndermanagmentsPersons

	             	   INNER JOIN (SELECT * 
					               FROM TA_Request 
					               WHERE request_FromDate >= @fromDate 
						           AND 
					               request_ToDate <= @toDate 
				                  ) Req
		               ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
		               AND Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId  
        ) req 
	    union
	    SELECT * 
		         FROM TA_Request 
			     WHERE request_FromDate >= @fromDate 
			           AND 
			           request_ToDate <= @toDate 
					   AND 
					   request_PersonID = @prsID				
   ) oprRequests             
 Inner join TA_Precard on Precrd_ID=oprRequests.request_PrecardID
 Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       	
WHERE request_FromDate >= @fromDate
	AND request_ToDate <= @toDate
	AND precardGroup.PishcardGrp_LookupKey<>'imperative'   
";

            IQuery Query = null;

            if (requestStatus == RequestState.Deleted)
            {
                string condition = @"	AND oprRequests.request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_IsDeleted=1
                                    )
                                    ";

                SQLCommand += condition;
            }

            else if (requestStatus == RequestState.Confirmed)
            {
                string condition = @"	AND oprRequests.request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_Confirm=1
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID AND reqStat_IsDeleted=1)                                    
                                    )
                                    ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.Unconfirmed)
            {
                string condition = @"	AND oprRequests.request_ID in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_Confirm=0
                                    AND reqStat_EndFlow=1
                                    AND not exists(select * from TA_RequestStatus where reqStat_RequestID=oprRequests.request_ID AND reqStat_IsDeleted=1)                                    
                                    )
                                    ";

                SQLCommand += condition;
            }
            else if (requestStatus == RequestState.UnderReview)
            {
                string condition = @"	AND oprRequests.request_ID not in 
                                    (
                                    select reqStat_RequestID from TA_RequestStatus 
                                    where 
                                    reqStat_RequestID=oprRequests.request_ID
                                    AND reqStat_EndFlow=1
                                    )";

                SQLCommand += condition;
            }


            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("personId", personId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);
            resultCount = Query.UniqueResult<int>();

            return resultCount;

        }

        /// <summary>
        /// تعداد درخواستهای اپراتور در فیلتر گزارشات ثبت شده
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="requestType"></param>
        /// <param name="submiter"></param>
        /// <param name="personId">شناسه پرسنلی اپراتور</param>
        /// <param name="userId">شناسه کاربری اپراتور</param>
        /// <param name="underManagmentPersonId">شناسه پرسنل تحت مدیریت یعنی فقط برای همین شخص فیلتر شود</param>
        /// <returns></returns>
        public int GetAllOperatorRequestCount(DateTime? fromDate, DateTime? toDate, RequestType? requestType, RequestSubmiter? submiter, decimal personId, decimal userId, decimal underManagmentPersonId)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
declare @prsID decimal(28,5),@fromDate datetime,@toDate datetime
set @prsID=:personId
set @fromDate=:fromDate
set @toDate=:toDate

SELECT COUNT(*)
FROM 
	(
	    Select * From 
	                 (
	                    SELECT distinct req.*  
	                    FROM 	    
		                    (SELECT opr_FlowId 
		                     FROM TA_Operator 
		                     WHERE opr_PersonId = @prsID  AND opr_Active=1
		                    ) opr
		                INNER JOIN (SELECT Flow.Flow_ID, PrcAccessGrpDtl.accessGrpDtl_PrecardId,Flow_FlowName
				                    FROM TA_Flow Flow
				                    INNER JOIN TA_PrecardAccessGroupDetail  PrcAccessGrpDtl
				                    ON PrcAccessGrpDtl.accessGrpDtl_AccessGrpId = Flow.Flow_AccessGroupID
                                    Where Flow.flow_Deleted = 0			
			                       ) AS UnderMgn
		                ON opr.opr_FlowId = UnderMgn.Flow_Id							
		                CROSS APPLY 
		                (
			               select Prs_Id from (
			               select * from [dbo].[TA_GetUnderManagmentPersons] (UnderMgn.Flow_ID)
			            ) unders
		             ) UndermanagmentsPersons

	             	   INNER JOIN (SELECT * 
					               FROM TA_Request 
					               WHERE request_FromDate >= @fromDate 
						           AND 
					               request_ToDate <= @toDate 
				                  ) Req
		               ON Req.request_PersonID = UndermanagmentsPersons.prs_id				
		               AND Req.request_PrecardID = UnderMgn.accessGrpDtl_PrecardId ";
                       if(underManagmentPersonId > 0)
                           SQLCommand += @"AND
                                           Req.request_PersonID = :underManagmentPersonId ";
        SQLCommand +=@"                                                                             
        ) req ";
        if(underManagmentPersonId <= 0)
           SQLCommand += @"
	                       union
	                       SELECT * 
		                            FROM TA_Request 
			                        WHERE request_FromDate >= @fromDate 
			                        AND 
			                        request_ToDate <= @toDate 
					                AND 
					                request_PersonID = @prsID ";
		SQLCommand += @"	
   ) oprRequests             
INNER JOIN TA_Person as prs on prs.Prs_ID = oprRequests.request_PersonID AND prs.prs_IsDeleted=0
Inner join TA_Precard on Precrd_ID=oprRequests.request_PrecardID
Inner join TA_PrecardGroups precardGroup on Precrd_pshcardGroupID=precardGroup.PishcardGrp_ID                                       	
WHERE precardGroup.PishcardGrp_LookupKey<>'imperative'        
		               ";

            IQuery Query = null;

            if (submiter != null)
            {
                string condition = "";
                if (submiter == RequestSubmiter.USER)
                {
                    condition = @"	AND 
								     request_userId = :userId 
                                    ";
                }
                else
                {
                    condition = @"	AND 
								     request_UserId <> :userId 
                                    ";
                }
                SQLCommand += condition;
            }
            if (requestType == RequestType.OverWork)
            {
                string condition = @"	AND 
                                        PishcardGrp_LookupKey = '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Daily)
            {
                string condition = @"	AND 
                                        Precrd_Daily = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }
            else if (requestType == RequestType.Hourly)
            {
                string condition = @"	AND 
                                        Precrd_Hourly = 1 AND
                                        PishcardGrp_LookupKey <> '" + PrecardGroupsName.overwork.ToString() + "'";

                SQLCommand += condition;
            }

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
               .SetParameter("personId", personId);

            Query.SetParameter("fromDate", fromDate);
            Query.SetParameter("toDate", toDate);
            if (underManagmentPersonId > 0)
                Query.SetParameter("underManagmentPersonId", underManagmentPersonId);
            if (submiter != null)
                Query.SetParameter("userId", userId);


            resultCount = Query.UniqueResult<int>();

            return resultCount;
        }

        /// <summary>
        /// تعداد درخواستهای ثبت شده که حذف نشده اند - جهت استفاده در اعتبار سنجی واسط کاربر
        /// </summary>
        /// <returns></returns>
        public int GetActiveRequestCount(decimal personId, decimal precardId, DateTime fromDate, DateTime toDate)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
                            declare @personId decimal(28,5),@fromDate datetime,
                            @toDate datetime,@preCardId decimal (28,5)
                            set @personId=:personId
                            set @fromDate=:fromDate
                            set @toDate=:toDate
                            set @preCardId=:precardId

                            SELECT COUNT(*) 
							FROM TA_Request Req
							WHERE 
							    request_PersonID=@personId
									AND							
								request_PrecardID = @preCardId
									AND							
								(( request_FromDate >= @fromDate
                                        AND 
                                      request_FromDate <= @toDate )
                                       OR
                                    ( request_ToDate >= @fromDate
                                        AND 
                                      request_ToDate <= @toDate ))
								    AND
								    
(Req.request_ID NOT IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts		                  
		                  WHERE ReqSts.reqStat_RequestID is not null and ReqSts.reqStat_RequestID=Req.request_ID
									AND
								ReqSts.reqStat_IsDeleted=1 ))
AND
								    
(Req.request_ID NOT IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts		                  
		                  WHERE ReqSts.reqStat_RequestID is not null and ReqSts.reqStat_RequestID=Req.request_ID
									AND
								ReqSts.reqStat_confirm=0 ))				
		               ";

            IQuery Query = null;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("personId", personId)
            .SetParameter("precardId", precardId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);
            resultCount = Query.UniqueResult<int>();

            return resultCount;
        }

        public int GetOperatorActiveRequestCount(decimal userID, UIValidationGroup uiValidationGroup, decimal precardID, DateTime fromDate, DateTime toDate)
        {           
            ISession NHSession = NHibernateFramework.NHibernateSessionManager.Instance.GetSession();
            Request requestAlias = null;
            RequestStatus requestStatusAlias = null;
            Precard precardAlias = null;
            UIValidationGroup uiValidationGroupAlias = null;
            Person personAlias = null;
            PersonTASpec personTASpecAlias = null;
            User userAlias = null;
            

            var DeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                        .Where(() => requestStatusAlias.EndFlow == true)
                                                        .And(() => requestStatusAlias.IsDeleted)
                                                        .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                        .Select(x => x.ID);
            int count = NHSession.QueryOver(() => requestAlias)
                                 .JoinAlias(() => requestAlias.Precard, () => precardAlias.ID)
                                 .JoinAlias(() => requestAlias.Person, () => personAlias)
                                 .JoinAlias(() => personAlias.PersonTASpecList, () => personTASpecAlias)
                                 .JoinAlias(() => personTASpecAlias.UIValidationGroup, () => uiValidationGroupAlias)
                                 .JoinAlias(() => requestAlias.User, () => userAlias)
                                 .Where(() => userAlias.ID == userID &&
                                              (
                                                (requestAlias.FromDate >= fromDate &&
                                                 requestAlias.FromDate <= toDate) ||
                                                (requestAlias.ToDate >= fromDate &&
                                                 requestAlias.ToDate <= toDate)
                                              ) &&
                                              uiValidationGroupAlias.ID == uiValidationGroup.ID
                                       )
                                 .WithSubquery
                                 .WhereNotExists(DeletedRequestStatusSubQuery)
                                 .RowCount();
            return count;                                 
        }

        /// <summary>
        /// مجموع مقدار درخواستهای ثبت شده که حذف نشده اند - جهت استفاده در اعتبار سنجی واسط کاربر
        /// </summary>
        /// <returns></returns>
        public int GetActiveRequestDateValues(decimal personId, decimal precardId, DateTime fromDate, DateTime toDate)
        {
            string SQLCommand = "";
            int resultCount = 0;


            SQLCommand = @"
                            declare @personId decimal(28,5),@fromDate datetime,
                            @toDate datetime,@preCardId decimal (28,5)
                            set @personId=:personId
                            set @fromDate=:fromDate
                            set @toDate=:toDate
                            set @preCardId=:precardId

                            SELECT sum(DATEPART(day, request_ToDate-request_FromDate))
							FROM TA_Request Req
							WHERE 
							    request_PersonID=@personId
									AND							
								request_PrecardID = @preCardId
									AND							
								(( request_FromDate >= @fromDate
                                        AND 
                                      request_FromDate <= @toDate )
                                       OR
                                    ( request_ToDate >= @fromDate
                                        AND 
                                      request_ToDate <= @toDate ))
								    AND
								    
(Req.request_ID NOT IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts		                  
		                  WHERE ReqSts.reqStat_RequestID is not null and ReqSts.reqStat_RequestID=Req.request_ID
									AND
								ReqSts.reqStat_IsDeleted=1 ))
AND
								    
(Req.request_ID NOT IN (SELECT ReqSts.reqStat_RequestID
		                  FROM TA_RequestStatus ReqSts		                  
		                  WHERE ReqSts.reqStat_RequestID is not null and ReqSts.reqStat_RequestID=Req.request_ID
									AND
								ReqSts.reqStat_confirm=0 ))
				
		               ";

            IQuery Query = null;

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("personId", personId)
            .SetParameter("precardId", precardId)
            .SetParameter("fromDate", fromDate)
            .SetParameter("toDate", toDate);
            resultCount = Query.UniqueResult<int>();

            return resultCount;
        }


        #endregion

        /// <summary>
        /// درخواستهای یک شخص دریک بازه را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="precardId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<GTS.Clock.Model.RequestFlow.Request> GetAllUsedRequest(decimal personId, decimal precardId, DateTime fromDate, DateTime toDate)
        {
            string HQLCommand = @"SELECT req FROM Request as req                                
                                WHERE req.Person.ID=:personId
                                AND req.Precard.ID=:precardId
                                AND req.ToDate>=:fromDate
                                AND req.ToDate<=:toDate
                                AND req.ID IN (SELECT stat.Request.ID FROM RequestStatus as stat WHERE stat.Confirm=1 AND stat.IsDeleted=0)";
            IList<GTS.Clock.Model.RequestFlow.Request> list = base.NHibernateSession.CreateQuery(HQLCommand)
                               .SetParameter("personId", personId)
                               .SetParameter("precardId", precardId)
                               .SetParameter("fromDate", fromDate)
                               .SetParameter("toDate", toDate)
                               .List<GTS.Clock.Model.RequestFlow.Request>();
            return list;
        }

        public void GetAllKartablData(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            ManagerFlow managerFlowAlias = null; ManagerFlow otherManagerFlowAlias = null;
            Manager managerAlias = null;
            Flow flowAlias = null;
            PrecardAccessGroup precardAccessGroupAlias = null;
            GTS.Clock.Model.RequestFlow.Request requestAlias = null;
            Person personAlias = null;
            RequestStatus requestStatusAlias = null;            

            IList<ManagerFlow> ManagerFlowList = NHibernateSession.QueryOver<ManagerFlow>(() => managerFlowAlias)
                                                 .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                                 .Where(() => managerAlias.ID == managerId)
                                                 .Clone()
                                                 .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                                 .Where(() => flowAlias.IsDeleted == false)
                                                 .Clone()
                                                 .JoinAlias(() => flowAlias.AccessGroup, () => precardAccessGroupAlias)
                                                 .List<ManagerFlow>();

            IList<Person> UnderManagmentPersonnelList = new List<Person>();
            foreach (ManagerFlow managerFlowItem in ManagerFlowList)
            {
                foreach (UnderManagment underManagmentItem in managerFlowItem.Flow.UnderManagmentList.Where(underManagment => underManagment.Contains && underManagment.Person != null && underManagment.Department == null).ToList<UnderManagment>())
                {
                    if (UnderManagmentPersonnelList.All(personnel => personnel.ID != underManagmentItem.Person.ID))
                        UnderManagmentPersonnelList.Add(underManagmentItem.Person);
                }

                foreach (UnderManagment underManagmentItem in managerFlowItem.Flow.UnderManagmentList.Where(underManagment => underManagment.Contains && underManagment.Person == null && underManagment.Department != null && underManagment.ContainInnerChilds == false).ToList<UnderManagment>())
                {
                    this.InsertUnderManagementPersonnelOverDepartment(ref UnderManagmentPersonnelList, managerFlowItem.Flow.UnderManagmentList, underManagmentItem.Department.PersonList);
                }

                foreach (UnderManagment underManagmentItem in managerFlowItem.Flow.UnderManagmentList.Where(underManagment => underManagment.Contains && underManagment.Person == null && underManagment.Department != null && underManagment.ContainInnerChilds == true).ToList<UnderManagment>())
                {
                    IList<Department> DepartmentsList = new List<Department>();
                    this.GetAllRelativeUnderManagementDepartments(ref DepartmentsList, underManagmentItem.Department);
                    foreach (Department department in DepartmentsList)
                    {
                        this.InsertUnderManagementPersonnelOverDepartment(ref UnderManagmentPersonnelList, managerFlowItem.Flow.UnderManagmentList, department.PersonList);
                    }
                }

                IQueryOver<GTS.Clock.Model.RequestFlow.Request, GTS.Clock.Model.RequestFlow.Request> BaseRequestIQueryOver = NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>(() => requestAlias)
                                                                .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                                .WhereRestrictionOn(() => personAlias.ID)
                                                                .IsIn(UnderManagmentPersonnelList.Select(x => x.ID).ToArray())
                                                                .Where(() => (requestAlias.FromDate >= fromDate && requestAlias.FromDate <= toDate) || (requestAlias.ToDate >= fromDate && requestAlias.ToDate <= toDate))
                                                                .OrderBy(() => requestAlias.FromDate).Desc
                                                                .ThenBy(() => requestAlias.FromTime).Desc
                                                                .ThenBy(() => requestAlias.ID).Desc;
                if (managerFlowItem.Level != 1)
                    BaseRequestIQueryOver = BaseRequestIQueryOver.WhereRestrictionOn(() => requestAlias.ID)
                                                                 .IsIn(NHibernateSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                 .JoinAlias(() => requestStatusAlias.ManagerFlow, () => managerFlowAlias)
                                                                 .Where(() => managerFlowAlias.ID == managerFlowItem.ID && managerFlowAlias.Level == managerFlowItem.Level - 1 && requestStatusAlias.Confirm)
                                                                 .Select(x => x.ID)
                                                                 .List<decimal>()
                                                                 .ToArray());
                IList<GTS.Clock.Model.RequestFlow.Request> RequestsList = BaseRequestIQueryOver.WhereRestrictionOn(() => requestAlias.ID).Not
                                                                   .IsIn(NHibernateSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                                          .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                                                          .Clone()
                                                                                          .JoinAlias(() => requestStatusAlias.ManagerFlow, () => managerFlowAlias)
                                                                                          .Select(requestStatus => requestStatus.ID)
                                                                                          .List<decimal>()
                                                                                          .ToArray())
                                                                    .AndRestrictionOn(() => requestAlias.ID).Not
                                                                    .IsIn(NHibernateSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                                           .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                                                           .Clone()
                                                                                           .JoinAlias(() => requestStatusAlias.ManagerFlow, () => otherManagerFlowAlias)
                                                                                           .Where(() => otherManagerFlowAlias.ID != managerFlowItem.ID)
                                                                                           .Clone()
                                                                                           .Select(requestStatus => requestStatus.ID)
                                                                                           .List<decimal>()
                                                                                           .ToArray())
                                                                    .AndRestrictionOn(() => requestAlias.ID).Not
                                                                    .IsIn(NHibernateSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                                           .JoinQueryOver(() => requestStatusAlias.Request, () => requestAlias)
                                                                                           .Where(() => requestStatusAlias.EndFlow)
                                                                                           .Select(requestStatus => requestStatus.ID)
                                                                                           .List<decimal>()
                                                                                           .ToArray())
                                                                    .Skip(pageIndex * pageSize)
                                                                    .Take(pageSize)
                                                                    .List<GTS.Clock.Model.RequestFlow.Request>();
            }
        }

       

        private void InsertUnderManagementPersonnelOverDepartment(ref IList<Person> MasterUnderManagementPersonnelList, IList<UnderManagment> FlowUnderManagementPersonnelList, IList<Person> PersonnelList)
        {
            foreach (Person person in PersonnelList)
            {
                if (MasterUnderManagementPersonnelList.All(personnel => personnel.ID != person.ID) && this.CheckPersonnelIsContainsInUnderManagement(FlowUnderManagementPersonnelList, person.ID))
                    MasterUnderManagementPersonnelList.Add(person);
            }
        }

        private void GetAllRelativeUnderManagementDepartments(ref IList<Department> DepartmentsList, Department ParentDepartment)
        {
            DepartmentsList.Add(ParentDepartment);
            foreach (Department department in ParentDepartment.ChildList)
            {
                this.GetAllRelativeUnderManagementDepartments(ref DepartmentsList, department);
            }
        }

        private bool CheckPersonnelIsContainsInUnderManagement(IList<UnderManagment> UnderManagementList, decimal PersonnelID)
        {
            bool IsContains = true;
            foreach (UnderManagment underManagementItem in UnderManagementList.Where(underManagement => !underManagement.Contains && underManagement.Person != null))
            {
                if (underManagementItem.Person.ID == PersonnelID)
                {
                    IsContains = false;
                    break;
                }
            }
            return IsContains;
        }


        #endregion

        public void DeleteRequestAttachment(string path)
        {
            if (path != null && path != string.Empty && File.Exists(path))
                File.Delete(path);
        }

        public Request GetRequestAttachmentByID(decimal requestID)
        {
            return this.GetById(requestID, false);
        }
    }
}
