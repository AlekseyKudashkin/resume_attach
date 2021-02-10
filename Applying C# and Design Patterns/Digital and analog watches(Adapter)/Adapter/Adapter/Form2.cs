using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adapter
{
    public partial class Form2 : Form
    {
        string currentTime;
        DigitalWatch digital;
        public Form2(DigitalWatch digital)
        {
            InitializeComponent();
            this.digital = digital;
            this.currentTime = digital.currentTime;
            textBox1.Text = currentTime.Split(':')[0];
            textBox3.Text = currentTime.Split(':')[1];
            textBox2.Text = currentTime.Split(':')[2];
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) < 23)
                textBox1.Text = (Int32.Parse(textBox1.Text) + 1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) > 0)
                textBox1.Text = (Int32.Parse(textBox1.Text) - 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox3.Text) < 59)
                textBox3.Text = (Int32.Parse(textBox3.Text) + 1).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox3.Text) > 0)
                textBox3.Text = (Int32.Parse(textBox3.Text) - 1).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox2.Text) < 59)
                textBox2.Text = (Int32.Parse(textBox2.Text) + 1).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox2.Text) > 0)
                textBox2.Text = (Int32.Parse(textBox2.Text) - 1).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.currentTime = (Int32.Parse(textBox1.Text) < 10 ? "0" + Int32.Parse(textBox1.Text).ToString() : Int32.Parse(textBox1.Text).ToString()) + ":"
                                + (Int32.Parse(textBox3.Text) < 10 ? "0" + Int32.Parse(textBox3.Text).ToString() : Int32.Parse(textBox3.Text).ToString()) + ":"
                                + (Int32.Parse(textBox2.Text) < 10 ? "0" + Int32.Parse(textBox2.Text).ToString() : Int32.Parse(textBox2.Text).ToString());
            digital.setCurrentTime(currentTime);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) > 23)
                textBox1.Text = "23";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox3.Text) > 59)
                textBox3.Text = "59";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox2.Text) > 59)
                textBox2.Text = "59";
        }
    }
}
