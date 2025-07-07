using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilityTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form test = new Form();
            test.AllowTransparency = true;
            test.FormBorderStyle = FormBorderStyle.None;
            test.BackColor = Color.FromArgb(255, 250, 250, 250);
            test.TransparencyKey = Color.FromArgb(250, 250, 250, 250);

            UCTest uCTest = new UCTest();
            uCTest.Dock = DockStyle.Fill;
            
            test.Controls.Add(uCTest);

            test.Show();
        }
    }
}
