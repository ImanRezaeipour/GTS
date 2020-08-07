using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Model.Concepts
{
    public enum SortOrder
    {
        asc, desc
    }
    public class BasicTrafficController
    {
        /// <summary>
        /// صعودی یا نزولی
        /// </summary>
       
        #region variable
        
        private int curentIndex = 0;
        private List<BasicTraffic> basicTrafficList = new List<BasicTraffic>();
      //  private BasicTraffic currentBasicTraffic = new BasicTraffic();

        #endregion        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trafficList">یک لیست از ترددها که میخواهیم آنرا پردازش کنیم</param>
        public BasicTrafficController(List<BasicTraffic> trafficList) 
        {
            if (trafficList != null)
            {
                basicTrafficList = trafficList.Where(x => x.Active).ToList();
                for (int i = 0; i < basicTrafficList.Count; i++)
                {
                    if (basicTrafficList[i].Used == false)
                    {
                        curentIndex = i;
                        break;
                    }
                }
            }
            else 
            {
                basicTrafficList = new List<BasicTraffic>();
            }
        }

        /// <summary>
        /// ایندکس آیتم فعلی
        /// </summary>
        public int CurrentIndex
        {
            get { return curentIndex; }       
        }

        /// <summary>
        /// آیا آیتم بعدی وجود دارد
        /// </summary>
        public bool HasNextItem 
        {
            get 
            {
                if (curentIndex < basicTrafficList.Count - 1) 
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// آیا دوتا آیتم جلوتر وجود دارد
        /// </summary>
        public bool HasAfterNextItem
        {
            get
            {
                if (curentIndex < basicTrafficList.Count - 2)
                {
                    return true;
                }
                return false;
            }
        }
       

        /// <summary>
        /// آیتم فعلی
        /// </summary>
        public BasicTraffic CurrentBasicItem
        {
            get
            {
                if (basicTrafficList.Count > 0)
                {
                    return basicTrafficList[curentIndex];
                }
                else 
                {
                    throw new Exception("The List Is Empty: GTS.Clock.Model.Concepts.CurrentBasicItem");
                }
            }
        }

        /// <summary>
        /// آیتم قبلی
        /// </summary>
        public BasicTraffic BeforeItem 
        {
            get 
            {
                if (curentIndex > 0) 
                {
                    return basicTrafficList[curentIndex - 1];
                }
                return new BasicTraffic();
            }
        }

        /// <summary>
        /// آیتم بعدی
        /// </summary>
        public BasicTraffic NextItem
        {
            get
            {
                if (curentIndex < basicTrafficList.Count - 1)
                {

                    return basicTrafficList[curentIndex + 1];
                }
                return new BasicTraffic();
            }
        }

        /// <summary>
        /// دوتا آیتم جلوتر
        /// </summary>
        public BasicTraffic AfterNextItem
        {
            get
            {
                if (curentIndex < basicTrafficList.Count - 2)
                {
                    return basicTrafficList[curentIndex + 2];
                }
                return new BasicTraffic();
            }
        }        

        /// <summary>
        /// آیا همه آیتم ها استفاده شده اند
        /// </summary>
        public bool Finished
        {
            get
            {
                if (basicTrafficList.Count == 0) 
                {
                    return true;
                }
                if (curentIndex == basicTrafficList.Count - 1 && CurrentBasicItem.Used)
                {
                    return true;
                }
                else if (CurrentBasicItem.Used)
                {
                    for (int i = curentIndex; i < basicTrafficList.Count; i++)
                    {
                        if (basicTrafficList[i].Used == false)
                        {
                            return false;
                        }
                    }
                }
                if (CurrentBasicItem.Used)//از حلقه بالا عبور کرده است
                {
                    return true;
                }
               
                return false;
            }
        }

        /// <summary>
        /// هم آیتم جاری را بررسی مسکند آیتم بعدی
        /// </summary>
        public bool CanFindPair 
        {
            get 
            {
                return (!Finished) && HasNextItem;
            }
        }

        /// <summary>
        /// آیا آیتم جاری در بازه زمانی شیفت ورودی میگنجد
        /// </summary>
        /// <param name="shift">شیفت مقایسه</param>
        public bool IsTrafficInShift(AssignedWGDShift shift)
        {
            foreach (ShiftPair sp in shift.Pairs) 
            {
                if (basicTrafficList[curentIndex].Time <= sp.To && basicTrafficList[curentIndex].Time >= sp.From) 
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا تردد ورودی در بازه زمانی شیفت ورودی میگنجد
        /// </summary>
        /// <param name="shift">شیفت مقایسه</param>
        /// <param name="basicTraffic">تردد مقایسه</param>
        /// <returns></returns>
        public bool IsTrafficInShift(AssignedWGDShift shift, BasicTraffic basicTraffic)
        {
            foreach (ShiftPair sp in shift.Pairs)
            {
                if (basicTraffic.Time <= sp.To && basicTraffic.Time >= sp.From)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیتم بعدی که استفاده نشده است را آیتم جاری قرار بده
        /// </summary>
        public void MoveCurrentToNextUnusedItem() 
        {
            for (int i = curentIndex ; i < basicTrafficList.Count; i++)
            {
                if (!basicTrafficList[i].Used) 
                {
                    curentIndex = i;
                    break;
                }
            }
        }


        /// <summary>
        /// مرتب سازی ترددهای پایه بر اساس زمان تردد
        /// </summary>
        /// <param name="order"></param>
        public void SortByTime(SortOrder order) 
        {
            if (order == SortOrder.asc)
            {
                basicTrafficList = basicTrafficList.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                 
            }
            else 
            {
                basicTrafficList = basicTrafficList.OrderByDescending(x => x.Date).ThenBy(x => x.Time).ToList();
                
            }
        }


        /// <summary>
        /// یک تردد پردازش نشده به لیست اضافه میکند
        /// </summary>
        /// <param name="_basicTraffic">این تردد باید با دقت در اضافه شود تا ترتیب لیست از بین نرود</param>
        public void InsertTrafficAfterCurentItem(BasicTraffic _basicTraffic1, BasicTraffic _basicTraffic2) 
        {
            basicTrafficList.Insert(curentIndex + 1, _basicTraffic1);
            basicTrafficList.Insert(curentIndex + 2, _basicTraffic2);
        }

        /// <summary>
        /// یک تردد پردازش نشده به لیست اضافه میکند
        /// </summary>
        /// <param name="_basicTraffic">این تردد باید با دقت در اضافه شود تا ترتیب لیست از بین نرود</param>
        public void InsertTrafficBeforeCurentItem(BasicTraffic _basicTraffic)
        {
            basicTrafficList.Insert(curentIndex, _basicTraffic);
        }

        private void CheckDublicate() 
        {

        }

        /// <summary>
        /// تنظیم دوباره اولین آیتم
        /// </summary>
        public void Reset() 
        {
            for (int i = 0; i < basicTrafficList.Count; i++)
            {
                if (basicTrafficList[i].Used == false)
                {
                    curentIndex = i;
                    break;
                }
            }
        }
        
    }
}
