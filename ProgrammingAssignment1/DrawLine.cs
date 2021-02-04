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
    public partial class DrawLine : Form
    {
        Bitmap myBitmap;
        Graphics myGraphic;
        Point start_point, end_point;
        int selected;
        Color lineColor = Color.Black;

        public DrawLine()
        {
            InitializeComponent();
            myBitmap = new Bitmap(drawingScreen.Width, drawingScreen.Height);
            myGraphic = Graphics.FromImage(myBitmap);
        }

        private void dotButton_Click(object sender, EventArgs e)
        {
            selected = 1;
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            selected = 2;
        }

        private void dotLineButton_Click(object sender, EventArgs e)
        {
            selected = 3;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lineColor = colorDialog1.Color;
            }
        }

        private void drawingScreen_MouseDown(object sender, MouseEventArgs e)
        {
            drawingScreen.Cursor = Cursors.Cross;
            start_point = new Point(e.X, e.Y);
            drawingScreen.Image = myBitmap;
        }

        private void drawingScreen_MouseUp(object sender, MouseEventArgs e)
        {
            drawingScreen.Cursor = Cursors.Default;
            drawingScreen.Image = myBitmap;
            end_point = new Point(e.X, e.Y);
            Line(start_point, end_point);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawingScreen.Image = null;
            myGraphic.Clear(Color.White);
        }

        public void Line(Point p1, Point p2)
        {
            int dx, dy;
            float m, n;
            dx = Math.Abs(p2.X - p1.X);
            dy = Math.Abs(p2.Y - p1.Y);

            if (p1.X != p2.X && p1.Y != p2.Y)
            {
                m = Math.Abs(dy / dx);
                n = Math.Abs(dx / dy);
            }

            switch(selected)
            {
                case 1: // Draw dot
                    myBitmap.SetPixel(p1.X, p1.Y, lineColor);
                    break;

                case 2: // Draw line
                    if (p1.Y == p2.Y) // Cover Case 1 & 2
                    {
                        if (p1.X < p2.X)
                        {
                            for (int i = p1.X; i <= p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (p1.Y + k), lineColor);
                                }
                            }
                        }
                        else
                        {
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (p1.Y + k), lineColor);
                                }
                            }
                        }
                        
                    }
                    else if (p1.X == p2.X) //Cover Case 3 & 4
                    {
                        if (p1.Y > p2.Y)
                        {
                            for (int i = p2.Y ; i >= p1.Y; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel((p1.X + k), i, lineColor);
                                }
                            }
                        }
                        else
                        {
                            for (int i = p1.Y; i <= p2.Y; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel((p1.X + k), i, lineColor);
                                }
                            }
                        }
                    }
                    else if (dx == dy) // Cover Case 5 -8
                    {
                        int j; 
                        if (p1.X < p2.X && p1.Y > p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p1.X; i <= p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j--;
                            }
                        }
                        else if (p1.X > p2.X && p1.Y > p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j--;
                            }
                        }
                        else if (p1.X > p2.X && p1.Y < p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j++;
                            }
                        }
                        else // p1.X < p2.X && p1.Y < p2.Y
                        {
                            j = p1.Y;
                            for (int i = p1.X; i < p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j++;
                            }
                        }
                    }
                    // Bresenham Line Algorithm (Case 9-16)
                    if (p2.X > p1.X) // |dx| > |dy|
                    {
                        if (p2.Y > p1.Y)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x <= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x++;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y++;
                                }
                            }
                        }
                        else // Case 16 (theta < 45 degree 4th quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x <= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x++;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y--;
                                }
                            }
                        }
                    }
                    else 
                    {
                        if (p2.Y > p1.Y) // Case 12 (theta < 45 degree 2nd quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x >= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x--;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y++;
                                }
                            }
                        }
                        else // Case 13 (theta < 45 degree 3rd quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x >= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x--;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y--;
                                }
                            }
                        }
                    }
                    // |dy| > |dx|
                    if (p2.X > p1.X)
                    {
                        if (p2.Y >p1.Y) // Case 10 (theta > 45 degree 1st quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y <= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y++;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x++;
                                }
                            }
                        }
                        else // Case 15 (theta > 45 degree 4th quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y >= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y--;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (p2.Y > p1.Y) // Case 11 (theta > 45 degree 2nd quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y <= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y++;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x--;
                                }
                            }
                        }
                        else // Case 14 (theta > 45 degree 3rd quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y >= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y--;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x--;
                                }
                            }
                        }


                    }
                    
                    break;

                case 3:
                    if (p1.Y == p2.Y) // Cover Case 1 & 2
                    {
                        if (p1.X < p2.X)
                        {
                            for (int i = p1.X; i <= p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                    {
                                        myBitmap.SetPixel(i, (p1.Y + k), lineColor);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                    {
                                        myBitmap.SetPixel(i, (p1.Y + k), lineColor);
                                    }
                                }
                            }
                        }

                    }
                    else if (p1.X == p2.X) //Cover Case 3 & 4
                    {
                        if (p1.Y > p2.Y)
                        {
                            for (int i = p2.Y; i >= p1.Y; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                    {
                                        myBitmap.SetPixel((p1.X + k), i, lineColor);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = p1.Y; i <= p2.Y; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                    {
                                        myBitmap.SetPixel((p1.X + k), i, lineColor);
                                    }
                                }
                            }
                        }
                    }
                    else if (dx == dy) // Cover Case 5 -8
                    {
                        int j;
                        if (p1.X < p2.X && p1.Y > p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p1.X; i <= p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                        myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j--;
                            }
                        }
                        else if (p1.X > p2.X && p1.Y > p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                        myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j--;
                            }
                        }
                        else if (p1.X > p2.X && p1.Y < p2.Y)
                        {
                            j = p1.Y;
                            for (int i = p2.X; i >= p1.X; i--)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                        myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j++;
                            }
                        }
                        else // p1.X < p2.X && p1.Y < p2.Y
                        {
                            j = p1.Y;
                            for (int i = p1.X; i < p2.X; i++)
                            {
                                for (int k = 0; k < Convert.ToInt32(numericUpDownWidth.Value); k++)
                                {
                                    if (i % 2 == 0)
                                        myBitmap.SetPixel(i, (j + k), lineColor);
                                }
                                j++;
                            }
                        }
                    }
                    // Bresenham Line Algorithm (Case 9-16)
                    if (p2.X > p1.X) // |dx| > |dy|
                    {
                        if (p2.Y > p1.Y)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x <= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x++;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y++;
                                }
                            }
                        }
                        else // Case 16 (theta < 45 degree 4th quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x <= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x++;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y--;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (p2.Y > p1.Y) // Case 12 (theta < 45 degree 2nd quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x >= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x--;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y++;
                                }
                            }
                        }
                        else // Case 13 (theta < 45 degree 3rd quadrant)
                        {
                            int dR = 2 * dy;
                            int dUR = 2 * (dy - dx);
                            int d = 2 * dy - dx;

                            int x = p1.X;
                            int y = p1.Y;

                            while (x >= p2.X)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                x--;
                                if (d < 0) // M is below the line, pick R
                                {
                                    d = d + dR;
                                }
                                else // M is either on the line or above the line, either case pick UR
                                {
                                    d = d + dUR;
                                    y--;
                                }
                            }
                        }
                    }
                    // |dy| > |dx|
                    if (p2.X > p1.X)
                    {
                        if (p2.Y > p1.Y) // Case 10 (theta > 45 degree 1st quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y <= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y++;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x++;
                                }
                            }
                        }
                        else // Case 15 (theta > 45 degree 4th quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y >= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y--;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (p2.Y > p1.Y) // Case 11 (theta > 45 degree 2nd quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y <= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y++;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x--;
                                }
                            }
                        }
                        else // Case 14 (theta > 45 degree 3rd quadrant)
                        {
                            int dR = 2 * dx;
                            int dUR = 2 * (dx - dy);
                            int d = 2 * dx - dy;

                            int x = p1.X;
                            int y = p1.Y;

                            while (y >= p2.Y)
                            {
                                myBitmap.SetPixel(x, y, lineColor);
                                y--;

                                if (d < 0)
                                {
                                    d = d + dR;
                                }
                                else
                                {
                                    d = d + dUR;
                                    x--;
                                }
                            }
                        }


                    }
                    break;
            }
        }
        
    }
}
