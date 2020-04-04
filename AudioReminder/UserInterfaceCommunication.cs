using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AudioReminder
{
    class UserInterfaceCommunication
    {
        const string NamedPipeName = @"AudioReminder-3D7C1DE8-2DFB-4291-9396-8E0CA4E8AD10";


        NamedPipeServerStream pipeServer = new NamedPipeServerStream(NamedPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
        
        public void test()
        {
            // Wait for a client to connect
            pipeServer.WaitForConnection();

        }

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
