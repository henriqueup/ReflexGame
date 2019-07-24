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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Rectangle GetRandomSquare()
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = GetRandomSquare();
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
