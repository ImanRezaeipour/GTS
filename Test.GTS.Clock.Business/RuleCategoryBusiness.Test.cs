using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business;
using GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters;
using GTS.Clock.Business.Security;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RuleCategoryBusiness : BaseFixture
    {
        TA_RuleCategoryTableAdapter ruleCatTA = new TA_RuleCategoryTableAdapter();
        TA_RuleCategoryPartTableAdapter ruleCAtPartTA = new TA_RuleCategoryPartTableAdapter();
        TA_PersonRuleCategoryAssignmentTableAdapter prsRleCatAsgTA = new TA_PersonRuleCategoryAssignmentTableAdapter();
        TA_PersonTableAdapter personTA = new TA_PersonTableAdapter();
        TA_RuleTemplateTableAdapter ruleTmpTA = new TA_RuleTemplateTableAdapter();
        TA_RuleTableAdapter ruleTA = new TA_RuleTableAdapter();

        BRuleCategory businessCategory;

        RuleCategory AdoRuleCat = new RuleCategory();
        RuleCategory ruleCat_testObject;
        PersonRuleCatAssignment AdoRleCatAsg = new PersonRuleCatAssignment();
        DatabaseGateway.TA_RuleCategoryDataTable table;


        [SetUp]
        public void TestSetup()
        {
            businessCategory = new BRuleCategory();
            ruleCat_testObject = new RuleCategory();

            Convert.ToInt32(personTA.InsertQuery("0000", "Ali", true, null));
            int personId = Convert.ToInt32(personTA.GetDataByBarcode("0000").Rows[0]["prs_ID"]);


            ruleCatTA.Insert("دسته قانون000", "0000", false,"00-00test1");
            table = ruleCatTA.GetDataByName("دسته قانون000");
            AdoRuleCat.ID = (Decimal)table[0]["RuleCat_ID"];
            AdoRuleCat.Name = (String)table[0]["RuleCat_Name"];

            prsRleCatAsgTA.Insert(personId, AdoRuleCat.ID, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), DateTime.Now);

            ruleCAtPartTA.Insert(businessCategory.GetRoot().ID, AdoRuleCat.ID, true);

            ClearSession();
        }

        [TearDown]
        public void TreatDown()
        {
            ruleCAtPartTA.DeleteByRuleCategory("00-00test1");
            ruleCAtPartTA.DeleteByRuleCategory("00-00test2");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test1");
            prsRleCatAsgTA.DeleteByRuleCategory("00-00test2");
            ruleCatTA.DeleteByCustomCode("00-00test1");
            ruleCatTA.DeleteByCustomCode("00-00test2");
            personTA.DeleteByBarcode("0000");
        }

        [Test]
        public void GetRulesFromRuleCategory()
        {
            try
            {
                RuleCategory rc = new RuleCategory();
                rc.Name = "دسته قانون001";
                rc.CustomCode = "00-00test2";

                decimal[] ids = new decimal[10];
                int count = 0;
                foreach (DatabaseGateway.TA_RuleTemplateRow item in ruleTmpTA.GetDataByTopCount(10))
                {
                    ids[count] = (decimal)item["RuleTmp_ID"];
                    count++;
                }
                rc.InsertedTemplateIDs = ids;
                
                decimal id = businessCategory.SaveChanges(rc, UIActionType.ADD);
                ClearSession();
                Assert.AreEqual(10, businessCategory.GetRulesByRuleCatID(id).Count());
            }
            catch (Exception ex) 
            {
                throw ex;
            }           
        }

        [Test]
        public void RootIsNotNullTest()
        {
            RuleCategory root = businessCategory.GetRoot();
            Assert.IsNotNull(root);
        }

        [Test]
        public void GetChilds_Test()
        {
            dataAccessRuleTA.Insert(BUser.CurrentUser.ID, AdoRuleCat.ID, false);

            RuleCategory root = businessCategory.GetRoot();
            Assert.AreEqual(1, root.ChildList.Count);
        }

        [Test]
        public void Insert_RuleCategoryWithRulesTest()
        {
            try
            {
                RuleCategory rc = new RuleCategory();
                rc.Name = "دسته قانون001";
                rc.CustomCode = "00-00test1";

                decimal[] ids = new decimal[10];
                int count = 0;
                foreach (DatabaseGateway.TA_RuleTemplateRow item in ruleTmpTA.GetDataByTopCount(10))
                {
                    ids[count] = (decimal)item["RuleTmp_ID"];
                    count++;
                }
                rc.InsertedTemplateIDs = ids;
                decimal id = businessCategory.SaveChanges(rc, UIActionType.ADD);
                Assert.AreNotEqual(id, 0);
                Assert.AreEqual(10, ruleTA.GetCountByCatId(id));
            }
            finally
            {
               
            }
        }

        [Test]
        public void Insert_ValidateEmptyName()
        {
            try
            {
                RuleCategory rc = new RuleCategory() { Name = "", InsertedTemplateIDs = new decimal[2] { 1, 2 } };
                businessCategory.SaveChanges(rc, UIActionType.ADD);
                Assert.Fail("نام خالی نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryNameRequierd);
            }

        }

        [Test]
        public void Insert_ValidationDuplicateNameTest()
        {
            try
            {
                RuleCategory rc = new RuleCategory() { Name = "دسته قانون000", InsertedTemplateIDs = new decimal[2] { 1, 2 } };
                businessCategory.SaveChanges(rc, UIActionType.ADD);
                Assert.Fail("نام تکراری نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryNameRepeated);
            }
        }

        [Test]
        public void Update_RuleCategoryWithRulesTest()
        {
            try
            {
                ///////////////////////// اضافه نمودن دسته قانون شامل 10 قانون

                RuleCategory InsertRleCat = new RuleCategory();
                InsertRleCat.Name = "دسته قانون001";
                InsertRleCat.CustomCode = "00-00test1";

                decimal[] ids = new decimal[16];
                int count = 0;
                foreach (DatabaseGateway.TA_RuleTemplateRow item in ruleTmpTA.GetDataByTopCount(10).OrderBy(x => x.RuleTmp_ID))
                {
                    ids[count] = (decimal)item["RuleTmp_ID"];
                    count++;
                }
                InsertRleCat.InsertedTemplateIDs = ids;
                decimal id = businessCategory.SaveChanges(InsertRleCat, UIActionType.ADD);

                //////////////////////////

                ClearSession();
                RuleCategory UpdateRleCat = new RuleCategory();
                UpdateRleCat.ID = id;
                UpdateRleCat.Name = "دسته قانون001 اصلاح شده";
                UpdateRleCat.Discription = "";

                //حذف 4 قانون با کم نمودن شمارنده
                count = 0;
                UpdateRleCat.DeletedTemplateIDs = ids.OrderByDescending(x => x).Take(4).ToArray<decimal>();
                UpdateRleCat.InsertedTemplateIDs = new decimal[10];
                /////////////////////////بروزرسانی دسته قانون به همراه اضافه کردن 10 قانون
                foreach (DatabaseGateway.TA_RuleTemplateRow item in ruleTmpTA.GetDataByTopCount(20).OrderByDescending(x => x.RuleTmp_ID))
                {
                    UpdateRleCat.InsertedTemplateIDs[count] = (decimal)item["RuleTmp_ID"];
                    count++;
                    if (count == 10)
                        break;
                }
                id = businessCategory.SaveChanges(UpdateRleCat, UIActionType.EDIT);
                Assert.AreEqual(17, ruleTA.GetCountByCatId(id));
            }
            finally
            {
            }
        }

        [Test]
        public void Update_ValidateEmptyName()
        {
            try
            {
                RuleCategory rc = new RuleCategory() { ID = AdoRuleCat.ID, Name = "",CustomCode="00-00test1" };
                businessCategory.SaveChanges(rc, UIActionType.ADD);
                Assert.Fail("نام خالی نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryNameRequierd);
            }

        }

        [Test]
        public void Update_ValidationDuplicateNameTest()
        {
            try
            {
                RuleCategory rc = new RuleCategory() { ID = -1, Name = "دسته قانون000" };
                businessCategory.SaveChanges(rc, UIActionType.EDIT);
                Assert.Fail("نام تکراری نباید درج شود");
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryNameRepeated);
            }
        }

        [Test]
        public void Delete_RootTest()
        {
            try
            {
                RuleCategory RC = businessCategory.GetRoot();
                ClearSession();
                businessCategory.SaveChanges(RC, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryRootDeleteIllegal);
            }

        }

        [Test]
        public void Delete_PersonDependencyTest()
        {
            try
            {
                businessCategory.SaveChanges(AdoRuleCat, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.AreEqual(ex.ExceptionList[0].ResourceKey, ExceptionResourceKeys.RuleCategoryUsedByPerson);
            }

        }


        [Test]
        public void Copy_InsertRuleCat_Test() 
        {
            try
            {
                decimal newId = businessCategory.CopyRuleCategory(AdoRuleCat.ID).ID;
                ClearSession();
                ruleCat_testObject = businessCategory.GetByID(newId);
                Assert.AreEqual(newId, ruleCat_testObject.ID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
          
        }

        [Test]
        public void Test_22222() 
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
