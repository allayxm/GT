using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Drawing;
using System.Data;

namespace JXDL.ClientBusiness
{
    public class CommonUnit
    {
        public static string ConvertLayerType(int type)
        {
            string vResult = "";
            switch (type)
            {
                case 0:
                    vResult = "点";
                    break;
                case 1:
                    vResult = "线";
                    break;
                case 2:
                    vResult = "面";
                    break;
                case 3:
                    vResult = "栅格";
                    break;
            }
            return vResult;
        }

        public static Type ConvertFeaturesFieldType(esriFieldType FieldType )
        {
            Type vType = null;
            switch (FieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    vType = typeof(Int16);
                    break;
                case esriFieldType.esriFieldTypeInteger:
                    vType = typeof(Int32);
                    break;
                case esriFieldType.esriFieldTypeSingle:
                    vType = typeof(float);
                    break;
                case esriFieldType.esriFieldTypeDouble:
                    vType = typeof(double);
                    break;
                case esriFieldType.esriFieldTypeString:
                    vType = typeof(string);
                    break;
                case esriFieldType.esriFieldTypeDate:
                    vType = typeof(DateTime);
                    break;
                case esriFieldType.esriFieldTypeOID:
                    vType = typeof(int);
                    break;
                case esriFieldType.esriFieldTypeGeometry:
                    vType = typeof(object);
                    break;
                case esriFieldType.esriFieldTypeBlob:
                    vType = typeof(byte[]);
                    break;
                case esriFieldType.esriFieldTypeRaster:
                    vType = typeof(byte[]);
                    break;
                case esriFieldType.esriFieldTypeGUID:
                case esriFieldType.esriFieldTypeGlobalID:
                case esriFieldType.esriFieldTypeXML:
                    vType = typeof(string);
                    break;
            }
            return vType;
        }

        public static DataTable CreateFeaturesTableStruct(IFeature feature)
        {
            DataTable vTable = new DataTable();
            for (int i = 0; i < feature.Fields.FieldCount; i++)
            {
                IField vField = feature.Fields.get_Field(i);
                if (vField.AliasName != "Shape")
                    vTable.Columns.Add(vField.AliasName, CommonUnit.ConvertFeaturesFieldType(vField.Type));
            }
            vTable.AcceptChanges();
            return vTable;
        }

        public static DataTable CreateFeaturesTableStruct(IFeatureClass FeatureClass)
        {
            DataTable vTable = new DataTable();
            for (int i = 0; i < FeatureClass.Fields.FieldCount; i++)
            {
                IField vField = FeatureClass.Fields.get_Field(i);
                if (vField.AliasName != "Shape")
                    vTable.Columns.Add(vField.AliasName, CommonUnit.ConvertFeaturesFieldType(vField.Type));
            }
            vTable.AcceptChanges();
            return vTable;
        }

        #region 颜色互转
        //Color转ArcEngine的IRgbColor
        public static IRgbColor ColorToIRgbColor(Color pColor)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.RGB = pColor.B * 65536 + pColor.G * 256 + pColor.R;
            return pRgbColor;
        }
        //Color转ArcEngine的IColor
        public static IColor ColorToIColor(Color color)
        {
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pColor;
        }
        #endregion
    }
}
