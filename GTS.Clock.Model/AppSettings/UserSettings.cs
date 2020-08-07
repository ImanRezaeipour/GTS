using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using GTS.Clock.Model.UI;

namespace GTS.Clock.Model.AppSetting
{
	#region Comments	
	/// <h3>Changes</h3>
	/// 	<listheader>
	/// 		<th>Author</th>
	/// 		<th>Date</th>
	/// 		<th>Details</th>
	/// 	</listheader>
	/// 	<item>
	/// 		<term>Farhad Salavati</term>
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class UserSettings : IEntity
    {
        private IList<EmailSettings> emailSettingList = new List<EmailSettings>();
        private IList<SMSSettings> smsSettingList = new List<SMSSettings>();
        private IList<DashboardSettings> dashboardSettingList = new List<DashboardSettings>();
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UserID value.
        /// </summary>
        public virtual Security.User User { get; set; }

        /// <summary>
        /// Gets or sets the LangID value.
        /// </summary>
        public virtual Languages Language { get; set; }

        public virtual UISkin Skin { get; set; }

        public virtual IList<MonthlyOperationGridClientSettings> MonthlyOperationGridSettingList
        {
            get;
            set;
        }

        public virtual MonthlyOperationGridClientSettings MonthlyOperationGridSetting 
        {
            get
            {
                if (this.MonthlyOperationGridSettingList != null && this.MonthlyOperationGridSettingList.Count > 0)
                {
                    return this.MonthlyOperationGridSettingList[0];
                }
                return new MonthlyOperationGridClientSettings();
            }
            set
            {
                if (this.MonthlyOperationGridSettingList == null)
                {
                    this.MonthlyOperationGridSettingList = new List<MonthlyOperationGridClientSettings>();
                }
                if (this.MonthlyOperationGridSettingList.Count == 0)
                {
                    this.MonthlyOperationGridSettingList.Add(value);
                }
                else
                {
                    this.MonthlyOperationGridSettingList[0] = value;
                }
            }
        }

        public virtual EmailSettings EmailSettings
        {
            get 
            {
                if (this.emailSettingList != null && this.emailSettingList.Count > 0) 
                {
                    return this.emailSettingList.First();
                }
                return null;
            }
            set 
            {
                if (this.emailSettingList == null )
                {
                    this.emailSettingList = new List<EmailSettings>();
                }
                if (this.emailSettingList.Count > 0)
                {
                    this.emailSettingList[0] = value;
                }
                else 
                {
                    this.emailSettingList.Add(value);
                }
            } 
        }

        public virtual SMSSettings SMSSettings 
        {
            get
            {
                if (this.smsSettingList != null && this.smsSettingList.Count > 0)
                {
                    return this.smsSettingList.First();
                }
                return null;
            }
            set
            {
                if (this.smsSettingList == null)
                {
                    this.smsSettingList = new List<SMSSettings>();
                }
                if (this.smsSettingList.Count > 0)
                {
                    this.smsSettingList[0] = value;
                }
                else
                {
                    this.smsSettingList.Add(value);
                }
            } 
        }
        public virtual DashboardSettings DashboardSettings
        {
            get
            {
                if (this.dashboardSettingList != null && this.dashboardSettingList.Count > 0)
                {
                    return this.dashboardSettingList.First();
                }
                return null;
            }
            set
            {
                if (this.dashboardSettingList == null)
                {
                    this.dashboardSettingList = new List<DashboardSettings>();
                }
                if (this.dashboardSettingList.Count > 0)
                {
                    this.dashboardSettingList[0] = value;
                }
                else
                {
                    this.dashboardSettingList.Add(value);
                }
            }
        }
        public virtual int SubSystemID
        {
            get;
            set;
        }
        #endregion
    }
}