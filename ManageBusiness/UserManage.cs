using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MXKJ.DBMiddleWareLib;
using JXDL.ManageEFModel;

namespace JXDL.ManageBusiness
{
    public class UserManage
    {
        BasicDBClass m_BasicDBClass = null;
        public UserManage()
        {
            m_BasicDBClass = new BasicDBClass(DataBaseType.SqlServer);
        }

        public Users Login( string UserName,string Password )
        {
            Users vResult = new Users();
            Users vUserEF = new Users();
            vUserEF.UserName = UserName;
            vUserEF.Password = Password;
            DataTable vTable =  m_BasicDBClass.SelectRecords(vUserEF);
            if (vTable.Rows.Count > 0)
            {
                CommClass.ConvertDataRowToStruct(ref vUserEF, vTable.Rows[0]);
                vUserEF.LateLoginTime = DateTime.Now;
                vUserEF.Token = DateTime.Now.ToString("mmssyyyyMMddHH");
                if (m_BasicDBClass.UpdateRecord(vUserEF))
                    vResult = vUserEF;
            }
            vTable.Clear();
            vTable.Dispose();
            return vResult;
        }

        public bool AddUser( string UserName,string Password,int Power)
        {
            bool vResult = false;
            Users vUserEF = new Users();
            vUserEF.UserName = UserName;
            vUserEF.Password = Password;
            vUserEF.LateLoginTime = DateTime.Now;
            vUserEF.Power = Power;
            vUserEF.IsUse = true;
            if (m_BasicDBClass.InsertRecord(vUserEF) > 0 )
                vResult = true;
            return vResult;
        }

        public bool UpdateUser( int UserID,string Password,int Power)
        {
            bool vResult = false;
            Users vUserEF = new Users();
            vUserEF.ID = UserID;
            vUserEF.Password = Password;
            
            vUserEF.Power = Power;
            vUserEF.IsUse = true;
            if (m_BasicDBClass.InsertRecord(vUserEF) > 0)
                vResult = true;
            return vResult;
        }

        public Users GetUserInfo( int UserID )
        {
            Users vUserEF = new Users();
            vUserEF.ID = UserID;
            DataTable vTable =  m_BasicDBClass.SelectRecords(vUserEF);
            if (vTable.Rows.Count > 0)
                CommClass.ConvertDataRowToStruct(ref vUserEF, vTable.Rows[0]);
            vTable.Clear();
            vTable.Dispose();
            return vUserEF;
        }
        public List<Users> OnlineUsersInfo()
        {
            List<Users> vResult = new List<Users>();
            DataTable vTable =  m_BasicDBClass.SelectCustom("Select *From Users where DATEDIFF(ss,LateLoginTime,'2016-05-23 22:18')>=-60");
            if ( vTable.Rows.Count > 0 )
            {

            }
            return vResult;
        }
    }
}
