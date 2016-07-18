using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Lesson3_Serialization
{
    public class IniSerialization
    {
        public void SerializeToIni(MyIni ini1, string file)
        {
            PropertyInfo[] props = ini1.GetType().GetProperties();
            
            using (TextWriter writer = new StreamWriter(file))
            {
                string previousSection = null;
                foreach (var property in props)
                {
                    foreach (
                        var item in
                            from item in property.CustomAttributes
                            where item.AttributeType == typeof(IniSectionAttribute)
                            select item)
                    {
                        if (previousSection == null || previousSection != (string) item.ConstructorArguments[0].Value)
                        {
                            writer.WriteLine();
                            writer.WriteLine("[{item.ConstructorArguments[0].Value}]");
                        }
                        previousSection = (string) item.ConstructorArguments[0].Value;
                        
                        foreach (
                            var key in
                                from key in property.CustomAttributes
                                where key.AttributeType == typeof(IniKeyAttribute)
                                select key)
                        {
                            writer.WriteLine("{key.ConstructorArguments[0].Value} = {property.GetValue(ini1)}");
                        }
                    }
                }
            }
        }

        public MyIni DeserializeFromIni(string file)
        {
            MyIni Result = new MyIni(name: "", organization: "", server: "", port: 0, file: "");

            using (TextReader reader = new StreamReader(file))
            {
                string allInputs = reader.ReadToEnd();
                allInputs = allInputs.Replace("\r\n", " ");
                string[] sList = allInputs.Split('[');
                Dictionary<string[], string> dataDictionary = new Dictionary<string[], string>();
                List<string> keysList = new List<string>();
                string previousSectionName = null;

                foreach (var line in sList)
                {
                    string[] attributes = new string[2];
                    
                    if (line.Contains("]"))
                    {
                        attributes[0] = line.Trim(']');
                        previousSectionName = line;
                    }

                    if (line.Contains("="))
                    {
                        if (!string.IsNullOrEmpty(attributes[0] = String.Empty))
                            attributes[0] = previousSectionName;
                        attributes[1] = line.TrimEnd('=');
                    }

                }
            }

            return Result;
        }

    }
}
