using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using JXDL.ManageBusiness;
using JXDL.ManageEFModel;

namespace JXDL.Manage.App
{
    public class DownloadFileController : ApiController
    {
        // GET: api/DownloadFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DownloadFile/5
        public HttpResponseMessage Get(int FileID)
        {
            try
            {
                FilesManage vFilesManage = new FilesManage();
                UploadFilesEF vUploadFile = vFilesManage.GetFileByID(FileID);
                if (vUploadFile.ID != 0)
                {
                    //var FilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"C:/");
                    //var stream = new FileStream(FilePath, FileMode.Open);
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    MemoryStream vStream = new MemoryStream(vUploadFile.Body);
                    response.Content = new StreamContent(vStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = vUploadFile.FileName
                    };
                    return response;
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        // POST: api/DownloadFile
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/DownloadFile/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/DownloadFile/5
        public void Delete(int id)
        {
        }
    }
}
