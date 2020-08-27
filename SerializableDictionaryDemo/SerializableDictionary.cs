using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SerializableDictionaryDemo
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                TKey tk = (TKey)KeySerializer.Deserialize(reader);
                TValue vl = (TValue)ValueSerializer.Deserialize(reader);

                this.Add(tk, vl);
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keySerializer.Serialize(writer, pair.Key);
                valueSerializer.Serialize(writer, pair.Value);
            }
        }
    }
}
