using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Utility;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace GTS.Clock.AppService.Host
{
    public partial class Image : System.Web.UI.Page
    {
        private const string PersonId = "personId";
        private decimal personId = 0;
        BPerson bperson = new BPerson(SysLanguageResource.English, LocalLanguageResource.English);
        private const string DefaultImage = "unknownUser.jpg";
              
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[PersonId] != null && Utility.IsNumber(Request.QueryString[PersonId]))
            {
                try
                {
                    personId = (decimal)Utility.ToInteger(Request.QueryString[PersonId]);
                    //GTS.Clock.Model.PersonDetail person = bperson.GetByID(personId).PersonDetail;
                    string image = bperson.GetPersonImage(personId);
                    if (image != null)
                    {
                        LoadImage(image);
                    }
                    else
                    {
                        LoadDefaultImage();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (Request.QueryString[PersonId] != null && Request.QueryString[PersonId] == "insert")
            {
                SaveImage();
            }
        }

        private string ImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] Ret;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }

            return BitConverter.ToString(Ret);
        }

        private void SaveImage()
        {
            
            //GTS.Clock.Model.Person person = bperson.GetByID(11802);
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("52.jpg"));
            //person.PersonDetail.Image = ImageToByteArray(image, ImageFormat.Jpeg);
            //bperson.SaveChanges(person, UIActionType.EDIT);
            bperson.UpdatePersonImage(11802, ImageToByteArray(image, ImageFormat.Jpeg));
        }

        private void LoadImage(string image)
        {
            if (image != null)
            {
                byte[] image1 = Enumerable.Range(0, image.Length)
                                    .Where(x => x % 2 == 0)
                                    .Select(x => Convert.ToByte(image.Substring(x, 2), 16))
                                    .ToArray();


                Response.ContentType = "image/JPEG";
                using (MemoryStream ms = new MemoryStream(image1, 0, image1.Length))
                {
                     ms.WriteTo(Response.OutputStream);                   
                }
            }
        }

        private void LoadDefaultImage()
        {
            System.Drawing.Image im = System.Drawing.Image.FromFile(Server.MapPath(DefaultImage));
            im.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.End();
        }     

    }
}