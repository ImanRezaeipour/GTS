using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace TestWebApplication
{
    public partial class LazyTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            PersonRepository rep = new PersonRepository(false);
            Person prs= rep.GetByBarcode(TextBox1.Text);
            TextBoxr1.Text = prs.ID.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PersonRepository rep = new PersonRepository(false);
            Person prs = rep.GetById(Utility.ToInteger(TextBox2.Text), false);
            TextBoxr2.Text = prs.BarCode.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            PersonRepository rep = new PersonRepository(false);
            Person.GetPersonRepository(false).EnableEfectiveDateFilter(Utility.ToInteger(TextBox3.Text), new DateTime(2012, 1, 1), DateTime.Now, new DateTime(2012, 1, 1), DateTime.Now, new DateTime(2012, 1, 1), DateTime.Now);

            Person prs = rep.GetById(Utility.ToInteger(TextBox3.Text), false);
            TextBoxr3.Text = String.Format("USername:{0}", prs.UserList.First().UserName);
            object obj = prs.AssignedRuleList.Count;
            obj = prs.AssignedWGDShiftList.Count;
            obj = prs.AssignedWorkGroupList.Count;
            obj = prs.BasicTrafficController;
            obj = prs.BasicTrafficList.Count;
            obj = prs.BudgetList;
            //obj = prs.ControlStation;
            obj = prs.CurrentActiveWorkGroup;
            obj = prs.Department;
            obj = prs.EmploymentType;
            obj = prs.LeaveCalcResultList.Count;
            obj = prs.LeaveYearRemainList.Count;
            obj = prs.OrganizationUnit;
            obj = prs.PermitList.Count;
            obj = prs.PersonRangeAssignList.Count;
            obj = prs.ProceedTrafficList.Count;
            obj = prs.ScndCnpValueList.Count;
            obj = prs.ShiftExceptionList.Count;
            //obj = prs.TrafficSettingsList.Count;
            obj = prs.UsedLeaveDetailList.Count;
            obj = prs.PersonDetail;
            obj = prs.PersonTASpec;
            obj = prs.PersonTASpec.ControlStation;
            obj = prs.PersonTASpec.UIValidationGroup;
            obj = prs.PersonTASpec.UIValidationGroup != null ? prs.PersonTASpec.UIValidationGroup.GroupingList : null;
         
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            RoleRepository roleRep = new RoleRepository();
            Role role = roleRep.GetById(Utility.ToInteger(TextBox4.Text), false);
            TextBoxr4.Text = role.UserList.Count.ToString();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            UserRepository userRep = new UserRepository();
            User user = userRep.GetByUserName(TextBox5.Text);
            object obj = user.UserSettingList.First();
            obj = user.UserSettingList.First().Language;
            TextBoxr5.Text = user.Person.ID.ToString();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}