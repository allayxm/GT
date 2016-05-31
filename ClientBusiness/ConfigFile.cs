using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace JXDL.ClientBusiness
{
    public class ConfigFile
    {
        #region 私有变量

        Configuration m_Configuration = null;
        public string RemotingServerAddress { get; set; }
        
        #endregion

        #region 构造
        public ConfigFile()
        {
            m_Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //远程服务器
            RemotingServerAddress = m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value;
        }
        #endregion

        #region 公有方法
        public void Save()
        {
            //远程服务器
            m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value = RemotingServerAddress;

            m_Configuration.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        
    }
}
