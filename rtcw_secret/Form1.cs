using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;

namespace rtcw_secret
{
    public partial class Form1 : Form
    {

        public Mem m = new Mem();

        string nameGame = "WolfSP";

        bool processOpen = false;
        bool checkLoad = false;
        int pID = 0;

        public Form1()
        {
            InitializeComponent();
        }


        int r = 252;
        int g = 3;
        int b = 3;

        private void Form1_Load(object sender, EventArgs e)
        {

            s_p.Text = "0";
            s_all.Text = "0";
            t_p.Text = "0";
            t_all.Text = "0";
            allEnd.Text = "";

            timer1.Start();

            timer3.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            processOpen = processOpen = m.OpenProcess(nameGame);
            pID = m.GetProcIdFromName(nameGame);
            int runProcess = m.ReadByte(nameGame + ".exe+A4021C");

            if (processOpen)
            {

                if (runProcess == 0)
                {
                    m.CloseProcess();
                }

                int secretPick = m.ReadInt("qagamex86.dll+5791E8");
                int secretAll = m.ReadInt("qagamex86.dll+726960");
                
                int treasurePick = m.ReadInt("qagamex86.dll+5791EC");
                int treasureAll = m.ReadInt("qagamex86.dll+726964");

                string nameStage = m.ReadString("cgamex86.dll+5A202D").Replace(".bsp", "");
                //string cutStage = "cutscene";



                if(secretPick >= 0) s_p.Text = secretPick.ToString();
                if(secretAll >= 0) s_all.Text = secretAll.ToString();

                if (treasurePick >= 0) t_p.Text = treasurePick.ToString();
                if (treasureAll >= 0) t_all.Text = treasureAll.ToString();

                if ((treasureAll != 0 || secretAll != 0) && (treasurePick == treasureAll && secretPick == secretAll)) allEnd.Text = "Everything collected";
                else if(treasureAll == 0 && secretAll == 0) allEnd.Text = "";
                else allEnd.Text = "";
                /*
                if (m.ReadByte(nameGame + ".exe+A4021C") == 1)
                {
                    if (nameStage.Contains(cutStage)) label7.Text = "";
                    else label7.Text = nameStage;
                }*/
               

            }
            else
            {
              
                string na = "N/A";
                s_p.Text = na;
                s_all.Text = na;
                t_p.Text = na;
                t_all.Text = na;
                allEnd.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }



        // COLORS TITTLE

        private void timer3_Tick(object sender, EventArgs e)
        {
            g += 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (g <= 252)
            {
                timer3.Stop();
                timer4.Start();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            r -= 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (r <= 3)
            {
                timer4.Stop();
                timer5.Start();
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            b += 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (b >= 252)
            {
                timer5.Stop();
                timer6.Start();
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            g -= 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (g <= 3)
            {
                timer6.Stop();
                timer7.Start();
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            r += 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (r >= 252)
            {
                timer7.Stop();
                timer8.Start();
            }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            b -= 5;
            tittleProgram.ForeColor = Color.FromArgb(r, g, b);
            if (b <= 3)
            {
                timer8.Stop();
                timer3.Start();
            }
        }

        /* ------------- */

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseX = e.X;
            mouseY = e.Y;
        }
    }
}
