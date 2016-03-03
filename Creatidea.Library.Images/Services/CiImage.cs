namespace Creatidea.Library.Images.Services
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Linq;

    using Creatidea.Library.Configs;
    using Creatidea.Library.Images.Enums;
    using Creatidea.Library.Results;

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
        /// 從CiConfig讀取CiImage.Size或CiImage.FitSize設定作為依據並縮圖
        /// </summary>
        /// <param name="path">The image path.</param>
        /// <param name="mode">縮圖品質.</param>
        /// <returns>Image.</returns>
        public static CiResult<Image> ThumbImage(string path, ThumbQuality mode = ThumbQuality.Normal)
        {
            var result = new CiResult<Image>() { Message = "縮圖發生錯誤！" };

            if (CiConfig.Global.CiImage.Size != null && CiConfig.Global.CiImage.Size.ToString() != "0")
            {
                int size;
                if (int.TryParse(CiConfig.Global.CiImage.Size.ToString(), out size))
                {
                    return ThumbImage(path, size, mode);
                }
            }

            if (CiConfig.Global.CiImage.FitSize != null)
            {
                string tmpSizeString = CiConfig.Global.CiImage.FitSize.ToString();
                string[] tmpSizeArray = tmpSizeString.Split(',');
                if (tmpSizeArray.Count() != 2)
                {
                    result.Message += "無法讀取縮圖尺寸設定檔！FitSize格式錯誤！";
                    return result;
                }

                int sizeX, sizeY;
                if (!int.TryParse(tmpSizeArray[0], out sizeX) || !int.TryParse(tmpSizeArray[0], out sizeY))
                {
                    result.Message += "無法讀取縮圖尺寸設定檔！FitSize格式錯誤！";
                    return result;
                }

                Size fitSize = new Size() { Width = sizeX, Height = sizeY };
                return ThumbImage(path, fitSize, false, mode);
            }

            result.Message += "無法讀取縮圖尺寸設定檔！";
            return result;
        }

        /// <summary>
        /// 傳入最大邊長度作為依據並縮圖且一定會維持比例
        /// </summary>
        /// <param name="path">The image path.</param>
        /// <param name="size">最大邊長度.</param>
        /// <param name="mode">縮圖品質.</param>
        /// <returns>Image.</returns>
        public static CiResult<Image> ThumbImage(string path, int size, ThumbQuality mode = ThumbQuality.Normal)
        {
            return ThumbImage(path, new Size(size, size), true, mode);
        }

        /// <summary>
        /// 傳入指定大小作為依據並縮圖
        /// </summary>
        /// <param name="path">完整圖片檔路徑.</param>
        /// <param name="size">The thumb size.</param>
        /// <param name="sameRatio">if set to <c>true</c> 維持長寬比.</param>
        /// <param name="mode">縮圖品質.</param>
        /// <returns>Image.</returns>
        public static CiResult<Image> ThumbImage(string path, Size size, bool sameRatio = true, ThumbQuality mode = ThumbQuality.Normal)
        {
            var result = new CiResult<Image>() { Message = "縮圖發生錯誤！" };

            // 讀取檔案
            Image srcImage;
            try
            {
                srcImage = Image.FromFile(path);
            }
            catch (Exception ex)
            {
                result.Message += string.Format("無法讀取圖片檔案！路徑：{0}，錯誤訊息：{1}！", path, ex.ToString());
                return result;
            }

            return ThumbImage(srcImage, size, sameRatio, mode);
        }

        /// <summary>
        /// 傳入<see cref="Image"/>後依據指定大小縮圖.
        /// </summary>
        /// <param name="srcImage">The source image.</param>
        /// <param name="size">The thumb size.</param>
        /// <param name="sameRatio">if set to <c>true</c> maintain same ratio.</param>
        /// <param name="mode">The image quality.</param>
        /// <returns>Image.</returns>
        public static CiResult<Image> ThumbImage(Image srcImage, Size size, bool sameRatio = true, ThumbQuality mode = ThumbQuality.Normal)
        {
            var result = new CiResult<Image>() { Message = "縮圖發生錯誤！" };

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

            result.Success = true;
            result.Data = newImage;
            result.Message = "縮圖成功";

            return result;
        }
    }
}
