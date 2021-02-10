using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adapter
{
    public partial class Form1 : Form
    {
        Bitmap contur;
        public DigitalWatch digital = new DigitalWatch();
        Adapter adaptedAnalog = new Adapter();
        Thread timeUpdate;
        string currentWatch = "Digital";

        public Form1()
        {
            InitializeComponent();
            contur = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            digital.setCurrentTime("00:00:00");
            timeUpdate = new Thread(TimeUpdate);
            timeUpdate.IsBackground = true;
            timeUpdate.Priority = ThreadPriority.Lowest;
            timeUpdate.Start();
        }

        private void TimeUpdate()
        {
            while(true)
            {
                digital.increaseSecond();
                PaintWatch();
                Thread.Sleep(1000);
            }
        }

        private void PaintWatch()
        {
            Graphics g = Graphics.FromImage(contur);
            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            if (currentWatch == "Digital")
                pictureBox1.Image = digital.getContur();
            else
            {
                adaptedAnalog.setCurrentTime(digital.currentTime);
                pictureBox1.Image = adaptedAnalog.getContur();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (currentWatch == "Digital")
                    currentWatch = "Analog";
                else
                    currentWatch = "Digital";
            }
            else if (e.Button == MouseButtons.Right)
            {
                Form2 newForm = new Form2(digital);
                newForm.Show();
            }

        }
    }
}
