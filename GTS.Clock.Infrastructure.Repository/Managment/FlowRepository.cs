using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;


namespace GTS.Clock.Infrastructure.Repository
{
    public class FlowRepository : RepositoryBase<Flow>, IFlowRepository
    {
        public override string TableName
        {
            get { return "TA_Flow"; }
        }

        public FlowRepository(bool disconectly) 
            : base(disconectly) 
        { }

        public IList<UnderManagment> GetAllUnderManagments(decimal flowId)
        {
            string HQLCommand = @"select unmngt  from UnderManagment as unmngt inner join
                                  unmngt.Flow as flw                                   
                                  where flw.ID=:flowId AND flw.IsDeleted = 0";
            IList<UnderManagment> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flowId", flowId)
                .List<UnderManagment>();
            return list;
        }

        /// <summary>
        /// مدیران جریان را غیر فعال میکند
        /// </summary>
        /// <param name="flowId"></param>
        public void DeleteManagerFlows(decimal flowId) 
        {
            string HQLCommand = @"UPDATE ManagerFlow as mngrFlow
                                  SET mngrFlow_Active=0
                                  where mngrFlow.Flow.ID=:flowId";
            base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flowId", flowId)
                .ExecuteUpdate();
        }

      

        /// <summary>
        /// آیا اگر این مدیر حذف شود جریانی وجود دارد هیچ مدیری داشته باشد
        /// </summary>
        /// <param name="managerFlowId"></param>
        /// <returns></returns>
        public int GetFlowManagerCount(decimal managerFlowId)
        {
            string HQLCommand = @"select COUNT(*) from ManagerFlow where Flow.IsDeleted=0 AND Flow.ID in (
                                select Flow.ID from ManagerFlow where Manager.ID in(
                                select Manager.ID from ManagerFlow where ID= :managerFlowId))
                                group by Flow.ID";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                 .SetParameter("managerFlowId", managerFlowId)
                 .List<object>().FirstOrDefault();
            return Utility.Utility.ToInteger(count == null ? "0" : count.ToString());
        }

        public IList<Flow> GetAllByAccessGroupName(string name,decimal[] restrictionIds) 
        {
            if (restrictionIds == null || restrictionIds.Length == 0)
                return new List<Flow>();
            string HQLCommand = @"select flw from Flow as flw
                                inner join flw.AccessGroup as access
                                where access.Name like :accessName
                                AND flw.IsDeleted=0 AND flw.ID in (:ids)";

            IList<Flow> list = base.NHibernateSession.CreateQuery(HQLCommand)
                 .SetParameter("accessName", String.Format("%{0}%", name))
                 .SetParameterList("ids", base.CheckListParameter(restrictionIds))
                 .List<Flow>();
            return list;
        }

        /// <summary>
        /// تعداد پیشکارتهای یک جریان که توسط یک جانشین قابل دسترس است را برمیگرداند
        /// </summary>
        /// <param name="substituteId"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public int GetSubstituteAccessGroupCount(decimal substituteId,decimal flowId) 
        {
            string SQLCommand = @"select COUNT(*) from TA_Flow
                                    Inner join TA_PrecardAccessGroupDetail on Flow_AccessGroupID = accessGrpDtl_AccessGrpId
                                    Inner join TA_SubstitutePrecardAccess on subaccess_PrecardId = accessGrpDtl_PrecardId
                                    Inner join TA_Substitute on sub_ID=subaccess_SubstituteId
                                    where Flow_Deleted=0 AND sub_ID=:substituteId and Flow_ID=:flowId";
            object count = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                 .SetParameter("substituteId", substituteId)
                 .SetParameter("flowId", flowId)
                 .List<object>().FirstOrDefault();
            return Utility.Utility.ToInteger(count == null ? "0" : count.ToString());
        }
    }
}
