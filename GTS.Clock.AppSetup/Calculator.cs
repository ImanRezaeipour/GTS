using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;
using System.Globalization;
using System.Diagnostics;

namespace GTS.Clock.AppSetup
{
    public partial class Calculator : GTS.Clock.AppSetup.BaseForm
    {
        PersianCalendar pc = new PersianCalendar();
        public Calculator()
        {
            base.menuStrip1.Visible = false;
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            base.menuStrip1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                miladiTB1.Text = PersianDateTime.ShamsiToMiladi(sunDateTB1.Text);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }           
        }           

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sunDateTB2.Text = PersianDateTime.MiladiToShamsi(miladiTB2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                timeTB1.Text = Utility.IntTimeToRealTime(Convert.ToInt32(minutesTB1.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parts = timeTB2.Text.Split(new char[] { ':' });
                if (parts.Length == 2)
                {
                    int h = Convert.ToInt32(parts[0].PadLeft(2, '0'));
                    int m = Convert.ToInt32(parts[1].PadLeft(2, '0'));
                    minutesTB2.Text = ((int)(h * 60 + m)).ToString();
                }
                else minutesTB2.Text = "0";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int min = Utility.ToInteger(minTBDay.Text);
            int day = min / (8 * 60);
            int hour = (min - (day * 8 * 60)) / 60;
            int minutes = min - (day * 8 * 60) - (hour * 60);
            dayResultLbl.Text = String.Format(" {0} روز و {1} ساعت و {2} دقیقه", day, hour, minutes);
        }      
    }
}
