using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace SemanticDataEnrichment.UI.TestConsole
{
    class XMLtoRDFtest
    {
        public XMLtoRDFtest()
        { }

        public List<RdfEntityDescriptor> ReadXML(string fileName)
        {
            List<RdfEntityDescriptor> data = new List<RdfEntityDescriptor>();

            XElement xml = XElement.Parse(File.ReadAllText(fileName));

            XElement facts = xml.Element("facts");
            if (facts == null)
                return data;

            foreach (XElement fact in facts.Elements())
            {
                RdfEntityDescriptor entity = new RdfEntityDescriptor(fact.Name.LocalName);
                foreach (XAttribute attrib in fact.Attributes())
                    entity.AddProperty(attrib.Name.LocalName, attrib.Value);
                foreach (XElement prop in fact.Elements())
                    entity.AddProperty(prop.Name.LocalName, prop.Attribute("val").Value);
                data.Add(entity);
            }

            return data;
        }

        public void ReadRDL(string fileName)
        {
            IGraph g = new Graph();
            RdfXmlParser fileParser = new RdfXmlParser();
            fileParser.Load(g, fileName);
        }
    }

    struct RdfPropertyElement
    {
        public RdfPropertyElement(string name, string value)
        { 
            Name = name;
            Value = value;
        }
        string Name;
        string Value;

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name, Value);
        }
    }

    class RdfEntityDescriptor : LinkedList<RdfPropertyElement>
    {
        public RdfEntityDescriptor() { }
        public RdfEntityDescriptor(string rdfClassName)
        {
            RdfClassName = rdfClassName;
        }

        public string RdfClassName { get; set; }

        public void AddProperty(string propertyName, string propertyValue)
        {
            this.AddLast(new RdfPropertyElement(propertyName, propertyValue));
        }

        public override string ToString()
        {
            return String.Format("Name: '{0}', PropertyCount: {1}", RdfClassName, this.Count);
        }
    }
}
