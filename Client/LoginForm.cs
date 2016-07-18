using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JXDL.ClientBusiness;

namespace JXDL.Client
{
    public partial class LoginForm : Form
    {
        public UserInfo LoginUserInfo;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            if ( textBox_Password.Text=="" || textBox_UserName.Text == "")
            {
                MessageBox.Show("请输入用户名和密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    RemoteInterface vRemoteInterface = new RemoteInterface();
                    UserInfo vUserInfo = vRemoteInterface.Login(textBox_UserName.Text, textBox_Password.Text);
                    if (vUserInfo.ID != null && vUserInfo.ID != 0)
                    {
                        if (vUserInfo.ID != -1)
                        {
                            LoginUserInfo = vUserInfo;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("该用户已在其它机器上登陆或没有正常退出，请在1分钟后重新尝试登陆", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch ( Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            ConfigForm vConfigForm = new ConfigForm();
            vConfigForm.ShowDialog();
        }
    }
}
