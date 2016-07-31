using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;

namespace JXDL.ManageEFModel
{
    [TableAttrib("Features", "ID")]
    public struct FeaturesEFModel
    {
        [ColumnAttrib("ID")]
        public int? ID { get; set; }
        [ColumnAttrib("TableName")]
        public String TableName { get; set; }
        [ColumnAttrib("SerialNumber")]
        public double? SerialNumber { get; set; }
        [ColumnAttrib("FeatureName")]
        public String FeatureName { get; set; }
        [ColumnAttrib("Code")]
        public double? Code
        {
            get; set;
        }
    }
}
