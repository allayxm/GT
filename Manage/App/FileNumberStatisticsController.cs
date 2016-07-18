using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using JXDL.IntrefaceStruct;
using JXDL.ManageBusiness;
using MXKJ.DBMiddleWareLib;

namespace JXDL.Manage.App
{
    public class FileNumberStatisticsController : ApiController
    {
        // GET: api/FileNumberStatistics
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileNumberStatistics/5
        public FileNumberStatisticsStruct[] Get(string AreaCodes)
        {
            AreaCodes = System.Web.HttpUtility.UrlDecode(AreaCodes);
            Statistics vStatistics = new Statistics();
            return vStatistics.FileNumberStatistics(AreaCodes);
        }

        // POST: api/FileNumberStatistics
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FileNumberStatistics/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FileNumberStatistics/5
        public void Delete(int id)
        {
        }
    }
}
