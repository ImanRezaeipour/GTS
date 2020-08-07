using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class DateRangeBusiness : BaseFixture
    {
        #region variables
        DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter assinTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonRangeAssignmentTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter groupTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationRangeGroupTableAdapter();
        DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter dateRangeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_CalculationDateRangeTableAdapter();
        DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter conceptTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_ConceptTemplateTableAdapter();


        CalculationRangeGroup ADOGroup = new CalculationRangeGroup();
        CalculationDateRange ADODateRange1 = new CalculationDateRange();
        CalculationDateRange ADODateRange2 = new CalculationDateRange();
        CalculationDateRange ADODateRange3 = new CalculationDateRange();
        CalculationDateRange ADODateRange4 = new CalculationDateRange();
        CalculationDateRange ADODateRange5 = new CalculationDateRange();
        SecondaryConcept ADOConcept1 = new SecondaryConcept();
        SecondaryConcept ADOConcept2 = new SecondaryConcept();
        SecondaryConcept ADOConcept3 = new SecondaryConcept();
        CalculationDateRange dateRange_testObject;
        CalculationRangeGroup group_testObject;
        IList<CalculationDateRange> dateRangList_testObject;
        BDateRange businessDateRange;
        IList<CalculationDateRange> defaultDateRanges = new List<CalculationDateRange>() 
            {
                new CalculationDateRange(){FromDay=1,FromMonth=1,ToDay=31,ToMonth=1},
                new CalculationDateRange(){FromDay=1,FromMonth=2,ToDay=31,ToMonth=2},
                new CalculationDateRange(){FromDay=1,FromMonth=3,ToDay=31,ToMonth=3},
                new CalculationDateRange(){FromDay=1,FromMonth=4,ToDay=31,ToMonth=4},
                new CalculationDateRange(){FromDay=1,FromMonth=5,ToDay=31,ToMonth=5},
                new CalculationDateRange(){FromDay=1,FromMonth=6,ToDay=31,ToMonth=6},
                new CalculationDateRange(){FromDay=1,FromMonth=7,ToDay=30,ToMonth=7},
                new CalculationDateRange(){FromDay=1,FromMonth=8,ToDay=30,ToMonth=8},
                new CalculationDateRange(){FromDay=1,FromMonth=9,ToDay=30,ToMonth=9},
                new CalculationDateRange(){FromDay=1,FromMonth=10,ToDay=30,ToMonth=10},
                new CalculationDateRange(){FromDay=1,FromMonth=11,ToDay=30,ToMonth=11},
                new CalculationDateRange(){FromDay=1,FromMonth=12,ToDay=29,ToMonth=12},
            };
        #endregion

        [SetUp]
        public void TestSetup()
        {
            businessDateRange = new BDateRange();
            dateRange_testObject = new CalculationDateRange();
            group_testObject = new CalculationRangeGroup();
            dateRangList_testObject = new List<CalculationDateRange>();

            personTA.InsertQuery("0000", "ali", true, null);
            int personId = Convert.ToInt32(personTA.GetDataByBarcode("0000")[0][0]);

            groupTA.Insert("RangeGroup", "",1);
            DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
            groupTA.FillByGroupName(groupTable, "RangeGroup");

            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0]["CalcRangeGroup_ID"]);
            ADOGroup.Name = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Name"]);
            ADOGroup.Description = Convert.ToString(groupTable.Rows[0]["CalcRangeGroup_Des"]);

            assinTA.Insert(personId, ADOGroup.ID, DateTime.Now);

            DatabaseGateway.TA_ConceptTemplateDataTable concepts = new DatabaseGateway.TA_ConceptTemplateDataTable();
            concepts = conceptTA.GetDataRangly();
            
            ADOConcept1.ID = Convert.ToDecimal(concepts.Rows[0]["concepttmp_ID"]);
            ADOConcept2.ID = Convert.ToDecimal(concepts.Rows[1]["concepttmp_ID"]);
            ADOConcept3.ID = Convert.ToDecimal(concepts.Rows[2]["concepttmp_ID"]);


            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 1, 14, 2, 1);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 2, 14, 3, 2);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 3, 14, 4, 3);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 4, 14, 5, 4);
            dateRangeTA.Insert(ADOConcept1.ID, ADOGroup.ID, 15, 5, 14, 6, 5);
            dateRangeTA.Insert(ADOConcept2.ID, ADOGroup.ID, 15, 1, 14, 2, 1);
            dateRangeTA.Insert(ADOConcept2.ID, ADOGroup.ID, 15, 2, 14, 3, 2);
            dateRangeTA.Insert(ADOConcept2.ID, ADOGroup.ID, 15, 3, 14, 4, 3);
            dateRangeTA.Insert(ADOConcept2.ID, ADOGroup.ID, 15, 4, 14, 5, 4);
            dateRangeTA.Insert(ADOConcept2.ID, ADOGroup.ID, 15, 5, 14, 6, 5);

            DatabaseGateway.TA_CalculationDateRangeDataTable rangeTable = new DatabaseGateway.TA_CalculationDateRangeDataTable();
            dateRangeTA.FillByGroup(rangeTable, ADOGroup.ID);
            ADODateRange1.ID = Convert.ToDecimal(rangeTable.Rows[0]["CalcDateRange_ID"]);
            ADODateRange2.ID = Convert.ToDecimal(rangeTable.Rows[1]["CalcDateRange_ID"]);
            ADODateRange3.ID = Convert.ToDecimal(rangeTable.Rows[2]["CalcDateRange_ID"]);
            ADODateRange4.ID = Convert.ToDecimal(rangeTable.Rows[3]["CalcDateRange_ID"]);
            ADODateRange5.ID = Convert.ToDecimal(rangeTable.Rows[4]["CalcDateRange_ID"]);

            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 1, ToDay = 31, ToMonth = 1, Order = CalculationDateRangeOrder.Month1 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 2, ToDay = 31, ToMonth = 2, Order = CalculationDateRangeOrder.Month2 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 3, ToDay = 31, ToMonth = 3, Order = CalculationDateRangeOrder.Month3 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 4, ToDay = 31, ToMonth = 4, Order = CalculationDateRangeOrder.Month4 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 5, ToDay = 31, ToMonth = 5, Order = CalculationDateRangeOrder.Month5 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 6, ToDay = 31, ToMonth = 6, Order = CalculationDateRangeOrder.Month6 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 7, ToDay = 31, ToMonth = 7, Order = CalculationDateRangeOrder.Month7 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 8, ToDay = 31, ToMonth = 8, Order = CalculationDateRangeOrder.Month8 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 9, ToDay = 31, ToMonth = 9, Order = CalculationDateRangeOrder.Month9 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 10, ToDay = 31, ToMonth = 10, Order = CalculationDateRangeOrder.Month10 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 11, ToDay = 31, ToMonth = 11, Order = CalculationDateRangeOrder.Month11 });
            dateRangList_testObject.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 12, ToDay = 29, ToMonth = 12, Order = CalculationDateRangeOrder.Month12 });
        }

        [TearDown]
        public void TreatDown()
        {
            groupTA.DeleteByName(ADOGroup.Name);

            groupTA.DeleteByName("Group Test 2");

            personTA.DeleteByBarcode("0000");

            conceptTA.DeleteQuery(20001);
        }

        [Test]
        public void GetAllTest()
        {
            businessDateRange.GetAll();
            Assert.Pass();
        }

        [Test]
        public void GetAllRanglyConcepts_Test()
        {
            IList<SecondaryConcept> list = businessDateRange.GetAllRanglyConcepts();
            Assert.IsTrue(list.Where(x => x.ID == ADOConcept1.ID).Count() == 1);
        }

        [Test]
        public void GetRanges_Test()
        {
            group_testObject = businessDateRange.GetByID(ADOGroup.ID);
            Assert.AreEqual(group_testObject.DateRangeList.Count, 10);
        }

        [Test]
        public void GetAllDateRangeCount_Test()
        {
            IList<CalculationDateRange> list = businessDateRange.GetAllDateRange(ADOConcept1.ID, ADOGroup.ID);
            Assert.AreEqual(list.Count, 5);
        }

        [Test]
        public void GetAllDateRangeItem_Test()
        {
            IList<CalculationDateRange> list = businessDateRange.GetAllDateRange(ADOConcept1.ID, ADOGroup.ID);
            Assert.AreEqual(list.Where(x => x.ID == ADODateRange1.ID).Count(), 1);
            Assert.AreEqual(list.Where(x => x.ID == ADODateRange3.ID).Count(), 1);
            Assert.AreEqual(list.Where(x => x.ID == ADODateRange5.ID).Count(), 1);
        }

        [Test]
        public void Insert_RangeCountValidation()
        {
            try
            {
                group_testObject.DateRangeList = new List<CalculationDateRange>();
                businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, new List<CalculationDateRange>() { ADODateRange1 }, new List<decimal>() { ADOConcept1.ID });
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesCountNotEqualToTwelve);
            }
        }

        [Test]
        public void Insert_OneConceptTest()
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();
            IList<CalculationDateRange> list = new List<CalculationDateRange>();
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 1, ToDay = 31, ToMonth = 1 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 2, ToDay = 31, ToMonth = 2 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 3, ToDay = 31, ToMonth = 3 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 4, ToDay = 31, ToMonth = 4 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 5, ToDay = 31, ToMonth = 5 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 6, ToDay = 31, ToMonth = 6 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 7, ToDay = 31, ToMonth = 7 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 8, ToDay = 31, ToMonth = 8 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 9, ToDay = 31, ToMonth = 9 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 10, ToDay = 31, ToMonth = 10 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 11, ToDay = 31, ToMonth = 11 });
            list.Add(new CalculationDateRange() { FromDay = 1, FromMonth = 12, ToDay = 29, ToMonth = 12 });
            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, list, new List<decimal>() { ADOConcept1.ID });

            Assert.Pass();
        }

        [Test]
        public void Insert_OneConceptCountTest()
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();

            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });

            ClearSession();

            CalculationRangeGroup group = businessDateRange.GetByID(group_testObject.ID);
            int conceptCount = conceptTA.GetDataRangly().Rows.Count;
            Assert.AreEqual(group.DateRangeList.Count, conceptCount * 12);

        }

        [Test]
        public void Insert_MultiConceptTest()
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();

            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID, ADOConcept2.ID, ADOConcept3.ID });

            Assert.Pass();
        }

        [Test]
        public void Insert_MultiConceptCountTest()
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();

            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID, ADOConcept2.ID, ADOConcept3.ID });

            ClearSession();

            CalculationRangeGroup group = businessDateRange.GetByID(group_testObject.ID);
            int conceptCount = conceptTA.GetDataRangly().Rows.Count;
            Assert.AreEqual(conceptCount * 12, group.DateRangeList.Count);
        }

        [Test]
        public void Insert_EmptyNameTest()
        {
            try
            {
                group_testObject.Name = "";
                group_testObject.DateRangeList = new List<CalculationDateRange>();

                businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });
                Assert.Fail("نام خالی میباشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesGroupNameRequierd);
            }
        }

        [Test]
        public void Insert_RepeatNameTest()
        {
            try
            {
                group_testObject.Name = ADOGroup.Name;
                group_testObject.DateRangeList = new List<CalculationDateRange>();

                businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });
                Assert.Fail("نام تباید تکراری باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesGroupNameRepeated);
            }
        }

        [Test]
        public void Insert_IndexTest() 
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();

            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID, ADOConcept2.ID, ADOConcept3.ID });

            ClearSession();

            CalculationRangeGroup group = businessDateRange.GetByID(group_testObject.ID);
            CalculationDateRange range = group.DateRangeList.Where(x => x.Order == CalculationDateRangeOrder.Month1).First();
            Assert.AreEqual(20101, range.FromIndex);//01/01
            Assert.AreEqual(20131, range.ToIndex);//01/31

            range = group.DateRangeList.Where(x => x.Order == CalculationDateRangeOrder.Month12).First();
            Assert.AreEqual(21201, range.FromIndex);//12/01
            Assert.AreEqual(21229, range.ToIndex);//12/29
        }

        [Test]
        public void Insert_IndexBeforeAndNextYearTest()
        {
            group_testObject.Name = "Group Test 2";
            group_testObject.DateRangeList = new List<CalculationDateRange>();

            dateRangList_testObject[0].FromMonth = 12;
            dateRangList_testObject[0].FromDay = 15;
            dateRangList_testObject[0].ToMonth = 1;
            dateRangList_testObject[0].ToDay = 14;

            dateRangList_testObject[11].FromMonth = 12;
            dateRangList_testObject[11].FromDay = 15;
            dateRangList_testObject[11].ToMonth = 1;
            dateRangList_testObject[11].ToDay = 14;

            businessDateRange.InsertDateRange(group_testObject, defaultDateRanges, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID, ADOConcept2.ID, ADOConcept3.ID });

            ClearSession();

            CalculationRangeGroup group = businessDateRange.GetByID(group_testObject.ID);
            CalculationDateRange range = group.DateRangeList.Where(x => x.Order == CalculationDateRangeOrder.Month1).First();
            Assert.AreEqual(11215, range.FromIndex);//12/15
            Assert.AreEqual(20114, range.ToIndex);//01/14

            range = group.DateRangeList.Where(x => x.Order == CalculationDateRangeOrder.Month12).First();
            Assert.AreEqual(21215, range.FromIndex);//12/15
            Assert.AreEqual(30114, range.ToIndex);//01/14
        }


        [Test]
        public void Update_RangeCountValidation()
        {
            try
            {
                group_testObject.DateRangeList = new List<CalculationDateRange>();
                group_testObject.ID = ADOGroup.ID;
                businessDateRange.UpdateDateRange(group_testObject, new List<CalculationDateRange>() { ADODateRange1 }, new List<decimal>() { ADOConcept1.ID });
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesCountNotEqualToTwelve);
            }
        }

        [Test]
        public void Update_EmptyConceptsValidation()
        {
            try
            {
                group_testObject.DateRangeList = new List<CalculationDateRange>();
                group_testObject.ID = ADOGroup.ID;
                businessDateRange.UpdateDateRange(group_testObject, dateRangList_testObject, new List<decimal>());
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesMustHaveConcept);
            }
        }

        [Test]
        public void Update_Test()
        {
            group_testObject.ID = ADOGroup.ID;
            group_testObject.Name = ADOGroup.Name;
            businessDateRange.UpdateDateRange(group_testObject, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });

            ClearSession();
            group_testObject = new CalculationRangeGroup();
            group_testObject = businessDateRange.GetByID(ADOGroup.ID);
            Assert.AreEqual(group_testObject.DateRangeList.Where(x => x.Order == dateRangList_testObject[0].Order && x.Concept.ID == ADOConcept1.ID).FirstOrDefault().FromDay, dateRangList_testObject[0].FromDay);
            Assert.AreEqual(10, group_testObject.DateRangeList.Count);
        }

        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                group_testObject.ID = ADOGroup.ID;
                businessDateRange.UpdateDateRange(group_testObject, dateRangList_testObject, new List<decimal>() { ADOConcept1.ID });

                Assert.Fail("نام نباید خالی باشد");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesGroupNameRequierd);
            }
        }

        [Test]
        public void Delete_UsedByPersonTest()
        {
            try
            {
                businessDateRange.DeleteDateRange(ADOGroup);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.DateRangesUsedByPerson);
            }
        }

        [Test]
        public void Copy_Test()
        {
            try
            {
                int count = groupTA.GetData().Rows.Count;
                businessDateRange.CopyDateRangeGroup(ADOGroup.ID);
                ClearSession();
                int count2 = groupTA.GetData().Rows.Count;
                Assert.AreEqual(count + 1, count2);
                DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
                groupTA.FillByGroupName(groupTable, " Copy Of " + ADOGroup.Name);
                if (groupTable.Rows.Count == 1)
                {
                    group_testObject.ID = Convert.ToDecimal(groupTable.Rows[0][0]);
                    businessDateRange.SaveChanges(group_testObject, UIActionType.DELETE);
                }
                else
                {
                    Assert.Fail("کپی پیدا نشد");
                }
            }
            finally
            {
                groupTA.DeleteByName(" Copy Of " + ADOGroup.Name);
            }
        }

        [Test]
        public void Copy_DependencyTest()
        {
            try
            {
                int count = groupTA.GetData().Rows.Count;
                businessDateRange.CopyDateRangeGroup(ADOGroup.ID);
                ClearSession();
                DatabaseGateway.TA_CalculationRangeGroupDataTable groupTable = new DatabaseGateway.TA_CalculationRangeGroupDataTable();
                groupTA.FillByGroupName(groupTable, " Copy Of " + ADOGroup.Name);
                if (groupTable.Rows.Count == 1)
                {
                    group_testObject.ID = Convert.ToDecimal(groupTable.Rows[0][0]);
                    group_testObject = businessDateRange.GetByID(group_testObject.ID);
                    Assert.AreEqual(group_testObject.DateRangeList.Count, 10);
                    ClearSession();
                    businessDateRange.SaveChanges(group_testObject, UIActionType.DELETE);
                }
                else
                {
                    Assert.Fail("کپی پیدا نشد");
                }
            }
            finally
            {
                groupTA.DeleteByName(" Copy Of " + ADOGroup.Name);
            }
        }
    }
}
