using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Concepts;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class WorkGroupRepository : RepositoryBase<WorkGroup>,IWorkGroupRepository
    {
        public override string TableName { get { return "TA_WorkGroup"; } }
       
        public WorkGroupRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }

        #region IWorkGroupRepository

        public IList<WorkGroup> SearchByName(string workgroupName) 
        {
            throw new NotImplementedException();
        }

        public bool UsedByPerson(decimal workGroupId)
        {
            string sqlQuery = "select COUNT(*) from  TA_AssignWorkGroup where AsgWorkGroup_WorkGroupId=:workgroupID";
            IQuery query = base.NHibernateSession.CreateSQLQuery(sqlQuery)
                .SetParameter("workgroupID", workGroupId);
            int count = query.UniqueResult<int>();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// کلیه انتسابات شیفت به گروه کاری را در یک بازه حذف میکند
        /// </summary>
        /// <param name="workGroupId"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        public void DeleteWorkGroupDetail(decimal workGroupId, DateTime minDate, DateTime maxDate)
        {
            string hqlQuery = @"delete from WorkGroupDetail where WorkGroup=:workgroup
                                and Date>=:minDate
                                and Date<=:maxDate";
            IQuery query = base.NHibernateSession.CreateQuery(hqlQuery)
                .SetParameter("workgroup", new WorkGroup() { ID = workGroupId })
                .SetParameter("minDate", minDate)
                .SetParameter("maxDate", maxDate);
            query.ExecuteUpdate();
        }

        public IList<WorkGroup> GetAllWorkGroupByShift(decimal shiftId)
        {
            String HQLCommand = @"from WorkGroup where ID in 
                                  (select WorkGroup from WorkGroupDetail
                                  where Shift.ID = :shiftId)";
            /*
            String sqlCommand = @"select WorkGroup_ID,WorkGroup_Name,WorkGroup_CustomCode from TA_WorkGroup wokgrp
                                  where WorkGroup_ID in (select WorkGroupDtl_WorkGroupId from TA_WorkGroupDetail 
                                  where WorkGroupDtl_ShiftId=:shiftId)";
            */
            IList<WorkGroup> result = NHibernateSession.CreateQuery(HQLCommand)
                //.SetResultTransformer(Transformers.AliasToBean(typeof(WorkGroup)))
                .SetParameter("shiftId", shiftId)
                .List<WorkGroup>();
            return result;
        }
      
        #endregion

    }
}
