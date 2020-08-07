using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// created at: 3/17/2012 12:04:53 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BPrivateMessage : BaseBusiness<PrivateMessage>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BPrivateMessage";
        private EntityRepository<PrivateMessage> objectRep = new EntityRepository<PrivateMessage>();

        public int GetAllRecievedMessagesCount() 
        {
            int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), BUser.CurrentUser.Person.ID),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true));
            return count;
        }

        public int GetAllUnReadRecievedMessagesCount() 
        {
            try
            {
                int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), BUser.CurrentUser.Person.ID),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().Status), false));
                return count;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        public int GetAllSentMessageCount()
        {
            int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromPersonID), BUser.CurrentUser.Person.ID),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromActive), true));
            return count;
        }

        public IList<PrivateMessage> GetAllRecievedMessages(int pageIndex, int pageSize) 
        {
            try
            {
                IList<PrivateMessage> list = objectRep.GetByCriteriaByPage(pageIndex, pageSize, new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), BUser.CurrentUser.Person.ID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true));
                list = list.OrderByDescending(x => x.Date).ToList();
                foreach (PrivateMessage pm in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        pm.TheDate = Utility.ToPersianDate(pm.Date);
                    }
                    else
                    {
                        pm.TheDate = Utility.ToString(pm.Date);
                    }
                }

                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPrivateMessage", "GetAllRecievedMessages");
                throw ex;
            }
        }

        public IList<PrivateMessage> GetAllSentMessage(int pageIndex, int pageSize)
        {
            try
            {
                IList<PrivateMessage> list = objectRep.GetByCriteriaByPage(pageIndex,pageSize, new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromPersonID), BUser.CurrentUser.Person.ID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromActive), true));
                list = list.OrderByDescending(x => x.Date).ToList();
                foreach (PrivateMessage pm in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        pm.TheDate = Utility.ToPersianDate(pm.Date);
                    }
                    else
                    {
                        pm.TheDate = Utility.ToString(pm.Date);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetAllSentMessage");
                throw ex;
            }
        }

        public void SetMessageAsRead(decimal messageId)
        {
            try
            {
                PrivateMessage pm = base.GetByID(messageId);
                pm.Status = true;
                this.SaveChanges(pm, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "SetMessageAsRead");
                throw ex;
            }
        }

        public void ReplyMessage(decimal messageId, string message)
        {
            try
            {
                PrivateMessage from = base.GetByID(messageId);
                PrivateMessage privateMsg = new PrivateMessage();
                privateMsg.Message = message;
                privateMsg.FromPersonID = BUser.CurrentUser.Person.ID;
                privateMsg.ToPersonID = from.FromPersonID;
                privateMsg.Date = DateTime.Now;
                privateMsg.Subject = from.Subject;
                privateMsg.Status = false;
                privateMsg.FromActive = true;
                privateMsg.ToActive = true;
                base.SaveChanges(privateMsg, UIActionType.ADD);

            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "ReplyMessage");
                throw ex;
            }
        }

        public void NewMessage(string subject, string message, IList<PersonDepartmentProxy> prsList)
        {
            try
            {
                IList<Person> toPrsList = new BUnderManagment().GetPersonsByDepartment(prsList);
                foreach (Person person in toPrsList)
                {
                    PrivateMessage msg = new PrivateMessage();
                    msg.Subject = subject;
                    msg.Message = message;
                    msg.FromPersonID = BUser.CurrentUser.Person.ID;
                    msg.Date = DateTime.Now;
                    msg.ToPersonID = person.ID;
                    msg.ToActive = true;
                    msg.FromActive = true;
                    base.SaveChanges(msg, UIActionType.ADD);
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPrivateMessage", "NewMessage");
                throw ex;
            }
        }

        /// <summary>
        /// ارسال پیام خرابی سیستم
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void NewMessage(string subject, string message)
        {
            try
            {
                IList<User> toPrsList = new BRole().GetUsersInSysAdminRole();
                var persons = from o in toPrsList
                              where o.Active && o.Person.Active && !o.Person.IsDeleted
                              select o.Person;
                           
                foreach (Person person in persons)
                {
                    PrivateMessage msg = new PrivateMessage();
                    msg.Subject = subject;
                    msg.Message = message;
                    msg.FromPersonID = BUser.CurrentUser.Person.ID;
                    msg.Date = DateTime.Now;
                    msg.ToPersonID = person.ID;
                    msg.ToActive = true;
                    msg.FromActive = true;
                    base.SaveChanges(msg, UIActionType.ADD);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "NewMessage To Sys Admin");
                throw ex;
            }
        }

        public void DeleteFromSentBox(IList<decimal> messageIds) 
        {
            try
            {
                foreach (decimal messageId in messageIds)
                {
                    PrivateMessage pm = base.GetByID(messageId);
                    pm.FromActive = false;
                    if (pm.ToActive)
                    {
                        base.SaveChanges(pm, UIActionType.EDIT);
                    }
                    else
                    {
                        base.SaveChanges(pm, UIActionType.DELETE);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "DeleteFromSentBox");
                throw ex;
            }
        }

        public void DeleteFromInbox(IList<decimal> messageIds)
        {
            try
            {
                foreach (decimal messageId in messageIds)
                {
                    PrivateMessage pm = base.GetByID(messageId);
                    pm.ToActive = false;
                    if (pm.FromActive)
                    {
                        base.SaveChanges(pm, UIActionType.EDIT);
                    }
                    else
                    {
                        base.SaveChanges(pm, UIActionType.DELETE);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "DeleteFromInbox");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPrivateMessagesLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSendPrivateMessageAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDeletePrivateMessageAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSystemMessageLoadAccess()
        { 
        }

        #region Tree

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentRoot()
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID)
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetDepartmentChildsWithoutDA(nodeID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID)
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetByID(departmentID).PersonList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentPerson");
                throw ex;
            }
        }
    
        #endregion

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(PrivateMessage obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(PrivateMessage obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(PrivateMessage obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        #endregion
    }
}
