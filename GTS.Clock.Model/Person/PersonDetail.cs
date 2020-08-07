using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Utility;
using System.Drawing;
using System.IO;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.PersonInfo;

namespace GTS.Clock.Model
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
	/// 		<description>5/24/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class PersonDetail : IEntity
	{

        public PersonDetail() 
        {
            this.BirthDate = Utility.GTSMinStandardDateTime;
        }

      
		#region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the MeliCode value.
        /// </summary>
        public virtual String MeliCode { get; set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public virtual String ShomareShenasname { get; set; }

        /// <summary>
        /// Gets or sets the BirthCertificate value.
        /// </summary>
        public virtual String BirthCertificate { get; set; }

        /// <summary>
        /// Gets or sets the Status value.
        /// </summary>
        public virtual String Grade { get; set; }

        /// <summary>
        /// Gets or sets the FatherName value.
        /// </summary>
        public virtual String FatherName { get; set; }

        /// <summary>
        /// Gets or sets the MilitaryStatus value.
        /// </summary>
        public virtual MilitaryStatus MilitaryStatus { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual String MilitaryStatusTitle
        {
            get
            {
                return this.MilitaryStatus.ToString("G");
            }
        }

        /// <summary>
        /// Gets or sets the PlaceIssued value.
        /// </summary>
        public virtual String PlaceIssued { get; set; }

        /// <summary>
        /// Gets or sets the Tel value.
        /// </summary>
        public virtual String Tel { get; set; }

        /// <summary>
        /// Gets or sets the MobileNumber value.
        /// </summary>
        public virtual String MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the Address value.
        /// </summary>
        public virtual String Address { get; set; }

        /// <summary>
        /// Gets or sets the Email value.
        /// </summary>
        public virtual String EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the BirthPlace value.
        /// </summary>
        public virtual String BirthPlace { get; set; }

        /// <summary>
        /// Gets or sets the BirthDate value.
        /// </summary>
        public virtual DateTime BirthDate { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual String UIBirthDate { get; set; }

        /// <summary>
        /// Gets or sets the Image value.
        /// </summary>
        public virtual string Image
        {
            get;
            set;
        }        
    
        /*
        #region Reserved Fields Lable
       
        /// <summary>
        /// Gets or sets the R1 value.
        /// </summary>
        public virtual String R1Lable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the R2 value.
        /// </summary>
        public virtual String R2Lable { get; set; }

        /// <summary>
        /// Gets or sets the R3 value.
        /// </summary>
        public virtual String R3Lable { get; set; }

        /// <summary>
        /// Gets or sets the R4 value.
        /// </summary>
        public virtual String R4Lable { get; set; }

        /// <summary>
        /// Gets or sets the R5 value.
        /// </summary>
        public virtual String R5Lable { get; set; }

        /// <summary>
        /// Gets or sets the R6 value.
        /// </summary>
        public virtual String R6Lable { get; set; }

        /// <summary>
        /// Gets or sets the R7 value.
        /// </summary>
        public virtual String R7Lable { get; set; }

        /// <summary>
        /// Gets or sets the R8 value.
        /// </summary>
        public virtual String R8Lable { get; set; }

        /// <summary>
        /// Gets or sets the R9 value.
        /// </summary>
        public virtual String R9Lable { get; set; }

        /// <summary>
        /// Gets or sets the R10 value.
        /// </summary>
        public virtual String R10Lable { get; set; }

        /// <summary>
        /// Gets or sets the R11 value.
        /// </summary>
        public virtual String R11Lable { get; set; }

        /// <summary>
        /// Gets or sets the R12 value.
        /// </summary>
        public virtual String R12Lable { get; set; }

        /// <summary>
        /// Gets or sets the R13 value.
        /// </summary>
        public virtual String R13Lable { get; set; }

        /// <summary>
        /// Gets or sets the R14 value.
        /// </summary>
        public virtual String R14Lable { get; set; }

        /// <summary>
        /// Gets or sets the R15 value.
        /// </summary>
        public virtual String R15Lable { get; set; }

        /// <summary>
        /// Gets or sets the R16 value.
        /// </summary>
        public virtual String R16Lable { get; set; }

        /// <summary>
        /// Gets or sets the R17 value.
        /// </summary>
        public virtual String R17Lable { get; set; }

        /// <summary>
        /// Gets or sets the R18 value.
        /// </summary>
        public virtual String R18Lable { get; set; }

        /// <summary>
        /// Gets or sets the R19 value.
        /// </summary>
        public virtual String R19Lable { get; set; }

        /// <summary>
        /// Gets or sets the R20 value.
        /// </summary>
        public virtual String R20Lable { get; set; }
        #endregion
     */
        /*
        #region English Reserved Fields Title
        /// <summary>
        /// Gets or sets the R1 value.
        /// </summary>
        public virtual String En_R1 { get; set; }

        /// <summary>
        /// Gets or sets the R2 value.
        /// </summary>
        public virtual String En_R2 { get; set; }

        /// <summary>
        /// Gets or sets the R3 value.
        /// </summary>
        public virtual String En_R3 { get; set; }

        /// <summary>
        /// Gets or sets the R4 value.
        /// </summary>
        public virtual String En_R4 { get; set; }

        /// <summary>
        /// Gets or sets the R5 value.
        /// </summary>
        public virtual String En_R5 { get; set; }

        /// <summary>
        /// Gets or sets the R6 value.
        /// </summary>
        public virtual String En_R6 { get; set; }

        /// <summary>
        /// Gets or sets the R7 value.
        /// </summary>
        public virtual String En_R7 { get; set; }

        /// <summary>
        /// Gets or sets the R8 value.
        /// </summary>
        public virtual String En_R8 { get; set; }

        /// <summary>
        /// Gets or sets the R9 value.
        /// </summary>
        public virtual String En_R9 { get; set; }

        /// <summary>
        /// Gets or sets the R10 value.
        /// </summary>
        public virtual String En_R10 { get; set; }

        /// <summary>
        /// Gets or sets the R11 value.
        /// </summary>
        public virtual String En_R11 { get; set; }

        /// <summary>
        /// Gets or sets the R12 value.
        /// </summary>
        public virtual String En_R12 { get; set; }

        /// <summary>
        /// Gets or sets the R13 value.
        /// </summary>
        public virtual String En_R13 { get; set; }

        /// <summary>
        /// Gets or sets the R14 value.
        /// </summary>
        public virtual String En_R14 { get; set; }

        /// <summary>
        /// Gets or sets the R15 value.
        /// </summary>
        public virtual String En_R15 { get; set; }

        /// <summary>
        /// Gets or sets the R16 value.
        /// </summary>
        public virtual String En_R16 { get; set; }

        /// <summary>
        /// Gets or sets the R17 value.
        /// </summary>
        public virtual String En_R17 { get; set; }

        /// <summary>
        /// Gets or sets the R18 value.
        /// </summary>
        public virtual String En_R18 { get; set; }

        /// <summary>
        /// Gets or sets the R19 value.
        /// </summary>
        public virtual String En_R19 { get; set; }

        /// <summary>
        /// Gets or sets the R20 value.
        /// </summary>
        public virtual String En_R20 { get; set; }
        #endregion
*/
	
        #endregion
	}
}