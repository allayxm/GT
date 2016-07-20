using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.IntrefaceStruct
{
    public class FileNumberStatisticsStruct
    {
        public string AreaCode { get; set; }
        public string UnitName { get; set; }
        public int FileNumber { get; set; }
    }
    
    public class StatisticsParamtStruct
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string AreaCodes { get; set; }
    }
}
