using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Exyao;

namespace ACMCodeSave {
	struct UserData {
		public string user, pass;
		public bool ok;
	}
	
	class Q {
		public static UserData FUNC_login(Form form) {
			UserData ret = new UserData();
			Form_login w = new Form_login();
			w.ShowDialog(form);
			ret.user = w.textBox_user.Text;
			ret.pass = w.textBox_pass.Text;
			w.Dispose();
			ret.ok = ret.user != "";
			return ret;
		}

		public static string DealCode(string code) {
			code = code.Replace("&lt;", "<");
			code = code.Replace("&gt;", ">");
			code = code.Replace("&quot;", "\"");
			code = code.Replace("&amp;", "&");
			code = code.Replace("&nbsp;", " ");
			Exyao_Regex reg = new Exyao_Regex("&#(.*?);");
			reg.match(code, true);
			for (int j = 0; j < reg.getn(); j++) {
				string p = "";
				p += (char)(int.Parse(reg.get_subtext(j, 0)));
				code = code.Replace(reg.get_text(j), p);
			}
			code = code.Replace("\n", "\r\n");
			return code;
		}

		public static void CheckDir(string dir) {
			if (Directory.Exists(dir)) return;
			Directory.CreateDirectory(dir);
		}
	}

	class Vjudge{
		public static UserData user;
		public delegate void Delegate(int i, int tot);
		public Delegate UpdateForm;

		public Vjudge(UserData u) {
			user = u;
		}

		public void NewThread(){
			Exyao_http http = new Exyao_http("utf-8");
			string ret = http.POST("http://acm.hust.edu.cn/vjudge/user/login.action", "username=" + user.user + "&password=" + user.pass);
			if (!F.intext(ret, "success")) {
				MessageBox.Show("登录失败", "Tip");
				return;
			}
			
			string dir = System.Environment.CurrentDirectory + "\\" + user.user + "\\";
			Q.CheckDir(dir); UpdateForm(-1, 0);

			ret = http.POST("http://acm.hust.edu.cn/vjudge/problem/fetchStatus.action", "draw=2&columns%5B0%5D%5Bdata%5D=0&columns%5B0%5D%5Bname%5D=&columns%5B0%5D%5Bsearchable%5D=true&columns%5B0%5D%5Borderable%5D=false&columns%5B0%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B0%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B1%5D%5Bdata%5D=1&columns%5B1%5D%5Bname%5D=&columns%5B1%5D%5Bsearchable%5D=true&columns%5B1%5D%5Borderable%5D=false&columns%5B1%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B1%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B2%5D%5Bdata%5D=2&columns%5B2%5D%5Bname%5D=&columns%5B2%5D%5Bsearchable%5D=true&columns%5B2%5D%5Borderable%5D=false&columns%5B2%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B2%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B3%5D%5Bdata%5D=3&columns%5B3%5D%5Bname%5D=&columns%5B3%5D%5Bsearchable%5D=true&columns%5B3%5D%5Borderable%5D=false&columns%5B3%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B3%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B4%5D%5Bdata%5D=4&columns%5B4%5D%5Bname%5D=&columns%5B4%5D%5Bsearchable%5D=true&columns%5B4%5D%5Borderable%5D=false&columns%5B4%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B4%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B5%5D%5Bdata%5D=5&columns%5B5%5D%5Bname%5D=&columns%5B5%5D%5Bsearchable%5D=true&columns%5B5%5D%5Borderable%5D=false&columns%5B5%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B5%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B6%5D%5Bdata%5D=6&columns%5B6%5D%5Bname%5D=&columns%5B6%5D%5Bsearchable%5D=true&columns%5B6%5D%5Borderable%5D=false&columns%5B6%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B6%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B7%5D%5Bdata%5D=7&columns%5B7%5D%5Bname%5D=&columns%5B7%5D%5Bsearchable%5D=true&columns%5B7%5D%5Borderable%5D=false&columns%5B7%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B7%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B8%5D%5Bdata%5D=8&columns%5B8%5D%5Bname%5D=&columns%5B8%5D%5Bsearchable%5D=true&columns%5B8%5D%5Borderable%5D=false&columns%5B8%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B8%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B9%5D%5Bdata%5D=9&columns%5B9%5D%5Bname%5D=&columns%5B9%5D%5Bsearchable%5D=true&columns%5B9%5D%5Borderable%5D=false&columns%5B9%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B9%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B10%5D%5Bdata%5D=10&columns%5B10%5D%5Bname%5D=&columns%5B10%5D%5Bsearchable%5D=true&columns%5B10%5D%5Borderable%5D=false&columns%5B10%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B10%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B11%5D%5Bdata%5D=11&columns%5B11%5D%5Bname%5D=&columns%5B11%5D%5Bsearchable%5D=true&columns%5B11%5D%5Borderable%5D=false&columns%5B11%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B11%5D%5Bsearch%5D%5Bregex%5D=false&order%5B0%5D%5Bcolumn%5D=0&order%5B0%5D%5Bdir%5D=desc&start=0&length=10000&search%5Bvalue%5D=&search%5Bregex%5D=false&un=" + user.user + "&OJId=All&probNum=&res=1&language=&orderBy=run_id");
			Exyao_Regex reg = new Exyao_Regex("\\[(\\d*?),.*?,(.*?),.*?,(.*?),(.*?),\"(.*?)\",(.*?),(.*?),.*?,.*?,\"(.*?)\",\"(.*?)\",.*?\\]");
			reg.match(ret, true);
			int n = reg.getn();
			UpdateForm(0, n);
			for (int i = n - 1; i >= 0; i--) {
				Trace.WriteLine(i);
				string Uid = reg.get_subtext(i, 0);
				string Pid = reg.get_subtext(i, 1);
				string Memory = reg.get_subtext(i, 2) + "KB";
				string TotTime = reg.get_subtext(i, 3) + "ms";
				string SubmitType = reg.get_subtext(i, 4);
				string CodeLen = reg.get_subtext(i, 5) + "B";
				string SubmitTime = reg.get_subtext(i, 6);
				string OJName = reg.get_subtext(i, 7);
				string OJPid = reg.get_subtext(i, 8);
				DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
				dt = dt.Add(new TimeSpan(long.Parse(SubmitTime + "0000")));
				dt = dt.Add(new TimeSpan(8, 0, 0));
				string t = dt.ToUniversalTime().ToString();

				string ans = string.Format(Properties.Resources.VjudgeFormat, user.user, OJName + OJPid, Pid, t, SubmitType, Memory, CodeLen, TotTime);
				string html = http.GET("http://acm.hust.edu.cn/vjudge/problem/viewSource.action?id=" + Uid);
				string code = Q.DealCode(F.substr(html, ",monospace\">", "</pre>"));
				ans = ans + "\r\n" + code;

				Q.CheckDir(dir + OJName);
				F.writefile(dir + OJName + "\\" + OJName + OJPid + ".txt", ans);
				UpdateForm(n - i, n);
			}
		}
	}
}
