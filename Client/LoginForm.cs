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
using System.Runtime.InteropServices;
using System.IO;

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

        private void LoginForm_Load(object sender, EventArgs e)
        {
            fontInstall();
        }

        void fontInstall()
        {
            var vFonts = System.Drawing.FontFamily.Families;
            bool vFont1 = false, vFont2 = false;
            foreach (var vTempFont in vFonts)
            {
                if (vTempFont.Name == "FCfont1")
                    vFont1 = true;
                if (vTempFont.Name == "FCfont2")
                    vFont2 = true;
            }

            if (!vFont1)
                FontOperate.InstallFont(string.Format(@"FCfont1.ttf", System.Environment.CurrentDirectory), "FCfont1");
            if (!vFont2)
                FontOperate.InstallFont(string.Format(@"FCfont2.ttf", System.Environment.CurrentDirectory), "FCfont2");
        }
    }

    public class FontOperate
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd,
        uint Msg,
        int wParam,
        int lParam
        );
        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);


        public static bool InstallFont(string sFontFileName, string sFontName)
        {
            string _sTargetFontPath = string.Format(@"{0}\fonts\{1}", System.Environment.GetEnvironmentVariable("WINDIR"), sFontFileName);//系统FONT目录
            string _sResourceFontPath = string.Format(@"{0}\Fonts\{1}", System.Windows.Forms.Application.StartupPath, sFontFileName);//需要安装的FONT目录
            try
            {
                if (!File.Exists(_sTargetFontPath) && File.Exists(_sResourceFontPath))
                {
                    int _nRet;
                    File.Copy(_sResourceFontPath, _sTargetFontPath);
                    _nRet = AddFontResource(_sTargetFontPath);
                    _nRet = WriteProfileString("fonts", sFontName + "(TrueType)", sFontFileName);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
