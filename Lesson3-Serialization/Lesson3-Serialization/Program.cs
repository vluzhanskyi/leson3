using System.Collections.Generic;


namespace Lesson3_Serialization
{
    class Program
    {
        static void Main()
        {           
            MyIni ini1 = new MyIni(name: "Slava", organization: "Cisco", server: "1.1.1.1", port: 5060, file: "RTP");

           // LinkedListNode<Data> ln;          
            Logic l = new Logic();
            Dictionary<object[], object> inputDictionary = l.CollectData(ini1);
           // LinkedList<Data> linkedData = l.CollectData(ini1, out ln);
          //  l.SerializeToIni(linkedData, ln);
            l.SerializeToIni(inputDictionary, "out.ini");
          //  LinkedList<Data> ReceivedData = l.GetDataFromIni();
        }
    }
    
}
