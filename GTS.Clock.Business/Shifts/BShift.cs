using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Concepts.Operations;
using System.Reflection;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Shifts
{
    public class BShift : BaseBusiness<Shift>
    {
        IDataAccess accessPort = new BUser();
        const int dayMinutes = 1440;
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.Shift";
        private ShiftRepository shiftRepository = new ShiftRepository(false);
        EntityRepository<ShiftPair> pairRep = new EntityRepository<ShiftPair>(false);

        /// <summary>
        /// لیست همه شیفتها را برمیگرداند
        /// اگر نوبت کاری نداشته باشد بمنظور نمایش در یو آی یک نوبت کاری صوری میسازد
        /// </summary>
        /// <returns></returns>
        public override IList<Shift> GetAll()
        {
            IList<Shift> shifts = new List<Shift>();
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleShifts();
                shifts = shiftRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Shift().ID), ids.ToArray(), CriteriaOperation.IN));
                for (int i = 0; i < shifts.Count; i++)
                {
                    if (shifts[i].NobatKari == null)
                    {
                        shifts[i].NobatKari = new NobatKari();
                    }
                }
                return shifts;
            }
            catch (Exception ex)
            {
                LogException(ex, "BShift", "GetAll");
                throw ex;
            }
            finally { }
        }            
     
        /// <summary>
        /// لیستی از نوبت کاری را بمنظور پر کردن کومبو باکس بر میگرداند
        /// چون این آیتم در دیتابیس میتواند خالی باشد پس باید آیتم اول آن مقداردهی شود
        /// </summary>
        /// <returns></returns>
        public IList<NobatKari> GetAllNobatKari(string firstItemTitle)
        {
            try
            {
                BNobatkari bnobat = new BNobatkari();
                IList<NobatKari> list = bnobat.GetAll();
                NobatKari firstItem = new NobatKari();
                firstItem.ID = 0;
                firstItem.Name = firstItemTitle;
                list.Insert(0, firstItem);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BShift", "GetAllNobatKari");
                throw ex;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام شیفت خالی نباشد
        /// «نام شیفت تکراری نباشد
        /// «رنگ شیفت نباید خالی باشد
        /// «رنگ شیفت نباید تکراری باشد    
        /// « کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="shift"></param>
        protected override void UpdateValidate(Shift shift)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(shift.Name))
            {
                exception.Add(ExceptionResourceKeys.ShiftNameRequierd, "بروزرسانی - نام شیفت نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.Name), shift.Name),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => shift.ID), shift.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftNameRepeated, "بروزرسانی - نام شیفت نباید تکراری باشد", ExceptionSrc);
                }
            }

            if (Utility.IsEmpty(shift.Color))
            {
                exception.Add(ExceptionResourceKeys.ShiftColorRequierd, "بروزرسانی - رنگ شیفت نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.Color), shift.Color),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => shift.ID), shift.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftColorRepeated, "بروزرسانی - رنگ شیفت نباید تکراری باشد", ExceptionSrc);
                }
            }

            if (shift.ShiftType == null)
            {
                exception.Add(ExceptionResourceKeys.ShiftTypeRequierd, "بروزرسانی - نوع شیفت نباید خالی باشد", ExceptionSrc);
            }
            else if (shift.ShiftType != ShiftTypesEnum.COMPENSATION_OVERTIME &&
                shift.ShiftType != ShiftTypesEnum.OVERTIME &&
                shift.ShiftType != ShiftTypesEnum.WORK)
            {
                exception.Add(ExceptionResourceKeys.ShiftTypeRequierd, "بروزرسانی - نوع شیفت نامعتبر میباشد", ExceptionSrc);
            }

            if (!Utility.IsEmpty(shift.CustomCode))
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.CustomCode), shift.CustomCode),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => shift.ID), shift.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ShiftCustomCodeRepeated, "بروزرسانی - کد گروه کاری نباید تکراری باشد7", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام شیفت خالی نباشد
        /// «نام شیفت تکراری نباشد
        /// «رنگ شیفت نباید خالی باشد
        /// «رنگ شیفت نباید تکراری باشد       
        /// « کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="shift"></param>
        protected override void InsertValidate(Shift shift)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(shift.Name))
            {
                exception.Add(ExceptionResourceKeys.ShiftNameRequierd, "درج - نام شیفت نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.Name), shift.Name)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftNameRepeated, "درج - نام شیفت نباید تکراری باشد", ExceptionSrc);
                }
            }

            if (Utility.IsEmpty(shift.Color))
            {
                exception.Add(ExceptionResourceKeys.ShiftColorRequierd, "درج - رنگ شیفت نباید خالی باشد", ExceptionSrc);
            }
            else
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.Color), shift.Color)) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftColorRepeated, "درج - رنگ شیفت نباید تکراری باشد", ExceptionSrc);
                }
            }

            if (shift.ShiftType == null)
            {
                exception.Add(ExceptionResourceKeys.ShiftTypeRequierd, "درج - نوع شیفت نباید خالی باشد", ExceptionSrc);
            }
            else if (shift.ShiftType != ShiftTypesEnum.COMPENSATION_OVERTIME &&
                shift.ShiftType != ShiftTypesEnum.OVERTIME &&
                shift.ShiftType != ShiftTypesEnum.WORK)
            {
                exception.Add(ExceptionResourceKeys.ShiftTypeRequierd, "درج - نوع شیفت نامعتبر میباشد", ExceptionSrc);
            }

            if (!Utility.IsEmpty(shift.CustomCode))
            {
                if (shiftRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => shift.CustomCode), shift.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ShiftCustomCodeRepeated, "درج - کد گروه کاری نباید تکراری باشد", ExceptionSrc));
                }
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Shift shift)
        {
            if (shiftRepository.HasWorkGroupDetail(shift.ID))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.ShiftUsedInWorkGroup, "این شیفت در گروه های کاری به یک روز انتساب داده شده است", ExceptionSrc);
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(Shift shift, UIActionType action)
        {
            if (shift.NobatKariID == 0)
            {
                shift.NobatKari = null;
            }
            else
            {
                NobatKari nobatkari = new NobatKari();
                nobatkari.ID = shift.NobatKariID;
                shift.NobatKari = nobatkari;
            }
        }

        public Shift GetShiftByColor(string color)
        {
            try
            {
                IList<Shift> list = shiftRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Shift().Color), color));
                if (list == null || list.Count == 0)
                {
                    throw new ItemNotExists(String.Format("رنگ شیفت با عنوان {0} در دیتابیس موجود نیست", color), ExceptionSrc);
                }
                else if (list.Count == 1)
                {
                    return list[0];
                }
                throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ShiftColorIsNotUnique, String.Format("رنگ شیفت با عنوان {0} یکتا نیست", color), ExceptionSrc);
            }
            catch (Exception ex) 
            {
                LogException(ex, "BShift", "GetShiftByColor");
                throw ex;
            }
        }
    

        #region ShiftPair

        /// <summary>
        /// عملیات درج و بروزرسانی انجام میشود
        ///  //  new ShiftPair() { ID = 5, ShiftId = 10 };
        /// </summary>
        /// <param name="pair"></param>
        public decimal SaveChangesShiftPair(ShiftPair pair, UIActionType action)
        {
            try
            {
                GetReadyBeforeSaveShiftPair(pair, action);

                if (action == UIActionType.EDIT)
                {
                    UpdateValidateShiftPair(pair.ShiftId, pair);
                    UpdateShiftPair(pair);
                }
                else if (action == UIActionType.ADD)
                {
                    InsertValidateShiftPair(pair.ShiftId, pair);
                    InsertShiftPair(pair);
                }
                else if (action == UIActionType.DELETE)
                {
                    DeleteShiftPair(pair.ID);
                }
                this.UpdateCFP(pair, action);
                return pair.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "Shift", "SaveChangesShiftPair");
                throw ex;
            }
        }

        /// <summary>
        /// یک آیتم را بوسیله کلید اصلی جستجو میکند
        /// اگر آیتم موجود نباشد خطا پرتاب میکند 
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public ShiftPair GetByShiftPairID(decimal shiftId, decimal shiftpairId)
        {
            try
            {
                Shift shift = shiftRepository.GetById(shiftId, false);
                if (shift != null)
                {
                    if (shift.Pairs.Where(x => x.ID == shiftpairId).Count() == 1)
                    {
                        return shift.Pairs.Where(x => x.ID == shiftpairId).First();
                    }
                }
                throw new ItemNotExists(" شیفت با این شناسه موجود نمیباشد", ExceptionSrc);
            }
            catch (Exception ex)
            {
                LogException(ex, "Shift", "GetByShiftPairID");
                throw ex;
            }
        }      

        private void InsertShiftPair(ShiftPair shiftPair)
        {
            try
            {
                //if (shiftPair.NextDayContinual) 
                //{
                //    shiftPair.To += dayMinutes;
                //}
                pairRep.Save(shiftPair);
            }
            catch (Exception ex)
            {
                LogException(ex, "Shift", "InsertShiftPair");
                throw ex;
            }
        }

        private void UpdateShiftPair(ShiftPair shiftPair)
        {
            try
            {
                pairRep.Merge(shiftPair);
            }
            catch (Exception ex)
            {
                LogException(ex, "Shift", "UpdateShiftPair");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shiftPair"></param>
        /// <param name="action"></param>
        private void GetReadyBeforeSaveShiftPair(ShiftPair shiftPair, UIActionType action)
        {
            if (shiftPair.NextDayContinual) 
            {
                shiftPair.To += 1440;
            }
        }

        /// <summary>
        /// حذف یک آیتم       
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public bool DeleteShiftPair(decimal shiftPairId)
        {
            ShiftPair shiftPair = new ShiftPair() { ID = shiftPairId };
            try
            {
                pairRep.Delete(shiftPair);
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "Shift", "DeleteShiftPair");
                throw ex;
            }
            finally { }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «مقدار دهی ابتدا و انتها
        /// «ابتدا بزرگتر از انتها نباشد
        /// «ابتدا و انتها برابر نباشد
        /// «اشتراک نداشته باشد
        /// 
        /// </summary>
        /// <param name="shift"></param>
        private void InsertValidateShiftPair(decimal shiftId, ShiftPair pair)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (pair != null)
            {
                if (pair.To == 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftPairFromToEmpty, "جفت شیفت نمیتواند تهی باشد", ExceptionSrc);
                }
                if (!pair.NextDayContinual && pair.From > pair.To)
                {
                    exception.Add(ExceptionResourceKeys.ShiftFromGreaterThanTo, "مقدار ابتدا از انتها بزرگتر است", ExceptionSrc);
                }
                if (!pair.NextDayContinual && pair.From == pair.To)
                {
                    exception.Add(ExceptionResourceKeys.ShiftFromAndToAreEquals, "مقدار ابتدا و انتها برابر است", ExceptionSrc);
                }

                Shift shift = GetByID(shiftId);
                if (Operation.Intersect((IPair)pair, shift.Pairs.ToList().ConvertAll(x => (IPair)x)).Value > 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftPairHasIntersect, "این جفت با جفتهای موجود در شیفت همپوشانی دارد", ExceptionSrc);
                }
            }
            else
            {
                exception.Add(ExceptionResourceKeys.ShiftPairNull, "جفت شیفت نمیتواند تهی باشد", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «مقدار دهی ابتدا و انتها
        /// «ابتدا بزرگتر از انتها نباشد
        /// «ابتدا و انتها برابر نباشد
        /// «اشتراک نداشته باشد
        /// 
        /// </summary>
        /// <param name="shift"></param>
        private void UpdateValidateShiftPair(decimal shiftId, ShiftPair pair)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (pair != null)
            {
                if (pair.To == 0)
                {
                    exception.Add(ExceptionResourceKeys.ShiftPairFromToEmpty, "جفت شیفت نمیتواند تهی باشد", ExceptionSrc);
                }
                if (!pair.NextDayContinual && pair.From > pair.To)
                {
                    exception.Add(ExceptionResourceKeys.ShiftFromGreaterThanTo, "مقدار ابتدا از انتها بزرگتر است", ExceptionSrc);
                }
                if (!pair.NextDayContinual && pair.From == pair.To)
                {
                    exception.Add(ExceptionResourceKeys.ShiftFromAndToAreEquals, "مقدار ابتدا و انتها برابر است", ExceptionSrc);
                }

                Shift shift = GetByID(shiftId);
                if (shift.Pairs != null && shift.Pairs.Count > 1)
                {
                    ShiftPair orignalPair = shift.Pairs.Where(x => x.ID == pair.ID).First();
                    shift.Pairs.Remove(orignalPair);
                    if (Operation.Intersect((IPair)pair, shift.Pairs.ToList().ConvertAll(x => (IPair)x)).Value > 0)
                    {
                        exception.Add(ExceptionResourceKeys.ShiftPairHasIntersect, "این جفت با جفتهای موجود در شیفت همپوشانی دارد", ExceptionSrc);
                    }
                }
            }
            else
            {
                exception.Add(ExceptionResourceKeys.ShiftPairNull, "جفت شیفت نمیتواند تهی باشد", ExceptionSrc);
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected void UpdateCFP(ShiftPair obj, UIActionType action)
        {
            //return;
            if (action == UIActionType.ADD || action == UIActionType.EDIT || action == UIActionType.DELETE)
            {
                BWorkGroupCalendar busWorkGroup = new BWorkGroupCalendar();
                WorkGroupRepository workRep = new WorkGroupRepository(false);
                IList<WorkGroup> list = workRep.GetAllWorkGroupByShift(obj.ShiftId);
                foreach (WorkGroup workGroup in list)
                {
                    DateTime? firstUse = shiftRepository.GetFirstShiftUsedInWorkGroup(obj.ShiftId, workGroup.ID);
                    if (firstUse != null && firstUse > Utility.GTSMinStandardDateTime)
                    {
                        busWorkGroup.UpdateCFPByWorkGroupId(workGroup.ID, (DateTime)firstUse);
                    }
                }
            }
        }   

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftsLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertShift(Shift shift, UIActionType UAT)
        {
            return base.SaveChanges(shift, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateShift(Shift shift, UIActionType UAT)
        {
            return base.SaveChanges(shift, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteShift(Shift shift, UIActionType UAT)
        {
            return base.SaveChanges(shift, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertShiftPair(ShiftPair shiftPair, UIActionType UAT)
        {
            return this.SaveChangesShiftPair(shiftPair, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateShiftPair(ShiftPair shiftPair, UIActionType UAT)
        {
            return this.SaveChangesShiftPair(shiftPair, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteShiftPair(ShiftPair shiftPair, UIActionType UAT)
        {
            return this.SaveChangesShiftPair(shiftPair, UAT);
        }
        
        #endregion
    }
}