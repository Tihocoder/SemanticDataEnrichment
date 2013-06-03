using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace SemanticDataEnrichment.Core
{
	/// <summary>
	/// Модель для отправки Sparql запросов к файлам, перечисленным в FilesToQuery
	/// </summary>
    public class RdfQueryViewModel : ViewModelBase
    {
		public RdfQueryViewModel()
		{
			this.filesToQuery = new ObservableCollection<FilePath>();
		}

		#region PublicProperties

		private ObservableCollection<FilePath> filesToQuery;
		/// <summary>
		/// Список путей к файлам, к которым нужно сделать запрос.
		/// Файлы перезагружаются при каждом запросе
		/// </summary>
		public ObservableCollection<FilePath> FilesToQuery
		{
			get { return filesToQuery; }
		}

		private string sparqlQueryText;
		/// <summary>
		/// Текст запроса для метода ExecuteQuery()
		/// </summary>
		public string SparqlQueryText
		{
			get { return this.sparqlQueryText; }
			set
			{
				this.sparqlQueryText = value;
				OnPropertyChanged("SparqlQueryText");
			}
		}

		private string queryResult;
		/// <summary>
		/// Результат последнего Sparql-запроса
		/// </summary>
		public string QueryResult
		{
			get { return this.queryResult; }
			private set
			{
				this.queryResult = value;
				OnPropertyChanged("QueryResult");
			}
		}

		#endregion

		//TODO: Добавить описатели исключений

		#region PublicMethods

		/// <summary>
		/// Добавляет в коллекцию FilesToQuery ссылку на файл
		/// </summary>
		/// <param name="filePath">Имя файла</param>
		public void AddRdfFile(string filePath)
		{
			FilesToQuery.Add(new FilePath(filePath));
		}

		/// <summary>
		/// Выполнение Sparql-запроса из SparqlQueryText
		/// </summary>
		/// <returns>Результат запроса в виде текста</returns>
		public string ExecuteQuery()
		{
			return ExecuteQuery(this.sparqlQueryText);
		}

		/// <summary>
		/// Выполнение Sparql-запроса
		/// </summary>
		/// <param name="sparqlCommandText">Текст Sparql-запроса</param>
		/// <returns>Результат запроса в виде текста</returns>
		public string ExecuteQuery(string sparqlCommandText)
		{
			using (Graph baseGraph = new Graph())
			{
				RdfXmlParser fileParser = new RdfXmlParser();
				foreach (string fileName in FilesToQuery)
				{
					if (String.IsNullOrWhiteSpace(fileName))
						continue;

					using (Graph g = new Graph())
					{
						try
						{
							fileParser.Load(g, fileName);
							baseGraph.Merge(g);
						}
						catch (Exception ex)
						{
							throw new Exception(String.Format("Ошибка при обработке файла {0}\r\n{1}",fileName, ex.Message), ex);
						}
					}
				}

				var resultSet = baseGraph.ExecuteQuery(sparqlCommandText);

				if (resultSet is SparqlResultSet)
				{
					SparqlResultSet outputSet = resultSet as SparqlResultSet;

					if (outputSet.IsEmpty)
						QueryResult = "Пустой результат";
					else
					{
						StringBuilder outputString = new StringBuilder();
						foreach (SparqlResult result in outputSet.Results)
							outputString.AppendLine(result.ToString());

						QueryResult = outputString.ToString();
					}
				}
				else if (resultSet is Graph)
				{
					Graph resultGraph = resultSet as Graph;

					if (resultGraph.IsEmpty)
						QueryResult = "Пустой граф";
					else
						QueryResult = VDS.RDF.Writing.StringWriter.Write(resultGraph, new RdfXmlWriter());
				}
				else
				{
					QueryResult = string.Format("Неизвестный результат: {0}", resultSet.GetType());
				}

				return QueryResult;
			}
		}

		#endregion
	}
}
