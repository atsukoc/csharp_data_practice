using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
namespace csharp_DataParse
{
    class Program
    {
        // these are the xml elements in each name node 
        const string _last_name = "family";
        const string _middle_name = "middle";
        const string _first_name = "given";

        static void Main(string[] args)
        {
            
            XmlDocument doc = LoadXmlFile("sample_patient_1.xml");
            string patientId = getPatientId(doc);
            Dictionary<string, string> officialName = getOfficialName(doc);

            // display patient info

            Console.WriteLine(String.Format("patient id: {0}", patientId));

            if(officialName.Count > 0)
            {
                foreach(KeyValuePair<string, string> item in officialName)
                {
                    Console.WriteLine(String.Format("{0} name: {1}", item.Key, item.Value));
                }
                
            }
            
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

        static string getPatientId(XmlDocument doc)
        {
            string id = null;

            try
            {
                XmlNode node = doc.SelectSingleNode("Patient/id");
                id = node.Attributes[0].InnerText;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if(id == null)
            {
                throw new Exception("patient id is null");

            }

            return id;
            
        }
        static Dictionary<String, String> getOfficialName(XmlDocument doc)
        {
            Dictionary<String, string> officialName = new Dictionary<string, string>();
            XmlNodeList names = doc.SelectNodes("Patient/name");

            foreach (XmlNode node in names)
            {
                XmlElement element = (XmlElement)node.SelectSingleNode("use");
                if (element.Attributes[0].InnerText == "official")
                {
                    XmlElement familyName = (XmlElement)node.SelectSingleNode(_last_name);
                    officialName.Add(_last_name, familyName.Attributes[0].InnerText);
                    
                    XmlNodeList givenNames = node.SelectNodes(_first_name);
                    officialName.Add(_first_name, givenNames[0].Attributes[0].InnerText);
                    officialName.Add(_middle_name, givenNames[1].Attributes[0].InnerText);
                
                }

            }

            return officialName;
        }
    }
}
