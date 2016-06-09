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
            vUserEF.IsUse = true;
            DataTable vTable =  m_BasicDBClass.SelectRecords(vUserEF);
            if (vTable.Rows.Count > 0)
            {
                CommClass.ConvertDataRowToStruct(ref vUserEF, vTable.Rows[0]);
                vUserEF.LateLoginTime = DateTime.Now;
                vUserEF.Token = DateTime.Now.ToString("mmssyyyyMMddHH");
                if (m_BasicDBClass.UpdateRecord(vUserEF))
                {
                    UserOperateLog vUserOperateLog = new UserOperateLog();
                    vUserOperateLog.WriteLog(vUserEF.ID.Value, vUserEF.UserName, "用户登录");
                    vResult = vUserEF;
                }
            }
            vTable.Clear();
            vTable.Dispose();
            return vResult;
        }

        public bool Logout( string UserName,string Token )
        {
            bool vResult = false;
            UsersEF vUserEF = new UsersEF();
            vUserEF.UserName = UserName;
            vUserEF.Token = Token;
            UsersEF[] vQueryData = m_BasicDBClass.SelectRecordsEx(vUserEF);
            if ( vQueryData.Length > 0 )
            {
                UserOperateLog vLog = new UserOperateLog();
                vLog.WriteLog(vQueryData[0].ID.Value, UserName, "用户退出");
                vResult = true;
            }
            return vResult;
        }

        public bool AddUser( string UserName,string Password,int Power)
        {
            bool vResult = false;
            UsersEF vUserEF = new UsersEF();
            vUserEF.UserName = UserName;
            vUserEF.Password = Password;
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
            if (m_BasicDBClass.UpdateRecord(vUserEF) )
                vResult = true;
            return vResult;
        }


        public UsersEF GetUserInfo( int UserID )
        {
            UsersEF vUserEF = new UsersEF();
            UsersEF vData =  m_BasicDBClass.SelectRecordByPrimaryKeyEx<UsersEF>(UserID);
            return vUserEF;
        }

        public UsersEF GetUserInfo( string UserName )
        {
            UsersEF vUserEF = new UsersEF();
            vUserEF.UserName = UserName;
            UsersEF[] vData = m_BasicDBClass.SelectRecordsEx(vUserEF);
            if (vData.Length > 0)
                vUserEF = vData[0];
            return vUserEF;
        }
        public UsersEF[] OnlineUsersInfo()
        {
            string vSql = string.Format("Select *From Users where DATEDIFF(ss,'{0}',LateLoginTime)>=-60", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//mmssyyyyMMddHH
            UsersEF[] vData =  m_BasicDBClass.SelectCustomEx<UsersEF>(vSql);
            return vData;
        }

        public UsersEF[] GetAllNormalUsers()
        {
            UsersEF vUserEF = new UsersEF();
            vUserEF.IsUse = true;
            return m_BasicDBClass.SelectRecordsEx(vUserEF);
        }

        public static string ConverPowerIDToName( int PowerID )
        {
            string vResult = "";
            switch ( PowerID)
            {
                case 1:
                    vResult = "村民";
                    break;
                case 2:
                    vResult = "村委会";
                    break;
                case 3:
                    vResult = "政府及城建部门";
                    break;
            }
            return vResult;
        }

        /// <summary>
        /// 心跳包
        /// </summary>
        /// <returns></returns>
        public bool HeartBeat( int UserID,string Token)
        {
            bool vResult = false;
            if (checkUserLegal(UserID, Token))
            {
                UsersEF vHeartBeat = new UsersEF();
                vHeartBeat.LateLoginTime = DateTime.Now;
                vResult = m_BasicDBClass.UpdateRecord(vHeartBeat,UserID);
            }
            return vResult;
        }

        /// <summary>
        /// 校验用户合法性
        /// </summary>
        bool checkUserLegal(int UserID,string Token )
        {
            UsersEF vUserInfo = m_BasicDBClass.SelectRecordByPrimaryKeyEx<UsersEF>(UserID);
            return vUserInfo.ID == 0 ? false : true;
        }

        public bool DeleteUses(int UserID )
        {
            bool vResult = false;
            UsersEF vUserEF = new UsersEF();
            vUserEF.ID = UserID;
            vUserEF.IsUse = false;
            vResult = m_BasicDBClass.UpdateRecord(vUserEF);
            return vResult;
        }

    }
}
