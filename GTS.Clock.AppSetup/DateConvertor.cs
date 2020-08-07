using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.Infrastructure.Utility;
using System.Globalization;
using System.Diagnostics;

namespace GTS.Clock.AppSetup
{
    public partial class DateConvertor : Form
    {
        public DateConvertor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {               
            PersianCalendar pc = new PersianCalendar();
            textBox5.Text = pc.ToDateTime(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), 1, 1, 1, 1).Date.ToString("yyyy/MM/dd");           
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //DateTime d1 = DateTime.Now;
            DateTime date=new DateTime(Convert.ToInt32(textBox8.Text), Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text));
            PersianDateTime pc = new PersianDateTime(date);
            textBox11.Text = pc.PersianDate;           
        }
    }
}
