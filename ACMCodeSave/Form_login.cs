using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACMCodeSave {
	public partial class Form_login : Form {
		public Form_login() {
			InitializeComponent();
		}

		private void textBox_pass_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				FUNC_loin();
			}
		}

		void FUNC_loin() {
			if (textBox_user.Text == "" || textBox_pass.Text == "") {
				MessageBox.Show("请输入正确账号密码", "Tip");
			}
			else this.Close();
		}
		private void button_login_Click(object sender, EventArgs e) {
			FUNC_loin();
		}
	}
}
