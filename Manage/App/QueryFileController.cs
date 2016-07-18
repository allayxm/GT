using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageEFModel;
using JXDL.ManageBusiness;
using JXDL.IntrefaceStruct;

namespace JXDL.Manage.App
{
    public class QueryFileController : ApiController
    {
        // GET: api/QueryFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/QueryFile/5
        public FileInfo[] Get(int UserID,string UserName,string Township,string VillageCommittee,string Village,string Author,string FileName)
        {
            UserOperateLog vUserOperateLog = new UserOperateLog();
            FileInfo[] vFileInfoArray = null;
            FilesManage vFilesManage = new FilesManage();
            UploadFilesEF[] vQueryResult =  vFilesManage.QueryFile(Township, VillageCommittee, Village, Author, FileName);
            if ( vQueryResult != null )
            {
                vFileInfoArray = new FileInfo[vQueryResult.Length];    
                for( int i =0;i< vFileInfoArray.Length;i++)
                {
                    FileInfo vNewFile = new FileInfo();
                    vNewFile.ID = vQueryResult[i].ID.Value;
                    vNewFile.AreaCode = vQueryResult[i].AreaCode;
                    vNewFile.Author = vQueryResult[i].Author;
                    vNewFile.FileName = vQueryResult[i].FileName;
                    vNewFile.UnitName = vQueryResult[i].UnitName;
                    vNewFile.UploadTime = vQueryResult[i].UploadTime;
                    vFileInfoArray[i] = vNewFile;
                    vUserOperateLog.WriteLog(UserID, UserName,string.Format( "查询管理文档,所属单位:{0} 作者:{1} 文件名:{2}", vNewFile.UnitName, vNewFile.Author, vNewFile.FileName ));
                }
            }
            return vFileInfoArray;
        }

        // POST: api/QueryFile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/QueryFile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QueryFile/5
        public void Delete(int id)
        {
        }
    }
}
