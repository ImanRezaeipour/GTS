using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model
{
    public class RuleCategoryPart : IEntity
    {    
        #region Properties

        public virtual decimal ID
        {
            get;
            set;
        }        

        public virtual RuleCategory Parent
        {
            get;
            set;
        }
        
        public virtual RuleCategory Child
        {
            get;
            set;
        }

        public virtual bool IsContain
        {
            get;
            set;
        }       
        
        #endregion
    }
}
