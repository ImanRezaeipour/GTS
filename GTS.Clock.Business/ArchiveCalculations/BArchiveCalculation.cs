using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using System.Reflection;

namespace GTS.Clock.Business.ArchiveCalculations
{
    public class BArchiveCalculator : MarshalByRefObject
    {
        const string ExceptionSrc = "GTS.Clock.Business.ArchiveCalculations.BArchiveCalculator"; 
        const string Empty = "-1000";
        ISearchPerson searchTool = new BPerson();
        ArchiveConceptsRepository archiveRep = new ArchiveConceptsRepository();
        EntityRepository<ArchiveFieldMap> mapRep = new EntityRepository<ArchiveFieldMap>(false);

        #region UI Meta Data
       
        public IList<ArchiveFieldMap> GetArchiveGridSettings()
        {
            try
            {
                IList<ArchiveFieldMap> mapList = mapRep.GetAll();
                foreach (ArchiveFieldMap map in mapList) 
                {
                    if (Utility.IsEmpty(map.PId)) 
                    {
                        map.Visible = false;
                    }
                    else if (!map.PId.ToLower().StartsWith("p")) 
                    {
                        map.Visible = false;
                    }

                    if (map.Visible) 
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            map.Title = map.FnTitle;
                            map.ColumnSize = map.FnColumnSize;
                        }
                        else 
                        {
                            map.Title = map.EnTitle;
                            map.ColumnSize = map.EnColumnSize;
                        }
                    }
                }

                return mapList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        public void SetArchiveGridSettings(IList<ArchiveFieldMap> mapList)
        {
            try
            {
                IList<ArchiveFieldMap> orginalMapList = mapRep.GetAll();
                foreach (ArchiveFieldMap changedMap in mapList)
                {
                    ArchiveFieldMap map = orginalMapList.Where(x => x.PId == changedMap.PId).FirstOrDefault();
                    if (map == null)
                        continue;
                    if (map.Visible != changedMap.Visible)
                    {
                        map.Visible = changedMap.Visible;
                        mapRep.Update(map);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        #endregion

        #region Count & Paging
    
        public int GetSearchCount(string searchKey, int year, int month) 
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();
                int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
                IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> personIds = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);
                return personIds.Count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        public int GetSearchCount(PersonAdvanceSearchProxy proxy, int year, int month)
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();                
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> personIds = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);
                return personIds.Count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }
   /*  
        public IList<Person> GetSearchResult(string searchKey, int year, int month, int pageIndex, int pageSize)
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();
                int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
                IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

                personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                return personList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        public IList<Person> GetSearchResult(PersonAdvanceSearchProxy proxy, int year, int month, int pageIndex, int pageSize)
        {
            try
            {
                int rangeOrder = month;
                ISearchPerson searchTool = new BPerson();
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
                var ids = from o in personList
                          select o.ID;
                IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

                personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                return personList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }
     */
        #endregion

        #region Check Archive & Archive

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا آرشیو شده اند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public ArchiveExsitsConditions IsArchiveExsits(int year, int month, decimal personId)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا آرشیو شده اند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public ArchiveExsitsConditions IsArchiveExsits(int year, int month, string searchKey)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا آرشیو شده اند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public ArchiveExsitsConditions IsArchiveExsits(int year, int month, PersonAdvanceSearchProxy proxy)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            return this.IsArchiveExsits(year, month, list);
        }

        /// <summary>
        /// پس از حذف آرشیو قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool ArchiveData(int year, int month, decimal personId,bool overwrite)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        /// <summary>
        /// پس از حذف آرشیو قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public bool ArchiveData(int year, int month, string searchKey, bool overwrite)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        /// <summary>
        /// پس از حذف آرشیو قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public bool ArchiveData(int year, int month, PersonAdvanceSearchProxy proxy, bool overwrite)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            bool result = this.ArchiveData(year, month, list, overwrite);
            return result;
        }

        #endregion

        #region Load Archive Values

        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, decimal personId)
        {
            Person prs = new BPerson().GetByID(personId);
            IList<Person> list = new List<Person>();
            list.Add(prs);

            return this.GetArchiveValues(year, month, list);
        }

        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, string searchKey, int pageIndex, int pageSize)
        {
            int rangeOrder = month;
            ISearchPerson searchTool = new BPerson();
            int totalCount = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> personList = searchTool.QuickSearchByPage(0, totalCount, searchKey);
            var ids = from o in personList
                      select o.ID;
            IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

            personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return this.GetArchiveValues(year, month, personList);
        }

        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            //int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            //IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            int rangeOrder = month;
            ISearchPerson searchTool = new BPerson();
            IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy);
            var ids = from o in personList
                      select o.ID;
            IList<decimal> existsPersonArchiveList = archiveRep.GetExsitsArchivePersons(ids.ToList(), year, rangeOrder);

            personList = personList.Where(x => existsPersonArchiveList.Any(y => y == x.ID))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return this.GetArchiveValues(year, month, personList);
        }

        #endregion

        #region Edit Data

        public void SetArchiveValues(int year, int month, decimal personId, ArchiveCalcValuesProxy proxy)
        {
            IList<ArchiveCalcValuesProxy> resultList = new List<ArchiveCalcValuesProxy>();
            int rangeOrder = month;           

            IList<ArchiveConceptValue> archiveValues = archiveRep.LoadArchiveValueList(new List<decimal>() { personId }, year, rangeOrder);

            IList<ArchiveFieldMap> fildsMapList = mapRep.GetAll();

            if (archiveValues.Any(x => x.PersonId == personId))
            {
                ArchiveFieldMap map = null;
                MemberInfo[] members = proxy.GetType().GetMembers();

                #region Data Type Validatin
                UIValidationExceptions exceptions = new UIValidationExceptions();
                foreach (MemberInfo member in members.Where(a => a.MemberType == MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);

                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    if (map != null)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            bool isValid = false;
                            string pValue = Utility.ToString(prop.GetValue(proxy, null));
                            int value = 0;
                            string cnpName = "";
                            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                            {
                                cnpName = map.FnTitle;
                            }
                            else
                            {
                                cnpName = map.EnTitle;
                            }
                            switch (cnpValue.Concept.DataType)
                            {
                                case ConceptDataType.Int:
                                    if (!Utility.IsNumber(pValue))
                                    {                                       
                                        exceptions.Add(new ValidationException(ExceptionResourceKeys.ArchiveDataTypeIsNotValid, cnpName, ExceptionSrc));
                                    }
                                    break;
                                case ConceptDataType.Hour:
                                    if (!Utility.IsTime(pValue))
                                    {
                                        exceptions.Add(new ValidationException(ExceptionResourceKeys.ArchiveDataTypeIsNotValid, cnpName, ExceptionSrc));
                                    }
                                    break;
                            }
                            if (isValid)
                            {
                                cnpValue.ChangedValue = value;
                                archiveRep.SaveOrUpdate(cnpValue);
                            }
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                #region Set Value
                foreach (MemberInfo member in members.Where(a => a.MemberType == MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);

                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    if (map != null)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            bool isValid = false;
                            string pValue = Utility.ToString(prop.GetValue(proxy, null));
                            int value = 0;
                            switch (cnpValue.Concept.DataType)
                            {
                                case ConceptDataType.Int:
                                    if (Utility.IsNumber(pValue))
                                    {
                                        value = Utility.ToInteger(pValue);
                                        isValid = true;
                                    }
                                    break;
                                case ConceptDataType.Hour:
                                    if (Utility.IsTime(pValue))
                                    {
                                        value = Utility.RealTimeToIntTime(pValue);
                                        isValid = true;
                                    }
                                    break;
                            }
                            if (isValid)
                            {
                                cnpValue.ChangedValue = value;
                                archiveRep.SaveOrUpdate(cnpValue);
                            }
                        }
                    }
                } 
                #endregion
            }


        }

        #endregion

        #region Private Services

        /// <summary>
        /// پس از حذف آرشیو قبلی , دادههای جدید را کپی میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personList"></param>
        /// <returns></returns>
        private bool ArchiveData(int year, int month, IList<Person> personList, bool overwrite)
        {
            try
            {
                DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                int rangeOrder = month;

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                }
                var ids = from o in personList
                          select o.ID;
                if (overwrite)
                {
                    archiveRep.DeleteArchiveValues(ids.ToList(), year, month);
                }
                foreach (Person person in personList)
                {
                    if (!overwrite && this.IsArchiveExsits(year, month, person.ID) != ArchiveExsitsConditions.NotExsitds)
                    {
                        continue;
                    }
                    archiveRep.ArchiveConceptValues(person.ID, year, rangeOrder, date);
                }
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// آیا داده های اشخاص مشخص شده قبلا آرشیو شده اند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personList"></param>
        /// <returns></returns>
        private ArchiveExsitsConditions IsArchiveExsits(int year, int month, IList<Person> personList)
        {
            try
            {
                int rangeOrder = month;
                var ids = from o in personList
                          select o.ID;
                int count = archiveRep.ExsitsArchiveCount(ids.ToList(), year, rangeOrder);
                if (count == 0)
                    return ArchiveExsitsConditions.NotExsitds;
                else if (count >= ids.Count())
                {
                    return ArchiveExsitsConditions.AllExsits;
                }
                else return ArchiveExsitsConditions.SomeExsits;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// آرشیو را بارگزاری میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personList"></param>
        /// <returns></returns>
        public IList<ArchiveCalcValuesProxy> GetArchiveValues(int year, int month, IList<Person> personList)
        {
            try
            {
                IList<ArchiveCalcValuesProxy> resultList = new List<ArchiveCalcValuesProxy>();                
                DateTime date = new DateTime(year, month, Utility.GetEndOfMiladiMonth(year, month));
                int rangeOrder = month;

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, Utility.GetEndOfPersianMonth(year, month)));
                }

                var ids = from o in personList
                          select o.ID;
                IList<ArchiveConceptValue> archiveValues = archiveRep.LoadArchiveValueList(ids.ToList(), year, rangeOrder);

                IList<ArchiveFieldMap> fildsMapList = mapRep.GetAll();

                foreach (Person prs in personList)
                {
                    if (archiveValues.Any(x => x.PersonId == prs.ID))
                    {
                        ArchiveCalcValuesProxy proxy = new ArchiveCalcValuesProxy() { PersonCode = prs.PersonCode, PersonName = prs.Name };
                        proxy = SetMapValue(proxy, prs.ID, archiveValues, fildsMapList);
                        resultList.Add(proxy);
                    }
                }

                return resultList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// فیلدهایی که باید مقدار بگیرند را مقدار دهی میکند
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="personId"></param>
        /// <param name="archiveValues"></param>
        /// <param name="fildsMapList"></param>
        /// <returns></returns>
        private ArchiveCalcValuesProxy SetMapValue(ArchiveCalcValuesProxy proxy, decimal personId, IList<ArchiveConceptValue> archiveValues, IList<ArchiveFieldMap> fildsMapList)
        {
            try
            {
                ArchiveFieldMap map = null;
                MemberInfo[] members = proxy.GetType().GetMembers();

                foreach (MemberInfo member in members.Where(a =>a.MemberType==MemberTypes.Property && !a.Name.ToLower().Contains("person")))
                {
                    PropertyInfo prop = typeof(ArchiveCalcValuesProxy).GetProperty(member.Name);
                    map = fildsMapList.Where(x => x.PId.ToLower().Equals(member.Name.ToLower())).FirstOrDefault();
                    prop.SetValue(proxy, Empty, null);

                    if (map != null)
                    {
                        ArchiveConceptValue cnpValue = archiveValues.Where(x => x.PersonId == personId && x.Concept.KeyColumnName.ToLower().Equals(map.ConceptKeyColumn.ToLower())).FirstOrDefault();

                        if (cnpValue != null)
                        {
                            string value = Utility.ToString(cnpValue.ChangedValue);
                            switch(cnpValue.Concept.DataType) 
                            {
                                case ConceptDataType.Int:
                                    value = Utility.ToInteger(value).ToString();
                                break;
                                case ConceptDataType.Hour:
                                value = Utility.IntTimeToTime(Utility.ToInteger(value));
                                break;
                            }
                            prop.SetValue(proxy, value, null);
                        }
                    }
                }       

                return proxy;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        #endregion
    }
}
