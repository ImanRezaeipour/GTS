using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

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

    public class Languages : IEntity
    {
        //public enum LanguagesName1
        //{
        //    Unknown = 0, Parsi = 1, English = 2
        //}

        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the LangID value.
        /// </summary>
        public virtual String LangID { get; set; }

        /// <summary>
        /// Gets or sets the IsActive value.
        /// </summary>
        public virtual Boolean IsActive { get; set; }

        public virtual LanguagesName Name
        {
            get
            {
                if (LangID != null)
                {
                    if (LangID.Equals("fa-IR"))
                    {
                        return LanguagesName.Parsi;
                    }
                    else if (LangID.Equals("en-US"))
                    {
                        return LanguagesName.English;
                    }
                }
                return LanguagesName.Unknown;
            }
        }

        public virtual IList<UserSettings> UserSettingList
        {
            get;
            set;
        }
        #endregion
    }
}