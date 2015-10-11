
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using AForge.Imaging;
using System.Drawing;
using AForge.Imaging.Filters;
using AForge;


namespace GetCutImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            button2_Click(sender, e);
            //pictureBox1.Image = DrowRedline(pictureBox1.Image);
            button3_Click(sender, e);
        }

        public Bitmap[] bs;
        private void button3_Click(object sender, EventArgs e)
        {
            //Bitmap b = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = pictureBox2.Image;
            bs = CutImg(pictureBox3.Image);

            /*for (int i = 0; i < bs.Length; i++)
            {
                imageList1.Images.Add(i.ToString(), bs[i]);
            }*/
           

            pictureBox4.Image = bs[Selectedbmp];

        }
        private int Selectedbmp = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            Selectedbmp++;
            if(Selectedbmp >= bs.Length)
            {
                Selectedbmp = 0;
            }
            //MessageBox.Show("val:"+ Selectedbmp);
            pictureBox4.Image = bs[Selectedbmp];
        }

        public Bitmap[] CutImg(System.Drawing.Image b)
        {

            try
            {
                Bitmap bt = new Bitmap(b);
                //MessageBox.Show(bt.Size.ToString());
                int Y = bt.Size.Height;
                int X = bt.Size.Width;
                Graphics g = Graphics.FromImage(bt);
                Pen mypen = new Pen(Color.Red);

                List<int> loca = new List<int>();
                List<int> cutloca = new List<int>();
                int cal = 0;
                int backup = 0;
                for (int i = 0; i < X; i++)
                {
                    int numcnt = 0;

                    for (int j = 0; j < Y; j++)
                    {

                        if (bt.GetPixel(i, j).R != 255)
                        {
                            // MessageBox.Show("(i,j):" + i + "," + j + ";" + bt.GetPixel(i, j).ToString());                        
                            numcnt++;
                            if (numcnt > 2)
                            {
                                if (backup + 1 != i)
                                {
                                    loca.Add(0);
                                    cal = (backup + i) / 2;
                                    //g.DrawLine(mypen, cal, 0, cal, Y);
                                    cutloca.Add(cal);
                                }
                                backup = i;

                                loca.Add(i);
                                break;
                            }

                        }
                    }
                }//for

                cal = (loca.Last() + X) / 2;
                cutloca.Add(cal);

                int[] locas_a = cutloca.ToArray();
                Rectangle[] rects = new Rectangle[locas_a.Length - 1];
                Bitmap[] ans = new Bitmap[locas_a.Length - 1];
                for (int i = 0; i < locas_a.Length - 1; i++)
                {
                    rects[i] = new Rectangle(locas_a[i], 0, locas_a[i+1] - locas_a[i], Y - 1);
                    g.DrawRectangle(mypen, rects[i]);
                    ans[i] = bt.Clone(rects[i], PixelFormat.Format32bppArgb);
                }

                //Rectangle rect = new Rectangle(locas_a[0], 0, locas_a[1] - locas_a[0], Y-1);

                //pictureBox2.Image = bt.Clone(rects[0], PixelFormat.Format32bppArgb);
                pictureBox3.Image = bt;

                
                return ans;

            }
            catch (Exception)
            {

                throw;
            }
            //return null;
        }


        #region Load and Deal image

        private void button1_Click(object sender, EventArgs e)
        {
            string ImgUrl = "http://www.f02.cn/image.jsp?time=1443711845988";
            pictureBox1.Image = LoadImg(ImgUrl);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            pictureBox2.Image = DealImg(pictureBox2.Image);
            
            //can not convert a System.Drawing.Image to System.Drawing.Bitmap
        }

        public Bitmap DrowRedline(System.Drawing.Image b)
        {
            //Bitmap[] imgs = new Bitmap[];


            try
            {
                Bitmap bt = new Bitmap(b);
                MessageBox.Show(bt.Size.ToString());
                int Y = bt.Size.Height;
                int X = bt.Size.Width;
                Graphics g = Graphics.FromImage(bt);
                Pen mypen = new Pen(Color.Red);

                List<int> loca = new List<int>();
                int backup = 0;
                for (int i = 0; i < X; i++)
                {
                    int numcnt = 0;
                    for (int j = 0; j < Y; j++)
                    {

                        if (bt.GetPixel(i, j).R != 255)
                        {
                            // MessageBox.Show("(i,j):" + i + "," + j + ";" + bt.GetPixel(i, j).ToString());                        
                            numcnt++;
                            if (numcnt > 2)
                            {
                                if (backup + 1 != i)
                                {
                                    loca.Add(0);
                                    int cal = (backup + i) / 2;
                                    g.DrawLine(mypen, cal, 0, cal, Y);
                                }
                                backup = i;

                                loca.Add(i);
                                break;
                            }

                        }
                    }
                }//for
                int calcu = (loca.Last() + X) / 2;
                g.DrawLine(mypen, calcu, 0, calcu, Y);


                String str = "";

                foreach (int ele in loca)
                {
                    str += ele + " ";
                }

                MessageBox.Show(str);
                #region those code sucks
                       /*         
                BlobCounter extractor = new BlobCounter();
                extractor.FilterBlobs = true;
                extractor.MinWidth = 9;
                extractor.MinHeight =9;
                extractor.MaxWidth = 17;
                extractor.MaxHeight = 16;
                extractor.ProcessImage(bt);

                Rectangle[] rects = extractor.GetObjectsRectangles();
                
                // create an instance of blob counter algorithm
                //BlobCounter bc = new BlobCounter();
                // process binary image
               // bc.ProcessImage(bt);
                //Rectangle[] rects = bc.GetObjectsRectangles();
                // process blobs
               
                //Graphics g = Graphics.FromImage(bt);
                //Pen mypen = new Pen(Color.Red);

                foreach (Rectangle rect in rects)
                {
                    //MessageBox.Show("x:"+ rect.X + "y: "+  rect.Y );
                    g.DrawRectangle(mypen, rect);
                }*/
                #endregion


                return bt;



            }
            catch (Exception)
            {

                throw;
            }
            //return null;
        }

        public Bitmap DealImg(System.Drawing.Image b)
        {
            try
            {
                /*var bnew = new Bitmap(b.Width, b.Height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bnew);
                g.DrawImage(b, 0, 0);
                g.Dispose();

                bnew = new Grayscale(0.2125, 0.7154, 0.0721).Apply(bnew);
                bnew = new BlobsFiltering(1, 1, b.Width, b.Height).Apply(bnew);
                bnew = new Sharpen().Apply(bnew);
                bnew = new Threshold(50).Apply(bnew);
                */
                //code above do not effect well here.

                Bitmap bnew = new Bitmap(b);
                Graphics g = Graphics.FromImage(bnew);
                g.DrawImage(b, 0, 0);
                g.Dispose();
                FiltersSequence seq = new FiltersSequence();
                seq.Add(Grayscale.CommonAlgorithms.BT709);
                seq.Add(new OtsuThreshold());
                bnew = seq.Apply(bnew);


                return bnew;
            }
            catch (Exception)
            {

                throw;
            }
            //return null;
        }

        public Bitmap LoadImg(string ImageUrl)
        {
            try
            {
                HttpWebRequest wrq = (HttpWebRequest)WebRequest.Create(ImageUrl);//请求的URL
                wrq.Method = "GET";
                wrq.Timeout = 5000;
                wrq.ContentType = "application/x-www-form-urlencoded";

                //获取返回资源
                HttpWebResponse response = (HttpWebResponse)wrq.GetResponse();

                //获取流
                Bitmap bt = Bitmap.FromStream(response.GetResponseStream()) as Bitmap;
                return bt;
            }
            catch
            {
                return null;
            }
            //return null;
        }

        #endregion

        
    }
}
