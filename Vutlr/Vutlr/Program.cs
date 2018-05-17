using HttpRequest1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace Vutlr
{
    class Program
    {    
        private static Timer timer;
        static void Main(string[] args)
        {   //执行过多次会进行Google人机验证，那么要GG一段时间
            //把文件下载下来再测试规律，别每次都请求，请求多了。。。
            //www.google-analytics.com/analytics.js需要代理才能访问
            //关了代理的话就 是验证码
            //string fil = File.ReadAllText(@"../../../vu.txt");
            //Detection.DetectString(fil);
            Console.WriteLine("开始执行。。。。");
            //解决System.Net.WebException:“请求被中止: 未能创建 SSL/TLS 安全通道。”
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //新建的账号
            //string url = "https://my.vultr.com?username=1374477863@qq.com&password=Qq12345678";
            //string str=   GetHtmlString.GetHtml()
            GetHtmlString getHtml = new GetHtmlString();
            string htmlStr1 = getHtml.PostAndCookies("https://my.vultr.com", "");
            Match match = Regex.Match(htmlStr1, "name=\"action\"\\s+value=\"(\\S+)\"");
            //string postData = "action=djJ8NjIxNGlNbHlWeXFqU2VsZkVHR0VjYVV5T29TUW84RVl8x06Ino26YcJeQLYTSgCZMB3DuPU1Hvc4ilvJJRh37z85gKNibQ";
            string postData = "action=" + match.Groups[1].Value;
            postData += "&username=1374477863@qq.com";
            postData += "&password=Qq12345678";
            string htmlStr2 = getHtml.PostAndCookies("https://my.vultr.com", postData);
            //Timer timer = new Timer();
            //timer.Interval = 3000;
            ////timer.Elapsed += new ElapsedEventHandler(Detection.DetectEvent(getHtml,new ElapsedEventArgs()));
            //timer.Elapsed += (sender, arg) => Detection.DetectEvent(sender, getHtml);
            ////Detection.Detect(getHtml);
            //timer.Start();

            timer = new Timer();
            //重点
            timer.Elapsed += new ElapsedEventHandler((s, e) => Detection.Detect(getHtml));
            timer.Interval = 1800000;
            timer.Enabled = true;

            Detection.Detect(getHtml);
            Console.ReadKey();
            Console.ReadKey();
        }


    }
}
