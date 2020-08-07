using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using NHibernate;
using GTS.Clock.Model;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class BDepartmentTest2 
    {
        private ISessionFactory _sessionFactory = null;

        private UnitOfWork _unitOfWork = null;

        DepartmentRepository depRep = new DepartmentRepository(false);

        DatabaseGatewayTableAdapters.TA_PersonTableAdapter personTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_PersonTableAdapter();
        DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter depTA = new DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
        DatabaseGateway.TA_DepartmentDataTable table = new DatabaseGateway.TA_DepartmentDataTable();

        BDepartment busDep;
        Department ADOdepartmentWithoutPerson = new Department();
        Department department_testObject;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sessionFactory =
                GTS.Clock.Infrastructure.NHibernateFramework.NHibernateSessionManager.Instance.GetSessionFactory();
        }

        [SetUp]
        public void TestSetup()
        {
            _unitOfWork = new UnitOfWork(_sessionFactory);
            //depRep.NHibernateSession = _unitOfWork.Session;
            department_testObject = new Department();
            busDep = new BDepartment();

            ADOdepartmentWithoutPerson = depRep.WithoutTransactSave(new Department() { Name = "Without Person", CustomCode = "test_0", ParentID = 162, ParentPath = "", ChildPath = "" });
            //departmentTA.InsertQuery("Without Person", "test_0", ADORoot.ID, "", "");
            //departmentTA.GetByCustomCode(table, "test_0");
            //ADOdepartmentWithoutPerson.ID = Convert.ToDecimal(table.Rows[0][0]);
 
        }

        [TearDown]
        public void TreatDown()
        {
            depTA.DeleteByCustomCode("0-0");
            depTA.DeleteByCustomCode("test_0");

            _unitOfWork.Rollback();
            _unitOfWork.Dispose();
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            _sessionFactory.Dispose();
        }


        [Test]
        public void GetByID_Test()
        {
            //department_testObject = busDep.GetByID(ADOdepartmentWithoutPerson.ID);
            //Assert.AreEqual(department_testObject.ID, ADOdepartmentWithoutPerson.ID);
        }

        [Test]
        public void find_Test()
        {
            PersonRepository prsRep = new PersonRepository();
            var list = prsRep.Find().Where(x => x.FirstName.Contains("ص"));
            IList<Person> l= list.ToList<Person>();


            var list2 = prsRep.Find().Where(x => x.Department.ID == 221).ToList();
            l = list.ToList<Person>();
        }
 

        [Test]
        public void Update_Test()
        {
            department_testObject.ID = ADOdepartmentWithoutPerson.ID;
            department_testObject.CustomCode = ADOdepartmentWithoutPerson.CustomCode;
            department_testObject.ParentID = ADOdepartmentWithoutPerson.ParentID;
            department_testObject.Name = "Updated";
            busDep.SaveChanges(department_testObject, UIActionType.EDIT);
            department_testObject = busDep.GetByID(ADOdepartmentWithoutPerson.ID);
            Assert.AreEqual(department_testObject.Name, "Updated");
        }

    }
}
