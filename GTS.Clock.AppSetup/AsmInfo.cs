using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace GTS.Clock.AppSetup
{
    public partial class AsmInfo : Form
    {
        public AsmInfo()
        {
            InitializeComponent();
        }

        private void AsmInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("GTS.Clock.Business.Calculator.dll"))
                {
                    string path = Path.GetFullPath("GTS.Clock.Business.Calculator.dll");
                    Assembly asm = Assembly.LoadFile(path);
                    Type t = asm.GetType("GTS.Clock.Business.Calculator.ObjectCalculator");
                    object obj = Activator.CreateInstance(t,new object[] { null });
                    Type myType = obj.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    engineDllNameLbl.Text = "Property not Found";
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.Name.Equals("EngineCompanyName")) 
                        {
                            engineDllNameLbl.Text = prop.GetValue(obj, null).ToString();
                        }
                    }
                }
                else
                {
                    engineDllNameLbl.Text = "Not Found!";
                }
            }
            catch (Exception ex) 
            {
                engineDllNameLbl.Text = ex.Message;
            }
        }
    }
}
