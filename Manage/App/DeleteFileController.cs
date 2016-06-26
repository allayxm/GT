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
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id,int UserID,string UserName )
        {
            FilesManage vFilesManage = new FilesManage();
            vFilesManage.DeleteFile(id);
            UserOperateLog vUserOperateLog = new UserOperateLog();
            vUserOperateLog.WriteLog(UserID, UserName,string.Format("删除编号为{0}的文件",id));
        }
    }
}
