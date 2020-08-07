using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.MonthlyReport
{
    public class InfoPeriodicScndCnpValue
    {
        public decimal ScndCnpValue_ID { get; set; }
        public decimal ScndCnpValue_PersonId { get; set; }
        public string ScndCnpValue_KeyColumnName { get; set; }
        public DateTime ScndCnpValue_FromDate { get; set; }
        public DateTime ScndCnpValue_ToDate { get; set; }
        public int ScndCnpValue_Value { get; set; }
        public int ScndCnpValue_FromPairs { get; set; }
        public int ScndCnpValue_ToPairs { get; set; }
        public int ConceptTmp_Color { get; set; }
        public decimal ScndCnpValue_PeriodicScndCnpId { get; set; }
        public DateTime ScndCnpValue_PeriodicFromDate { get; set; }
        public DateTime ScndCnpValue_PeriodicToDate { get; set; }
        public decimal ScndCnpValue_PeriodicValue { get; set; }
    }
}
