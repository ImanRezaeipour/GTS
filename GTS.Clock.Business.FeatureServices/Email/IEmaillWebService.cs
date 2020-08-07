using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.FeatureServices
{
    [ServiceContract]
    public interface IEmailWebService 
    {
        [OperationContract]
        void SendKartablEmails(IList<InfoServiceProxy> readyForSendEmail);

        [OperationContract]
        void SendRequestEmails(IList<InfoServiceProxy> readyForSendEmail);
      
        [OperationContract]
        void SendTrafficEmails(IList<InfoServiceProxy> readyForSendEmail);

        [OperationContract]
        IList<InfoServiceProxy> GetAllEmailSettings();

        [OperationContract]
        void RunEmailServices(IList<InfoServiceProxy> readyForSendEmail);



    }
}
