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

                    // Call on the sliding window function
                    slideWindow(selectedImage);

                    // Present the results
                    stencilBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    stencilBox.Image = stencil;

                    imageBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    imageBox.Image = selectedImage;                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
