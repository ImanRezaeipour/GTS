using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTS.Clock.AppSetup
{
    public partial class ShowQueryForm : BaseForm
    {
        public static string state = "";
        public static string parameter = "";
        public ShowQueryForm()
        {
            InitializeComponent();
        }

        private void ShowQueryForm_Load(object sender, EventArgs e)
        {
            if (state.Equals("catrule"))
            {
                ShowCatRule(parameter);
            }
        }
        private void ShowCatRule(string barcode) 
        {
            string sql = @"select CatRle_ID,CatRle_IdentifierCode,CatRle_CatId,CatRle_Name ,CatRle_PersonId ,CatRle_Order ,CatRle_FromDate ,CatRle_ToDate 
 from CategorisedRule_view join TA_Category on CatRle_CatId =Cat_ID 
 where CatRle_PersonId=dbo.getperson('" + barcode + @"')
order by CatRle_IdentifierCode ,
CatRle_Order ";

            QueryExecuter executer = new QueryExecuter(null);
            dataGridView1.DataSource= executer.RunQueryResult(sql, GTSAppSettings.SQLConnection);
            
        }
    }
}
