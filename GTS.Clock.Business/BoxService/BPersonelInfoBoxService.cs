using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.Charts;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business.BoxService
{
    public class BPersonelInfoBoxService : MarshalByRefObject
    {
        /// <summary>
        /// جهت واکشی تصویر پرسنل
        /// </summary>
        public decimal CurrentPersonId
        {
            get
            {
                return BUser.CurrentUser.Person.ID;
            }
        }

        public IList<PersonInfoProxy> GetPersonInfo()
        {
            try
            {
                IList<PersonInfoProxy> list = new List<PersonInfoProxy>();

                if (BUser.CurrentUser.Person != null)
                {
                    Person currentPrs = new BPerson().GetByID(BUser.CurrentUser.Person.ID);
                    PersonInfoProxy proxy = new PersonInfoProxy();

                    #region Person Name
                    proxy.Active = true;
                    proxy.Order = 1;
                    proxy.Value = BUser.CurrentUser.Person.Name;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.Title = "نام";
                    }
                    else
                    {
                        proxy.Title = "Name";
                    }
                    proxy.Title += "<BR/>";
                    list.Add(proxy);
                    #endregion

                    #region Person Code
                    proxy = new PersonInfoProxy();
                    proxy.Active = true;
                    proxy.Order = 2;
                    proxy.Value = currentPrs.PersonCode;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.Title = "شماره پرسنلی";
                    }
                    else
                    {
                        proxy.Title = "Person Code";
                    }
                    proxy.Title += "<BR/>";
                    list.Add(proxy);
                    #endregion

                    #region Employment Date
                    proxy = new PersonInfoProxy();
                    proxy.Active = true;
                    proxy.Order = 3;

                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.Value = Utility.ToPersianDate(currentPrs.EmploymentDate);
                        proxy.Title = "تاریخ استخدام";
                    }
                    else
                    {
                        proxy.Value = Utility.ToString(currentPrs.EmploymentDate);
                        proxy.Title = "Employ Date";
                    }
                    if (currentPrs.EmploymentDate == Utility.GTSMinStandardDateTime)
                    {
                        proxy.Value = " --- ";
                    }
                    else
                    {
                        proxy.Title += "<BR/>";
                        list.Add(proxy);
                    }
                    #endregion

                    #region Department Name
                    if (currentPrs.Department != null)
                    {
                        proxy = new PersonInfoProxy();
                        proxy.Active = true;
                        proxy.Order = 4;
                        proxy.Value = currentPrs.Department.Name;
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            proxy.Title = "نام بخش";
                        }
                        else
                        {
                            proxy.Title = "Department Name";
                        }
                        if (!Utility.IsEmpty(proxy.Value))
                        {
                            proxy.Title += "<BR/>";
                            list.Add(proxy);
                        }
                    }
                    #endregion

                    #region Organization Unit Name
                    /*
                    if (currentPrs.OrganizationUnit != null)
                    {
                        proxy = new PersonInfoProxy();
                        proxy.Active = true;
                        proxy.Order = 5;
                        proxy.Value = currentPrs.OrganizationUnit.Name;
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            proxy.Title = "سمت";
                        }
                        else
                        {
                            proxy.Title = "Organization Unit Name";
                        }
                        list.Add(proxy);
                    }
                     * */
                    #endregion

                    #region Flow Manager
                    GTS.Clock.Infrastructure.Repository.RequestStatusRepositiory reqStatusRep = new GTS.Clock.Infrastructure.Repository.RequestStatusRepositiory(false);
                    IList<RegisteredRequestsFlowLevel> levels = reqStatusRep.GetRequestLevels(currentPrs.Department.ID, currentPrs.ID);
                    if (levels != null && levels.Count > 0) 
                    {
                        levels = levels.Where(x => x.FlowID == levels.First().FlowID).OrderBy(x => x.ManagerLevel).ToList();
                        
                        proxy = new PersonInfoProxy();
                        proxy.Active = true;
                        proxy.Order = 5;
                        var managers = from manager in levels select manager.ManagerName;

                        proxy.Value = string.Join(" , ", managers.ToArray<string>());
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {                            
                            proxy.Title = "مدیران";
                        }
                        else
                        {
                            proxy.Title = "Managers";
                        }
                        proxy.Title += "<BR/>";
                        list.Add(proxy);
                    }
                   
                    #endregion
                }
                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BPersonInfoBoxService", "GetPersonInfo");
                throw new Exception("خطا در بارگزاری اطلاعات شخصی");
            }
        }
    }
}

