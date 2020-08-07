using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Security;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class RoleTest : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_SecurityRoleTableAdapter roleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityRoleTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityResourceTableAdapter resourceTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityResourceTableAdapter();
        DatabaseGatewayTableAdapters.TA_SecurityAuthorizeTableAdapter athorizeTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityAuthorizeTableAdapter();
        #endregion

        #region ADOObjects
        Role role_testObject;
        Role ADORootRole = new Role();
        Role ADORole1 = new Role();
        Role ADORole2 = new Role();
        Resource ADORootResource = new Resource();
        Resource ADORedource1 = new Resource();
        Resource ADORedource2 = new Resource();
        Resource ADORedource3 = new Resource();
        BRole busRole;
        #endregion

        [SetUp]
        public void TestSetup()
        {
            role_testObject = new Role();
            busRole = new BRole();

            DatabaseGateway.TA_SecurityRoleDataTable roleTable = new DatabaseGateway.TA_SecurityRoleDataTable();
            roleTable = roleTA.GetRoot();
            if (roleTable.Rows.Count == 0)
            {
                roleTA.Insert("TestLevel1", 0, true, "","000-0");
                roleTable = roleTA.GetRoot();
            }
            ADORootRole.ID = Convert.ToInt32(roleTable.Rows[0]["role_ID"]);

            roleTA.Insert("TestRuleName1", ADORootRole.ID, true, "", "000-1");
            roleTable = roleTA.GetDataByRoleName("TestRuleName1");
            ADORole1.ID = Convert.ToInt32(roleTable.Rows[0]["role_ID"]);
            ADORole1.Name = Convert.ToString(roleTable.Rows[0]["role_name"]);
            ADORole1.CustomCode = Convert.ToString(roleTable.Rows[0]["role_customcode"]);

            roleTA.Insert("TestRuleName2", ADORole1.ID, true, "", "000-2");
            roleTable = roleTA.GetDataByRoleName("TestRuleName2");
            ADORole2.ID = Convert.ToInt32(roleTable.Rows[0]["role_ID"]);
            ADORole2.Name = Convert.ToString(roleTable.Rows[0]["role_name"]);
            ADORole2.CustomCode = Convert.ToString(roleTable.Rows[0]["role_customcode"]);

            DatabaseGateway.TA_SecurityResourceDataTable resourceTable = new DatabaseGateway.TA_SecurityResourceDataTable();

            resourceTable= resourceTA.GetRoot();
            if (resourceTable.Rows.Count == 0)
            {
                resourceTA.Insert("TestRootResource", 0, "", "", "", "");
                resourceTable = resourceTA.GetRoot();
            }
            ADORootResource.ID = Convert.ToInt32(resourceTable.Rows[0][0]);

            resourceTA.Insert("TestResource1", ADORootResource.ID, "", "", "", "," + ADORootResource.ID.ToString() + ",");
            resourceTable = resourceTA.GetDataByName("TestResource1");
            ADORedource1.ID = Convert.ToInt32(resourceTable.Rows[0][0]);
            ADORedource1.ResourceID = Convert.ToString(resourceTable.Rows[0]["resource_ResourceID"]);

            resourceTA.Insert("TestResource2", ADORootResource.ID, "", "", "", "," + ADORootResource.ID.ToString() + ",");
            resourceTable = resourceTA.GetDataByName("TestResource2");
            ADORedource2.ID = Convert.ToInt32(resourceTable.Rows[0][0]);
            ADORedource2.ResourceID = Convert.ToString(resourceTable.Rows[0]["resource_ResourceID"]);

            resourceTA.Insert("TestResource3", ADORedource1.ID, "", "", "", "," + ADORootResource.ID.ToString() + "," + ADORedource1.ID.ToString() + ",");
            resourceTable = resourceTA.GetDataByName("TestResource3");
            ADORedource3.ID = Convert.ToInt32(resourceTable.Rows[0][0]);
            ADORedource3.ResourceID = Convert.ToString(resourceTable.Rows[0]["resource_ResourceID"]);


            //athorizeTA.Insert(ADORole1.ID, ADORootResource.ID,true);
            athorizeTA.Insert(ADORole1.ID, ADORedource1.ID,true);
            athorizeTA.Insert(ADORole1.ID, ADORedource2.ID,false);
        }

        [TearDown]
        public void TreatDown()
        {
            roleTA.DeleteByRoleName("TestLevel1");
            roleTA.DeleteByRoleName("TestRuleName1");
            roleTA.DeleteByRoleName("TestRuleName2");
            resourceTA.DeleteByName("TestRootResource");
            resourceTA.DeleteByName("TestResource3");
            resourceTA.DeleteByName("TestResource1");
            resourceTA.DeleteByName("TestResource2");
            
        }

        [Test]
        public void GetByID_Test()
        {
            role_testObject = busRole.GetByID(ADORole1.ID);
            Assert.AreEqual(role_testObject.ParentId, ADORootRole.ID);
        }

        [Test]
        public void GetAll_Test()
        {
            IList<Role> list = busRole.GetAll();
            Assert.IsTrue(list.Where(x => x.ID == ADORole1.ID).Count() > 0);

        }

        [Test]
        public void GetTree_Test() 
        {
            try 
            {
                role_testObject = busRole.GetRoleTree();
                Assert.AreEqual(role_testObject.ID, ADORootRole.ID);
                Assert.IsTrue(role_testObject.ChildList.Count > 0);
                Assert.IsTrue(role_testObject.ChildList.Where(x => x.ID == ADORole1.ID).Count() > 0);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Insert_EmptyNameTest() 
        {
            try
            {
                busRole.SaveChanges(role_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleNameRequierd));
            }
            
        }

        [Test]
        public void Insert_RepeatNameTest()
        {
            try
            {
                role_testObject.Name = ADORole1.Name;
                busRole.SaveChanges(role_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleNameReplication));
            }

        }

        [Test]
        public void Insert_RepeatCodeTest()
        {
            try
            {
                role_testObject.CustomCode = ADORole1.CustomCode;
                busRole.SaveChanges(role_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleCodeReplication));
            }

        }

        [Test]
        public void Insert_EmptyParentTest()
        {
            try
            {
                busRole.SaveChanges(role_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleParentNotSpecified));
            }

        }

        [Test]
        public void Update_EmtpyNameTest()
        {
            try
            {
                ADORole1.Name = "";
                busRole.SaveChanges(ADORole1, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleNameRequierd));
            }

        }

        [Test]
        public void Update_EmtpyParentTest()
        {
            try
            {
                ADORole1.Name = "ADOTestRole11";
                ADORole1.ParentId = 0;
                busRole.SaveChanges(ADORole1, UIActionType.EDIT);
                ClearSession();
                role_testObject = busRole.GetByID(ADORole1.ID);
                Assert.AreEqual("ADOTestRole11", role_testObject.Name);
                Assert.AreEqual(ADORootRole.ID, role_testObject.ParentId);
                busRole.SaveChanges(role_testObject, UIActionType.DELETE);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void Update_RepeatNameTest()
        {
            try
            {
                ADORole1.Name = ADORole2.Name;
                busRole.SaveChanges(ADORole1, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleNameReplication));
            }

        }

        [Test]
        public void Update_RepeatCodeTest()
        {
            try
            {
                ADORole1.CustomCode = ADORole2.CustomCode;
                busRole.SaveChanges(ADORole1, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.RoleCodeReplication));
            }
        }

        [Test]
        public void Delete_ChildDeleteTest() 
        {
            try
            {
                busRole.SaveChanges(ADORole1, UIActionType.DELETE);
                ClearSession();
                role_testObject = busRole.GetByID(ADORole2.ID);
                Assert.Fail("بچه حذف نشده است");
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetResources_Test() 
        {
             ResourceProxy p= busRole.GetResources(ADORole1.ID);
             Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource1.ID).Count() > 0);
             Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource2.ID).Count() > 0);
             Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource1.ID).First().IsAllowed);
             Assert.IsFalse(p.ChildList.Where(x => x.ID == ADORedource2.ID).First().IsAllowed);
        }

        [Test]
        public void UpdateAuthorize_Test()
        {
            IList<decimal> resourceIdList = new List<decimal>() { ADORedource2.ID };
            busRole.UpdateAthorize(ADORole1.ID, resourceIdList);
            ClearSession();
            ResourceProxy p = busRole.GetResources(ADORole1.ID);
            Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource1.ID).Count() == 1);
            Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource2.ID).Count() == 1);
            Assert.IsFalse(p.ChildList.Where(x => x.ID == ADORedource1.ID).First().IsAllowed);
            Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource2.ID).First().IsAllowed);
        }

        [Test]
        public void UpdateAuthorize_ParentsUpdateTest()
        {
            IList<decimal> resourceIdList = new List<decimal>() { ADORedource3.ID };
            busRole.UpdateAthorize(ADORole1.ID, resourceIdList);
            ClearSession();
            ResourceProxy p = busRole.GetResources(ADORole1.ID);
            Assert.IsTrue(p.ChildList.Where(x => x.ID == ADORedource1.ID).Count() == 1);
            ResourceProxy proxy = p.ChildList.Where(x => x.ID == ADORedource1.ID).First();
            Assert.IsTrue(proxy.ChildList.Where(x => x.ID == ADORedource3.ID).Count() == 1);

        }

        [Test]
        public void Test_2222222()
        {
            ResourceProxy p = busRole.GetResources(3);          
        }

    }
}
