using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.IntrefaceStruct
{
    public class UploadFileStruct
    {
        public UserAuthorSturct UsersAuthor { get; set; }
        public FileInfo[] Files { get; set; }
    }

    public class FileInfo
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Author { get; set; }
        public string AreaCode { get; set; }
        public string UnitName { get; set; }
        public DateTime UploadTime { get; set; }

    }
}
