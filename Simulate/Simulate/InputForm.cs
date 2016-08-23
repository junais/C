using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulate
{
    public partial class InputForm : Form
    {
        Form1 firstformRef;

        int aa = 0;
        public InputForm(ref Form1 form1handel)
        {
           
            firstformRef = form1handel;
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
                aa=  Convert.ToInt32(  firstformRef.TextBoxString );
           // MessageBox.Show(aa);

            var n = aa;
            for (int i = 0; i < n; i++)
            {
                //Create label
                Label label = new Label();
                label.BackColor = System.Drawing.Color.Transparent;
                label.Text = String.Format("Neuron {0}", i + 1);
                
               // label.
                //Position label on screen
                label.Left = 30;
                label.Top = (i + 1) * 40;
                //Create textbox
                TextBox textBox = new TextBox();
                //Position textbox on screen
                textBox.Left = 140;
                textBox.Top = (i + 1) * 40;


                TextBox textBox1 = new TextBox();
                //Position textbox on screen
                textBox1.Left = 340;
                textBox1.Top = (i + 1) * 40;



                TextBox textBox2 = new TextBox();
                //Position textbox on screen
                textBox2.Left = 440;
                textBox2.Top = (i + 1) * 40;


                TextBox textBox3 = new TextBox();
                //Position textbox on screen
                textBox3.Left = 540;
                textBox3.Top = (i + 1) * 40;




                //Add controls to form
                this.Controls.Add(label);
                this.Controls.Add(textBox);
                this.Controls.Add(textBox1);

                this.Controls.Add(textBox2);

                this.Controls.Add(textBox3);
            }




/*
            for (int i = 0; i < n; i++)
            {

                int j = n+4;


                Label label = new Label();
                label.Text = String.Format("Syn of nueron {0}", i + 1);
                // label.
                //Position label on screen
                label.Left = 30;
                label.Top = (i + j + 1) * 30;
              


                TextBox textBox1 = new TextBox();
                //Position textbox on screen
                textBox1.Left = 340;
                textBox1.Top = (i + j + 1) * 30;
                textBox1.Width = 50;



                TextBox textBox2 = new TextBox();
                //Position textbox on screen
                textBox2.Left = 390;
                textBox2.Top = (i + j + 1) * 30;

                textBox2.Width = 50;


                TextBox textBox3 = new TextBox();
                //Position textbox on screen
                textBox3.Left = 440;
                textBox3.Top = (i + j + 1) * 30;

                textBox3.Width = 50;




                //Add controls to form
                this.Controls.Add(label);
               // this.Controls.Add(textBox);
                this.Controls.Add(textBox1);

                this.Controls.Add(textBox2);

                this.Controls.Add(textBox3);
            }

*/

        }



        public int check_val()
        {
            int ii = 0;
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {


                if (tb.Text == "")
                {
                    ii = 1;
                }
            }

            return ii;


            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (check_val() != 1)
            {

                string outstring = "" + aa + ",";




                foreach (TextBox tb in this.Controls.OfType<TextBox>())
                {


                    outstring += tb.Text;
                    outstring += ",";




                }



                //  MessageBox.Show(outstring);





                StreamWriter writetext = new StreamWriter("write.txt");





                writetext.WriteLine(outstring);
                writetext.Close();



                this.Hide();

                ConnectionIpform cn = new ConnectionIpform();

                // Firststage f2 = new Firststage();

                cn.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string command = @"u1.pdf";

            var process = new System.Diagnostics.Process
            {
                StartInfo =
                    new System.Diagnostics.ProcessStartInfo(command)
            };

            process.Start();

        }
    }
}
