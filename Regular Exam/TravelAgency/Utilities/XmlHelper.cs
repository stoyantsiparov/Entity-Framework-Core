using System.Text;
using System.Xml.Serialization;

namespace TravelAgency.Utilities;

public class XmlHelper
{
    public T Deserialize<T>(string inputXml, string rootName)
    {
        XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

        using StringReader stringReader = new StringReader(inputXml);
        object? deserializedObj = xmlSerializer.Deserialize(stringReader);
        if (deserializedObj == null ||
            deserializedObj is not T deserializedObjTypes)
        {
            throw new InvalidOperationException();
        }

        return deserializedObjTypes;
    }

    public string Serialize<T>(T obj, string rootName)
    {
        StringBuilder sb = new StringBuilder();
        XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

        XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
        xmlSerializerNamespaces.Add(string.Empty, string.Empty);

        using StringWriter stringWriter = new StringWriter(sb);
        xmlSerializer.Serialize(stringWriter, obj, xmlSerializerNamespaces);

        return sb.ToString().TrimEnd();
    }
}