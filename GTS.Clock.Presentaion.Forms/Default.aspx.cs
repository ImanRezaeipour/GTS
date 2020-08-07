using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.RequestFlow;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //public ISession NHibernateSession
    //{
    //    get
    //    {
    //        return NHibernateSessionManager.Instance.GetSession();
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //decimal managerID = 47750;
        //IList < GTS.Clock.Model.RequestFlow.Request> requestsList = this.NHibernateSession.QueryOver<GTS.Clock.Model.RequestFlow.Request>()
        //                                                                                  .JoinQueryOver<GTS.Clock.Model.RequestFlow.RequestStatus>(req => req.RequestStatusList)
        //                                                                                  .Where(reqStatus => !reqStatus.EndFlow)
        //                                                                                  .JoinQueryOver<ManagerFlow>(reqStatus => reqStatus.ManagerFlow)
        //                                                                                  .JoinQueryOver<Manager>(managerFlow => managerFlow.Manager)
        //                                                                                  .Where(manager => manager.ID == managerID)
        //                                                                                  .List<GTS.Clock.Model.RequestFlow.Request>();
    }

}