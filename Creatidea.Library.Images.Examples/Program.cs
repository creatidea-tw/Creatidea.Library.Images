namespace Creatidea.Library.Images.Examples
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    using Creatidea.Library.Configs;
    using Creatidea.Library.Images.Enums;
    using Creatidea.Library.Images.Services;

    /// <summary>
    /// 測試範例.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            ShowTextImage();

            ShowThumb();
        }

        /// <summary>
        /// Shows the thumb image.
        /// </summary>
        private static void ShowThumb()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("Thumb Image Example:");
            Console.WriteLine();

            string appDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(appDirectory, "Images", "Demo.png");

            // 讀取設定檔，首先會尋找 Size，若找不到再尋找 FitSize，若找到任何一屬性則自動停止尋找
            // Size會維持原比例，FitSize不會維持原比例
            Console.WriteLine("Method1: Read size config from CiImage config file");
            var result1 = CiImage.Thumb(path);
            if (!result1.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result1.Message);
            }
            else
            {
                var link = SaveImage(result1.Data, ImageFormat.Jpeg);
                Console.WriteLine("Show result1: {0}", link);
            }

            // 直接傳入指定大小給縮圖方法
            Console.WriteLine("Method2: Direct assign size to method");
            var result2 = CiImage.Thumb(path, 300);
            if (!result2.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result2.Message);
            }
            else
            {
                var link = SaveImage(result2.Data, ImageFormat.Jpeg);
                Console.WriteLine("Show result2: {0}", link);
            }

            // 直接傳入指定寬與高給方法，並可控制是否要維持比例，預設為 true
            Console.WriteLine("Method3: Direct assign fitSize to method");
            Size size = new Size(500, 500);
            var result3 = CiImage.Thumb(path, size);
            if (!result3.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result3.Message);
            }
            else
            {
                var link = SaveImage(result3.Data, ImageFormat.Jpeg);
                Console.WriteLine("Show result3: {0}", link);
            }

            // 每個縮圖方法都可以指定縮圖品質
            Console.WriteLine("Method4: Read size config from CiImage config file");
            var result4 = CiImage.Thumb(path, ThumbQuality.Best);
            if (!result4.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result4.Message);
            }
            else
            {
                var link = SaveImage(result4.Data, ImageFormat.Jpeg);
                Console.WriteLine("Show result4: {0}", link);
            }
        }

        /// <summary>
        /// Shows the text to image.
        /// </summary>
        private static void ShowTextImage()
        {
            string txt = CiConfig.Global.CiConfig.Text;

            Font font = new Font("Microsoft JhengHei", 20);
            Color txtColor = Color.Bisque;
            Color backColor = Color.DarkBlue;

            var result4 = CiImage.TextImage(txt, font, txtColor, backColor, 500);
            if (!result4.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result4.Message);
            }
            else
            {
                var link = SaveImage(result4.Data, ImageFormat.Jpeg);
                Console.WriteLine("ShowTextImage: {0}", link);
            }
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.String.</returns>
        private static string SaveImage(Image image, ImageFormat format)
        {
            string appDirectory = Directory.GetCurrentDirectory();
            string fileName = DateTime.Now.Ticks + "." + format.ToString().ToLower();
            if (!Directory.Exists(Path.Combine(appDirectory, "Temps")))
            {
                Directory.CreateDirectory(
                    Path.Combine(appDirectory, "Temps"));
            }

            image.Save(Path.Combine(appDirectory, "Temps", fileName), format);

            return fileName;
        }
    }
}
