using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using ACMCodeSave;
using Exyao;

namespace ACMCodeSave {
	public partial class Form_main : Form {
		public Form_main() {
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
		}

		private void button_vjudge_Click(object sender, EventArgs e) {
			UserData user = Q.FUNC_login(this);
			if (!user.ok) return;

			button_vjudge.Enabled = false;
			Vjudge vjudge = new Vjudge(user);
			vjudge.UpdateForm = UpdateForm;

			Thread thr = new Thread(vjudge.NewThread);
			thr.IsBackground = true;
			thr.Start();
		}

		void UpdateForm(int i,int tot) {
			if (i == -1) {
				toolStripStatusLabel.Text = "正在读取中";
				toolStripProgressBar.Value = 0;
			}
			else {
				toolStripStatusLabel.Text = "" + i + "/" + tot;
				Trace.WriteLine(100 * i / tot);
				toolStripProgressBar.Value = 100 * i / tot;
			}

			if (i == tot) {
				MessageBox.Show("Vjudge备份完成\r\n代码保存在当前目录里", "Tip");
				button_vjudge.Enabled = true;
				toolStripStatusLabel.Text = "准备就绪";
				toolStripProgressBar.Value = 0;
			}
		}
		private void Form_main_Load(object sender, EventArgs e) {
			
		}
	}
}
