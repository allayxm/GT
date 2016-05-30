using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;
using JXDL.ManageEFModel;


namespace JXDL.ManageBusiness
{
    public class MapServer: Business
    {
        
        public SystemConfigEF GetMapConfig()
        {
            SystemConfigEF vSystemEF = new SystemConfigEF();
            vSystemEF.ItemName = "MapServer";
            SystemConfigEF[] vData = m_BasicDBClass.SelectRecordsEx(vSystemEF);
            if (vData.Length > 0)
                vSystemEF = vData[0];
            return vSystemEF;
        }

        public bool UpdateMapConfig( string MapServerAddress,string MapName)
        {
            SystemConfigEF vSystemEF = new SystemConfigEF();
            vSystemEF.ItemValue1 = MapServerAddress;
            vSystemEF.ItemValue2 = MapName;
            return m_BasicDBClass.UpdateRecord(vSystemEF, "ItemName='MapServer'");
        }
    }
}
