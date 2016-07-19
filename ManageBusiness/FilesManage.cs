using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JXDL.ManageEFModel;
using JXDL.IntrefaceStruct;

namespace JXDL.ManageBusiness
{
    public class FilesManage: Business
    {

        public bool AddFile( int UserID,string AreaCode,string UnitName, 
            string FileName,string Author,byte[] FileBody )
        {
            UploadFilesEF vModelEF = new UploadFilesEF();
            vModelEF.UserID = UserID;
            vModelEF.AreaCode = AreaCode;
            vModelEF.UnitName = UnitName;
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

        public UploadFilesEF[] GetFilesFormArea( string[] AreaCodeArray )
        {
            UploadFilesEF[] vResult = null;
            if (AreaCodeArray != null)
            {
                string vAreaStrCode = "";
                foreach( string vTempArray in AreaCodeArray)
                {
                    vAreaStrCode += string.Format("'{0}',", System.Web.HttpUtility.UrlDecode(vTempArray));// System.Web.HttpUtility.UrlDecode(vTempArray) + ",";
                }
                if (vAreaStrCode != "")
                    vAreaStrCode = vAreaStrCode.Remove(vAreaStrCode.Length - 1);
                vResult = m_BasicDBClass.SelectCustomEx<UploadFilesEF>( string.Format( "Select ID,AreaCode,UnitName,[FileName],UserID,UploadTime,Author From UploadFiles where AreaCode in ({0}) order by AreaCode, UnitName", vAreaStrCode) );
            }
            return vResult;
        }

        public UploadFilesEF[] QueryFile(string Township, string VillageCommittee, string Village,
            string Author, string FileName)
        {
            UploadFilesEF[] vResult = null;
            string vAreaCode = "";
            if (Township!=null && Township != "")
                vAreaCode = Township;
            if (VillageCommittee!=null && VillageCommittee != "")
                vAreaCode = VillageCommittee;
            if (Village!=null && Village != "")
                vAreaCode = Village;
            string vSql = "Select ID,AreaCode,UnitName,[FileName],UserID,UploadTime,Author From UploadFiles";
            string vSqlCondition = "";
            if (vAreaCode!=null && vAreaCode != "")
                vSqlCondition += string.Format("AreaCode like '{0}%'", vAreaCode);
            if (Author!=null && Author != "")
            {
                if (vSqlCondition=="")
                    vSqlCondition += string.Format("Author like '%{0}%'", Author);
                else
                    vSqlCondition += string.Format(" and Author like '%{0}%'", Author);
            }
            if (FileName!=null && FileName != "")
            {
                if ( vSqlCondition=="")
                    vSqlCondition += string.Format("FileName like '%{0}%'", FileName);
                else
                    vSqlCondition += string.Format(" and FileName like '%{0}%'", FileName);
            }
            if (vSqlCondition != "")
                vSql += " Where "+vSqlCondition;
            vResult = m_BasicDBClass.SelectCustomEx<UploadFilesEF>(vSql);
            return vResult;
        }

        public UploadFilesEF GetFileByID( int FileID )
        {
            return m_BasicDBClass.SelectRecordByPrimaryKeyEx<UploadFilesEF>(FileID);
        }


    }
}
