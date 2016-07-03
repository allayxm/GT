using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageBusiness;

namespace JXDL.Manage.App
{
    public class DeleteFileController : ApiController
    {
        // GET: api/DeleteFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DeleteFile/5
        public bool Get(int id, int UserID, string UserName,string FileName)
        {
            FilesManage vFilesManage = new FilesManage();
            bool vResult = vFilesManage.DeleteFile(id);
            UserOperateLog vUserOperateLog = new UserOperateLog();
            vUserOperateLog.WriteLog(UserID, UserName, string.Format("删除文件,文件名:{0}", FileName));
            return vResult;
        }

        // POST: api/DeleteFile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DeleteFile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeleteFile/5
        public void Delete(int id )
        {
           
        }
    }
}
