using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JXDL.IntrefaceStruct;
using System.Data;
using JXDL.ManageBusiness;
using MXKJ.DBMiddleWareLib;
using JXDL.ManageEFModel;

namespace JXDL.Manage.App
{
    public class GetSymbolsController : ApiController
    {
        public SymbolEFModel[] Get()
        {
            MapServer vMapServer = new MapServer();
            return  vMapServer.GetSymbols();
        }
    }
}
