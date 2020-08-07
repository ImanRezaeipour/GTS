<%@ WebService Language="C#" Class="RuleEditor_WebService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Xml;
using GTS.Clock.Presentation.View;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Presentation.Proxy;
using System.Collections.Generic;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
//xml بصورتRuleEditor تامین داده برای   


public class RuleEditor_WebService : BaseWebService
{
    RuleConvertor_View ruleConvertor_View = new RuleConvertor_View();

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public XmlDocument GetRuleDataAsXmlDocument(int ruleId)
    {
        try
        {
            return ruleConvertor_View.GetRuleDataAsXmlDocument(ruleId);
        }
        catch (BaseException  ex)
        {
            ExceptionHandler.HandleException(LogLevel.Error, "Exception in Webservice", ex);
            throw ex;

        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool UpdateRules(List<CategoryProxy>categoryList)
    {
        try
        {
            if (!ruleConvertor_View.UpdateRule(categoryList))
            {
               return  false;
            }
            return true;
        }
        catch (BaseException bx)
        {
            ExceptionHandler.HandleException(LogLevel.Error, "Exception in Webservice", bx);
            throw bx;
            return false;
        }
        catch (NHibernate.HibernateException hx) 
        {
            return false;
        }
        catch (Exception ex)
        {
            //ExceptionHandler.HandleException(LogLevel.Error, "Exception in Webservice", ex);
            throw ex;
            return false;

        }

    }
}

