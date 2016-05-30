using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageBusiness;
using JXDL.ManageEFModel;

namespace JXDL.Manage.App
{
    public class MapConfigController : ApiController
    {
        // GET: api/MapConfig
        public IEnumerable<string> Get()
        {
            string vReuslt = "";
            MapServer vSystemConfig = new MapServer();
            SystemConfigEF vModel = vSystemConfig.GetMapConfig();
            return new string[] { vModel.ItemValue1, vModel.ItemValue2 };
        }

        // GET: api/MapConfig/5
        public string Get(int ID )
        {
            return "";
        }

        // POST: api/MapConfig
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MapConfig/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MapConfig/5
        public void Delete(int id)
        {
        }
    }
}
