using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public class Tbs
    {
        public string tbs { get; set; }
        public int is_login { get; set; }
    }

    public class SignResult
    {
        public int no { get; set; }
        public string error { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
        public int sign_version { get; set; }
        public int is_block { get; set; }
        public Finfo finfo { get; set; }
        public Uinfo uinfo { get; set; }
    }

    public class Finfo
    {
        public Forum_Info forum_info { get; set; }
        public Current_Rank_Info current_rank_info { get; set; }
    }

    public class Forum_Info
    {
        public int forum_id { get; set; }
        public string forum_name { get; set; }
    }

    public class Current_Rank_Info
    {
        public int sign_count { get; set; }
    }

    public class Uinfo
    {
        public long user_id { get; set; }
        public int is_sign_in { get; set; }
        public int user_sign_rank { get; set; }
        public int sign_time { get; set; }
        public int cont_sign_num { get; set; }
        public int total_sign_num { get; set; }
        public int cout_total_sing_num { get; set; }
        public int hun_sign_num { get; set; }
        public int total_resign_num { get; set; }
        public int is_org_name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sign("腿", GetTbs()));
            Console.ReadKey();
        }
        static string Sign(string tbName, string tbs)
        {
            string psotdata = "ie=utf-8&kw=" + tbName + "&tbs=" + tbs;
            var req = CreateReq("https://tieba.baidu.com/sign/add");
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(new Cookie("BDUSS", "NncUxQTGd6NUc1R2dhWmM3bERHQXhFb09MVlBRSEdSZVVNfktwZzhib1VEcTliQVFBQUFBJCQAAAAAAAAAAAEAAADKxgCtze2357S1tb3O0rb6sd8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABSBh1sUgYdbRD", "/", "baidu.com"));
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 BIDUBrowser/8.7 Safari/537.36";
            req.Method = "POST";
            byte[] buffer = Encoding.UTF8.GetBytes(psotdata);
            req.GetRequestStream().Write(buffer, 0, buffer.Length);

            string str = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
            SignResult o = JsonConvert.DeserializeObject<SignResult>(str);
            return o.data.errmsg;
        }
        static string GetTbs()
        {
            var req = CreateReq("http://tieba.baidu.com/dc/common/tbs");
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(new Cookie("BDUSS", "NncUxQTGd6NUc1R2dhWmM3bERHQXhFb09MVlBRSEdSZVVNfktwZzhib1VEcTliQVFBQUFBJCQAAAAAAAAAAAEAAADKxgCtze2357S1tb3O0rb6sd8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABSBh1sUgYdbRD", "/", "baidu.com"));
            string str = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
            Tbs o = JsonConvert.DeserializeObject<Tbs>(str);
            return o.tbs;
        }

        static HttpWebRequest CreateReq(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            return req;
        }
        static HttpWebResponse GetRes(HttpWebRequest req)
        {
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            return res;
        }
    }
}
