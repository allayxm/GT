using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MXKJ.DBMiddleWareLib;
using JXDL.IntrefaceStruct;

namespace JXDL.ManageBusiness
{
    public class Statistics
    {
        BasicDBClass m_BasicDBClass;
        public Statistics()
        {
            m_BasicDBClass = new BasicDBClass( DataBaseType.SqlServer);
        }
        public FileNumberStatisticsStruct[] FileNumberStatistics(string AreaStr)
        {
            List<FileNumberStatisticsStruct> vResult = new List<FileNumberStatisticsStruct>();
            string[] vAreaArray = AreaStr.Split('|');
            foreach (string vTempArea in vAreaArray)
            {
                string[] vAreaSplit =  vTempArea.Split(',');
                string vAreaCode = "",vAreaName="";
                if (vAreaSplit.Length==2)
                {
                    vAreaCode = vAreaSplit[0];
                    vAreaName = vAreaSplit[1];
                    string vSql = string.Format("Select count(*)From UploadFiles where AreaCode like '{0}%'", vAreaCode);
                    DataTable vResultTable = m_BasicDBClass.SelectCustom(vSql);
                    int vFileNumber = DBConvert.ToInt32(vResultTable.Rows[0][0]).Value;
                    FileNumberStatisticsStruct vNewAreaResult = new FileNumberStatisticsStruct()
                    {
                        AreaCode = vAreaCode,
                        UnitName = vAreaName,
                         FileNumber = vFileNumber
                    };
                    vResult.Add(vNewAreaResult);
                }
            }
            return vResult.ToArray();
        }

     
    }
    
}
