using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace GTS.Clock.AppService.Host
{
    public partial class Clock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void calcReadyBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;         
           
            connection = new SqlConnection(GetConnectionString("DBConnection"));
            msgLbl.Text = "";

            try
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(String.Format("exec dbo.calcready '{0}','{1}'", barcodeTB.Text.PadLeft(8, '0'), dateTB.Text), connection);
                    cmd.ExecuteReader();
                }
                else
                {
                    msgLbl.Text = "DBConncetion is null";
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            barcode2TB.Text = barcodeTB.Text;

            string date = dateTB.Text;
            fromTB.Text = date;
            string[] strs = date.Split('/');
            string mounth = (Convert.ToInt32(strs[1])).ToString().PadLeft(2, '0');
            toDateTB.Text = String.Format("{0}/{1}/{2}", strs[0], mounth , 31 /*strs[2]*/);
            msgLbl.Text = "آماده سازی صورت گرفت";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            GTS.Clock.Business.WebServices.TotalWebService total = new GTS.Clock.Business.WebServices.TotalWebService();
            //total.GTS_FillByPersonBarCodeAndToDate(barcodeTB.Text, toDateTB.Text);
            //GTS.Clock.Infrastructure.Repository.PersonRepository pr=new GTS.Clock.Infrastructure.Repository.PersonRepository();

            //GTS.Clock.Model.TrafficMapping.TrafficMapper y = new GTS.Clock.Model.TrafficMapping.TrafficMapper(pr.GetByBarcode("0070020"));
            //y.DoMap();
            msgLbl.Text = "پایان محاسبات";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            string[] barcods = barcodesTB.Text.Split(',');
            SqlConnection connection = null;

            connection = new SqlConnection(GetConnectionString("DBConnection"));
            foreach (string barcode in barcods)
            {
                try
                {
                    if (connection != null)
                    {
                        connection.Open();

                        SqlCommand cmd = new SqlCommand(String.Format("exec dbo.calcready '{0}','{1}'", barcode.PadLeft(8, '0'), datsTB.Text), connection);
                        cmd.ExecuteReader();
                    }
                    else
                    {
                        msgLbl.Text = "DBConncetion is null";
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            string date = datsTB.Text;
            fromAllTB.Text = date;
            string[] strs = date.Split('/');
            string mounth = (Convert.ToInt32(strs[1]) ).ToString().PadLeft(2, '0');
            toAllTB.Text = String.Format("{0}/{1}/{2}", strs[0], mounth, 31/* strs[2]*/);

            msgLbl.Text = "آماده سازی دسته جمعی صورت گرفت";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            GTS.Clock.Business.WebServices.TotalWebService total = new GTS.Clock.Business.WebServices.TotalWebService();
            //total.GTS_ExecuteByToDate(toAllTB.Text);
            msgLbl.Text = "پایان محاسبات دسته جمعی";
        }

        public string GetConnectionString(string _connectionStringsName)
        {
            System.Configuration.ConnectionStringSettingsCollection config = System.Configuration.ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < config.Count; i++)
            {
                if (config[i].Name.Equals(_connectionStringsName, StringComparison.OrdinalIgnoreCase))
                    return config[i].ToString();
            }
            return String.Empty;
        }
    }
}
