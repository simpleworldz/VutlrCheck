using HttpRequest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Vutlr
{
    public class Detection
    {
        /// <summary>
        /// 检测是否Sold Out
        /// </summary>
        /// <param name="getHtml"></param>
        public static void Detect(GetHtmlString getHtml)
        {

            string htmlStr = getHtml.PostAndCookies("https://my.vultr.com/deploy/", "");
            DetectString(htmlStr);

        }
        /// <summary>
        /// 检测是否Sold Out的Times.Timer事件Event (已废弃）
        /// </summary>
        /// <param name="source"></param>

        /// <param name="getHtml"></param>
        public static void DetectEvent(object source, ElapsedEventArgs e, GetHtmlString getHtml)
        {

            string htmlStr = getHtml.PostAndCookies("https://my.vultr.com/deploy/", "");
            DetectString(htmlStr);
        }
        /// <summary>
        /// 根据html中包含的信息判断Japan 和 Singapore的vps是否售罄
        /// （html网页变化的话，判断方法可能需要重写
        /// </summary>
        /// <param name="htmlStr"></param>
        public static void DetectString(string htmlStr)
        {
            if (htmlStr.Contains("www.google-analytics.com/analytics.js"))
            {
                Console.WriteLine("遇到了人机验证，怕是要GG一段时间");
                return;
            }
            string strJ1 = "24\":{\"0\":{\"200\":\"soldout\"";
            string strJ2 = "\"25\":[[]]";
            string strS1 = "40\":{\"0\":{\"200\":\"soldout\"";
            string strS2 = "\"40\":[[]]";
            if (htmlStr.Contains(strJ1)||htmlStr.Contains(strJ2))
            {
                Console.WriteLine("Japan vps Sold Out!");
            }
            else
            {
                Console.WriteLine("Japan 2.5$ vps available! ");
            }
            if (htmlStr.Contains(strS1)||htmlStr.Contains(strS2))
            {
                Console.WriteLine("Singapore vps Sold Out!");
            }
            else
            {
                Console.WriteLine("Singapore 2.5$ vps available!");
            }
            

        }
    }
}
