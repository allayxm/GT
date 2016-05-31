using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.ManageBusiness;
using JXDL.ManageEFModel;
using System.Web.Script.Serialization;

namespace JXDL.Manage.App_Start
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public UsersEF Get(string UserName,string Password)
        {
            UserManage vUserManage = new UserManage();
            UsersEF vUsers =  vUserManage.Login(UserName, Password);
            //JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string vResult= vJSC.Serialize(vUsers);
            return vUsers;
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
