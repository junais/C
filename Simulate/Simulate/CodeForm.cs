using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Simulate
{
    public partial class CodeForm : Form
    {
        Form1 codeformRef;
        public CodeForm(ref Form1 form1handel1)
        {
            codeformRef = form1handel1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writecodetext = new StreamWriter("writecode.txt");
            writecodetext.WriteLine(textBox1.Text);
            writecodetext.Close();

            this.Hide();
            //Firststage an = new Firststage();
            Alterstage an = new Alterstage();
            an.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close!", "Message", MessageBoxButtons.YesNo);
            // MessageBox.Show( MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {

                // break;
                //this.Close();
                //do something else
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
