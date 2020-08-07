using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.RequestFlow
{
    public class BUnderManagment : IManagerKartabl, IManagerReviewedRequests,ISubstituteKartabl, IUserRegisteredRequests, IOperatorRegisteredRequests
    {
        private const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BUnderManagment";
        private UnderManagmentRepository underRep = new UnderManagmentRepository(false);
        private decimal workingManagerId = 0;
        private string workingUsername = "";
        public BUnderManagment() { }

        /// <summary>
        /// تنها جهت تست
        /// </summary>
        /// <param name="personId"></param>
        public BUnderManagment(decimal personId)
        {
            this.workingManagerId = personId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal GetCurentManager()
        {
            if (workingManagerId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    this.workingManagerId = user.Person.ID;
                    this.workingUsername = user.UserName;
                }
            }
            return workingManagerId;
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="mangerId"></param>
        /// <returns></returns>
        public IList<Department> GetUnderManagmentDepartmentByManager(decimal mangerId)
        {
            if (mangerId > 0)
            {

                IList<Department> depList = new List<Department>();
                List<decimal> nodeParentChildsId = new List<decimal>();
                Manager manager = new BManager().GetByID(mangerId);
                var flows = from n in manager.ManagerFlowList
                            select n.Flow;
                IList<Flow> flowList = flows.ToList();
                foreach (Flow flow in flowList)
                {
                    foreach (UnderManagment underManagment in flow.UnderManagmentList)
                    {
                        if (underManagment.Department != null && underManagment.Department.ID > 0)
                        {
                            nodeParentChildsId.Add(underManagment.Department.ID);
                            nodeParentChildsId.AddRange(underManagment.Department.ParentPathList);
                            if (underManagment.ContainInnerChilds)
                            {
                                nodeParentChildsId = GetDepartmentChildsIdList(underManagment.Department);
                            }
                        }
                        else
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull, "بخش مربوط به افراد تحت مدیریت در هیچ صورتی نباید تهی باشد", ExceptionSrc);
                        }
                    }
                }
                nodeParentChildsId = nodeParentChildsId.GroupBy(x => x).Select(x => x.First()).ToList();
                foreach (decimal id in nodeParentChildsId)
                {
                    depList.Add(new Department() { ID = id });
                }
                return depList;
            }
            else
            {
                throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت یک اپراتور را برمیگرداند
        /// </summary>
        /// <param name="mangerId"></param>
        /// <returns></returns>
        public IList<Department> GetUnderManagmentDepartmentByOperator(decimal operatorPersonId)
        {
            if (operatorPersonId > 0)
            {
                BOperator bus = new BOperator();
                IList<Operator> opList = bus.GetOperator(operatorPersonId);
                IList<Department> depList = new List<Department>();
                List<decimal> nodeParentChildsId = new List<decimal>();

                var flows = from n in opList
                            select n.Flow;
                IList<Flow> flowList = flows.ToList();
                foreach (Flow flow in flowList)
                {
                    foreach (UnderManagment underManagment in flow.UnderManagmentList)
                    {
                        if (underManagment.Department != null && underManagment.Department.ID > 0)
                        {
                            nodeParentChildsId.Add(underManagment.Department.ID);
                            nodeParentChildsId.AddRange(underManagment.Department.ParentPathList);
                            if (underManagment.ContainInnerChilds)
                            {
                                nodeParentChildsId = GetDepartmentChildsIdList(underManagment.Department);
                            }
                        }
                        else
                        {
                            throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull, "بخش مربوط به افراد تحت مدیریت در هیچ صورتی نباید تهی باشد", ExceptionSrc);
                        }
                    }
                }
                nodeParentChildsId = nodeParentChildsId.GroupBy(x => x).Select(x => x.First()).ToList();
                foreach (decimal id in nodeParentChildsId)
                {
                    depList.Add(new Department() { ID = id });
                }
                return depList;
            }
            else
            {
                throw new IllegalServiceAccess(String.Format("این سرویس تنها توسط مدیران قابل استفاده میباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
            }
        }

        /// <summary>
        ///  بخشهای تحت مدیریت در یک جریان را برمیگرداند
        ///  لیست والدها را بدلیل انواع استفاده گوناگون از این سرویس برنمیگرداند
        ///  the ChildList property of each nod is not valid
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="containsParentNodes">لیست والدها بدلیل نمایش درخت کامل</param>
        /// <returns></returns>
        public IList<Department> GetUnderManagmentDepartmentByFlow(Flow flow, bool containsParentNodes)
        {
            try
            {
                List<Department> explicitContainsNode = new List<Department>();
                List<Department> implicitContainsNode = new List<Department>();
                List<Department> noContainsNode = new List<Department>();
                List<Department> resultContainsNode = new List<Department>();
                List<decimal> nodeParentsId = new List<decimal>();
                foreach (UnderManagment underManagment in flow.UnderManagmentList)
                {
                    if (underManagment.Department != null && underManagment.Department.ID > 0)
                    {
                        if (underManagment.Person == null || underManagment.Person.ID == 0)
                        {
                            if (underManagment.Contains)
                            {
                                explicitContainsNode.Add(underManagment.Department);
                                if (containsParentNodes)
                                    nodeParentsId.AddRange(underManagment.Department.ParentPathList);
                                if (underManagment.ContainInnerChilds)
                                {
                                    implicitContainsNode.AddRange(GetDepartmentChildList(underManagment.Department));
                                }
                            }
                            else
                            {
                                noContainsNode.Add(underManagment.Department);
                                if (underManagment.ContainInnerChilds)
                                {
                                    noContainsNode.AddRange(GetDepartmentChildList(underManagment.Department));
                                }
                            }
                        }
                        else if (underManagment.Contains)//اگر شخص باید تحت مدیریت باشد آنگاه بخش او را اضافه کن
                        {
                            explicitContainsNode.Add(underManagment.Department);
                            if (containsParentNodes)
                                nodeParentsId.AddRange(underManagment.Department.ParentPathList);
                        }
                    }
                    else
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull, "بخش مربوط به افراد تحت مدیریت در هیچ صورتی نباید تهی باشد", ExceptionSrc);
                    }
                }
                explicitContainsNode = explicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();
                implicitContainsNode = implicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();
                nodeParentsId = nodeParentsId.GroupBy(x => x).Select(x => x.First()).ToList();

                foreach (decimal id in nodeParentsId)
                {
                    Department dep = new Department() { ID = id };
                    explicitContainsNode.Add(dep);
                }
                foreach (Department node in noContainsNode)
                {
                    implicitContainsNode.Remove(node);
                }
                resultContainsNode.AddRange(explicitContainsNode);
                resultContainsNode.AddRange(implicitContainsNode);
                resultContainsNode = resultContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();
                return resultContainsNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Person> GetPersonsByDepartment(IList<PersonDepartmentProxy> prsList)
        {
            try
            {
                List<Department> explicitContainsNode = new List<Department>();
                List<Department> implicitContainsNode = new List<Department>();
                List<Department> containsNode = new List<Department>();
                List<Person> containsPersons = new List<Person>();
                List<Person> persons = new List<Person>();
                foreach (PersonDepartmentProxy underManagment in prsList)
                {
                    if (underManagment.DepartmentId != null && underManagment.DepartmentId > 0)
                    {
                        if (underManagment.PersonId == null || underManagment.PersonId == 0)
                        {
                            Department department = new BDepartment().GetByID(underManagment.DepartmentId);
                            explicitContainsNode.Add(department);
                            if (underManagment.ContainsInnerchilds)
                            {
                                implicitContainsNode.AddRange(GetDepartmentChildList(department));
                            }
                        }
                        else
                        {
                            Person person = new PersonRepository(false).GetById(underManagment.PersonId, false);
                            containsPersons.Add(person);
                        }
                    }
                    else
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull, "بخش مربوط به افراد انتخابی بوسیله بخش در هیچ صورتی نباید تهی باشد", ExceptionSrc);
                    }
                }
                explicitContainsNode = explicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();
                implicitContainsNode = implicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                containsNode.AddRange(explicitContainsNode);
                containsNode.AddRange(implicitContainsNode);
                containsNode = containsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                foreach (Department dep in containsNode)
                {
                    persons.AddRange(dep.PersonList);
                }
                foreach (Person prs in containsPersons)
                {
                    persons.Add(prs);
                }

                persons = persons.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                return persons;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUnderManagment", "GetPersonsByDepartment");
                throw ex;
            }
        }

        /// <summary>
        ///  پرسنل تحت مدیریت در یک جریان را برمیگرداند
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public IList<Person> GetUnderManagmentPersonsByFlow(Flow flow)
        {
            try
            {
                List<Department> explicitContainsNode = new List<Department>();
                List<Department> implicitContainsNode = new List<Department>();
                List<Department> noContainsNode = new List<Department>();
                List<Department> containsNode = new List<Department>();
                List<Person> containsPersons = new List<Person>();
                List<Person> noContainsPersons = new List<Person>();
                List<Person> persons = new List<Person>();
                foreach (UnderManagment underManagment in flow.UnderManagmentList)
                {
                    if (underManagment.Department != null && underManagment.Department.ID > 0)
                    {
                        if (underManagment.Person == null || underManagment.Person.ID == 0)
                        {
                            if (underManagment.Contains)
                            {
                                explicitContainsNode.Add(underManagment.Department);
                                if (underManagment.ContainInnerChilds)
                                {
                                    implicitContainsNode.AddRange(GetDepartmentChildList(underManagment.Department));
                                }
                            }
                            else
                            {
                                noContainsNode.Add(underManagment.Department);
                                if (underManagment.ContainInnerChilds)
                                {
                                    noContainsNode.AddRange(GetDepartmentChildList(underManagment.Department));
                                }
                            }
                            foreach (Department node in noContainsNode)
                            {
                                implicitContainsNode.Remove(node);
                            }
                        }
                        else if (underManagment.Contains)
                        {
                            containsPersons.Add(underManagment.Person);
                        }
                        else
                        {
                            noContainsPersons.Add(underManagment.Person);
                        }
                    }
                    else
                    {
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UnderManagmentDepartmentNull, "بخش مربوط به افراد تحت مدیریت در هیچ صورتی نباید تهی باشد", ExceptionSrc);
                    }
                }
                explicitContainsNode = explicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();
                implicitContainsNode = implicitContainsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                containsNode.AddRange(explicitContainsNode);
                containsNode.AddRange(implicitContainsNode);
                containsNode = containsNode.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                foreach (Department dep in containsNode)
                {
                    persons.AddRange(dep.PersonList);
                }
                foreach (Person prs in containsPersons)
                {
                    persons.Add(prs);
                }

                persons = persons.GroupBy(x => x.ID).Select(x => x.First()).ToList();

                foreach (Person prs in noContainsPersons)
                {
                    persons.Remove(prs);
                }
                return persons;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUnderManagment", "GetUnderManagmentPersonsByFlow");
                throw ex;
            }
        }

        /// <summary>
        /// لیست افراد تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="mangerId"></param>
        /// <returns></returns>
        public IList<Person> GetUnderManagmentPersonsByManager(decimal mangerId)
        {
            List<Person> result = new List<Person>();
            if (mangerId > 0)
            {

                IList<Department> depList = new List<Department>();
                List<decimal> nodeParentChildsId = new List<decimal>();
                Manager manager = new BManager().GetByID(mangerId);
                var flows = from n in manager.ManagerFlowList
                            select n.Flow;
                IList<Flow> flowList = flows.ToList();
                foreach (Flow flow in flowList)
                {
                    result.AddRange(this.GetUnderManagmentPersonsByFlow(flow));
                }
            }
            return result;
        }

        /// <summary>
        /// شناسه گرهای بچه را استخراج میکند
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<decimal> GetDepartmentChildsIdList(Department node)
        {
            BDepartment bDepartment = new BDepartment();
            IList<Department> departmentchilds;
            List<decimal> childIds = new List<decimal>();
            DepartmentRepository depRep = new DepartmentRepository(false);
            departmentchilds = bDepartment.GetDepartmentChildsByParentPath(node.ID);
            if (departmentchilds == null || departmentchilds.Count == 0)
            {
                return new List<decimal>();
            }
            else
            {
                var ids = from n in departmentchilds select n.ID;
                childIds.AddRange(ids.ToList<decimal>());
                return childIds;
            }
        }

        /// <summary>
        /// همه بچهای یک گره را بصورت بازگشتی برمیگرداند 
        /// </summary>
        /// <param name="childs"></param>
        /// <param name="node"></param>
        private List<Department> GetDepartmentChildList(Department node)
        {
            try
            {
                BDepartment bDepartment = new BDepartment();
                IList<Department> departmentchilds;
                DepartmentRepository depRep = new DepartmentRepository(false);
                departmentchilds = bDepartment.GetDepartmentChildsByParentPath(node.ID);
                if (departmentchilds == null || departmentchilds.Count == 0)
                {
                    return new List<Department>();
                }
                else
                {
                    return departmentchilds.ToList();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUnderManagment", "GetDepartmentChildList");
                throw ex;
            }
        }


        #region ISubstituteKartabl Members
/*
        int ISubstituteKartabl.GetRequestCount(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            int result = requestRep.GetAllSubstituteKartablCount(fromDate, toDate, requestType, managerId, personId, null);
            return result;
        }
        */
        int ISubstituteKartabl.GetRequestCount(decimal managerId, decimal personId, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            int result = requestRep.GetAllSubstituteKartablCount(fromDate, toDate, managerId, personId);
            return result;
        }
/*
        int ISubstituteKartabl.GetRequestCount(decimal managerId, decimal personId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            var ids = from o in quickSearchUnderMnagment
                      select o.ID;
            int result = requestRep.GetAllSubstituteKartablCount(fromDate, toDate, RequestType.None, managerId, personId, ids.ToArray());
            return result;
        }

        IList<InfoRequest> ISubstituteKartabl.GetAllRequests(decimal managerId, decimal personId, IList<Person> quickSearchUnderMnagment, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            RequestRepository requestRep = new RequestRepository(false);
            var ids = from o in quickSearchUnderMnagment
                      select o.ID;
            IList<InfoRequest> result = requestRep.GetAllSubstituteKartabl(fromDate, toDate, RequestType.None, managerId, personId, pageIndex, pageSize, ids.ToArray(), orderby);
            return result;
        }

        IList<InfoRequest> ISubstituteKartabl.GetAllRequests(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> result = requestRep.GetAllSubstituteKartabl(fromDate, toDate, requestType, managerId, personId, pageIndex, pageSize, null, orderby);
            return result;
        }

        IList<InfoRequest> ISubstituteKartabl.GetAllRequests(decimal managerId, decimal personId)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> result = requestRep.GetAllSubstituteKartabl(managerId, personId, KartablOrderBy.PersonCode);
            return result;
        }

        int ISubstituteKartabl.GetRequestCountByFilter(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, IList<RequestFliterProxy> fliters)
        {
            throw new NotImplementedException();
        }

        IList<InfoRequest> ISubstituteKartabl.GetAllRequestsByFilter(decimal managerId, decimal personId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, IList<RequestFliterProxy> fliters, KartablOrderBy orderby)
        {
            throw new NotImplementedException();
        }*/

        #endregion
    
        #region IManagerKartabl Members

        int IManagerKartabl.GetRequestCount(decimal managerId, RequestType requestType, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            //int result = requestRep.GetAllManagerKartablCount(fromDate, toDate, requestType, managerId, null);
            //return result;

            //int result1 = requestRep.GetAllManagerKartablItemsCount(fromDate, toDate, requestType, managerId);
            //int result2 = requestRep.GetAllSubstituteKartablItemsCount(fromDate, toDate, requestType, BUser.CurrentUser.Person.ID);
            //return result1 + result2;

            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllKartablItems(fromDate, toDate, RequestType.None, managerList, KartablOrderBy.None);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);

            return requests.Count;
        }

        /// <summary>
        /// جستجو بر اساس 
        /// نام شخص
        /// شماره پرسنلی
        /// پیشکارت
        /// تاریخ ثبت ,شروع ,پایان
        /// توضیح درخواست
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="searchKey"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        int IManagerKartabl.GetRequestCount(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate)
        {
            //int result = requestRep.GetAllManagerKartablCount(fromDate, toDate, RequestType.None, managerId, ids.ToArray());
            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            RequestRepository requestRep = new RequestRepository(false);

            IList<InfoRequest> requests = requestRep.GetAllKartablItems(fromDate, toDate, RequestType.None, managerList, KartablOrderBy.None);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);

            if (!Utility.IsEmpty(searchKey))
            {
                requests = this.AppllyQuickSearch(searchKey, requests);
            }          

            return requests.Count;
        }

        IList<InfoRequest> IManagerKartabl.GetAllRequests(decimal managerId, RequestType requestType, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            //IList<InfoRequest> result = requestRep.GetAllManagerKartabl(fromDate, toDate, requestType, managerId, pageIndex, pageSize, null, orderby);

            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> requests = requestRep.GetAllKartablItems(fromDate, toDate, requestType, managerList, orderby);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);
            return requests.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        IList<InfoRequest> IManagerKartabl.GetAllRequests(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            RequestRepository requestRep = new RequestRepository(false);
            //var ids = from o in quickSearchUnderMnagment
            //          select o.ID;
            //IList<InfoRequest> result = requestRep.GetAllManagerKartabl(fromDate, toDate, RequestType.None, managerId, pageIndex, pageSize, ids.ToArray(), orderby);

            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllKartablItems(fromDate, toDate, RequestType.None, managerList, orderby);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);

            foreach (InfoRequest req in requests)
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                    req.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                    req.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                }
                else
                {
                    req.RegistrationDate = Utility.ToString(req.RegisterDate);
                    req.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                    req.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                }
            }

            if (!Utility.IsEmpty(searchKey))
            {
                requests = this.AppllyQuickSearch(searchKey, requests);
            }
            return requests.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        IList<InfoRequest> IManagerKartabl.GetAllRequests(decimal managerId)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> result = requestRep.GetAllManagerKartabl(managerId, KartablOrderBy.PersonCode);
            return result;
        }

        #endregion

        #region IManagerReviewedRequests Members

        int IManagerReviewedRequests.GetRequestCount(decimal managerId, RequestState requestStatus, DateTime fromDate, DateTime toDate)
        {

            RequestRepository requestRep = new RequestRepository(false);
            //int result = requestRep.GetAllManagerReviewdCount(fromDate, toDate, requestStatus, managerId, null);
            //return result;
            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllReviewdKartablItems(fromDate, toDate, requestStatus, managerList, KartablOrderBy.RequestDate);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);
            return requests.Count;
        }

        int IManagerReviewedRequests.GetRequestCount(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            //var ids = from o in quickSearchUnderMnagment
            //          select o.ID;
            //int result = requestRep.GetAllManagerReviewdCount(fromDate, toDate, RequestState.UnKnown, managerId, ids.ToArray());
            //return result;
            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllReviewdKartablItems(fromDate, toDate, RequestState.UnKnown, managerList, KartablOrderBy.RequestDate);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);

            if (!Utility.IsEmpty(searchKey))
            {
                requests = this.AppllyQuickSearch(searchKey, requests);
            }
            return requests.Count;
        }

        IList<InfoRequest> IManagerReviewedRequests.GetAllRequests(decimal managerId, string searchKey, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            RequestRepository requestRep = new RequestRepository(false);
            //var ids = from o in quickSearchUnderMnagment
            //          select o.ID;
            //IList<InfoRequest> result = requestRep.GetAllManagerReviewd(fromDate, toDate, RequestState.UnKnown, managerId, pageIndex, pageSize, ids.ToArray(), orderby);

            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllReviewdKartablItems(fromDate, toDate, RequestState.UnKnown, managerList, orderby);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);

            foreach (InfoRequest req in requests)
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    req.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                    req.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                    req.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                }
                else
                {
                    req.RegistrationDate = Utility.ToString(req.RegisterDate);
                    req.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                    req.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                }
            }
            if (!Utility.IsEmpty(searchKey))
            {
                requests = this.AppllyQuickSearch(searchKey, requests);
            }
            return requests.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        IList<InfoRequest> IManagerReviewedRequests.GetAllRequests(decimal managerId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            RequestRepository requestRep = new RequestRepository(false);
            //IList<InfoRequest> result = requestRep.GetAllManagerReviewd(fromDate, toDate, requestState, managerId, pageIndex, pageSize, null, orderby);
            //return result;

            IList<decimal> managerList = new List<decimal>();
            BSubstitute subBus = new BSubstitute();
            IList<Substitute> substituteList = subBus.GetSubstitute(BUser.CurrentUser.Person.ID);
            if (substituteList.Count > 0)
            {
                managerList = subBus.GetSubstituteManagerList(BUser.CurrentUser.Person.ID);
            }
            managerList.Add(managerId);

            IList<InfoRequest> requests = requestRep.GetAllReviewdKartablItems(fromDate, toDate, requestState, managerList, orderby);
            requests = this.AppllySubstituteAccess(managerId, requests, substituteList);
            return requests.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        #endregion

        #region IUserRegisteredRequests Members

        IList<InfoRequest> IUserRegisteredRequests.GetAllRequests(decimal personId, RequestState requestState, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IUserRegisteredRequests bus = new BUnderManagment();
            IList<InfoRequest> result = requestRep.GetAllUserRequest(fromDate, toDate, requestState, personId);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="requestStatus"></param>
        /// <param name="fromDate">تاریخ تایید یا عدم تایید درخواست</param>
        /// <param name="toDate">تاریخ تایید یا عدم تایید درخواست</param>
        /// <returns></returns>
        int IUserRegisteredRequests.GetRequestCount(decimal personId, RequestState requestStatus, DateTime fromDate, DateTime toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            int result = requestRep.GetAllUserRequestCount(fromDate, toDate, requestStatus, personId);
            return result;
        }

        IList<InfoRequest> IUserRegisteredRequests.GetAllRequests(decimal personId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> result = requestRep.GetAllUserRequestByPagging(fromDate, toDate, requestState, personId, pageIndex, pageSize);
            return result;
        }

        #region filter
        int IUserRegisteredRequests.GetRequestCountByFilter(decimal personId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate)
        {
            RequestRepository requestRep = new RequestRepository(false);
            int count = requestRep.GetAllUserRequestCount(fromDate, toDate, requestType, submiter, personId);
            return count;
        }

        IList<InfoRequest> IUserRegisteredRequests.GetAllRequestsByFilter(decimal personId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> list = requestRep.GetAllUserFilterRequestByPagging(fromDate, toDate, requestType, submiter, personId, pageIndex, pageSize);
            return list;
        }

        #endregion

        #endregion

        #region IOperatorRegisteredRequests Members

        int IOperatorRegisteredRequests.GetRequestCount(decimal personId, decimal userId, RequestState requestStatus, DateTime fromDate, DateTime toDate)
        {
            //IList<decimal> personIds = this.GetOperatorUnderManagment(personId);

            RequestRepository requestRep = new RequestRepository(false);
            int result = requestRep.GetAllOperatorRequestCount(fromDate, toDate, requestStatus, personId);
            return result;
        }

        IList<InfoRequest> IOperatorRegisteredRequests.GetAllRequests(decimal personId, decimal userId, RequestState requestState, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {
            //IList<decimal> personIds = this.GetOperatorUnderManagment(personId);
            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> result = requestRep.GetAllOperatorRequestByPagging(fromDate, toDate, requestState, personId, pageIndex, pageSize);
            return result;
        }

        int IOperatorRegisteredRequests.GetRequestCountByFilter(decimal personId, decimal userId, decimal undermanagmentPersonId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate)
        {
            //IList<decimal> personIds = this.GetOperatorUnderManagment(personId);
            if (fromDate == null)
            {
                fromDate = DateTime.Now.AddYears(-2);
            }
            if (toDate == null)
            {
                toDate = DateTime.Now.AddYears(1);
            }

            RequestRepository requestRep = new RequestRepository(false);
            int count = requestRep.GetAllOperatorRequestCount(fromDate, toDate, requestType, submiter, personId, userId, undermanagmentPersonId);
            return count;

        }

        IList<InfoRequest> IOperatorRegisteredRequests.GetAllRequestsByFilter(decimal personId, decimal userId, decimal undermanagmentPersonId, RequestType? requestType, RequestSubmiter? submiter, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            //IList<decimal> personIds = this.GetOperatorUnderManagment(personId);
            if (fromDate == null)
            {
                fromDate = DateTime.Now.AddYears(-2);
            }
            if (toDate == null)
            {
                toDate = DateTime.Now.AddYears(1);
            }

            RequestRepository requestRep = new RequestRepository(false);
            IList<InfoRequest> list = requestRep.GetAllOperatorRequestByPagging(fromDate, toDate, requestType, submiter, personId, userId, undermanagmentPersonId, pageIndex, pageSize);
            return list;
        }

        #endregion


        /// <summary>
        /// افراد تحت مدیریت اپراتوری که کاربر فعلی سیستم است را برمیگرداند
        /// </summary>
        /// <returns></returns>
        private IList<decimal> GetOperatorUnderManagment1(decimal personId)
        {
            IList<Operator> opList = new BOperator().GetOperator(personId);
            List<decimal> personIds = new List<decimal>();
            foreach (Operator op in opList)
            {
                IList<Person> personList = new BUnderManagment().GetUnderManagmentPersonsByFlow(op.Flow);
                var ids = from person in personList
                          select person.ID;
                personIds.AddRange(ids.ToList<decimal>());
            }
            return personIds;
        }

        /// <summary>
        /// اعمال تاریخ و دسترسی پیشکارت جانشین
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="substituteList"></param>
        /// <returns></returns>
        private IList<InfoRequest> AppllySubstituteAccess(decimal managerId, IList<InfoRequest> requests, IList<Substitute> substituteList)
        {
            BSubstitute subBus = new BSubstitute();
            if (substituteList.Count > 0)
            {
                IList<Precard> access = substituteList.First().PrecardList;
                requests = requests.Where(x => access.Where(y => y.ID == x.PrecardID).Count() > 0).ToList();
                requests = requests.Where(x => substituteList.Where(y => (y.Manager.ID == x.ManagerID && y.FromDate <= x.RegisterDate && y.ToDate >= x.RegisterDate) || x.ManagerID == managerId).Count() > 0).ToList();
            }
            return requests;
        }

        /// <summary>
        /// جستجودر بین درخواستهای ارسالی
        /// کد پرسنلی
        /// نام پرسنل
        /// نام پیشکارت
        /// توضیح درخواست
        /// تاریخ قبت
        /// تاریخ شروزع و پایان درخواست
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="requests"></param>
        /// <returns></returns>
        private IList<InfoRequest> AppllyQuickSearch(string searchKey, IList<InfoRequest> requests)
        {
            if (!Utility.IsEmpty(searchKey))
            {
                searchKey = searchKey.ToLower();
                requests = requests.Where(x =>
                    Utility.GetValue(x.PersonCode).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.Applicant).ToLower().Contains(searchKey) ||
                    (Utility.GetValue(x.ApplicantFirstName) + " " + Utility.GetValue(x.ApplicantLastName)).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.PrecardName).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.Description).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.TheFromDate).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.TheToDate).ToLower().Contains(searchKey) ||
                    Utility.GetValue(x.RegistrationDate).ToLower().Contains(searchKey)).ToList();
            }
            return requests;
        }
    }
}
