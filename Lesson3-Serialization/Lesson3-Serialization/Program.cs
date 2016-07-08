using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson3_Serialization
{
    class Program
    {
        static void Main(string[] args)
        {           
            MyIni ini1 = new MyIni(name: "Slava", organization: "Cisco", server: "1.1.1.1", port: 5060, file: "RTP");
            LinkedList<Data> linkedData = new LinkedList<Data>();
            LinkedListNode<Data> ln = new LinkedListNode<Data>(null);

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

    internal class Data
    {
        public Data(object value, string[] attributes)
        {
            Value = value;
            Attributes = attributes;
        }
        public object Value { get; set; }
        public string[] Attributes { get; set; }
    }

    public class MyIni
    {
       public MyIni(string name, string organization, string server, int port, string file)
        {
            Name = name;
            Organization = organization;
            Server = server;
            Port = port;
            File = file;
        }

        [IniSection(elementName: "owner")]
        [IniKey(elementName: "name")]
        public string Name { get; set; }
       
        [IniSection(elementName: "owner")]
        [IniKey(elementName: "organization")]
        public string Organization { get; set; }

        [IniSection(elementName: "database")]
        [IniKey(elementName: "server")]
        public string Server { get; set; }

        [IniSection(elementName: "database")]
        [IniKey(elementName: "port")]
        public int Port { get; set; }

        [IniSection(elementName: "database")]
        [IniKey(elementName: "file")]
        public string File { get; set; }
    }
}
