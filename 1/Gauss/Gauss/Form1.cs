using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gauss
{
    public partial class Form1 : Form
    {
        private PictureBox PictureBox1;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureBox1 = new PictureBox();
                    PictureBox1.Image = new Bitmap(dlg.FileName);
                    PictureBox1.Width = PictureBox1.Image.Width;
                    PictureBox1.Height = PictureBox1.Image.Height;
                    
                    Form2 f2 = new Form2();
                    f2.Width = PictureBox1.Width;
                    f2.Height = PictureBox1.Height;
                    f2.Controls.Add(PictureBox1);
                    f2.Show();
                }
            }
        }

        private void blurButton_Click(object sender, EventArgs e)
        {
            Processor proc = new Processor((Bitmap)PictureBox1.Image);
            //Bitmap bmp = proc.ProcessSeparable();
            Bitmap bmp = proc.ProcessMatrix();

            PictureBox PictureBox2 = new PictureBox();
            PictureBox2.Width = bmp.Width;
            PictureBox2.Height = bmp.Height;
            PictureBox2.Image = bmp;

            Form3 f3 = new Form3();
            f3.Width = PictureBox2.Width;
            f3.Height = PictureBox2.Height;
            f3.Controls.Add(PictureBox2);
            f3.Show();

            bmp = proc.ProcessSeparable();
            PictureBox PictureBox3 = new PictureBox();
            PictureBox3.Width = bmp.Width;
            PictureBox3.Height = bmp.Height;
            PictureBox3.Image = bmp;

            Form4 f4 = new Form4();
            f4.Width = PictureBox3.Width;
            f4.Height = PictureBox3.Height;
            f4.Controls.Add(PictureBox3);
            f4.Show();
        }
    }
}
