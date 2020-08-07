using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Charts;
using NUnit.Framework;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;

namespace GTSTestUnit.Clock.Business
{
    [TestFixture]
    public class DepartmentRepositoryTest:BaseFixture
    {
        DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter departmentTA = new GTSTestUnit.Clock.Business.DatabaseGatewayTableAdapters.TA_DepartmentTableAdapter();
        DepartmentRepository depRep;
        
        Department ADORoot = new Department();
        Department ADODepartment1 = new Department();
        Department ADODepartment2 = new Department();



        [SetUp]
        public void TestSetup() 
        {
            depRep=new DepartmentRepository(false);
            DatabaseGateway.TA_DepartmentDataTable depTable = new DatabaseGateway.TA_DepartmentDataTable();
            depTable = departmentTA.GetRoot();
            if (depTable.Rows.Count == 0)
            {
                departmentTA.InsertQuery("TestLevel1", "123", 0, "", "");
                depTable = departmentTA.GetRoot();
            }

            ADORoot.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
            ADORoot.ParentID = Utility.ToInteger(depTable.Rows[0]["dep_ParentID"]);
            ADORoot.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
            ADORoot.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);

            //decimal id = Convert.ToDecimal(departmentTA.InsertQuery("Parent", "123", 0,"",""));
            departmentTA.Insert("Child", "1234", ADORoot.ID, "", "");
            departmentTA.Insert("child", "1235", ADORoot.ID, "", "");
            departmentTA.Insert("Parent", "124", null,"","");

            departmentTA.GetByCustomCode(depTable, "1234");
            ADODepartment1.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
            ADODepartment1.ParentID = Convert.ToInt32(depTable.Rows[0]["dep_ParentID"]);
            ADODepartment1.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
            ADODepartment1.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);

            departmentTA.GetByCustomCode(depTable, "1235");
            ADODepartment2.ID = Convert.ToInt32(depTable.Rows[0]["dep_ID"]);
            ADODepartment2.ParentID = Convert.ToInt32(depTable.Rows[0]["dep_ParentID"]);
            ADODepartment2.Name = Convert.ToString(depTable.Rows[0]["dep_Name"]);
            ADODepartment2.CustomCode = Convert.ToString(depTable.Rows[0]["dep_CustomCode"]);
        }

        [TearDown]
        public void TreatDown() 
        {
            departmentTA.DeleteByCustomCode("123");
            departmentTA.DeleteByCustomCode("124");
            departmentTA.DeleteByCustomCode("1234");
            departmentTA.DeleteByCustomCode("1235");

        }

        /// <summary>
        /// آیا گره های والد را درست برمیگرداند
        /// </summary>
        [Test]
        public void GetParentsNodes()
        {
            IList<Department> list = depRep.GetDepartmentTree();
            Assert.IsTrue(list.Where(x => x.CustomCode == ADORoot.CustomCode).Count() > 0);
        }
        [Test]
        public void GetParentsNodesIfParentNull()
        {
            IList<Department> list = depRep.GetDepartmentTree();
            Assert.IsTrue(list.Where(x => x.CustomCode == "124").Count() > 0);
        }
        [Test]
        public void GetChilds()
        {
            IList<Department> list = depRep.GetDepartmentTree();
            Department dep = list.Where(x => x.CustomCode == ADORoot.CustomCode).First();
            Assert.IsTrue(dep.ChildList.Where(x=>x.ID == ADODepartment1.ID).Count()==1);
            Assert.IsTrue(dep.ChildList.Where(x => x.ID == ADODepartment2.ID).Count() == 1);
        }
    }
}
