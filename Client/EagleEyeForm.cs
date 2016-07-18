using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using System.Drawing.Drawing2D;
using ESRI.ArcGIS.Display;

namespace JXDL.Client
{
    public partial class EagleEyeForm : Form
    {
        public AxMapControl MainMapControl { get; set; }
        /// <summary>
        /// 乡镇及街道要素
        /// </summary>
        public IFeatureLayer TownshipFeatureLayer { get; set; }

        public EagleEyeForm()
        {
            InitializeComponent();
        }

        private void EagleEyeForm_Load(object sender, EventArgs e)
        {
            TopMost = true;
            if (MainMapControl!=null )
            {
                MainMapControl.OnExtentUpdated += MainMapControl_OnExtentUpdated;
            }
            if (TownshipFeatureLayer!=null)
            {
                axMapControl_EagleEye.ClearLayers();
                axMapControl_EagleEye.AddLayer(TownshipFeatureLayer as ILayer);
            }
        }

        private void MainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope vEnvelope = (IEnvelope)e.newEnvelope;
            IGraphicsContainer vGraphicsContainer = axMapControl_EagleEye.Map as IGraphicsContainer;
            IActiveView vActiveView = vGraphicsContainer as IActiveView;
            vGraphicsContainer.DeleteAllElements();
            IElement vElement = new RectangleElementClass();
            vElement.Geometry = vEnvelope;
            ILineSymbol vOutLineSymbol = new SimpleLineSymbolClass();
            vOutLineSymbol.Width = 2;
            vOutLineSymbol.Color = getColor(255, 0, 0, 255);
            IFillSymbol vFileSymbol = new SimpleFillSymbolClass();
            vFileSymbol.Color = getColor(9, 0, 0, 0);
            vFileSymbol.Outline = vOutLineSymbol;
            IFillShapeElement vFillShapeElement = vElement as IFillShapeElement;
            vFillShapeElement.Symbol = vFileSymbol;
            vGraphicsContainer.AddElement((IElement)vFillShapeElement,0);
            vActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll,null,null);
        }

        IRgbColor getColor(int r,int g,int b,int t)
        {
            IRgbColor vRgbColor = new RgbColorClass();
            vRgbColor.Red = r;
            vRgbColor.Green = g;
            vRgbColor.Blue = b;
            vRgbColor.Transparency = (byte)t;
            return vRgbColor;
        }

        private void axMapControl_EagleEye_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint vPoint = new PointClass();
            vPoint.PutCoords(e.mapX, e.mapY);
            MainMapControl.CenterAt(vPoint);

        }

        private void EagleEyeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }

        private void EagleEyeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
