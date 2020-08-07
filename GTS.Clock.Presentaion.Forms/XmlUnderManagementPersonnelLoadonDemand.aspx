<%@ Page Language="C#" AutoEventWireUp="true" %>
<%@ import Namespace="ComponentArt.Web.UI" %>
<%@ Import Namespace="GTS.Clock.Business.Charts" %>
<%@ Import Namespace="GTS.Clock.Model.Charts" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GTS.Clock.Infrastructure.Exceptions.UI" %>
<%@ Import Namespace="GTS.Clock.Presentaion.Forms.App_Code" %>
<%@ Import Namespace="GTS.Clock.Business.RequestFlow" %>
<%@ Import Namespace="GTS.Clock.Model" %>
<% Response.ContentType = "text/xml";%>
<script language="C#" runat="server">
void Page_Load(Object sender,EventArgs e)
{
  string[] retMessage = new string[3];
  HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
  string requestedPageUrl = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
  ComponentArt.Web.UI.TreeView trvDepartmentsPersonnel = new ComponentArt.Web.UI.TreeView();    
  string LangID = Request.QueryString["LangID"];

  try
  {
      BFlow FlowBusiness = BusinessHelper.GetBusinessInstance<BFlow>();
      decimal flowID = decimal.Parse(Request.QueryString["FlowID"]);
      decimal departmentID = decimal.Parse(Request.QueryString["ParentDepartmentID"]);
      IList<Department> deparmentsList =   FlowBusiness.GetDepartmentChilds(departmentID, flowID);
      foreach (Department childDepartment in deparmentsList)
      {
          string depImageUrl = "Images\\TreeView\\folder.gif";
          string depImagePath = "Images/TreeView/folder.gif";
          TreeViewNode childDepartmentNode = new TreeViewNode();
          childDepartmentNode.ID = childDepartment.ID.ToString();
          childDepartmentNode.Text = childDepartment.Name;
          childDepartmentNode.Value = ((int)UnderManagmentTypes.Department).ToString();
          if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + depImageUrl))
              childDepartmentNode.ImageUrl = depImagePath;
          childDepartmentNode.ContentCallbackUrl = "XmlUnderManagementPersonnelLoadonDemand.aspx?FlowID=" + flowID + "&ParentDepartmentID=" + childDepartment.ID + "&LangID=" + LangID;
          if (FlowBusiness.GetDepartmentChilds(childDepartment.ID, flowID).Count > 0 || FlowBusiness.GetDepartmentPerson(childDepartment.ID).Count > 0)
              childDepartmentNode.Nodes.Add(new TreeViewNode());
          trvDepartmentsPersonnel.Nodes.Add(childDepartmentNode);
      }
      IList<Person> personnelList = FlowBusiness.GetDepartmentPerson(departmentID, flowID);
      foreach (Person childPerson in personnelList)
      {
          string perImageUrl = "Images\\TreeView\\user.png";
          string perImagePath = "Images/TreeView/user.png";
          TreeViewNode childPersonnelNode = new TreeViewNode();
          childPersonnelNode.ID = childPerson.ID.ToString();
          childPersonnelNode.Text = childPerson.FirstName + " " + childPerson.LastName;
          childPersonnelNode.Value = ((int)UnderManagmentTypes.Person).ToString();
          if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + perImageUrl))
              childPersonnelNode.ImageUrl = perImagePath;
          trvDepartmentsPersonnel.Nodes.Add(childPersonnelNode);
      }
      Response.Write(trvDepartmentsPersonnel.GetXml());   
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
    Response.Redirect(RequestPageUrl + "?ErrorType=" + retMessage[0] + "&ErrorBody=" + retMessage[1] + "&error=error" +  "&UnderManagementPersonnelErrorSender=" + this.Page.ToString().Replace("ASP.", "").Replace("_aspx", ".aspx"));
}
</script>