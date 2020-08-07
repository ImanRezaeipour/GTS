<%@ Page Language="C#" AutoEventWireUp="true" %>
<%@ import Namespace="ComponentArt.Web.UI" %>
<%@ Import Namespace="GTS.Clock.Business.Charts" %>
<%@ Import Namespace="GTS.Clock.Model.Charts" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GTS.Clock.Infrastructure.Exceptions.UI" %>
<%@ Import Namespace="GTS.Clock.Presentaion.Forms.App_Code" %>
<%@ Import Namespace="GTS.Clock.Business.Security"%>
<%@ Import Namespace="GTS.Clock.Infrastructure" %>
<%@ Import Namespace="GTS.Clock.Business.Presentaion_Helper.Proxy"%>
<% Response.ContentType = "text/xml";%>
<script language="C#" runat="server">
void Page_Load(Object sender,EventArgs e)
{  
  string[] retMessage = new string[3];
  HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
  string requestedPageUrl = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
  ComponentArt.Web.UI.TreeView trvDal = new ComponentArt.Web.UI.TreeView();    
  string LangID = Request.QueryString["LangID"];

  try
  {
      BDataAccess MultiLevelsDataAccessLevelesBusiness = new BDataAccess();
      IList<DataAccessProxy> DalList = null;
      DataAccessLevelsType Dalt = (DataAccessLevelsType)Enum.Parse(typeof(DataAccessLevelsType), Request.QueryString["Dalt"]);
      DataAccessParts Dalk = (DataAccessParts)Enum.Parse(typeof(DataAccessParts), Request.QueryString["Dalk"]);
      decimal UserID = decimal.Parse(Request.QueryString["UserID"].ToString());
      decimal ParentDalID = decimal.Parse(Request.QueryString["ParentDalID"]);
      switch (Dalt)
      {
          case DataAccessLevelsType.Source:
              DalList = MultiLevelsDataAccessLevelesBusiness.GetOrganizationChilds(ParentDalID);              
              break;
          case DataAccessLevelsType.Target:
              DalList = MultiLevelsDataAccessLevelesBusiness.GetOrganizationOfUser(UserID, ParentDalID);
              break;
      }
      foreach (DataAccessProxy childDal in DalList)
      {
          TreeViewNode childDalNode = new TreeViewNode();
          childDalNode.ID = childDal.ID.ToString();
          childDalNode.Text = childDal.Name;
          childDalNode.Value = childDal.DeleteEnable.ToString().ToLower();
          if (Dalk == GTS.Clock.Infrastructure.DataAccessParts.OrganizationUnit)
          {
              childDalNode.ContentCallbackUrl = "XmlMultiLevelsDataAccessLevels.aspx?Dalt=" + Dalt.ToString() + "&Dalk=" + Dalk.ToString() + "&UserID=" + UserID.ToString() + "&ParentDalID=" + childDalNode.ID + "&LangID=" + LangID;
              if (MultiLevelsDataAccessLevelesBusiness.GetOrganizationChilds(childDal.ID).Count > 0)
                  childDalNode.Nodes.Add(new TreeViewNode());
          }
          string ImagePath = string.Empty;
          string ImageUrl = string.Empty;
          if (childDal.DeleteEnable)
          {
              ImagePath = "\\Images\\TreeView\\folder_blue.gif";
              ImageUrl = "Images/TreeView/folder_blue.gif";
          }
          else
          {
              ImagePath = "\\Images\\TreeView\\folder.gif";
              ImageUrl = "Images/TreeView/folder.gif";
          }
          if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
              childDalNode.ImageUrl = ImageUrl;
          trvDal.Nodes.Add(childDalNode);
      }
      Response.Write(trvDal.GetXml());   
  }
  catch (UIValidationExceptions ex)
  {
      this.ParentPageRedirect(requestedPageUrl,LangID,ExceptionTypes.UIValidationExceptions,ex,retMessage);
  }
  catch (UIBaseException ex)
  {
      this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.UIBaseException, ex, retMessage);
  }
  catch (Exception ex)
  {
      this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.Exception, ex, retMessage);      
  }
}

private void ParentPageRedirect(string RequestPageUrl,string CurrentCulture,  ExceptionTypes exceptionType, Exception ex, string[] retMessage)
{
    ExceptionHandler exceptionHandler = new ExceptionHandler();
    exceptionHandler.CurrentPage = RequestPageUrl;
    exceptionHandler.CurrentCulture = CurrentCulture;
    retMessage = exceptionHandler.HandleException(null, exceptionType, ex, retMessage);
    Response.Redirect(RequestPageUrl + "?ErrorType=" + retMessage[0] + "&ErrorBody=" + retMessage[1] + "&error=error" +  "&DalErrorSender=" + this.Page.ToString().Replace("ASP.", "").Replace("_aspx", ".aspx"));
}
</script>