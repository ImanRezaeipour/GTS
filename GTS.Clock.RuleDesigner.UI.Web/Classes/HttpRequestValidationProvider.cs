﻿using System;
using System.Web;

/// <summary>
/// Summary description for HttpRequestValidationProvider
/// </summary>
namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    public class HttpRequestValidationProvider : IHttpModule
    {
        public HttpRequestValidationProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication App = (HttpApplication)sender;
            string path = App.Request.Path;

            string strExt = System.IO.Path.GetExtension(path);

            if ((strExt == ".js" || strExt == ".css"))
            {
                if (HttpContext.Current.Request.UrlReferrer == null)
                    App.Context.RewritePath(App.Request.ApplicationPath + "/WhitePage.aspx?DownloadError=DownloadIllegalAccess");
                else
                    App.Context.Response.Write(" ");
            }
        }

    }
}