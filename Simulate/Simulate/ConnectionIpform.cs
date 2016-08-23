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
    public partial class ConnectionIpform : Form
    {
        public ConnectionIpform()
        {
            InitializeComponent();

            label1.Text = "Please eneter number of connections:";
           // InitializeComponent();

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbx_input.Text != "")
            {


                // StreamReader readtext = new StreamReader("write.txt");
                //  string readmetext = readtext.ReadLine();

                // string[] words = readmetext.Split(',');
                int SetLimit = Convert.ToInt32(tbx_input.Text);
                // readtext.Close();

                int aa = 0;
                aa = SetLimit;
                var n = aa;
                for (int i = 0; i < n; i++)
                {

                    //Create textbox
                    TextBox textBox = new TextBox();
                    //Position textbox on screen
                    textBox.Left = 140;
                    textBox.Top = (i + 1) * 52;



                    TextBox textBox1 = new TextBox();
                    //Position textbox on screen
                    textBox1.Left = 290;
                    textBox1.Top = (i + 1) * 52;



                    //Add controls to form
                    //   this.Controls.Add(label);
                    this.Controls.Add(textBox);
                    this.Controls.Add(textBox1);
                }
            }

              
               
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

       // int aa=0;
        private void button1_Click(object sender, EventArgs e)
        {




            if (check_val() != 1)
            {


                string outstring = "";

                foreach (TextBox tb in this.Controls.OfType<TextBox>())
                {

                    outstring += tb.Text;
                    outstring += ",";

                }

                // MessageBox.Show(outstring);
                StreamWriter writetext = new StreamWriter("writeconnection.txt");

                writetext.WriteLine(outstring);
                writetext.Close();











                this.Hide();

                Firststage fn = new Firststage();


                fn.Show();
            }



        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
