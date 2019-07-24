using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflexGame
{
    class MyRandomGenerator
    {
        readonly bool isFake;
        Random trueGenerator;

        public MyRandomGenerator(bool isFake = false)
        {
            this.isFake = isFake;
            trueGenerator = new Random();
        }

        public int NextInt(int start, int limit, int fakeResult = 0)
        {
            if (isFake)
            {
                return fakeResult;
            }
            else
            {
                return trueGenerator.Next(start, limit);
            }
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Rectangle GetRandomSquare(MyRandomGenerator rnd)
        {
            int squareSize = rnd.NextInt(10, 100);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = GetRandomSquare(new MyRandomGenerator());
            Pen pen = new Pen(Color.Aqua, 2);
            e.Graphics.DrawEllipse(pen, rect);
        }

        public void UpdatePainting()
        {
            //Console.WriteLine("printing");
            this.Invalidate();
            this.Paint += new PaintEventHandler(this.Form1_Paint);
        }
    }
}
