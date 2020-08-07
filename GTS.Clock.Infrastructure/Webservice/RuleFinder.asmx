<%@ WebService Language="C#" Class="RuleFinder_Webservice" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Xml;
using GTS.Clock.Presentation.View;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
//xml بصورتRuleFinder تامین داده برای   

public class RuleFinder_Webservice : BaseWebService
{
    RuleConvertor_View ruleConvertor_View = new RuleConvertor_View();

    [WebMethod]
    // [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string GetRuleParentCategoriesAsXmlDocument()
    {
        // return ruleConvertor_View.GetRuleParentCategoriesAsXmlDocument();
        return ruleConvertor_View.GetRuleParentCategoryList();
    }

    [WebMethod]
    public string GetRuleChildCategoriesAsXmlDocument(int categoryId)
    {
        return ruleConvertor_View.GetRuleChildCategoryList(categoryId);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool HasRuleCategoryChild(int categoryId)
    {
        return ruleConvertor_View.HasRuleCategoryChild(categoryId);
    }

    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string GetRulesByCategoryAsXmlDocument(int categoryId)
    {
        return ruleConvertor_View.GetRulesByCategoryList(categoryId);
    }
}

