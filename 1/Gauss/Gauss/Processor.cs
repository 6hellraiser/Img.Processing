using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    public class Processor
    {
        private Bitmap img;
        public Processor(Bitmap image)
        {
            img = new Bitmap(image);
        }

        public Bitmap ProcessMatrix() {
            int[,] mask = new int[3, 3] {{1,2,1},
                                         {2,4,2},
                                         {1,2,1}}; // divide by 16
            Bitmap output = new Bitmap(img.Width, img.Height);
            const int divider = 16;
            const int sigma = 1;
            Color p;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (x >= sigma && y >= sigma && x < img.Width - sigma && y < img.Height - sigma)
                    {
                        double sumR = 0; double sumG = 0; double sumB = 0;
                        for (int i = x - sigma; i <= x + sigma; i++)
                        {
                            for (int j = y - sigma; j <= y + sigma; j++)
                            {
                                //sum += img[i,j]*mask[sigma+i-x, sigma+j-y]
                                p = img.GetPixel(i, j);
                                sumR += p.R * mask[sigma + i - x, sigma + j - y];
                                sumG += p.G * mask[sigma + i - x, sigma + j - y];
                                sumB += p.B * mask[sigma + i - x, sigma + j - y];
                            }
                        }
                        sumR /= divider; sumG /= divider; sumB /= divider;
                        Color clr = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                        output.SetPixel(x, y, clr); 
                    }
                }
            }
               
            return output;
        }

        public Bitmap ProcessSeparable()
        {
            const int sigma = 2;
            Bitmap output = new Bitmap(img.Width, img.Height);
            double s2 = 2 * Math.Pow(sigma, 2);
            const int N = 3 * sigma;
            const int size = 2 * N + 1;
            //const int size = 3;
            double[] window = new double[size];
            int plus = size / 2 + 1;
            int minus = size / 2 - 1;
            window[size / 2] = 1;
            int cnt = 1;
            while (plus < size)
            {
                window[plus] = (Math.Exp(-cnt * cnt / s2)) / sigma / Math.Sqrt(2 * Math.PI);
                window[minus] = (Math.Exp(-cnt * cnt / s2)) / sigma / Math.Sqrt(2 * Math.PI);
                plus++; minus--; cnt++;
            }
            double sum = 0;
            for (int k = 0; k < size; k++) {
                sum += window[k];
            }

            //horizontal blur
            double pixG, pixR, pixB;
            Color p;
            int l;
            Bitmap tmpX = new Bitmap(output.Width, 1);
            for (int y = 0; y < output.Height; y++)
            {
                for (int x = 0; x < output.Width; x++)
                {
                    pixR = 0; pixG = 0; pixB = 0;
                    for (int k = 0; k < size; k++)
                    {
                        l = x + k;
                        if ((l >= 0) && (l < output.Width))
                        {
                            p = img.GetPixel(l, y);
                            pixR += p.R * window[k];
                            pixG += p.G * window[k];
                            pixB += p.B * window[k];
                        }
                    }
                    pixR /= sum; pixG /= sum; pixB /= sum;
                    Color clr = Color.FromArgb((int)pixR,(int)pixG,(int)pixB);
                    tmpX.SetPixel(x, 0, clr);
                }
                for (int x = 0; x < output.Width; x++) 
                {
                    output.SetPixel(x, y, tmpX.GetPixel(x, 0));
                }
            }

            //vertical blur
            Bitmap tmpY = new Bitmap(1, output.Height);
            for (int x = 0; x < output.Width; x++)
            {
                for (int y = 0; y < output.Height; y++)
                {
                    pixR = 0; pixG = 0; pixB = 0;
                    for (int k = 0; k < size; k++)
                    {
                        l = y + k;
                        if ((l >= 0) && (l < output.Height))
                        {
                            p = img.GetPixel(x, l);
                            pixR += p.R * window[k];
                            pixG += p.G * window[k];
                            pixB += p.B * window[k];
                        }
                    }
                    pixR /= sum; pixG /= sum; pixB /= sum;
                    Color clr = Color.FromArgb((int)pixR, (int)pixG, (int)pixB);
                    tmpY.SetPixel(0, y, clr);
                }
                for (int y = 0; y < output.Height; y++)
                {
                    output.SetPixel(x, y, tmpY.GetPixel(0, y));
                }
            }
            
            return output;
        }
    }
}
