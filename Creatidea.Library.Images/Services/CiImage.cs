namespace Creatidea.Library.Images.Services
{
    using System.Drawing;

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
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            // paint the background
            drawing.Clear(backColor);

            // create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, new RectangleF(5f, 10f, textSize.Width - 5, textSize.Height - 10));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
