using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using SemanticDataEnrichment.Core.ModelComponents;
using System.Collections.ObjectModel;
using SemanticDataEnrichment.Core.SemanticElements;
using System.Collections.Generic;

namespace SemanticDataEnrichment.Core
{
	/// <summary>
	/// Модель-диспетчер, обеспечивающая логику работы с Томита-консолью
	/// </summary>
	public class ProcessViewModel : NotifyPropertyChangedBase, ISemanticModel
	{
		private string tomitaPath;
		private string tomitaConfigPath;
		private string tomitaOutputFileName;
		private string tomitaInputFileName;
		private string rdfOutputFileName;

		/// <summary>
		/// Модель-диспетчер, обеспечивающая логику работы с Томита-консолью
		/// </summary>
		/// <param name="tomitaPath">Путь к томита-консоли</param>
		/// <param name="tomitaConfigPath">Путь к фалу-конфигу томиты</param>
		/// <param name="tomitaInputFileName">Имя для временного файла со входным текстом для томиты</param>
		/// <param name="tomitaOutputFileName">Имя файла, возвращаемого томитой</param>
		/// <param name="rdfOutputFileName">Имя файла для сохранения сконвертированных данных</param>
		public ProcessViewModel(string tomitaPath, string tomitaConfigPath, string tomitaInputFileName, string tomitaOutputFileName, string rdfOutputFileName)
		{
			this.tomitaPath = tomitaPath;
			this.tomitaConfigPath = tomitaConfigPath;
			this.tomitaOutputFileName = tomitaOutputFileName;
			this.tomitaInputFileName = tomitaInputFileName;
			this.rdfOutputFileName = rdfOutputFileName;
			URL = "http://";
		}

		#region PublicProperties

		private string fileName;
		/// <summary>
		/// Имя файла для загрузки данных в TextData
		/// </summary>
		public string FileName
		{
			get { return this.fileName; }
			set
			{
				this.fileName = value;
				OnPropertyChanged("FileName");
			}
		}

		private string url;
		/// <summary>
		/// URL HTTP-протокола для загрузки данных в TextData
		/// </summary>
		public string URL
		{
			get { return this.url; }
			set
			{
				this.url = value;
				OnPropertyChanged("URL");
			}
		}

		private string textData;
		/// <summary>
		/// Данные для обработки консоли
		/// </summary>
		public string TextData
		{
			get { return this.textData; }
			set
			{
				this.textData = value;
				OnPropertyChanged("TextData");
			}
		}

		private string processedXmlData;
		/// <summary>
		/// Обработанные тамитой данные (xml)
		/// </summary>
		public string ProcessedXmlData
		{
			get { return this.processedXmlData; }
			private set
			{
				this.processedXmlData = value;
				OnPropertyChanged("ProcessedXmlData");
			}
		}

		private string processedRdfData;
		/// <summary>
		/// Сконвертированные в RDF обработанные томитой данные
		/// </summary>
        public string ProcessedRdfData
        {
            get { return this.processedRdfData; }
            private set
            {
                this.processedRdfData = value;
				OnPropertyChanged("ProcessedRdfData");
            }
        }

		private string processedQueryData;
		/// <summary>
		/// Результат запроса к итогам работы томиты и схемам
		/// </summary>
		public string ProcessedQueryData
		{
			get { return this.processedQueryData; }
			private set
			{
				this.processedQueryData = value;
				OnPropertyChanged("ProcessedQueryData");
			}
		}

		private IEnumerable<SemanticElement> semanticElements;
		/// <summary>
		/// Результат запроса к итогам работы томиты и схемам в виде семантических объектов
		/// </summary>
		public IEnumerable<SemanticElement> SemanticElements
		{
			get { return this.semanticElements; }
			private set
			{
				this.semanticElements = new ObservableCollection<SemanticElement>(value);
				OnPropertyChanged("SemanticElements");
			}
		}

		private string consoleOutput;
		/// <summary>
		/// Дополнительная информация, возвращаемая томитой в ходе работы (в т.ч. ошибки)
		/// </summary>
		public string ConsoleOutput
		{
			get { return this.consoleOutput; }
			private set
			{
				if (this.consoleOutput != value)
				{
					this.consoleOutput = value;
					OnPropertyChanged("ConsoleOutput");
				}
			}
		}

		private string semanticHTML;
		public string SemanticHTML
		{
			get { return this.semanticHTML; }
			private set
			{
				this.semanticHTML = value;
				OnPropertyChanged("SemanticHTML");
			}
		}

		#endregion

		//TODO: корректно описать исключения у методов

		#region PublicMethods

		/// <summary>
		/// Загрузить данные TextData из файла, указанного в FileName
		/// </summary>
		/// <exception cref="System.ArgumentNullException">Не задано имя фала в FileName</exception>
		/// <exception cref="System.IO.FileNotFoundException">Файл по указанному пути не найден</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException"/>
		/// <exception cref="System.IO.PathTooLongException"/>
		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.UnauthorizedAccessException"/>
		/// <exception cref="System.Security.SecurityException"/>
		public void OpenFile()
		{
			OpenFile(this.fileName);
		}

		/// <summary>
		/// Загрузить данные TextData из файла
		/// </summary>
		/// <param name="fileName">Полный путь и имя файла с данными</param>
		/// <exception cref="System.ArgumentNullException">Не задано имя фала</exception>
		/// <exception cref="System.IO.FileNotFoundException">Файл по указанному пути не найден</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException"/>
		/// <exception cref="System.IO.PathTooLongException"/>
		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.UnauthorizedAccessException"/>
		/// <exception cref="System.Security.SecurityException"/>
		public void OpenFile(string fileName)
		{
			if (String.IsNullOrWhiteSpace(fileName))
				throw new ArgumentNullException("FileName", "Не определно имя файла");

			TextData = File.ReadAllText(fileName);
		}

		/// <summary>
		/// Загрузить данные TextData из интернета (протокол HTTP), по ссылке, указанной в URL
		/// </summary>
		/// <exception cref="System.ArgumentNullException">Не задан URL</exception>
		/// <exception cref="System.UriFormatException">Не верный URL</exception>
		/// <exception cref="System.Security.SecurityException"/>
		/// <exception cref="System.Net.ProtocolViolationException"/>
		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.OutOfMemoryException"/>
		public void OpenUrl()
		{
			OpenUrl(this.url);
		}

		/// <summary>
		/// Загрузить данные TextData из интернета (протокол HTTP)
		/// </summary>
		/// <param name="url">HTTP URL</param>
		/// <exception cref="System.ArgumentNullException">Не задан URL</exception>
		/// <exception cref="System.UriFormatException">Не верный URL</exception>
		/// <exception cref="System.Security.SecurityException"/>
		/// <exception cref="System.Net.ProtocolViolationException"/>
		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.OutOfMemoryException"/>
		public void OpenUrl(string url)
		{

			if (String.IsNullOrWhiteSpace(url))
				throw new ArgumentNullException("URL", "Не определен URL");

			WebRequest request = WebRequest.Create(url);
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{

				string responseText = (new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(response.CharacterSet))).ReadToEnd();


				//stringstr = "<h1>Получить текст HTML C# отсюда</h1>";
				//Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
				//TextData = reg.Replace(responseText, "");
				TextData = responseText;

			}
		}

		/// <summary>
		/// Обработать текст из TextData
		/// </summary>
		public void ProcessText()
		{
			ProcessText(this.textData);
		}

		/// <summary>
		/// Обработать заданный текст
		/// </summary>
		/// <param name="textData">Текст для обработки</param>
		/// <exception cref="SemanticDataEnrichment.Core.WorkConsloeException">Ошибки при работе консоли</exception>
		public void ProcessText(string textData)
		{
			if (String.IsNullOrWhiteSpace(textData))
				throw new ArgumentNullException("TextData", "Не задан текст для обработки");

			File.WriteAllText(this.tomitaInputFileName, textData);
			if (File.Exists(this.tomitaOutputFileName))
				File.Delete(this.tomitaOutputFileName);
			if (File.Exists(this.rdfOutputFileName))
				File.Delete(this.rdfOutputFileName);

			ProcessTomitaConsole(this.tomitaConfigPath);

			if (File.Exists(this.tomitaOutputFileName))
			{
				XmlRdfParser parser = new XmlRdfParser();
				parser.ConvertXmlRdf(this.tomitaOutputFileName, this.rdfOutputFileName);
				ProcessedXmlData = XmlRdfParser.GetFormattedStringFromXmlFile(this.tomitaOutputFileName);
				ProcessedRdfData = XmlRdfParser.GetFormattedStringFromXmlFile(this.rdfOutputFileName);
				ProcessedQueryData = ExecuteFileQuery("query.txt", parser.GetBestPropertyValue("CompanyName", ProcessedRdfData));
				SemanticElements = XmlRdfParser.ConvertRdfToSemanticElements(ProcessedQueryData);
			}
			else
			{
				ProcessedXmlData = String.Empty;
				ProcessedRdfData = String.Empty;
			}
		}

		public string CreateSemanticHTML()
		{
			if (String.IsNullOrWhiteSpace(TextData))
				return String.Empty;

			if (SemanticElements == null || !SemanticElements.Any())
				return textData;

			return CreateSemanticHTML(TextData, SemanticElements);
		}

		public string CreateSemanticHTML(string inputHTML, IEnumerable<SemanticElement> semanticElements)
		{
			string targetTag = "</HEAD>";
			StringBuilder sb = new StringBuilder(targetTag);
			foreach (var el in semanticElements)
				sb.AppendLine(el.GetHtml());

			SemanticHTML = inputHTML.Replace(targetTag, sb.ToString());
			return SemanticHTML;
		}

		/// <summary>
		/// Очищает переменные вывода (ConsoleOutput, ProcessedXmlData, ProcessedRdfData)
		/// </summary>
		public void ClearOutput()
		{
			ConsoleOutput = String.Empty;
			ProcessedXmlData = String.Empty;
			ProcessedRdfData = String.Empty;
		}

		public Version GetCurrentVersion()
		{
			return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
		}

		#endregion

		#region PrivateMethods

		private void ProcessTomitaConsole(string consoleArguments)
		{
			if (!File.Exists(this.tomitaPath))
                throw new Exception(String.Format("Не найдена томита по пути {0}", this.tomitaPath));


			using (Process workConsole = new Process()
			{
				StartInfo = new ProcessStartInfo(this.tomitaPath, consoleArguments)
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,
					
				},
			})
			{
				if (workConsole.Start())
				{
					using (StreamReader consoleErrors = new StreamReader(workConsole.StandardError.BaseStream, Encoding.UTF8))
					{
						using (StreamReader consoleOutoput = new StreamReader(workConsole.StandardOutput.BaseStream, Encoding.UTF8))
						{
							string errors = consoleErrors.ReadToEnd();
							ConsoleOutput = consoleOutoput.ReadToEnd();
							ConsoleOutput += errors;
							workConsole.WaitForExit();
						}
					}
				}
				else
					throw new Exception("Не удалось инициализировать процесс томиты");
			}
		}

		private string ExecuteFileQuery(string fileName, string param)
		{
			string sparqlCommandText = File.ReadAllText(fileName).Replace("{0}", param);

			RdfQueryViewModel queryModel = new RdfQueryViewModel();
			queryModel.AddRdfFile("FdoDS.rdf");
			queryModel.AddRdfFile("schema.xml");
			queryModel.AddRdfFile(this.rdfOutputFileName);
			return queryModel.ExecuteQuery(sparqlCommandText);
		}

		#endregion
	}
}
