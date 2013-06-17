using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SemanticDataEnrichment.Core;
using System.IO;

namespace SemanticDataEnrichment.UI.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			string tmpFileName = "test.txt"; //TODO:Тут и далее - разобраться с параметрами и вынести в конфиг
			this.DataContext = new ProcessViewModel("Tomita\\tomitaparser.exe", "Tomita\\config.proto", tmpFileName, "facts.xml", "output.rdf");
			if (File.Exists(tmpFileName)) 
				CurrentContext.OpenFile(tmpFileName);
		}

		/// <summary>
		/// Акцессор для view model данного окна.
		/// </summary>
		public ProcessViewModel CurrentContext
		{
			get
			{
				return this.DataContext as ProcessViewModel;
			}
		}

		/// <summary>
		/// Версия данного интерфейса
		/// </summary>
		public Version CurrentUIVersion
		{
			get
			{
				return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		/// <summary>
		/// Версия библиотеки с логикой
		/// </summary>
		public Version CurrentCoreVersion
		{
			get
			{
				return CurrentContext.GetCurrentVersion();
			}
		}

		private void UrlButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (new WaitCursor())
				{
					CurrentContext.OpenUrl();
				}
			}
			catch (Exception ex)
			{
				App.ShowError(ex);
			}
		}

		private void FileButton_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog() { Filter = "Текстовые фалы (.txt)|*.txt|Все фалы|*.*", DefaultExt = "txt" };
			bool? result = dlg.ShowDialog();
			if (result.GetValueOrDefault() == true)
				try
				{
					using (new WaitCursor())
					{
						CurrentContext.OpenFile(dlg.FileName);
					}
				}
				catch (Exception ex)
				{
					App.ShowError(ex);
				}
		}

		private void ProcessButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (new WaitCursor())
				{
					string prettyOutput = "Debug.html"; //TODO: в конфиг!
					if (File.Exists(prettyOutput))
						File.Delete(prettyOutput);
					if (String.IsNullOrEmpty(CurrentContext.TextData) && !String.IsNullOrEmpty(CurrentContext.URL.ToLower().Replace("http:\\\\", "")))
						CurrentContext.OpenUrl();

					CurrentContext.ProcessText();

					if (File.Exists(prettyOutput))
						WebBr.NavigateToString(File.ReadAllText(prettyOutput));
					else
						WebBr.Navigate("about:blank");

					SemanticElementsWindow sem = new SemanticElementsWindow(CurrentContext) { Owner = this };
					sem.Show();
				}
			}
			catch (Exception ex)
			{
				App.ShowError(ex);
			}
		}

		private void ClearInputButton_Click(object sender, RoutedEventArgs e)
		{
			CurrentContext.TextData = String.Empty;
		}

		private void ClearOutputButton_Click(object sender, RoutedEventArgs e)
		{
			CurrentContext.ClearOutput();
		}

		private void QueriesButton_Click(object sender, RoutedEventArgs e)
		{
			SparqlQueryWindow queryWindow = new SparqlQueryWindow();
			queryWindow.Owner = this;
			queryWindow.Show();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (CurrentContext != null
				&& (!String.IsNullOrWhiteSpace(CurrentContext.ConsoleOutput) || !String.IsNullOrWhiteSpace(CurrentContext.ProcessedXmlData) || !String.IsNullOrWhiteSpace(CurrentContext.ProcessedRdfData))
				&& MessageBox.Show("Закрыть прогармму?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
				e.Cancel = true;
		}
	}
}

