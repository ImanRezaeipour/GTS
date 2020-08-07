using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Infrastructure.Repository
{
    public class AuthorizeRepository : RepositoryBase<Authorize>, IAuthorizeRepository
    {
        /// <summary>
        /// لیستی از منابعی که استفاده از آن برای یک نقش خاص ممنوع است را برمیگرداند
        /// درهنگام ذخیره تنها آنهایی اجازه دسترسی دارد را ذخیره میکنیم
        /// </summary>    
        /// <returns></returns>
        public IList<Resource> GetAccessDenied(decimal roleID)         
        {
            string query = String.Format(@"select * from TA_SecurityResource where  resource_ResourceID is not null AND resource_SubSystemID = " + ((int)SubSystemIdentifier.TimeAtendance).ToString() + " AND " +
                    "resource_ID not in ( " +
                    "select Athorize_ResourceID  from TA_SecurityAuthorize " +
                    "where Athorize_RoleID={0} and Athorize_Allow=1)", roleID);
            return base.NHibernateSession.CreateSQLQuery(query).AddEntity(typeof(Resource)).List<Resource>();
        }
       
        /// <summary>
        /// لیستی از منابعی که استفاده از آن برای یک نقش خاص مجاز است را برمیگرداند
        /// </summary>    
        /// <returns></returns>
        public IList<Resource> GetAccessAllowed(decimal roleID)
        {
            string query = String.Format(@"select * from TA_SecurityResource where  resource_ResourceID is not null AND resource_SubSystemID = " + ((int)SubSystemIdentifier.TimeAtendance).ToString() + " AND " +
                    "resource_ID in ( " +
                    "select Athorize_ResourceID  from TA_SecurityAuthorize " +
                    "where Athorize_RoleID={0} and Athorize_Allow=1)", roleID);
            return base.NHibernateSession.CreateSQLQuery(query).AddEntity(typeof(Resource)).List<Resource>();
        }

        /// <summary>
        /// لیستی از منابعی که استفاده از آن برای یک نقش خاص ممنوع است را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Resource> GetAccessDenied(Role role)
        {
            return GetAccessDenied(role.ID);
        }

        /// <summary>
        /// تمام منابع را به ازای یک نقش برمیگرداند
        /// </summary>    
        public IList<Resource> GetResourceByRoleId(decimal roleId) 
        {
            string query = String.Format("select TA_SecurityResource.*,TA_SecurityAuthorize.Athorize_Allow  from TA_SecurityResource  " +
                    "join TA_SecurityAuthorize on  Athorize_ResourceID=resource_ID " +
                    " where Athorize_RoleID={0} and resource_ParentID=0 " +
                    " order by resource_ParentID ", roleId);
            return base.NHibernateSession.CreateSQLQuery(query).AddEntity(typeof(Resource)).List<Resource>();

        }

        /// <summary>
        ///تعیین سطح دسترسی
        /// </summary>    
        public void SetAuthorize(decimal resourceId, decimal roleId, bool allow)
        {
            string query = String.Format("UPDATE TA_SecurityAuthorize  SET Athorize_Allow={0}  " +
                    " WHERE  Athorize_ResourceID={1} AND Athorize_RoleID={2}", Convert.ToInt16(allow), resourceId, roleId);

             base.NHibernateSession.CreateSQLQuery(query).AddEntity(typeof(Resource)).List<Resource>();
        }

        public override string TableName
        {
            get { return "TA_SecurityAuthorize"; }
        }
    }
}