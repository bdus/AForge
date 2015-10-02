using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AForgeImaging
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2_Click(sender, e);
            button1_Click(sender, e);
            button4_Click(sender, e);
            button4_Click(sender, e);

            button3_Click(sender, e);
            button2_Click(sender, e);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //get image in pb_1 and convert to Format24bppRgb
            Bitmap b = new Bitmap(pictureBox1.Image);
            Bitmap newb = new Bitmap(b.Width, b.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(newb);
            g.DrawImage(b, 0, 0);
            g.Dispose();
            //b = new Grayscale(0.7, 0.2, 0.1).Apply(b);
            //            b = new Grayscale(0.299, 0.587, 0.114).Apply(b);

            this.pictureBox2.Image = newb;


        }

        private void button1_Click(object sender, EventArgs e)
        {            
            //Grayscale
            Bitmap b = new Bitmap(pictureBox2.Image);
            b = new Grayscale(0.299, 0.587, 0.114).Apply(b);

            this.pictureBox2.Image = b;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox2.Image);
//            b = new Grayscale(0.7, 0.2, 0.1).Apply(b);
            b = new Grayscale(0.299, 0.587, 0.114).Apply(b);

            b = new Threshold(50).Apply(b);

            this.pictureBox2.Image = b;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox2.Image);
            b = new BlobsFiltering(2, 2, b.Width, b.Height).Apply(b);
            b = new BlobsFiltering(1, 2, b.Width, b.Height).Apply(b);
            b = new BlobsFiltering(1, 1, b.Width, b.Height).Apply(b);
            //b = new BlobsFiltering(10,10, b.Width, b.Height).Apply(b);

            this.pictureBox2.Image = b;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox2.Image);
            b = new Sharpen().Apply(b);

            this.pictureBox2.Image = b;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Forms.RecogNums newRecog = new Forms.RecogNums();
            newRecog.Show();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            #region switch
            switch (comboBox1.SelectedItem.ToString())
            {
                case "0":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._0;
                    break;
                case "1":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._1;
                    break;
                case "2":
                    this.pictureBox1.Image = global::AForgeImaging.Properties.Resources._2;
                    break;
                case "3":
                    this.pictureBox1.Image = global::AForgeImaging.Properties.Resources._3;
                    break;
                case "4":
                    this.pictureBox1.Image = global::AForgeImaging.Properties.Resources._4;
                    break;
                case "5":
                    this.pictureBox1.Image = global::AForgeImaging.Properties.Resources._5;
                    break;
                case "6":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._6;
                    break;

                case "7":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._7;
                    break;
                case "8":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._8;
                    break;
                case "9":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources._9;
                    break;
                case "dot":
                    pictureBox1.Image = global::AForgeImaging.Properties.Resources.dot;
                    break;
                default:
                    break;
            }
            #endregion
 
        }


    }
}
