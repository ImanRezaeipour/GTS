using System;
using System.Collections.Generic;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    /// <summary>
    /// Summary description for AdvancedPersonnelSearchProvider
    /// </summary>
    public class AdvancedPersonnelSearchProvider
    {
        public PersonAdvanceSearchProxy CreateAdvancedPersonnelSearchProxy(string StrObjPersonnelAdvancedSearch)
        {
            PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, object> ParamDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(StrObjPersonnelAdvancedSearch);
            if (int.Parse(ParamDic["Sex"].ToString()) != -1)
                personAdvanceSearchProxy.Sex = (PersonSex)Enum.ToObject(typeof(PersonSex), int.Parse(ParamDic["Sex"].ToString()));
            if (ParamDic["FatherName"].ToString() != string.Empty)
                personAdvanceSearchProxy.FatherName = ParamDic["FatherName"].ToString();
            if (int.Parse(ParamDic["MarriageState"].ToString()) != -1)
                personAdvanceSearchProxy.MaritalStatus = (MaritalStatus)Enum.ToObject(typeof(MaritalStatus), int.Parse(ParamDic["MarriageState"].ToString()));
            if (int.Parse(ParamDic["MilitaryState"].ToString()) != -1)
                personAdvanceSearchProxy.Military = (MilitaryStatus)Enum.ToObject(typeof(MilitaryStatus), int.Parse(ParamDic["MilitaryState"].ToString()));
            if (ParamDic["Education"].ToString() != string.Empty)
                personAdvanceSearchProxy.Education = ParamDic["Education"].ToString();
            if (ParamDic["BirthLocation"].ToString() != string.Empty)
                personAdvanceSearchProxy.BirthPlace = ParamDic["BirthLocation"].ToString();
            if (ParamDic["CardNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.CartNumber = ParamDic["CardNumber"].ToString();
            if (ParamDic["EmployNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.EmployeeNumber = ParamDic["EmployNumber"].ToString();
            if (decimal.Parse(ParamDic["DepartmentID"].ToString()) != 0)
                personAdvanceSearchProxy.DepartmentId = decimal.Parse(ParamDic["DepartmentID"].ToString());
            personAdvanceSearchProxy.IncludeSubDepartments = bool.Parse(ParamDic["IsContainsSubDepartment"].ToString());
            if (decimal.Parse(ParamDic["OrganizationPostID"].ToString()) != 0)
                personAdvanceSearchProxy.OrganizationUnitId = decimal.Parse(ParamDic["OrganizationPostID"].ToString());
            if (decimal.Parse(ParamDic["EmployTypeID"].ToString()) != 0)
                personAdvanceSearchProxy.EmploymentType = decimal.Parse(ParamDic["EmployTypeID"].ToString());
            if (ParamDic["EmployFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromEmploymentDate = ParamDic["EmployFromDate"].ToString();
            if (ParamDic["EmployToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToEmploymentDate = ParamDic["EmployToDate"].ToString();
            if (decimal.Parse(ParamDic["ControlStationID"].ToString()) != 0)
                personAdvanceSearchProxy.ControlStationId = decimal.Parse(ParamDic["ControlStationID"].ToString());
            if (ParamDic["FromBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromBirthDate = ParamDic["EmployToDate"].ToString();
            if (ParamDic["ToBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToBirthDate = ParamDic["ToBirthDate"].ToString();
            if (decimal.Parse(ParamDic["WorkGroupID"].ToString()) != 0)
                personAdvanceSearchProxy.WorkGroupId = decimal.Parse(ParamDic["WorkGroupID"].ToString());
            if (ParamDic["WorkGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.WorkGroupFromDate = ParamDic["WorkGroupFromDate"].ToString();
            if (decimal.Parse(ParamDic["RuleGroupID"].ToString()) != 0)
                personAdvanceSearchProxy.RuleGroupId = decimal.Parse(ParamDic["RuleGroupID"].ToString());
            if (ParamDic["RuleGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupFromDate = ParamDic["RuleGroupFromDate"].ToString();
            if (ParamDic["RuleGroupToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupToDate = ParamDic["RuleGroupToDate"].ToString();
            if (decimal.Parse(ParamDic["CalculationRangeID"].ToString()) != 0)
                personAdvanceSearchProxy.CalculationDateRangeId = decimal.Parse(ParamDic["CalculationRangeID"].ToString());
            if (ParamDic["CalculationRangeFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.CalculationFromDate = ParamDic["CalculationRangeFromDate"].ToString();
            return personAdvanceSearchProxy;
        }
    }

}