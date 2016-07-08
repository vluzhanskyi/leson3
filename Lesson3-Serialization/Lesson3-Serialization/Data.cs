using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3_Serialization
{
    public class Data
    {
        public Data(object value, string[] attributes)
        {
            Value = value;
            Attributes = attributes;
        }
        public object Value { get; set; }
        public string[] Attributes { get; set; }
    }
}
