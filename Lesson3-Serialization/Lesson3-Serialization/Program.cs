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
            LinkedListNode<Data> ln = new LinkedListNode<Data>(null);          
            Logic l = new Logic();
            LinkedList<Data> linkedData = l.CollectData(ini1, out ln);
            l.Print(linkedData, ln);
        }
    }
    
}
