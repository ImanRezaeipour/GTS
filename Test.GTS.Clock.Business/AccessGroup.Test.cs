using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.RequestFlow;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class AccessGroupTest : BaseFixture
    {
        #region Table Adapters
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter accessGroupTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter precardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter accessDetailTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardAccessGroupDetailTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter groupPrecardTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_PrecardGroupsTableAdapter();
        DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter flowTA = new GTSTestUnit.Clock.Business.DatasetGatewayWorkFlowTableAdapters.TA_FlowTableAdapter();
        #endregion

        #region ADOObjects        
        BPrecardAccessGroup busAccessGroup;
        Precard ADOPrecard1 = new Precard();
        Precard ADOPrecard2 = new Precard();
        Precard ADOPrecard3 = new Precard();
        PrecardGroups ADOGroup = new PrecardGroups();

        PrecardAccessGroup accessGroup_testObject;
        PrecardAccessGroup ADOaccessGroup1 = new PrecardAccessGroup();
        PrecardAccessGroup ADOaccessGroup2 = new PrecardAccessGroup();
        Flow ADOFlow = new Flow();
        #endregion

        [SetUp]
        public void TestSetup()
        {
            accessGroup_testObject = new PrecardAccessGroup();
            busAccessGroup = new BPrecardAccessGroup();

            accessGroupTA.Insert("TestAccessGroup1");
            DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable accessTable = new DatasetGatewayWorkFlow.TA_PrecardAccessGroupDataTable();
            accessTable = accessGroupTA.GetDataBy1("TestAccessGroup1");
            ADOaccessGroup1.ID = Convert.ToDecimal(accessTable.Rows[0][0]);
            ADOaccessGroup1.Name = "TestAccessGroup1";
            accessGroupTA.Insert("TestAccessGroup2");
            accessTable = accessGroupTA.GetDataBy1("TestAccessGroup2");
            ADOaccessGroup2.ID = Convert.ToDecimal(accessTable.Rows[0][0]);
            ADOaccessGroup2.Name = "TestAccessGroup2";

            groupPrecardTA.Insert("TestPrecardGroup", "TestGroup1");
            DatasetGatewayWorkFlow.TA_PrecardGroupsDataTable groupTable;
            groupTable = groupPrecardTA.GetDataByName("TestGroup1");
            ADOGroup.ID = Convert.ToDecimal(groupTable.Rows[0][0]);

            precardTA.Insert("TestPish1", true, ADOGroup.ID, true, false, false, "1001", false);
            precardTA.Insert("TestPish2", true, ADOGroup.ID, false, true, false, "1002", false);
            precardTA.Insert("TestPish3", true, ADOGroup.ID, false, true, false, "1003", false);

            DatasetGatewayWorkFlow.TA_PrecardDataTable precardTable = new DatasetGatewayWorkFlow.TA_PrecardDataTable();
            precardTable = precardTA.GetDataByName("TestPish1");
            ADOPrecard1.ID = Convert.ToDecimal(precardTable.Rows[0][0]);
            ADOPrecard1.Name = "TestPish1";
            ADOPrecard1.Active = true;
            ADOPrecard1.IsHourly = true;
            ADOPrecard1.Code = "1001";
            precardTable = precardTA.GetDataByName("TestPish2");
            ADOPrecard2.ID = Convert.ToDecimal(precardTable.Rows[0][0]);
            ADOPrecard2.Name = "TestPish2";
            ADOPrecard2.Active = true;
            ADOPrecard2.IsDaily = true;
            ADOPrecard2.Code = "1002";
            precardTable = precardTA.GetDataByName("TestPish3");
            ADOPrecard3.ID = Convert.ToDecimal(precardTable.Rows[0][0]);
            ADOPrecard3.Name = "TestPish3";
            ADOPrecard3.Active = true;
            ADOPrecard3.IsDaily = true;
            ADOPrecard3.Code = "1003";

            accessDetailTA.Insert(ADOaccessGroup1.ID, ADOPrecard1.ID);
            accessDetailTA.Insert(ADOaccessGroup1.ID, ADOPrecard2.ID);
            accessDetailTA.Insert(ADOaccessGroup1.ID, ADOPrecard3.ID);

            flowTA.Insert(ADOaccessGroup2.ID, false, false, "TestMyFlow");

        }

        [TearDown]
        public void TreatDown()
        {
            flowTA.DeleteByName("TestMyFlow");

            accessGroupTA.DeleteByName("TestAccessGroup1");
            accessGroupTA.DeleteByName("TestAccessGroup2");
            accessGroupTA.DeleteByName("TestAccessGroup3");

            precardTA.DeleteByID("1001");
            precardTA.DeleteByID("1002");
            precardTA.DeleteByID("1003");
            groupPrecardTA.DeleteByName("TestPrecardGroup");
        }

        [Test]
        public void GetByID_Test()
        {
            accessGroup_testObject = busAccessGroup.GetByID(ADOaccessGroup1.ID);
            Assert.AreEqual(ADOaccessGroup1.ID, accessGroup_testObject.ID);
        }

        [Test]
        public void GetAll_Test()
        {
            Assert.AreEqual(accessGroupTA.GetCount(), busAccessGroup.GetAll().Count);
        }

        [Test]
        public void GetPrecardTree_InsertModeTest()
        {
            IList<PrecardGroups> list = busAccessGroup.GetPrecardTree(0);
            Assert.IsTrue(list.Count > 0);

        }

        [Test]
        public void GetPrecardTree_UpdateModeTest()
        {
            IList<PrecardGroups> list = busAccessGroup.GetPrecardTree(ADOaccessGroup1.ID);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOGroup.ID).Count() > 0);
            Assert.IsTrue(list.Where(x => x.ID == ADOGroup.ID).Where(x => x.PrecardList.Contains(ADOPrecard1)).Count() > 0);
            PrecardGroups group = list.Where(x => x.ID == ADOGroup.ID).First();
            Assert.IsTrue(group.ContainInPrecardAccessGroup);
            Precard p = group.PrecardList.Where(x => x.ID == ADOPrecard2.ID && x.ContainInPrecardAccessGroup).FirstOrDefault();
            Assert.IsNotNull(p);
            Assert.AreEqual(ADOPrecard2.ID, p.ID);

        }

        [Test]
        public void Insert_EmptyNameTest() 
        {
            try
            {
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex) 
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AccessGroupNameRequierd));
            }
        }

        [Test]
        public void Insert_RepeatNameTest()
        {
            try
            {
                accessGroup_testObject.Name = ADOaccessGroup1.Name;
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AccessGroupNameRepeated));
            }
        }

        [Test]
        public void Insert_EmptyDetailTest()
        {
            try
            {
                accessGroup_testObject.Name = ADOaccessGroup1.Name + "123";
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.ADD);
                ClearSession();
                Assert.IsTrue(accessGroup_testObject.ID > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally 
            {
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.DELETE);
            }
        }

        [Test]
        public void Insert_DetailTest() 
        {
            try
            {
                accessGroup_testObject.Name = "TestAccessGroup3";
                accessGroup_testObject.PrecardList = new List<Precard>();
                accessGroup_testObject.PrecardList.Add(ADOPrecard1);
                accessGroup_testObject.PrecardList.Add(ADOPrecard2);
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.ADD);
                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(accessGroup_testObject.ID);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(2, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void InsertByProxy_CountTest1()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                AccessGroupProxy proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard1.ID;
                proxy.Checked = true;
                proxyList.Add(proxy);
                proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard2.ID;
                proxy.Checked = true;
                proxyList.Add(proxy);
                decimal id= busAccessGroup.InsertByProxy("TestAccessGroup3", "", proxyList);
    
                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(2, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void InsertByProxy_CountTest2()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                AccessGroupProxy proxy = new AccessGroupProxy();
                proxy.ID = ADOGroup.ID;
                proxy.IsParent = true;
                proxyList.Add(proxy);
                proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard3.ID;
                proxy.Checked = true;
                proxyList.Add(proxy);
                decimal id = busAccessGroup.InsertByProxy("TestAccessGroup3", "", proxyList);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(4, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void InsertByProxy_CountTest3()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                AccessGroupProxy proxy = new AccessGroupProxy();
                proxy.ID = ADOGroup.ID;
                proxy.IsParent = true;
                proxyList.Add(proxy);
                proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard3.ID;
                proxy.Checked = false;
                proxyList.Add(proxy);
                decimal id = busAccessGroup.InsertByProxy("TestAccessGroup3", "", proxyList);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(2, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void InsertByProxy_CountTest4()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                AccessGroupProxy proxy = new AccessGroupProxy();
                proxy.ID = ADOGroup.ID;
                proxy.IsParent = true;
                proxyList.Add(proxy);
                proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard1.ID;
                proxy.Checked = false;
                proxyList.Add(proxy);
                decimal id = busAccessGroup.InsertByProxy("TestAccessGroup3", "", proxyList);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(2, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [Test]
        public void Update_EmptyNameTest()
        {
            try
            {
                accessGroup_testObject.ID = ADOaccessGroup1.ID;
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AccessGroupNameRequierd));
            }
        }

        [Test]
        public void Update_RepeatNameTest()
        {
            try
            {
                accessGroup_testObject.ID = ADOaccessGroup1.ID;
                accessGroup_testObject.Name = ADOaccessGroup2.Name;
                busAccessGroup.SaveChanges(accessGroup_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AccessGroupNameRepeated));
            }
        }

        /// <summary>
        ///  لیست نباید در دیتابیس تغییری داده شود
        /// </summary>
        [Test]       
        public void UpdateByProxy_CountTest1() 
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                decimal id = busAccessGroup.UpdateByProxy(ADOaccessGroup1.ID, "TestAccessGroup3", "", proxyList, false);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(3, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// لیست باید خالی شود
        /// </summary>
        [Test]
        public void UpdateByProxy_CountTest2()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                decimal id = busAccessGroup.UpdateByProxy(ADOaccessGroup1.ID, "TestAccessGroup3", "", proxyList, true);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(0, group.PrecardList.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// اگر لیست داده داشته باشد باید دادهای قبلی حذف و جدید درج شود
        /// </summary>
        [Test]
        public void UpdateByProxy_CountTest3()
        {
            try
            {
                List<AccessGroupProxy> proxyList = new List<AccessGroupProxy>();

                AccessGroupProxy proxy = new AccessGroupProxy();
                proxy.ID = ADOPrecard3.ID;
                proxy.Checked = true;
                proxyList.Add(proxy);

                decimal id = busAccessGroup.UpdateByProxy(ADOaccessGroup1.ID, "TestAccessGroup3", "", proxyList, true);

                ClearSession();
                PrecardAccessGroup group = busAccessGroup.GetByID(id);
                Assert.IsTrue(group.PrecardList != null);
                Assert.AreEqual(1, group.PrecardList.Count);
                Assert.AreEqual(ADOPrecard3.ID, group.PrecardList[0].ID);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }       


        [Test]
        public void Delete_UsedByFlowTest() 
        {
            try
            {
                busAccessGroup.SaveChanges(ADOaccessGroup2, UIActionType.DELETE);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.AccessGroupUsedByFlow));
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busAccessGroup.SaveChanges(ADOaccessGroup1, UIActionType.DELETE);
                ClearSession();
                accessGroup_testObject = busAccessGroup.GetByID(ADOaccessGroup1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass(ex.Message);
            }
        }

      
    }
}
