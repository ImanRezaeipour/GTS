using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace GTS.Clock.Presentaion.Forms.App_Code
{
    public class DateObj
    {
        public int Month { get; set; }
        public int Day { get; set; }
    }
     
    public enum SupportedLangs
    {
        faIR,
        enUS  
    }

    public class LanguageProvider
    {
        private string currentUser;
        public string CurrentUser
        {
            get
            {
                this.currentUser = "2244";
                return this.currentUser;
            }
        }

        public LanguageProvider()
        {
        }

        public void SetCurrentLanguage(string LangID)
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            conStr += AppDomain.CurrentDomain.BaseDirectory + "\\common\\db\\demo.mdb";
            System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
            srcDB.Open();
            string lngSql = "Select * From Languages Where LangID = '" + LangID + "'";
            DataTable lngdt = new DataTable();
            System.Data.OleDb.OleDbDataAdapter lngda = new System.Data.OleDb.OleDbDataAdapter(lngSql, srcDB);
            lngda.Fill(lngdt);
            if (lngdt.Rows.Count > 0)
            {
                string sql = "Update Settings AS S inner join Languages AS L on L.ID = S.LangID Set S.LangID = " + lngdt.Rows[0]["ID"] + "  Where S.UserID = '" + this.CurrentUser + "'";
                DataTable dt = new DataTable();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
                da.Fill(dt);
            }
            srcDB.Close();
            HttpContext.Current.Session.Add("LangID", LangID);
        }

        public string GetCurrentLanguage()
        {
            if (HttpContext.Current.Session["LangID"] == null)
            {
                string LangID = string.Empty;
                string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
                conStr += AppDomain.CurrentDomain.BaseDirectory + "\\common\\db\\demo.mdb";
                System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
                srcDB.Open();
                string sql = "Select L.LangID From Settings AS S Inner Join Languages As L on S.LangID = L.ID Where UserID = '" + this.CurrentUser + "'";
                DataTable dt = new DataTable();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
                da.Fill(dt);
                srcDB.Close();
                LangID = dt.Rows[0]["LangID"].ToString();
                HttpContext.Current.Session.Add("LangID", LangID);
            }
            return HttpContext.Current.Session["LangID"].ToString();
        }

        public string GetCurrentSysLanguage()
        {
            string SysLangID = string.Empty;
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            conStr += AppDomain.CurrentDomain.BaseDirectory + "\\common\\db\\demo.mdb";
            System.Data.OleDb.OleDbConnection srcDB = new System.Data.OleDb.OleDbConnection(conStr);
            srcDB.Open();
            string sql = "Select L.LangID From GeneralSettings AS Gs Inner Join Languages As L on Gs.LangID = L.ID Where Gs.IsActive = true";
            DataTable dt = new DataTable();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, srcDB);
            da.Fill(dt);
            srcDB.Close();
            SysLangID = dt.Rows[0]["LangID"].ToString();
            return SysLangID;
        }

        public string GetSysDateString(DateTime dateTime)
        {
            string SysDateString = string.Empty;
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    SysDateString = pCal.GetYear(dateTime).ToString() + "/" + pCal.GetMonth(dateTime).ToString() + "/" + pCal.GetDayOfMonth(dateTime);
                    break;
                case "en-US":
                    SysDateString = dateTime.ToShortDateString();
                    break;
            }
            return SysDateString;
        }

        public DateTime GetSysDateTime(string strDateTime)
        {
            DateTime SysDateTime = DateTime.Now;
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    string[] strDateTimeParts = strDateTime.Split(new char[]{'/'});
                    PersianCalendar pCal = new PersianCalendar();
                    SysDateTime = pCal.ToDateTime(int.Parse(strDateTimeParts[0]), int.Parse(strDateTimeParts[1]), int.Parse(strDateTimeParts[2]), 0, 0, 0, 0);
                    break;
                case "en-US":
                    SysDateTime = DateTime.Parse(strDateTime);
                    break;
            }
            return SysDateTime;
        }

        public DateObj GetDBDateTime(string strDBDateTime)
        {
            DateObj dateObj = new DateObj();
            DateTime dbDateTime = DateTime.Parse(strDBDateTime);
            switch (this.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    PersianCalendar pCal = new PersianCalendar();
                    dateObj.Month = pCal.GetMonth(dbDateTime);
                    dateObj.Day = pCal.GetDayOfMonth(dbDateTime);
                    break;
                case "en-US":
                    dateObj.Month = dbDateTime.Month;
                    dateObj.Day = dbDateTime.Day;
                    break;
            }
            return dateObj;
        }

    }




}
