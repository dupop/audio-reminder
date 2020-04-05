using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AudioReminder
{
    public static class SerializationHelper
    {
        public static string ToXmlString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringBuilder stringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(stringBuilder))
            {
                serializer.Serialize(xmlWriter, obj);
            }
            return stringBuilder.ToString();
        }

        public static TObjectType FromXmlString<TObjectType>(string xmlString)
        {
            var deserializer = new XmlSerializer(typeof(TObjectType));
            TObjectType objectFromXmlString;
            using (var xmlReader = XmlReader.Create(new StringReader(xmlString)))
            {
                objectFromXmlString = (TObjectType)deserializer.Deserialize(xmlReader);
            }

            return objectFromXmlString;
        }

    }
}
