using System;
using System.Web.UI;
using System.Reflection;

namespace GTS.Clock.RuleDesigner.UI.Web.Classes
{
    /// <summary>
    /// Summary description for ScriptHelper
    /// </summary>
    public class ScriptHelper
    {
        private const string ScriptsBasePath = "JS/API/";

        public static void InitializeScripts(Page page, Type Scripts)
        {
            foreach (MemberInfo memberInfo in Scripts.GetMembers())
            {
                if (memberInfo.DeclaringType == Scripts && memberInfo.Name != "value__")
                {
                    string Key = memberInfo.Name + ".js";
                    string Script = "<script type=\"text/javascript\" src=\"" + ScriptsBasePath + Key + "?" + DateTime.Now.Ticks.ToString() + "\"></script>";
                    if (!page.ClientScript.IsClientScriptBlockRegistered(Key))
                        page.ClientScript.RegisterClientScriptBlock(page.GetType(), Key, Script);
                }
            }
        }
    }
}