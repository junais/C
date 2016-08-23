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
using System.Threading;
using System.Diagnostics;

namespace Simulate
{
    public partial class Firststage : Form
    {



        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1 = new System.Drawing.Pen(Color.Blue, 2F);
        private System.Drawing.Pen pen2 = new System.Drawing.Pen(Color.Red, 2F);
        private System.Drawing.Pen pen3 = new System.Drawing.Pen(Color.Green, 2F);
     
        public Firststage()
        {

            InitializeComponent();

           // label1.Text = "Please eneter number of connections:";


        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            int TimeFactor = 1;

            if (textBox1.Text != "")
            {

                TimeFactor = Convert.ToInt32(textBox1.Text);
            }
            


            StreamReader readtext = new StreamReader("write.txt");
            string readmetext = readtext.ReadLine();


            StreamWriter writeconf = new StreamWriter("writeconf.txt");
            String outstring = ""  + "";
            Rectangle h = new Rectangle(0, 130, 130, 40);

            Rectangle mark = new Rectangle(0, 172, 130, 60);
           
            


            string[] words = readmetext.Split(',');
            int SetLimit = Convert.ToInt32(words[0]);


            int[] conf = new int[SetLimit];
            int[] consume = new int[SetLimit];

            int[] prod = new int[SetLimit];

            int[] delay = new int[SetLimit];

            int[] change = new int[SetLimit];


            //in zero at start

            int[] tv = new int[SetLimit];
            int[] sv = new int[SetLimit];
            int[] dv = new int[SetLimit];
            
            

            //in zero at start
            int i,j,c,t,step=0,b,k,p=0,Flag=0;
            int SetLimitcoonection = 0;
            int[,] syn = new int[SetLimit, SetLimit];
            int[,] temp = new int[SetLimit, SetLimit];
            int[,] cons = new int[SetLimit, SetLimit];

            foreach (string word in words)
            {

                int loop = 1;
                for (int kk = 0; kk <= SetLimit-1; kk++)
                {

                    conf[kk] = Convert.ToInt32(words[loop]);
                    loop++;
                    consume[kk] = Convert.ToInt32(words[loop]);
                    loop++;
                    prod[kk] = Convert.ToInt32(words[loop]);
                    loop++;
                    delay[kk] = Convert.ToInt32(words[loop]);
                    loop++;

                }


                for (i = 0; i < SetLimit; i++)		//consumption matrix
                {
                    for (j = 0; j < SetLimit; j++)
                        if (i == j)
                        {
                            cons[i,j] = (-consume[i]);
                        }
                        else
                        {
                            cons[i,j] = 0;

                        }
                }





                for (int kk = 0; kk <= SetLimit - 1; kk++)
                {
                    tv[kk] = 0;
                    dv[kk] = 0;
                    change[kk] = 0;
                  //  hv[kk] = 0;

                }


                    for (int kk = 0; kk < SetLimit; kk++)
                    {

                        for (int jj = 0; jj < SetLimit; jj++)
                        {

                            syn[kk, jj] = 0;
                           
                        }
                    }

                    StreamReader readtext2 = new StreamReader("writeconnection.txt");
                    string readmetext2 = readtext2.ReadLine();

                    string[] words2 = readmetext2.Split(',');
                    SetLimitcoonection = Convert.ToInt32(words2[0]);

                    int kkk = 1;
                    for (int gg = 0; gg <SetLimitcoonection; gg++)
                    {
                        
                        syn[(Convert.ToInt32(words2[kkk])-1), (Convert.ToInt32(words2[kkk+1])-1)] = 1;

                        kkk= kkk + 2;

                    }

                    readtext2.Close();
                }


   
            //______________________________________________-here


            readtext.Close();

            printresult(SetLimit, conf, tv, dv, syn, SetLimitcoonection,consume,prod,delay,step,TimeFactor,h,mark,Flag,sv);
            savetofile(SetLimit, writeconf,outstring,conf);
            
            //Read complete________________________________

            //computation
            //________________________________________________________________________________________________________________
           /* int halt()
            {
            int h1=0,h2=0;
            for(i=0;i<SetLimit;i++)
	        {
	            if (consume[i]>conf[i])
		        {
		        h1=h1+1;
		        }
	        }
            for(int i=0;i<SetLimit;i++)
	        {
	    	hv[i]=dv[i];
	        }
            for(int i=0;i<SetLimit;i++)
	        {
	            if (hv[i]>0)
	        	{
		            hv[i]=hv[i]-1;
		        }
	        }
            for(int i=0;i<SetLimit;i++)
        	{
	            if(hv[i]==0)
	        	{
		    	h2=h2+1;
		        }
	        }
			
        if((h1==SetLimit)&&(h2==SetLimit))
        return 0;
        else
        return 1;	
        }*/


            do{
                step++;
                c=0;
                t=0;
                for(i=0;i<SetLimit;i++)
	            if (dv[i]>0)
	            {
	            	dv[i]=dv[i]-1;
	            }

                for(i=0;i<SetLimit;i++)
	            if (tv[i]>0)
	            {
		            tv[i]=tv[i]-1;		
	            }



               for(i=0;i<SetLimit;i++)
	           if ((dv[i]==0)&&(consume[i]<=conf[i]))
	           {
		            sv[i]=1;	//obtain spiking vector
	           }
	           else
	           { sv[i]=0;}
               for(i=0;i<SetLimit;i++)
               {
                    if(sv[i]==1)
                        {c=c+1;}
               }



                if(c>0)
                {
                      for(j=0;j<SetLimit;j++)
	                   {
	                	    temp[0,j]=0;
		                    for(k=0;k<SetLimit;k++)
		                    {
		                      temp[0,j]=temp[0,j]+sv[k]*cons[k,j];
		                    }
	                   }

                       for(i=0;i<SetLimit;i++)
	                    {
	                          conf[i]=conf[i]+temp[0,i];
	                          change[i]+=-temp[0,i];
	                    }
                 }




        for(i=0;i<SetLimit;i++)		//checking transfer vector
        {
	        if(tv[i]==1)
	        {
	        	t=t+1;
		        for(b=0;b<SetLimit;b++)
		        {
	                if(syn[i,b]==1)
			        {
			        	if(dv[b]==0)
			        	{
				        	if(change[i]>=prod[i])
				        	{
				            	conf[b]=conf[b]+prod[i];
				            	change[i]-=prod[i];
				        	}
					
			   	        }
			        }
			
		        }
	      }
	
        }




printresult(SetLimit, conf, tv, dv, syn, SetLimitcoonection,consume,prod,delay,step,TimeFactor,h,mark,Flag,sv);
savetofile(SetLimit, writeconf,outstring,conf);

for(i=0;i<SetLimit;i++)
	{
		if(sv[i]==1)
		{
			if(delay[i]>0)
			{
			dv[i]=delay[i]+1;
			tv[i]=delay[i]+1;
			//l++;
			}
			else
			{
			dv[i]=0;
			tv[i]=0;
			}
		}
		
	}

}while(Convert.ToBoolean( halt(SetLimit,tv,consume,conf)));


            Flag = 1;
            writeconf.Close();
           // PictureBox1.Refresh();
          //  printresult(SetLimit, conf, tv, dv, syn, SetLimitcoonection, consume, prod, delay, step, TimeFactor, h, mark, Flag);
           // g.DrawString("Iteration--"+"Halts", new Font("Arial", 12), Brushes.Black, h);
            //___________________________________________________________________________________________________________________
            
            }

         


        //halt code
        int halt(int SetLimit,int[] tv,int[] consume,int[] conf)
        {
            int[] hv = new int[SetLimit];
            for (int kk = 0; kk <= SetLimit - 1; kk++)
            { hv[kk] = 0; }
            int h1 = 0, h2 = 0, h3 = 0, i;
            for (i = 0; i < SetLimit; i++)
            {
                if (consume[i] > conf[i])
                {
                    h1 = h1 + 1;
                }
            }
            for (i = 0; i < SetLimit; i++)
            {
                hv[i] = tv[i];
            }
            for (i = 0; i < SetLimit; i++)
            {
                if (hv[i] == 1)
                {
                    h3 = h3 + 1;
                    hv[i] = hv[i] - 1;
                }
               /* if(hv[i]==0)
                {
                    h3 = h3 + 1;
                }*/
            }
            for (i = 0; i < SetLimit; i++)
            {
                if (hv[i] == 0)
                {
                    h2 = h2 + 1;
                }
            }

            if ((h1 == SetLimit) && (h2 == SetLimit))    //&&(h3>0)    
            {  return 0;}
            else
            { return 1; }
        }
    

        void printresult(int SetLimit, int[] conf, int[] tv, int[] dv, int[,] syn, int SetLimitcoonection,int[] consume,int[] prod,int[] delay,int step,int TimeFactor,Rectangle h,Rectangle mark,int Flag,int[] sv)
        {
            
           // StreamWriter writeconf = new StreamWriter("writeconf.txt");
            g = PictureBox1.CreateGraphics();


          string[,] tempposition = new string[SetLimit, SetLimit];
          int  ssw=100;

          for (int kk = 0; kk < SetLimit; kk++)
            {

                for (int jj = 0; jj < SetLimit; jj++)
                {
                    
                    
                    
                        string xx= ""+ (kk * ssw + 40) + "," + ( 40 + kk * 10) +"";

                        string yy="" + (((jj + 1) * ssw) + 40) + ","+  ( 40 +(( jj + 1) * 10))+ "";
    

                         tempposition[kk, jj] = xx + "-" + yy;
                    }
                  
                }

          string[,] tempnew = new string[SetLimit, SetLimit];
          for (int kk = 0; kk < SetLimit; kk++)
          {

              for (int jj = 0; jj < SetLimit; jj++)
              {
                  tempnew[kk, jj] = tempposition[kk, jj];
              }
          }

          for (int kk = 0; kk < SetLimit; kk++)
          {

              for (int jj = 0; jj < SetLimit; jj++)
              {
                  if (kk == jj)
                  {
                      for (int pp = jj; pp < SetLimit-1; pp++)
                      {
                          
                          tempposition[kk, pp + 1] =tempnew[kk,pp];
                      }

                      tempposition[kk, jj] = "0,0" + "-" + "0,0";

                      
                  }
              }
          }

          for (int kk = 0; kk < SetLimit; kk++)
          {

              for (int jj = 0; jj < SetLimit; jj++)
              {
                  if (kk > jj)
                  {
                     string ss=tempposition[jj, kk];
                     string[] xx = ss.Split('-');
                     tempposition[kk, jj] = xx[1] +"-"+ xx[0];

                   //   tempposition[kk, jj] 
                  }
              }
          }
                    



            for (int kk = 0; kk < Convert.ToInt32(SetLimit); kk++)
            {

                int ss = 100;
                int yyf = 10;
                 if (tv[kk] == 0)
                 {


                     Pen Pens = new Pen(Brushes.Green);
                     Pens.Width = 4f;
                     Rectangle r = new Rectangle(kk * ss, kk * yyf, 80, 100);
                     g.DrawRectangle(Pens, r);
                     g.DrawString("Neuron" + (kk + 1) + "" + "----------------------------"+ conf[kk] + "," + consume[kk] + "-->" + prod[kk] + "(" + delay[kk]+")", new Font("Arial", 12), Brushes.Green, r);
                   //  r. = Color.Green;
                 }
                 if (tv[kk] == 1)
                 {

                     Pen Pens = new Pen(Brushes.Black);
                     Pens.Width = 4f;
                    Rectangle s = new Rectangle(kk * ss, kk * yyf, 80, 100);
                    g.DrawRectangle(Pens, s);
                    g.DrawString("Neuron" + (kk + 1) + "" + "----------------------------"+ conf[kk] + "," + consume[kk] + "-->" + prod[kk] + "(" + delay[kk] + ")", new Font("Arial", 12), Brushes.Black, s);
                  //  g.Graphics.FillRectangle(Color.Green, s);
                 }
                if (tv[kk] > 1)
                {

                    Pen Pens = new Pen(Brushes.Red);
                    Pens.Width = 4f;
                    Rectangle t = new Rectangle(kk * ss, kk * yyf, 80, 100);
                    g.DrawRectangle(Pens, t);
                    g.DrawString("Neuron" + (kk + 1) + "" + "----------------------------"+ conf[kk] + "," + consume[kk] + "-->" + prod[kk] + "(" + delay[kk] + ")", new Font("Arial", 12), Brushes.Red, t);
                }
              //  Rectangle h = new Rectangle(0,130, 100, 100);
                Pen Pens1 = new Pen(Brushes.Black);
                g.DrawRectangle(Pens1, h);
                g.DrawString("Iteration--" + step, new Font("Arial", 12), Brushes.Black, h);

                Pen Pens2 = new Pen(Brushes.Black);
                g.DrawRectangle(Pens2, mark);
                g.DrawString("spikes," + "consume" + "---->" + "produce" + "(" + "delay" + ")", new Font("Arial", 12), Brushes.Black, mark);
            


                
                Rectangle active = new Rectangle(150, 210, 25, 25);
                Pen Pensg = new Pen(Brushes.Green);
                Pensg.Width = 3f;
                g.DrawRectangle(Pensg, active);

                Rectangle inactive = new Rectangle(190, 210, 25, 25);
                Pen Pensr = new Pen(Brushes.Red);
                Pensr.Width = 3f;
                g.DrawRectangle(Pensr,inactive );

                Rectangle tran = new Rectangle(230, 210, 25, 25);
                Pen Penst = new Pen(Brushes.Black);
                Penst.Width = 3f;
                g.DrawRectangle(Penst,tran);

                /*if (Convert.ToBoolean(halt(SetLimit, dv, consume, conf) == 0))
                {
                    g.DrawString("Iteration--" + step+"Halts", new Font("Arial", 12), Brushes.Black, h);
                }*/


                    for (int jk = 0; jk < SetLimit; jk++)
                    {

                       if( syn[kk, jk] == 1)
                       {

                           string cc=tempposition[kk, jk];
                           string[] xx = cc.Split('-');
                           string[] xx0 = xx[0].Split(',');
                           string[] yy0 = xx[1].Split(',');

                           if(kk<jk)
                           {
                             g.DrawLine(pen1, Convert.ToInt32(xx0[0]), Convert.ToInt32(xx0[1]), Convert.ToInt32(yy0[0]), Convert.ToInt32(yy0[1]));
                           }
                           else //kk>jk
                           {
                               if (syn[jk, kk] != 1)
                               {
                                   g.DrawLine(pen2, Convert.ToInt32(xx0[0]), Convert.ToInt32(xx0[1]), Convert.ToInt32(yy0[0]), Convert.ToInt32(yy0[1]));
                               }
                           }
                          // for (int mk = 0; mk < jk; mk++)
                          // {
                               if(syn[jk,kk]==1)
                               {
                                   string cc1 = tempposition[jk, kk];
                                   string[] xx1 = cc.Split('-');
                                   string[] xx10 = xx1[0].Split(',');
                                   string[] yy10 = xx1[1].Split(',');
                                   g.DrawLine(pen3, Convert.ToInt32(xx10[0]), Convert.ToInt32(xx10[1]), Convert.ToInt32(yy10[0]), Convert.ToInt32(yy10[1]));
                               }
                         //  }
                              

                          
                       }

                    }
     
                
            }
            var stopwatch = Stopwatch.StartNew();
            Thread.Sleep(TimeFactor*1000);
            stopwatch.Stop();
            if ((Convert.ToBoolean(haltdraw(SetLimit, tv, consume, conf,sv,delay) == 1)))
            {   //Convert.ToBoolean(halt(SetLimit, tv, consume, conf)==1)&& 
                PictureBox1.Refresh();
            }
            else
            {
                g.DrawString("                       "+"Halts", new Font("Arial", 12), Brushes.Black, h);
            }
         
                
            
            
        }


        void savetofile(int SetLimit,StreamWriter writeconf,String outstring,int[] conf)
        {
            for (int i = 0; i < SetLimit; i++)
            {
                outstring += conf[i];
                outstring += ",";
            }

            writeconf.WriteLine(outstring);
          //  writeconf.Close();
        }

        int haltdraw(int SetLimit, int[] tv, int[] consume,int[] conf,int[] sv,int[] delay)
        {
           
            int[] dhv = new int[SetLimit];
            for (int kk = 0; kk <= SetLimit - 1; kk++)
            { dhv[kk] = 0; }
            int dh1 = 0, dh2 = 0, dh3 = 0, i;
            for (i = 0; i < SetLimit; i++)
            {
                dhv[i] = tv[i];
            }
            for ( i = 0; i < SetLimit; i++)
            {
                if (sv[i] == 1)
                {
                    if (delay[i] > 0)
                    {
                        dhv[i] = delay[i] + 1;
                        //tv[i] = delay[i] + 1;
                        //l++;
                    }
                    else
                    {
                        //dv[i] = 0;
                        tv[i] = 0;
                    }
                }

            }


            for (i = 0; i < SetLimit; i++)
            {
                if (consume[i] > conf[i])
                {
                    dh1 = dh1 + 1;
                }
            }
            
            for (i = 0; i < SetLimit; i++)
            {
                if (dhv[i] == 1)
                {
                    dh3 = dh3 + 1;
                    dhv[i] = dhv[i] - 1;
                }
                /* if(hv[i]==0)
                 {
                     h3 = h3 + 1;
                 }*/
            }
            for (i = 0; i < SetLimit; i++)
            {
                if (dhv[i] == 0)
                {
                    dh2 = dh2 + 1;
                }
            }

            if ((dh1 == SetLimit) && (dh2 == SetLimit) )   // && (dh3 > 0)
            { return 0; }
            else
            { return 1; }
        }

        private void Firststage_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();

            StreamReader readtext3 = new StreamReader("writeconf.txt");

            int go = 0;

            if (textBox2.Text == "")
            {
                string[] content = readtext3.ReadToEnd().Replace("\n", "").Split('\t');


                //   string[] readconf = readmetext3.Split(',');
                  string ddd = content[0];





                  MessageBox.Show(ddd);
                  
            }
            else
            {

                go = Convert.ToInt32(textBox2.Text);
                string line;

                int someval = 0;
                while ((line = readtext3.ReadLine()) != null)
                {
                    // 4
                    // Insert logic here.
                    // ...
                    // "line" is a line in the file. Add it to our List.


                    if (someval >= go)
                    {
                        lines.Add(line);
                    }
                    someval++;

                }
               // DialogResult dialogResult = MessageBox.Show(line, "Result", MessageBoxButtons.YesNo);
               // int ct = 0;
                foreach (string s in lines)
                {

                    
                    
                        DialogResult dialogResult = MessageBox.Show(s, "Result", MessageBoxButtons.YesNo);
                            
                        // MessageBox.Show( MessageBoxButtons.YesNo);
                    
                    
                    if (dialogResult == DialogResult.Yes)
                    {

                         MessageBox.Show(s, "Result", MessageBoxButtons.YesNo);
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                        break;
                       
                        //this.Close();
                        //do something else
                    }

                    
                }

            }
           
           // string readmetext3 = readtext3.ReadLine();


           // StreamReader reader = new StreamReader("input.txt");
        //    string[] content = readtext3.ReadToEnd().Replace("\n", "").Split('\t');


         //   string[] readconf = readmetext3.Split(',');
          //  string ddd = content[0];

            



          //  MessageBox.Show(ddd);

            
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

        private void button4_Click(object sender, EventArgs e)
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
