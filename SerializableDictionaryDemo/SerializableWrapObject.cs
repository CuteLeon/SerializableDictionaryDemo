using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializableDictionaryDemo
{
    [Serializable]
    public class SerializableWrapObject
    {
        public string Title { get; set; }

        private SerializableDictionary<string, decimal> values;

        [XmlElement("XMLWrapValues")]
        [JsonProperty("JSONWrapValues")]
        public SerializableDictionary<string, decimal> Values { get => values; set => values = value; }
    }
}
