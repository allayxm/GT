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
        public LogEF[] GetAllLog()
        {
            LogEF[] vData= m_BasicDBClass.SelectAllRecordsEx<LogEF>("ID desc","*");
            return vData;
        }

        public bool WriteLog( int UserID,string UserName,string Type)
        {
            LogEF vLogEF = new LogEF();
            vLogEF.UserID = UserID;
            vLogEF.UserName = UserName;
            vLogEF.Type = Type;
            vLogEF.OperateTime = DateTime.Now;
            if (m_BasicDBClass.InsertRecord(vLogEF) > 0)
                return true;
            else
                return false;
        }
    }
}
