using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<(int, int), Color> dict = new Dictionary<(int, int), Color>();
            Bitmap image = Properties.Resources.MarioBackground;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if(j != image.Height - 1 && i != 0)
                    {

                        Color currColor;
                        if (!dict.ContainsKey((i, j)))
                        {
                            currColor = image.GetPixel(i, j);
                            dict.Add((i, j), currColor);
                        }
                        else
                        {
                            currColor = dict[(i, j)];
                        }
                        Color otherColor;
                        if (!dict.ContainsKey((i-1, j+1)))
                        {
                            otherColor = image.GetPixel(i-1, j+1);
                            dict.Add((i-1, j+1), otherColor);
                        }
                        else
                        {
                            otherColor = dict[(i-1, j+1)];
                        }

                        Color col = Color.FromArgb(max(0,currColor.A - otherColor.A), 255- max(0, currColor.R - otherColor.R), 255- max(0,currColor.G - otherColor.G), 255-max(0,currColor.B - otherColor.B));//White black lines
                        //Color col = Color.FromArgb(max(0,currColor.A - otherColor.A), max(0, currColor.R - otherColor.R), max(0,currColor.G - otherColor.G), max(0,currColor.B - otherColor.B));//Black White lines
                        //Color col = Color.FromArgb(Math.Abs(currColor.A - otherColor.A), Math.Abs(currColor.R - otherColor.R), Math.Abs(currColor.G - otherColor.G), Math.Abs(currColor.B - otherColor.B));
                        image.SetPixel(i, j, col);
                    }
                    else
                    {
                        //image.SetPixel(i, j, Color.Black); Black with white lines
                        image.SetPixel(i, j, Color.White);//White with black lines 
                    }
                }
            }
            pictureBox2.Image = image;
        }

        int max(int first, int second)
        {
            return first>second ? first:  second;
        }
        Random gen = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap img = Properties.Resources.MarioBackground;
            int.TryParse(textBox1.Text, out int result);
            result = result <= 2 ? 2 : result;
            int.TryParse(textBox2.Text, out int resultColor);
            resultColor = resultColor <= 0 ? 1 : resultColor > 255 ? 255 : resultColor  ;


            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color c = img.GetPixel(i, j);
                    int a = gen.Next(0, result) == 0 ? gen.Next(0, resultColor) : c.A;
                    int r = gen.Next(0, result) == 0 ? gen.Next(0, resultColor) : c.R;
                    int g = gen.Next(0, result) == 0 ? gen.Next(0, resultColor) : c.G;
                    int b = gen.Next(0, result) == 0 ? gen.Next(0, resultColor) : c.B;
                    Color col = Color.FromArgb(a, r, g, b); 
                    img.SetPixel(i, j, col);
                }
            }
            pictureBox3.Image = img;
        }

        
    }
}
