using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HttpRequest1
{
    public class GetHtmlString
    {
        /// <summary>
        /// 获取html页面
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns>string</returns>
        public string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }

        }
        //public static void PostAndCookies1(string url, string postData, ref CookieContainer cookies)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    if (cookies.Count == 0)
        //    {//也许可以不用这样
        //        request.CookieContainer = new CookieContainer();
        //        cookies = request.CookieContainer;

        //    }
        //    else
        //    {
        //        request.CookieContainer = cookies;
        //    }
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = postData.Length;


        //}
        CookieContainer cookies = new CookieContainer();

        /// <summary>
        /// post
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postData">要提交的信息 例 name=zhen&password=123456</param>
        /// <returns></returns>
        public string PostAndCookies(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = postData.Length;
            request.ContentLength = Encoding.UTF8.GetByteCount(postData);
            request.CookieContainer = cookies;
            Stream myRequeststream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequeststream);
            myStreamWriter.Write(postData);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookies.GetCookies(response.ResponseUri);
            Stream myResposeStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResposeStream);
            string htmlString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResposeStream.Close();
            return htmlString;


        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="getData">提交的数据，例name=zhen&password=123456</param>
        /// <returns></returns>
        public string GetAndCookies(string url, string getData)
        {
            string get1 = getData == "" ? "" : "?";
            string url1 = url + get1 + getData;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url1);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            //不懂要不要，有没有用，先试着
            request.CookieContainer = cookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookies.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string htmlString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return htmlString;



        }
        /// <summary>
        /// 需手动输入 cookies 的get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="getData">要传的数据</param>
        /// <param name="myCookies">cookie</param>
        /// <returns></returns>
        public string GetAndCookies(string url, string getData, params Cookie[] myCookies)
        {
            string get1 = getData == "" ? "" : "?";
            string url1 = url + get1 + getData;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url1);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            //不懂要不要，有没有用，先试着

            request.CookieContainer = cookies;

            if (myCookies.Length > 0)
            {
                Uri uri = new Uri(url);
                for (int i = 0; i < myCookies.Length; i++)
                {
                    request.CookieContainer.Add(uri, myCookies[i]);
                }


            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookies.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string htmlString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return htmlString;



        }
    }
}
