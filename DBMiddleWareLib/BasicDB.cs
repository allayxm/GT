using System;
using System.Reflection;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace MXKJ.DBMiddleWareLib
{
    #region 数据基类
    public class BasicDBClass:IDisposable
    {
        #region 数据库参数
        public static string DataSource = string.Empty;
        public static string DBName = string.Empty;
        public static string UserID = string.Empty;
        public static string Password = string.Empty;
        public static bool IntegratedSecurity = false;
        public static string AttachDbFilename = string.Empty;
        public static int PoolSize = 100;
        public static int Timeout = 30;

        protected static DbConnection m_DbConnection;
        protected static DbCommand m_DbCommand;
        protected static DbDataAdapter m_DbDataAdapter;
        protected string[] m_TableList = null;
        public string[] TableList
        {
            get
            {
                return m_TableList;
            }
        }
        protected string[] m_TableViewList = null;
        public string[] TableViewList
        {
            get
            {
                return m_TableViewList;
            }
        }
        #endregion

        #region 私有变量
        /// <summary>
        /// 表名
        /// </summary>
        protected string m_TableName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        //protected string m_TableViewName = string.Empty; 
        /// <summary>
        /// 是否为视图
        /// </summary>
        protected bool m_IsTableView = false;
        /// <summary>
        /// 主键值
        /// </summary>
        protected string m_PrimaryKey = string.Empty;
        protected static DataBaseType m_DataBaseType = DataBaseType.SqlServer;
        #endregion

        #region 属性
        protected bool m_IsUserView = true;
        public bool IsUserView
        {
            set
            {
                m_IsUserView = value;
            }

            get
            {
                return m_IsUserView;
            }
        }
        #endregion

        #region 构造
        public BasicDBClass( DataBaseType DBTypeValue )
        {
            m_DataBaseType = DBTypeValue;
            if (m_DbConnection == null)
                InitConnection();
            FillTableList();
            FillTableViewList();
            
        }
        #endregion

        #region 事务
        public void TransactionBegin()
        {
            if (m_DbCommand.Transaction == null && OpenConnection())
            {
                SqlTransaction transaction = (SqlTransaction)m_DbConnection.BeginTransaction();
                m_DbCommand.Transaction = transaction;
            }
        }

        public void TransactionCommit()
        {
            if (m_DbCommand.Transaction != null)
            {
                m_DbCommand.Transaction.Commit();
                m_DbCommand.Transaction = null;
            }
        }

        public void TransactionRollback()
        {
            if (m_DbCommand.Transaction != null)
            {
                m_DbCommand.Transaction.Rollback();
                m_DbCommand.Transaction = null;
            }
        }

        #endregion

        #region 公有方法
        public bool SaveDataTableToDB<T>(DataTable DataSource) where T : new()
        {
            bool vResult = false;
            if (DataSource != null && DataSource.Rows.Count > 0)
            {
                TransactionBegin();
                foreach (DataRow vTempRow in DataSource.Rows)
                {
                    int vID = 0;
                    T vRecord;
                    switch (vTempRow.RowState)
                    {
                        case DataRowState.Added:
                            vRecord = new T();
                            CommClass.ConvertDataRowToStruct(ref vRecord, vTempRow);
                            vResult = InsertRecord(vRecord) == -1 ? false : true ;
                            break;
                        case DataRowState.Modified:
                            vRecord = new T();
                            CommClass.ConvertDataRowToStruct(ref vRecord, vTempRow);
                            vID = (int)vTempRow["ID"];
                            vResult = UpdateRecord(vRecord, vID);
                            break;
                        case DataRowState.Deleted:
                            vTempRow.RejectChanges();
                            vID = (int)vTempRow["ID"];
                            vResult = DeleteRecordByPrimaryKey<T>(vID);
                            vTempRow.Delete();
                            break;
                        default:
                            vResult = true;
                            break;
                    }
                    if (!vResult)
                        break;
                }
                if (vResult)
                    TransactionCommit();
                else
                    TransactionRollback();
            }
            return vResult;
        }

        public bool TestConnection()
        {
            if (m_DbConnection != null)
            {

                if (m_DbConnection.State == ConnectionState.Open)
                    return true;
                else
                {
                    try
                    {
                        m_DbConnection.Open();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
                return false;

        }

        public static void InitConnection()
        {
            string sqlConn = "";
            
            switch (m_DataBaseType)
            {
                case DataBaseType.SqlServer:
                    if (AttachDbFilename != null && AttachDbFilename != "" && AttachDbFilename != string.Empty)
                        sqlConn = string.Format("Data Source={0};AttachDbFilename={1};Integrated Security={2};Connect Timeout={3}",DataSource, AttachDbFilename, IntegratedSecurity,Timeout);
                    else if (!IntegratedSecurity)
                        sqlConn = string.Format("data source={0};user id={1};passWord={2};initial Catalog={3};Connect Timeout={4}", DataSource, UserID,
                         Password, DBName,Timeout);
                    else
                        sqlConn = string.Format("data source={0};initial Catalog={3};Integrated Security={4};Connect Timeout={5}", DataSource, UserID,
                         Password, DBName, IntegratedSecurity,Timeout);
                    
                    //sqlConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\用户目录\我的文档\YXMDB.mdf;Integrated Security=True;Connect Timeout=30";
                    m_DbConnection = new SqlConnection(sqlConn);
                    m_DbCommand = new SqlCommand();
                    m_DbCommand.Connection = m_DbConnection;
                    m_DbDataAdapter = new SqlDataAdapter((SqlCommand)m_DbCommand);
                    break;
                case DataBaseType.Access:
                    if (Password!=string.Empty || Password != "" )
                        sqlConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1};", DataSource, Password);
                    else
                        sqlConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=True", DataSource);
                    m_DbConnection = new OleDbConnection(sqlConn);
                    m_DbCommand = new OleDbCommand();
                    m_DbCommand.Connection = m_DbConnection;
                    m_DbDataAdapter = new OleDbDataAdapter((OleDbCommand)m_DbCommand);
                    break;
                case DataBaseType.MySql:
                    sqlConn = string.Format("Server={0};Database={1};User={2};Password={3};Use Procedure Bodies=false;"
                        +"Charset=utf8;Allow Zero Datetime=True; Pooling=true; Max Pool Size={4}; ",DataSource, DBName,UserID,Password,PoolSize);
                    m_DbConnection = new OleDbConnection(sqlConn);
                    m_DbCommand = new OleDbCommand();
                    m_DbCommand.Connection = m_DbConnection;
                    m_DbDataAdapter = new OleDbDataAdapter((OleDbCommand)m_DbCommand);
                    break;
                case DataBaseType.Oracle:
                    sqlConn = string.Format("user id={0};data source={1};password={2}", UserID,DataSource,Password);
                    m_DbConnection = new OracleConnection(sqlConn);
                    m_DbCommand = new OracleCommand();
                    m_DbCommand.Connection = m_DbConnection;
                    m_DbDataAdapter = new OracleDataAdapter((OracleCommand)m_DbCommand);
                    break;
            }
            
        }

        public void FillTableViewList()
        {
            switch (m_DataBaseType)
            {
                case DataBaseType.SqlServer:
                    m_DbCommand.CommandText = "Select name From sysobjects Where xtype='v'";
                    break;
                case DataBaseType.Access:
                    m_DbCommand.CommandText = "Select name from MSysObjects where type=5 and flags=0";
                    break;
                case DataBaseType.MySql:
                    //m_DbCommand.CommandText = "Select table_name as name from information_schema.tables where table_schema='csdb' and table_type='base table'";
                    m_DbCommand.CommandText = "select * from information_schema.VIEWS";
                    break;
                case DataBaseType.Oracle:
                    m_DbCommand.CommandText = "select * from user_views";
                    break;
            }

            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                DataSet vDataSet = new DataSet();
                DataTable vDataTable = new DataTable();
                m_DbDataAdapter.Fill(vDataSet);
                if (vDataSet.Tables.Count > 0)
                    vDataTable = vDataSet.Tables[0];
                if (vDataSet.Tables.Count > 0 && vDataTable.Rows.Count > 0)
                {
                    List<string> vTableViewList = new List<string>();
                    foreach (DataRow vTempRow in vDataTable.Rows)
                    {
                        string vTableName = vTempRow["Name"].ToString();
                        vTableViewList.Add(vTableName);
                    }
                    m_TableViewList = vTableViewList.ToArray();
                }
            }
        }


        public void FillTableList()
        {
            switch ( m_DataBaseType )
            {
                case DataBaseType.SqlServer:
                    m_DbCommand.CommandText = "Select name From sysobjects Where xtype='u'";
                    break;
                case DataBaseType.Access:
                    m_DbCommand.CommandText = "Select name from MSysObjects where type=1 and flags=0";
                    break;
                case DataBaseType.MySql:
                    //m_DbCommand.CommandText = "Select table_name as name from information_schema.tables where table_schema='csdb' and table_type='base table'";
                    m_DbCommand.CommandText = "select * from information_schema.VIEWS";
                    break;
                case DataBaseType.Oracle:
                    m_DbCommand.CommandText = "Select * from tab";
                    break;
            }
            
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                DataSet vDataSet = new DataSet();
                DataTable vDataTable = new DataTable();
                m_DbDataAdapter.Fill(vDataSet);
                if (vDataSet.Tables.Count > 0)
                    vDataTable = vDataSet.Tables[0];
                if (vDataSet.Tables.Count > 0 && vDataTable.Rows.Count > 0)
                {
                    List<string> vTableNameList = new List<string>();
                    foreach (DataRow vTempRow in vDataTable.Rows)
                    {
                        string vTableName = vTempRow["Name"].ToString();
                        vTableNameList.Add(vTableName);
                    }
                    m_TableList = vTableNameList.ToArray();
                }
            }
        }

        public string TableName
        {
            set
            {
                if (TableIsExist(value))
                    m_TableName = "[" + value + "]";
            }
            get
            {
                return m_TableName;
            }
        }

        //public string TableViewName
        //{
        //    set
        //    {
        //        if (TableIsExist(value))
        //            m_TableViewName = "[" + value + "]";
        //    }
        //    get
        //    {
        //        return m_TableViewName;
        //    }
        //}

        public virtual DataTable SelectRecords<T>(T Record, string Sort, string Columns)
        {
            DataSet resultDataSet = new DataSet();
            DataTable resultTable = new DataTable(m_TableName);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            if (Columns == "")
                MarkSelectSql(Record, out DbCommand, "", out DbParameters, Sort, null);
            else
            {
                string[] ColumnsArray = Columns.Split(',');
                MarkSelectSql(Record, out DbCommand, "", out DbParameters, Sort, ColumnsArray);
            }

            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);

                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultDataSet, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultDataSet);
                }
            }
            if (resultDataSet.Tables.Count > 0)
                resultTable = resultDataSet.Tables[0];
            return resultTable;
        }

        #region SelectRecordsEx
        public virtual T[] SelectRecordsEx<T>(T Record, string Sort, string Columns) where T:new()
        {
            T[] resultArray = new T[0];
            DataSet resultDataSet = new DataSet();
            DataTable resultTable = new DataTable(m_TableName);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            if (Columns == "")
                MarkSelectSql(Record, out DbCommand, "", out DbParameters, Sort, null);
            else
            {
                string[] ColumnsArray = Columns.Split(',');
                MarkSelectSql(Record, out DbCommand, "", out DbParameters, Sort, ColumnsArray);
            }

            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);

                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultDataSet, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultDataSet);
                }
            }
            if (resultDataSet.Tables.Count > 0)
            {
                resultTable = resultDataSet.Tables[0];
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            //T[] recordTArray = new T[resultTable.Rows.Count];
            //for( int i=0;i< recordTArray.Length;i++)
            //{
            //    recordTArray[i] = ConvertDataRowToStruct<T>(resultTable.Rows[i]);
            //}
            
            return resultArray;

        }

        public virtual T[] SelectRecordsEx<T>(T Record) where T : new()
        {
            T[] resultArray = new T[0];
            DataTable resultTable = new DataTable(m_TableName);
            initTableParam(Record);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            MarkSelectSql(Record, out DbCommand, "", out DbParameters, "", null);
            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T[] SelectRecordsEx<T>(T Record, string CustomCondition, string Sort, string Columns) where T : new()
        {
            T[] resultArray = new T[0];
            DataTable resultTable = new DataTable(m_TableName);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            if (Columns == "")
                MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, Sort, null);
            else
            {
                string[] ColumnsArray = Columns.Split(',');
                MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, Sort, ColumnsArray);
            }

            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T[] SelectRecordsEx<T>(T Record, string CustomCondition) where T : new()
        {
            T[] resultArray = new T[0];
            DataTable resultTable = new DataTable(m_TableName);
            initTableParam(Record);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, "", null);
            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T[] SelectCustomEx<T>(string SelectSql) where T : new()
        {
            T[] resultArray = new T[0];
            DataTable resultTable = new DataTable();
            if (SelectSql != null && SelectSql != "" && SelectSql != string.Empty)
            {
                m_DbCommand.CommandText = SelectSql;
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T[] SelectAllRecordsEx<T>(string Sort, string Columns) where T : new()
        {
            T[] resultArray = new T[0];
            initTableParam(new T());
            DataTable resultTable = new DataTable(m_TableName);
            string vSqlColumns = "*";
            if (Columns != null && Columns != "")
                vSqlColumns = Columns;
            if (Sort != null && Sort != "")
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Order By {2}", Columns, getTableName(), Sort);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1}", Columns, getTableName());
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                m_DbDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T[] SelectAllRecordsEx<T>() where T : new()
        {
            T[] resultArray = new T[0];
            initTableParam(new T());
            DataTable resultTable = new DataTable(m_TableName);
            m_DbCommand.CommandText = string.Format("Select *From {0}", getTableName());
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                m_DbDataAdapter.Fill(resultTable);
            }
            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable);
            }
            return resultArray;
        }

        public virtual T SelectRecordByPrimaryKeyEx<T>(object PrimaryKeyValue) where T : new()
        {
            T resultArray = new T();
            initTableParam(new T());
            DataTable resultTable = new DataTable();
            m_DbCommand.CommandText = string.Format("Select *From {0} Where {1}=@PrimaryKeyValue", m_TableName, m_PrimaryKey);
            m_DbCommand.Parameters.Clear();

            DbParameter newPrimaryKeyParam = CreateDbParameter();
            newPrimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newPrimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newPrimaryKeyParam.Value = PrimaryKeyValue;
            m_DbCommand.Parameters.Add(newPrimaryKeyParam);

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                m_DbDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable)[0];
            }
            return resultArray;
        }

        public virtual T SelectRecordByPrimaryKeyEx<T>(object PrimaryKeyValue, string Columns) where T : new()
        {
            T resultArray = new T();
            initTableParam(new T());
            DataTable resultTable = new DataTable();
            if (Columns == "")
                m_DbCommand.CommandText = string.Format("Select *From {0} Where {1}=@PrimaryKeyValue", m_TableName, m_PrimaryKey);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Where {2}=@PrimaryKeyValue", Columns, m_TableName, m_PrimaryKey);
            m_DbCommand.Parameters.Clear();

            DbParameter newPrimaryKeyParam = CreateDbParameter();
            newPrimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newPrimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newPrimaryKeyParam.Value = PrimaryKeyValue;
            m_DbCommand.Parameters.Add(newPrimaryKeyParam);

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                m_DbDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count > 0)
            {
                resultArray = ConvertTableToStructArray<T>(resultTable)[0];
            }
            return resultArray;
        }
        #endregion

        public virtual DataTable SelectRecords<T>(T Record)
        {
            DataTable resultTable = new DataTable(m_TableName);
            initTableParam(Record);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            MarkSelectSql(Record, out DbCommand, "", out DbParameters, "", null);
            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        public virtual DataTable SelectRecords<T>(T Record, string CustomCondition, string Sort, string Columns)
        {
            DataTable resultTable = new DataTable(m_TableName);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            if (Columns == "")
                MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, Sort, null);
            else
            {
                string[] ColumnsArray = Columns.Split(',');
                MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, Sort, ColumnsArray);
            }

            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        public virtual DataTable SelectRecords<T>(T Record, string CustomCondition)
        {
            DataTable resultTable = new DataTable(m_TableName);
            initTableParam(Record);
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            MarkSelectSql(Record, out DbCommand, CustomCondition, out DbParameters, "", null);
            if (DbCommand != string.Empty)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                if (DbParameters != null)
                    m_DbCommand.Parameters.AddRange(DbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        public virtual DataTable SelectCustom(string SelectSql)
        {
            DataTable resultTable = new DataTable();
            if (SelectSql != null && SelectSql != "" && SelectSql != string.Empty)
            {
                m_DbCommand.CommandText = SelectSql;
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        //public virtual bool InsertRecord<T>(T Record)
        //{
        //    string DbCommand = string.Empty;
        //    DbParameter[] DbParameters = null;
        //    MarkInsertSql(Record, out DbCommand, out DbParameters);
        //    if (DbCommand != string.Empty && DbParameters != null)
        //    {
        //        m_DbCommand.CommandText = DbCommand;
        //        m_DbCommand.Parameters.Clear();
        //        m_DbCommand.Parameters.AddRange(DbParameters);

        //        if (OpenConnection())
        //        {
        //            if (m_DbCommand.ExecuteNonQuery() > 0)
        //                return true;
        //            else
        //                return false;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //        return false;
        //}

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Record">实体类</param>
        /// <returns>-1为插入失败，其它返回值为插入记录的主键值</returns>
        public virtual int InsertRecord<T>(T Record)
        {
            int result = -1;
            string DbCommand;
            DbParameter[] DbParameters = null;
            MarkInsertSql(Record, out DbCommand, out DbParameters);
            if (DbCommand != string.Empty && DbParameters != null)
            {

                m_DbCommand.CommandText = DbCommand;// + ";SELECT SCOPE_IDENTITY() as NewID";
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(DbParameters);

                if (OpenConnection())
                {
                    if (m_DbCommand.ExecuteNonQuery() > 0)
                    {
                        m_DbCommand.Parameters.Clear();
                        //m_DbCommand.CommandText = "SELECT SCOPE_IDENTITY() as NewID";
                        switch (m_DataBaseType)
                        {
                            case DataBaseType.SqlServer:
                            case DataBaseType.Access:
                            case DataBaseType.MySql:
                                m_DbCommand.CommandText = "SELECT @@IDENTITY as NewID";
                                break;
                        }
                        result = Convert.ToInt32(m_DbCommand.ExecuteScalar());
                    }
                }
            }
            return result;
        }

        public virtual bool UpdateRecord<T>(T Record)
        {
            initTableParam(Record);
            string DbCommand;
            m_IsUserView = false;
            DbParameter[] DbParameters = null;
            MarkUpdateSql(Record, string.Format("{0}=@PrimaryKeyValue",m_PrimaryKey), out DbCommand, out DbParameters);

            object primaryKey = getFieldValue(Record, m_PrimaryKey);
            DbParameter newprimaryKeyParam = CreateDbParameter();
            newprimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newprimaryKeyParam.DbType = ConvertTypeToDbType(primaryKey);
            newprimaryKeyParam.Value = primaryKey;
            

            if (DbCommand != string.Empty && DbParameters != null)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(DbParameters);
                m_DbCommand.Parameters.Add(newprimaryKeyParam);

                OpenConnection();
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public virtual bool UpdateRecord<T>(T Record, object PrimaryKeyValue )
        {
            initTableParam(Record);
            string DbCommand;
            m_IsUserView = false;
            DbParameter[] DbParameters = null;
            MarkUpdateSql(Record, string.Format("{0}=@PrimaryKeyValue", m_PrimaryKey), out DbCommand, out DbParameters);

            DbParameter newPrimaryKeyParam = CreateDbParameter();
            newPrimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newPrimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newPrimaryKeyParam.Value = PrimaryKeyValue;

            if (DbCommand != string.Empty && DbParameters != null)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(DbParameters);
                m_DbCommand.Parameters.Add(newPrimaryKeyParam);

                OpenConnection();
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        protected bool Base_UpdateRecord<T>(T Record, string Condition)
        {
            string DbCommand;
            DbParameter[] DbParameters = null;
            MarkUpdateSql(Record, Condition, out DbCommand, out DbParameters);
            if (DbCommand != string.Empty && DbParameters != null)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(DbParameters);

                OpenConnection();
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public virtual DataTable SelectAllRecords<T>() where T : new()
        {
            initTableParam(new T());
            DataTable records = new DataTable(m_TableName);
            m_DbCommand.CommandText = string.Format("Select *From {0}", getTableName());
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(records, SchemaType.Mapped);
                m_DbDataAdapter.Fill(records);
            }
            return records;
        }

        public virtual DataTable SelectAllRecords<T>(string Sort, string Columns) where T : new()
        {
            initTableParam(new T());
            DataTable records = new DataTable(m_TableName);
            string vSqlColumns = "*";
            if (Columns != null && Columns != "")
                vSqlColumns = Columns;
            if (Sort != null && Sort != "")
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Order By {2}", Columns, getTableName(), Sort);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1}", Columns, getTableName());
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(records, SchemaType.Mapped);
                m_DbDataAdapter.Fill(records);
            }
            return records;
        }

        public virtual bool DeleteRecordByPrimaryKey<T>(object PrimaryKeyValue) where T : new()
        {
            initTableParam(new T());

            m_DbCommand.CommandText = string.Format("Delete From {0} Where {1}=@PrimaryKey", m_TableName, m_PrimaryKey);
            m_DbCommand.Parameters.Clear();

            //object primaryKey = getFieldValue(Record, m_PrimaryKey);
            DbParameter newprimaryKeyParam = CreateDbParameter();
            newprimaryKeyParam.ParameterName = "@PrimaryKey";
            newprimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newprimaryKeyParam.Value = PrimaryKeyValue;
            m_DbCommand.Parameters.Add(newprimaryKeyParam);

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public virtual bool DeleteAllRecord<T>() where T : new()
        {
            initTableParam(new T());

            m_DbCommand.CommandText = string.Format("Delete From {0} ", m_TableName);
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbCommand.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }


        public virtual bool DeleteRecordByPrimaryKey<T>(T Record) 
        {
            
            string DbCommand = string.Empty;
            DbParameter[] DbParameters = null;
            MarkDeleteSql(Record, out DbCommand, "", out DbParameters);
            //MarkSelectSql(Record, out DbCommand, "", out DbParameters, "", null);

            //m_DbCommand.CommandText = string.Format("Delete From {0} Where {1}=@PrimaryKey", m_TableName, m_PrimaryKey);
            m_DbCommand.Parameters.Clear();
            m_DbCommand.CommandText = DbCommand;
            m_DbCommand.Parameters.AddRange(DbParameters);

            //object primaryKey = getFieldValue(Record, m_PrimaryKey);
            //DbParameter newprimaryKeyParam = CreateDbParameter();
            //newprimaryKeyParam.ParameterName = "@PrimaryKey";
            //newprimaryKeyParam.DbType = ConvertTypeToDbType(primaryKey);
            //newprimaryKeyParam.Value = primaryKey;
            //m_DbCommand.Parameters.Add(newprimaryKeyParam);

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public virtual DataTable SelectRecordByPrimaryKey<T>(object PrimaryKeyValue) where T : new()
        {
            initTableParam(new T());
            DataTable table = new DataTable();
            m_DbCommand.CommandText = string.Format("Select *From {0} Where {1}=@PrimaryKeyValue",m_TableName,m_PrimaryKey);
            m_DbCommand.Parameters.Clear();

            DbParameter newPrimaryKeyParam = CreateDbParameter();
            newPrimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newPrimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newPrimaryKeyParam.Value = PrimaryKeyValue;
            m_DbCommand.Parameters.Add(newPrimaryKeyParam);

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(table, SchemaType.Mapped);
                m_DbDataAdapter.Fill(table);
            }

            return table;
        }


        public virtual DataTable SelectRecordByPrimaryKey<T>(object PrimaryKeyValue, string Columns) where T : new()
        {
            initTableParam(new T());
            DataTable table = new DataTable();
            if (Columns == "")
                m_DbCommand.CommandText = string.Format("Select *From {0} Where {1}=@PrimaryKeyValue",  m_TableName,m_PrimaryKey);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Where {2}=@PrimaryKeyValue", Columns, m_TableName,m_PrimaryKey);
            m_DbCommand.Parameters.Clear();

            DbParameter newPrimaryKeyParam = CreateDbParameter();
            newPrimaryKeyParam.ParameterName = "@PrimaryKeyValue";
            newPrimaryKeyParam.DbType = ConvertTypeToDbType(PrimaryKeyValue);
            newPrimaryKeyParam.Value  = PrimaryKeyValue;
            m_DbCommand.Parameters.Add(newPrimaryKeyParam);

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(table, SchemaType.Mapped);
                m_DbDataAdapter.Fill(table);
            }
            return table;

        }

    


        public bool UpdateRecord<T>(T Record, string Condition)
        {
            string DbCommand;
            DbParameter[] DbParameters = null;
            initTableParam(Record);
            MarkUpdateSql(Record, Condition, out DbCommand, out DbParameters);
            if (DbCommand != string.Empty && DbParameters != null)
            {
                m_DbCommand.CommandText = DbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(DbParameters);

                OpenConnection();
                if (m_DbCommand.ExecuteNonQuery() >= 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region 私有方法

        protected StructT[] ConvertTableToStructArray<StructT>( DataTable Table ) where StructT:new()
        {
            StructT record = new StructT();
            StructT[] recordTArray = new StructT[Table.Rows.Count];
            for (int i = 0; i < recordTArray.Length; i++)
            {
                recordTArray[i] = ConvertDataRowToStruct<StructT>(Table.Rows[i]);
            }

            return recordTArray;
        }
        protected StructT ConvertDataRowToStruct<StructT>(DataRow row) where StructT:new()
        {
            StructT record = new StructT();
            Type recordType = record.GetType();
            PropertyInfo[] fieldInfos = recordType.GetProperties();
            object recordOBJ = (object)record;
            foreach (PropertyInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {

                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && row.Table.Columns[columnAttrib.ColumnName] != null)
                    {
                        if (columnAttrib.IsViewColumn)
                        {
                            try
                            {
                                object tempObj = row[columnAttrib.ColumnName];
                            }
                            catch
                            {
                                break;
                            }
                        }

                        object fieldValue = null;
                        switch (fieldInfo.PropertyType.ToString())
                        {
                            case "System.Boolean":
                            case "System.Nullable`1[System.Boolean]":
                            case "System.Data.SqlTypes.SqlBoolean":
                                fieldValue = DBConvert.ToBoolean(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Int16":
                            case "System.Nullable`1[System.Int16]":
                            case "System.Data.SqlTypes.SqlInt16":
                                fieldValue = DBConvert.ToInt16(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Int32":
                            case "System.Nullable`1[System.Int32]":
                            case "System.Data.SqlTypes.SqlInt32":
                                fieldValue = DBConvert.ToInt32(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.String":
                            case "System.Nullable`1[System.String]":
                            case "System.Data.SqlTypes.SqlString":
                                fieldValue = DBConvert.ToString(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.DateTime":
                            case "System.Nullable`1[System.DateTime]":
                            case "System.Data.SqlTypes.SqlDateTime":
                                fieldValue = DBConvert.ToDateTime(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //float
                            case "System.Single":
                            case "System.Nullable`1[System.Single]":
                            case "System.Data.SqlTypes.SqlSingle":
                                fieldValue = DBConvert.ToSingle(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //Decimal
                            case "System.Decimal":
                            case "System.Nullable`1[System.Decimal]":
                            case "System.Data.SqlTypes.SqlDecimal":
                                fieldValue = DBConvert.ToDecimal(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //byte[]
                            case "System.Byte[]":
                            case "System.Nullable`1[System.Byte[]]":
                            case "System.Data.SqlTypes.SqlBytes":
                                fieldValue = DBConvert.ToBytes(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //double
                            case "System.Double":
                            case "System.Nullable`1[System.Double]":
                            case "System.Data.SqlTypes.SqlDouble":
                                fieldValue = DBConvert.ToDouble(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                        }
                    }
                }
            }
            //record = (StructT)recordOBJ;
            return (StructT)recordOBJ;
        }

        protected bool TableViewIsExist(string TableViewName)
        {
            if (m_TableViewList != null)
            {
                foreach (string vTempTableViewName in m_TableViewList)
                {
                    if (TableViewName.ToUpper() == vTempTableViewName.ToUpper())
                        return true;
                }
            }
            return false;
        }

        protected bool TableIsExist(string TableName)
        {
            
            if (m_TableList != null)
            {
                foreach (string vTempTableName in m_TableList)
                {
                    if (TableName.ToUpper() == vTempTableName.ToUpper())
                        return true;
                }
            }
            return false;
        }

        protected bool OpenConnection()
        {
            try
            {
                if (m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    m_DbConnection.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        protected object getFieldValue<T>(T Record,string FieldName)
        {
            object resultValue = DBNull.Value;
            Type recordType = Record.GetType();
            PropertyInfo[] propertyInfos = recordType.GetProperties();
            foreach (PropertyInfo fieldInfo in propertyInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && columnAttrib.ColumnName==FieldName)
                        resultValue = fieldInfo.GetValue(Record);
                }
            }
            return resultValue;
        }


        /// <summary>
        /// 初始化表中的主键名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableStruct"></param>
        //protected void initPrimaryKeyName<T>(T TableStruct)
        //{
        //    Type vTableStructType = TableStruct.GetType();
        //    object[] vTableAttribs = vTableStructType.GetCustomAttributes(typeof(TableAttrib), true);
        //    if (vTableAttribs.Length > 0)
        //    {
        //        TableAttrib vTableAttrib = vTableAttribs[0] as TableAttrib;
        //        if (vTableAttrib!=null && vTableAttrib.PrimaryKey != string.Empty && vTableAttrib.PrimaryKey != "")
        //        {
        //            m_PrimaryKey = vTableAttrib.PrimaryKey;
        //        }
        //    }
        //}

        /// <summary>
        /// 初始化表的各项参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableStruct"></param>
        protected void initTableParam<T>(T TableStruct)
        {
            Type vTableStructType = TableStruct.GetType();
            object[] vTableAttribs = vTableStructType.GetCustomAttributes(typeof(TableAttrib), true);
            if (vTableAttribs.Length > 0)
            {
                TableAttrib vTableAttrib = vTableAttribs[0] as TableAttrib;
                if (!vTableAttrib.IsDynamic)
                {
                    if (TableIsExist(vTableAttrib.TableName))
                    {
                        m_TableName = "[" + vTableAttrib.TableName + "]";
                        //if (m_IsUserView && vTableAttrib.TableViewName != "")
                        //    m_TableViewName = "[" + vTableAttrib.TableViewName + "]";
                        //else
                        //    m_TableViewName = "";
                    }else if (TableViewIsExist(vTableAttrib.TableName) )
                    {
                        m_TableName = "[" + vTableAttrib.TableName + "]";
                    }


                    if (vTableAttrib != null && vTableAttrib.PrimaryKey != string.Empty && vTableAttrib.PrimaryKey != "")
                    {
                        m_PrimaryKey = vTableAttrib.PrimaryKey;
                    }
                }
                else
                {
                    string vTableName = string.Format("{0}{1}", vTableAttrib.TableName, ((IDynamicTable)TableStruct).DynamicVar);
                    if (TableIsExist(vTableName))
                    {
                        m_TableName = "[" + vTableName + "]";
                        //if (m_IsUserView && vTableAttrib.TableViewName != "")
                        //    m_TableViewName = string.Format("[{0}{1}]", vTableAttrib.TableViewName, ((IDynamicTable)TableStruct).DynamicVar);
                        //else
                        //    m_TableViewName = "";
                    }
                }
            }
        }

        protected string getTableName()
        {
            //if (m_TableViewName != "" && m_IsUserView)
            //    return m_TableViewName;
            //else
                return m_TableName;
        }

        void MarkInsertSql<T>(T Record, out string Sql, out DbParameter[] DbParameters)
        {
            initTableParam(Record);
            List<DbParameter> sqlParas = new List<DbParameter>();
            string fieldsSql = "", valuesSql = "";
            DbParameters = null;

            Type recordType = Record.GetType();
            PropertyInfo[] propertyInfos = recordType.GetProperties();

            foreach (PropertyInfo prpertyInfo in propertyInfos)
            {
                object[] attributes = prpertyInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && !columnAttrib.IsViewColumn)
                    {
                        object fieldValue = prpertyInfo.GetValue(Record);
                        //if (!((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        if (! (fieldValue==null) && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            fieldsSql += string.Format("[{0}],", columnAttrib.ColumnName);
                            valuesSql += string.Format("@{0},", columnAttrib.ColumnName);
                            //需要根据不断数据来判断
                            DbParameter newSqlParam = CreateDbParameter();
                            newSqlParam.DbType = ConvertTypeToDbType(fieldValue);
                            //对Access类型的时间需要转换为字符串
                            if (m_DataBaseType == DataBaseType.Access && newSqlParam.DbType == DbType.DateTime)
                                newSqlParam.Value = fieldValue.ToString();
                            else
                                newSqlParam.Value = fieldValue;
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }
                    }
                }
            }

            //去除最后一个","号
            if (fieldsSql != "")
                fieldsSql = fieldsSql.Remove(fieldsSql.Length - 1);
            if (valuesSql != "")
                valuesSql = valuesSql.Remove(valuesSql.Length - 1);

            Sql = string.Format("Insert Into {0} ( {1} ) Values ( {2} )", m_TableName, fieldsSql, valuesSql);
            DbParameters = sqlParas.ToArray();
            sqlParas.Clear();
        }

        void MarkDeleteSql<T>(T Record, out string Sql, string CustomCondition, out DbParameter[] DbParameters)
        {
            initTableParam(Record);
            List<DbParameter> sqlParas = new List<DbParameter>();
            string deleteSql = string.Empty;
            string OutColumnsSql = string.Empty;
            DbParameters = null;

            Type recordType = Record.GetType();

            PropertyInfo[] propertyInfos = recordType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    //if ((columnAttrib != null && !columnAttrib.IsViewColumn) || (columnAttrib != null && (columnAttrib.IsViewColumn && m_TableViewName != string.Empty)))
                    if ((columnAttrib != null && !columnAttrib.IsViewColumn) || (columnAttrib != null && (columnAttrib.IsViewColumn)))
                    {
                        object fieldValue = propertyInfo.GetValue(Record);
                        //if (fieldValue != null && !((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        if (fieldValue != null && columnAttrib.ColumnName.ToUpper() != m_PrimaryKey)
                        {
                            deleteSql += string.Format("([{0}]=@{0}) and ", columnAttrib.ColumnName);
                            //需要根据不同数据来判断
                            DbParameter newSqlParam = CreateDbParameter();
                            newSqlParam.DbType = ConvertTypeToDbType(fieldValue);
                            newSqlParam.Value = fieldValue;
                            if (m_DataBaseType == DataBaseType.Access && fieldValue is string)
                                newSqlParam.Size = ((string)fieldValue).Length;
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }
                    }
                }
            }

            if (deleteSql != string.Empty)
                deleteSql = deleteSql.Remove(deleteSql.Length - 5);
            if (CustomCondition != string.Empty && CustomCondition != null && CustomCondition != "")
            {
                if (deleteSql != string.Empty)
                    deleteSql = string.Format("{0} and {1}", deleteSql, CustomCondition);
                else
                    deleteSql = CustomCondition;
            }

            if (deleteSql != string.Empty)
            {
                Sql = string.Format("Delete  From  {0} Where {1}",  getTableName(), deleteSql);
                DbParameters = sqlParas.ToArray();
            }
            else
                Sql = string.Format("Delete  From  {0}", getTableName());
        }

        void MarkSelectSql<T>(T Record, out string Sql, string CustomCondition, out DbParameter[] DbParameters, string Sort, string[] Columns)
        {
            initTableParam(Record);
            List<DbParameter> sqlParas = new List<DbParameter>();
            string selectSql = string.Empty;
            string OutColumnsSql = string.Empty;
            DbParameters = null;

            Type recordType = Record.GetType();

            PropertyInfo[] propertyInfos = recordType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    //if ((columnAttrib != null && !columnAttrib.IsViewColumn) || (columnAttrib != null && (columnAttrib.IsViewColumn && m_TableViewName != string.Empty)))
                    if ((columnAttrib != null && !columnAttrib.IsViewColumn) || (columnAttrib != null && (columnAttrib.IsViewColumn)))
                    {
                        object fieldValue = propertyInfo.GetValue(Record);
                        //if (fieldValue != null && !((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        if (fieldValue != null && columnAttrib.ColumnName.ToUpper() != m_PrimaryKey)
                        {
                            selectSql += string.Format("([{0}]=@{0}) and ", columnAttrib.ColumnName);
                            //需要根据不同数据来判断
                            DbParameter newSqlParam = CreateDbParameter();
                            newSqlParam.DbType = ConvertTypeToDbType(fieldValue);
                            newSqlParam.Value = fieldValue;
                            if (m_DataBaseType == DataBaseType.Access && fieldValue is string)
                                newSqlParam.Size = ((string)fieldValue).Length;
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }

                        if (Columns != null)
                        {
                            bool is_Out = false;
                            foreach (string column in Columns)
                            {
                                if (column == columnAttrib.ColumnName)
                                {
                                    is_Out = true;
                                    break;
                                }
                            }
                            if (is_Out)
                                OutColumnsSql += string.Format("[{0}],", columnAttrib.ColumnName);
                        }
                    }
                }
            }

            if (selectSql != string.Empty)
                selectSql = selectSql.Remove(selectSql.Length - 5);
            if (CustomCondition != string.Empty && CustomCondition != null && CustomCondition != "")
            {
                if (selectSql != string.Empty)
                    selectSql = string.Format("{0} and {1}", selectSql, CustomCondition);
                else
                    selectSql = CustomCondition;
            }

            if (OutColumnsSql != string.Empty)
                OutColumnsSql = OutColumnsSql.Remove(OutColumnsSql.Length - 1);
            else
                OutColumnsSql = "*";

            if (selectSql != string.Empty)
            {
                if (Sort == "")
                    Sql = string.Format("Select {0} From  {1} Where {2}", OutColumnsSql, getTableName(), selectSql);
                else
                    Sql = string.Format("Select {0} From  {1} Where {2} Order By {3}", OutColumnsSql, getTableName(), selectSql, Sort);
                DbParameters = sqlParas.ToArray();
            }
            else
            {
                if (Sort == "")
                    Sql = string.Format("Select {0} From  {1}", OutColumnsSql, getTableName());
                else
                    Sql = string.Format("Select {0} From  {1} Order By {2}", OutColumnsSql, getTableName(), Sort);
            }

        }

        protected void MarkUpdateSql<T>(T Record, string Condition, out string Sql, out DbParameter[] DbParameters)
        {
            initTableParam(Record);
            List<DbParameter> sqlParas = new List<DbParameter>();
            string setSql = string.Empty;
            DbParameters = null;

            Type recordType = Record.GetType();
            PropertyInfo[] propertyInfos = recordType.GetProperties();
            foreach (PropertyInfo proertyInfo in propertyInfos)
            {
                object[] attributes = proertyInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && !columnAttrib.IsViewColumn)
                    {
                        object fieldValue = proertyInfo.GetValue(Record);
                        //if (fieldValue != null && !((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        if (fieldValue != null && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            setSql += string.Format("[{0}]=@{0},", columnAttrib.ColumnName);
                            //需要根据不同数据据来判断
                            //需要根据不同数据来判断
                            DbParameter newSqlParam = CreateDbParameter();
                            newSqlParam.DbType = ConvertTypeToDbType(fieldValue);
                            //对Access类型的时间需要转换为字符串
                            if ( m_DataBaseType == DataBaseType.Access && newSqlParam.DbType== DbType.DateTime)
                                newSqlParam.Value = fieldValue.ToString();
                            else
                                newSqlParam.Value = fieldValue.ToString();
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }
                    }
                }
            }

            if (setSql != string.Empty)
            {
                setSql = setSql.Remove(setSql.Length - 1);
                Sql = string.Format("Update {0} Set {1} Where {2}", getTableName(), setSql, Condition);
                DbParameters = sqlParas.ToArray();
            }
            else
                Sql = string.Empty;
        }

        /// <summary>
        /// 创建数据参数
        /// </summary>
        /// <returns></returns>
        DbParameter CreateDbParameter()
        {
            DbParameter DbParameterValue = null;
            switch (m_DataBaseType)
            {
                case DataBaseType.SqlServer:
                    DbParameterValue = new SqlParameter();
                    break;
                case DataBaseType.Access:
                case DataBaseType.MySql:
                    DbParameterValue = new OleDbParameter();
                    break;
                case DataBaseType.Oracle:
                    DbParameterValue = new OracleParameter();
                    break;
            }
            return DbParameterValue;
        }

        //SqlDbType ConvertTypeToSqlDbType(Object ObjectType)
        //{
        //    SqlDbType SqlDbTypeValue = SqlDbType.VarChar;


        //    if (ObjectType is SqlBinary)
        //        SqlDbTypeValue = SqlDbType.Binary;
        //    else if (ObjectType is SqlBoolean)
        //        SqlDbTypeValue = SqlDbType.Bit;
        //    else if (ObjectType is SqlByte)
        //        SqlDbTypeValue = SqlDbType.TinyInt;
        //    else if (ObjectType is SqlDateTime)
        //        SqlDbTypeValue = SqlDbType.DateTime;
        //    else if (ObjectType is SqlDecimal)
        //        SqlDbTypeValue = SqlDbType.Decimal;
        //    else if (ObjectType is SqlDouble)
        //        SqlDbTypeValue = SqlDbType.Float;
        //    else if (ObjectType is SqlFileStream)
        //        SqlDbTypeValue = SqlDbType.VarBinary;
        //    else if (ObjectType is SqlGuid)
        //        SqlDbTypeValue = SqlDbType.UniqueIdentifier;
        //    else if (ObjectType is SqlInt16)
        //        SqlDbTypeValue = SqlDbType.SmallInt;
        //    else if (ObjectType is SqlInt32)
        //        SqlDbTypeValue = SqlDbType.Int;
        //    else if (ObjectType is SqlInt64)
        //        SqlDbTypeValue = SqlDbType.BigInt;
        //    else if (ObjectType is SqlMoney)
        //        SqlDbTypeValue = SqlDbType.Money;
        //    else if (ObjectType is SqlSingle)
        //        SqlDbTypeValue = SqlDbType.Real;
        //    else if (ObjectType is SqlString)
        //        SqlDbTypeValue = SqlDbType.VarChar;
        //    else if (ObjectType is SqlXml)
        //        SqlDbTypeValue = SqlDbType.Xml;
        //    return SqlDbTypeValue;
        //}

        //OleDbType ConvertTypeToOleDbType(Object ObjectType)
        //{

        //    if (ObjectType is SqlBinary)
        //        return OleDbType.Binary;
        //    else if (ObjectType is SqlBoolean)
        //        return OleDbType.Boolean;
        //    else if (ObjectType is SqlByte)
        //        return OleDbType.UnsignedTinyInt;
        //    else if (ObjectType is SqlDateTime)
        //        return OleDbType.Date;
        //    else if (ObjectType is SqlDecimal)
        //        return OleDbType.Decimal;
        //    else if (ObjectType is SqlDouble)
        //        return OleDbType.Single;
        //    else if (ObjectType is SqlFileStream)
        //        return OleDbType.VarBinary;
        //    else if (ObjectType is SqlGuid)
        //        return OleDbType.Guid;
        //    else if (ObjectType is SqlInt16)
        //        return OleDbType.SmallInt;
        //    else if (ObjectType is SqlInt32)
        //        return OleDbType.Integer;
        //    else if (ObjectType is SqlInt64)
        //        return OleDbType.BigInt;
        //    else if (ObjectType is SqlMoney)
        //        return OleDbType.Numeric;
        //    else if (ObjectType is SqlSingle)
        //        return OleDbType.Double;
        //    else if (ObjectType is SqlString)
        //        return OleDbType.BSTR;
        //    else
        //        return OleDbType.BSTR;
        //}
        DbType ConvertTypeToDbType(Object ObjectType)
        {
            DbType DbTypeValue = DbType.String;

            if (ObjectType is byte || ObjectType is sbyte)
                DbTypeValue = DbType.Byte;
            else if (ObjectType is short || ObjectType is ushort)
                DbTypeValue = DbType.Int16;
            else if (ObjectType is int || ObjectType is uint)
                DbTypeValue = DbType.Int32;
            else if (ObjectType is long || ObjectType is ulong)
                DbTypeValue = DbType.Int64;
            else if (ObjectType is float)
                DbTypeValue = DbType.Single;
            else if (ObjectType is double)
                DbTypeValue = DbType.Double;
            else if (ObjectType is bool)
                DbTypeValue = DbType.Boolean;
            else if (ObjectType is char)
                DbTypeValue = DbType.String;
            else if (ObjectType is decimal)
                DbTypeValue = DbType.Decimal;
            else if (ObjectType is string)
                DbTypeValue = DbType.String;
            else if (ObjectType is DateTime)
                DbTypeValue = DbType.DateTime;
            else if (ObjectType is object)
                DbTypeValue = DbType.Binary;
            return DbTypeValue;
        }

        public void Dispose()
        {
            if (m_DbDataAdapter != null)
            {
                m_DbDataAdapter.Dispose();
                m_DbDataAdapter = null;
            }
            if ( m_DbCommand != null )
            {
                m_DbCommand.Dispose();
                m_DbCommand = null;
            }
            if (m_DbConnection != null)
            {
                m_DbConnection.Close();
                m_DbConnection.Dispose();
                m_DbDataAdapter = null;
            }
            m_TableList = null;
            m_TableViewList = null;
            //GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }

        #endregion
    }
    #endregion

    #region 自定义属性
    /// <summary>
    ///自定义表属性 
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Property| AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttrib : Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        string m_TableName = string.Empty;
        /// <summary>
        /// 是否为视图
        /// </summary>
        bool m_IsTableView = false;
        /// <summary>
        /// 主键名称
        /// </summary>
        string m_PrimaryKey = string.Empty;
        /// <summary>
        /// 表名是否为动态
        /// </summary>
        bool m_IsDynamic = false; 


        public TableAttrib(string TableName,string PrimaryKey)
        {
            m_TableName = string.Format("{0}", TableName);
            m_PrimaryKey = PrimaryKey;
        }


        public TableAttrib(string TableName, bool IsTableView,bool IsDynamic)
        {
            m_TableName = string.Format("{0}", TableName);
            m_IsTableView = IsTableView;
            m_IsDynamic = IsDynamic;
        }
        public TableAttrib(string TableName, bool IsTableView)
        {
            m_TableName = string.Format("{0}", TableName);
            m_IsTableView = IsTableView;
        }

        public bool IsDynamic
        {
            get
            {
                return m_IsDynamic;
            }
        }

        public string TableName
        {
            get
            {
                return m_TableName;
            }
        }

        public string PrimaryKey
        {
            get
            {
                return m_PrimaryKey;
            }
        }
    }
    /// <summary>
    /// 自定义属性(列)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttrib : Attribute
    {
        /// <summary>
        /// 是否为视图行
        /// </summary>
        bool m_IsViewColumn = false;
        /// <summary>
        /// 行名称
        /// </summary>
        string m_ColumnName = string.Empty;

        /// <summary>
        /// 值是否改变
        /// </summary>
        bool m_IsChanged = false;

        public ColumnAttrib(string ColumnName)
        {
            m_ColumnName = ColumnName;
        }

        public ColumnAttrib(string ColumnName, bool IsViewColumn)
        {
            m_ColumnName = ColumnName;
            m_IsViewColumn = IsViewColumn;

        }

        public bool IsViewColumn
        {
            get
            {
                return m_IsViewColumn;
            }
        }

        public string ColumnName
        {
            get
            {
                return m_ColumnName;
            }
        }
    }
    #endregion
}
