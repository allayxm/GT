using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageEFModel;
using JXDL.ManageBusiness;
using JXDL.IntrefaceStruct;
using System.Web.Script.Serialization;

namespace JXDL.Manage.App
{
    public class GetFileListController : ApiController
    {
        // GET: api/GetFileList
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GetFileList/5
        public string Get(string AreaStrCode)
        {
            //FilesManage vFilesManage = new FilesManage();
            //UploadFilesEF[] vFiles =  vFilesManage.GetFilesFormArea(AreaStrCode);
            string vJson = "";

            //if (vFiles!= null )
            //{
            //    FileInfo[] vFileInfoArray = new FileInfo[vFiles.Length];
            //    for (int i = 0; i < vFileInfoArray.Length; i++)
            //    {
            //        vFileInfoArray[i].ID = vFiles[i].ID.Value;
            //        vFileInfoArray[i].AreaCode = vFiles[i].AreaCode;
            //        vFileInfoArray[i].Author = vFiles[i].Author;
            //        vFileInfoArray[i].FileName = vFiles[i].FileName;
            //        vFileInfoArray[i].UnitName = vFiles[i].UnitName;
            //    }
            //    JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            //    string vResult = vJSC.Serialize(vFiles);
            //}
            return vJson;
        }

        // POST: api/GetFileList
        public string Post([FromBody]string[] AreaCodeArray)
        {
            FilesManage vFilesManage = new FilesManage();
            UploadFilesEF[] vFiles = vFilesManage.GetFilesFormArea(AreaCodeArray);
            string vJson = "";
            if (vFiles != null)
            {
                FileInfo[] vFileInfoArray = new FileInfo[vFiles.Length];
                for (int i = 0; i < vFileInfoArray.Length; i++)
                {
                    vFileInfoArray[i].ID = vFiles[i].ID.Value;
                    vFileInfoArray[i].AreaCode = vFiles[i].AreaCode;
                    vFileInfoArray[i].Author = vFiles[i].Author;
                    vFileInfoArray[i].FileName = vFiles[i].FileName;
                    vFileInfoArray[i].UnitName = vFiles[i].UnitName;
                }
                JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
                vJSC.MaxJsonLength = Int32.MaxValue;
                vJson = vJSC.Serialize(vFileInfoArray);
            }
            return vJson;
        }

        // PUT: api/GetFileList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetFileList/5
        public void Delete(int id)
        {
        }
    }
}
