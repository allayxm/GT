using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ClientBusiness
{
    public class FeatureCollection
    {
        public string Name { get; set; }
        List<ChildFeature> FeatureList { get; set; }
    }

    public class ChildFeature
    {
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
