using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.AppSetting;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    /// <summary>
    /// Summary description for SkinHelper
    /// </summary>
    public class SkinHelper
    {
        public static BUserSettings UserSettingsBusiness
        {
            get
            {
                return new BUserSettings();            
            }
        }

        public static ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public static BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public SkinHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InitializeSkin(Page page)
        {
            ControlCollection HeaderControlCollection = page.Header.Controls;
            string SkinID = GetCurrentSkinID(page);
            foreach (Control Ctrl in HeaderControlCollection)
            {
                if (Ctrl is HtmlLink)
                {
                    HtmlLink htmlLink = (HtmlLink)Ctrl;
                    if (!htmlLink.Href.Contains("Skins/" + SkinID + "/"))
                        htmlLink.Href = "Skins/" + SkinID + "/" + htmlLink.Href;
                }
            }
        }

        public static string GetRelativeHeaderFlash(Page page)
        {
            string SkinID = GetCurrentSkinID(page);
            string HeaderFlashPath = "Skins/" + SkinID + "/swf/my.swf";
            return HeaderFlashPath;
        }

        public static void SetRelativeTabStripImageBaseUrl(Page page, TabStrip tabStrip)
        {
            string SkinID = GetCurrentSkinID(page);
            tabStrip.ImagesBaseUrl = "Skins/" + SkinID + "/" + tabStrip.ImagesBaseUrl;
        }

        public static IList<UISkin> GetAllSkins()
        {
            return UserSettingsBusiness.GetAll();
        }

        public static void SetCurrentSkin(decimal SkinID)
        {
            UserSettingsBusiness.SetCurrentSkin(SkinID);
        }

        private static string GetCurrentSkinID(Page page)
        {
            string[] retMessage = new string[4];
            string CurrentSkin = string.Empty;
            try
            {
                CurrentSkin = BUserSettings.CurrentSkin;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = exceptionHandler.HandleException(page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
            }
            catch (UIBaseException ex)
            {
                retMessage = exceptionHandler.HandleException(page, ExceptionTypes.UIBaseException, ex, retMessage);
                HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
            }
            catch (Exception ex)
            {
                retMessage = exceptionHandler.HandleException(page, ExceptionTypes.Exception, ex, retMessage);
                HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
            }
            return CurrentSkin;
        }

    }
}