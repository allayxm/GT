using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageEFModel;
using JXDL.ManageBusiness;

namespace JXDL.Manage.App
{
    public class HeartbeatController : ApiController
    {
        // GET: api/Heartbeat
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/Heartbeat/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Heartbeat
        public void Post([FromBody]UsersEF UserInfo)
        {
          
        }

        // PUT: api/Heartbeat/5
        public bool Put([FromBody]UsersEF UserInfo)
        {
            UserManage vUserManage = new UserManage();
            return vUserManage.HeartBeat(UserInfo.ID.Value, UserInfo.Token);
        }

        // DELETE: api/Heartbeat/5
        public void Delete(int id)
        {
        }
    }
}
