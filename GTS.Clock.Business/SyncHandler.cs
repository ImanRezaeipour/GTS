using System;
using System.Web;

namespace Business 
{
    public class SyncHandler : IHttpHandler
    {
       
        #region IHttpHandler Members

        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
           
        }

        #endregion
    }
}