using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace Lesson3_Serialization
{
    public class Logic
    {
        public LinkedList<Data> CollectData(MyIni ini1, out LinkedListNode<Data> ln)
        {
            LinkedList<Data> linkedData = new LinkedList<Data>();
            ln = new LinkedListNode<Data>(null);
            PropertyInfo[] props = ini1.GetType().GetProperties();

            foreach (var property in props)
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

                    if (key == null)
                    {
                        key = attr as IniKeyAttribute;
                        if (key != null)
                            attributes[1] = key.Element;
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
                    if (ln != null && ln.Next == null)
                    {
                        wr.WriteLine();
                        wr.WriteLine("[{0}]", variable.Attributes[0]);
                    }
                        
                    else
                    {
                        if (ln != null &&
                            (ln.Previous != null && ln.Value.Attributes[0] != ln.Previous.Value.Attributes[0]))
                        {
                            wr.WriteLine();
                            wr.WriteLine("[{0}]", variable.Attributes[0]);
                        }
                            

                    }
                    wr.WriteLine("  {0} = {1}", variable.Attributes[1],  variable.Value);
                    if (ln != null) ln = ln.Previous;
                }
            }
        }
    }
}
