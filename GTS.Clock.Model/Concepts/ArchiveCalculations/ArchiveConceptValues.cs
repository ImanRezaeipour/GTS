using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Model.Concepts
{
    public class ArchiveConceptValues : ArchiveConceptValue
    {
        public string ConceptKeyColumn { get; set; }
 
    }
}