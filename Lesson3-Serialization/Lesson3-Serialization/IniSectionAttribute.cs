using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3_Serialization
{
    [AttributeUsage(AttributeTargets.All)]
    public class IniSectionAttribute : Attribute
    {
        public IniSectionAttribute(string elementName)
        {
            Element = elementName;
        }

        public string Element { get; set; }
    }
}
