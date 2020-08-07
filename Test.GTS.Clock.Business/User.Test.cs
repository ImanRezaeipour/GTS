using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
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
    public class UserTest : BaseFixture
    {
        #region Table Adapters
        DatabaseGatewayTableAdapters.TA_SecurityRoleTableAdapter roleTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_SecurityRoleTableAdapter();
        DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter userSetTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_UserSettingsTableAdapter();
        #endregion

        #region ADOObjects
        UserProxy puser_testObject;
        User user_testObject;
        Role ADORootRole = new Role();
        Role ADORole1 = new Role();
        Role ADORole2 = new Role();
        BUser busUser;
        #endregion

        [SetUp]
        public void TestSetup()
        {
            puser_testObject = new UserProxy();
            user_testObject = new User();
            busUser = new BUser();

            #region Roles
            DatabaseGateway.TA_SecurityRoleDataTable roleTable = new DatabaseGateway.TA_SecurityRoleDataTable();
            roleTable = roleTA.GetRoot();
            if (roleTable.Rows.Count == 0)
            {
                roleTA.Insert("TestLevel1", 0, true, "", "000-0");
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
            #endregion

            #region user
            userTA.UpdateRoleById(ADORole1.ID, "123", ADOUser1.ID);
            userTA.UpdateRoleById(ADORole2.ID, "123", ADOUser1.ID);


            userSetTA.Insert(ADOUser2.ID, 2, null);
            #endregion
        }

        [TearDown]
        public void TreatDown()
        {
            userTA.DeleteByUsername("TestUser22");
            userTA.DeleteByUsername("TestUserName1");
            userTA.DeleteByUsername("TestUserName2");
            roleTA.DeleteByRoleName("TestLevel1");
            roleTA.DeleteByRoleName("TestRuleName1");
            roleTA.DeleteByRoleName("TestRuleName2");
            personTA.DeleteByBarcode("00001");
            personTA.DeleteByBarcode("00002");
        }

        [Test]
        public void GetByID_Test()
        {
            user_testObject = busUser.GetByID(ADOUser1.ID);
            Assert.AreEqual(user_testObject.ID, ADOUser1.ID);
        }

        [Test]
        public void GetAll_Test()
        {
            IList<User> list = busUser.GetAll();
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }

        //....................
        [Test]
        public void GetAllCount_BySearchRoleName() 
        {
            int count = busUser.GetAllByPageBySearchCount(UserSearchKeys.RoleName, ADORole1.Name.Substring(0, 3));
            Assert.IsTrue(count > 0);

        }

        [Test]
        public void GetAll_BySearchRoleName()
        {
            IList<UserProxy> list = busUser.GetAllByPageBySearch(UserSearchKeys.RoleName, ADORole1.Name.Substring(0, 3), 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }

        //....................
        [Test]
        public void GetAllCount_BySearchPersonName()
        {
            int count = busUser.GetAllByPageBySearchCount(UserSearchKeys.Name, ADOPerson1.Name.Substring(0, 3));
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void GetAll_BySearchPersonName()
        {
            IList<UserProxy> list = busUser.GetAllByPageBySearch(UserSearchKeys.Name, ADOPerson1.Name.Substring(0, 3), 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }
        //....................
        [Test]
        public void GetAllCount_BySearchQuickSearch()
        {
            int count = busUser.GetAllByPageBySearchCount(UserSearchKeys.NotSpecified, ADOPerson1.Name.Substring(0, 3));
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void GetAll_BySearchQuickSearch()
        {
            IList<UserProxy> list = busUser.GetAllByPageBySearch(UserSearchKeys.NotSpecified, ADOPerson1.BarCode.Substring(0, 3), 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }

       
        //....................
        [Test]
        public void GetAllCount_BySearchPersonBarcode()
        {
            int count = busUser.GetAllByPageBySearchCount(UserSearchKeys.PersonCode, ADOPerson1.PersonCode.Substring(0, 3));
            Assert.IsTrue(count > 0);

        }

        [Test]
        public void GetAll_BySearchPersonBarcode()
        {
            IList<UserProxy> list = busUser.GetAllByPageBySearch(UserSearchKeys.PersonCode, ADOPerson1.PersonCode.Substring(0, 3), 0, 50);
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }
        //....................
        [Test]
        public void GetAllCount_BySearchUsername()
        {
            int count = busUser.GetAllByPageBySearchCount(UserSearchKeys.Username, ADOUser1.UserName.Substring(0, 3));
            Assert.IsTrue(count > 0);

        }

        [Test]
        public void GetAll_BySearchUsername()
        {
            IList<UserProxy> list = busUser.GetAllByPageBySearch(UserSearchKeys.Username, ADOUser1.UserName.Substring(0, 3), 0, 10);
            Assert.IsTrue(list.Where(x => x.ID == ADOUser1.ID).Count() > 0);

        }
     
        //....................
        //          Active Directory Test
        [Test]
        public void GetAllADUsers_Test() 
        {
            IList<string> list = busUser.GetActiveDirectoryUsers(1);
            Assert.IsTrue(list.Count > 0);
        }


        [Test]
        public void Insert_Test()
        {
            try
            {              
                puser_testObject.Active = false;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qwewqeqw";
                puser_testObject.ConfirmPassword = "qwewqeqw";
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;

                decimal id = busUser.InsertUser(puser_testObject);
                ClearSession();
                user_testObject = busUser.GetByID(id);
                Assert.AreEqual("TestUser22", user_testObject.UserName);
                Assert.AreEqual(ADORole2.ID, user_testObject.Role.ID);
                
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally 
            {
                ClearSession();
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }

        }

        [Test]
        public void Insert_UserSettingsTest()
        {
            try
            {
                puser_testObject.Active = false;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qwewqeqw";
                puser_testObject.ConfirmPassword = "qwewqeqw";
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;

                decimal id = busUser.InsertUser(puser_testObject);
                ClearSession();
                user_testObject = busUser.GetByID(id);
                Assert.IsNotNull(user_testObject.UserSetting);
                Assert.IsTrue(user_testObject.UserSetting.ID > 0);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                ClearSession();
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }

        }

        [Test]
        public void Insert_ActiveTest1()
        {
            try
            {
                puser_testObject.Active = false;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qwewqeqw";                
                puser_testObject.ConfirmPassword = "qwewqeqw";
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;
                
                decimal id = busUser.InsertUser(puser_testObject);
                ClearSession();
                user_testObject = busUser.GetByID(id);
                Assert.AreEqual(false, user_testObject.Active);
                ClearSession();
                UserProxy p = busUser.GetAllByPageBySearch(UserSearchKeys.Username, "TestUser22", 0, 10).First();
                Assert.AreEqual(false, p.Active);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                ClearSession();
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }

        }

        [Test]
        public void Insert_ActiveTest2()
        {
            try
            {
                puser_testObject.Active = true;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qwewqeqw";
                puser_testObject.ConfirmPassword = "qwewqeqw";
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;

                decimal id = busUser.InsertUser(puser_testObject);
                ClearSession();
                user_testObject = busUser.GetByID(id);
                Assert.AreEqual(true, user_testObject.Active);
                ClearSession();
                UserProxy p = busUser.GetAllByPageBySearch(UserSearchKeys.Username, "TestUser22", 0, 10).First();
                Assert.AreEqual(true, p.Active);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                ClearSession();
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }

        }

        [Test]
        public void Insert_EmptyUserNameTest() 
        {
            try
            {               
                busUser.InsertUser(puser_testObject);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UsernameNotProvided));
            }
            
        }

        [Test]
        public void Insert_RepeatUserNameTest()
        {
            try
            {
                puser_testObject.UserName = ADOUser1.UserName;                
                busUser.InsertUser(puser_testObject);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UsernameReplication));
            }

        }

        [Test]
        public void Insert_PasswordTest()
        {
            try
            {                
                puser_testObject.UserName = "TestMaxUser";
                puser_testObject.Password = "123";
                puser_testObject.ConfirmPassword = "1234";
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;

                decimal id = busUser.InsertUser(puser_testObject);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserConfirmPasswordNotEqual));
            }

        }

        [Test]
        public void Insert_EmptyRoleAndPersonTest()
        {
            try
            {
                busUser.InsertUser(puser_testObject);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserPersonIsNotSpecified));
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UserRoleIsNotSpecified));
            }

        }        

        [Test]
        public void Update_EmptyUserNameTest()
        {
            try
            {
                puser_testObject.ID = ADOUser1.ID;
                                
                busUser.EditUser(puser_testObject);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                Assert.IsTrue(ex.Exists(ExceptionResourceKeys.UsernameNotProvided));
            }

        }

        [Test]
        public void Update_Test()
        {
            try
            {
                puser_testObject.ID = ADOUser1.ID;
                puser_testObject.Active = true;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "3255";
                puser_testObject.ConfirmPassword = "3255";
                puser_testObject.IsPasswodChanged = true;
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;
                busUser.EditUser(puser_testObject);        
               
                ClearSession();
                user_testObject = busUser.GetByID(ADOUser1.ID);
                Assert.AreEqual("TestUser22", user_testObject.UserName);
                Assert.AreEqual(ADORole2.ID, user_testObject.Role.ID);
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void Update_PasswordNotChangedTest1() 
        {
            try
            {
                puser_testObject.ID = ADOUser1.ID;
                puser_testObject.Active = true;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qqqqqq";
                puser_testObject.ConfirmPassword = "qqqqqq";
                puser_testObject.IsPasswodChanged = false;
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;
                busUser.EditUser(puser_testObject);
                ClearSession();
                user_testObject = busUser.GetByID(ADOUser1.ID);
                Assert.AreEqual("123", user_testObject.Password);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally 
            {
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }
        }

        [Test]
        public void Update_PasswordNotChangedTest2()
        {
            try
            {
                puser_testObject.ID = ADOUser1.ID;
                puser_testObject.Active = true;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "qqqqqq";
                puser_testObject.ConfirmPassword = "qqqqqq";
                puser_testObject.IsPasswodChanged = true;
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;
                busUser.EditUser(puser_testObject);
                
                ClearSession();

                puser_testObject.IsPasswodChanged = false;
                puser_testObject.Password = "12313";
                puser_testObject.ConfirmPassword = "12313";                
                busUser.EditUser(puser_testObject);

                ClearSession();
                user_testObject = busUser.GetByID(ADOUser1.ID);
                Assert.IsTrue(Utility.VerifyHashCode("qqqqqq", user_testObject.Password));
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }
        }

        [Test]
        public void Update_UserSettingsTest()
        {
            try
            {
                puser_testObject.ID = ADOUser2.ID;
                puser_testObject.Active = true;
                puser_testObject.UserName = "TestUser22";
                puser_testObject.Password = "3255";
                puser_testObject.ConfirmPassword = "3255";
                puser_testObject.IsPasswodChanged = true;
                puser_testObject.RoleID = ADORole2.ID;
                puser_testObject.PersonID = ADOPerson1.ID;
                busUser.EditUser(puser_testObject);

                ClearSession();
                user_testObject = busUser.GetByID(ADOUser2.ID);
                Assert.IsNotNull(user_testObject.UserSetting);
                Assert.IsTrue(user_testObject.UserSetting.ID > 0);

            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                ClearSession();
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }

        }

        [Test]
        public void Delete_Test() 
        {
            try
            {
                user_testObject.ID = ADOUser1.ID;
                UserProxy proxy = new UserProxy(user_testObject);
                busUser.DeleteUser(proxy);
                ClearSession();
                user_testObject = busUser.GetByID(ADOUser1.ID);
                Assert.Fail(" حذف نشده است");
            }
            catch (ItemNotExists ex) 
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetAllPerson_Test() 
        {
            IList<Person> list= busUser.QuickSearchPersonByPage("", 0, 10);
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void GetAllPersonCount_Test()
        {
            int count= busUser.QuickSearchPersonCount("");
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void ChangePassword_Test1() 
        {
            try
            {
                puser_testObject.ID = BUser.CurrentUser.ID;
                puser_testObject.Active = true;
                puser_testObject.UserName = BUser.CurrentUser.UserName;
                puser_testObject.Password = "qqqqqq";
                puser_testObject.ConfirmPassword = "qqqqqq";
                puser_testObject.IsPasswodChanged = true;
                puser_testObject.RoleID = BUser.CurrentUser.Role.ID;
                puser_testObject.PersonID = BUser.CurrentUser.Person.ID;
                ClearSession();
                busUser.EditUser(puser_testObject);
                ClearSession();
                bool sucs = busUser.ChangePassword("qqqqqq", "ddddd", "ddddd");
                Assert.IsTrue(sucs);
                GTSMembershipProvider mmm = new GTSMembershipProvider();
                bool validate= mmm.ValidateUser(BUser.CurrentUser.UserName, "ddddd");
                Assert.IsTrue(validate);
            }
            catch (UIValidationExceptions ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                busUser.SaveChanges(user_testObject, UIActionType.DELETE);
            }
        }

        [Test]
        public void GetAll_22222() 
        {
            try
            {

                IList<UserProxy> list = busUser.GetAllByPage(0, 30);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

    }
}
