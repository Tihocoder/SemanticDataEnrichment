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


namespace SemanticDataEnrichment.UI.TestConsole
{
	class Program
	{
		private const string WORK_CONSOLE_FILE_NAME = "Tomita\\tomitaparser.exe";
		private const string OUTPUT_FILE_NAME = "Tomita\\facts.xml";

		static void Main(string[] args)
		{
			TestRDF();
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
			fileParser.Load(g, "schema.xml");

			//SparqlQueryParser queryParser = new SparqlQueryParser();
			//queryParser.ParseFromString("SELECT * WHERE { ?s a ?type }");

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
//										WHERE { ?organization rdf:type fact:UniqueCompany");




		}
	}
}
