using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;


namespace Lesson3_Serialization
{
    public class Logic
    {
        public Dictionary<object[], object> CollectData(MyIni ini1)
        {

            //LinkedList<Data> linkedData = new LinkedList<Data>();
            //ln = new LinkedListNode<Data>(null);
            PropertyInfo[] props = ini1.GetType().GetProperties();

            var serialDictionary = new Dictionary<object[], object>();

            foreach (PropertyInfo var in props)
            {
                    serialDictionary.Add(var.GetCustomAttributes(true), var.GetValue(ini1));
            }
                       


            //foreach (var property in props)
            //{
            //    IniSectionAttribute section = null;
            //    IniKeyAttribute key = null;
            //    string[] attributes = new string[2];
            //    object[] attrs = property.GetCustomAttributes(true);

            //    foreach (var attr in attrs)
            //    {
            //        if (section == null)
            //        {
            //            section = attr as IniSectionAttribute;
            //            if (section != null)
            //                attributes[0] = section.Element;
            //        }

            //        if (key == null)
            //        {
            //            key = attr as IniKeyAttribute;
            //            if (key != null)
            //                attributes[1] = key.Element;
            //        }
            //    }
            //    object value = property.GetValue(ini1);
            //    Data inputData = new Data(value, attributes);
            //    ln = linkedData.AddLast(inputData);
            //}

            return serialDictionary;
        }

        public void PrintToIni(Dictionary<object[], object> inputDaDictionary, string file)
        {
            using (TextWriter writer = new StreamWriter(file))
            {
                foreach (var valuePair in inputDaDictionary)
                {
                    foreach (var o in valuePair.Key)
                    {
                        writer.WriteLine("[{0}]", o);
                    }
                   
                   writer.WriteLine("{0} = {1}", valuePair.Key[1], valuePair.Value);
                }

                //foreach (var variable in linkedData)
                //{
                //    if (ln != null && ln.Next == null)
                //    {
                //        writer.WriteLine();
                //        writer.WriteLine("[{0}]", variable.Attributes[0]);
                //    }
                        
                //    else
                //    {
                //        if (ln != null &&
                //            (ln.Previous != null && ln.Value.Attributes[0] != ln.Previous.Value.Attributes[0]))
                //        {
                //            writer.WriteLine();
                //            writer.WriteLine("[{0}]", variable.Attributes[0]);
                //        }
                            

                //    }
                //    writer.WriteLine("  {0} = {1}", variable.Attributes[1],  variable.Value);
                //    if (ln != null) ln = ln.Previous;
                }
            }
        }

        //public LinkedList<Data> GetDataFromIni()
        //{
        //    LinkedList<Data> linkedData = new LinkedList<Data>();
        //    var ln = new LinkedListNode<Data>(null);

        //    using (TextReader reader = new StreamReader("Out.ini"))
        //    {
        //        string text = reader.ReadToEnd();
        //        //text = text.TrimEnd(Convert.ToChar("="));
        //        text = text.Remove('\n');
        //        string[] textArr = text.Split('\r');

        //        foreach (string line in textArr)
        //        {
        //            if (text.Equals("owner", StringComparison.InvariantCultureIgnoreCase) || text.Equals("database", StringComparison.InvariantCultureIgnoreCase))
        //                ln.Value.Attributes[0] = text;
        //            else if (text.Equals("name", StringComparison.InvariantCultureIgnoreCase) || text.Equals("organization", StringComparison.InvariantCultureIgnoreCase) ||
        //                text.Equals("server", StringComparison.InvariantCultureIgnoreCase) || text.Equals("port", StringComparison.InvariantCultureIgnoreCase) ||
        //            text.Equals("file", StringComparison.CurrentCultureIgnoreCase))
        //            {
        //                ln.Value.Attributes[1] = text;
        //            }
        //        }
               
                

        //    }

       //     return linkedData;
       // }
   // }
}
