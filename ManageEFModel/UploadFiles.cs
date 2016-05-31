using System;
using MXKJ.DBMiddleWareLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ManageEFModel
{
    [TableAttrib("UploadFiles", "ID")]
    public struct UploadFilesEF
    {
        [ColumnAttrib("ID")]
        public int? ID { get; set; }
        [ColumnAttrib("ObjectID")]
        public int? ObjectID { get; set; }
        [ColumnAttrib("FileName")]
        public String FileName { get; set; }
        [ColumnAttrib("UserID")]
        public int? UserID { get; set; }
        [ColumnAttrib("UploadTime")]
        public DateTime UploadTime { get; set; }
        [ColumnAttrib("Author")]
        public String Author { get; set; }
        [ColumnAttrib("Body")]
        public byte[] Body { get; set; }
        [ColumnAttrib("Length")]
        public int? Length { get; set; }
    }

}
