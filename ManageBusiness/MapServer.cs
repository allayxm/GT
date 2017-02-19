using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;
using System.Data;
using JXDL.ManageEFModel;
using JXDL.IntrefaceStruct;


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

        public bool SetMapConfig( string MapServerAddress,string MapName,string DBUserName,string DBPassword)
        {
            SystemConfigEF vSystemEF = new SystemConfigEF();
            vSystemEF.ItemValue1 = MapServerAddress;
            vSystemEF.ItemValue2 = MapName;
            vSystemEF.ItemValue3 = DBUserName;
            vSystemEF.ItemValue4 = DBPassword;

            return m_BasicDBClass.UpdateRecord(vSystemEF, "ItemName='MapServer'");
        }

        public DataTable GetLayers()
        {
            return m_BasicDBClass.SelectAllRecords<LayersEFModel>();
        }

        public SymbolEFModel[] GetSymbols()
        {
            return m_BasicDBClass.SelectAllRecordsEx<SymbolEFModel>();
        }
    }
}
