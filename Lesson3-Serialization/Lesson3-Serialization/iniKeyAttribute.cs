﻿using System;

namespace Lesson3_Serialization
{
    public class IniKeyAttribute : Attribute
    {
        public IniKeyAttribute(string elementName)
        {
            Element = elementName;
        }

        public string Element { get; set; }
    }
}
