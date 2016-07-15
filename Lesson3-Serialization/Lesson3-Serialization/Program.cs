using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace Lesson3_Serialization
{
    class Program
    {
        static void Main()
        {           
            MyIni myIni = new MyIni(name: "Slava", organization: "Cisco", server: "1.1.1.1", port: 5060, file: "RTP");
            IniSerialization l = new IniSerialization();
            Dictionary<object[], object> ini1 = l.GetCollectionToSerialize(myIni);

            l.SerializeToIni(ini1, "out.ini");

            Dictionary<object[], object> ini2 = l.DeserializeFromIni("out.ini");
                        

            Console.ReadLine();
        }
    }
    
}
