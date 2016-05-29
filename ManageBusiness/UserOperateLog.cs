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
    public class UserOperateLog
    {
        BasicDBClass m_BasicDBClass = null;
        public UserOperateLog()
        {
            m_BasicDBClass = new BasicDBClass( DataBaseType.SqlServer);
        }


        public List<Log> GetAllLog()
        {
            List<Log> vResult = new List<Log>();
            DataTable vTable = m_BasicDBClass.SelectAllRecords<Log>();
            if ( vTable.Rows.Count > 0 )
            {
                vResult = CommClass.ConvertDataTableToList<Log>(vTable);
            }
            vTable.Clear();
            vTable.Dispose();
            return vResult;
        }
    }
}
