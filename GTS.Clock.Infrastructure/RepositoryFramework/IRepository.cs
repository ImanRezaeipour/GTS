using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using GTS.Clock.Infrastructure.Utility;
namespace GTS.Clock.Infrastructure.RepositoryFramework
{
    /// <summary>
    /// .رابط انباره پایه برای تمام رابطهای قابل تعریف در مدل
    /// </summary>
    /// <typeparam name="T">.نوع موجودیتی که انباره با آن کار می کند</typeparam>
    public interface IRepository<T>
    {
        T GetById(decimal id, bool shouldLock);
        T CheckById(decimal id);
        IList<T> GetAll();
        IList<T> GetAllByPage(int pageIndex, int pageSize);
        T Save(T entity);
        T Update(T entity);
        T SaveOrUpdate(T entity);
        T Merge(T Entity);
        T WithoutTransactSave(T entity);
        T WithoutTransactUpdate(T entity);
        T WithoutTransactSaveOrUpdate(T entity);
        T WithoutTransactMerge(T Entity);
        void Delete(T entity);
        void RunHQL(string Query);
        void RunSQL(string Query);
        void CommitChanges();
        ITransaction BeginTransaction();
        void RollebackTransaction();
        IQuery GetNamedQuery(string QueryName);
        void Refresh(T Entity);
        IList<T> QuickSearch(string searchValue, int startIndex, int pageSize);
        int QuickSearchCount(string searchValue);
        int RowCount();
        string TableName { get; }
        IList<T> GetByCriteria(params CriteriaStruct[] cretriaParam);
        IList<T> GetByCriteria(ConditionOperations conOp, params CriteriaStruct[] cretriaParam);
        IList<T> GetByCriteriaByPage(int pageIndex, int pageSize, params CriteriaStruct[] cretriaParam);
        IList<T> GetByCriteriaByPage(int pageIndex, int pageSize, ConditionOperations conOp, params CriteriaStruct[] cretriaParam);
        int GetCountByCriteria(params CriteriaStruct[] cretriaParam);
        int GetCountByCriteria(ConditionOperations conOp, params CriteriaStruct[] cretriaParam);
        string GetConnectionString();
        decimal GetMaxId();
        IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
