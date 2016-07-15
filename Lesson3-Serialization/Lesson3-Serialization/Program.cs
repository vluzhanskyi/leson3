using System.Collections.Generic;
using System;
using System.Reflection;

namespace Lesson3_Serialization
{
    class Program
    {
        static void Main()
        {           
            MyIni ini1 = new MyIni(name: "Slava", organization: "Cisco", server: "1.1.1.1", port: 5060, file: "RTP");

           // LinkedListNode<Data> ln;          
            IniSerialization l = new IniSerialization();
           // Dictionary<PropertyInfo, object> inputDictionary = l.CollectData(ini1);
           // LinkedList<Data> linkedData = l.CollectData(ini1, out ln);
          //  l.SerializeToIni(linkedData, ln);
            l.SerializeToIni(ini1, "out.ini");

            MyIni ini2 = l.DeserializeFromIni("out.ini");
            //  LinkedList<Data> ReceivedData = l.GetDataFromIni();
        }
    }
    
}
