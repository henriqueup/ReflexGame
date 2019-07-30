using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflexGame
{
    public partial class Form1 : Form
    {
        public UI UI;
        Thread circleCreation, circleGrowth, timerThread;
        int limitValue;
        List<string> labelLimitValues = new List<string>();
        public bool IsPlaying { get; private set; }
        MyRandomNumberGenerator rnd;
        public List<Rectangle> circles = new List<Rectangle>(); //try my own class of rectangles for performance comparison

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            DoubleBuffered = true;
            rnd = new MyRandomNumberGenerator();
            AddsGameModes();
        }

        public void AddsGameModes()
        {
            comboBoxModes.Items.Add("Timer");
            comboBoxModes.Items.Add("Clicks");
            labelLimitValues.Add("Limit (seconds):");
            labelLimitValues.Add("Limit (clicks):");

            comboBoxModes.SelectedIndex = 0;
            labelLimit.Text = labelLimitValues[0];
        }

        public void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            //circleCreation.Join();
            //circleGrowth.Join();
        }

        public void CreatesCircles()
        {
            while (IsPlaying)
            {
                if (circles.Count < 10)
                {
                    Wait(1000);
                    if (IsPlaying)
                    {
                        CreateNewCircle();
                        UpdatePainting();
                    }
                }
            }
        }

        public void GrowAllCircles()
        {
            for (int i = 0; i < circles.Count; i++)
            {
                Rectangle rect = circles[i];

                rect.Width++;
                rect.Height++;

                circles[i] = rect;
            }
        }

        public void GrowsCircles()
        {
            while (IsPlaying)
            {
                Wait(100);
                GrowAllCircles();
                UpdatePainting();
            }
        }

        private void TimerMode(int timeToWait)
        {
            Wait(timeToWait * 1000);
            IsPlaying = false;
            UI.GameOver();
        }

        public void Run()
        {
            //CreateNewCircle();
            //UpdatePainting();

            circleCreation = new Thread(() => CreatesCircles())
            {
                IsBackground = true
            };
            circleCreation.Start();

            circleGrowth = new Thread (() => GrowsCircles())
            {
                IsBackground = true
            };
            circleGrowth.Start();

            limitValue = Int32.Parse(numericUpDownLimitValue.Text);
            if (comboBoxModes.SelectedIndex == 0)
            {
                timerThread = new Thread(() => TimerMode(limitValue));
                timerThread.Start();
            }
        }

        public Rectangle GetRandomSquare(IRandomNumberGenerator rnd)
        {
            int x = rnd.NextInt(0, this.Size.Width-35);
            int y = rnd.NextInt(0, this.Size.Height-60);
            return new Rectangle(new Point(x, y), new Size(20, 20));
        }

        public bool PointIsInsideCircle(Point clicked, Rectangle circle)
        {
            Point circleCenter = circle.Location;
            circleCenter.X += circle.Width / 2;
            circleCenter.Y += circle.Height / 2;

            double clickedDistanceFromCenter = Math.Sqrt((clicked.X - circleCenter.X) * (clicked.X - circleCenter.X) + (clicked.Y - circleCenter.Y) * (clicked.Y - circleCenter.Y));
            return clickedDistanceFromCenter <= circle.Width / 2;
        }

        public void ProcessClick(Point clicked)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                if (PointIsInsideCircle(clicked, circles[i]))
                {
                    UI.AddScore(circles[i]);

                    circles.RemoveAt(i);
                    i--;
                    UpdatePainting();


                    if (comboBoxModes.SelectedIndex == 1)
                    {
                        limitValue--;
                        if (limitValue == 0)
                        {
                            IsPlaying = false;
                            UI.GameOver();
                        }
                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point clicked = new Point(Cursor.Position.X - this.Left - 8, Cursor.Position.Y - this.Top - 30);
                ProcessClick(clicked);
            }
        }

        public void CreateNewCircle()
        {
            Rectangle rect = GetRandomSquare(rnd);
            circles.Add(rect);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush bursh = new SolidBrush(Color.Aqua);
            for (int i = 0; i < circles.Count; i++)
            {
                e.Graphics.FillEllipse(bursh, circles[i]);
            }
        }

        public void UpdatePainting()
        {
            //Console.WriteLine("printing");
            this.Invalidate();
            this.Paint += new PaintEventHandler(this.Form1_Paint);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            IsPlaying = true;
            UI = new UI(this);

            this.Size = new Size(800, 600);
            foreach (Control c in this.Controls)
            {
                if (c.Tag.ToString() == "Menu")
                {
                    c.Visible = false;
                }
            }
            
            Run();
        }

        private void comboBoxModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelLimit.Text = labelLimitValues[comboBoxModes.SelectedIndex];
        }

        public void Wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (s, e) =>
            {
                timer.Enabled = false;
                timer.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
