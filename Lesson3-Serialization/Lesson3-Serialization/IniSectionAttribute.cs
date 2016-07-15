using System;

namespace Lesson3_Serialization
{
    public class IniSectionAttribute : Attribute
    {
        public IniSectionAttribute(string elementName)
        {
            Element = elementName;
        }

        public string Element { get; set; }
    }
}
