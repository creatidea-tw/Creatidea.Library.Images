namespace Creatidea.Library.Images.Services
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    using Creatidea.Library.Images.Enums;

    /// <summary>
    /// CiImage 公開類別.
    /// </summary>
    public class CiImage
    {
        /// <summary>
        /// 文字轉換為圖片.
        /// </summary>
        /// <param name="text">欲轉換之文字.</param>
        /// <param name="font">文字字型.</param>
        /// <param name="textColor">文字顏色.</param>
        /// <param name="backColor">背景顏色.</param>
        /// <param name="width">The default width.</param>
        /// <returns>System.String.</returns>
        public static Image TextToImage(string text, Font font, Color textColor, Color backColor, int width = 0)
        {
            // first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            // measure the string to see how big the image needs to be
            SizeF textSize = new SizeF();
            if (width != 0)
            {
                textSize = drawing.MeasureString(text, font, width);
            }
            else
            {
                textSize = drawing.MeasureString(text, font);
            }

            // free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            // create a new image of the right size
            // plus 15 prvent bottom text not being cut
            img = new Bitmap((int)textSize.Width, (int)textSize.Height + 15);

            drawing = Graphics.FromImage(img);

            // paint the background
            drawing.Clear(backColor);

            // create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, new RectangleF(5f, 10f, textSize.Width, textSize.Height));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }


        /// <summary>
        /// Thumbs the image.
        /// </summary>
        /// <param name="path">The image path.</param>
        /// <param name="size">The thumb size.</param>
        /// <param name="sameRatio">if set to <c>true</c> maintain same ratio.</param>
        /// <param name="format">The format.</param>
        /// <param name="mode">The image quality.</param>
        /// <returns>Image.</returns>
        public static Image ThumbImage(string path, Size size, bool sameRatio = true, ImageFormat format = null, ThumbQuality mode = ThumbQuality.Normal)
        {
            Image srcImage = Image.FromFile(path);

            // check is need to maintain ratio
            if (sameRatio == true)
            {
                // Figure out the ratio
                double ratioX = (double)size.Width / (double)srcImage.Width;
                double ratioY = (double)size.Height / (double)srcImage.Height;

                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                size.Width = Convert.ToInt32(srcImage.Width * ratio);
                size.Height = Convert.ToInt32(srcImage.Height * ratio);
            }

            int newWidth = size.Width;
            int newHeight = size.Height;

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                // set thumb quality
                switch (mode)
                {
                    case ThumbQuality.Best:
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        break;
                    case ThumbQuality.High:
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        break;
                    case ThumbQuality.Low:
                        gr.SmoothingMode = SmoothingMode.HighSpeed;
                        gr.InterpolationMode = InterpolationMode.Low;
                        gr.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        break;
                    default:
                        gr.SmoothingMode = SmoothingMode.Default;
                        gr.InterpolationMode = InterpolationMode.Default;
                        gr.PixelOffsetMode = PixelOffsetMode.Default;
                        break;
                }

                gr.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
            }

            // Dispose
            srcImage.Dispose();

            return newImage;
        }

        /// <summary>
        /// Thumbs the image.
        /// </summary>
        /// <param name="srcImage">The source image.</param>
        /// <param name="size">The thumb size.</param>
        /// <param name="sameRatio">if set to <c>true</c> maintain same ratio.</param>
        /// <param name="format">The format.</param>
        /// <param name="mode">The image quality.</param>
        /// <returns>Image.</returns>
        public static Image ThumbImage(Image srcImage, Size size, bool sameRatio = true, ImageFormat format = null, ThumbQuality mode = ThumbQuality.Normal)
        {
            // check is need to maintain ratio
            if (sameRatio == true)
            {
                // Figure out the ratio
                double ratioX = (double)size.Width / (double)srcImage.Width;
                double ratioY = (double)size.Height / (double)srcImage.Height;

                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                size.Width = Convert.ToInt32(srcImage.Width * ratio);
                size.Height = Convert.ToInt32(srcImage.Height * ratio);
            }

            int newWidth = size.Width;
            int newHeight = size.Height;

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                // set thumb quality
                switch (mode)
                {
                    case ThumbQuality.Best:
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        break;
                    case ThumbQuality.High:
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        break;
                    case ThumbQuality.Low:
                        gr.SmoothingMode = SmoothingMode.HighSpeed;
                        gr.InterpolationMode = InterpolationMode.Low;
                        gr.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        break;
                    default:
                        gr.SmoothingMode = SmoothingMode.Default;
                        gr.InterpolationMode = InterpolationMode.Default;
                        gr.PixelOffsetMode = PixelOffsetMode.Default;
                        break;
                }

                gr.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
            }

            // Dispose
            srcImage.Dispose();

            return newImage;
        }
    }
}
