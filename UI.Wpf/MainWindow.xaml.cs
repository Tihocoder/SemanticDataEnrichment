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
			this.DataContext = new ProcessViewModel("Tomita\\tomitaparser.exe", "facts.xml");
			if (File.Exists("test.txt")) //TODO: Временно сюда
				CurrentContext.OpenFile("test.txt");
		}

		public ProcessViewModel CurrentContext
		{
			get
			{
				return this.DataContext as ProcessViewModel;
			}
		}

		private void UrlButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CurrentContext.OpenUrl();
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
			}
		}

		private void ShowError(string message)
		{
			MessageBox.Show(message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private void FileButton_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog() { Filter = "Текстовые фалы (.txt)|*.txt|Все фалы|*.*", DefaultExt = "txt" };
			bool? result = dlg.ShowDialog();
			if (result.GetValueOrDefault() == true)
				try
				{
					CurrentContext.OpenFile(dlg.FileName);
				}
				catch (Exception ex)
				{
					ShowError(ex.Message);
				}
		}

		private void ProcessButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (String.IsNullOrEmpty(CurrentContext.TextData) && !String.IsNullOrEmpty(CurrentContext.URL.ToLower().Replace("http:\\\\", "")))
					CurrentContext.OpenUrl();
				CurrentContext.ProcessText();
				if (File.Exists("Debug.html")) //TODO: в конфиг!
				{
					string html = File.ReadAllText("Debug.html");
					WebBr.NavigateToString(html);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
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

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (CurrentContext != null
				&& (!String.IsNullOrWhiteSpace(CurrentContext.ConsoleOutput) || !String.IsNullOrWhiteSpace(CurrentContext.ProcessedTextData))
				&& MessageBox.Show("Закрыть прогармму?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
				e.Cancel = true;
		}

		public Version CurrentUIVersion
		{
			get
			{
				return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		public Version CurrentCoreVersion
		{
			get
			{
				return CurrentContext.GetCurrentVersion();
			}
		}

	}
}

