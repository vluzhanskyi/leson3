using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson3_Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
           
            MyIni ini1 = new MyIni();
            ini1.Name = "Slava";
            ini1.Organization = "CISCO";
            ini1.ServerIP = "1.1.1.1";
            ini1.Port = 5060;
            ini1.File = "RTP";

            XmlSerializer serializer = new XmlSerializer(typeof(MyIni));

            using (TextWriter wr = new StreamWriter("Out.ini"))
            {
                wr.WriteLine(ini1);
  
            }

            using (TextWriter wr = new StreamWriter("Out.xml"))
            {
                serializer.Serialize(wr, ini1);
            }
            
        }
    }

    public class MyIni
    {
        [IniSection(elementName: "owner")]
        [iniKey(elementName: "name")]
        public string Name { get; set; }

        [IniSection(elementName: "owner")]
        [iniKey(elementName: "organization")]
        public string Organization { get; set; }

        [IniSection(elementName: "section")]
        [iniKey(elementName: "serverIP")]
        public string ServerIP { get; set; }

        [IniSection(elementName: "section")]
        [iniKey(elementName: "port")]
        public int Port { get; set; }

        [IniSection(elementName: "section")]
        [iniKey(elementName: "file")]
        public string File { get; set; }
    }
}
