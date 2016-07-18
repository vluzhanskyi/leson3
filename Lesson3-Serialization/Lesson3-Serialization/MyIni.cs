namespace Lesson3_Serialization
{
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
