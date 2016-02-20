using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Exyao {
    class Exyao_http {
        public CookieContainer cookie = new CookieContainer();
        public string encode;
        public HttpWebResponse response;

        public Exyao_http(string _Encoding) {
            encode = _Encoding;
        }

        public string GET(string url, string referer = "") {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);
            http.Method = "GET";
			http.Referer = referer;
			http.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SE 2.X MetaSr 1.0)";
            http.CookieContainer = cookie;
            HttpWebResponse res = (HttpWebResponse)http.GetResponse();

            response = res;
			Stream s = res.GetResponseStream();
            StreamReader rs = new StreamReader(s, Encoding.GetEncoding(encode));
            string ret = rs.ReadToEnd();
            rs.Close();
            s.Close();
            return ret;
        }

        public string POST(string url, string data = "", string referer = "") {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);
            http.CookieContainer = cookie;
            http.Method = "POST";
			http.Referer = referer;
			http.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SE 2.X MetaSr 1.0)";
            http.ContentType = "application/x-www-form-urlencoded";
            Stream sd = http.GetRequestStream();
            StreamWriter sw = new StreamWriter(sd);
            sw.Write(data);
            sw.Close();

            HttpWebResponse res = (HttpWebResponse)http.GetResponse();
            response = res;
            Stream s = res.GetResponseStream();
            StreamReader rs = new StreamReader(s, Encoding.GetEncoding(encode));
            string ret = rs.ReadToEnd();
			rs.Close();
            s.Close();
            return ret;
        }

        public void deal_cookie() {
            string[] h = response.Headers.GetValues("Set-Cookie");
            if (h == null) return;
            foreach (string c in h) {
                cookie.SetCookies(response.ResponseUri, c);
            }
        }
    }

    class Exyao_Regex {
        private Regex r;
        private Match ret_;
        private bool is_all;
        private MatchCollection ret;

        public Exyao_Regex(string s) {
            r = new Regex(s);
        }

        public bool match(string s, bool all = false, int start = 0) {
            is_all = all;
            if (all) {
                ret = r.Matches(s, start);
                return ret.Count != 0;
            }
            else {
                ret_ = r.Match(s, start);
                return ret_.Success;
            }
        }

        public int getn() {
            return ret.Count;
        }

        public string get_text(int id = 0) {
            if (is_all) {
                return ret[id].Value;
            }
            else {
                return ret_.Value;
            }
        }

        public string get_subtext(int i = 0, int j = 0) {
            if (is_all) {
                GroupCollection g = ret[i].Groups;
                return g[j + 1].Value;
            }
            else {
                GroupCollection g = ret_.Groups;
                return g[j + 1].Value;
            }
        }
    }

    class F {
        public static string urlencode_utf8(string s) {
            byte[] bs = Encoding.GetEncoding("utf-8").GetBytes(s);
            int len = bs.Length;
            string ret = "";
            for (int i = 0; i < len; i++) {
                int c = bs[i];
                c = c < 0 ? 256 + c : c;
                if (c < 42 || c == 43 || c > 57 && c < 64 || c > 90 && c < 95 || c == 96 || c > 122) {
                    ret += "%" + c.ToString("x2");
                }
                else {
                    ret += (char)c;
                }
            }
            return ret;
        }

        public static string urlencode_gb2312(string url) {
            byte[] bs = Encoding.GetEncoding("gb2312").GetBytes(url);
            int len = bs.Length;
            string ret = "";
            for (int i = 0; i < len; i++) {
                int c = bs[i];
                c = c < 0 ? 256 + c : c;
                if (c < 42 || c == 43 || c > 57 && c < 64 || c > 90 && c < 95 || c == 96 || c > 122) {
                    ret += "%" + c.ToString("x2");
                }
                else {
                    ret += (char)c;
                }
            }
            return ret;
        }

        public static bool intext(string a, string b) {
            if (a.IndexOf(b) >= 0) return true;
            return false;
        }

        public static string substr(string s, string l, string r, int begin = 0) {
            int lp, rp;
            lp = s.IndexOf(l, begin);
            if (lp < 0) return "";

            rp = s.IndexOf(r, lp + l.Length);
            if (rp < 0) return "";

            return s.Substring(lp + l.Length, rp - lp - l.Length);
        }

        public static string readfile(string file, string encode = "gb2312") {
            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(encode));
            string ret = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return ret;
        }

        public static void writefile(string file, string content = "", string encode = "gb2312") {
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(encode));
            sw.Write(content);
            sw.Close();
            fs.Close();
        }

        public static string gb2312_to_utf8(string s) {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            Encoding utf8 = Encoding.GetEncoding("utf-8");

            byte[] w = gb2312.GetBytes(s);
            w = Encoding.Convert(gb2312, utf8, w);
            return utf8.GetString(w);
        }

        public static string utf8_to_gb2312(string s) {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            Encoding utf8 = Encoding.GetEncoding("utf-8");

            byte[] w = utf8.GetBytes(s);
            w = Encoding.Convert(utf8, gb2312, w);
            return gb2312.GetString(w);
        }
    }
}
