using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using JXDL.IntrefaceStruct;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JXDL.ClientBusiness
{
    public class SymbolHelper
    {
        //public static SymbolStruct[] GetAllSymbolData()
        //{
            
        //}

        //MultiLayerLineSymbol
        public static ILineSymbol CreateLineDirectionSymbol()
        {
            ICartographicLineSymbol symbol = new CartographicLineSymbolClass();
            symbol.Cap = esriLineCapStyle.esriLCSButt;
            symbol.Join = esriLineJoinStyle.esriLJSRound;
            
            symbol.Color = ColorHelper.CreateColor(0, 0, 200);
            LineDecorationClass class2 = new LineDecorationClass();
            SimpleLineDecorationElementClass lineDecorationElement = new SimpleLineDecorationElementClass();
            lineDecorationElement.AddPosition(0.3);
            lineDecorationElement.AddPosition(0.7);
            lineDecorationElement.PositionAsRatio = true;
            IMarkerSymbol symbol2 = (lineDecorationElement.MarkerSymbol as IClone).Clone() as IMarkerSymbol;
            symbol2.Size = 9.0;
            symbol2.Color = ColorHelper.CreateColor(0, 200, 0);
            lineDecorationElement.MarkerSymbol = symbol2;
            class2.AddElement(lineDecorationElement);
            (symbol as ILineProperties).LineDecoration = class2;
            return symbol;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width)
        {
            SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
            
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = esriSimpleLineStyle.esriSLSSolid;
            class2.Width = Math.Abs(width);
            return class2;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width, esriSimpleLineStyle eStyle)
        {
            SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = eStyle;
            class2.Width = Math.Abs(width);
            return class2;
        }

    }
}
