using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Images.Examples
{
    using System.Drawing;
    using System.Drawing.Imaging;

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
            string txt =
                @"先不管大家認不認識 Sienta 這部車，但我們相信這個名字的討論度會愈來愈高，因為它可能從明年下半年開始，就會大舉進軍國產轎式 MPV（亦可稱為 Compact MPV）市場，這時就會有人問：「那麼原本賣得很好的 Wish 呢？」兩個主要原因，讓這部深受家庭與運將朋友喜愛的車款面臨改朝換代的命運；第一，自 2009 年發表的第二代 Wish，其實已經接近大改款的時程，但日本原廠並沒有繼續推出第三代車款的計畫。第二，因應國內法規，2016 年 7 月 1 日起，強制新出廠車輛裝設胎壓偵測系統，但目前國產 Wish 仍未有針對法規而推出的改款車型，所以除非和泰汽車買斷取得 Wish 的繼續生產權，並針對法規推出所謂的「胎壓偵測升級版」，否則 2016 年 6 月 30 日以後，就無法繼續生產這部熱賣車款。

綜合以上幾點，尋覓 Wish 的接班人成了當務之急，但和泰汽車並非省油的燈，早就備妥了周全計畫，前一屆台北車展中，Toyota 就在現場展示一部前代 Sienta 進行市場調查，試探水溫的意味十分濃厚，加上今年七月才在日本上市的第二代 Sienta，換上全新底盤架構後，不僅車身尺寸增加，軸距也拉長至與 Wish 相同的 2750mm，雖然整體車身尺寸還是小於 Wish，但三排七座設定（亦有六座版本）、雙邊側滑門以及豐富的座椅變化機能，是一部相當適合亞洲市場的新世代 Compact MPV，從目前 Toyota 在台灣市場的佈局中，上有 Alphard、Previa 這兩部中大型 MPV，並在近期引進 Prius α這部 Hybrid 動力七人座轎式 MPV 補齊產品陣線，因此擔當入門級轎式 MPV 的重要角色，無論如何都是 Sienta 最為適任，且 Sienta 在 2016 年國產化上市的傳聞不斷，為了提早讓網友們認識這部重要車款，因此我們特地從日本帶來這篇獨家試駕報導。";

            Font font = new Font("Arial", 20);
            Color txtColor = Color.Bisque;
            Color backColor = Color.DarkBlue;

            var img = CiImage.TextToImage(txt, font, txtColor, backColor, 500);

            img.Save(@"D:\test.jpg", ImageFormat.Jpeg);
        }
    }
}
