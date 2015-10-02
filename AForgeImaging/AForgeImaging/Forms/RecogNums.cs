using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AForgeImaging.Forms
{
    public partial class RecogNums : Form
    {
        public RecogNums()
        {
            InitializeComponent();
        }

        private void RecogNums_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = global::AForgeImaging.Properties.Resources._0;
            pictureBox2.Image = global::AForgeImaging.Properties.Resources._0;
            
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            #region switch
            switch (comboBox1.SelectedItem.ToString())
            {
                case "0":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._0;
                    break;
                case "1":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._1;
                    break;
                case "2":
                    this.pictureBox2.Image = global::AForgeImaging.Properties.Resources._2;
                    break;
                case "3":
                    this.pictureBox2.Image = global::AForgeImaging.Properties.Resources._3;
                    break;
                case "4":
                    this.pictureBox2.Image = global::AForgeImaging.Properties.Resources._4;
                    break;
                case "5":
                    this.pictureBox2.Image = global::AForgeImaging.Properties.Resources._5;
                    break;
                case "6":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._6;
                    break;

                case "7":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._7;
                    break;
                case "8":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._8;
                    break;
                case "9":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources._9;
                    break;
                case "dot":
                    pictureBox2.Image = global::AForgeImaging.Properties.Resources.dot;
                    break;
                default:
                    break;
            }
            #endregion
 
            Bitmap b = imgOpPB_2();
            b = imgCut(b);
            pictureBox1.Image = b;

            dealpb_1(b);

        }

        private Bitmap imgOpPB_2()
        {
            Bitmap b = new Bitmap(pictureBox2.Image.Width, pictureBox2.Image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(pictureBox2.Image, 0, 0);
            g.Dispose();

            
            //b = new Grayscale(0.299, 0.587, 0.114).Apply(b);
            //b = new Threshold(30).Apply(b);
            //b = new BlobsFiltering(1, 1, b.Width, b.Height).Apply(b);

            return b;
        }

        private void dealpb_1(Bitmap machImg)
        {

            Bitmap[] files = {
               global::AForgeImaging.Properties.Resources._0,
               global::AForgeImaging.Properties.Resources._1,
               global::AForgeImaging.Properties.Resources._2,
               global::AForgeImaging.Properties.Resources._3,
               global::AForgeImaging.Properties.Resources._4,
               global::AForgeImaging.Properties.Resources._5,
               global::AForgeImaging.Properties.Resources._6,
               global::AForgeImaging.Properties.Resources._7,
               global::AForgeImaging.Properties.Resources._8,
               global::AForgeImaging.Properties.Resources._9,
               global::AForgeImaging.Properties.Resources.dot
             };

            ExhaustiveTemplateMatching templateMatching = new ExhaustiveTemplateMatching(0.9f);
            
            float max = 0;
            int index = 0;
            for (int i = 0; i < files.Length; i++)
            {
                Bitmap newb = imgCut( files[i]);
                var compare = templateMatching.ProcessImage(machImg, newb);
                if (compare.Length > 0 && compare[0].Similarity > max)
                {
                    //记录下最相似的
                    max = compare[0].Similarity;
                    index = i;
                    
                }

            }

            label3.Text = index.ToString();
        }//private void dealpb_1

        private Bitmap imgCut(Bitmap cutthis)
        {
            Bitmap b = new Bitmap(15, 20, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.DrawImage(cutthis, 0, 0);
            b = new Grayscale(0.299, 0.587, 0.114).Apply(b);
            b = new Sharpen().Apply(b);
            b = new Sharpen().Apply(b);
            b = new Threshold(50).Apply(b);

            
            return b;
        }
            
    }

}
