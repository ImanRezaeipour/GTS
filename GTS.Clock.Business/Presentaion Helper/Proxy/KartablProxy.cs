using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
    public class KartablProxy
    {
        public decimal ID { get; set; }


        public decimal ManagerFlowID { get; set; }


        public int FlowLevels { get; set; }

        public RequestState FlowStatus { get; set; }

        public RequestType RequestType { get; set; }

        public RequestSource RequestSource { get; set; }


        public int Row { get; set; }


        public string Barcode { get; set; }

        /// <summary>
        /// درخواست کننده
        /// </summary>
        public string Applicant { get; set; }


        public decimal RequestID
        {
            get;
            set;
        }


        public string RequestTitle { get; set; }


        public string TheFromDate { get; set; }


        public string TheToDate { get; set; }


        public string TheFromTime { get; set; }


        public string TheToTime { get; set; }

        public string TheDuration { get; set; }


        public string Description { get; set; }


        public string ManagerDescription { get; set; }

        /// <summary>
        /// صادر کننده
        /// </summary>
        public string OperatorUser { get; set; }


        public string RegistrationDate { get; set; }

        public PrecardGroupsName PrecardGroup { get; set; }

        /// <summary>
        /// شناسه درخواست دهنده
        /// </summary>
        public decimal PersonId { get; set; }

        public string AttachmentFile { get; set; }

        public string PersonImage { get; set; }

    }
}
