using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializableDictionaryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var wrapObject = new WrapObject()
            {
                Title = "I am WrapObject",
                Values = new Dictionary<string, decimal>()
                {
                    { "Life", 100000m },
                    { "CI", 80000m },
                    { "TPD", 60000m },
                    { "CIWOP", 40000m },
                }
            };

            var wrapObjectXML = string.Empty;
            try
            {
                var serializer = new XmlSerializer(typeof(WrapObject));
                using (MemoryStream stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        serializer.Serialize(writer, wrapObject);
                        wrapObjectXML = Encoding.UTF8.GetString(stream.GetBuffer());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML序列化原生字典：{ex.Message}");
            }
            Console.WriteLine($"wrapObjectXML => {wrapObjectXML}");

            var wrapObjectJSON = string.Empty;
            try
            {
                wrapObjectJSON = JsonConvert.SerializeObject(wrapObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON序列化原生字典：{ex.Message}");
            }
            Console.WriteLine($"wrapObjectJSON => {wrapObjectJSON}");

            WrapObject wrapObjectClone = null;
            try
            {
                wrapObjectClone = JsonConvert.DeserializeObject<WrapObject>(wrapObjectJSON);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON反序列化原生字典：{ex.Message}");
            }
            Console.WriteLine($"wrapObjectClone => {wrapObjectClone.Title}, {string.Join(", ", wrapObjectClone.Values.Select(pair => $"{pair.Key} = {pair.Value}"))}");
            Console.WriteLine("————————————————————————————");

            var serializableWrapObject = new SerializableWrapObject()
            {
                Title = "I am SerializableWrapObject",
                Values = new SerializableDictionary<string, decimal>()
                {
                    { "Life", 100000m },
                    { "CI", 80000m },
                    { "TPD", 60000m },
                    { "CIWOP", 40000m },
                }
            };
            SerializableWrapObject serializableWrapObjectClone = null;

            var serializableWrapObjectXML = string.Empty;
            try
            {
                var serializer = new XmlSerializer(typeof(SerializableWrapObject));
                using (MemoryStream stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        serializer.Serialize(writer, serializableWrapObject);
                        serializableWrapObjectXML = Encoding.UTF8.GetString(stream.GetBuffer());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML可序列化字典：{ex.Message}");
            }
            Console.WriteLine($"serializableWrapObjectXML => {serializableWrapObjectXML}");

            try
            {
                var serializer = new XmlSerializer(typeof(SerializableWrapObject));
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(serializableWrapObjectXML)))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        serializableWrapObjectClone = serializer.Deserialize(reader) as SerializableWrapObject;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML可反序列化字典：{ex.Message}");
            }
            Console.WriteLine($"serializableWrapObjectClone => {serializableWrapObjectClone.Title}, {string.Join(", ", serializableWrapObjectClone.Values.Select(pair => $"{pair.Key} = {pair.Value}"))}");

            var serializableWrapObjectJSON = string.Empty;
            try
            {
                serializableWrapObjectJSON = JsonConvert.SerializeObject(serializableWrapObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON可序列化字典：{ex.Message}");
            }
            Console.WriteLine($"serializableWrapObjectJSON => {serializableWrapObjectJSON}");

            try
            {
                serializableWrapObjectClone = JsonConvert.DeserializeObject<SerializableWrapObject>(serializableWrapObjectJSON);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON可反序列化字典：{ex.Message}");
            }
            Console.WriteLine($"serializableWrapObjectClone => {serializableWrapObjectClone.Title}, {string.Join(", ", serializableWrapObjectClone.Values.Select(pair => $"{pair.Key} = {pair.Value}"))}");

            Console.Read();
        }
    }
}
