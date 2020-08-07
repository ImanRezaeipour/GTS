using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository()
            : base()
        { }
        public RoleRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
        public override string TableName
        {
            get { return "TA_SecurityRole"; }
        }
        public Role GetByRoleName(string rolename)
        {
            return base.NHibernateSession.CreateCriteria(typeof(Role))
                                       .Add(Expression.Eq("Name", rolename))
                                       .UniqueResult<Role>();
        }

        public Role GetByRoleCode(string roleCode)
        {
            return base.NHibernateSession.CreateCriteria(typeof(Role))
                                       .Add(Expression.Eq("CustomCode", roleCode))
                                       .UniqueResult<Role>();
        }

        public void DeleteUserFromRole(decimal roleId, decimal userId)
        {
            string query = String.Format(" DELETE FROM TA_SecurityUserRole WHERE usrRle_RoleID={0} AND usrRle_UserID = {1} ", roleId, userId);
            base.NHibernateSession.CreateQuery(query);
        }

        public bool ExistsRole(decimal roleId)
        {
            string SQLQuery = String.Format(" SELECT Count(*) FROM TA_SecurityRole WHERE role_ID={0}", roleId.ToString());

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                                           .List<int>().First();
            return count > 0;
        }

        public bool ExistsRole(string roleName)
        {
            string SQLQuery = String.Format(" SELECT Count(*) FROM TA_SecurityRole WHERE role_Name={0}", roleName);

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                                           .List<int>().First();
            return count > 0;
        }

        public bool IsUserInRole(decimal userId, decimal roleId) 
        {
            string SQLQuery = String.Format(" select Count(*) from TA_SecurityUserRole where usrRle_UserID={0} and usrRle_RoleID={1} ", userId, roleId);

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                                           .List<int>().First();
            return count > 0;
        }

        public bool IsUserInRole(string username, decimal roleId)
        {
            string SQLQuery = String.Format(" select Count(*) from TA_SecurityUserRole where usrRle_UserID in (select USER_ID from TA_SecurityUser where user_UserName='{0}') and usrRle_RoleID={1} ", username, roleId);

            int count = base.NHibernateSession.CreateSQLQuery(SQLQuery)
                                           .List<int>().First();
            return count > 0;
        }

        public void UpdateUserRole(decimal userId, decimal roleId)
        {
            string SQLQuery = String.Format(" update TA_SecurityUserRole set usrRle_RoleID={0} where usrRle_UserID={1}", roleId, userId);

            base.NHibernateSession.CreateQuery(SQLQuery);
        }

        public bool IsRoot(decimal roleId)
        {
            int count = base.GetCountByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Role().ID), roleId),
                                     new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Role().ParentId), (decimal)0));
            return count > 0 ? true : false;
        }


        public decimal GetParentId(decimal roleId) 
        {
            decimal result = base.NHibernateSession.QueryOver<Role>()
                                                  .Select(x => x.ParentId)
                                                  .Where(x => x.ID == roleId)
                                                  .SingleOrDefault<decimal>();

            return result;
        }
    }
}
