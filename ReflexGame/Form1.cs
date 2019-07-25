﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        bool playing;
        public List<Rectangle> circles = new List<Rectangle>(); //try my own class of rectangles for performance comparison

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            DoubleBuffered = true;

            playing = true;
            Run();
        }

        public void CreatesCircles()
        {
            while (playing)
            {
                if (circles.Count < 10)
                {
                    Wait(2000);
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
                Wait(200);
                GrowAllCircles();
                UpdatePainting();
            }
        }

        public void Run()
        {
            CreateNewCircle();
            UpdatePainting();

            Thread circleCreation = new Thread(new ThreadStart(CreatesCircles))
            {
                IsBackground = true
            };
            circleCreation.Start();

            Thread circleGrowth = new Thread (new ThreadStart(GrowsCircles))
            {
                IsBackground = true
            };
            circleGrowth.Start();
        }

        public Rectangle GetRandomSquare(IRandomNumberGenerator rnd)
        {
            int x = rnd.NextInt(0, this.Size.Width-20);
            int y = rnd.NextInt(0, this.Size.Height-20);
            return new Rectangle(new Point(x, y), new Size(20, 20));
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point clicked = new Point(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        public void CreateNewCircle()
        {
            Rectangle rect = GetRandomSquare(new MyRandomNumberGenerator());
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
