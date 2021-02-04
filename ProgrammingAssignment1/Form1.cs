using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ProgrammingAssignment1
{
    public partial class Form1 : Form
    {
        private List<LineList> MyLines = new List<LineList>();
        //int x1=120, y1=50, x2=300, y2=200, temp;
        //int x1 = 120, y1 = 100, x2 = 120, y2 = 300, temp;
        Point start_point, end_point;
        ArrayList listOfPoints;
        bool isMouseDown;
        Color colorLine = Color.Black;
        Bitmap myBitmap;
        Graphics myGraph;
        private void button1_Click(object sender, EventArgs e)
        {
            drawingScreen.Image = myBitmap;
            //Line();
        }

        private void drawingScreen_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            start_point.X = e.X;
            start_point.Y = e.Y;
            end_point.X = e.X;
            end_point.Y = e.Y;
        }

        private void drawingScreen_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (isMouseDown && start_point.X !=-1 && start_point.Y != -1)
            {
                Line(start_point, e.Location);
                start_point.X = -1;
                start_point.Y = -1;
            }
            */
            
            if (isMouseDown == false) return;
            end_point.X = e.X;
            end_point.Y = e.Y;
            
            drawingScreen.Invalidate();
        }
        private void drawingScreen_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (e.Button == MouseButtons.Left)
            {
                LineList DrawLine = new LineList();
                DrawLine.X1 = start_point.X;
                DrawLine.Y1 = start_point.Y;
                DrawLine.X2 = end_point.X;
                DrawLine.Y2 = end_point.Y;
                MyLines.Add(DrawLine);


                pictureBox1.Invalidate();
            }
            /*
            isMouseDown = false;
            if (e.Button == MouseButtons.Left)
            {
                listOfPoints.Add(start_point);
                listOfPoints.Add(end_point);
            }
            */
        }

        private void drawingScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= MyLines.Count - 1; i++)
            {
                //int x1 = 120, y1 = 100, x2 = 120, y2 = 300, temp
                int x1 = MyLines[i].X1, y1 = MyLines[i].Y1, x2 = MyLines[i].X2, y2 = MyLines[i].Y2;
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

            if (isMouseDown == true)
            {
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



        }

        

        
        public Form1()
        {
            InitializeComponent();
            myBitmap = new Bitmap(drawingScreen.Width, drawingScreen.Height);
            listOfPoints = new ArrayList();
            isMouseDown = false;
            myGraph = Graphics.FromImage(myBitmap);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            drawingScreen.Image = null;
            myGraph.Clear(Color.White);
            //drawingScreen.Image = null;
            //drawingScreen.BackColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            colorLine = p.BackColor;
        }

        

        public void Line(Point start_point, Point end_point)
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
    }
}
