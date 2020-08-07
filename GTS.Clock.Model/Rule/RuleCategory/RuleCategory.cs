using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.CustomeXMLConvertor;
namespace GTS.Clock.Model
{
    [XMLConvertorRoot("categories")]
    public class RuleCategory : IEntity
    {
        #region Properties

        [XMLConvertorElement("ID")]
        public virtual decimal ID
        {
            get;
            set;
        }

        public virtual bool IsRoot
        {
            get;
            set;
        }

        public virtual string Discription
        {
            get;
            set;
        }
       
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string CustomCode { get; set; }

        protected internal virtual IList<RuleCategory> ParentList
        {
            get;
            set;
        }

        public virtual RuleCategory Parent
        {
            get
            {
                return this.ParentList[0];
            }
            set
            {
                if (this.ParentList == null)
                    this.ParentList = new List<RuleCategory>();
                this.ParentList.Insert(0, value);
            }
        }

        /// <summary>
        /// جهت نمایش در درخت
        /// </summary>
        public virtual bool Visible { get; set; }

        public virtual IList<Rule> RuleList
        {
            get;
            set;
        }

        public virtual IList<RuleCategory> ChildList
        {
            get;
            set;
        }

        public virtual decimal[] InsertedTemplateIDs
        {
            get;
            set;
        }

        public virtual decimal[] DeletedTemplateIDs
        {
            get;
            set;
        }

        public virtual IList<PersonRuleCatAssignment> PersonRuleCatAssignList
        {
            get;
            set;
        }      
        

        #endregion

        public override string ToString()
        {
            string summery = "";
            summery = String.Format("نام:{0} میباشد ", this.Name);
            return summery;
        }


    }

    public class RuleCategoryComparer : IEqualityComparer<RuleCategory>
    {
        public bool Equals(RuleCategory x, RuleCategory y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(RuleCategory obj)
        {
            return obj.ID.GetHashCode();
        }
    }

}
