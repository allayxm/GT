using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JXDL.ManageEFModel;
using MXKJ.DBMiddleWareLib;

namespace JXDL.ManageBusiness
{
    public class AdminManage: Business
    {
        public AdminUserEF Login(string UserName,string Password)
        {
            AdminUserEF vModelEF = new AdminUserEF();
            vModelEF.UserName = UserName;
            vModelEF.Password = Password;
            AdminUserEF[] vResult = m_BasicDBClass.SelectRecordsEx(vModelEF);
            if (vResult.Length > 0)
            {
                vModelEF = vResult[0];
                vModelEF.LateLoginTime = DateTime.Now;
                m_BasicDBClass.UpdateRecord(vModelEF);
            }
            return vModelEF;
        }

        public bool ChangePassword( string UserName, string OldPassword,string NewPassword )
        {
            bool vResult = false;
            AdminUserEF vModelEF = new AdminUserEF();
            vModelEF.UserName = UserName;
            vModelEF.Password = OldPassword;
            AdminUserEF[] vData = m_BasicDBClass.SelectRecordsEx(vModelEF);
            if (vData.Length > 0)
            {
                vModelEF = new AdminUserEF();
                vModelEF.ID = vData[0].ID;
                vModelEF.Password = NewPassword;
                vResult = m_BasicDBClass.UpdateRecord(vModelEF);
            }
            else
                vResult = false;
            return vResult;
        }
    }
}
