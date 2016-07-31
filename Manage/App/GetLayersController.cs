using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using JXDL.IntrefaceStruct;
using JXDL.ManageBusiness;
using MXKJ.DBMiddleWareLib;

namespace JXDL.Manage.App
{
    public class GetLayersController : ApiController
    {
        // GET: api/GetLayers
        public LayerStruct[] Get()
        {
            MapServer vMapServer = new MapServer();
            DataTable vLayerTable = vMapServer.GetLayers();
            List<LayerStruct> vLyaers = new List<LayerStruct>();
            foreach( DataRow vTempRow in vLayerTable.Rows )
            {
                LayerStruct vLayer = new LayerStruct()
                {
                    ID = DBConvert.ToInt32(vTempRow["ID"]),
                    classify = DBConvert.ToString(vTempRow["classify"]),
                    Expository = DBConvert.ToString(vTempRow["Expository"]),
                    Name = DBConvert.ToString(vTempRow["Name"]),
                    Type = DBConvert.ToInt32(vTempRow["Type"])
                };
                vLyaers.Add(vLayer);
            }
            return vLyaers.ToArray();
        }

        // GET: api/GetLayers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetLayers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetLayers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetLayers/5
        public void Delete(int id)
        {
        }
    }
}
