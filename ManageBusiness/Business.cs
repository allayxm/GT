using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;

namespace JXDL.ManageBusiness
{
    public class Business
    {
       protected BasicDBClass m_BasicDBClass = null;
       public Business()
        {
            m_BasicDBClass = new BasicDBClass( DataBaseType.SqlServer);
        }

    }
}
