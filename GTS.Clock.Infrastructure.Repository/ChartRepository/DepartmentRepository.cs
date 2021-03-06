using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Infrastructure.Repository
{
    public class DepartmentRepository : RepositoryBase<GTS.Clock.Model.Charts.Department>, IDepartmentRepository
    {
        public override string TableName
        {
            get { return "TA_Department"; }
        }

        public DepartmentRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
      
        #region Model Interface

        public IList<Department> GetDepartmentTree()
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(Department));
            crit.Add(Expression.Or(
                Expression.IsNull("Parent"),
                Expression.Eq("Parent.ID", Convert.ToDecimal(0))));

            IList<Department> parents = crit.List<Department>();            

            return parents;
        }

        public decimal GetParentID(decimal departmentID)
        {
            string SQLCommand = String.Format("SELECT dep_ParentID FROM TA_Department " +
                                                "WHERE dep_ID = {0} ", departmentID);

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            object parentID = query.List<object>().FirstOrDefault();
            if (parentID != null)
                return (decimal)parentID;
            return 0;
        }

        public bool IsRoot(decimal departmentID)
        {
            if (GetParentID(departmentID) == 0)
                return true;
            return false;
        }

        /// <summary>
        /// لیست بچهای یک گره را با توجه به آدرس والدین برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        //public IList<Department> GetChilds(decimal parentId) 
        //{
        //    IList<Department> depList = base.GetByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Department().ParentPath), String.Format(",{0},", parentId), CriteriaOperation.Like));
        //    return depList;
        //}

        public IList<Department> GetById(IList<decimal> idList) 
        {
            string HQLCommand = "FROM Department " +
                                                "WHERE ID in (:idList) ";

            IQuery query = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameterList("idList", base.CheckListParameter(idList));

            IList<Department> list= query.List<Department>();
            return list;
        }

        /// <summary>
        /// بصورت سلسله مراتبی حذف میکند
        /// </summary>
        /// <param name="parentId"></param>
        public void DelateHirenchicalByParentId(decimal parentId) 
        {
            string SQLCommand = String.Format("DELETE FROM TA_Department where dep_ParentPath like('%,{0},%')", parentId);
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                   .ExecuteUpdate();
        }

        #endregion      
        
    }
}
