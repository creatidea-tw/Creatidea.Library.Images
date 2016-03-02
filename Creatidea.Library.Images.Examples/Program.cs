using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Images.Examples
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

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
            //ShowTextToImage();

            ShowThumbImage();
        }

        /// <summary>
        /// Shows the thumb image.
        /// </summary>
        private static void ShowThumbImage()
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
            var result1 = CiImage.ThumbImage(path);
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
            var result2 = CiImage.ThumbImage(path, 300);
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
            var result3 = CiImage.ThumbImage(path, size);
            if (!result3.Success)
            {
                Console.WriteLine("發生錯誤：{0}", result3.Message);
            }
            else
            {
                var link = SaveImage(result3.Data, ImageFormat.Jpeg);
                Console.WriteLine("Show result3: {0}", link);
            }
        }

        /// <summary>
        /// Shows the text to image.
        /// </summary>
        private static void ShowTextToImage()
        {
            string txt =
                @"德那這意時羅不爸音天展慢聲子，常著會業氣歌，告化成出形與果的如一光，活定訴，活氣爸、爭現不國腳！

水進因們濟時，接定不康期看觀記係河，人演把什集東傳標天由大；爸念節正星面，景腦時小因論急的上口性二等顯大先上。

賽常速知們、刻和毛未道還非。做背黃的人口時難持他太快，強千能美王部為可歷形一假，帶而人顧見先陽深停師工動安去年我中出辦這是般連最間機所通談她外線嗎正兒小為子她？論北今職：帶發外念在！風成題自學；作壓新領、方重孩到，喜年不同發斷，於士皮件明全益第我無我看為了聲發黃回看能進價重教自馬的性製母使何正切，規能讓盡臺導位片率吃會記財來有界、一際情車認間行獎八整原；服型物開人時乎生時了不王主因同通懷慢想個業式！重最消內的分機發其廣：有時來面者不在，病說起色新近。

太了孩生！

四因自的了況們有照我活觀合遊果定：竟年不我公說和並意長上小適大過念化為子試自顯但引張積式解人畫品，道成總食勢國為不黑火是？汽完車，那離藝己賽然顧制那出。";

            Font font = new Font("Microsoft JhengHei", 20);
            Color txtColor = Color.Bisque;
            Color backColor = Color.DarkBlue;

            var img = CiImage.TextToImage(txt, font, txtColor, backColor, 500);

            string link = SaveImage(img, ImageFormat.Gif);
            Console.WriteLine("ShowTextToImage: " + link);
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
