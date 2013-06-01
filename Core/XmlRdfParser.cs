using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SemanticDataEnrichment.Core
{
	/// <summary>
	/// Конвертирование xml-ответов томиты в rdf-документы и/или файлы для дальнейшей обработки
	/// </summary>
    internal class XmlRdfParser
    {
		private Dictionary<string, XNamespace> namespaces;

		public XmlRdfParser()
		{
			this.namespaces = new Dictionary<string, XNamespace>()
			{
				{"rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#"}
				, {"owl", "http://www.w3.org/2002/07/owl#"}
				, {"ont", "http://www.co-ode.org/ontologies/ont.owl#"}
			};
		}

		#region PublicMethods

		/// <summary>
		/// Конвертирует xml-ответ томиты в rdf-документ
		/// </summary>
		/// <param name="source">xml-ответ томиты</param>
		/// <returns>rdf-документ</returns>
		public XDocument ConvertXmlRdf(XElement source)
		{
			XDocument outputRdf = CreateEmptyRdfDocument();
			XElement root = CreateEmptyRoot();
			outputRdf.Add(root);

			XElement facts = source.Descendants("facts").FirstOrDefault();
			if (facts == null)
				return outputRdf;

			foreach (XElement fact in facts.Elements())
			{
				XElement namedIndividual = new XElement(
						this.namespaces["owl"] + "NamedIndividual"
						, new XAttribute(this.namespaces["rdf"] + "about", this.namespaces["ont"].NamespaceName + fact.Attribute("FactID").Value)
						, new XElement(this.namespaces["rdf"] + "type", new XAttribute(this.namespaces["rdf"] + "resource", this.namespaces["ont"].NamespaceName + fact.Name.LocalName))
					);

				foreach (XAttribute attrib in fact.Attributes())
					namedIndividual.Add(new XElement(this.namespaces["ont"] + attrib.Name.LocalName, attrib.Value));
				foreach (XElement prop in fact.Elements())
					namedIndividual.Add(new XElement(this.namespaces["ont"] + prop.Name.LocalName, prop.Attribute("val").Value));

				root.Add(namedIndividual);
			}
			return outputRdf;
		}

		/// <summary>
		/// Конвертирует xml-ответ томиты в rdf-документ, который сохраняет в файл
		/// </summary>
		/// <param name="source">xml-ответ томиты</param>
		/// <param name="destFileName">Имя файла для сохранения результата</param>
		/// <returns>rdf-документ</returns>
		public XDocument ConvertXmlRdf(XElement source, string destFileName)
		{
			XDocument outputRdf = ConvertXmlRdf(source);
			outputRdf.Save(destFileName);
			return outputRdf;
		}

		/// <summary>
		/// Конвертирует xml-файл (итог работы томиты) в rdf-документ, который сохраняет в файл
		/// </summary>
		/// <param name="sourceFileName">Имя файла с xml-ответом томиты</param>
		/// <param name="destFileName">Имя файла для сохранения результата</param>
		/// <returns>rdf-документ</returns>
        public XDocument ConvertXmlRdf(string sourceFileName, string destFileName)
        {
			XElement source = XElement.Parse(File.ReadAllText(sourceFileName));
			return ConvertXmlRdf(source, destFileName);
        }

		#endregion

		#region StaticMethods

		/// <summary>
		/// Возвращает отформатированный переносами строк и пробелами текст
		/// </summary>
		/// <param name="fileName">Файл с xml-данными</param>
		/// <returns>Отформатированный текст</returns>
		public static string GetFormattedStringFromXmlFile(string fileName)
		{
			return XDocument.Parse(File.ReadAllText(fileName)).ToString();
		}

		#endregion

		#region PrivateMethods

		/// <summary>
		/// Создает пустой XDocument с шапкой rdf семантики, заполненной "по умолчанию"
		/// </summary>
		private XDocument CreateEmptyRdfDocument()
        {
			string internalSubset = @"<!ENTITY mstns ""http://tempuri.org/FdoDS.xsd"" >
<!ENTITY owl ""http://www.w3.org/2002/07/owl#"" >
<!ENTITY xs ""http://www.w3.org/2001/XMLSchema"" >
<!ENTITY xsd ""http://www.w3.org/2001/XMLSchema#"" >
<!ENTITY msprop ""urn:schemas-microsoft-com:xml-msprop"" >
<!ENTITY msdata ""urn:schemas-microsoft-com:xml-msdata"" >
<!ENTITY rdfs ""http://www.w3.org/2000/01/rdf-schema#"" >
<!ENTITY ont ""http://www.co-ode.org/ontologies/ont.owl#"" >
<!ENTITY rdf ""http://www.w3.org/1999/02/22-rdf-syntax-ns#"" >";

			return new XDocument(new XDocumentType("rdf:RDF", null, null, internalSubset));
        }

		/// <summary>
		/// Создает пустой корневой тег с прописанными неймспейсами для rdf документа
		/// </summary>
		private XElement CreateEmptyRoot()
		{
			XElement root = new XElement(this.namespaces["rdf"] + "RDF");
			foreach (string namespacePrefix in this.namespaces.Keys)
				root.Add(new XAttribute(XNamespace.Xmlns + namespacePrefix, namespaces[namespacePrefix].NamespaceName));
			return root;
		}

		#endregion
	}
}
