using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.PersonInfo
{
    public class BPersonReservedField : BaseBusiness<PersonReserveField>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Person.BPersonReservedField";

        /// <summary>
        /// فیلدهای رزرو را برمیگرداند
        /// </summary>
        public IList<PersonReserveField> GetAllReservedFields()
        {
            try
            {
                IList<PersonReserveField> list = base.GetAll();
                if (list == null || list.Count != 20)
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PersonReservedFiledsCount, "تعداد فیلدهای رزرو پرسنل برابر 20 نیست", ExceptionSrc);

                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// یک آیتم به کومبو اضافه میکند
        /// </summary>
        /// <param name="reservedFieldId"></param>
        /// <param name="comboTitle"></param>
        /// <param name="comboValue"></param>
        public void InsertComboItem(decimal reservedFieldId, string comboTitle, string comboValue)
        {
            try
            {
                PersonReserveField field = base.GetByID(reservedFieldId);
                if (field.ControlType == PersonReservedFieldsType.ComboValue)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    if (field.ComboItems == null)
                    {
                        field.ComboItems = new List<PersonReserveFieldComboValue>();
                    }
                    if (Utility.IsEmpty(comboTitle) || Utility.IsEmpty(comboValue))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PrsRsvFldComboValueIsEmpty, "مشخصات وارد شده خالی میباشد", ExceptionSrc));
                    }
                    else if (field.ComboItems.Where(x => x.ComboText.ToLower().Equals(comboTitle.ToLower())
                        ||
                        x.ComboValue.ToLower().Equals(comboValue.ToLower())).Count() > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.PrsRsvFldComboValueRepeated, "مشخصات وارد شده تکراری میباشد", ExceptionSrc));
                    }
                   
                    if (exception.Count > 0) 
                    {
                        throw exception;
                    }

                    PersonReserveFieldComboValue item = new PersonReserveFieldComboValue();
                    item.ComboText = comboTitle;
                    item.ComboValue = comboValue;
                    item.ReserveFieldID = field.ID;
                    field.ComboItems.Add(item);
                    this.SaveChanges(field, UIActionType.EDIT);
                }
                else
                {
                    throw new IllegalServiceAccess("تنها برای فیلدهایی از نوع انتخابی میتوان گزینه تعریف کرد", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// یک آیتم از کومبو ویرایش میکند
        /// </summary>
        /// <param name="reservedFieldId"></param>
        /// <param name="comboItemId"></param>
        public void EditComboItem(decimal reservedFieldId, decimal comboItemId, string comboTitle, string comboValue)
        {
            try
            {
                if (Utility.IsEmpty(comboTitle) || Utility.IsEmpty(comboValue))
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(new ValidationException(ExceptionResourceKeys.PrsRsvFldComboValueIsEmpty, "مشخصات وارد شده خالی میباشد", ExceptionSrc));
                    throw exception;
                }
              
                PersonReserveField field = base.GetByID(reservedFieldId);
                if (field.ControlType == PersonReservedFieldsType.ComboValue)
                {
                    if (field.ComboItems == null)
                    {
                        field.ComboItems = new List<PersonReserveFieldComboValue>();
                    }
                    PersonReserveFieldComboValue item = field.ComboItems.Where(x => x.ID == comboItemId).FirstOrDefault();

                    if (item != null)
                    {
                        item.ComboText = comboTitle;
                        item.ComboValue = comboValue;
                        this.SaveChanges(field, UIActionType.EDIT);
                    }
                }
                else
                {
                    throw new IllegalServiceAccess("تنها برای فیلدهایی از نوع انتخابی ، گزینه موجود است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// یک آیتم از کومبو حذف میکند
        /// </summary>
        /// <param name="reservedFieldId"></param>
        /// <param name="comboItem"></param>
        public void DeleteComboItem(decimal reservedFieldId, decimal comboItem)
        {
            try
            {
                PersonReserveField field = base.GetByID(reservedFieldId);
                if (field.ControlType == PersonReservedFieldsType.ComboValue)
                {
                    if (field.ComboItems == null)
                    {
                        field.ComboItems = new List<PersonReserveFieldComboValue>();
                    }
                    PersonReserveFieldComboValue item = field.ComboItems.Where(x => x.ID == comboItem).FirstOrDefault();

                    if (item != null)
                    {
                        EntityRepository<PersonDetail> dtlRep = new EntityRepository<PersonDetail>(false);
                        bool flag = false;
                        switch (field.OrginalName) 
                        {
                            case "R16":
                                if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonTASpec().R16), item.ID)) > 0) 
                                {
                                    flag = true;
                                }
                                break;
                            case "R17":
                                if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonTASpec().R17), item.ID)) > 0)
                                {
                                    flag = true;
                                }
                                break;
                            case "R18":
                                if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonTASpec().R18), item.ID)) > 0)
                                {
                                    flag = true;
                                }
                                break;
                            case "R19":
                                if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonTASpec().R19), item.ID)) > 0)
                                {
                                    flag = true;
                                }
                                break;
                            case "R20":
                                if (dtlRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonTASpec().R20), item.ID)) > 0)
                                {
                                    flag = true;
                                }
                                break;
                        }
                        if (flag) 
                        {
                            UIValidationExceptions exception = new UIValidationExceptions();
                            exception.Add(new ValidationException(ExceptionResourceKeys.PrsRsvFldComboValueUsedByPerson, "آیتم انتخابیی جهت حذف ، در پرسنل قبلا استفاده شده است", ExceptionSrc));
                            throw exception;
                        }
                        
                        field.ComboItems.Remove(item);
                        this.SaveChanges(field, UIActionType.EDIT);
                    }
                }
                else
                {
                    throw new IllegalServiceAccess("تنها برای فیلدهایی از نوع انتخابی ، گزینه  موجود است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// جهت استفاده در بروزرسانی نام فیلد رزرو
        /// </summary>
        /// <param name="reservedFieldId"></param>
        /// <param name="lable"></param>
        public void UpdateReservedFieldLable(decimal reservedFieldId, string lable)
        {
            try
            {
                PersonReserveField field = base.GetByID(reservedFieldId);
                field.Lable = lable;
                this.SaveChanges(field, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IList<PersonReserveFieldComboValue> GetComboItemsByOrginalName(PersonReservedFieldComboItems item)
        {
            IList<PersonReserveField> list = this.GetAllReservedFields();
            PersonReserveField field = list.Where(x => x.OrginalName.Trim().Equals(item.ToString())).FirstOrDefault();
            if (field != null && field.ComboItems != null)
                return field.ComboItems;
            else
                return new List<PersonReserveFieldComboValue>();
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelReserveFieldsLoadAccess_onPersonnelInsert()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelReserveFieldsLoadAccess_onPersonnelUpdate()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateReservedFieldLable_onPersonnelInsert(decimal reservedFieldId, string lable)
        {
            this.UpdateReservedFieldLable(reservedFieldId, lable);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateReservedFieldLable_onPersonnelUpdate(decimal reservedFieldId, string lable)
        {
            this.UpdateReservedFieldLable(reservedFieldId, lable);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertComboItem_onPersonnelInsert(decimal reservedFieldId, string comboTitle, string comboValue)
        {
            this.InsertComboItem(reservedFieldId, comboTitle, comboValue);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertComboItem_onPersonnelUpdate(decimal reservedFieldId, string comboTitle, string comboValue)
        {
            this.InsertComboItem(reservedFieldId, comboTitle, comboValue);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void EditComboItem_onPersonnelInsert(decimal reservedFieldId, decimal comboItemId, string comboTitle, string comboValue)
        {
            this.EditComboItem(reservedFieldId, comboItemId, comboTitle, comboValue);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void EditComboItem_onPersonnelUpdate(decimal reservedFieldId, decimal comboItemId, string comboTitle, string comboValue)
        {
            this.EditComboItem(reservedFieldId, comboItemId, comboTitle, comboValue);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteComboItem_onPersonnelInsert(decimal reservedFieldId, decimal comboItem)
        {
            this.DeleteComboItem(reservedFieldId, comboItem);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteComboItem_onPersonnelUpdate(decimal reservedFieldId, decimal comboItem)
        {
            this.DeleteComboItem(reservedFieldId, comboItem);
        }



        

        

        #region Impelemetation

        protected override void InsertValidate(PersonReserveField obj)
        {
            throw new IllegalServiceAccess("سرویس درج برای این موجودیت تعریف نشده است", ExceptionSrc);
        }

        protected override void UpdateValidate(PersonReserveField rsvField)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(rsvField.Lable))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PrsRsvFldLableIsEmpty, "بروزرسانی - نام نباید خالی باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(PersonReserveField obj)
        {
            throw new IllegalServiceAccess("سرویس حذف برای این موجودیت تعریف نشده است", ExceptionSrc);
        }
        #endregion
    }
}
