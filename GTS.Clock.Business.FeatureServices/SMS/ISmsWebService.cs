using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.FeatureServices
{
    [ServiceContract]
    public interface ISmsWebService 
    {
        [OperationContract]
        void SendKartablSmss(IList<InfoServiceProxy> readyForSendSms);

        [OperationContract]
        void SendRequestSmss(IList<InfoServiceProxy> readyForSendSms);
      
        [OperationContract]
        void SendTrafficSmss(IList<InfoServiceProxy> readyForSendSms);

        [OperationContract]
        IList<InfoServiceProxy> GetAllSmsSettings();

        [OperationContract]
        void RunSmsServices(IList<InfoServiceProxy> readyForSendSms);



    }
}
