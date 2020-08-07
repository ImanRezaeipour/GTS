using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report.Web;
using Stimulsoft.Report;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions.UI;

public partial class ReportViewer : GTSBasePage
{
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    enum Scripts
    {
        ReportViewer_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetReport_ReportViewer();
        this.InitReportViewer_ReportViewer();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    private void InitReportViewer_ReportViewer()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportTitle"))
            hfReportViewerTitle_ReportViewer.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportTitle"]);
        //this.StiReportViewer.ShowExportToXps = false;
        //this.StiReportViewer.ShowExportToText = false;
        //this.StiReportViewer.ShowExportToOdt = false;
        //this.StiReportViewer.ShowExportToCsv = false;
        //this.StiReportViewer.ShowExportToDbf = false;
        //this.StiReportViewer.ShowExportToPcx = false;
    }


    private void GetReport_ReportViewer()
    {
        string[] retMessage = new string[3];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportGUID"))
            {
                string stiReportGUID = HttpContext.Current.Request.QueryString["ReportGUID"];
                if (Session["SysReports"] != null)
                {
                    Dictionary<string, StiReport> SysReportsDic = (Dictionary<string, StiReport>)Session["SysReports"];
                    if (SysReportsDic.Keys.Contains(stiReportGUID))
                    {
                        this.StiReportViewer.Report = SysReportsDic[stiReportGUID];
                        SysReportsDic.Remove(stiReportGUID);
                        Session["SysReports"] = SysReportsDic;
                    }
                }
            }

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
    }
}