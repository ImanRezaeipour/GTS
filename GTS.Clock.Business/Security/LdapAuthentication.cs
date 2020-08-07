using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Data;
using System.Data.SqlClient;
using System.Security.Authentication ;
using System.Linq;


namespace GTS.Clock.Business.Security
{
    public class LdapAuthentication
    {
        #region variables
        private string _path;
        private string _filterAttribute;
        private string _username;
        private string _pass;
        private string _domain;
        string _domainAndUsername;
        #endregion

        #region Constructor

        public LdapAuthentication()
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">"LDAP://company.etc/DC=company,DC=etc"</param>
        public LdapAuthentication(string path)
        {
            _path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain">company.etc</param>
        /// <param name="username">jack</param>
        /// <param name="pass">123</param>
        public LdapAuthentication(string domain, string username, string pass)
        {
            _domain = domain;
            _username = username.ToLower(); 
            _pass = pass;
            _domainAndUsername = _domain + @"\" + _username;
            //   "LDAP://ghadir.local/DC=ghadir,DC=local";
            _path = String.Format("LDAP://{0}/DC={1},DC={2}", domain, domain.Split('.')[0], domain.Split('.')[1]);
        }
        #endregion

        /// <summary>
        /// آیا کاربری با این مشخصات در دامین موجود است
        /// </summary>
        /// <param name="domain">نام دامنه</param>
        /// <param name="username">نام کاربری</param>
        /// <param name="pwd">کلمه عبور</param>
        /// <returns></returns>
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                // Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (result == null)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }

        /// <summary>
        ///  با کلیدجستجوی اطلاعات موجود در اکتیو دایرکتوری
        /// </summary>
        /// <param name="key">کلید</param>
        /// <returns></returns>
        public string GetInfo(string key)
        {
            if (_username.Length > 0)
            {
                DirectoryEntry entry = new DirectoryEntry(_path, _domainAndUsername, _pass);

                try
                {
                    // Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(SAMAccountName=" + _username + ")";
                    search.PropertiesToLoad.Add(key);
                    SearchResult result = search.FindOne();
                    if (null == result)
                    {
                        return "";
                    }
                    // Update the new path to the user in the directory
                    _path = result.Path;
                    _filterAttribute = result.Properties[key][0].ToString();
                    return _filterAttribute;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error authenticating user. " + ex.Message);
                }
            }
            return "";
        }

        /// <summary>
        /// همه اطلاعات اکتیو دایرکتوری را برای کاریر جاری بر میگرداند 
        /// </summary>
        /// <returns></returns>
        public string GetAllInfo()
        {
            if (_username.Length > 0)
            {
                DirectoryEntry myLdapConnection = new DirectoryEntry(_path, _domainAndUsername, _pass);
                DirectorySearcher search = new DirectorySearcher(myLdapConnection);

                search.CacheResults = true;
                //search.PropertiesToLoad.Add("cn");

                SearchResultCollection allResults = search.FindAll();
                DataTable resultsTable = new DataTable("Results");

                string result = "";
                //add columns for each property in results
                foreach (SearchResult searchResult in allResults)
                {
                    foreach (string propName in searchResult.Properties.PropertyNames)
                    {
                        ResultPropertyValueCollection valueCollection =
                        searchResult.Properties[propName];
                        foreach (Object propertyValue in valueCollection)
                        {
                            result += String.Format("<b>{0} : </b> {1} <br>", propName, propertyValue.ToString());
                        }
                    }
                }
                return result;
            }
            return "";
        }

        /// <summary>
        /// لیست تمام کاربران دامین را بر میگرداند
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAllDomainUsers()
        {
            IList<string> allUsers = new List<string>();

            DirectoryEntry searchRoot = new DirectoryEntry(_path);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PageSize = 10000;
            search.SizeLimit = 10000;
            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname"))
                    {
                        allUsers.Add((String)result.Properties["samaccountname"][0]);
                    }
                }
            }
            return allUsers;

        }

        /// <summary>
        /// بررسی وجود یک شناسه در لیست کاربران
        /// </summary>
        /// <param name="username"> نام دامنه </param>
        /// <param name="dc">پسوند دامنه</param>
        /// <param name="username">شناسه کاربری</param>
        /// <returns></returns>
        public bool LookupUser(string domain,string username,string dc)
        {
            _domain = domain;
            _username = username.ToLower();      
            _domainAndUsername = _domain + @"\" + _username;
            string[] dcs = dc.Split(',');
            string compleName = "";
            for (int i = 0; i < dcs.Length;i++ )
            {
                dcs[i] = dcs[i].Replace("DC=", "");
                compleName += "." + dcs[i];
            }
            if (compleName.StartsWith("."))
            {
                compleName = compleName.Remove(0, 1);
            }

            //   "LDAP://ghadir.local/DC=ghadir,DC=local";
            _path = String.Format("LDAP://{0}/{1}", compleName, dc);
            if (GetAllDomainUsers().Where(x => x.ToLower().Equals(_username)).Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
