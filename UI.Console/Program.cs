using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticDataEnrichment.Core;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using System.Xml.Linq;
using System.Linq;


namespace SemanticDataEnrichment.UI.TestConsole
{
	class Program
	{
		private const string WORK_CONSOLE_FILE_NAME = "Tomita\\tomitaparser.exe";
		private const string OUTPUT_FILE_NAME = "Tomita\\facts.xml";

		static void Main(string[] args)
		{
			//RdfQueryViewModel model = new RdfQueryViewModel();
			//Console.WriteLine(model.ExecuteQuery());

			TestRDF();

			//TestCount("FdoDS.rdf", "CompanyName");

            //new XMLtoRDFtest().ReadXML("FdoDS.rdf");
            //new XMLtoRDFtest().ReadRDL("TestRdf.xml");
            //var xml = new XMLtoRDFtest().ConvertToRdf("TestXml.xml");
            //string xml = new XMLtoRDFtest().ConvertToRdf("TestXml.xml").ToString();
            //using (FileStream f = new FileStream("output.txt", FileMode.Truncate))
            //{
            //    f.
            //}
            //File.WriteAllText("output.txt", xml);
            //xml.Save("_output.xml");
            //Console.WriteLine(xml);
			//TestProto();
			//TestRDF();
			//Encoding
			//ConfigViewModel config = new ConfigViewModel();
			//config.Test();
			//using(FileStream s = new FileStream("Testser.txt", FileMode.Create))
			//{
			//    IFormatter f = new BinaryFormatter();
			//    f.Serialize(s, test);
			//    s.Close();
			//}

			//ProcessViewModel model = new ProcessViewModel(WORK_CONSOLE_FILE_NAME, OUTPUT_FILE_NAME);
			//model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
			//model.TextData = "qwertyuiop";
			//model.ProcessText();
			Console.ReadKey();
		}

		//static void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		//{
		//    if (e.PropertyName == "ConsoleOutput")
		//        Console.WriteLine((sender as ProcessViewModel).ConsoleOutput);
		//}

		/// <summary>
		/// https://bitbucket.org/dotnetrdf/dotnetrdf/wiki/User%20Guide
		/// </summary>
		static void TestRDF()
		{
			IGraph g = new Graph();
			RdfXmlParser fileParser = new RdfXmlParser();
			//fileParser.Load(g, "schema.xml");
			fileParser.Load(g, "TestRdf.xml");

			//SparqlQueryParser queryParser = new SparqlQueryParser();
			//queryParser.ParseFromString("SELECT * WHERE { ?s a ?type }");
			//VDS.RDF.Writing.StringWriter.Write(g, new RdfXmlWriter())

			StringBuilder div = new StringBuilder();

			XElement rdf = XElement.Parse(VDS.RDF.Writing.StringWriter.Write(g, new RdfXmlWriter()));
			foreach (XElement element in rdf.Elements())
			{
				div.AppendFormat("<div itemscope itemtype=\"{0}/{1}\">", element.Name.NamespaceName.TrimEnd('/'), element.Name.LocalName);
				div.AppendLine();
				foreach (XElement content in element.Elements())
				{
					div.AppendFormat("<meta itemprop=\"{0}\" content=\"{1}\">", content.Name.LocalName, content.Value);
					div.AppendLine();
				}
				div.AppendLine("</div>");
			}

			var output = g.ExecuteQuery("SELECT * WHERE { ?s a ?type }");



			 
			SparqlParameterizedString query = new SparqlParameterizedString();
			query.Namespaces.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
			query.Namespaces.AddNamespace("owl", new Uri("http://www.w3.org/2002/07/owl#"));
			query.Namespaces.AddNamespace("xsd", new Uri("http://www.w3.org/2001/XMLSchema#"));
			query.Namespaces.AddNamespace("rdfs", new Uri("http://www.w3.org/2000/01/rdf-schema#"));
			query.Namespaces.AddNamespace("schema", new Uri("http://schema.org/"));
			query.Namespaces.AddNamespace("fact", new Uri("http://www.co-ode.org/ontologies/ont.owl#"));

			query.CommandText = "SELECT * WHERE { ?organization rdf:type fact:UniqueCompany }";

			var output2 = g.ExecuteQuery(query);

//            var output2 = g.ExecuteQuery(@"PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
//										PREFIX owl: <http://www.w3.org/2002/07/owl#>
//										PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
//										PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
//										PREFIX schema: <http://schema.org/>
//										PREFIX fact:<http://www.co-ode.org/ontologies/ont.owl#>
//										SELECT *
//										WHERE { ?organization rdf:type fact:UniqueCompany }");




		}

		static void TestProto()
		{
			TTextMinerConfig config = new TTextMinerConfig() { PrettyOutput = "qwert.html", Output = new TTextMinerConfig.TOutputParams() { File = "asdf.xml", Format = TTextMinerConfig.TOutputParams.OutputFormat.xml } };

			using (var file = File.Create("test.proto"))
			{
				//ProtoBuf.Serializer.Serialize(file, config);
				//ProtoBuf.Serializer.SerializeWithLengthPrefix(file, config, ProtoBuf.PrefixStyle.Base128);
				//ProtoBuf.Serializer.Serialize(new Seriali
			}

			TTextMinerConfig config2;
			using (var file = new FileStream("test.proto", FileMode.Open))
			{
				//file.SetLength(file.Length);
				config2 = ProtoBuf.Serializer.Deserialize<TTextMinerConfig>(file);
			}
		}

		static void TestCount(string filename, string propertyName)
		{
			string propertyValue = String.Empty;
			XNamespace owl = "http://www.w3.org/2002/07/owl#";
			XNamespace ont = "http://www.co-ode.org/ontologies/ont.owl#";
			XElement rdf = XElement.Parse(File.ReadAllText(filename));

			var my = rdf.Elements(owl + "NamedIndividual")
				.Where(el => el.Elements().Select(par => par.Name.LocalName).Contains(propertyName) && el.Elements(ont + "pos").Any())
				.GroupBy(n => n.Element(ont + propertyName).Value)
				.Select(o => new
				{
					PropertyValue = o.Key,
					Count = o.Count(),
					MinPos = o.Min(p => int.Parse(p.Element(ont + "pos").Value))// o.Elements(ont + "pos")//.Select(p => int.Parse(p.Value)).Min()
				})
				.OrderByDescending(r => r.Count).ThenBy(e => e.MinPos).First().PropertyValue;

		}
	}
}
