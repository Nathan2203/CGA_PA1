using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingAssignment1
{
    public partial class Form1 : Form
    {
        //int x1=120, y1=50, x2=300, y2=200, temp;
        //int x1 = 120, y1 = 100, x2 = 120, y2 = 300, temp;
        Point start_point, end_point;
        Color colorLine = Color.Black;
        Bitmap myBitmap;
        private void button1_Click(object sender, EventArgs e)
        {
            drawingScreen.Image = myBitmap;
            //Line();
        }

        private void drawingScreen_MouseDown(object sender, MouseEventArgs e)
        {
            //get new start_point
            if (e.Button == MouseButtons.Left)
            {
                start_point = new Point(e.X, e.Y);
                //MessageBox.Show(" " + e.X + " "+ e.Y);
            }
        }

        private void drawingScreen_Paint(object sender, PaintEventArgs e)
        {
            //int x1 = 120, y1 = 100, x2 = 120, y2 = 300, temp
            int x1 = start_point.X, y1 = start_point.Y, x2 = end_point.X, y2 = end_point.Y;
            int temp;
            drawingScreen.Image = myBitmap;
            int dx, dy, d, x, y;
            float m;
            if ((x2 - x1) == 0) // If it's Case 1 & 2
                m = (y2 - y1);
            else
                m = (y2 - y1) / (x2 - x1);

            if (Math.Abs(m) < 1)
            {
                if (x1 > x2)
                {
                    temp = x1;
                    x1 = x2;
                    x2 = temp;

                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }

                dx = Math.Abs(x2 - x1);
                dy = Math.Abs(y2 - y1);

                d = 2 * dy - dx; // Initial parametric value

                x = x1;
                y = y1;

                while (x <= x2)
                {
                    //set pixel
                    myBitmap.SetPixel(x, y, colorLine);
                    x++;

                    if (d >= 1)
                    {
                        if (m < 1)
                            y += 1;
                        else
                            y -= 1;
                        d = d + 2 * dy - 2 * dx;
                    }
                    else
                    {
                        d = d + 2 * dy;
                    }
                }
            }

            if (Math.Abs(m) >= 1)
            {
                if (y1 > y2)
                {
                    temp = x1;
                    x1 = x2;
                    x2 = temp;

                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }

                dx = Math.Abs(x2 - x1);
                dy = Math.Abs(y2 - y1);

                d = 2 * dx - dy; // Initial parametric value

                x = x1;
                y = y1;

                while (y <= y2)
                {
                    //set pixel
                    myBitmap.SetPixel(x, y, colorLine);
                    y++;

                    if (d >= 1)
                    {
                        if (m >= 1)
                            x += 1;
                        else
                            x -= 1;
                        d = d + 2 * dx - 2 * dy;
                    }
                    else
                    {
                        d = d + 2 * dx;
                    }
                }
            }
        }

        private void drawingScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                end_point = new Point(e.X, e.Y);
                drawingScreen.Invalidate();
            }
        }

        
        public Form1()
        {
            InitializeComponent();
            myBitmap = new Bitmap(drawingScreen.Width, drawingScreen.Height);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            drawingScreen.Image = null;
            drawingScreen.BackColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorLine = pictureBox1.BackColor;
        }

        public void Line(Point p1, Point p2)
        {
            
            



        }
    }
}
