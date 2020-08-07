using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Charts
{
    public class Chart : IEntity
    {
        #region variable
        private IList<Unit> unitList;
        #endregion

        #region Property
        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual IList<Unit> UnitList 
        {
            get { return unitList; }
            set { unitList = value; }
        }
        #endregion
    }
}
