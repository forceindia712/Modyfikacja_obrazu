using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Lab7
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        int wys, szer;

        int[] R = new int[256];
        int[] G = new int[256];
        int[] B = new int[256];

        int[] H = new int[256];

        int K = 1;

        double[,] macierz;

        public Form1()
        {
            InitializeComponent();
        }

        private void histogram()
        {
            for (int y = 0; y < wys; y++)
            {
                for (int x = 0; x < szer; x++)
                {
                    Color c = bitmap.GetPixel(x, y);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    R[r]++;
                    G[g]++;
                    B[b]++;
                }
            }

            for (int i = 0; i < H.Length; i++)
            {
                H[i] = R[i] + B[i] + G[i];
            }
        }

        private void histogram2(Bitmap bitmapEdited)
        {
            for (int y = 0; y < wys; y++)
            {
                for (int x = 0; x < szer; x++)
                {
                    Color c = bitmapEdited.GetPixel(x, y);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    R[r]++;
                    G[g]++;
                    B[b]++;
                }
            }

            for (int i = 0; i < H.Length; i++)
            {
                H[i] = R[i] + B[i] + G[i];
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (K == 0)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float s = H[i];

                    s = s / (pictureBox1.Image.Height * pictureBox1.Image.Width);
                    s *= 1700;

                    graphR.DrawLine(new Pen(Color.Black), i, panel2.Height, i, panel2.Height - s);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (K == 0)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float s = H[i];

                    s = s / (pictureBox1.Image.Height * pictureBox1.Image.Width);
                    s *= 1700;

                    graphR.DrawLine(new Pen(Color.Black), i, panel2.Height, i, panel2.Height - s);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

            for (int y = 0; y < wys; y++)
            {
                for (int x = 0; x < szer; x++)
                {
                    Color c = bitmapCopy.GetPixel(x, y);

                    double r = (double)c.R;
                    double g = (double)c.G;
                    double b = (double)c.B;

                    int c1 = 102;
                    int d = 230;


                    r = (r - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;
                    g = (g - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;
                    b = (b - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;


                    if (r < 0)
                    {
                        r = 0;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (b > 255)
                    {
                        b = 255;
                    }


                    bitmapEdited.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            pictureBox2.Image = bitmapEdited;

            Array.Clear(R, 0, R.Length);
            Array.Clear(G, 0, G.Length);
            Array.Clear(B, 0, B.Length);


            histogram2(bitmapEdited);
            K = 0;
            panel3.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();


            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bitmapCopy.GetPixel(x, y);

                    double r = (double)c.R;
                    double g = (double)c.G;
                    double b = (double)c.B;

                    int c1 = 102;
                    int d = 230;


                    r = (r - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;
                    g = (g - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;
                    b = (b - trackBar1.Value) * (d - c1) / (trackBar2.Value - trackBar1.Value) + c1;

                    if (r < 0)
                    {
                        r = 0;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (b > 255)
                    {
                        b = 255;
                    }

                    bitmapEdited.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            pictureBox2.Image = bitmapEdited;


            Array.Clear(R, 0, R.Length);
            Array.Clear(G, 0, G.Length);
            Array.Clear(B, 0, B.Length);


            histogram2(bitmapEdited);

            K = 0;
            panel3.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

            double pomocR = 0, pomocG = 0, pomocB = 0;

            double[,] macierz = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };


            for (int i = 0; i < wys; i++)
            {
                for (int j = 0; j < szer; j++)
                {

                    pomocR = 0; pomocG = 0; pomocB = 0;

                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            int x1 = i + k;
                            int y1 = j + l;

                            if (x1 < 0)
                            {
                                x1 = 0;
                            }
                            if (x1 >= bitmap.Height)
                            {
                                x1 = bitmap.Height - 1;
                            }
                            if (y1 < 0)
                            {
                                y1 = 0;
                            }
                            if (y1 >= bitmap.Width)
                            {
                                y1 = bitmap.Width - 1;
                            }
                            Color p = bitmapCopy.GetPixel(y1, x1);

                            double r = (double)p.R;
                            double g = (double)p.G;
                            double b = (double)p.B;

                            pomocR += r * macierz[k + 1, l + 1];
                            pomocG += g * macierz[k + 1, l + 1];
                            pomocB += b * macierz[k + 1, l + 1];

                        }
                    }

                    pomocR = pomocR / 9;
                    pomocG = pomocG / 9;
                    pomocB = pomocB / 9;


                    if (pomocR < 0)
                    {
                        pomocR = 0;
                    }
                    if (pomocG < 0)
                    {
                        pomocG = 0;
                    }
                    if (pomocB < 0)
                    {
                        pomocB = 0;
                    }
                    if (pomocR > 255)
                    {
                        pomocR = 255;
                    }
                    if (pomocG > 255)
                    {
                        pomocG = 255;
                    }
                    if (pomocB > 255)
                    {
                        pomocB = 255;
                    }

                    bitmapEdited.SetPixel(j, i, Color.FromArgb((int)pomocR, (int)pomocG, (int)pomocB));
                }
            }

            pictureBox2.Image = bitmapEdited;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int k = 7;
            int r1 = 15;
            int sigma = 10;
            double suma = 0;

            macierz = new double[r1, r1];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    macierz[i, j] = (1 / (2 * Math.PI * Math.Pow(sigma, 2))) * Math.Pow(Math.E, -(Math.Pow(i - k, 2) + Math.Pow(j - k, 2)) / (2 * Math.Pow(sigma, 2)));
                    suma = suma + macierz[i, j];

                    Debug.WriteLine(macierz[i, j]);
                }
            }

            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();
            Bitmap bitmapCopy = (Bitmap)bitmap.Clone();

            double pomocR = 0, pomocG = 0, pomocB = 0;

            for (int i = 0; i < wys; i++)
            {
                for (int j = 0; j < szer; j++)
                {

                    pomocR = 0; pomocG = 0; pomocB = 0;

                    for (int f = -7; f <= 7; f++)
                    {
                        for (int l = -7; l <= 7; l++)
                        {
                            int x1 = i + f;
                            int y1 = j + l;

                            if (x1 < 0)
                            {
                                x1 = 0;
                            }
                            if (x1 >= bitmap.Height)
                            {
                                x1 = bitmap.Height - 1;
                            }
                            if (y1 < 0)
                            {
                                y1 = 0;
                            }
                            if (y1 >= bitmap.Width)
                            {
                                y1 = bitmap.Width - 1;
                            }
                            Color p = bitmapCopy.GetPixel(y1, x1);

                            double r = (double)p.R;
                            double g = (double)p.G;
                            double b = (double)p.B;

                            pomocR += r * macierz[f + k, l + k];
                            pomocG += g * macierz[f + k, l + k];
                            pomocB += b * macierz[f + k, l + k];

                        }
                    }

                    pomocR = pomocR / suma;
                    pomocG = pomocG / suma;
                    pomocB = pomocB / suma;


                    if (pomocR < 0)
                    {
                        pomocR = 0;
                    }
                    if (pomocG < 0)
                    {
                        pomocG = 0;
                    }
                    if (pomocB < 0)
                    {
                        pomocB = 0;
                    }
                    if (pomocR > 255)
                    {
                        pomocR = 255;
                    }
                    if (pomocG > 255)
                    {
                        pomocG = 255;
                    }
                    if (pomocB > 255)
                    {
                        pomocB = 255;
                    }

                    bitmapEdited.SetPixel(j, i, Color.FromArgb((int)pomocR, (int)pomocG, (int)pomocB));
                }
            }

            pictureBox2.Image = bitmapEdited;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                bitmap = (Bitmap)this.pictureBox1.Image;

                wys = pictureBox1.Image.Height;
                szer = pictureBox1.Image.Width;

                Array.Clear(R, 0, R.Length);
                Array.Clear(G, 0, G.Length);
                Array.Clear(B, 0, B.Length);

                histogram();

                K = 0;

                panel2.Invalidate();
                panel3.Invalidate();
                pictureBox2.Image = bitmap;

            }
        }
    }
}
