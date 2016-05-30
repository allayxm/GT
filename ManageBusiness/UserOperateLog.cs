using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MXKJ.DBMiddleWareLib;
using JXDL.ManageEFModel;

namespace JXDL.ManageBusiness
{
    public class UserOperateLog:Business
    {
        public List<LogEF> GetAllLog()
        {
            List<LogEF> vResult = new List<LogEF>();
            LogEF[] vData= m_BasicDBClass.SelectAllRecordsEx<LogEF>();
            vResult.AddRange(vData);
            return vResult;
        }
    }
}
