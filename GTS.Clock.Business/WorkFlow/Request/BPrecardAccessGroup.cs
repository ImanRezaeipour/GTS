using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RequestFlow
{
    public class BPrecardAccessGroup : BaseBusiness<PrecardAccessGroup>
    {
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BPrecardAccessGroup";
        EntityRepository<PrecardAccessGroup> accessGroupRep = new EntityRepository<PrecardAccessGroup>();

        /// <summary>
        /// دلیل استفاده از این سرویس این است که واسط کاربر باید درختی از پیشکارتها را نمایش دهد
        /// و بدلیل محدودیتها جاوا اسکریپت مجبوریم شمای درخت را در سرویس دریافت و آنرا تحلیل کنیم
        /// </summary>
        /// <param name="name">نام گروه دسترسی</param>
        /// <param name="description">توضیح</param>
        /// <param name="accessGroupList">لیست پیکارتها که از درخت استخراج شده است</param>
        public decimal InsertByProxy(string name, string description, IList<AccessGroupProxy> accessGroupList)
        {
            try
            {
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                IList<Precard> removeList = new List<Precard>();
                PrecardAccessGroup accessGroup = new PrecardAccessGroup();
                accessGroup.Name = name;
                accessGroup.Description = description;
                accessGroup.PrecardList = new List<Precard>();
                foreach (AccessGroupProxy proxy in accessGroupList)
                {
                    if (proxy.IsParent)
                    {
                        PrecardGroups group = groupRep.GetById(proxy.ID, false);
                        foreach (Precard p in group.PrecardList)
                        {
                            accessGroup.PrecardList.Add(p);
                        }
                    }
                    else if (proxy.Checked)
                    {
                        accessGroup.PrecardList.Add(new Precard() { ID = proxy.ID });
                    }
                    else
                    {
                        removeList.Add(new Precard() { ID = proxy.ID });
                    }
                }
                foreach (Precard p in removeList)
                {
                    accessGroup.PrecardList.Remove(p);
                }
                SaveChanges(accessGroup, UIActionType.ADD);
                return accessGroup.ID;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPrecardAccessGroup", "InsertByProxy");
                throw ex;
            }
        }

        public decimal UpdateByProxy(decimal accessGroupId, string name, string description, IList<AccessGroupProxy> accessGroupList,bool updateAccessGroupDetail)
        {
            try
            {
                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                IList<Precard> removeList = new List<Precard>();
                PrecardAccessGroup accessGroup = new PrecardAccessGroup();
                accessGroup = base.GetByID(accessGroupId);
                accessGroup.Name = name;
                accessGroup.Description = description;

                if (updateAccessGroupDetail) 
                {//اگر این لیست خالی باشد معنایش این است که آیتم های قبلی نباید دست بخورد
                    accessGroup.PrecardList.Clear();
                }
                
                foreach (AccessGroupProxy proxy in accessGroupList)
                {
                    if (proxy.IsParent)
                    {
                        PrecardGroups group = groupRep.GetById(proxy.ID, false);
                        foreach (Precard p in group.PrecardList)
                        {
                            accessGroup.PrecardList.Add(p);
                        }
                    }
                    else if (proxy.Checked)
                    {
                        accessGroup.PrecardList.Add(new Precard() { ID = proxy.ID });
                    }
                    else
                    {
                        removeList.Add(new Precard() { ID = proxy.ID });
                    }
                }
                foreach (Precard p in removeList)
                {
                    accessGroup.PrecardList.Remove(p);
                }
                SaveChanges(accessGroup, UIActionType.EDIT);
                return accessGroup.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecardAccessGroup", "UpdateByProxy");
                throw ex;
            }
        }

        /// <summary>
        /// همه پیشکارتها را جهت نمایش گروهی برمیگرداند
        /// اگر پارامتر ورودی صفر باشد بدین معنی است که
        /// در مد اینزرت هستیم
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <returns></returns>
        public IList<PrecardGroups> GetPrecardTree(decimal accessGroupId)
        {
            try
            {
                BPrecard bPrecard = new BPrecard();
                IList<PrecardGroups> groupList = bPrecard.GetAllPrecardGroups();
                if (accessGroupId > 0)
                {
                    PrecardAccessGroup accessGroup = base.GetByID(accessGroupId);

                    foreach (PrecardGroups group in groupList)
                    {
                        int precardCount = group.PrecardList.Count, childCounter = 0;
                        foreach (Precard precard in group.PrecardList)
                        {
                            foreach (Precard p in accessGroup.PrecardList)
                            {
                                if (p.Equals(precard))
                                {
                                    childCounter++;
                                    precard.ContainInPrecardAccessGroup = true;
                                    break;
                                }
                            }
                        }
                        if (precardCount == childCounter && precardCount > 0)
                            group.ContainInPrecardAccessGroup = true;
                    }
                }
                return groupList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrecardAccessGroup", "GetPrecardTree");
                throw ex;
            }
        }

        /// <summary>
        /// پیشکارتهای یک گروه را برمیگرداند
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <returns></returns>
        public IList<Precard> GetPrecardGroupChilds(decimal accessGroupId) 
        {
            BPrecard bprecard = new BPrecard();
            return bprecard.GetAllByPrecardGroup(accessGroupId);
        }

        /// <summary>        
        /// نام نباید خالی باشد
        /// نام تکراری نباشد       
        /// لیست جزیات نباید خالی باشد        
        /// </summary>
        /// <param name="precard"></param>
        protected override void InsertValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(accessGroup.Name)) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRequierd, "نام گروه دسترسی نباید خالی باشد", ExceptionSrc));
            }
            else if (accessGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), accessGroup.Name)) > 0) 
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRepeated, "نام گروه دسترسی نباید تکراری باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// نام نباید خالی باشد
        /// نام تکراری نباشد       
        /// </summary>
        /// <param name="precard"></param>
        protected override void UpdateValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            
            if (Utility.IsEmpty(accessGroup.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRequierd, "نام گروه دسترسی نباید خالی باشد", ExceptionSrc));
            }
            else if (accessGroupRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Precard().Name), accessGroup.Name),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new Precard().ID), accessGroup.ID,CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupNameRepeated, "نام گروه دسترسی نباید تکراری باشد", ExceptionSrc));
            }            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// در جریان ها استفاده نشده باشد
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(PrecardAccessGroup accessGroup)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            FlowRepository flowRep = new FlowRepository(false);
            int flowCount=flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Model.RequestFlow.Flow().AccessGroup), accessGroup));
            if (flowCount > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AccessGroupUsedByFlow, "گروه دسترسی در جریان مورد استفاده قرار گرفته است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
    
        /// <summary>
        /// if details list is empty means that list should not be update else 
        /// we should update the list
        /// </summary>
        /// <param name="accessGroup"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(PrecardAccessGroup accessGroup, UIActionType action)
        {
            if (action == UIActionType.ADD || action == UIActionType.EDIT)
            {
                if (!Utility.IsEmpty<Precard>(accessGroup.PrecardList))
                {
                    foreach (Precard precard in accessGroup.PrecardList)
                    {
                        precard.AccessGroupList = new List<PrecardAccessGroup>() { accessGroup };
                    }
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAccessGroupsLoadAccess_onOrganizationFlowInsert()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAccessGroupsLoadAccess_onOrganizationFlowUpdate()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAccessGroup_onOrganizationFlowInsert(string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList)
        {
            return this.InsertByProxy(AccessGroupName, AccessGroupDescription, AccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertAccessGroup_onOrganizationFlowUpdate(string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList)
        {
            return this.InsertByProxy(AccessGroupName, AccessGroupDescription, AccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAccessGroup_onOrganizationFlowInsert(decimal selectedAccessGroupID, string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList, bool IsUpdateAccessLevelsList)
        {
            return this.UpdateByProxy(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateAccessGroup_onOrganizationFlowUpdate(decimal selectedAccessGroupID, string AccessGroupName, string AccessGroupDescription, IList<AccessGroupProxy> AccessLevelsList, bool IsUpdateAccessLevelsList)
        {
            return this.UpdateByProxy(selectedAccessGroupID, AccessGroupName, AccessGroupDescription, AccessLevelsList, IsUpdateAccessLevelsList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAccessGroup_onOrganizationFlowInsert(PrecardAccessGroup accessGroup, UIActionType UAT)
        {
            return base.SaveChanges(accessGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteAccessGroup_onOrganizationFlowUpdate(PrecardAccessGroup accessGroup, UIActionType UAT)
        {
            return base.SaveChanges(accessGroup, UAT);
        }




        
    }
}
