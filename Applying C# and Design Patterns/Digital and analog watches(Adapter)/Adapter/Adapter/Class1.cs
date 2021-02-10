using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class DigitalWatch
    {
        public string currentTime;

        public Bitmap getContur()
        {
            Bitmap contur = new Bitmap(800, 488);
            Graphics g = Graphics.FromImage(contur);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(Brushes.Black, 200, 150, 400, 100);
            g.DrawString(currentTime,
            new System.Drawing.Font("TimesNewRoman", 64, FontStyle.Bold),
            new SolidBrush(Color.White), 
            220, 150);

            return contur;
        }


        public void setCurrentTime(string currentTime)
        {
            this.currentTime = currentTime;
        }

        public void increaseSecond()
        {
            int hours = Int32.Parse(currentTime.Split(':')[0]);
            int minutes = Int32.Parse(currentTime.Split(':')[1]);
            int seconds = Int32.Parse(currentTime.Split(':')[2]);

            seconds++;

            if (seconds > 59)
            {
                seconds = 0;
                minutes++;

                if(minutes > 59)
                {
                    minutes = 0;
                    hours++;

                    if(hours > 23)
                    {
                        hours = 0;
                    }
                }
            }

            this.currentTime = (hours < 10 ? "0" + hours.ToString(): hours.ToString()) + ":" 
                                + (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":" 
                                + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
        }
    }

    class Adapter : DigitalWatch
    {
        private AnalogWatch analogWatch = new AnalogWatch();

        public void setCurrentTime(string currentTime)
        {
            int hours = Int32.Parse(currentTime.Split(':')[0]);
            int minutes = Int32.Parse(currentTime.Split(':')[1]);
            int seconds = Int32.Parse(currentTime.Split(':')[2]);
            analogWatch.setCurrentAngles(Math.PI / 6 * Convert.ToDouble(hours - 3) + 0.13, Math.PI / 30 * Convert.ToDouble(minutes - 14) + 0.03, Math.PI / 30 * Convert.ToDouble(seconds - 13) + 0.03);
        }

        public Bitmap getContur()
        {
            Bitmap contur = new Bitmap(800, 488);
            Graphics g = Graphics.FromImage(contur);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillEllipse(Brushes.Black, 200, 20, 400, 400);

            double add = Math.PI / 6;
            int num = 3;

            for (int i = 0; i < 12; i++)
            {
                double x1 = 387 + 180 * Math.Cos(add * i);
                double y1 = 205 + 180 * Math.Sin(add * i);
                g.DrawString(num.ToString(),
                     new System.Drawing.Font("TimesNewRoman", 20, FontStyle.Bold),
                     new SolidBrush(Color.White),
                     Convert.ToSingle(x1), Convert.ToSingle(y1));

                if (num == 12)
                    num = 1;
                else
                    num++;
            }

            double x = 387 + 100 * Math.Cos(analogWatch.hourAngle);
            double y = 205 + 100 * Math.Sin(analogWatch.hourAngle);
            g.DrawLine(new Pen(Color.White, 5), 400, 220, Convert.ToSingle(x), Convert.ToSingle(y));

            x = 387 + 100    * Math.Cos(analogWatch.minuteAngle);
            y = 205 + 100 * Math.Sin(analogWatch.minuteAngle);
            g.DrawLine(new Pen(Color.White, 3), 400, 220, Convert.ToSingle(x), Convert.ToSingle(y));

            x = 387 + 100 * Math.Cos(analogWatch.secondAngle);
            y = 205 + 100 * Math.Sin(analogWatch.secondAngle);
            g.DrawLine(new Pen(Color.Red, 1), 400, 220, Convert.ToSingle(x), Convert.ToSingle(y));
            g.FillEllipse(Brushes.White, 390, 210, 20, 20);

            return contur;
        }
    }

    class AnalogWatch
    {
        public double hourAngle;
        public double minuteAngle;
        public double secondAngle;

        public void setCurrentAngles(double hour, double minute, double second)
        {
            this.hourAngle = hour;
            this.minuteAngle = minute;
            this.secondAngle = second;
        }

    }
}
