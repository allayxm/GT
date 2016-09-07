using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
namespace MyGISClass
{
    /// <summary>  
    /// 该类主要包含了要素类的复制以及同要素类数据的加载  
    /// 函数主要用于SDE与Personal GDB之间数据的处理  
    /// </summary>  
    class FeatureClassDataManager
    {
        /// <summary>  
        /// 根据传入的源要素类OldFeatureClass,新空间范围,要素存储工作空间,新要素类名  
        /// 产生具有相同字段结构和不同空间范围的要素类  
        /// </summary>  
        /// <param name="OldFeatureClass">源要素类</param>  
        /// <param name="SaveFeatWorkspace">存储工作空间</param>  
        /// <param name="FeatClsName">新要素类名</param>  
        /// <param name="pDomainEnv">新空间范围,可为null</param>  
        /// <returns></returns>  
        public IFeatureClass CloneFeatureClassInWorkspace(IFeatureClass OldFeatureClass, IFeatureWorkspace SaveFeatWorkspace, string FeatClsName, IEnvelope pDomainEnv)
        {
            IFields pFields = CloneFeatureClassFields(OldFeatureClass, pDomainEnv);
            return SaveFeatWorkspace.CreateFeatureClass(FeatClsName, pFields, null, null, esriFeatureType.esriFTSimple, OldFeatureClass.ShapeFieldName, "");
        }
        /// <summary>  
        /// 复制AnnotationClass,未完待续  
        /// </summary>  
        /// <param name="OldFeatureClass"></param>  
        /// <param name="SaveFeatWorkspace"></param>  
        /// <param name="FeatClsName"></param>  
        /// <param name="pDomainEnv"></param>  
        /// <returns></returns>  
        public IFeatureClass CloneAnnotationClassInWorkspace(IFeatureClass OldFeatureClass, IFeatureWorkspace SaveFeatWorkspace, string FeatClsName, IEnvelope pDomainEnv)
        {
            IFeatureWorkspaceAnno pFWSAnno = (IFeatureWorkspaceAnno)SaveFeatWorkspace;
            IAnnoClass pAnnoClass = (IAnnoClass)OldFeatureClass.Extension;
            return null;
        }
        /// <summary>  
        /// 将inFeatureClass要素类中所有符合pQueryFilter的要素复制到saveFeatureClass中,仅复制不做任何修改  
        /// </summary>  
        /// <param name="inFeatureClass">源要素类</param>  
        /// <param name="saveFeatureClass">存储要素类</param>  
        /// <param name="pQueryFilter">过滤参数</param>  
        /// <returns></returns>  
        public bool LoadFeatureClass(IFeatureClass inFeatureClass, IFeatureClass saveFeatureClass, IQueryFilter pQueryFilter)
        {
            //生成两个要素类字段的对应表  
            Dictionary<int, int> pFieldsDict = new Dictionary<int, int>();
            this.GetFCFieldsDirectory(inFeatureClass, saveFeatureClass, ref pFieldsDict);
            IFeatureCursor pinFeatCursor = inFeatureClass.Search(pQueryFilter, false);
            long nCount = inFeatureClass.FeatureCount(pQueryFilter);
            IFeature pinFeat = pinFeatCursor.NextFeature();
            IFeatureCursor psaveFeatCursor = saveFeatureClass.Insert(true);
            //使用IFeatureBuffer在内存中产生缓存避免多次打开,关闭数据库  
            IFeatureBuffer psaveFeatBuf = null;
            IFeature psaveFeat = null;
            long n = 0;
            while (pinFeat != null)
            {
                try
                {
                    psaveFeatBuf = saveFeatureClass.CreateFeatureBuffer();
                    psaveFeat = psaveFeatBuf as IFeature;
                    if (inFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        IAnnotationFeature pAF = (IAnnotationFeature)pinFeat;
                        IAnnotationFeature pNAF = (IAnnotationFeature)psaveFeat;
                        if (pAF.Annotation != null)
                        {
                            pNAF.Annotation = pAF.Annotation;
                        }
                    }
                    psaveFeat.Shape = pinFeat.Shape;
                    foreach (KeyValuePair<int, int> keyvalue in pFieldsDict)
                    {
                        if (pinFeat.get_Value(keyvalue.Key).ToString() == "")
                        {
                            if (psaveFeat.Fields.get_Field(keyvalue.Value).Type == esriFieldType.esriFieldTypeString)
                            {
                                psaveFeat.set_Value(keyvalue.Value, "");
                            }
                            else
                            {
                                psaveFeat.set_Value(keyvalue.Value, 0);
                            }
                        }
                        else
                        {
                            psaveFeat.set_Value(keyvalue.Value, pinFeat.get_Value(keyvalue.Key));
                        }
                    }
                    psaveFeatCursor.InsertFeature(psaveFeatBuf);
                }
                catch (Exception ex) { }
                finally
                {
                    psaveFeat = null;
                    n++;
                    if (n % 2000 == 0)
                    {
                        psaveFeatCursor.Flush();
                    }
                    pinFeat = pinFeatCursor.NextFeature();
                }
            }
            psaveFeatCursor.Flush();
            return true;
        }
        private IFields CloneFeatureClassFields(IFeatureClass pFeatureClass, IEnvelope pDomainEnv)
        {
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            //根据传入的要素类,将除了shape字段之外的字段复制  
            long nOldFieldsCount = pFeatureClass.Fields.FieldCount;
            long nOldGeoIndex = pFeatureClass.Fields.FindField(pFeatureClass.ShapeFieldName);
            for (int i = 0; i < nOldFieldsCount; i++)
            {
                if (i != nOldGeoIndex)
                {
                    pFieldsEdit.AddField(pFeatureClass.Fields.get_Field(i));
                }
                else
                {
                    IGeometryDef pGeomDef = new GeometryDefClass();
                    IGeometryDefEdit pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
                    ISpatialReference pSR = null;
                    if (pDomainEnv != null)
                    {
                        pSR = new UnknownCoordinateSystemClass();
                        pSR.SetDomain(pDomainEnv.XMin, pDomainEnv.XMax, pDomainEnv.YMin, pDomainEnv.YMax);
                    }
                    else
                    {
                        IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
                        pSR = CloneSpatialReference(pGeoDataset.SpatialReference);
                    }
                    //设置新要素类Geometry的参数  
                    pGeomDefEdit.GeometryType_2 = pFeatureClass.ShapeType;
                    pGeomDefEdit.GridCount_2 = 1;
                    pGeomDefEdit.set_GridSize(0, 10);
                    pGeomDefEdit.AvgNumPoints_2 = 2;
                    pGeomDefEdit.SpatialReference_2 = pSR;
                    //产生新的shape字段  
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "shape";
                    pFieldEdit.AliasName_2 = "shape";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pFieldEdit.GeometryDef_2 = pGeomDef;
                    pFieldsEdit.AddField(pField);
                }
            }
            return pFields;
        }
        private ISpatialReference CloneSpatialReference(ISpatialReference pSrcSpatialReference)
        {
            double xmin, xmax, ymin, ymax;
            pSrcSpatialReference.GetDomain(out xmin, out xmax, out ymin, out ymax);
            ISpatialReference pSR = new UnknownCoordinateSystemClass();
            pSR.SetDomain(xmin, xmax, ymin, ymax);
            return pSR;
        }
        private void GetFCFieldsDirectory(IFeatureClass pFCold, IFeatureClass pFCnew, ref Dictionary<int, int> FieldsDictionary)
        {
            for (int i = 0; i < pFCold.Fields.FieldCount; i++)
            {
                string tmpstrold = pFCold.Fields.get_Field(i).Name.ToUpper();
                switch (tmpstrold)
                {
                    case "OBJECTID":
                    case "SHAPE":
                    case "SHAPE_LENGTH":
                    case "SHAPE_AREA":
                    case "FID":
                        {
                            //以上字段由系统自动生成  
                            break;
                        }
                    default:
                        {
                            for (int j = 0; j < pFCnew.Fields.FieldCount; j++)
                            {
                                string tmpstrnew = pFCnew.Fields.get_Field(j).Name.ToUpper();
                                if (tmpstrold == tmpstrnew)
                                {
                                    FieldsDictionary.Add(i, j);
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}