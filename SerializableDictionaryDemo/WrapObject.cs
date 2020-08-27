using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializableDictionaryDemo
{
    [Serializable]
    public class WrapObject
    {
        public string Title { get; set; }

        private Dictionary<string, decimal> values;

        [XmlElement("XMLWrapValues")]
        [JsonProperty("JSONWrapValues")]
        public Dictionary<string, decimal> Values { get => values; set => values = value; }
    }
}
