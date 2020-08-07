<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="GTS.Clock.Business.RuleDesigner" %>
<%@ Import Namespace="GTS.Clock.Infrastructure.Exceptions.UI" %>
<%@ Import Namespace="GTS.Clock.RuleDesigner.UI.Web.Classes.ConceptEditor" %>
<% Response.ContentType = "text/xml";%>
<script language="C#" runat="server">
	
    void Page_Load(Object sender, EventArgs e)
    {
        //var retMessage = new string[3];
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //var requestedPageUrl = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
        var trvOrgPosts = new ComponentArt.Web.UI.TreeView();
        var langId = Request.QueryString["LangID"];
        bool editable = Request.QueryString["Editable"] == "1" || Request.QueryString["Editable"] == "true";

        try
        {
            var NodeValueExpressions_Object = new NodeValueExpressions();
            
            var conceptExpressions = new BConceptExpression();
            var orgUnitList = conceptExpressions.GetByParentId(decimal.Parse(Request.QueryString["CncptExprsnId"]));

            NodeValueExpressions_Object.MakeJsonObjectListString(orgUnitList, trvOrgPosts, langId, editable);

            Response.Write(trvOrgPosts.GetXml());
        }
        catch (UIValidationExceptions ex)
        {
            //this.ParentPageRedirect(requestedPageUrl,LangID,ExceptionTypes.UIValidationExceptions,ex,retMessage);
        }
        catch (UIBaseException ex)
        {
            //this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.UIBaseException, ex, retMessage);
        }
        catch (Exception ex)
        {
            //this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.Exception, ex, retMessage);      
        }
    }

</script>
