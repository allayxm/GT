using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace MXKJ.DBMiddleWareLib
{

    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DataBaseType { SqlServer=0,Access=1,MySql=2,Oracle=3 }
    /// <summary>
    /// 公用方法类
    /// </summary>
    public class CommClass
    {
        /// <summary>
        /// 将DataTable中的数据转换为List
        /// </summary>
        /// <typeparam name="StructT"></typeparam>
        /// <param name="SourceTable"></param>
        /// <returns></returns>
        public static List<StructT> ConvertDataTableToList<StructT>( DataTable SourceTable) where StructT:new()
        {
            List<StructT> vResult = new List<StructT>();
            foreach( DataRow vTempRow in SourceTable.Rows )
            {
                StructT vTempStruct = new StructT();
                ConvertDataRowToStruct(ref vTempStruct, vTempRow);
                vResult.Add(vTempStruct);
            }
            return vResult;
        }

        /// <summary>
        /// 将DataRow中的数据转换为Struct
        /// </summary>
        /// <typeparam name="StructT"></typeparam>
        /// <param name="record"></param>
        /// <param name="row"></param>
        public static void ConvertDataRowToStruct<StructT>(ref StructT record, DataRow row)
        {
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
            record = (StructT)recordOBJ;
        }

        public static void ConvertStructToDataRow(object Record, DataRow Row)
        {
            Type recordType = Record.GetType();
            PropertyInfo[] fieldInfos = recordType.GetProperties();

            foreach (PropertyInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && Row.Table.Columns[columnAttrib.ColumnName] != null)
                    {
                        if (!columnAttrib.IsViewColumn && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            object vObjectValue = fieldInfo.GetValue(Record);
                            switch (fieldInfo.PropertyType.ToString())
                            {
                                case "System.Data.SqlTypes.SqlBoolean":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlBoolean)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlInt16":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlInt16)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlInt32":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlInt32)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlString":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlString)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlDateTime":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDateTime)vObjectValue).Value;
                                    break;
                                //float
                                case "System.Data.SqlTypes.SqlSingle":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlSingle)vObjectValue).Value;
                                    break;
                                //Decimal
                                case "System.Data.SqlTypes.SqlDecimal":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDecimal)vObjectValue).Value;
                                    break;
                                //byte[]
                                case "System.Data.SqlTypes.SqlBytes":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlBytes)vObjectValue).Value;
                                    break;
                                //double
                                case "System.Data.SqlTypes.SqlDouble":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDouble)vObjectValue).Value;
                                    break;
                            }
                        }
                    }

                }

            }
        }
    }

    public class DBConvert
    {

        #region 系统类型
        public static string ToString(object value)
        {
            string vResult = "";
            if (value is SqlString)
                vResult = ((SqlString)value).IsNull ? null : ((SqlString)value).Value;
            else if (value is string)
                vResult = value == DBNull.Value || value == null ? null : (string)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToString(value);
            return vResult;
        }

        public static Int32? ToInt32(object value)
        {
            int? vResult = null;
            if (value is SqlInt32)
                vResult = ((SqlInt32)value).IsNull ? 0 : ((SqlInt32)value).Value;
            else if (value is Int32)
                vResult = value == DBNull.Value ? null : (Int32?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToInt32(value);
            return vResult;
        }

        public static Int16? ToInt16(object value)
        {
            Int16? vResult = null;
            if (value is SqlInt16)
                vResult = ((SqlInt16)value).IsNull ? (short)0 : ((SqlInt16)value).Value;
            else if (value is Int16)
                vResult = value == DBNull.Value ? null : (Int16?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToInt16(value);
            return vResult;
        }


        public static decimal? ToDecimal(object value)
        {
            decimal? vResult = null;
            if (value is SqlDecimal)
                vResult = ((SqlDecimal)value).IsNull ? 0 : ((SqlDecimal)value).Value;
            else if (value is Decimal)
                vResult = value == DBNull.Value ? null : (decimal?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToDecimal(value);
            return vResult;
        }


        public static decimal? ToDecimal(object value, int Decimals)
        {
            decimal? vResult = null;
            if (value is SqlDecimal)
                vResult = ((SqlDecimal)value).IsNull ? 0 : ((SqlDecimal)value).Value;
            if (value is Decimal)
                vResult = value == DBNull.Value ? null : (decimal?)value;
            if (vResult != null)
                return Math.Round((decimal)vResult, Decimals);
            else
                return vResult;
        }

        public static Single? ToSingle(object value)
        {
            Single? vResult = null;
            if (value is SqlSingle)
                vResult = ((SqlSingle)value).IsNull ? 0 : (Single)value;
            else if (value is Single)
                vResult = value == DBNull.Value ? null : (Single?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToSingle(value);
            return vResult;
        }

        public static DateTime? ToDateTime(object value)
        {
            DateTime? vResult = null;
            if (value is SqlDateTime)
                vResult = ((SqlDateTime)value).IsNull ? DateTime.MinValue : ((SqlDateTime)value).Value;
            else if (value is DateTime)
                vResult = value == DBNull.Value ? null : (DateTime?)value;
            else if (value is DBNull)
                vResult = null;
            else
                Convert.ToDateTime(value);
            return vResult;
        }

        public static byte[] ToBytes(object value)
        {
            byte[] vResult = null;
            if (value is SqlBinary)
                vResult = ((SqlBinary)value).IsNull ? null : ((SqlBinary)value).Value;
            else if (value is byte[])
                vResult = value == DBNull.Value ? null : (byte[])value;
            else if (value is DBNull)
                vResult = null;
            return vResult;
        }

        public static bool? ToBoolean(object value)
        {
            bool? vResult = null;
            if (value is SqlBoolean)
                vResult = ((SqlBoolean)value).IsNull ? false : ((SqlBoolean)value).Value;
            else if (value is Boolean)
                vResult = value == DBNull.Value ? null : (Boolean?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToBoolean(value);
            return vResult;
        }

        public static double? ToDouble(object value)
        {
            double? vResult = null;
            if (value is SqlDouble)
                vResult = ((SqlDouble)value).IsNull ? 0 : ((SqlDouble)value).Value;
            else if (value is double)
                vResult = value == DBNull.Value ? null : (double?)value;
            else if (value is DBNull)
                vResult = null;
            else
                vResult = Convert.ToDouble(value);
            return vResult;
        }
        #endregion


        #region SqlDbType

        public static SqlString ToSqlString(object value)
        {

            return value == DBNull.Value || value == null ? SqlString.Null : (string)value;
        }

        public static SqlSingle ToSqlSingle(object value)
        {
            return value == DBNull.Value || value == null ? SqlSingle.Null : (Single)value;
        }

        public static SqlDecimal ToSqlDecimal(object value)
        {

            return value == DBNull.Value || value == null ? SqlDecimal.Null : (decimal)value;
        }

        public static SqlInt16 ToSqlInt16(object value)
        {
            return value == DBNull.Value || value == null ? SqlInt16.Null : (Int16)value;
        }

        public static SqlInt32 ToSqlInt32(object value)
        {
            return value == DBNull.Value || value == null ? SqlInt32.Null : (Int32)value;
        }

        public static SqlDateTime ToSqlDateTime(object value)
        {
            return value == DBNull.Value || value == null ? SqlDateTime.Null : (DateTime)value;
        }

        public static SqlBytes ToSqlBytes(object value)
        {
            SqlBytes vSqlBytes = null;
            if (value == DBNull.Value || value == null)
                vSqlBytes = null;
            else
            {
                byte[] vBytes = (byte[])value;
                vSqlBytes = new SqlBytes(vBytes);
            }

            return vSqlBytes;
        }

        public static SqlBoolean ToSqlBoolean(object value)
        {
            return value == DBNull.Value || value == null ? SqlBoolean.Null : (Boolean)value;
        }

        public static SqlDouble ToSqlDouble(object value)
        {
            return value == DBNull.Value || value == null ? SqlDouble.Null : (double)value;
        }
        #endregion
    }

    public interface IDynamicTable
    {
        string DynamicVar
        {
            get;
            set;
        }
    }
}
