using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3_Serialization
{
    class Logic
    {
        public LinkedList<Data> CollectData(MyIni ini1)
        {
            LinkedList<Data> linkedData = new LinkedList<Data>();
            PropertyInfo[] props = ini1.GetType().GetProperties();

            foreach (PropertyInfo property in props)
            {
                IniSectionAttribute section = null;
                IniKeyAttribute key = null;
                string[] attributes = new string[2];
                object[] attrs = property.GetCustomAttributes(true);

                foreach (var attr in attrs)
                {
                    if (section == null)
                    {
                        section = attr as IniSectionAttribute;
                        if (section != null)
                            attributes[0] = section.Element;
                    }

                    if (key != null) continue;
                    key = attr as IniKeyAttribute;
                    if (key != null)
                        attributes[1] = key.Element;
                }
                object value = property.GetValue(ini1);
                linkedData.AddLast(new Data(value, attributes));
            }

            return linkedData;
        }

    }
}
