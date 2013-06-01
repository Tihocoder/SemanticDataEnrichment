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

        public XDocument ConvertToRdf(string fileName)
        {
            XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            XNamespace owl = "http://www.w3.org/2002/07/owl#";
            XNamespace ont = "http://www.co-ode.org/ontologies/ont.owl#";

            XElement root = new XElement(rdf + "RDF"
                , new XAttribute(XNamespace.Xmlns + "rdf", rdf.NamespaceName)
                , new XAttribute(XNamespace.Xmlns + "owl", owl.NamespaceName)
                , new XAttribute(XNamespace.Xmlns + "ont", ont.NamespaceName)
            );

            string internalSubset = @"<!ENTITY mstns ""http://tempuri.org/FdoDS.xsd"" >
<!ENTITY owl ""http://www.w3.org/2002/07/owl#"" >
<!ENTITY xs ""http://www.w3.org/2001/XMLSchema"" >
<!ENTITY xsd ""http://www.w3.org/2001/XMLSchema#"" >
<!ENTITY msprop ""urn:schemas-microsoft-com:xml-msprop"" >
<!ENTITY msdata ""urn:schemas-microsoft-com:xml-msdata"" >
<!ENTITY rdfs ""http://www.w3.org/2000/01/rdf-schema#"" >
<!ENTITY ont ""http://www.co-ode.org/ontologies/ont.owl#"" >
<!ENTITY rdf ""http://www.w3.org/1999/02/22-rdf-syntax-ns#"" >";

            XDocument outputRdf = new XDocument(new XDocumentType("rdf:RDF", null, null, internalSubset), root);

            XElement xml = XElement.Parse(File.ReadAllText(fileName));
            XElement facts = xml.Descendants("facts").FirstOrDefault();
            if (facts == null)
                return outputRdf;

            foreach (XElement fact in facts.Elements())
            {
                XElement namedIndividual = new XElement(
                        owl + "NamedIndividual"
                    //, new XAttribute(rdf + "about",  "&" + root.GetPrefixOfNamespace(ont) + ";" + fact.Attribute("FactID").Value)
                    //, new XElement(rdf + "type", new XAttribute(rdf + "resource", "&" + root.GetPrefixOfNamespace(ont) + ";" + fact.Name.LocalName))
                        , new XAttribute(rdf + "about", ont.NamespaceName + fact.Attribute("FactID").Value)
                        , new XElement(rdf + "type", new XAttribute(rdf + "resource", ont.NamespaceName + fact.Name.LocalName))
                    );

                foreach (XAttribute attrib in fact.Attributes())
                    namedIndividual.Add(new XElement(ont + attrib.Name.LocalName, attrib.Value));
                foreach (XElement prop in fact.Elements())
                    namedIndividual.Add(new XElement(ont + prop.Name.LocalName, prop.Attribute("val").Value));

                root.Add(namedIndividual);
            }



            return outputRdf;
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

			//VDS.RDF.Nam 
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
