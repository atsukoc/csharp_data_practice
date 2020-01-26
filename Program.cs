using System;
using System.IO;
using System.Xml;
namespace csharp_DataParse
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument doc = LoadXmlFile("sample_patient_1.xml");
            getOfficialName(doc);
        }

        static XmlDocument LoadXmlFile(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;

            try
            {
                doc.Load(filepath);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File doesn't exist");
            }

            return doc;

        }

        static string getOfficialName(XmlDocument doc)
        {
            string family = "";

            XmlNodeList names = doc.SelectNodes("Patient/name");
            foreach (XmlNode node in names)
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("use");
                if (element.Attributes[0].InnerText == "official")
                {
                    XmlElement familyName = (XmlElement)node.SelectSingleNode("family");
                    family = familyName.Attributes[0].InnerText;
                    Console.WriteLine(family);
                }

            }

            return family;
        }
    }
}
