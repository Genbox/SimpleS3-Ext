﻿using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BenchmarkDotNet.Attributes;

namespace Genbox.SimpleS3.Core.Benchmarks.Tests;

[InProcess]
[MemoryDiagnoser]
public class XmlReaderBenchmarks
{
    private readonly byte[] data = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                                                          + "<XmlTestObject>"
                                                          + "	<Person>"
                                                          + "		<Name>Santa Claus</Name>"
                                                          + "		<Age>800</Age>"
                                                          + "		<IsMissing>true</IsMissing>"
                                                          + "	</Person>"
                                                          + "	<Person>"
                                                          + "		<Name>Donald Trump</Name>"
                                                          + "		<Age>7</Age>"
                                                          + "		<IsMissing>false</IsMissing>"
                                                          + "	</Person>"
                                                          + "</XmlTestObject>");

    [Benchmark]
    public XmlTestObject? WithXmlDeserializer()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlTestObject));
        using (MemoryStream ms = new MemoryStream(data))
        using (XmlTextReader reader = new XmlTextReader(ms))
        {
            reader.Namespaces = false;
            return (XmlTestObject?)xmlSerializer.Deserialize(reader);
        }
    }

    [Benchmark]
    public XmlTestObject WithXmlReader()
    {
        using (MemoryStream ms = new MemoryStream(data))
        using (XmlTextReader reader = new XmlTextReader(ms))
        {
            XmlTestObject obj = new XmlTestObject();
            obj.People = new List<Person>(2);

            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                if (reader.LocalName != "Person")
                    continue;

                Person p = new Person();

                while (reader.Read())
                {
                    switch (reader.LocalName)
                    {
                        case "Name":
                            p.Name = reader.ReadString();
                            break;
                        case "Age":
                            p.Age = reader.ReadElementContentAsInt();
                            break;
                        case "IsMissing":
                            p.IsMissing = reader.ReadElementContentAsBoolean();
                            break;
                    }

                    if (reader.LocalName == "Person" && reader.NodeType == XmlNodeType.EndElement)
                        break;
                }

                obj.People.Add(p);
            }

            return obj;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMissing { get; set; }
    }

    [XmlRoot]
    public class XmlTestObject
    {
        [XmlElement("Person")] public List<Person> People { get; set; }
    }
}