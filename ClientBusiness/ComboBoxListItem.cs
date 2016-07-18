using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ClientBusiness
{
    public class ComboBoxListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ComboBoxListItem(string value,string name)
        {
            Name = name;
            Value = value;
        }

        public ComboBoxListItem()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
