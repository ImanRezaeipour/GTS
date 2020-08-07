using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GTS.Clock.AppSetup
{
    [Serializable]
    public static class GTSAppSettings
    {
        [Serializable]
        public struct Settings
        {
            public byte[] LogUserName
            {
                get;
                set;
            }
            public byte[] LogPassword
            {
                get;
                set;
            }
            public byte[] GTSUserName
            {
                get;
                set;
            }
            public byte[] GTSPassword
            {
                get;
                set;
            }
            public byte[] FromDate
            {
                get;
                set;
            }
            public byte[] ToDate
            {
                get;
                set;
            }
            public byte[] Barcode
            {
                get;
                set;
            }
            public byte[] FilterDate
            {
                get;
                set;
            }
            public byte[] PTableName
            {
                get;
                set;
            }
            public byte[] ClockCalculation
            {
                get;
                set;
            }
 

            public byte[] SQLConnection
            {
                get;
                set;
            }
            public byte[] WebServiceAddress
            {
                get;
                set;
            }
            public byte[] LogSQLConnection
            {
                get;
                set;
            }
        }
        private static SqlConnection sqlConnectionInfo = null;
        private static SqlConnection sqlLogConnectionInfo = null;
        private static string ConnectionName = "GTS.Clock.AppSetup.Properties.Settings.FalatConnectionString";
        private static string LogConnectionName = "GTS.Clock.AppSetup.Properties.Settings.LogConnection";
        private static string ServiceAddress = null;
        private static string barcode = "";
        private static string fromDate = "";
        private static string toDate = "";
        private static string filterDate = "";
        private static string gtsPass = "";
        private static string logPass = "";
        private static string gtsun = "";
        private static string logun = "";
        private static string ptableName = "";
        private static string settingName = "";
        private static string settingClockCalculation = "";


        public static string LogUserName
        {
            get { return logun; }
            set { logun = value; }
        }
        public static string LogPassword
        {
            get { return logPass; }
            set { logPass = value; }
        }
        public static string GTSUserName
        {
            get { return gtsun; }
            set { gtsun = value; }
        }
        public static string GTSDBName { get; set; }
        public static string GTSPassword
        {
            get { return gtsPass; }
            set { gtsPass = value; }
        }
        public static string FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        public static string ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }
        public static string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }
        public static string FilterDate
        {
            get { return filterDate; }
            set { filterDate = value; }
        }
        public static string PTableName
        {
            get { return ptableName; }
            set { ptableName = value; }
        }
        public static string SettingName
        {
            get { return settingName; }
            set { settingName = value; }
        }
        public static bool ClockCalculation
        {
            get { return settingClockCalculation == "" ? false : true; }
            set { settingClockCalculation = value == true ? "Clock" : ""; }
        }



        public static SqlConnection SQLConnection
        {
            get
            {
                if (sqlConnectionInfo == null)
                {
                    string connection = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
                    sqlConnectionInfo = new SqlConnection(connection);
                }
                return sqlConnectionInfo;
            }
            set
            {
                sqlConnectionInfo = value;
            }
        }

        public static string WebServiceAddress
        {
            get
            {
                if (ServiceAddress == null)
                {
                    ServiceAddress = System.Configuration.ConfigurationSettings.AppSettings["ServiceAddress"];
                }
                return ServiceAddress;
            }
            set
            {
                ServiceAddress = value;
            }
        }

        public static SqlConnection LogSQLConnection
        {
            get
            {
                if (sqlLogConnectionInfo == null)
                {
                    string connection = ConfigurationManager.ConnectionStrings[LogConnectionName].ConnectionString;
                    sqlLogConnectionInfo = new SqlConnection(connection);
                }
                return sqlLogConnectionInfo;
            }
            set
            {
                sqlLogConnectionInfo = value;
            }
        }

        public static Settings Export()
        {           
            
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Settings seting = new Settings();
            seting.Barcode = encoding.GetBytes(GTSAppSettings.Barcode);
            seting.FromDate = encoding.GetBytes(GTSAppSettings.FromDate);
            seting.ToDate = encoding.GetBytes(GTSAppSettings.ToDate);
            seting.WebServiceAddress = encoding.GetBytes(GTSAppSettings.WebServiceAddress);
            seting.FilterDate = encoding.GetBytes(GTSAppSettings.FilterDate);
            seting.PTableName = encoding.GetBytes(GTSAppSettings.PTableName);
            seting.GTSPassword = encoding.GetBytes(GTSAppSettings.GTSPassword);
            seting.GTSUserName = encoding.GetBytes(GTSAppSettings.GTSUserName);
            seting.LogPassword = encoding.GetBytes(GTSAppSettings.LogPassword);
            seting.LogUserName = encoding.GetBytes(GTSAppSettings.LogUserName);
            seting.LogSQLConnection = encoding.GetBytes(GTSAppSettings.LogSQLConnection.ConnectionString);
            seting.SQLConnection =  encoding.GetBytes(GTSAppSettings.SQLConnection.ConnectionString);
            seting.ClockCalculation = encoding.GetBytes(GTSAppSettings.ClockCalculation ? "clock" : "");
            return seting;
        }

        public static void Import(Settings seting)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            GTSAppSettings.Barcode = encoding.GetString(seting.Barcode);
            GTSAppSettings.FromDate = encoding.GetString(seting.FromDate);
            GTSAppSettings.ToDate = encoding.GetString(seting.ToDate);
            GTSAppSettings.WebServiceAddress = encoding.GetString(seting.WebServiceAddress);
            GTSAppSettings.FilterDate = encoding.GetString(seting.FilterDate);
            GTSAppSettings.PTableName = encoding.GetString(seting.PTableName);
            GTSAppSettings.GTSPassword = encoding.GetString(seting.GTSPassword);
            GTSAppSettings.GTSUserName = encoding.GetString(seting.GTSUserName);
            GTSAppSettings.LogPassword = encoding.GetString(seting.LogPassword);
            GTSAppSettings.LogUserName = encoding.GetString(seting.LogUserName);
            GTSAppSettings.LogSQLConnection = new SqlConnection(encoding.GetString(seting.LogSQLConnection));
            GTSAppSettings.SQLConnection = new SqlConnection(encoding.GetString(seting.SQLConnection));
            GTSAppSettings.ClockCalculation = encoding.GetString(seting.ClockCalculation) == "" ? false : true;
        }

        public static void SaveToFile()
        {
            SaveToFile(SettingName);          
        }

        public static void SaveToFile(string filename)
        {
            if (filename.Length == 0)
            {
                filename = "appcon.gus";
            }
            FileStream stram;
            if (!filename.Contains(".gus")) 
            {
                filename += ".gus";
            }
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (stram = new FileStream(filename, FileMode.Create))
            {
                BinaryFormatter formater = new BinaryFormatter();
                formater.Serialize(stram, GTSAppSettings.Export());
            }
        }

        public static void LoadFromFile()
        {
            LoadFromFile(SettingName);
           /* if (File.Exists(configName))
            {
                FileStream stram;
                using (stram = new FileStream(configName, FileMode.Open))
                {
                    if (stram.Length > 0)
                    {
                        BinaryFormatter formater = new BinaryFormatter();
                        GTSAppSettings.Settings setting = new GTSAppSettings.Settings();
                        setting = (GTSAppSettings.Settings)formater.Deserialize(stram);
                        GTSAppSettings.Import(setting);
                    }
                }
            }*/
        }

        public static void LoadFromFile(string filename)
        {
            if (filename.Length == 0) 
            {
                filename = "appcon.gus";
            }
            if (!filename.Contains(".gus"))
            {
                filename += ".gus";
            }
            if (File.Exists(filename))
            {
                FileStream stram;
                using (stram = new FileStream(filename, FileMode.Open))
                {
                    if (stram.Length > 0)
                    {
                        BinaryFormatter formater = new BinaryFormatter();
                        GTSAppSettings.Settings setting = new GTSAppSettings.Settings();
                        setting = (GTSAppSettings.Settings)formater.Deserialize(stram);
                        GTSAppSettings.Import(setting);
                    }
                }
            }
        }

        public static string[] GetExistSettings()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.gus");         
            IList<string> list = new List<string>();
            foreach (string file in files) 
            {
                FileInfo f = new FileInfo(file);
                list.Add(f.Name.Replace(".gus", ""));
            }
            return list.ToArray();
        }
    }
}