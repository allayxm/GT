using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using JXDL.ManageEFModel;

namespace JXDL.ManageBusiness
{
    public class FilesManage: Business
    {
        public bool AddFile( int UserID,string AreaCode, string FileName,string Author,byte[] FileBody )
        {
            UploadFilesEF vModelEF = new UploadFilesEF();
            vModelEF.UserID = UserID;
            vModelEF.AreaCode = AreaCode;
            vModelEF.FileName = FileName;
            vModelEF.UploadTime = DateTime.Now;
            vModelEF.Author = Author;
            vModelEF.Body = FileBody;
            vModelEF.Length = FileBody.Length;
            return m_BasicDBClass.InsertRecord(vModelEF)>0?true:false ;
        }

        public bool DeleteFile( int FileID )
        {
            bool vResult = m_BasicDBClass.DeleteRecordByPrimaryKey<UploadFilesEF>(FileID);
            return vResult;
        }

        public bool UpdateFileInfo( int FileID,string Authorh )
        {
            UploadFilesEF vModelEF = new UploadFilesEF();
            vModelEF.Author = Authorh;
            return m_BasicDBClass.UpdateRecord(vModelEF, FileID);
        }

        public UploadFilesEF[] GetFilesFormArea( string AreaCode )
        {
            UploadFilesEF vModelEF = new UploadFilesEF();
            vModelEF.AreaCode = AreaCode;
            return m_BasicDBClass.SelectRecordsEx(vModelEF);
        }

        public UploadFilesEF GetFileByID( int FileID )
        {
            return m_BasicDBClass.SelectRecordByPrimaryKeyEx<UploadFilesEF>(FileID);
        }


    }
}
