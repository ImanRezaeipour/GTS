using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure;

/// <summary>
/// Summary description for AdvancedPersonnelSearchProvider
/// </summary>
public class AdvancedPersonnelSearchProvider
{
    public PersonAdvanceSearchProxy CreateAdvancedPersonnelSearchProxy(string StrObjPersonnelAdvancedSearch)
	{
        PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        if (StrObjPersonnelAdvancedSearch != string.Empty)
        {
            Dictionary<string, object> ParamDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(StrObjPersonnelAdvancedSearch);
            if (ParamDic.ContainsKey("Active") && ParamDic["Active"].ToString() == string.Empty)
                personAdvanceSearchProxy.PersonActivateState = null;
            else
                personAdvanceSearchProxy.PersonActivateState = bool.Parse(ParamDic["Active"].ToString());
            if (ParamDic.ContainsKey("Sex") && int.Parse(ParamDic["Sex"].ToString()) != -1)
                personAdvanceSearchProxy.Sex = (PersonSex)Enum.ToObject(typeof(PersonSex), int.Parse(ParamDic["Sex"].ToString()));
            if (ParamDic.ContainsKey("FatherName") && ParamDic["FatherName"].ToString() != string.Empty)
                personAdvanceSearchProxy.FatherName = ParamDic["FatherName"].ToString();
            if (ParamDic.ContainsKey("MarriageState") && int.Parse(ParamDic["MarriageState"].ToString()) != -1)
                personAdvanceSearchProxy.MaritalStatus = (MaritalStatus)Enum.ToObject(typeof(MaritalStatus), int.Parse(ParamDic["MarriageState"].ToString()));
            if (ParamDic.ContainsKey("MilitaryState") && int.Parse(ParamDic["MilitaryState"].ToString()) != -1)
                personAdvanceSearchProxy.Military = (MilitaryStatus)Enum.ToObject(typeof(MilitaryStatus), int.Parse(ParamDic["MilitaryState"].ToString()));
            if (ParamDic.ContainsKey("Education") && ParamDic["Education"].ToString() != string.Empty)
                personAdvanceSearchProxy.Education = ParamDic["Education"].ToString();
            if (ParamDic.ContainsKey("BirthLocation") && ParamDic["BirthLocation"].ToString() != string.Empty)
                personAdvanceSearchProxy.BirthPlace = ParamDic["BirthLocation"].ToString();
            if (ParamDic.ContainsKey("CardNumber") && ParamDic["CardNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.CartNumber = ParamDic["CardNumber"].ToString();
            if (ParamDic.ContainsKey("EmployNumber") && ParamDic["EmployNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.EmployeeNumber = ParamDic["EmployNumber"].ToString();
            if (ParamDic.ContainsKey("DepartmentID") && decimal.Parse(ParamDic["DepartmentID"].ToString()) != 0)
                personAdvanceSearchProxy.DepartmentId = decimal.Parse(ParamDic["DepartmentID"].ToString());
            if (ParamDic.ContainsKey("IsContainsSubDepartment"))
                personAdvanceSearchProxy.IncludeSubDepartments = bool.Parse(ParamDic["IsContainsSubDepartment"].ToString());
            if (ParamDic.ContainsKey("OrganizationPostID") && decimal.Parse(ParamDic["OrganizationPostID"].ToString()) != 0)
                personAdvanceSearchProxy.OrganizationUnitId = decimal.Parse(ParamDic["OrganizationPostID"].ToString());
            if (ParamDic.ContainsKey("EmployTypeID") && decimal.Parse(ParamDic["EmployTypeID"].ToString()) != 0)
                personAdvanceSearchProxy.EmploymentType = decimal.Parse(ParamDic["EmployTypeID"].ToString());
            if (ParamDic.ContainsKey("EmployFromDate") && ParamDic["EmployFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromEmploymentDate = ParamDic["EmployFromDate"].ToString();
            if (ParamDic.ContainsKey("EmployToDate") && ParamDic["EmployToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToEmploymentDate = ParamDic["EmployToDate"].ToString();
            if (ParamDic.ContainsKey("ControlStationID") && decimal.Parse(ParamDic["ControlStationID"].ToString()) != 0)
                personAdvanceSearchProxy.ControlStationId = decimal.Parse(ParamDic["ControlStationID"].ToString());
            if (ParamDic.ContainsKey("FromBirthDate") && ParamDic["FromBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromBirthDate = ParamDic["EmployToDate"].ToString();
            if (ParamDic.ContainsKey("ToBirthDate") && ParamDic["ToBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToBirthDate = ParamDic["ToBirthDate"].ToString();
            if (ParamDic.ContainsKey("WorkGroupID") && decimal.Parse(ParamDic["WorkGroupID"].ToString()) != 0)
                personAdvanceSearchProxy.WorkGroupId = decimal.Parse(ParamDic["WorkGroupID"].ToString());
            if (ParamDic.ContainsKey("WorkGroupFromDate") && ParamDic["WorkGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.WorkGroupFromDate = ParamDic["WorkGroupFromDate"].ToString();
            if (ParamDic.ContainsKey("RuleGroupID") && decimal.Parse(ParamDic["RuleGroupID"].ToString()) != 0)
                personAdvanceSearchProxy.RuleGroupId = decimal.Parse(ParamDic["RuleGroupID"].ToString());
            if (ParamDic.ContainsKey("RuleGroupFromDate") && ParamDic["RuleGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupFromDate = ParamDic["RuleGroupFromDate"].ToString();
            if (ParamDic.ContainsKey("RuleGroupToDate") && ParamDic["RuleGroupToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupToDate = ParamDic["RuleGroupToDate"].ToString();
            if (ParamDic.ContainsKey("CalculationRangeID") && decimal.Parse(ParamDic["CalculationRangeID"].ToString()) != 0)
                personAdvanceSearchProxy.CalculationDateRangeId = decimal.Parse(ParamDic["CalculationRangeID"].ToString());
            if (ParamDic.ContainsKey("CalculationRangeFromDate") && ParamDic["CalculationRangeFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.CalculationFromDate = ParamDic["CalculationRangeFromDate"].ToString();
        }
        return personAdvanceSearchProxy;
	}


}