using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Repository;


namespace GTS.Clock.Business.TrafficMapping
{
    /// <summary>
    /// جفت کردن ترددها
    /// </summary>
    public class TrafficSync
    {
        private Person _person;
        TrafficMapper _mapper;

        /// <summary>
        /// سازنده
        /// جهت اجرا از بیرون
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="person"></param>
        public TrafficSync(TrafficMapper mapper,Person person)
        {
            _person = person;
            _mapper = mapper;
        }

        /// <summary>
        /// اگر در بین ترددهایی که پردازش شده اند , ترددی وجود داشته باشد که 
        /// پردازش شده نباشد(یعنی جدید باشد یا ویرایش شده باشد)
        /// آنگاه باید محاسبات دوباره انجام شود
        /// </summary>
        public void Synchronize()
        { 
            if (_person.BasicTrafficList
                .Where(x => x.Active == false || x.Used == false).Count() > 0)
            {
                _person.BasicTrafficList = _person.BasicTrafficList
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Time)
                    .ToList();

                _person.ProceedTrafficList = _person.ProceedTrafficList
                                    .OrderBy(x => x.FromDate)
                                    .ThenBy(x => x.ID)
                                    .ToList();
                //CheckChangedItems();

            }
        }

        /// <summary>
        /// در بین آیتم های قبلی که محاسبه روی آنها صورت گرفته است
        /// بررسی میکند که آیا آیتم جدید یا ویرایش شده وجود دارد یا نه 
        /// در تریگر قرار است انجام شود
        /// </summary>
        private void CheckChangedItems()
        {
            if (_person.BasicTrafficList.Count > 0)
            {
                List<BasicTraffic> basicTrafList = _person.BasicTrafficList
                .Where(x => x.Active == false || x.Used == false)
                .ToList();
                if (basicTrafList.Count > 0)
                {

                    int index = CheckForNewItem(_person);
                    BasicTraffic bt = basicTrafList.First();
                    if (index > -1 && bt.Date >= _person.BasicTrafficList[index].Date)
                    {
                        bt = _person.BasicTrafficList[index];
                    }
                    List<ProceedTraffic> proceedTrafList = _person.ProceedTrafficList
                       .Where(x => x.Pairs
                           .Where(y => y.BasicTrafficIdFrom == bt.ID || y.BasicTrafficIdTo == bt.ID)
                           .Count() > 0)
                       .ToList();
                    if (proceedTrafList.Count > 0)
                    {
                        ProceedTraffic pt = proceedTrafList.First();
                        while (true)
                        {
                            proceedTrafList = _person.ProceedTrafficList
                                .Where(x => x.ID < pt.ID)
                                .ToList();
                            if (proceedTrafList.Count > 0)
                            {
                                pt = proceedTrafList.Last();
                                if (pt.Pairs.First().BasicTrafficIdFrom > 0)
                                {                                   
                                    List<BasicTraffic> btList = _person.BasicTrafficList.Where(x => x.ID == pt.Pairs[0].BasicTrafficIdFrom).ToList();
                                    if (btList.Count > 0)
                                    {
                                        BasicTraffic tmpBasic = btList.First();
                                        for (int i = 0; i < _person.BasicTrafficList.Count; i++)
                                        {
                                            decimal id = _person.BasicTrafficList[i].ID;
                                            if (tmpBasic.Date <= _person.BasicTrafficList[i].Date)
                                            {
                                                _person.BasicTrafficList[i].Used = false;
                                            }
                                        }
                                    }
                                    RemoveAfterID(_person, pt.ID);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (bt.Active == false)
                    {
                        _person.BasicTrafficList.Remove(bt);
                    }
                    //آگر آیتم جدید اضافه شده باشد و آیتم شروع ان قبلا محاسبه شده باشد آنرا حذف میکند
                    BasicTraffic bt1= _person.BasicTrafficController.CurrentBasicItem;
                    if (_person.ProceedTrafficList.Count>0 && _person.ProceedTrafficList.OrderBy(x => x.FromDate).Last().FromDate.Date == bt1.Date.Date)
                    {
                        ProceedTraffic proceedTraffic = _person.ProceedTrafficList.OrderBy(x => x.FromDate).Last();
                        foreach (ProceedTrafficPair pair in proceedTraffic.Pairs) 
                        {
                            if (_person.BasicTrafficList.Where(x => x.ID == pair.BasicTrafficIdFrom).Count() > 0)
                            {
                                _person.BasicTrafficList.Where(x => x.ID == pair.BasicTrafficIdFrom).First().Used = false;
                            }
                        }
                        Remove(_person, proceedTraffic);
                    }
                }
            }
        }

        /// <summary>
        /// از جای ایندکس به بعد ترددهای پردازش شده را حذف میکند
        /// </summary>
        private void RemoveAfterID(Person p, decimal id)
        {
            int index = -1;
            for (int i = 0; i < p.ProceedTrafficList.Count; i++)
            {
                if (p.ProceedTrafficList[i].ID == id)
                {
                    index = i;
                    break;
                }
            }
            int count = p.ProceedTrafficList.Count;
            PersonRepository personRepository = new PersonRepository(false);
            for (int i = index; i < count; i++)
            {
                personRepository.DeleteProceedTraffic(p.ProceedTrafficList[p.ProceedTrafficList.Count - 1]);
                p.ProceedTrafficList.RemoveAt(p.ProceedTrafficList.Count - 1);
            }

        }

        private void Remove(Person p, ProceedTraffic pt)
        {
           
            PersonRepository personRepository = new PersonRepository(false);
           
                personRepository.DeleteProceedTraffic(pt);
                p.ProceedTrafficList.Remove(pt);
            

        }

        /// <summary>
        /// بررسی اینکه آیا آیتم جدیدی در بین آیتمهایی که قبلا پردازش شده اند درج گردیده است با خیر
        /// </summary>
        /// <param name="p">فرض بر این است که ترددهای خام قبلا پردازش شده اند</param>       
        /// <returns>در صورتی که آیتمی را پیداکند که در بین 
        /// آیتمهایی که قبلا پردازش شده اند درج گردیده است
        /// ایندکس نزدیکترین آیتم قبل از آن را برمیگرداند تا محاسبات از آنجا تکرار شود
        /// در صورتی که آیتمی را پیدا نکند -1 برمیگرداند
        /// </returns>
        private int CheckForNewItem(Person p)
        {
            int index = -1;
            for (int i = 0; i < p.BasicTrafficList.Count; i++)
            {
                if (!p.BasicTrafficList[i].Used)
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                bool isNewAndBetweenUsedItems = false;
                //بررسی آیتمهای بعدی تا ببیند آیا آیتمی پردازش شده میبیند یا خیر
                for (int i = index; i < p.BasicTrafficList.Count; i++)
                {
                    if (p.BasicTrafficList[i].Used)
                    {
                        isNewAndBetweenUsedItems = true;
                        break;
                    }
                }
                if (isNewAndBetweenUsedItems)
                {
                    //برگرداندن نزدیکترین آیتم قبلی 
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (p.BasicTrafficList[i].Used)
                        {
                            return i;
                        }
                    }
                    return 0;
                }
            }
            return -1;
        }

    }

}
