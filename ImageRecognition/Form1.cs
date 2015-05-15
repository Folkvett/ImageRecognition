using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageRecognition
{
    public partial class Form1 : Form
    {
        private Bitmap stencil;
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap applyGaussianFilter(Bitmap image)
        {
            Bitmap blurredImage = new Bitmap(image);

            double weight = 5.0;
            double[,] kernel = new double[3, 3];

            int kernelRadius = 1;
            double Euler = 1.0 / (2.0 * Math.PI * Math.Pow(weight, 2));
            double distance = 0.0;

            double sumTotal = 0;

            // Set up our kernel
            for (int kernelX = -kernelRadius; kernelX <= kernelRadius; ++kernelX)
            {
                for (int kernelY = -kernelRadius; kernelY <= kernelRadius; ++kernelY)
                {
                    distance = ((kernelX*kernelX) + (kernelY*kernelY))/ (2 * (weight*weight));

                    // Since kernelX starts at -kernelRadius we normalize by adding kernelRadius
                    kernel[kernelX + kernelRadius, kernelY + kernelRadius] = Euler * Math.Exp(-distance);

                    sumTotal += kernel[kernelX + kernelRadius, kernelY + kernelRadius];
                }
            }

            // Normalize the kernel so all elements together add up to 1
            for (int y = 0; y < 3; ++y)
                for (int x = 0; x < 3; ++x)
                    kernel[x, y] = kernel[x, y] * (1.0 / sumTotal);

            // Start applying the filter
            double red = 0.0;
            double green = 0.0;
            double blue = 0.0;
            for (int y = 0 + kernelRadius; y < image.Height - kernelRadius; ++y)
            {
                for (int x = 0 + kernelRadius; x < image.Width - kernelRadius; ++x)
                {
                    red = 0.0;
                    green = 0.0;
                    blue = 0.0;

                    // Apply the kernel for the pixel at X, Y
                    for (int filterX = -kernelRadius; filterX <= kernelRadius; ++filterX)
                    {
                        for (int filterY = -kernelRadius; filterY <= kernelRadius; ++filterY)
                        {
                            red += image.GetPixel(x + filterX, y + filterY).R * kernel[filterX + kernelRadius, filterY + kernelRadius];
                            green += image.GetPixel(x + filterX, y + filterY).G * kernel[filterX + kernelRadius, filterY + kernelRadius];
                            blue += image.GetPixel(x + filterX, y + filterY).B * kernel[filterX + kernelRadius, filterY + kernelRadius];
                        }
                    }

                    blue = (blue > 255 ? 255 : (blue < 0 ? 0 : blue));
                    green = (green > 255 ? 255 : (green < 0 ? 0 : green));
                    red = (red > 255 ? 255 : (red < 0 ? 0 : red));

                    blurredImage.SetPixel(x, y, Color.FromArgb((int)red, (int)green, (int)blue));
                }
            }

            return blurredImage;
        }

        // Create a sliding window, a miniature image consisting of only a few pixels within
        // the full image
        private void slideWindow(Bitmap image)
        {
            stencil = new Bitmap(image);
            for (int y = 0; y < stencil.Height; ++y)
                for (int x = 0; x < stencil.Width; ++x)
                    stencil.SetPixel(x, y, Color.White);

            Color[,] window = new Color[3,3];

            // Get pixels for the 3x3 window, and slide it one pixel at a time
            for (int y = 0; y < image.Height-3; ++y)
            {
                for (int x = 0; x < image.Width-3; ++x)
                {
                    window[0, 0] = image.GetPixel(x, y);
                    window[1, 0] = image.GetPixel(x+1, y);
                    window[2, 0] = image.GetPixel(x+2, y);

                    window[0, 1] = image.GetPixel(x, y+1);
                    window[1, 1] = image.GetPixel(x+1, y+1);
                    window[2, 1] = image.GetPixel(x+1, y+1);

                    window[0, 2] = image.GetPixel(x, y+2);
                    window[1, 2] = image.GetPixel(x+1, y+2);
                    window[2, 2] = image.GetPixel(x+2, y+2);

                    // The two current checks, look for difference in brightness in horizontal ...
                    if(Math.Max(window[0, 0].GetBrightness(), window[2,0].GetBrightness()) -0.1 >
                        Math.Min(window[0,0].GetBrightness(), window[2, 0].GetBrightness()) &&

                        Math.Max(window[0, 1].GetBrightness(), window[2,1].GetBrightness()) -0.1 >
                        Math.Min(window[0,1].GetBrightness(), window[2, 1].GetBrightness()) &&

                        Math.Max(window[0, 2].GetBrightness(), window[2,2].GetBrightness()) -0.1 >
                        Math.Min(window[0,2].GetBrightness(), window[2, 2].GetBrightness()))
                    {
                       stencil.SetPixel(x + 1, y + 1, Color.Black);
                    }

                    // .. and in vertical, if there is a difference, mark the "pivot" black in the stencil.
                    if (Math.Max(window[0, 0].GetBrightness(), window[0, 2].GetBrightness()) - 0.1 >
                        Math.Min(window[0, 0].GetBrightness(), window[0, 2].GetBrightness()) &&

                        Math.Max(window[1, 0].GetBrightness(), window[1, 2].GetBrightness()) - 0.1 >
                        Math.Min(window[1, 0].GetBrightness(), window[1, 2].GetBrightness()) &&

                        Math.Max(window[2, 0].GetBrightness(), window[2, 2].GetBrightness()) - 0.1 >
                        Math.Min(window[2, 0].GetBrightness(), window[2, 2].GetBrightness()))
                    {
                        stencil.SetPixel(x + 1, y + 1, Color.Black);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectImageDialog = new OpenFileDialog();

            selectImageDialog.InitialDirectory = "c:\\Users\\";
            selectImageDialog.Filter = "image files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            if (selectImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create a bitmap image from the selected file
                    Bitmap selectedImage = (Bitmap)Image.FromFile(selectImageDialog.FileName, true);
                    
                    btnSelectImage.Text = selectImageDialog.FileName;

                    Bitmap blurredImage = applyGaussianFilter(selectedImage);

                    // Call on the sliding window function
                    slideWindow(selectedImage);

                    // Present the results
                    stencilBox.SizeMode = PictureBoxSizeMode.Zoom;
                    stencilBox.Image = stencil;

                    imageBox.SizeMode = PictureBoxSizeMode.Zoom;
                    imageBox.Image = selectedImage;

                    blurredBox.SizeMode = PictureBoxSizeMode.Zoom;
                    blurredBox.Image = blurredImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
