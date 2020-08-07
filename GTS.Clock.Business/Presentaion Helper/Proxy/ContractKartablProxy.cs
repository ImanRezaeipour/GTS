using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using System.Runtime.Serialization;
using GTS.Clock.Business;

namespace GTS.Clock.Business.Proxy
{
    [DataContract]
    public class ContractKartablProxy
    {
        [DataMember]
        public decimal ID { get; set; }

        [DataMember]
        public decimal ManagerFlowID { get; set; }

        [DataMember]
        public int FlowLevels { get; set; }

        
        //public ContractRequestState FlowStatus { get; set; }
        
        [DataMember]
        public string FlowStatus { get; set; }

        //public ContractRequestType RequestType { get; set; }

        [DataMember]
        public string RequestType { get; set; }

        //[DataMember]
        //public ContractRequestSource RequestSource { get; set; }

        [DataMember]
        public string RequestSource { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        /// <summary>
        /// درخواست کننده
        /// </summary>
        [DataMember]
        public string Applicant { get; set; }

        [DataMember]
        public decimal RequestID
        {
            get;
            set;
        }

        [DataMember]
        public string RequestTitle { get; set; }

        [DataMember]
        public string TheFromDate { get; set; }

        [DataMember]
        public string TheToDate { get; set; }

        [DataMember]
        public string TheFromTime { get; set; }

        [DataMember]
        public string TheToTime { get; set; }

        [DataMember]
        public string TheDuration { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ManagerDescription { get; set; }

        /// <summary>
        /// صادر کننده
        /// </summary>
        [DataMember]        
        public string OperatorUser { get; set; }

        [DataMember]
        public string RegistrationDate { get; set; }

        //[DataMember]
        //public PrecardGroupsName PrecardGroup { get; set; }
        
        [DataMember]
        public string PrecardGroup { get; set; }

        /// <summary>
        /// شناسه درخواست دهنده
        /// </summary>
        [DataMember]
        public decimal PersonId { get; set; }


    }
}
