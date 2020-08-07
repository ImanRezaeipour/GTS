using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Utility;
using System.Linq;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using NHibernate.Transform;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Infrastructure.NHibernateFramework;


namespace GTS.Clock.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        /// <summary>
        /// بمنظور کلاس بندی رکوردهای استخراج شده از 
        /// دیتابیس بمنظور بررسی سطح دسترسی کاربر استفاده میشود
        /// </summary>
        public struct UserAuthorization
        {
            public string Username { get; set; }
            public string Description { get; set; }
            public string Method { get; set; }
            public bool Allow { get; set; }

            public override string ToString()
            {
                return String.Format("{0} , {1}", Method, Allow);
            }
        }

        #region Constructor
        public UserRepository()
            : base()
        { }
        public UserRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }
        #endregion

        public override string TableName
        {
            get { return "TA_SecurityUser"; }
        }

        public User GetByUserName(string username)
        {
            User user = new User();
            user= base.NHibernateSession.CreateCriteria(typeof(User))
                                       .Add(Expression.Eq(Utility.Utility.GetPropertyName(() => user.UserName), username))
                                       .UniqueResult<User>();
            return user;
        }

        public bool ExistsUser(string username)
        {
            User user = new User();
            int count = base.NHibernateSession.CreateCriteria(typeof(User))
                                       .Add(Expression.Eq(Utility.Utility.GetPropertyName(() => user.UserName), username))
                                       .Add(Expression.Eq(Utility.Utility.GetPropertyName(() => user.Active), username))
                                       .UniqueResult<int>();
            return count > 0;
        }

        #region Get Count
        public int GetNumberOfOnlineUsers(DateTime compareTime)
        {

            User user = new User();
            int count = base.NHibernateSession.CreateCriteria(typeof(User))
                                       .Add(Expression.Ge(Utility.Utility.GetPropertyName(() => user.LastActivityDate), compareTime))
                                       .UniqueResult<int>();
            return count;
        }

        public int GetNumberOfUsers()
        {
            User user = new User();
            int count = base.NHibernateSession.CreateCriteria(typeof(User))
                                       .Add(Expression.Ge(Utility.Utility.GetPropertyName(() => user.Active), true))
                                       .UniqueResult<int>();
            return count;
        }

        public int GetNumberOfUsersByBarcode(string searchValue, decimal[] dataAccess)
        {
            User user = null;
            Person prs = null;
            int count = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .WhereRestrictionOn(x => x.Person)
                                  .IsNotNull()
                                  .WhereRestrictionOn(() => prs.BarCode)
                                  .IsLike(searchValue, MatchMode.Anywhere)
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .RowCount();
            return count;
        }

        public int GetNumberOfUsersByName(string searchValue, decimal[] dataAccess)
        {
            string HQLCommand = @"select Count(*) from User as usr
                                  WHERE usr.Person.FirstName + ' ' + usr.Person.LastName like :name
                                    AND usr.Person.ID in (:list)";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
                 .SetParameter("name", String.Format("%{0}%", searchValue))
                 .SetParameterList("list", base.CheckListParameter(dataAccess))
                 .List<object>().FirstOrDefault();
            return Utility.Utility.ToInteger(count == null ? "0" : count.ToString());
        }

        public int GetNumberOfUsersByUserName(string searchValue, decimal[] dataAccess)
        {
            User user = new User();
            Person prs = null;
            int count = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .WhereRestrictionOn(() => user.UserName)
                                  .IsLike(searchValue, MatchMode.Anywhere)
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .RowCount();
            return count;
        }

        public int GetNumberOfUsersByRoleName(string searchValue, decimal[] dataAccess)
        {
            User user = null;
            Role rle = null;
            Person prs = null;
            int count = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .JoinAlias(() => user.Role, () => rle)
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .WhereRestrictionOn(x => x.Role)
                                  .IsNotNull()
                                  .WhereRestrictionOn(() => rle.Name)
                                  .IsLike(searchValue, MatchMode.Anywhere)
                                  .RowCount();

            return count;
        }

        public int GetNumberOfUsersByQuickSearch(UserSearchKeys searchKey, string searchTerm, decimal userId)
        {
            string SqlCommand = string.Empty;
            switch (searchKey)
            {
                case UserSearchKeys.PersonCode:
                    SqlCommand = @"select Count(*) from TA_SecurityUser as usr
                                   join TA_Person on prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE Prs_Barcode like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.Name:
                    SqlCommand = @"select Count(*) from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE prs_FirstName + ' ' + prs_LastName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.Username:
                    SqlCommand = @"select Count(*) from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE usr.user_UserName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.RoleName:
                    SqlCommand = @"select Count(*)
                                          from TA_SecurityRole as rol
                                                                      inner join
                                                                               (select usr.* from TA_SecurityUser as usr
                                                                                join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                               )personUser
                                                                      on personUser.user_RoleID = rol.role_ID
                                           where rol.role_Name like :searchTerm";
                    break;
                case UserSearchKeys.NotSpecified:
                    SqlCommand = @"select Count(*)
                                         from TA_SecurityRole as rol inner join                                         
                                                                               (select usr.* from TA_SecurityUser as usr inner join TA_Person
                                                                                                                         on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                               )personUser
                                                                     on personUser.user_RoleID = rol.role_ID
                                         where rol.role_Name like :searchTerm 
								         and personUser.user_ID not in
										                              (select usr.user_ID from TA_SecurityUser as usr inner join TA_Person                                                                        
                                                                                                                      on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                       where (prs_FirstName + ' ' + prs_LastName like :searchTerm
                                                                       or  prs_BarCode like :searchTerm
                                                                       or  user_UserName like :searchTerm)
                                                                       and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                      )
                                   union
                                   select Count(*)
	                                      from 
	                                          (select usr.* from TA_SecurityUser as usr inner join TA_Person 
                                                                                        on  prs_IsDeleted=0 AND Prs_ID=user_PersonID                                                                              
                                                            where (prs_FirstName + ' ' + prs_LastName like :searchTerm
                                                            or  prs_BarCode like :searchTerm
                                                            or  user_UserName like :searchTerm)
                                                            and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                              )personUser inner join TA_SecurityRole rol
		                                                   on rol.role_ID = personUser.user_RoleID
                                         where personUser.user_ID not in
	                                                                     (select user_ID from TA_SecurityUser inner join TA_SecurityRole 
									                                                                          on user_RoleID = role_ID
									                                                     where role_Name like :searchTerm
									                                     )
								  union
								  select Count(*)
										 from TA_SecurityRole as rol inner join                                         
																			(select usr.* from TA_SecurityUser as usr inner join TA_Person
																														on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                            where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																							and prs_FirstName + ' ' + prs_LastName like :searchTerm
                                                                            )personUser
                                                                    on personUser.user_RoleID = rol.role_ID
                                         where rol.role_Name like :searchTerm 
								         and personUser.user_ID not in
										 (
											select personUser.user_ID
											from TA_SecurityRole as rol inner join                                         
																			      (select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person
																																				   on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																													   where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																				   )personUser
																		on personUser.user_RoleID = rol.role_ID
											where rol.role_Name like :searchTerm 
											and personUser.user_ID not in
																		(select usr.user_ID from TA_SecurityUser as usr
																		inner join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																		where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																		or prs_BarCode like :searchTerm
																		or user_UserName like :searchTerm)
																		and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																		)
										 )
										 and personUser.user_ID not in
										 (
											select personUser.user_ID
											from 
												(select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person 
																													on  prs_IsDeleted=0 AND Prs_ID=user_PersonID            
																						where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																							or prs_BarCode like :searchTerm
																							or user_UserName like :searchTerm)
																							and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
												)personUser inner join TA_SecurityRole rol
															on rol.role_ID = personUser.user_RoleID
											where personUser.user_ID not in
																			(select user_ID from TA_SecurityUser inner join TA_SecurityRole 
																													on user_RoleID = role_ID
																							where role_Name like :searchTerm
																			)

										 )";

                    break;
            }
            IList<object> countList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                                 .SetParameter("searchTerm", String.Format("%{0}%", searchTerm))
                                                 .SetParameter("userId", userId)
                                                 .List<object>();
            int Count = 0;
            foreach (object obj in countList)
            {
                Count += Utility.Utility.ToInteger(obj == null ? "0" : obj.ToString());                
            }
            return Count;

            //            string SqlCommand = @"select Count(*) from TA_SecurityUser as usr
            //                                    join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
            //                                    WHERE (prs_FirstName + ' ' + prs_LastName like :name
            //                                    OR  prs_BarCode like :name
            //                                    OR  user_UserName like :name)
            //                                    AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
            //object count = base.NHibernateSession.CreateSQLQuery(SqlCommand)
            //                                     .SetParameter("name", String.Format("%{0}%", searchTerm))
            //                                     .SetParameter("userId", userId)
            //                                     .List<object>().FirstOrDefault();
            // return Utility.Utility.ToInteger(count == null ? "0" : count.ToString());
        }

        #endregion

        #region Get List
        public IList<User> GetAllActiveUsers()
        {
            User user = new User();
            IList<User> users = base.NHibernateSession.CreateCriteria(typeof(User))
                                       .Add(Expression.IsNotNull(Utility.Utility.GetPropertyName(() => user.Person)))
                                       .Add(Expression.Eq(Utility.Utility.GetPropertyName(() => user.Active), true))
                                       .List<User>();
            return users;

        }

        public IList<User> GetAllByPage( decimal userId,int pageIndex, int pageSize)
        {
            string SQLCommand = "";

            SQLCommand = @"SELECT usr.* FROM TA_SecurityUser as usr
                                  where user_personID in (select * from fn_GetAccessiblePersons(0,:userId,1))";          

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .AddEntity(typeof(User))
                .SetParameter("userId", userId)
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize);

            IList<User> users = new List<User>();          
            users = query.List<User>();
         
            return users;
        }

        public IList<User> GetAllByPageBarcode(string barcode, int pageIndex, int pageSize, decimal[] dataAccess)
        {
            User user = null;
            Person prs = null;
            IList<User> userList = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .WhereRestrictionOn(x => x.Person)
                                  .IsNotNull()
                                  .WhereRestrictionOn(() => prs.BarCode)
                                  .IsLike(barcode, MatchMode.Anywhere)
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .Skip(pageIndex * pageSize)
                                  .Take(pageSize)
                                  .List<User>();
            return userList;
        }

        public IList<User> GetAllByPageName(string name, int pageIndex, int pageSize, decimal[] dataAccess)
        {
            string HQLCommand = @"select usr from User as usr
                                  inner join usr.Person as prs
                                  WHERE prs.FirstName + ' ' + prs.LastName like :name
                                   AND usr.Person.ID in (:list)";
            IList<User> userList = base.NHibernateSession.CreateQuery(HQLCommand)
                 .SetParameter("name", String.Format("%{0}%", name))
                 .SetParameterList("list", base.CheckListParameter(dataAccess))
                 .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                 .List<User>();

            return userList;
        }

        public IList<User> GetAllByPageUserName(string username, int pageIndex, int pageSize, decimal[] dataAccess)
        {

            User user = null;
            Person prs = null;
            IList<User> userList = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .WhereRestrictionOn(x => x.Person)
                                  .IsNotNull()
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .WhereRestrictionOn(() => user.UserName)
                                  .IsLike(username, MatchMode.Anywhere)
                                  .Skip(pageIndex * pageSize)
                                  .Take(pageSize)
                                  .List<User>();
            return userList;
        }

        public IList<User> GetAllByPageRoleName(string username, int pageIndex, int pageSize, decimal[] dataAccess)
        {

            User user = null;
            Role rle = null;
            Person prs = null;
            IList<User> userList = base.NHibernateSession.QueryOver<User>(() => user)
                                  .JoinAlias(() => user.Person, () => prs)
                                  .JoinAlias(() => user.Role, () => rle)
                                  .WhereRestrictionOn(() => prs.ID)
                                  .IsIn(dataAccess)
                                  .WhereRestrictionOn(x => x.Role)
                                  .IsNotNull()
                                  .WhereRestrictionOn(() => rle.Name)
                                  .IsLike(username, MatchMode.Anywhere)
                                  .Skip(pageIndex * pageSize)
                                  .Take(pageSize)
                                  .List<User>();

            return userList;
        }

        public IList<User> GetAllByPageQuickSearch(UserSearchKeys searchkey, string searchTerm, decimal userId, int pageIndex, int pageSize)
        {
            //   SqlCommand = @"select usr.* from TA_SecurityUser as usr
            //                  join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
            //                  WHERE (prs_FirstName + ' ' + prs_LastName like :searchTerm
            //                  OR  prs_BarCode like :searchTerm
            //                  OR  user_UserName like :searchTerm)
            //                  AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";

            string SqlCommand = string.Empty;
            IList<User> userList = null;
            switch (searchkey)
            {
                case UserSearchKeys.PersonCode:
                    SqlCommand = @"select usr.* from TA_SecurityUser as usr
                                   join TA_Person on prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE Prs_Barcode like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.Name:
                    SqlCommand = @"select usr.* from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE prs_FirstName + ' ' + prs_LastName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.Username:
                    SqlCommand = @"select usr.* from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE usr.user_UserName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                    break;
                case UserSearchKeys.RoleName:
                    SqlCommand = @"select personUser.user_Active, 
                                          personUser.user_DomainID, 
		                                  personUser.user_ID, 
		                                  personUser.user_IsADAuthenticateActive, 
		                                  personUser.user_LastActivityDate, 
		                                  personUser.user_Password, 
		                                  personUser.user_PersonID, 
		                                  personUser.user_RoleID, 
		                                  personUser.user_UserName
                                          from TA_SecurityRole as rol
                                                                      inner join
                                                                               (select usr.* from TA_SecurityUser as usr
                                                                                join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                               )personUser
                                                                      on personUser.user_RoleID = rol.role_ID
                                           where rol.role_Name like :searchTerm";                       
                    break;
                case UserSearchKeys.NotSpecified:
                    SqlCommand = @"select * from 
                                                 (select *, ROW_NUMBER() OVER(ORDER BY user_ID desc) as rowno from
                                                                                                                   (select personUser.user_Active,  
																					                                       personUser.user_DomainID, 
																					                                       personUser.user_ID, 
																					                                       personUser.user_IsADAuthenticateActive, 
																					                                       personUser.user_LastActivityDate, 
																					                                       personUser.user_Password, 
																					                                       personUser.user_PersonID, 
																					                                       personUser.user_RoleID, 
																					                                       personUser.user_UserName
																					                                       from TA_SecurityRole as rol inner join                                         
																															                                     (select usr.* from TA_SecurityUser as usr inner join TA_Person
																																										                                    on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                                                                                                               where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                                                                                                                 )personUser
                                                                                                                                                        on personUser.user_RoleID = rol.role_ID
                                                                                                                           where rol.role_Name like :searchTerm 
								                                                                                           and personUser.user_ID not in
																													                                    (select usr.user_ID from TA_SecurityUser as usr
																													                                    inner join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																													                                    where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																													                                    or prs_BarCode like :searchTerm
																													                                    or user_UserName like :searchTerm)
																													                                    and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																													                                    )
                                                                                                                     union
																				                                     select personUser.user_Active, 
																						                                    personUser.user_DomainID, 
																						                                    personUser.user_ID, 
																						                                    personUser.user_IsADAuthenticateActive, 
																						                                    personUser.user_LastActivityDate, 
																						                                    personUser.user_Password, 
																						                                    personUser.user_PersonID, 
																						                                    personUser.user_RoleID, 
																						                                    personUser.user_UserName
																				                                     from 
																					                                    (select usr.* from TA_SecurityUser as usr inner join TA_Person 
																																                                    on  prs_IsDeleted=0 AND Prs_ID=user_PersonID            
																									                                    where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																									                                    or prs_BarCode like :searchTerm
																									                                    or user_UserName like :searchTerm)
																									                                    and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																					                                    )personUser inner join TA_SecurityRole rol
																								                                    on rol.role_ID = personUser.user_RoleID
																				                                    where personUser.user_ID not in
																												                                    (select user_ID from TA_SecurityUser inner join TA_SecurityRole 
																																					                                    on user_RoleID = role_ID
																																                                    where role_Name like :searchTerm
																												                                    )
																			                                         union
																			                                         select personUser.user_Active,  
																					                                         personUser.user_DomainID, 
																					                                         personUser.user_ID, 
																					                                         personUser.user_IsADAuthenticateActive, 
																					                                         personUser.user_LastActivityDate, 
																					                                         personUser.user_Password, 
																					                                         personUser.user_PersonID, 
																					                                         personUser.user_RoleID, 
																					                                         personUser.user_UserName
																					                                 from TA_SecurityRole as rol inner join                                         
																															                               (select usr.* from TA_SecurityUser as usr inner join TA_Person
																																										                             on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                                                                                            where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																																		                    and prs_FirstName + ' ' + prs_LastName like :searchTerm
                                                                                                                                                           )personUser
                                                                                                                                                  on personUser.user_RoleID = rol.role_ID
                                                                                                                     where rol.role_Name like :searchTerm 
								                                                                                     and personUser.user_ID not in
																					                                                              (
																					                                                                select personUser.user_ID
																						                                                            from TA_SecurityRole as rol inner join                                         
																																                                                (select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person
																																											                                                                      on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																																			                                     where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																																                                                )personUser
																														                                                        on personUser.user_RoleID = rol.role_ID
																						                                                            where rol.role_Name like :searchTerm 
																						                                                            and personUser.user_ID not in
																														                                                        (select usr.user_ID from TA_SecurityUser as usr
																														                                                         inner join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																														                                                         where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																														                                                         or prs_BarCode like :searchTerm
																														                                                         or user_UserName like :searchTerm)
																														                                                         and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1)))
																					                                                                                            )
																					                                 and personUser.user_ID not in
																					                                                               (
																							                                                         select personUser.user_ID
																							                                                                                   from 
																								                                                                               (select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person 
																																			                                                                                                     on  prs_IsDeleted=0 AND Prs_ID=user_PersonID            
																												                                                                where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																												                                                                or prs_BarCode like :searchTerm
																												                                                                or user_UserName like :searchTerm)
																												                                                                and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																								                                                                               )personUser inner join TA_SecurityRole rol
																											                                                                               on rol.role_ID = personUser.user_RoleID
																							                                                                                   where personUser.user_ID not in
																															                                                                                  (select user_ID from TA_SecurityUser inner join TA_SecurityRole 
																																								                                                                                   on user_RoleID = role_ID
																																			                                                                                  where role_Name like :searchTerm
																															                                                                                   )

																						                                                           )

																		                                           )list1
			                                     )list2
                                    where   rowno > :pageIndex * :pageSize
	                                    AND rowno <= (:pageIndex + 1) * :pageSize
                                    order by rowno";                       
                    break;
            }

            if (searchkey != UserSearchKeys.NotSpecified)
            {
                userList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                                 .AddEntity(typeof(User))
                                                 .SetParameter("searchTerm", String.Format("%{0}%", searchTerm))
                                                 .SetParameter("userId", userId)
                                                 .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                                 .List<User>();
            }
            else
            {
                userList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                 .AddEntity(typeof(User))
                                 .SetParameter("searchTerm", String.Format("%{0}%", searchTerm))
                                 .SetParameter("userId", userId)
                                 .SetParameter("pageIndex", pageIndex)
                                 .SetParameter("pageSize", pageSize)
                                 .List<User>();
            }

            return userList;
        }

        #endregion

        public IList<UserAuthorization> GetUserAthorization(string username)
        {
            string SQLCommand = @"SELECT SecurityUser.user_UserName, resource_Description,resource_MethodInfo,Athorize_Allow 
            FROM (SELECT * FROM TA_SecurityUser  WHERE user_UserName = :username) AS SecurityUser 
            INNER JOIN TA_SecurityAuthorize on Athorize_RoleID = SecurityUser.user_RoleID 
            INNER JOIN TA_SecurityResource on resource_ID = Athorize_ResourceID";

            IList<UserAuthorization> list = this.NHibernateSession.CreateSQLQuery(SQLCommand)
            .SetParameter("username", username)
             .List<object[]>()
             .Select(exp =>
                new UserAuthorization()
                {
                    Username = (string)exp[0],
                    Description = (string)exp[1],
                    Method = (string)exp[2],
                    Allow = (bool)exp[3]
                }).ToList<UserAuthorization>();
            return list;
        }

        /// <summary>
        /// زبان انتخابی کاربر را با توجه به شناسه کاربری برمیگرداند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Model.AppSetting.Languages GetLanguageByUsername(string username)
        {
            string HQLCommand = @"select lang from Languages as lang
                                  inner join lang.UserSettingList as userSetList
                           inner join  userSetList.User as u
                            where u.UserName =:username";
            IList<Model.AppSetting.Languages> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("username", username)
                .List<Model.AppSetting.Languages>();
            return list.FirstOrDefault();
        }

        /// <summary>
        /// کلمه عبور یک کاربر را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetPasswordByUserId(decimal userId)
        {
            string result = base.NHibernateSession.QueryOver<User>()
                                                 .Select(x => x.Password)
                                                 .Where(x => x.ID == userId)
                                                 .SingleOrDefault<string>();

            return result;
        }

        /// <summary>
        /// تاریخ آخرین فعالیت یک کاربر را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DateTime GetLastActivityDateByUserId(decimal userId)
        {
            DateTime result = base.NHibernateSession.QueryOver<User>()
                                                 .Select(x => x.LastActivityDate)
                                                 .Where(x => x.ID == userId)
                                                 .SingleOrDefault<DateTime>();

            return result;
        }



        #region Data Access

        /// <summary>
        /// آیا بر همه بخش ها دسترسی دارد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool HasAllDepartmentAccess(decimal userId)
        {
            string SQLCommand = @"select COUNT(*) from TA_DataAccessDepartment 
                                  where DataAccessDep_UserID =:userId and DataAccessDep_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            int result = Query.UniqueResult<int>();
            return result > 0;
        }

        /// <summary>
        /// آیا بر همه افراد مدیریت دارد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal HasAllManagerAccess(decimal userId)
        {
            string SQLCommand = @"select top(1) ISNULL(DataAccessManager_ID,0) from TA_DataAccessManager 
                                  where DataAccessManager_UserID =:userId and DataAccessManager_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result ;
        }

        public bool HasAllOrganAccess(decimal userId) 
        {
            string SQLCommand = @"select COUNT(*) from TA_DataAccessOrganizationUnit 
                                  where DataAccessOrgUnit_UserID =:userId and DataAccessOrgUnit_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            int result = Query.UniqueResult<int>();
            return result > 0;
        }

        public decimal HasAllWorkGroupAccess(decimal userId)
        {
            string SQLCommand = @"select TOP(1) ISNULL(dataAccessWorkGrp_ID,0) from TA_DataAccessWorkGroup 
                                  where DataAccessWorkGrp_UserID =:userId and DataAccessWorkGrp_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result ;
        }

        public decimal HasAllRuleGroupAccess(decimal userId)
        {
            string SQLCommand = @"select top(1) ISNULL(DataAccessRuleGrp_ID,0) from TA_DataAccessRuleGroup 
                                  where DataAccessRuleGrp_UserID =:userId and DataAccessRuleGrp_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllShiftAccess(decimal userId)
        {
            string SQLCommand = @"select TOP(1) ISNULL(dataAccessShift_ID,0) from TA_DataAccessShift
                                  where DataAccessShift_UserID =:userId and DataAccessShift_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllEmploymentTypesAccess(decimal userId)
        {
            decimal result = 0;
            NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
            DAEmploymentType daEmploymentType = NHSession.QueryOver<DAEmploymentType>()
                                                         .Where(x => x.UserID == userId &&
                                                                     x.All)
                                                         .SingleOrDefault<DAEmploymentType>();
            if (daEmploymentType != null)
            {
                if(daEmploymentType.EmploymentTypeID == null)
                   daEmploymentType.EmploymentTypeID = 0;
                result = (decimal)daEmploymentType.ID;
            }
            return result;
        }


        public decimal HasAllPrecardAccess(decimal userId)
        {
            string SQLCommand = @"select TOP(1) ISNULL(DataAccessPrecard_ID,0) from TA_DataAccessPrecard
                                  where DataAccessPrecard_UserID =:userId and DataAccessPrecard_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllControlStationAccess(decimal userId)
        {
            string SQLCommand = @"select TOP(1) ISNULL(DataAccessCtrlStation_ID,0) from TA_DataAccessCtrlStation
                                  where DataAccessCtrlStation_UserID =:userId and DataAccessCtrlStation_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllDoctorAccess(decimal userId)
        {
            string SQLCommand = @"select top(1) ISNULL(DataAccessDoctor_ID,0) from TA_DataAccessDoctor
                                  where DataAccessDoctor_UserID =:userId and DataAccessDoctor_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllFlowAccess(decimal userId)
        {
            string SQLCommand = @"select top(1) ISNULL(DataAccessFlow_ID,0) from TA_DataAccessFlow
                                  where DataAccessFlow_UserID =:userId and DataAccessFlow_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        public decimal HasAllReportAccess(decimal userId)
        {
            string SQLCommand = @"select top(1) ISNULL(DataAccessReport_ID,0) from TA_DataAccessReport
                                  where DataAccessReport_UserID =:userId and DataAccessReport_All=1";
            IQuery Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                             .SetParameter("userId", userId);
            decimal result = Query.UniqueResult<decimal>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetUserDepartmentList(decimal userId)
        {
            string SQLCommand = @"select DataAccessDep_DepID from TA_DataAccessDepartment 
                                  where DataAccessDep_UserID=:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;

        }

        /// <summary>
        /// لیست مدیرانی که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetUserManagerIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessManager_ManagerID from TA_DataAccessManager 
                                  where DataAccessManager_UserID=:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Manager> GetUserManagerList(decimal userId)
        {
            string HQLCommand = @"select mng from Manager mng
                                    join mng.DataAccessList as daList                                   
                                    where daList.UserID=:userId";
            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                                 
                                  .SetParameter("userId", userId)
                                  .List<Manager>();
            return list;
        }

        public IList<Manager> GetUserManagerList(string searchKey, decimal userId, int pageIndex, int pageSize)
        {
            string HQLCommand = @"select mngr  from Manager mngr
                                 join mngr.DataAccessList as daList 
                                 left outer join mngr.Person prs 
                                 left outer join mngr.OrganizationUnit.Person organPrs 
                                 left outer join mngr.Person.OrganizationUnitList prsOrgan 
                                 where daList.UserID=:userId 
                                 AND (
                                 prs.FirstName + ' ' + prs.LastName like :key OR
                                 organPrs.FirstName + ' ' + organPrs.LastName like :key OR
                                 prs.BarCode like :key  OR 
                                 prsOrgan.Name like :key   )";

            IList<Manager> list = base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("userId", userId)
                .SetParameter("key", "%" + searchKey + "%")
                .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                .List<Manager>();
            return list;
        }

        public IList<decimal> GetUserOrganList(decimal userId)
        {
            string SQLCommand = @"select DataAccessOrgUnit_OrgUnitId from TA_DataAccessOrganizationUnit 
                                  where DataAccessOrgUnit_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        public IList<decimal> GetUserWorkGroupIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessWorkGrp_WorkGrpID from TA_DataAccessWorkGroup                                  
                                  where DataAccessWorkGrp_UserID=:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<WorkGroup> GetUserWorkGroupList(decimal userId)
        {
            string SQLCommand = @"select DataAccessWorkGrp_ID ID,WorkGroup_Name Name , WorkGroup_CustomCode CustomCode
                                 from TA_WorkGroup
                                 join TA_DataAccessWorkGroup on WorkGroup_ID=DataAccessWorkGrp_WorkGrpID
                                 where DataAccessWorkGrp_UserID=:userId";
            IList<WorkGroup> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(WorkGroup)))
                                  .SetParameter("userId", userId)
                                  .List<WorkGroup>();
            return list;
        }

        public IList<decimal> GetUserRuleGroupIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessRuleGrp_RuleGrpId from TA_DataAccessRuleGroup
                                  where DataAccessRuleGrp_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<RuleCategory> GetUserRuleGroupList(decimal userId)
        {
            string SQLCommand = @"select DataAccessRuleGrp_ID ID,RuleCat_Name Name,RuleCat_CustomCode CustomCode
                                    from TA_RuleCategory join TA_DataAccessRuleGroup 
                                    on RuleCat_ID=DataAccessRuleGrp_RuleGrpID
                                    where DataAccessRuleGrp_UserID=:userId";
            IList<RuleCategory> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(RuleCategory)))
                                  .SetParameter("userId", userId)
                                  .List<RuleCategory>();
            return list;
        }

        public IList<decimal> GetUserShiftIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessShift_ShiftId from TA_DataAccessShift
                                  where DataAccessShift_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Shift> GetUserShiftList(decimal userId)
        {
            string SQLCommand = @"select[DataAccessShift_ID] AS ID
      ,[Shift_Name] AS Name
      ,[Shift_MinNobatKari] AS MinNobatKari
      ,[Shift_Breakfast] AS Breakfast
      ,[Shift_Lunch] AS Lunch
      ,[Shift_Dinner] AS Dinner
      ,[Shift_Color] AS Color
      ,[Shift_CustomCode] AS CustomCode from TA_DataAccessShift
                                  join TA_Shift as shift on Shift_ID=DataAccessShift_ShiftId
                                  where DataAccessShift_UserID =:userId";
            IList<Shift> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)                
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(Shift)))
                                  .SetParameter("userId", userId)
                                  .List<Shift>();
            return list;
        }

        public IList<EmploymentType> GetUserEmployTypeList(decimal userID)
        {
            IList<EmploymentType> userEmploymentTypesList = new List<EmploymentType>();
            IList<DAEmploymentType> daEmploymentTypeList = NHSession.QueryOver<DAEmploymentType>()
                                                                       .Where(x => x.UserID == userID)
                                                                       .List<DAEmploymentType>();

            foreach (DAEmploymentType daEmploymentTypeItem in daEmploymentTypeList)
            {
                EmploymentType employmentType = NHSession.QueryOver<EmploymentType>()
                                                         .Where(x => x.ID == daEmploymentTypeItem.EmploymentTypeID)
                                                         .SingleOrDefault();
                if (employmentType != null)
                {
                    userEmploymentTypesList.Add(new EmploymentType() { 
                        ID = daEmploymentTypeItem.ID,
                        Name = employmentType.Name,
                        CustomCode = employmentType.CustomCode
                    });
                }
            }
            return userEmploymentTypesList;
        }

        public IList<decimal> GetUserEmployTypeIdsList(decimal userID)
        {
            IList<decimal> userEmploymentTypeIdsList = NHSession.QueryOver<DAEmploymentType>()
                                                                .Where(x => x.UserID == userID)
                                                                .Select(x => x.EmploymentTypeID)
                                                                .List<decimal>();
            return userEmploymentTypeIdsList;
        }
        
        public IList<decimal> GetUserPrecardIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessPrecard_PrecardId from TA_DataAccessPrecard
                                  where DataAccessPrecard_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Precard> GetUserPrecardList(decimal userId)
        {
            string SQLCommand = @"select DataAccessPreCard_ID ID,Precrd_Name Name,Precrd_Code Code
                                  from TA_Precard join
                                  TA_DataAccessPrecard on Precrd_ID=DataAccessPreCard_PreCardID
                                  where DataAccessPreCard_UserID =:userId";
            IList<Precard> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(Precard)))
                                  .SetParameter("userId", userId)
                                  .List<Precard>();
            return list;
        }

        public IList<decimal> GetUserControlStationIDList(decimal userId)
        {
            string SQLCommand = @"select DataAccessCtrlStation_CtrlStationId from TA_DataAccessCtrlStation
                                  where DataAccessCtrlStation_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ControlStation> GetUserControlStationList(decimal userId)
        {
            string SQLCommand = @"select DataAccessCtrlStation_ID ID, Station_Name Name,Station_CustomCode CustomCode
                                    from TA_ControlStation join
                                    TA_DataAccessCtrlStation on Station_ID=DataAccessCtrlStation_CtrlStationID
                                    where DataAccessCtrlStation_UserID=:userId";
            IList<ControlStation> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(ControlStation)))
                                  .SetParameter("userId", userId)
                                  .List<ControlStation>();
            return list;
        }

        public IList<decimal> GetUserDoctorIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessDoctor_DoctorID from TA_DataAccessDoctor 
                                  where DataAccessDoctor_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }
        
        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Doctor> GetUserDoctorList(decimal userId)
        {
            string SQLCommand = @"select DataAccessDoctor_ID ID,dr_FirstName FirstName , dr_LastName LastName
                                  from TA_Doctor join
                                  TA_DataAccessDoctor on dr_ID=DataAccessDoctor_DoctorID
                                  where DataAccessDoctor_UserID =:userId";
            IList<Doctor> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(Doctor)))
                                  .SetParameter("userId", userId)
                                  .List<Doctor>();
            return list;
        }

        public IList<decimal> GetUserFlowIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessFlow_FlowId from TA_DataAccessFlow
                                  where DataAccessFlow_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Flow> GetUserFlowList(decimal userId)
        {
            string SQLCommand = @"select DataAccessFlow_ID ID,Flow_FlowName FlowName
                                    from TA_Flow join TA_DataAccessFlow
                                    on Flow_ID=DataAccessFlow_FlowID
                                    where DataAccessFlow_UserID=:userId and flow_Deleted = 0";
            IList<Flow> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(Flow)))
                                  .SetParameter("userId", userId)
                                  .List<Flow>();
            return list;
        }

        public IList<decimal> GetUserReportIdList(decimal userId)
        {
            string SQLCommand = @"select DataAccessReport_ReportId from TA_DataAccessReport
                                  where DataAccessReport_UserID =:userId";
            IList<decimal> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("userId", userId)
                                  .List<decimal>();
            return list;
        }

        /// <summary>
        /// بجای شناسه خودش , شناسه دسترسی برگردانده شد
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Report> GetUserReportList(decimal userId)
        {
            string SQLCommand = @"select DataAccessReport_ID ID,Report_Name Name
                                    from TA_Report join TA_DataAccessReport
                                    on Report_ID=DataAccessReport_ReportID
                                    where DataAccessReport_UserID=:userId";
            IList<Report> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetResultTransformer(Transformers.AliasToBean(typeof(Report)))
                                  .SetParameter("userId", userId)
                                  .List<Report>();
            return list;
        }

        public IList<Corporation> GetUserCorporationList(decimal userId)
        {
            IList<Corporation> CorporationsList = new List<Corporation>();
            IList<DACorporation> DACorporationsList = NHibernateSession.QueryOver<DACorporation>()
                                                   .Where(daCorporation => daCorporation.UserID == userId)
                                                   .List<DACorporation>();
            foreach (DACorporation daCorporation in DACorporationsList)
            {
                CorporationsList.Add(new Corporation() { ID = daCorporation.ID, Name = daCorporation.Corporation.Name, Code = daCorporation.Corporation.Code });
            }
            return CorporationsList;
        }

        #endregion

        public IList<decimal> GetAllUserIDList(decimal currentUserID, UserSearchKeys? searchKey, string searchTerm, bool singleResult)
        {
            IList<decimal> userIDList = new List<decimal>();
            string SqlCommand = "select ";
            if (singleResult)
                SqlCommand += "top 1 ";
            if (searchKey == null && searchTerm == string.Empty)
            {
                SqlCommand += @"usr.user_ID FROM TA_SecurityUser as usr
                                  where usr.user_PersonID in (select * from fn_GetAccessiblePersons(0,:currentUserID,1))";

                userIDList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                                   .SetParameter("currentUserID", currentUserID)
                                                   .List<decimal>();
            }
            else
            {
                switch (searchKey)
                {
                    case UserSearchKeys.PersonCode:
                        SqlCommand = @"select usr.user_ID from TA_SecurityUser as usr
                                   join TA_Person on prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE Prs_Barcode like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                        break;
                    case UserSearchKeys.Name:
                        SqlCommand = @"select usr.user_ID from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE prs_FirstName + ' ' + prs_LastName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                        break;
                    case UserSearchKeys.Username:
                        SqlCommand = @"select usr.user_ID from TA_SecurityUser as usr
                                   join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                   WHERE usr.user_UserName like :searchTerm
                                   AND prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))";
                        break;
                    case UserSearchKeys.RoleName:
                        SqlCommand = @"select personUser.user_ID 
                                          from TA_SecurityRole as rol
                                                                      inner join
                                                                               (select usr.* from TA_SecurityUser as usr
                                                                                join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                               )personUser
                                                                      on personUser.user_RoleID = rol.role_ID
                                           where rol.role_Name like :searchTerm";
                        break;
                    case UserSearchKeys.NotSpecified:
                        SqlCommand = @"select personUser.user_ID  
												from TA_SecurityRole as rol inner join                                         
																						(select usr.* from TA_SecurityUser as usr inner join TA_Person
																																on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                                    where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
                                                                                        )personUser
                                                                            on personUser.user_RoleID = rol.role_ID
                                                where rol.role_Name like :searchTerm 
								                and personUser.user_ID not in
																			(select usr.user_ID from TA_SecurityUser as usr
																			inner join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																			where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																			or prs_BarCode like :searchTerm
																			or user_UserName like :searchTerm)
																			and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																			)
                                               union
										       select personUser.user_ID
										       from 
											        (select usr.* from TA_SecurityUser as usr inner join TA_Person 
																						        on  prs_IsDeleted=0 AND Prs_ID=user_PersonID            
															        where (prs_FirstName + ' ' + prs_LastName like :searchTerm
															        or prs_BarCode like :searchTerm
															        or user_UserName like :searchTerm)
															        and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
											        )personUser inner join TA_SecurityRole rol
														        on rol.role_ID = personUser.user_RoleID
										       where personUser.user_ID not in
																		        (select user_ID from TA_SecurityUser inner join TA_SecurityRole 
																											        on user_RoleID = role_ID
																						        where role_Name like :searchTerm
																		        )
                                               union
											   select personUser.user_PersonID  
											   from TA_SecurityRole as rol inner join                                         
																					(select usr.* from TA_SecurityUser as usr inner join TA_Person
																																on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
                                                                                    where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																					and prs_FirstName + ' ' + prs_LastName like :searchTerm
                                                                                    )personUser
                                                                            on personUser.user_RoleID = rol.role_ID
                                                where rol.role_Name like :searchTerm 
								                and personUser.user_ID not in
																			(
																			 select personUser.user_ID
																			 from TA_SecurityRole as rol inner join                                         
																										(select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person
																																											on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																											where prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																										)personUser
																										on personUser.user_RoleID = rol.role_ID
																			 where rol.role_Name like :searchTerm 
																			 and personUser.user_ID not in
																										(select usr.user_ID from TA_SecurityUser as usr
																											inner join TA_Person on  prs_IsDeleted=0 AND Prs_ID=user_PersonID
																											where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																											or prs_BarCode like :searchTerm
																											or user_UserName like :searchTerm)
																											and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1)))
																					                    )
												and personUser.user_ID not in
																			(
																			  select personUser.user_ID
																							            from 
																								        (select usr.user_ID, usr.user_RoleID from TA_SecurityUser as usr inner join TA_Person 
																																			                                on  prs_IsDeleted=0 AND Prs_ID=user_PersonID            
																										where (prs_FirstName + ' ' + prs_LastName like :searchTerm
																										or prs_BarCode like :searchTerm
																										or user_UserName like :searchTerm)
																										and prs_ID in (select * from dbo.fn_GetAccessiblePersons(0,:userId,1))
																								        )personUser inner join TA_SecurityRole rol
																											        on rol.role_ID = personUser.user_RoleID
																							            where personUser.user_ID not in
																															            (select user_ID from TA_SecurityUser inner join TA_SecurityRole 
																																								            on user_RoleID = role_ID
																																			            where role_Name like :searchTerm
																															            )

																		  )";
                        break;
                }

                userIDList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                                    .SetParameter("searchTerm", String.Format("%{0}%", searchTerm))
                                                    .SetParameter("userId", currentUserID)
                                                    .List<decimal>();
            }
            return userIDList;
        }

        public bool CheckIsUserDefined(Person definedPerson)
        {
            bool isUserDefined = false;
            int rowCount = NHibernateSession.QueryOver<User>()
                                         .JoinQueryOver<Person>(user => user.Person)
                                         .Where(person => person.ID == definedPerson.ID)
                                         .RowCount();
            if (rowCount > 0)
                isUserDefined = true;
            return isUserDefined;                                            
        }

        public void DeleteDeletedPersonUsers(decimal personID)
        {
            Person personAlias = null;
            User userAlias = null;
            IList<User> relativeUsers = NHibernateSessionManager.Instance.GetSession().QueryOver<User>(() => userAlias)
                                                                                      .JoinAlias(() => userAlias.Person, () => personAlias)
                                                                                      .Where(() => personAlias.ID == personID)
                                                                                      .List<User>();
            foreach (User user in relativeUsers)
            {
                this.Delete(user);
            }

        }

    }
}
