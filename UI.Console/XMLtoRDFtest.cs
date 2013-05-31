using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

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

			//IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
			//IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
			//ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
			//ILiteralNode bonjourMonde = g.CreateLiteralNode("Bonjour tout le Monde", "fr");

			//g.Assert(new Triple(dotNetRDF, says, helloWorld));
			//g.Assert(new Triple(dotNetRDF, says, bonjourMonde));

			//foreach (Triple t in g.Triples)
			//{
			//    Console.WriteLine(t.ToString());
			//}

			//NTriplesWriter ntwriter = new NTriplesWriter();
			//ntwriter.Save(g, "HelloWorld.nt");

			//RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();
			//rdfxmlwriter.Save(g, "HelloWorld.rdf");

			RdfXmlParser fileParser = new RdfXmlParser();
			fileParser.Load(g, fileName);

			VDS.RDF.Nam 
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
