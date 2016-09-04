using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace FetchXmlWrapper
{
    public partial class FetchExpression
    {
        public string Entity()
        {
            //parse the XML
            XElement fetchXml = XElement.Parse(Serialize());

            //isolate the entity node
            XElement entity = fetchXml.Descendants("entity").FirstOrDefault();

            //get the value of the name attribute
            return entity.Attribute("name").Value.ToString();
        }

        public XElement ToXElement()
        {
            try
            {
                return XElement.Parse(Serialize());
            }
            catch (Exception e)
            {
                //throw new ArgumentException("Invalid Fetch.");
                throw e;
            }
        }

        public string ToXml()
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(Serialize());

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }
    }
}
