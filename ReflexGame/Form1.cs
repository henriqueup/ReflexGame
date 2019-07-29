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
        Thread circleCreation, circleGrowth;
        const string scoreStr = "Score: ";
        int curScore;
        bool playing;
        MyRandomNumberGenerator rnd;
        public List<Rectangle> circles = new List<Rectangle>(); //try my own class of rectangles for performance comparison

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            DoubleBuffered = true;
            rnd = new MyRandomNumberGenerator();

            playing = true;
            curScore = 0;
            Run();
        }

        public int GetCurrentScore()
        {
            return curScore;
        }

        public void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            //circleCreation.Join();
            //circleGrowth.Join();
        }

        public void CreatesCircles()
        {
            while (playing)
            {
                if (circles.Count < 10)
                {
                    Wait(1000);
                    CreateNewCircle();
                    UpdatePainting();
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
            while (playing)
            {
                Wait(100);
                GrowAllCircles();
                UpdatePainting();
            }
        }

        public void Run()
        {
            //CreateNewCircle();
            //UpdatePainting();

            circleCreation = new Thread(new ThreadStart(CreatesCircles))
            {
                IsBackground = true
            };
            circleCreation.Start();

            circleGrowth = new Thread (new ThreadStart(GrowsCircles))
            {
                IsBackground = true
            };
            circleGrowth.Start();
        }

        public bool GetThreadStatuses()
        {
            return circleCreation.IsAlive && circleGrowth.IsAlive;
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

        void AddScore(Rectangle circle)
        {
            double circleArea = Math.PI * (circle.Width/2)*(circle.Width/2);
            curScore += (int)circleArea;
            labelScore.Text = scoreStr + curScore;
        }

        public void ProcessClick(Point clicked)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                if (PointIsInsideCircle(clicked, circles[i]))
                {
                    AddScore(circles[i]);

                    circles.RemoveAt(i);
                    i--;
                    UpdatePainting();
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
