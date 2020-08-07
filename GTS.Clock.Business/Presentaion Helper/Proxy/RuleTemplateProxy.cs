using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Proxy
{
    public class RuleTemplateProxy
    {
        public RuleTemplateProxy(RuleTemplate ruleTemplate)
        {
            TemplateId = ruleTemplate.ID;
            Script = ruleTemplate.Script;
            Name = ruleTemplate.Name;
            IdentifierCode = ruleTemplate.IdentifierCode;
            CustomCategoryCode = ruleTemplate.CustomCategoryCode;
            TypeId = ruleTemplate.TypeId;
            CSharpCode = ruleTemplate.CSharpCode;
            UserDefined = ruleTemplate.UserDefined;
            JsonObject = ruleTemplate.JsonObject;
        }

        #region properties
        public string CustomCategoryCode { get; set; }
        public decimal IdentifierCode { get; set; }
        public string Name { get; set; }
        public decimal TemplateId { get; set; }
        public decimal CategoryId { get; set; }
        public decimal TypeId { get; set; }
        public string Type { get; set; }
        public bool IsForcible { get; set; }
        public string Script { get; set; }
        public string CSharpCode { get; set; }
        public bool UserDefined { get; set; }
        public string JsonObject { get; set; }
        #endregion
    }

}
