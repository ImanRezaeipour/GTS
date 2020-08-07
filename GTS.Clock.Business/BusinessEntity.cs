using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business
{
    /// <summary>
    /// جهت استفاده از متدهای غیر استاتیک base business
    /// </summary>
    public class BusinessEntity : BaseBusiness<Entity>
    {
        public ILockCalculationUIValidation UIValidator { get { return base.UIValidator; } }

        protected override void InsertValidate(Entity obj)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateValidate(Entity obj)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteValidate(Entity obj)
        {
            throw new NotImplementedException();
        }

        public CFP GetCFP(decimal personId)
        {
            return base.GetCFP(personId);
        }

        public void UpdateCFP(IList<CFP> cfpList, bool invalidateTraffic)
        {
            try
            {
                base.UpdateCFP(cfpList, invalidateTraffic);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void UpdateCFP(IList<Person> personList, DateTime newCfpDate, bool invalidateTraffic)
        {
            try
            {
                IList<CFP> cfpList = new List<CFP>();
                foreach (Person prs in personList)
                {
                    decimal personId = prs.ID;
                    CFP cfp = base.GetCFP(personId);
                    if (cfp.ID == 0 || cfp.Date > newCfpDate)
                    {
                        cfp.Date = newCfpDate.Date;
                        cfp.PrsId = personId;
                        cfpList.Add(cfp);
                    }
                }
                base.UpdateCFP(cfpList, invalidateTraffic);
            }
            catch (Exception ex) 
            {
                LogException(ex);
            }
        }
    }
}
