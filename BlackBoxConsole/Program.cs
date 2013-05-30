using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace SemanticDataEnrichment.BlackBoxConsole
{
	internal class Program
	{
		private const string OUTPUT_FILE_NAME = "Output.xml";

		static void Main(string[] args)
		{
			if (args == null || args.Length < 1)
			{
				ShowMessage("Не заданы параметры. Укажите путь к файлу для парсинга.");
				return;
			}

			string inputPath = args[0];
			if (!File.Exists(inputPath))
			{
				ShowMessage("Файл по адресу: {0} не найден.", inputPath);
				return;
			}


			try
			{
				string fileData = File.ReadAllText(inputPath);
				XElement xmlData = GenerateXML(fileData);
				string outputDir = OUTPUT_FILE_NAME.Replace(OUTPUT_FILE_NAME.Split('\\').Last(), "");
				if (!String.IsNullOrWhiteSpace(outputDir) && !Directory.Exists(outputDir))
					Directory.CreateDirectory(outputDir);
				if (File.Exists(OUTPUT_FILE_NAME))
					File.Delete(OUTPUT_FILE_NAME);
				xmlData.Save(OUTPUT_FILE_NAME);
			}
			catch (Exception ex)
			{
				ShowMessage("Ошибка при обработке:\r\n{0}", ex.Message);
				return;
			}
		}

		static XElement GenerateXML(string inputData)
		{
			XElement outputData =
				new XElement("Root",
					new XElement("Contact",
						new XElement("Name", "Patrick Hines"),
						new XElement("Phone", "206-555-0144"),
						new XElement("Address",
							new XElement("Street", "123 Main St"),
							new XElement("City", "Mercer Island"),
							new XElement("State", "WA"),
							new XElement("Postal", "68042")
						)
					),
					new XElement("InputData", inputData)
				);
			return outputData;
		}

		static void ShowMessage(string message, params object[] args)
		{
			ShowMessage(String.Format(message, args));
		}

		static void ShowMessage(string message)
		{
			Console.WriteLine(message);
			Console.ReadKey();
		}
	}
}
