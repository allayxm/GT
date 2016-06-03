using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageBusiness;

namespace JXDL.Manage.App
{
    public class LogoutController : ApiController
    {
        // GET: api/Logout
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Logout/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Value 1：用户名 2：Token
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Logout
        public bool Post([FromBody]string[] value)
        {
            UserManage vUserManage = new UserManage();
            return vUserManage.Logout(value[0], value[1]);
        }

        // PUT: api/Logout/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Logout/5
        public void Delete(int id)
        {
        }
    }
}
