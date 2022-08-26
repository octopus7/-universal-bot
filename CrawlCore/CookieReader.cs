using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibCrawl
{
    public class CookieReader
    {
        public static CookieContainer Parse(string cookies, string cookieUrl)
        {
            CookieContainer cc = new CookieContainer();
            string[] lines = cookies.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

            Uri target = new Uri(cookieUrl);

            foreach (var cookie in lines)
            {
                string[] cols = cookie.Split(new char[] { '=' });
                string c = cookie.ToString();
                Console.WriteLine(c);
                cc.Add(new System.Net.Cookie(cols[0], cols[1]) { Domain = target.Host });
            }

            return cc;
        }
    }
}
