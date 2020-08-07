using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace GTS.Clock.Business.Engine.WebServices
{
    [ServiceContract]
    public interface ITotalWebService 
    {
        [OperationContract]
        void GTS_ExecuteByPersonID(string CallerIdentity, decimal PersonId);

        //[OperationContract]
        //void GTS_FillByPersonBarCode(string PersonBarCode);

        [OperationContract]
        bool GTS_ExecuteByPersonIdAndToDate(string CallerIdentity, decimal PersonId, DateTime Date);

        [OperationContract(IsOneWay = true)]
        void GTS_ExecuteAll(string CallerIdentity);

        [OperationContract(IsOneWay = true)]
        void GTS_ExecuteAllByToDate(string CallerIdentity, DateTime Date);

        [OperationContract]
        bool GTS_ExecutePersonsByToDate(string CallerIdentity, IList<decimal> Persons, DateTime Date);

        [OperationContract]
        int GTS_GETTotalExecuting(string CallerIdentity);

        [OperationContract]
        int GTS_GETRemainExecuting(string CallerIdentity);

        [OperationContract]
        bool GTS_LockCalculation();

        [OperationContract]
        bool GTS_UnLockCalculation();
    

    }
}
