using System.Net;
using System.IO;
using System.Text;
using System;
using JXDL.ClientBusiness;
using System.Web.Script.Serialization;

namespace JXDL.ClientBusiness
{
    public class RemoteInterface
    {
        string m_RemotingServerAddress = "";
        public RemoteInterface()
        {
            ConfigFile vConfigFile = new ConfigFile();
            m_RemotingServerAddress = vConfigFile.RemotingServerAddress;
        }

        public UserInfo Login( string UserName,string Password )
        {
            string vUrl = string.Format("{0}/Api/Login", m_RemotingServerAddress);
            string vPostData = string.Format("UserName={0}&Password={1}", UserName, Password);
            string vResult = HttpGet(vUrl, vPostData);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            UserInfo vUserInfo = vJSC.Deserialize<UserInfo>(vResult);
            return vUserInfo;
        }

        public bool Logout( string UserName,string Token )
        {
            string vUrl = string.Format("{0}/Api/Logout", m_RemotingServerAddress);
            string[] vData = new string[] { UserName, Token };
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vPostData = vJSC.Serialize(vData);
            string vResult = HttpPost(vUrl, vPostData);
            return vJSC.Deserialize<bool>(vResult);
        }

        public void GetMapServer(ref string MapServerAddress,ref string MapServerName )
        {
            string vUrl = string.Format("{0}/Api/MapConfig", m_RemotingServerAddress);
            string vPostData = "";
            string vResult = HttpGet(vUrl, vPostData);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string[] vMapConfig = vJSC.Deserialize<string[]>(vResult);
            MapServerAddress = vMapConfig[0];
            MapServerName = vMapConfig[1];
        }

        public void Heartbeat( UserInfo LoginUserInfo)
        {
            string vUrl = string.Format("{0}/Api/Heartbeat", m_RemotingServerAddress);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vPostData = vJSC.Serialize(LoginUserInfo);
            string vResult = HttpPut(vUrl, vPostData);
        }

        string HttpDelete(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "Delete";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        string HttpPut(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "Put";
            request.ContentType = "application/json";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        ///
        /// 下载文件方法
        ///
        /// 文件保存路径和文件名
        /// 返回服务器文件名
        ///
        bool DownloadFile(string DownUrl, string SaveFileName)
        {
            bool flag = false;
            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //判断要下载的文件夹是否存在
            if (File.Exists(SaveFileName))
            {
                //打开要下载的文件
                FStream = File.OpenWrite(SaveFileName);
                //获取已经下载的长度
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current);
            }
            else
            {
                //文件不保存创建一个文件
                FStream = new FileStream(SaveFileName, FileMode.Create);
                SPosition = 0;
            }
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(DownUrl);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                //关闭流
                FStream.Close();
                myStream.Close();
                flag = true;        //返回true下载成功
            }
            catch (Exception)
            {
                FStream.Close();
                flag = false;       //返回false下载失败
            }
            return flag;
        }
    }
}
