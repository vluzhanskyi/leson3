using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3_Serialization
{
    class IniSection : Attribute
    {
        public IniSection(string elementName)
        {
            this.ElementName = elementName;
        }

        public string ElementName { get; set; }
    }
}
