using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulate
{
    public partial class Form1 : Form
    {

        public string TextBoxString;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {

            if (tbx_nuecount.Text != "")
            {
                Form1 newform = new Form1();
                newform = this;

                TextBoxString = tbx_nuecount.Text;
                this.Hide();
                InputForm f2 = new InputForm(ref newform);

                f2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string command = @"u1.pdf";

            var process = new System.Diagnostics.Process
            {
                StartInfo =
                    new System.Diagnostics.ProcessStartInfo(command)
            };

            process.Start();

 
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 newcodeform = new Form1();
            this.Hide();
            CodeForm f3 = new CodeForm(ref newcodeform);

            f3.Show();

        }
    }
}
