using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using JXDL.ManageBusiness;
using JXDL.IntrefaceStruct;
using System.Web.Script.Serialization;

namespace JXDL.Manage.App
{
    public class UploadFileController : ApiController
    {
        // GET: api/UploadFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UploadFile/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/UploadFile/5
        public void Put(int id, [FromBody]string value)
        {
        
        }

        // POST: api/UploadFile
        public async Task<bool> Post(int id = 0)
        {
            bool vResult = false;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            Dictionary<string, string> vDic = new Dictionary<string, string>();
            //string root = HttpContext.Current.Server.MapPath("~/App_Data");//指定要将文件存入的服务器物理位置  
            var vProvider = new MultipartFormDataMemoryStreamProvider();
            try
            {
                // 从流中读取数据
                await Request.Content.ReadAsMultipartAsync(vProvider);
                foreach (var key in vProvider.FormData.AllKeys)
                {//接收FormData  
                    vDic.Add(key, vProvider.FormData[key]);
                }
                string vJsonStr = vDic["Json"];
                if (vJsonStr != null && vJsonStr != "" && vProvider.FileContents.Count > 0)
                {
                    JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
                    UploadFileStruct value = vJSC.Deserialize<UploadFileStruct>(vJsonStr);
                    // 获取流中所有的文件
                    for (int i = 0; i < vProvider.FileContents.Count; i++)
                    {
                        var vFileContent = vProvider.FileContents[i];
                        var vFileInfo = value.Files[i];
                        var vStream = await vFileContent.ReadAsStreamAsync();
                        byte[] vBody = new byte[vStream.Length];
                        vStream.Read(vBody, 0, vBody.Length);
                        FilesManage vFilesManage = new FilesManage();
                        vResult = vFilesManage.AddFile(value.UsersAuthor.UserID, vFileInfo.AreaCode, vFileInfo.UnitName,
                            vFileInfo.FileName, vFileInfo.Author, vBody);
                        if (!vResult)
                        {
                            break;
                        }
                        else
                        {
                            UserOperateLog vUserOperateLog = new UserOperateLog();
                            vUserOperateLog.WriteLog(value.UsersAuthor.UserID, value.UsersAuthor.UserName,string.Format( "上传文件，文件名[{0}]", vFileInfo.FileName));
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return vResult;
        }

        // DELETE: api/UploadFile/5
        public void Delete(int id)
        {
        }
    }
}
