using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure;

public partial class TestPage : GTSBasePage
{
    public BCorporation CorporationBusiness
    {
        get
        {
            return new BCorporation();
        }
    }

    public BDataAccess DataAccessBusiness
    {
        get
        {
            return new BDataAccess();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnInsertCorporation_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 1; i <= 5; i++)
            {
                Corporation corporation = new Corporation() { Name = "Corporation" + i.ToString() };
                this.CorporationBusiness.SaveChanges(corporation, UIActionType.ADD);
            }
        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }
    protected void btnCorporationUserDataAccess_Click(object sender, EventArgs e)
    {
        try
        {
            IList<decimal> UserIdList = new List<decimal>(){535, 14218, 35440};
            for (int i = 0; i < UserIdList.Count; i++)
            {
                this.DataAccessBusiness.InsertDataAccess(DataAccessParts.Corporation, 6, UserIdList[i]);
            }

        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }
    protected void btnGetAllUserDataAccessLevels_Click(object sender, EventArgs e)
    {
        try
        {
            IList<DataAccessProxy> UserDataAccessLevelsList = this.DataAccessBusiness.GetAllByUserId(DataAccessParts.Corporation, 535);
        }
        catch (Exception ex)
        {            
            throw new Exception(ex.Message);
        }
    }
}