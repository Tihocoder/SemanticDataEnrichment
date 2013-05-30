using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace SemanticDataEnrichment.Core
{//ejp73
	public class ProcessViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}

		#endregion

		private string fileName;
		private string url;
		private string textData;
		private string processedTextData;
		private string workConsolePath;
		private string consoleOutput;
		private string outputConsoleFileName;

		public ProcessViewModel(string workConsolePath, string outputConsoleFileName)
		{
			this.workConsolePath = workConsolePath;
			this.outputConsoleFileName = outputConsoleFileName;
			this.url = "http:\\\\";
		}

		#region PublicProperties

		public string FileName
		{
			get { return this.fileName; }
			set
			{
				this.fileName = value;
				OnPropertyChanged("FileName");
			}
		}

		public string URL
		{
			get { return this.url; }
			set
			{
				this.url = value;
				OnPropertyChanged("URL");
			}
		}


		public string TextData
		{
			get { return this.textData; }
			set
			{
				this.textData = value;
				OnPropertyChanged("TextData");
			}
		}

		public string ProcessedTextData
		{
			get { return this.processedTextData; }
			private set
			{
				this.processedTextData = value;
				OnPropertyChanged("ProcessedTextData");
			}
		}

		public string WorkConsolePath
		{
			get { return this.workConsolePath; }
			set
			{
				this.workConsolePath = value;
				OnPropertyChanged("WorkConsolePath");
			}
		}

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

		#endregion

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

			string inputFilePath = CreateTmpFile(textData);

			string outputConsole = ProcessWorkConsole("Tomita\\config.proto");//TODO: Аргумент во вне
			if (!String.IsNullOrWhiteSpace(outputConsole))
			{
				XElement xmlData = XElement.Parse(outputConsole);

				ProcessXml(xmlData);
				ProcessedTextData = xmlData.ToString();
			}
			else
				ProcessedTextData = outputConsole;
		}

		public void ClearOutput()
		{
			ConsoleOutput = String.Empty;
			ProcessedTextData = String.Empty;
		}

		public Version GetCurrentVersion()
		{
			return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
		}

		#endregion

		#region PrivateMethods

		private string CreateTmpFile(string content)
		{
			string filePath = "test.txt";//TODO: То же в конфиг
			//string filePath = Path.GetTempFileName();
			if (File.Exists(filePath)) 
				File.Delete(filePath);
			File.AppendAllText(filePath, content);

			//File f = File.Op
			return filePath;
		}

		private string ProcessWorkConsole(string consoleArguments)
		{
			if (!File.Exists(this.workConsolePath))
				throw new WorkConsloeException(String.Format("Не найдена консоль по пути {0}", this.workConsolePath));

			using (Process workConsole = new Process()
			{
				StartInfo = new ProcessStartInfo(this.workConsolePath, consoleArguments)
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,
					
				},
			})
			{
				//workConsole.StartInfo.
				if (workConsole.Start())
				{
					using (StreamReader consoleErrors = new StreamReader(workConsole.StandardError.BaseStream, Encoding.UTF8))
					{
						using (StreamReader consoleOutoput = new StreamReader(workConsole.StandardOutput.BaseStream, Encoding.UTF8))
						{
							//while (!workConsole.HasExited)
							//{
							//    //string errors = consoleErrors.ReadLine();
							//    //if (!String.IsNullOrWhiteSpace(errors))
							//        //throw new WorkConsloeException("Ошибка при обработке текста консолью", errors);
							//    ConsoleOutput = ConsoleOutput + consoleOutoput.ReadLine();
							//    workConsole.WaitForExit(100);
							//}
							string errors = consoleErrors.ReadToEnd();
							ConsoleOutput = consoleOutoput.ReadToEnd();
							ConsoleOutput += errors;
							workConsole.WaitForExit();
						}
					}
				}
				else
					throw new WorkConsloeException("Не удалось инициализировать процесс консоли");
			}

			if (File.Exists(this.outputConsoleFileName)) //TODO: Разобраться с выводом консоли
				//    throw new WorkConsloeException(String.Format("Не удалось обнаружить файл с итогом работы косоли")); 
				return File.ReadAllText(this.outputConsoleFileName);
			else
				return String.Empty;
		}

		private void ProcessXml(XElement xml)
		{ }

		#endregion
	}
}
