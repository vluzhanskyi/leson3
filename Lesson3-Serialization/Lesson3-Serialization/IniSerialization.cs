using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Lesson3_Serialization
{
    public class IniSerialization
    {
       public Dictionary<object[], object> GetCollectionToSerialize(object iniObject)
        {
            var inPutDictionary = new Dictionary<object[], object>();
            
            foreach (var property in iniObject.GetType().GetProperties())
            {
                object[] attributes = new object[2];

                foreach (
                    var item in
                        from item in property.CustomAttributes
                        where item.AttributeType == typeof(IniSectionAttribute)
                        select item)
                {
                    attributes[0] = item.ConstructorArguments[0].Value;
                    
                    foreach (
                        var key in
                            from key in property.CustomAttributes
                            where key.AttributeType == typeof(IniKeyAttribute)
                            select key)
                    {
                        attributes[1] = key.ConstructorArguments[0].Value;
                        inPutDictionary.Add(attributes, property.GetValue(iniObject));
                    }
                }
            }
            return inPutDictionary;
        }
        public void SerializeToIni(Dictionary<object[], object> inPutDictionary, string file)
        {
            string previousSection = null;

            using (TextWriter writer = new StreamWriter(file))
            {
                foreach (var item in inPutDictionary)
                {
                    if (previousSection == null || previousSection != (string)item.Key[0])
                    {
                        writer.WriteLine();
                        writer.WriteLine("[{0}]", item.Key[0]);
                        previousSection = item.Key[0].ToString();
                    }

                    writer.WriteLine("{0} = {1}", item.Key[1], item.Value);
                }

            }
        }
            
        public Dictionary<object[], object> DeserializeFromIni(string file)
        {
            Dictionary<object[], object> Result = new Dictionary<object[], object>();
            List<PropertyInfo> props = new List<PropertyInfo>();

            using (TextReader reader = new StreamReader(file))
            {
                string allInputs = reader.ReadToEnd();
                allInputs = allInputs.Replace("\r\n", "  ");
                string[] sList = allInputs.Split(new string[] { "  "}, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string[], string> dataDictionary = CollectDataFromFile(sList);

                foreach (var item in dataDictionary)
                {
                    var section = new IniSectionAttribute(item.Key[0]);
                    var key = new IniKeyAttribute(item.Key[1]);
                    Result.Add(new object [] { section, key} , item.Value);
                }
            }
             
            return Result;
        }

        private Dictionary<string[], string> CollectDataFromFile(string[] inilines)
        {
            Dictionary<string[], string> dataDictionary = new Dictionary<string[], string>();

            string previousSectionName = null;

            foreach (var line in inilines)
            {
                string[] attributes = new string[2];

                if (line.Contains("]"))
                {
                    attributes[0] = new string((from c in line where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());
                    previousSectionName = attributes[0];
                }

                if (line.Contains("="))
                {
                    if (string.IsNullOrEmpty(attributes[0]))
                        attributes[0] = previousSectionName;
                    attributes[1] = line.Substring(0, line.LastIndexOf(" = "));
                    dataDictionary.Add(attributes, line.Substring(line.LastIndexOf(" = ") + 3));
                }

            }

            return dataDictionary;
        }

    }
}
