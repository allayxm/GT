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
    public class UserManage: Business
    {
        public UsersEF Login( string UserName,string Password )
        {
            UsersEF vResult = new UsersEF();
            UsersEF vUserEF = new UsersEF();
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
            UsersEF vUserEF = new UsersEF();
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
            UsersEF vUserEF = new UsersEF();
            vUserEF.ID = UserID;
            vUserEF.Password = Password;
            
            vUserEF.Power = Power;
            vUserEF.IsUse = true;
            if (m_BasicDBClass.InsertRecord(vUserEF) > 0)
                vResult = true;
            return vResult;
        }

        public UsersEF GetUserInfo( int UserID )
        {
            UsersEF vUserEF = new UsersEF();
            vUserEF.ID = UserID;
            DataTable vTable =  m_BasicDBClass.SelectRecords(vUserEF);
            if (vTable.Rows.Count > 0)
                CommClass.ConvertDataRowToStruct(ref vUserEF, vTable.Rows[0]);
            vTable.Clear();
            vTable.Dispose();
            return vUserEF;
        }
        public List<UsersEF> OnlineUsersInfo()
        {
            List<UsersEF> vResult = new List<UsersEF>();
            UsersEF[] vData =  m_BasicDBClass.SelectCustomEx<UsersEF>("Select *From Users where DATEDIFF(ss,LateLoginTime,'2016-05-23 22:18')>=-60");
            vResult.AddRange(vData);
            return vResult;
        }

        public UsersEF[] GetAllNormalUsers()
        {
            UsersEF vUserEF = new UsersEF();
            vUserEF.IsUse = false;
            return m_BasicDBClass.SelectRecordsEx(vUserEF);
        }

        /// <summary>
        /// 心跳包
        /// </summary>
        /// <returns></returns>
        public bool HeartBeat( string UserNama,string Token)
        {
            bool vResult = false;
            UsersEF vUserEF = new UsersEF();
            vUserEF.UserName = UserNama;
            vUserEF.Token = Token;
            UsersEF[] vData = m_BasicDBClass.SelectRecordsEx(vUserEF);
            if ( vData.Length > 0 )
            {
                UsersEF vHeartBeat = new UsersEF();
                vHeartBeat.LateLoginTime = DateTime.Now;
                vResult = m_BasicDBClass.UpdateRecord(vHeartBeat, vData[0].ID);
            }
            return vResult;
        }

    }
}
