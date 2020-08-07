using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;


namespace GTSTestUnit.Clock.Business
{
    /// <summary>
    /// created at: 2012-01-09 5:22:47 PM
    /// write your name here
    /// </summary>
    [TestFixture]
    public class OperatorBusinessTest : BaseSubstituteOperator
    {
        BOperator busOperator;
        Operator operator_testObject;

        [SetUp]
        public void TestSetup()
        {
            busOperator = new BOperator();
            operator_testObject = new Operator();

            #region Operator
            operatorTA.Insert(ADOPerson1.ID, true, ADOFlow1.ID, "");
            operatorTA.Insert(ADOPerson3.ID, true, ADOFlow2.ID, "");

            DatasetGatewayWorkFlow.TA_OperatorDataTable opTable = operatorTA.GetByPesonId(ADOPerson1.ID);
            ADOOperator1.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;

            opTable = operatorTA.GetByPesonId(ADOPerson3.ID);
            ADOOperator2.ID = (opTable.Rows[0] as DatasetGatewayWorkFlow.TA_OperatorRow).opr_ID;


            #endregion
        }

        [TearDown]
        public void TreatDown()
        {
        }

        [Test]
        public void GetById_Test()
        {
            operator_testObject = busOperator.GetByID(ADOOperator1.ID);
            Assert.IsTrue(operator_testObject.ID > 0);
        }

        [Test]
        public void Insert_Test()
        {
            try
            {
                operator_testObject.Flow = ADOFlow1;
                operator_testObject.Person = ADOPerson4;
                operator_testObject.Active = true;
                operator_testObject.Description = "";
                busOperator.SaveChanges(operator_testObject, UIActionType.ADD);
                Assert.IsTrue(operator_testObject.ID > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                busOperator.SaveChanges(operator_testObject, UIActionType.DELETE);
            }
        }

        [Test]
        public void Insert_PersonRequiered_Test()
        {
            try
            {
                operator_testObject.Flow = ADOFlow1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorPersonIsRequierd);
            }           
        }

        [Test]
        public void Insert_PersonRepeat_Test()
        {
            try
            {
                operator_testObject.Flow = ADOFlow1;
                operator_testObject.Person = ADOPerson1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorRepeated);
            }
        }

        [Test]
        public void Insert_FlowRequiered_Test()
        {
            try
            {
                operator_testObject.Person = ADOPerson1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.ADD);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorFlowIsRequierd);
            }
        }


        [Test]
        public void Update_Test()
        {
            try
            {
                operator_testObject = busOperator.GetByID(ADOOperator1.ID);
                ClearSession();
                operator_testObject.Active = false;
                busOperator.SaveChanges(operator_testObject, UIActionType.EDIT);
                ClearSession();
                operator_testObject = new Operator();
                operator_testObject = busOperator.GetByID(ADOOperator1.ID);
                Assert.AreEqual(false, operator_testObject.Active);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Update_PersonRequiered_Test()
        {
            try
            {
                operator_testObject.Flow = ADOFlow1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorPersonIsRequierd);
            }
        }

        [Test]
        public void Update_PersonRepeat_Test()
        {
            try
            {
                operator_testObject.Flow = ADOFlow1;
                operator_testObject.Person = ADOPerson1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorRepeated);
            }
        }

        [Test]
        public void Update_FlowRequiered_Test()
        {
            try
            {
                operator_testObject.Person = ADOPerson1;
                operator_testObject.Active = true;
                busOperator.SaveChanges(operator_testObject, UIActionType.EDIT);
                Assert.Fail();
            }
            catch (UIValidationExceptions ex)
            {
                ex.Exists(ExceptionResourceKeys.OperatorFlowIsRequierd);
            }
        }

        [Test]
        public void Delete_Test()
        {
            try
            {
                busOperator.SaveChanges(ADOOperator1, UIActionType.DELETE);
                ClearSession();
                operator_testObject = busOperator.GetByID(ADOOperator1.ID);
                Assert.Fail();
            }
            catch (ItemNotExists ex)
            {
                Assert.Pass();
            }
        }
    }
}
