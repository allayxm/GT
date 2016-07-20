using System.Net;
using System.IO;
using System.Text;
using System;
using System.Web;
using System.Net.Http;
using JXDL.ClientBusiness;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Collections.Specialized;
using JXDL.IntrefaceStruct;
using System.Data;

namespace JXDL.ClientBusiness
{
    public class RemoteInterface
    {
        string m_RemotingServerAddress = "";
        readonly int m_UserID;
        readonly string m_UserName;
        readonly string m_Token;
        public RemoteInterface( int UserID,string UserName,string Token )
        {
            ConfigFile vConfigFile = new ConfigFile();
            m_RemotingServerAddress = vConfigFile.RemotingServerAddress;
            m_UserID = UserID;
            m_UserName = UserName;
            m_Token = Token;
        }
        public RemoteInterface()
        {
            ConfigFile vConfigFile = new ConfigFile();
            m_RemotingServerAddress = vConfigFile.RemotingServerAddress;
        }

        #region 地区文档数据统计
        public FileNumberStatisticsStruct[] FileNumberStatistics( string AreaStr)
        {
            JavaScriptSerializer vJSS = new JavaScriptSerializer();
            FileNumberStatisticsStruct[] vFileNumberStatisticsData = null;

            string vUrl = string.Format("{0}/Api/FileNumberStatistics", m_RemotingServerAddress);
            StatisticsParamtStruct vPostParam = new StatisticsParamtStruct();
            vPostParam.UserID = m_UserID;
            vPostParam.UserName = m_UserName;
            vPostParam.AreaCodes = HttpUtility.UrlEncode(AreaStr);
            string vPostData = vJSS.Serialize(vPostParam);
            //string vPostData = string.Format("UserID={0}&UserName={1}&AreaCodes={2}",m_UserID,m_UserName, HttpUtility.UrlEncode(AreaStr));
            string vResult = HttpPost(vUrl, vPostData);
            vResult = HttpUtility.UrlDecode(vResult);
            vFileNumberStatisticsData = vJSS.Deserialize<FileNumberStatisticsStruct[]>(vResult);
            return vFileNumberStatisticsData;
        }
        #endregion

        #region 删除文件
        public bool DeleteFile( int FileID,string FileName )
        {
            string vUrl = string.Format("{0}/Api/DeleteFile", m_RemotingServerAddress);
            string vPostData = string.Format("ID={0}&UserID={1}&UserName={2}&FileName={3}", FileID, m_UserID,HttpUtility.UrlEncode(m_UserName),HttpUtility.UrlEncode( FileName));
            string vResult = HttpGet(vUrl, vPostData);
            return vResult.ToUpper() == "TRUE" ? true : false;
        }
        #endregion

        #region 文件查询
        public DataTable QueryFile(string Township, string VillageCommittee, string Village,
            string Author, string FileName)
        {
            JXDL.IntrefaceStruct.FileInfo[] vFileInfoList = null;
            string vUrl = string.Format("{0}/Api/QueryFile", m_RemotingServerAddress);
            string vPostData = string.Format("UserID={0}&UserName={1}&Township={2}&VillageCommittee={3}&Village={4}&Author={5}&FileName={6}",m_UserID, HttpUtility.UrlEncode(m_UserName), HttpUtility.UrlEncode(Township),
                HttpUtility.UrlEncode(VillageCommittee), HttpUtility.UrlEncode(Village), HttpUtility.UrlEncode(Author), HttpUtility.UrlEncode(FileName));
            string vResult = HttpGet(vUrl, vPostData);
            if (vResult != null && vResult != "" && vResult != "[]")
            {
                JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
                vFileInfoList = vJSC.Deserialize<JXDL.IntrefaceStruct.FileInfo[]>(vResult);
            }
           
            return convertFileInfoToDataTable(vFileInfoList);
        }

        DataTable convertFileInfoToDataTable(JXDL.IntrefaceStruct.FileInfo[] FileInfoList)
        {
            DataTable vFileInfoTable = createFileInfoDataTable();
            if (FileInfoList != null)
            {
                foreach (JXDL.IntrefaceStruct.FileInfo vTempFileInfo in FileInfoList)
                {
                    DataRow vNewRow = vFileInfoTable.NewRow();
                    vNewRow["ID"] = vTempFileInfo.ID;
                    vNewRow["FileName"] = vTempFileInfo.FileName;
                    vNewRow["Author"] = vTempFileInfo.Author;
                    vNewRow["AreaCode"] = vTempFileInfo.AreaCode;
                    vNewRow["UnitName"] = vTempFileInfo.UnitName;
                    vNewRow["UploadTime"] = vTempFileInfo.UploadTime;
                    vFileInfoTable.Rows.Add(vNewRow);
                }
            }
            vFileInfoTable.AcceptChanges();
            return vFileInfoTable;
        }


        //public int ID { get; set; }
        //public string FileName { get; set; }
        //public string Author { get; set; }
        //public string AreaCode { get; set; }
        //public string UnitName { get; set; }
        //public DateTime UploadTime { get; set; }

        DataTable createFileInfoDataTable()
        {
            DataTable vNewTable = new DataTable("FileInfo");
            vNewTable.Columns.Add("ID",typeof(int));
            vNewTable.Columns.Add("FileName", typeof(string));
            vNewTable.Columns.Add("Author", typeof(string));
            vNewTable.Columns.Add("AreaCode", typeof(string));
            vNewTable.Columns.Add("UnitName", typeof(string));
            vNewTable.Columns.Add("UploadTime", typeof(DateTime));
            vNewTable.AcceptChanges();
            return vNewTable;
        }
        #endregion

        #region 通过区域代码获取对应文件信息
        public JXDL.IntrefaceStruct.FileInfo[] GetFiles( string[] AreaCodeArray)
        {
            JXDL.IntrefaceStruct.FileInfo[] vFileInfoList = null;
            string vUrl = string.Format("{0}/Api/GetFileList", m_RemotingServerAddress);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vPostData = vJSC.Serialize(AreaCodeArray);
           
            string vResult = HttpPost(vUrl,  vPostData);
            //string vResult = HttpGet(vUrl, string.Format("AreaStrCode={0}", AreaStrCode) );
            if ( vResult != null && vResult != "" && vResult!= "[]")
            {
                vFileInfoList = vJSC.Deserialize<JXDL.IntrefaceStruct.FileInfo[]>(vResult);
                foreach( JXDL.IntrefaceStruct.FileInfo vTempFileInfo in vFileInfoList)
                {
                    vTempFileInfo.Author   = HttpUtility.UrlDecode(vTempFileInfo.Author);
                    vTempFileInfo.AreaCode = HttpUtility.UrlDecode(vTempFileInfo.AreaCode);
                    vTempFileInfo.FileName = HttpUtility.UrlDecode(vTempFileInfo.FileName);
                    vTempFileInfo.UnitName = HttpUtility.UrlDecode(vTempFileInfo.UnitName);
                }
            }
            return vFileInfoList;
        }
        #endregion

        #region 登陆
        public UserInfo Login( string UserName,string Password )
        {
            string vUrl = string.Format("{0}/Api/Login", m_RemotingServerAddress);
            string vPostData = string.Format("UserName={0}&Password={1}", UserName, Password);
            string vResult = HttpGet(vUrl, vPostData);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            UserInfo vUserInfo = vJSC.Deserialize<UserInfo>(vResult);
            return vUserInfo;
        }
        #endregion

        #region 登出
        public bool Logout( string UserName,string Token )
        {
            string vUrl = string.Format("{0}/Api/Logout", m_RemotingServerAddress);
            string[] vData = new string[] { UserName, Token };
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vPostData = vJSC.Serialize(vData);
            string vResult = HttpPost(vUrl, vPostData);
            return vJSC.Deserialize<bool>(vResult);
        }
        #endregion

        #region 获取地图服务器配置信息
        public void GetMapServer(ref string MapServerAddress,ref string MapDBName,
            ref string DBUserName,ref string DBPassword )
        {
            string vUrl = string.Format("{0}/Api/MapConfig", m_RemotingServerAddress);
            string vPostData = "";
            string vResult = HttpGet(vUrl, vPostData);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string[] vMapConfig = vJSC.Deserialize<string[]>(vResult);
            MapServerAddress = vMapConfig[0];
            MapDBName = vMapConfig[1];
            DBUserName = vMapConfig[2];
            DBPassword = vMapConfig[3];
        }
        #endregion

        #region 心跳包
        public void Heartbeat( UserInfo LoginUserInfo)
        {
            string vUrl = string.Format("{0}/Api/Heartbeat", m_RemotingServerAddress);
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vPostData = vJSC.Serialize(LoginUserInfo);
            string vResult = HttpPut(vUrl, vPostData);
        }
        #endregion

        #region 上传文件
        public bool UploadFiles(string[] Files,string[] Authors,
            string[] AreaCodeList,string[] UnitNameList )
        {
            string vUrl = string.Format("{0}/Api/UploadFile", m_RemotingServerAddress);
            UploadFileStruct vUFStruct = new UploadFileStruct();
            vUFStruct.UsersAuthor = new UserAuthorSturct();
            vUFStruct.UsersAuthor.UserID = m_UserID;
            vUFStruct.UsersAuthor.Token = m_Token;

            vUFStruct.Files = new IntrefaceStruct.FileInfo[Files.Length];
            for( int i=0;i<Files.Length;i++)
            {
                vUFStruct.Files[i] = new IntrefaceStruct.FileInfo();
                vUFStruct.Files[i].FileName = Path.GetFileName( Files[i] );
                vUFStruct.Files[i].Author = Authors[i];
                vUFStruct.Files[i].AreaCode = AreaCodeList[i];
                vUFStruct.Files[i].UnitName = UnitNameList[i];
            }
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vJsonStr = vJSC.Serialize(vUFStruct);
            NameValueCollection vCollection = new NameValueCollection();
            vCollection.Add("Json", vJsonStr);

            List<ByteArrayContent> vFormDatas = GetFormDataByteArrayContent(vCollection);
            List<ByteArrayContent> vFiles = GetFileByteArrayContent(Files);
            
            string vResult = HttpPostFile(vUrl, vFormDatas, vFiles);
            return bool.Parse( vResult );
        }

        public bool UploadFile(string File, string Author, 
            string AreaCode,string UnitName)
        {
            string vUrl = string.Format("{0}/Api/UploadFile", m_RemotingServerAddress);
            UploadFileStruct vUFStruct = new UploadFileStruct();
            vUFStruct.UsersAuthor = new UserAuthorSturct();
            vUFStruct.UsersAuthor.UserID = m_UserID;
            vUFStruct.UsersAuthor.Token = m_Token;
            vUFStruct.UsersAuthor.UserName = m_UserName;

            vUFStruct.Files = new IntrefaceStruct.FileInfo[1];
            vUFStruct.Files[0] = new IntrefaceStruct.FileInfo();
            vUFStruct.Files[0].FileName = Path.GetFileName(File);
            vUFStruct.Files[0].Author = Author;
            vUFStruct.Files[0].AreaCode = AreaCode;
            vUFStruct.Files[0].UnitName = UnitName;


            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            string vJsonStr = vJSC.Serialize(vUFStruct);
            NameValueCollection vCollection = new NameValueCollection();
            vCollection.Add("Json", vJsonStr);

            List<ByteArrayContent> vFormDatas = GetFormDataByteArrayContent(vCollection);
            List<ByteArrayContent> vFiles = GetFileByteArrayContent( new string[] { File });

            string vResult = HttpPostFile(vUrl, vFormDatas, vFiles);
            return bool.Parse(vResult);
        }

        /// <summary>
        /// 获取文件集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private List<ByteArrayContent> GetFileByteArrayContent(string[] files)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var file in files)
            {
                var fileContent = new ByteArrayContent(File.ReadAllBytes(file));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(file)
                };
                list.Add(fileContent);
            }
            return list;
        }

        /// <summary>
        /// 获取键值集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        private List<ByteArrayContent> GetFormDataByteArrayContent(NameValueCollection Collection)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var key in Collection.AllKeys)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(Collection[key]));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = key
                };
                list.Add(dataContent);
            }
            return list;
        }

        string HttpPostFile(string Url,List<ByteArrayContent> FormDatas, List<ByteArrayContent> Files)
        {
            string vResult = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));//设定要响应的数据格式  
                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    //var formDatas = this.GetFormDataByteArrayContent(this.GetNameValueCollection(this.gv_FormData));//获取键值集合对应的ByteArrayContent集合  
                    //var files = this.GetFileByteArrayContent(this.GetHashSet(this.gv_File));//获取文件集合对应的ByteArrayContent集合  
                    Action<List<ByteArrayContent>> act = (dataContents) =>
                    {//声明一个委托，该委托的作用就是将ByteArrayContent集合加入到MultipartFormDataContent中  
                        foreach (var byteArrayContent in dataContents)
                        {
                            content.Add(byteArrayContent);
                        }
                    };
                    act(FormDatas);//执行act  
                    act(Files);//执行act  
                    try
                    {
                        var result = client.PostAsync(Url, content).Result;//post请求  
                        vResult = result.Content.ReadAsStringAsync().Result;//将响应结果显示在文本框内  
                    }
                    catch (Exception ex)
                    {
                        vResult = ex.ToString();//将异常信息显示在文本框内  
                    }
                }
            }
            return vResult;
        }
        #endregion

        #region 下载文件
        public  bool DownloadFile(int FileID,string SaveFileName)
        {
            string vDownUrl = string.Format("{0}/Api/DownloadFile?FileID={1}&UserID={2}&UserName={3}", m_RemotingServerAddress, FileID,m_UserID,HttpUtility.UrlEncode(m_UserName));
        
            bool flag = false;
            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //判断要下载的文件夹是否存在
            if (File.Exists(SaveFileName))
            {
                File.Delete(SaveFileName);
                //打开要下载的文件
                //FStream = File.OpenWrite(SaveFileName);
                //获取已经下载的长度
                //SPosition = FStream.Length;
                //FStream.Seek(SPosition, SeekOrigin.Current);
            }
            //else
            //{
                //文件不保存创建一个文件
                FStream = new FileStream(SaveFileName, FileMode.Create);
                SPosition = 0;
            //}
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(vDownUrl);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();

                //获取文件名
                //HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();

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
        #endregion

        #region Http操作协议
        string HttpDelete(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "DELETE";
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
        #endregion
    }
}
