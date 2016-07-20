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
        public FileNumberStatisticsStruct[] Get(int UserID, string UserName,string AreaCodes)
        {
            AreaCodes = System.Web.HttpUtility.UrlDecode(AreaCodes);
            UserOperateLog vUserOperateLog = new UserOperateLog();
            string vAreaName = "";
            string[] vAreaArray = AreaCodes.Split('|');
            foreach (string vTempAera in vAreaArray)
            {
                string[] vSplitArea = vTempAera.Split(',');
                if (vSplitArea.Length == 2)
                    vAreaName += vSplitArea[1]+"、";
            }
            vUserOperateLog.WriteLog(UserID, UserName, string.Format("统计数据,区域包括：{0}", vAreaName));
            Statistics vStatistics = new Statistics();
            return vStatistics.FileNumberStatistics(AreaCodes);
        }

        // POST: api/FileNumberStatistics
        public FileNumberStatisticsStruct[] Post([FromBody]StatisticsParamtStruct QueryParam)
        {
            string AreaCodes = System.Web.HttpUtility.UrlDecode(QueryParam.AreaCodes);
            UserOperateLog vUserOperateLog = new UserOperateLog();
            string vAreaName = "";
            string[] vAreaArray = AreaCodes.Split('|');
            foreach (string vTempAera in vAreaArray)
            {
                string[] vSplitArea = vTempAera.Split(',');
                if (vSplitArea.Length == 2)
                    vAreaName += vSplitArea[1] + "、";
            }
            vUserOperateLog.WriteLog(QueryParam.UserID, QueryParam.UserName, string.Format("统计数据,区域包括：{0}", vAreaName));
            Statistics vStatistics = new Statistics();
            return vStatistics.FileNumberStatistics(AreaCodes);
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
