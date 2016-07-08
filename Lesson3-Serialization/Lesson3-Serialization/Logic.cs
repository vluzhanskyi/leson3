using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3_Serialization
{
    public class Logic
    {
        public LinkedList<Data> CollectData(MyIni ini1, out LinkedListNode<Data> ln)
        {
            LinkedList<Data> linkedData = new LinkedList<Data>();
            ln = new LinkedListNode<Data>(null);
            PropertyInfo[] props = ini1.GetType().GetProperties();

            foreach (PropertyInfo property in props)
            {
                List<object> valuesList = new List<object>();
                IniSectionAttribute Section = null;
                IniKeyAttribute Key = null;
                string[] attributes = new string[2];
                object[] attrs = property.GetCustomAttributes(true);

                foreach (var attr in attrs)
                {
                    if (Section == null)
                    {
                        Section = attr as IniSectionAttribute;
                        if (Section != null)
                            attributes[0] = Section.Element;
                    }

                    if (Key == null)
                    {
                        Key = attr as IniKeyAttribute;
                        if (Key != null)
                            attributes[1] = Key.Element;
                    }
                }
                object value = property.GetValue(ini1);
                Data inputData = new Data(value, attributes);
                ln = linkedData.AddLast(inputData);
            }

            return linkedData;
        }

        public void Print(LinkedList<Data> linkedData, LinkedListNode<Data> ln)
        {
            using (TextWriter wr = new StreamWriter("Out.ini"))
            {
                foreach (var variable in linkedData)
                {
                    if (ln.Next == null)
                        wr.WriteLine("[{0}]", variable.Attributes[0]);
                    else
                    {
                        if (ln.Previous != null && ln.Value.Attributes[0] != ln.Previous.Value.Attributes[0])
                            wr.WriteLine("[{0}]", variable.Attributes[0]);
                    }
                    var v = variable.Attributes[0];
                    wr.WriteLine(variable.Attributes[1] + " = " + variable.Value);
                    ln = ln.Previous;
                }
            }
        }
    }
}
