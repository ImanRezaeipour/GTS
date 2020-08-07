using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class ShiftsQuery : GTS.Clock.AppSetup.BaseForm
    {
        #region variables
        string shiftQuery = @"select WorkGroup_Name [WorkGroup],dbo.GTS_ASM_MiladiToShamsi( CONVERT(varchar(10), WGDShift_Date ,101)) [Date] ,Shift_Name [Shift],dbo.minutestotime (ShiftPair_From)[From],dbo.minutestotime (ShiftPair_To)[To]
from TA_WorkGroup join WorkGroupDetailShift_View 
on WorkGroup_ID=WGDShift_WGId  join TA_AssignWorkGroup 
on WorkGroup_ID=AsgWorkGroup_WorkGroupId join TA_ShiftPair 
on ShiftPair_ShiftId=WGDShift_ShiftId  join TA_Shift 
on Shift_ID=WGDShift_ShiftId 
where AsgWorkGroup_PersonId=dbo.getperson('{0}') order by [date]";

        string ruleTemplateByColumns = @"select RuleTmp_Name ,RuleTmp_IdentifierCode ,RuleTmp_Order,TmpCondition_ColumnName  from TA_RuleTemplate join TA_RuleTemplateMapping 
on RuleTmp_ID =RleTmpMapping_RuleTemplateId 
join TA_TemplateCondition 
on TmpCondition_ID=RleTmpMapping_TemplateConditionId 
where TmpCondition_ColumnName='{0}'";

        string ruleTemplateByRuleName = @"select RuleTmp_Name ,RuleTmp_IdentifierCode,RuleTmp_IdentifierCode ,RuleTmp_Order,TmpCondition_ColumnName  from TA_RuleTemplate join TA_RuleTemplateMapping 
on RuleTmp_ID =RleTmpMapping_RuleTemplateId 
join TA_TemplateCondition 
on TmpCondition_ID=RleTmpMapping_TemplateConditionId 
where 'R'+Convert(varchar(10),RuleTmp_IdentifierCode )='{0}' or Convert(varchar(10),RuleTmp_IdentifierCode )='{0}'";


        string categorizedRule = @"declare @personId int,@barcode varchar(10)
set @barcode='{0}'
set @barcode= RIGHT('00000000' + CONVERT(VARCHAR,@barcode), 8)
select @personId=prs_ID from TA_Person where Prs_Barcode=@barcode 
select CatRle_ID,CatRle_IdentifierCode,CatRle_CatId,CatRle_Name ,CatRle_PersonId ,CatRle_Order ,CatRle_FromDate ,CatRle_ToDate 
 from CategorisedRule_view join TA_Category on CatRle_CatId =Cat_ID 
 where CatRle_PersonId=@personId
order by CatRle_IdentifierCode ,
CatRle_Order ";

        string permit = @"declare @personId int,@barcode varchar(10)
set @barcode='{0}'
set @barcode= RIGHT('00000000' + CONVERT(VARCHAR,@barcode), 8)
select @personId=prs_ID from TA_Person where Prs_Barcode=@barcode 
select  Permit_ID  ,PermitPair_PishCardID Precard ,dbo.GTS_ASM_MiladiToShamsi(convert(varchar(10),Permit_FromDate,101)) as date
,[dbo].[MinutesToTime](convert(varchar(10),PermitPair_From,101))as [from]
,[dbo].[MinutesToTime] (convert(varchar(10),PermitPair_To,101)) as [to],PermitPair_Value as value,Permit_IsPairly IsPairly 
 
 from TA_Permit join TA_PermitPair   on
 PermitPair_PermitId=Permit_ID 
 where Permit_PersonId=@personId 
 order by Permit_FromDate";

        #endregion
        public ShiftsQuery()
        {
            InitializeComponent();
        }
        
        public string FormState
        {
            get;
            set;
        }
       
        public string FormParameter
        {
            get;
            set;
        }
       
        private void ShiftsQuery_Load(object sender, EventArgs e)
        {
            base.menuStrip1.Visible = false;
            barcodeTB.Text = GTSAppSettings.Barcode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FormState.Length == 0) return;
            string sql = "";
            if (FormState.Equals("shift")) 
            {
                sql = String.Format(shiftQuery, barcodeTB.Text);
            }
            else if (FormState.Equals("rulebycolumn"))
            {
                sql = String.Format(ruleTemplateByColumns, barcodeTB.Text);
            }
            else if (FormState.Equals("rulebyidentifier")) 
            {
                sql = String.Format(ruleTemplateByRuleName, barcodeTB.Text);
            }

            else if (FormState.Equals("categorisedrule"))
            {
                sql = String.Format(categorizedRule, barcodeTB.Text);
            }

            else if (FormState.Equals("permit"))
            {
                sql = String.Format(permit, barcodeTB.Text);
            }

            try
            {
                QueryExecuter exe = new QueryExecuter(null);
                dataGridView1.DataSource = exe.RunQueryResult(sql, GTSAppSettings.SQLConnection);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            
            if (index == 0)
            {
                FormState = "permit";
                label1.Text = "کد پرسنلی";
            }
            else if (index  == 1) 
            {
                FormState = "shift";
                label1.Text = "کد پرسنلی";
            }
            else if (index == 2)
            {
                FormState = "categorisedrule";
                label1.Text = "کد پرسنلی";
            }
            else if (index == 3) 
            {
                FormState = "rulebycolumn";
                label1.Text = "نام ستون";
            }

            else if (index == 4) 
            {
                FormState = "rulebyidentifier";
                label1.Text = "شناسه قانون";
            }
            

        }

        private void ShiftsQuery_ResizeEnd(object sender, EventArgs e)
        {
            topMetginHeight = 6;
            int mergin = 15;
            int windowHight = this.Height - topMetginHeight;
            int windowWdth = this.Width;
            gridContainerGrpBox.Height = windowHight - groupBox1.Height - 50;
            gridContainerGrpBox.Width = windowWdth - 2 * mergin;
            groupBox1.Width = windowWdth - 2 * mergin;
            groupBox1.Location = new Point(groupBox1.Location.X, topMetginHeight);
        }
    }
}
